using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;

using System.Configuration;

using System.Data;
using TheEMIClubApplication.AppCode;
using AVFramework;
using System.Data.SqlClient;
using TheEMIClubApplication.BussinessLayer;
using Twilio;
using AjaxControlToolkit;
using TheEMIClubApplication.Model;
using System.Net;
using System.Net.Http;

namespace TheEMIClubApplication.Admin

{
    public partial class PayoutPaymentApproval : System.Web.UI.Page
    {
        string BOSpayBaseUrl = ConfigurationManager.AppSettings["payoutBaseUrl"].ToString();
        string OQMerchnatCode = ConfigurationManager.AppSettings["OQMerchnarcode"].ToString();
        BLTransfertoAgent ObjpendingPayout = new BLTransfertoAgent();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortalCommon.BindDropDownList(ddlRetailer, "GetRetailer", string.Empty,
                     string.Empty, string.Empty, string.Empty, string.Empty, "-- Select All --");
                //  LoadRetailers();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
                    if (ddlApproveType.SelectedValue == "Payout")
            {
                BindPendingApprovalGridPayout();
            }
            else if (ddlApproveType.SelectedValue == "HoldAmt")
            {
                BindPendingApprovalGridHoldAmt();
                ApplyButtonVisibility();
            }

   
        }

