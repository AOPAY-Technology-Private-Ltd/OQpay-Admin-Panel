using AjaxControlToolkit;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor.Rtf;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class MakePaymentApproval : System.Web.UI.Page
    {
        BLMakePayment objMakepayment = new BLMakePayment();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();

        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSessions.SessionLoginId != null)
                {
                    ddlRecordStatus.SelectedValue = "Pending";
                    CustomerTypeSearchDropdown();
                    BindGridView();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }

        private void CustomerTypeSearchDropdown()
        {
            if (ddlCutomerType.SelectedValue == "Customer")
            {
                ddlSearchCriteria.Items.Clear();
                //ddlCutomerType.Items.Add(new ListItem("-- Select Type --", ""));
                ddlSearchCriteria.Items.Add(new ListItem("CustomerCode", "CustomerCode"));
                ddlSearchCriteria.Items.Add(new ListItem("LoanCode", "LoanCode"));
               
            }
            else
            {
                ddlSearchCriteria.Items.Clear();
                ddlSearchCriteria.Items.Add(new ListItem("RetailerCode", "RetailerCode"));
                ddlSearchCriteria.Items.Add(new ListItem("LoanCode", "LoanCode"));
            }
        }

        private void TransfertoAgentDeposit()
        {
            try
            {
                GenerateRefrenceId();
                // Set up the user details
                objTransfertoAgent.RegistrationId = hfdretailercode.Value;
                objTransfertoAgent.TransferFrom = "Admin";
                objTransfertoAgent.TransferTo = txtCustomerCode.Text;
                objTransfertoAgent.Remark = "Payin Deposit";
                objTransfertoAgent.TransferAmt =Convert.ToDecimal(txtEMIAmount.Text.Trim());
                objTransfertoAgent.TransferFromMsg = "Deposit of " + objTransfertoAgent.TransferAmt.ToString("N2") + " has been successfully processed to Customer Code: " + objTransfertoAgent.TransferTo + ".";
                objTransfertoAgent.TransferToMsg = "A deposit of " + objTransfertoAgent.TransferAmt.ToString("N2") + " has been credited to your account. Please check your balance for confirmation.";
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
                objTransfertoAgent.Amt_Transfer_TransID =RefrenceId;       
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
        private void BindGridView()
        {
            try
            {
                objMakepayment.Mode = "GET";

                // 🔹 Default filters
                objMakepayment.Cutomertype = ddlCutomerType.SelectedValue;
                objMakepayment.Customercode = string.Empty;
                objMakepayment.Loancode = string.Empty;
                objMakepayment.Recordstatus = "Pending";   // default

                // 🔹 Apply search criteria if selected
                string criteria = ddlSearchCriteria.SelectedItem != null
                                    ? ddlSearchCriteria.SelectedItem.Text.Trim().ToUpper()
                                    : string.Empty;
                string value = txtSearchValue.Text.Trim();

                if (criteria == "CUSTOMERCODE" && !string.IsNullOrEmpty(value) || criteria == "RETAILERCODE" && !string.IsNullOrEmpty(value))
                {
                    objMakepayment.Customercode = value;
                    objMakepayment.Recordstatus = string.Empty;
                }

                else if (criteria == "LOANCODE" && !string.IsNullOrEmpty(value))
                {
                    objMakepayment.Loancode = value;
                    objMakepayment.Recordstatus = string.Empty;
                }
                else if (criteria == "PENDING")
                {
                    objMakepayment.Recordstatus = "Pending";
                }
                else
                {
                    // fallback to ddlRecordStatus if no search selected
                    string selectedStatus = ddlRecordStatus.SelectedValue;
                    if (!string.IsNullOrEmpty(selectedStatus))
                        objMakepayment.Recordstatus = selectedStatus;
                }

                // 🔹 Bind data
                DataTable dt = objMakepayment.ApproveMakePayment();

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvMakePayment.DataSource = dt;
                    gvMakePayment.DataBind();
                    string SucessMessage = "Records Retrieve Successfully";

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
                  
                    gvMakePayment.DataSource = null;
                    gvMakePayment.DataBind();
                    

                    string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");


                    string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript( this, this.GetType(),"showToastrError",script,true);
                }

                //// 🔹 Record count display
                //int totalRecords = dt.Rows.Count;
                //int startRecord = (gvMakePayment.PageIndex * gvMakePayment.PageSize) + 1;
                //int endRecord = Math.Min(startRecord + gvMakePayment.PageSize - 1, totalRecords);

                //lblMakePaymentRecordCount.Text = totalRecords > 0
                //    ? $"Showing {startRecord}–{endRecord} of {totalRecords} records"
                //    : "No records found";
            }
            catch (Exception ex)
            {
              Common.WriteExceptionLog(ex);
              spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnCode;
            string returnMsg;

            try
            {
                
               if (txtCustomerCode.Text == string.Empty)
                {
                    txtCustomerCode.Focus();
                    txtLoanCode.Focus();
                    txtPaidAmount.Focus();
                    lblErrormsg.InnerText = "click gridview edit button";
                    lblErrormsg.Attributes["class"] = "text-danger";
                }
                else
                {
                    objMakepayment.Mode = "Approved";
                    objMakepayment.RID = Convert.ToInt32(lblRID.Text);
                    objMakepayment.Customercode = txtCustomerCode.Text;
                    objMakepayment.Loancode = txtLoanCode.Text.Trim();
                    objMakepayment.NoofPaidEMI=txtNoofpaidEMI.Text.Trim();
                    objMakepayment.Recordstatus = ddlRecordStatus.SelectedItem.Text.Trim();
                    objMakepayment.remarks = txtremarks.Text.Trim();

                   var result = objMakepayment.ApproveMakePayment(out returnCode, out returnMsg);
                    //BindGridView();
                    if (returnCode == 0) 
                    {

                       // TransfertoAgentDeposit();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{returnMsg}');", true);
                        DivMakePayment.Style["display"] = "none";
                        DivSearchMakePayment.Style["display"] = "block";
                    }
                    //else if (returnCode == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{returnMsg}');", true);
                    //}
                    //else if (returnCode == 2)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{returnMsg}');", true);
                    //}
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{returnMsg}');", true);
                    }
                    Clear();
                    BindGridView();
                }
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            DivMakePayment.Style["display"] = "none";
            DivSearchMakePayment.Style["display"] = "block";
        }

        private void Clear()
        {
            txtCustomerCode.Text = "";
            txtEMIAmount.Text = "";
            txtLoanCode.Text = "";
            txtPaidAmount.Text = "";
            txtNoofpaidEMI.Text = "";
            txtTxnNo.Text = "";
            ddlRecordStatus.SelectedItem.Text = "Pending";
        }

        protected void gvMakePayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                //LinkButton lnk = (LinkButton)e.CommandSource;
                //GridViewRow grdrow = (GridViewRow)lnk.NamingContainer;

                //if (grdrow.RowIndex >= 0)
                //{
                //    //int rid = Convert.ToInt32(e.CommandArgument.ToString());
                //    int rid = Convert.ToInt32(gvMakePayment.DataKeys[grdrow.RowIndex].Values["RID"]);
                //    string Customercode = Convert.ToString(gvMakePayment.DataKeys[grdrow.RowIndex].Values["CustomerCode"]);
                //    string Loancode = Convert.ToString(gvMakePayment.DataKeys[grdrow.RowIndex].Values["LoanCode"]);

                //    objMakepayment.Mode = "GET";
                //    objMakepayment.RID = rid;
                //    DataTable dt = objMakepayment.ApproveMakePayment();

                //    DataRow row = dt.Rows[0];


                //    //gvMakePayment.DataSource = dt;
                //    //gvMakePayment.DataBind();

                //    //string customercode= grdrow.Cells[0].Text;
                //    //string loancode = grdrow.Cells[1].Text;
                //    //string txnNo = grdrow.Cells[2].Text;
                //    //string noofemi = grdrow.Cells[3].Text;
                //    //string paidamount = grdrow.Cells[4].Text;
                //    //string emiamt = grdrow.Cells[5].Text;
                //    //string imagepath = ImageUrl + grdrow.Cells[6].Text;
                //    //string activestatus = grdrow.Cells[7].Text;
                //    //string recordstatus = grdrow.Cells[8].Text;

                //    // if (dt.Rows.Count>0)
                //    //{

                //    //}
                //    //else
                //    //{

                //    //}

                //    string customercode = row["CustomerCode"].ToString();
                //    string loancode = row["LoanCode"].ToString();
                //    string txnNo = row["txnNumber"].ToString();
                //    string noofemi = row["DivideNo"].ToString();
                //    string paidamount = row["PaidAmount"].ToString();
                //    string emiamt = row["EMIAmt"].ToString();
                //    //string imagepath = ImageUrl + row["ReceiptImage_Path"].ToString();
                //    string imagepath = "http://192.168.1.56:89" + row["ReceiptImage_Path"].ToString();
                //    string activestatus = row["ActiveStatus"].ToString();
                //    string recordstatus = row["RecordStatus"].ToString();

                //    if (e.CommandName == "EditRow")
                //    {
                //        lblRID.Text = rid.ToString();
                //        txtCustomerCode.Text = customercode;
                //        txtLoanCode.Text = loancode;
                //        txtEMIAmount.Text = emiamt;
                //        txtPaidAmount.Text = paidamount;
                //        if (txnNo == "")
                //        {
                //            txtTxnNo.Text = "0";
                //        }
                //        else
                //        {
                //            txtTxnNo.Text = txnNo;
                //        }
                //        if (noofemi == "")
                //        {
                //            txtNoofpaidEMI.Text = "0";
                //        }
                //        else
                //        {
                //            txtNoofpaidEMI.Text = noofemi;
                //        }

                //        if (string.IsNullOrEmpty(imagepath) && string.IsNullOrWhiteSpace(imagepath))
                //        {
                //            imgReceiptPreview.ImageUrl = "../Images/image%20icon.png";
                //        }
                //        //else if (imagepath = " &nbsp;")
                //        //{

                //        //}
                //        else
                //        {
                //            imgReceiptPreview.ImageUrl = imagepath;
                //            //imgReceiptPreview.ImageUrl = "http://192.168.1.56:89/ReceiptImage/638920880212122788_ReceiptPhoto1756471416473.jpg";
                //        }
                //        if (ddlActiveStatus.Items.FindByText(activestatus) != null)
                //        {
                //            ddlActiveStatus.SelectedItem.Text = activestatus;
                //        }


                //        if (ddlRecordStatus.Items.FindByText(recordstatus) != null)
                //        {
                //            ddlRecordStatus.SelectedItem.Text = recordstatus;
                //        }

                //        DivMakePayment.Style["display"] = "block";
                //        DivSearchMakePayment.Style["display"] = "none";

                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{

                //}

                if (e.CommandName == "Page")
                    return;

                if (e.CommandName == "EditRow")
                {
                    if (e.CommandSource is LinkButton lnk)
                    {
                        GridViewRow grdrow = (GridViewRow)lnk.NamingContainer;

                        if (grdrow.RowIndex >= 0)
                        {

                            int rid = Convert.ToInt32(gvMakePayment.DataKeys[grdrow.RowIndex].Values["RID"]);
                            string Customercode = Convert.ToString(gvMakePayment.DataKeys[grdrow.RowIndex].Values["CustomerCode"]);
                            string Loancode = Convert.ToString(gvMakePayment.DataKeys[grdrow.RowIndex].Values["LoanCode"]);

                            objMakepayment.Mode = "GET";
                            objMakepayment.RID = rid;
                            objMakepayment.Cutomertype = ddlCutomerType.SelectedValue;
                            DataTable dt = objMakepayment.ApproveMakePayment();

                            DataRow row = dt.Rows[0];



                            string customercode = row["CustomerCode"].ToString();
                            string loancode = row["LoanCode"].ToString();
                            string txnNo = row["UTRNumber"].ToString();
                            string noofemi = row["DivideNo"].ToString();
                            string paidamount = row["Monthly EMI"].ToString();
                            string emiamt = row["EMIAmount"].ToString();
                            string ImageUrl = "http://api.oqpay.in";                       
                            //if (ddlCutomerType.SelectedValue == "Retailer")
                            //{
                            //     emiamt = row["EMIAmount"].ToString();
                            //     imagepath = ImageUrl + row["ReceiptImagePath"].ToString();
                            //}
                            //else
                            //{
                            //     emiamt = row["EMIAmount"].ToString();
                            //    imagepath = ImageUrl + row["ReceiptImagePath"].ToString();

                            //}
                             string InterestAmt = row["InterestAmt"].ToString();
                            string Fine = row["Fine"].ToString();
                            string TotalEMIAmt = row["Total EMIAmt"].ToString();
                            //imagepath = ImageUrl + row["ReceiptImagePath"].ToString();
                            string imagepath = row["ReceiptImagePath"].ToString();
                            string activestatus = row["ActiveStatus"].ToString();
                            string recordstatus = row["RecordStatus"].ToString();
                            string RetailerCode = row["RetailerCode"].ToString();

                            if (e.CommandName == "EditRow")
                            {
                                lblRID.Text = rid.ToString();
                                txtCustomerCode.Text = customercode;
                                txtLoanCode.Text = loancode;
                                txtEMIAmount.Text = emiamt;
                                txtMonthlyEMIAmount.Text = paidamount;
                                txtPaidAmount.Text = TotalEMIAmt;
                                txtInterestAmt.Text = InterestAmt;
                                txtFine.Text = Fine;
                                txtTotalEMIAmt.Text = TotalEMIAmt;
                                hfdretailercode.Value = RetailerCode;

                                if (txnNo == "")
                                {
                                    txtTxnNo.Text = "0";
                                }
                                else
                                {
                                    txtTxnNo.Text = txnNo;
                                }
                                if (noofemi == "")
                                {
                                    txtNoofpaidEMI.Text = "0";
                                }
                                else
                                {
                                    txtNoofpaidEMI.Text = noofemi;
                                }

                                if (string.IsNullOrEmpty(imagepath) && string.IsNullOrWhiteSpace(imagepath))
                                {
                                    imgReceiptPreview.ImageUrl = "../Images/image%20icon.png";
                                }

                                else
                                {
                                    imgReceiptPreview.ImageUrl = ImageUrl + imagepath;
                                }
                                if (ddlActiveStatus.Items.FindByText(activestatus) != null)
                                {
                                    ddlActiveStatus.SelectedItem.Text = activestatus;
                                }


                                if (ddlRecordStatus.Items.FindByText(recordstatus) != null)
                                {
                                    ddlRecordStatus.SelectedItem.Text = recordstatus;
                                }

                                DivMakePayment.Style["display"] = "block";
                                DivSearchMakePayment.Style["display"] = "none";

                            }
                        }
                        else
                        {
                            spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, $"Unhandled Gridview RowIndex: {grdrow.RowIndex}");
                        }

                    }
                    else
                    {
                        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, $"Unhandled CommandSource: {e.CommandSource}");
                    }
                }
                else
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, $"Unhandled Command: {e.CommandName}");
                }

            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }

        }

        protected void gvMakePayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           // gvMakePayment.PageIndex = e.NewPageIndex;
            BindGridView();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvMakePayment.PageCount)
            {
                gvMakePayment.PageIndex = e.NewPageIndex;
                BindGridView();
            }
        }

        protected void btnDownloadReceipt_Click(object sender, EventArgs e)
        {
            string imageUrl = imgReceiptPreview.ImageUrl;
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }

        protected void gvMakePayment_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gvMakePayment.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvMakePayment.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvMakePayment.PageIndex < gvMakePayment.PageCount - 1;
            }
            else
            {
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string criteria = ddlSearchCriteria.SelectedItem.Text.Trim().ToUpper();
            string value = txtSearchValue.Text.Trim().ToUpper();

            if (criteria == "PENDING")
            {
                objMakepayment.Recordstatus = "Pending";  // ✅ set status filter
                objMakepayment.Customercode = string.Empty;
                objMakepayment.Loancode = string.Empty;
            }
            else if (criteria == "CUSTOMERCODE")
            {
                objMakepayment.Recordstatus = string.Empty;
                objMakepayment.Customercode = value;      // ✅ set customer code
                objMakepayment.Loancode = string.Empty;
            }
            else if (criteria == "LOANCODE")
            {
                objMakepayment.Recordstatus = string.Empty;
                objMakepayment.Customercode = string.Empty;
                objMakepayment.Loancode = value;          // ✅ set loan code
            }

            BindGridView();
        }

        protected void ddlCutomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerTypeSearchDropdown();
            BindGridView();
        }
    }
}