        private void BindPendingApprovalGridPayout()
        {
            BLTransfertoAgent ObjpendingPayout = new BLTransfertoAgent();
            try
            {
                // 🔹 Parse dates if entered, otherwise pass null
                ObjpendingPayout.FromDate = string.IsNullOrWhiteSpace(txtFromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txtFromDate.Text.Trim()).Date;

                ObjpendingPayout.ToDate = string.IsNullOrWhiteSpace(txtToDate.Text)
                                          ? (DateTime?)null
                                          : DateTime.Parse(txtToDate.Text.Trim()).Date;

                // 🔹 Retailer ID optional
                ObjpendingPayout.RegistrationId =
                 string.IsNullOrWhiteSpace(ddlRetailer.SelectedValue) || ddlRetailer.SelectedValue == "0"
                 ? null
                 : ddlRetailer.SelectedValue;

                // 🔹 Get data from stored procedure
                DataTable dtCompany = ObjpendingPayout.GetPendingPayout();

                if (dtCompany != null && dtCompany.Rows.Count > 0)
                {
                    pnlResults.Visible = true;
                    holdingpanel.Visible = false;
                    gvResults.PageSize = PortalCommon.GetGridPageSize;
                    gvResults.DataSource = dtCompany;
                    gvResults.DataBind();

                    string SucessMessage = "Payout Pending Records Retrieve Successfully";

                    // toastr.success must be lowercase
                    string script = $"toastr.success('{SucessMessage}', 'Success');";

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "showToastrSuccess",
                        script,
                        true
                    );
                }
                else
                {
                    holdingpanel.Visible = false;
                    pnlResults.Visible = false;
                    gvResults.DataSource = null;
                    gvResults.DataBind();

                    string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");


                    string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript(
                   this,
                   this.GetType(),
                   "showToastrError",
                   script,
                   true
               );
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            finally
            {
                ObjpendingPayout = null;
            }
        }


        private void ExecuteHoldAmountTransfer()
        {
            try
            {
                GenerateRefrenceId();
                // Set up the user details
                objTransfertoAgent.RegistrationId = spnHoldTo.InnerText;
                objTransfertoAgent.TransferFrom = "Admin";
                objTransfertoAgent.TransferTo = spnHoldTo.InnerText;
                objTransfertoAgent.Remark = "Deposit From Holding Amount";
                objTransfertoAgent.TransferAmt = Convert.ToDecimal(spnHoldAmt.InnerText.Trim());
                objTransfertoAgent.TransferFromMsg = $"Holding amount of {objTransfertoAgent.TransferAmt:N2} transferred from Retailer (ID: {spnHoldTo.InnerText}) to Wallet against Transaction ID: {spnHoldTxnId.InnerText.Trim()}.";
                objTransfertoAgent.TransferToMsg = $"Holding amount of {objTransfertoAgent.TransferAmt:N2} transferred to Admin account against Transaction ID: {spnHoldTxnId.InnerText.Trim()}.";
                objTransfertoAgent.Amount_Type = "Deposit";
                objTransfertoAgent.ActualCommissionAmount = Convert.ToDecimal("0.00");
                objTransfertoAgent.GSTAMT = Convert.ToDecimal("0.00");
                objTransfertoAgent.TdsAmt = Convert.ToDecimal("0.00");
                objTransfertoAgent.commissionWithoutGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.TransIpAddress = AppSessions.SessionLocalHostIP;
                objTransfertoAgent.Services_charge_Amt = Convert.ToDecimal("0.00");
                objTransfertoAgent.Services_charge_GSTAmt = Convert.ToDecimal("0.00");
                objTransfertoAgent.Services_charge_Without_GST = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommission = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommissionGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommissionWithoutGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.Amt_Transfer_TransID = RefrenceId;
                objTransfertoAgent.Holdingamt = "0.00";
                objTransfertoAgent.Actual_Transaction_Amount = Convert.ToDecimal(spnHoldAmt.InnerText.Trim());
                objTransfertoAgent.TransertoAgent();
                objTransfertoAgent.RefrenceId = RefrenceId;
                objTransfertoAgent.ApprovedStatus = "Approved";
                objTransfertoAgent.DepositUpdate();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        public static string @RefrenceId;
        private void GenerateRefrenceId()
        {
            try
            {
                // Assuming you have a text box for the input series code
                string inputSeriesCode = "Transactionid"; // Get the series code from a TextBox

                // Output parameter to capture the generated series code
                string outputSeriesCode;

                // Call the RowCount method
                string result = objTransfertoAgent.RowCount(inputSeriesCode, out outputSeriesCode);

                // Check if the result is valid and display the output
                if (!string.IsNullOrEmpty(outputSeriesCode))
                {

                    @RefrenceId = outputSeriesCode; // Display the output series code in a TextBox
                }
                else
                {

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB); // Log SQL exception
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex); // Log general exception
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
        }

        private void BindPendingApprovalGridHoldAmt()
        {
            BLTransfertoAgent ObjpendingPayout = new BLTransfertoAgent();
            try
            {
                // 🔹 Parse dates if entered, otherwise pass null
                ObjpendingPayout.FromDate = string.IsNullOrWhiteSpace(txtFromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txtFromDate.Text.Trim()).Date;

                ObjpendingPayout.ToDate = string.IsNullOrWhiteSpace(txtToDate.Text)
                                          ? (DateTime?)null
                                          : DateTime.Parse(txtToDate.Text.Trim()).Date;



                if (ddlRetailer.SelectedValue == "0")
                {
                    ObjpendingPayout.RegistrationId = null;
                }
                else
                {
                    ObjpendingPayout.RegistrationId = ddlRetailer.SelectedValue;
                }

           

                // 🔹 Get data from stored procedure
                DataTable dtCompany = ObjpendingPayout.GetPendingHoldAmt();

                if (dtCompany != null && dtCompany.Rows.Count > 0)
                {
                    holdingpanel.Visible = true;
                    pnlResults.Visible = false;
                    gvHoldingamt.PageSize = PortalCommon.GetGridPageSize;
                    gvHoldingamt.DataSource = dtCompany;
                    gvHoldingamt.DataBind();
                    string SucessMessage = "Holding Pending Records Retrieve Successfully";

                    // toastr.success must be lowercase
                    string script = $"toastr.success('{SucessMessage}', 'Success');";

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "showToastrSuccess",
                        script,
                        true
                    );
                }
                else
                {   
                    holdingpanel.Visible = false;
                    gvHoldingamt.DataSource = null;
                    gvHoldingamt.DataBind();


                    string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");


                    string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript(
                   this,
                   this.GetType(),
                   "showToastrError",
                   script,
                   true
               );

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            finally
            {
                ObjpendingPayout = null;
            }
        }
        // 🔹 Paging event
        protected void gvResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResults.PageIndex = e.NewPageIndex;
            BindPendingApprovalGridPayout();
        }

        protected void gvHoldingamt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHoldingamt.PageIndex = e.NewPageIndex;
            BindPendingApprovalGridHoldAmt();
        }


        protected void gvResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApprovePayout")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvResults.Rows[rowIndex];

                // Pull values safely from DataKeys
                string referenceId = gvResults.DataKeys[rowIndex]["RefrenceID"].ToString();
                string registrationId = gvResults.DataKeys[rowIndex]["RegistrationId"].ToString();
                string TransferAmt = gvResults.DataKeys[rowIndex]["PayoutAmount"].ToString();
                string Services_charge_Amt = gvResults.DataKeys[rowIndex]["ServiceChargeAmt"].ToString();
                string Services_charge_GSTAmt = gvResults.DataKeys[rowIndex]["GSTAmt"].ToString();
                string amount = gvResults.DataKeys[rowIndex]["TransferAmt"].ToString();
                string paymentMode = gvResults.DataKeys[rowIndex]["PaymentMode"].ToString();
                string paymentDate = gvResults.DataKeys[rowIndex]["PaymentDate"].ToString();
                string accountHolder = gvResults.DataKeys[rowIndex]["AccountHolder"].ToString();
                string bankName = gvResults.DataKeys[rowIndex]["DepositBankName"].ToString();
                string branchName = gvResults.DataKeys[rowIndex]["BranchName"].ToString();
                string ifscCode = gvResults.DataKeys[rowIndex]["IFSCCode"].ToString();
                string AccountNo = gvResults.DataKeys[rowIndex]["BeneId"].ToString();

                // Hidden field
                hdnRefrenceID.Value = referenceId;

                // Assign values to modal spans
                spnRefId.InnerText = referenceId;
                spnRegId.InnerText = registrationId;
                spnPayoutAmount.InnerText = TransferAmt;
                spnServiceChargeAmt.InnerText = Services_charge_Amt;
                spnGSTAmt.InnerText = Services_charge_GSTAmt;
                spnAmount.InnerText = amount;
                spnPayMode.InnerText = paymentMode;
                spnPayDate.InnerText = paymentDate;
                spnAccHolder.InnerText = accountHolder;
                spnBank.InnerText = bankName;
                spnBranch.InnerText = branchName;
                spnIFSC.InnerText = ifscCode;
                spnAccountNo.InnerText = AccountNo;

                // Reset modal fields
                ddlApprovalStatus.SelectedIndex = 0;
                txtApprovalRemarks.Text = "";

                // Show Bootstrap modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal",
                    "$('#approvalModal').modal('show');", true);
            }
        }


        protected void gvHoldingamt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApproveHoldAmt")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvHoldingamt.Rows[rowIndex];

                string txnId = gvHoldingamt.DataKeys[rowIndex].Values["Amt_Transfer_TransID"].ToString();
                string amount = gvHoldingamt.DataKeys[rowIndex].Values["TransferAmt"].ToString();
                string status = gvHoldingamt.DataKeys[rowIndex].Values["TransactionStatus"].ToString();
                // Fill modal fields
                spnHoldTxnId.InnerText = txnId;
                spnHoldTxnDate.InnerText = row.Cells[1].Text; // Or bind properly
                spnHoldFrom.InnerText = row.Cells[2].Text;
                spnHoldTo.InnerText = row.Cells[3].Text;
                spnHoldAmt.InnerText = amount;
                spnHoldStatus.InnerText = status;
                hdnHoldAmtId.Value = txnId;
                // Show modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal",
                    "$('#holdAmtApprovalModal').modal('show');", true);
            }       
        }
        private void ApplyButtonVisibility()
        {
            for (int i = 0; i < gvHoldingamt.Rows.Count; i++)
            {
                GridViewRow row = gvHoldingamt.Rows[i];

                string status = gvHoldingamt.DataKeys[i]["TransactionStatus"].ToString();

                Button btnApprove = row.FindControl("btnApprove") as Button;

                if (btnApprove != null)
                {
                    if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ||
                        status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
                    {
                        btnApprove.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = true;   // Pending or Null
                    }
                }
            }
        }
        protected void btnApproveSelected_Click(object sender, EventArgs e)
        {
            try
            {
                bool anySelected = false;
                List<string> failedMessages = new List<string>();

                foreach (GridViewRow row in gvResults.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                    if (chk != null && chk.Checked)
                    {
                        anySelected = true;

                        string refId = gvResults.DataKeys[row.RowIndex]["ReferenceID"].ToString();
                        string accountNo = gvResults.DataKeys[row.RowIndex]["BeneId"].ToString();
                        string amount = gvResults.DataKeys[row.RowIndex]["TransferAmt"].ToString();
                        string ifsc = gvResults.DataKeys[row.RowIndex]["IFSCCode"].ToString();
                        string accHolder = gvResults.DataKeys[row.RowIndex]["AccountHolder"].ToString();
                        string registrationID = gvResults.DataKeys[row.RowIndex]["RegistrationId"].ToString();
                        string Emailid = gvResults.DataKeys[row.RowIndex]["EmailID"].ToString();
                        string mobileNo = gvResults.DataKeys[row.RowIndex]["MobileNo"].ToString();
                        string PaymentMode = gvResults.DataKeys[row.RowIndex]["PaymentMode"].ToString();

                        // Call payout without delay
                        string result = PayoutApprovedSync(Emailid, mobileNo, accountNo, amount, ifsc, accHolder, OQMerchnatCode, refId, PaymentMode);
                        if (!string.IsNullOrEmpty(result))
                        {
                            failedMessages.Add(result);
                        }
                    }
                }

                if (!anySelected)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "toastr.warning('Please select at least one payout.');", true);
                    return;
                }

                BindPendingApprovalGridPayout();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                //    "toastr.success('Selected payouts processed successfully.');", true);

                // Show modal if there are failed payouts
                if (failedMessages.Count > 0)
                {
                    string failedList = string.Join("", failedMessages.Select(m => $"<li>{m}</li>"));
                    string script = $@"
        var modalEl = document.getElementById('failedPayoutsModal');
        var modal = new bootstrap.Modal(modalEl);
        document.getElementById('failedPayoutsList').innerHTML = `{failedList}`;
        modal.show();
    ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "FailedPayouts", script, true);
                }
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnpayout.InnerHtml = "An unexpected error occurred while processing payouts.";
            }
        }
      
        protected void BtnRejectSelected_Click(object sender, EventArgs e)
        {
            try
            {
                bool anySelected = false;
                List<string> failedMessages = new List<string>();

                foreach (GridViewRow row in gvResults.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                    if (chk != null && chk.Checked)
                    {
                        anySelected = true;

                        string refId = gvResults.DataKeys[row.RowIndex]["ReferenceID"].ToString();
                        string accountNo = gvResults.DataKeys[row.RowIndex]["BeneId"].ToString();
                        string amount = gvResults.DataKeys[row.RowIndex]["TransferAmt"].ToString();
                        string ifsc = gvResults.DataKeys[row.RowIndex]["IFSCCode"].ToString();
                        string accHolder = gvResults.DataKeys[row.RowIndex]["AccountHolder"].ToString();
                        string registrationID = gvResults.DataKeys[row.RowIndex]["RegistrationId"].ToString();
                        string Emailid = gvResults.DataKeys[row.RowIndex]["EmailID"].ToString();
                        string mobileNo = gvResults.DataKeys[row.RowIndex]["MobileNo"].ToString();
                        string PayoutAmt= gvResults.DataKeys[row.RowIndex]["PayoutAmount"].ToString();
                        // Call Reject Payout and update into table
                        //Dev : Naim Khan
                        //Date : 04022026

                        //Rejected Entry Deposit
                        //Dev : Naim Khan
                        //Date : 12022026

                        //GenerateRefrenceId();
                        //// Set up the user details
                        //objTransfertoAgent.RegistrationId = registrationID;
                        //objTransfertoAgent.TransferFrom = "Admin";
                        //objTransfertoAgent.TransferTo = registrationID;
                        //objTransfertoAgent.Remark = "Deposit From Payout Rejected Amount";
                        //objTransfertoAgent.TransferAmt = Convert.ToDecimal(PayoutAmt);
                        //objTransfertoAgent.TransferFromMsg = $"Reject Payout amount of {PayoutAmt:N2} transferred from Retailer (ID: {registrationID}) to Wallet against Transaction ID: {RefrenceId}.";
                        //objTransfertoAgent.TransferToMsg = $"Reject Payout amount of {PayoutAmt:N2} transferred to Admin account against Transaction ID: {RefrenceId}.";
                        //objTransfertoAgent.Amount_Type = "Deposit";
                        //objTransfertoAgent.ActualCommissionAmount = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.GSTAMT = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.TdsAmt = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.commissionWithoutGST = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.TransIpAddress = AppSessions.SessionLocalHostIP;
                        //objTransfertoAgent.Services_charge_Amt = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.Services_charge_GSTAmt = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.Services_charge_Without_GST = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.customerCommission = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.customerCommissionGST = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.customerCommissionWithoutGST = Convert.ToDecimal("0.00");
                        //objTransfertoAgent.Amt_Transfer_TransID = RefrenceId;
                        //objTransfertoAgent.Holdingamt = "0.00";
                        //objTransfertoAgent.Actual_Transaction_Amount = Convert.ToDecimal(PayoutAmt);
                        //objTransfertoAgent.TransertoAgent();
                        //objTransfertoAgent.RefrenceId = RefrenceId;
                        //objTransfertoAgent.ApprovedStatus = "Approved";
                        //objTransfertoAgent.DepositUpdate();

                        ObjpendingPayout.RefrenceId = refId;
                        ObjpendingPayout.ApprovedBy = AppSessions.SessionLoginId;
                        ObjpendingPayout.ApprovedStatus = "Rejected";
                        ObjpendingPayout.ApporveRemakrs = txtApprovalRemarks.Text.Trim();
                        ObjpendingPayout.PayoutApprovedRequest();

                        txtApprovalRemarks.Text = string.Empty;
                    }
                }
                if (!anySelected)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "toastr.warning('Please select at least one payout.');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    "toastr.success('Selected payout Rejected successfully.');", true);

                BindPendingApprovalGridPayout();
                txtApprovalRemarks.Text = string.Empty;
                // Show modal if there are failed payouts
                if (failedMessages.Count > 0)
                {
                    string failedList = string.Join("", failedMessages.Select(m => $"<li>{m}</li>"));
                    string script = $@"
        var modalEl = document.getElementById('failedPayoutsModal');
        var modal = new bootstrap.Modal(modalEl);
        document.getElementById('failedPayoutsList').innerHTML = `{failedList}`;
        modal.show();
    ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "FailedPayouts", script, true);
                }
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnpayout.InnerHtml = "An unexpected error occurred while processing payouts.";
            }
        }
        private string PayoutApprovedSync(string Emailid, string MobileNo,string accountNo, string amount, string ifsc, string accHolder,
            string registrationID, string refId,string paymentMode)
        {
            try
            {
                PayoutRequestModelv3 queryRequest = new PayoutRequestModelv3
                {
                    accountNumber = accountNo.Trim(),
                    amount = amount.Trim(),
                    transactionType = paymentMode,
                    beneficiaryIFSC = ifsc.Trim().ToUpper(),
                    beneficiaryName = accHolder.Trim(),
                    emailID = Emailid,
                    mobileNo = MobileNo,
                    registrationID = OQMerchnatCode,
                    tranrefID = refId
                };

                string output = JsonConvert.SerializeObject(queryRequest);

                var client = new RestClient(BOSpayBaseUrl + "api/V6/Payout/AOPPayout");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", output, ParameterType.RequestBody);

                // Execute synchronously
                IRestResponse response = client.Execute(request);

                // Save logs
                System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/WebPayoutRequestLog.txt"), output + Environment.NewLine);
                System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/WebPayoutResponseLog.txt"), response.Content + Environment.NewLine);

                PayoutResponsev3 payout = JsonConvert.DeserializeObject<PayoutResponsev3>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK &&
                    payout?.initiateAuthGenericFundTransferAPIResp?.metaData?.status == "SUCCESS")
                {
                    ObjpendingPayout.RefrenceId = refId;
                    ObjpendingPayout.ApprovedBy = AppSessions.SessionLoginId;
                    ObjpendingPayout.ApprovedStatus = "Approved";
                    ObjpendingPayout.ApporveRemakrs = txtApprovalRemarks.Text.Trim();
                    ObjpendingPayout.PayoutApprovedRequest();

                    return null; // no error

             
                }
                else
                {
                    return $"RefID: {refId} - {payout?.message ?? "Unknown error"}";
                }
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                return $"RefID: {refId} - Unexpected error: {ex.Message}";
            }
        }

        //private void PayoutApproved()
        //{
        //    try
        //    {
        //        PayoutRequestModelv3 queryRequest = new PayoutRequestModelv3();
        //        PayoutResponsev3 payout = new PayoutResponsev3();

        //        queryRequest.accountNumber = spnAccountNo.InnerText.Trim();
        //        queryRequest.amount = spnAmount.InnerText.Trim();
        //        queryRequest.transactionType = "NEFT";
        //        queryRequest.beneficiaryIFSC = spnIFSC.InnerText.Trim().ToUpper();
        //        queryRequest.beneficiaryName = spnAccHolder.InnerText.Trim();
        //        queryRequest.emailID = "Test@gmail.com";
        //        queryRequest.mobileNo="8888888888";
        //        queryRequest.registrationID = OQMerchnatCode;
        //        queryRequest.tranrefID = spnRefId.InnerText.Trim();

        //        // Serialize request to JSON
        //        string output = JsonConvert.SerializeObject(queryRequest);
        //        var client = new RestClient(BOSpayBaseUrl + "api/V6/Payout/AOPPayout");
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("accept", "application/json");
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddParameter("application/json", output, ParameterType.RequestBody);

        //        // Execute API request
        //        IRestResponse response = client.Execute(request);

        //        // Save request and response logs
        //        string root = HttpContext.Current.Server.MapPath("~/WebPayoutRequestLog.txt");
        //        System.IO.File.WriteAllText(root, output);
        //        root = HttpContext.Current.Server.MapPath("~/WebPayoutResponseLog.txt");
        //        System.IO.File.WriteAllText(root, response.Content);

        //        // Deserialize response
        //        payout = JsonConvert.DeserializeObject<PayoutResponsev3>(response.Content);

        //        if (response.StatusCode == HttpStatusCode.OK &&
        //            payout?.initiateAuthGenericFundTransferAPIResp?.metaData?.status == "SUCCESS")
        //        {
        //           //approvedbyAdmin();
        //        }
        //        else
        //        {
        //            spnpayout.InnerHtml = payout?.message ?? "Transaction failed. Please try again.";

        //            // Attempt to parse API error response
        //            try
        //            {
        //                ApiErrorResponse apiError = JsonConvert.DeserializeObject<ApiErrorResponse>(response.Content);
        //                if (apiError != null)
        //                {
        //                    spnpayout.InnerHtml = $"Error Code: {apiError.Code}<br>Reason: {apiError.Reason}<br>Service: {apiError.Service}<br>Details: {apiError.Details}";
        //                }
        //            }
        //            catch
        //            {
        //                spnpayout.InnerHtml += "<br>Could not parse error details.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception for debugging
        //        Common.WriteExceptionLog(ex);
        //        spnpayout.InnerHtml = "An unexpected error occurred. Please try again later.";
        //    }
        //}
        public class ApiErrorResponse
        {
            public string Code { get; set; }
            public string Reason { get; set; }
            public string Service { get; set; }
            public string Details { get; set; }
        }
        //private void ApprovalbyAdmin()
        //{
        //    try
        //    {
        //        ObjpendingPayout.RefrenceId = spnRefId.InnerText.Trim();
        //        ObjpendingPayout.ApprovedBy = AppSessions.SessionLoginId;
        //        ObjpendingPayout.ApprovedStatus = ddlApprovalStatus.Text.Trim();
        //        ObjpendingPayout.ApporveRemakrs = txtApprovalRemarks.Text.Trim();

        //        short retVal = ObjpendingPayout.PayoutApprovedRequest();

        //        string message = string.Empty;
        //        if (retVal == 1)
        //        {
        //            if (ddlApprovalStatus.SelectedValue == "Approved")
        //            {
        //                message = "Your Payout Request is Approved";

        //                // ✅ Call function only when approved
        //                PayoutApproved();
        //            }
        //            else if (ddlApprovalStatus.SelectedValue == "Rejected")
        //            {
        //                message = "Your Payout Request is Rejected";
        //            }

        //            // Show the respective popup message
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);

        //            // Refresh grid to reflect changes
        //            BindPendingApprovalGridPayout();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Update failed. Please try again.');", true);
        //            BindPendingApprovalGridPayout();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteExceptionLog(ex);
        //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        private void ApprovalholdamtbyAdmin()
        {
            try
            {
                ObjpendingPayout.RefrenceId = spnHoldTxnId.InnerText.Trim();
                ObjpendingPayout.ApprovedBy = AppSessions.SessionLoginId;
                ObjpendingPayout.ApprovedStatus = ddlHoldApprovalStatus.Text.Trim();
                ObjpendingPayout.ApporveRemakrs = txtHoldApprovalRemarks.Text.Trim();

                short retVal = ObjpendingPayout.HoldAmtApprovedRequest();

                string message = string.Empty;
                if (retVal == 1)
                {
                    if (ddlHoldApprovalStatus.SelectedValue == "Approved")
                    {
                        message = "Your Holding Amt. Request is Approved";

                        // ✅ Call function only when approved
                        ExecuteHoldAmountTransfer();
                    }
                    else if (ddlHoldApprovalStatus.SelectedValue == "Rejected")
                    {
                        message = "Your Holding Amt. Request is Rejected";
                    }

                    // Show the respective popup message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);

                    // Refresh grid to reflect changes
                    BindPendingApprovalGridHoldAmt();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Update failed. Please try again.');", true);
                    BindPendingApprovalGridPayout();
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnSubmitApproval_Click(object sender, EventArgs e)
        {
           //ApprovalbyAdmin();
        }

        protected void btnSubmitHoldApproval_Click(object sender, EventArgs e)
        {
            ApprovalholdamtbyAdmin();
        }

        protected void btnRejectSelected_Click(object sender, EventArgs e)
        {

        }
    }
}