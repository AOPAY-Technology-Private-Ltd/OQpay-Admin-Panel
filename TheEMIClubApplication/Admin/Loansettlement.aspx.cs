using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class Loansettlement : System.Web.UI.Page
    {
        string cs = ConfigurationManager.AppSettings["ConnectionString"];
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLLoanApproval objLoan = new BLLoanApproval();
        DataTable dt = new DataTable();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetHoldingAmt();
                GridBind();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string settlementMode = ddlLoanSettlement.SelectedValue.Trim().ToLower();
                // settlement | foreclosure

                objLoan.Mode = settlementMode;
                objLoan.Loanid = txtLoanCode.Text.Trim().ToUpper();

                objLoan.RecordStatus = ddlLoanSettlement.SelectedValue.Trim().ToUpper();
                objLoan.SettlementType = ddlLoanSettlement.SelectedValue.Trim().ToUpper();

                objLoan.Remarks = txtremarks.Text.Trim();

                objLoan.SettlementOrForeclosureCharg =
                    string.IsNullOrEmpty(txtTotalCharge.Text) ? 0 : Convert.ToDecimal(txtTotalCharge.Text);

                objLoan.FineAmount =
                    string.IsNullOrEmpty(txttotgrandefine.Text) ? 0 : Convert.ToDecimal(txttotgrandefine.Text);

                objLoan.FinalPaidamt =
                    string.IsNullOrEmpty(txtFinalPayableAmount.Text) ? 0 : Convert.ToDecimal(txtFinalPayableAmount.Text);

                objLoan.PaymentMode = ddlPaymentMode.SelectedValue.Trim();

                objLoan.TxnNo = null;
                objLoan.TxnDate = DateTime.MinValue;
                objLoan.BankName = null;

                if (objLoan.PaymentMode != "Cash")
                {
                    objLoan.TxnNo = txtTxnNo.Text.Trim();
                    objLoan.BankName = txtBankName.Text.Trim();

                    if (!string.IsNullOrEmpty(txtTxnDate.Text))
                        objLoan.TxnDate = Convert.ToDateTime(txtTxnDate.Text);
                }

                DataTable result = objLoan.GetLoanSettlementDetails();

                if (result != null && result.Rows.Count > 0)
                {
                    string status = result.Rows[0]["Success"].ToString();
                    string statusMsg = result.Rows[0]["LoanMsg"].ToString();

                    if (status == "200")
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "SuccessAlert",
                            $@"setTimeout(function(){{
                Swal.fire({{
                    icon: 'success',
                    title: 'Success',
                    text: '{statusMsg}',
                    confirmButtonText: 'OK'
                }});
            }}, 300);",
                            true
                        );

                        Div_LoanSettlementSearch.Visible = true;
                        chkApplyForeclosureCharge.Visible = false;
                        chkApplyLateFine.Visible = false;
                        chkApplySettlementCharge.Visible = false;
                        Div_Loansettelmentform.Visible = false;

                        GridBind();
                    }

                    else if (status != "200")
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "WarningAlert",
                            $"showAlert('warning','Warning','{statusMsg}','');",
                            true
                        );
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "ErrorAlert",
                            $"showAlert('error','Failed','{statusMsg}','');",
                            true
                        );
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "NoResponse",
                        "showAlert('info','No Response','No response received from server. Please try again.','');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "ExceptionAlert",
                    $"showAlert('error','System Error','{ex.Message.Replace("'", "")}','');",
                    true
                );
            }
        }


        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // 🔹 Decide Mode based on dropdown
        //        string settlementMode = ddlLoanSettlement.SelectedValue.Trim().ToLower();
        //        // settlement | foreclosure

        //        objLoan.Mode = settlementMode;
        //        objLoan.Loanid = txtLoanCode.Text.Trim().ToUpper();

        //        // Status & Type
        //        objLoan.RecordStatus = ddlLoanSettlement.SelectedValue.Trim().ToUpper();
        //        objLoan.SettlementType = ddlLoanSettlement.SelectedValue.Trim().ToUpper();

        //        // Remarks
        //        objLoan.Remarks = txtremarks.Text.Trim();

        //        // EMI Details
        //      //  objLoan.DuesEMI = txtTotalDueEMIMonths.Text.Trim();
        //      //  objLoan.PaidEMI = txtTotalPaidEMIMonths.Text.Trim();

        //        // Charges (safe conversion)
        //        objLoan.SettlementOrForeclosureCharg =
        //            string.IsNullOrEmpty(txtTotalCharge.Text)
        //            ? 0
        //            : Convert.ToDecimal(txtTotalCharge.Text);

        //        objLoan.FineAmount =
        //            string.IsNullOrEmpty(txttotgrandefine.Text)
        //            ? 0
        //            : Convert.ToDecimal(txttotgrandefine.Text);

        //        objLoan.FinalPaidamt =
        //           string.IsNullOrEmpty(txtFinalPayableAmount.Text)
        //           ? 0
        //           : Convert.ToDecimal(txtFinalPayableAmount.Text);

        //        // Payment Mode
        //        objLoan.PaymentMode = ddlPaymentMode.SelectedValue.Trim();

        //        // Default values (for Cash)
        //        objLoan.TxnNo = null;
        //        objLoan.TxnDate = DateTime.MinValue;
        //        objLoan.BankName = null;

        //        // Conditional Txn Details
        //        if (objLoan.PaymentMode != "Cash")
        //        {
        //            objLoan.TxnNo = txtTxnNo.Text.Trim();
        //            objLoan.BankName = txtBankName.Text.Trim();

        //            if (!string.IsNullOrEmpty(txtTxnDate.Text))
        //            {
        //                objLoan.TxnDate = Convert.ToDateTime(txtTxnDate.Text);
        //            }
        //        }

        //        // 🔹 Execute Stored Procedure
        //        DataTable result = objLoan.GetLoanSettlementDetails();

        //        if (result != null && result.Rows.Count > 0)
        //        {
        //            string status = result.Rows[0]["Success"].ToString();
        //            string statusMsg = result.Rows[0]["LoanMsg"].ToString();

        //            ScriptManager.RegisterStartupScript(
        //                this,
        //                this.GetType(),
        //                "Popup",
        //                $"ShowPopup('{statusMsg}');",
        //                true
        //            );

        //            if (status == "200")
        //            {
        //                // Optional future calls
        //                // EMISettelment();
        //                // TransfertoAgentDeposit();
        //                //Claer();
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(
        //                this,
        //                this.GetType(),
        //                "Popup",
        //                "ShowPopup('No response received from server.');",
        //                true
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(
        //            this,
        //            this.GetType(),
        //            "Popup",
        //            $"ShowPopup('{ex.Message.Replace("'", "")}');",
        //            true
        //        );
        //    }
        //}


        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string TotalDueEMIAmount = txtTotalDueEMIAmount.Text;
        //    string TotalDueEMIMonths = txtTotalDueEMIMonths.Text;
        //    string TotalPaidEMIMonths = txtTotalPaidEMIMonths.Text;

        //    objLoan.Mode = "settlement";
        //    objLoan.Loanid = txtLoanCode.Text.ToUpper().Trim();
        //    objLoan.RecordStatus = ddlLoanSettlement.SelectedValue.ToUpper().Trim();
        //    objLoan.SettlementType= ddlLoanSettlement.SelectedValue.ToUpper().Trim();
        //    objLoan.Remarks = txtremarks.Text.Trim();
        //    objLoan.PaymentMode = ddlPaymentMode.Text.Trim();
        //    objLoan.DuesEMI = txtTotalDueEMIMonths.Text;
        //    objLoan.PaidEMI = txtTotalPaidEMIMonths.Text;

        //    var result = objLoan.GetLoanSettlementDetails();
        //    string status = result.Rows[0]["Success"].ToString();
        //    string statusMsg = result.Rows[0]["LoanMsg"].ToString();
        //    if (result != null)
        //    {

        //        if (status == "200")
        //        {
        //          //  EMISettelment();
        //           // TransfertoAgentDeposit();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{statusMsg}');", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{statusMsg}');", true);
        //        }

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{statusMsg}');", true);
        //    }
        //    Claer();
        //}



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Div_LoanSettlementSearch.Visible = true;
            chkApplyForeclosureCharge.Visible = false;
            chkApplyLateFine.Visible = false;
            chkApplySettlementCharge.Visible = false;
            Div_Loansettelmentform.Visible = false;
        }

        private void Claer()
        {
            txtCustomerCode.Text = "";
            txtCustomerFullName.Text = "";
            txtCustomerMobileNo.Text = "";
            txtCustomerEmailid.Text = "";
            txtDealerid.Text = "";
            txtDealerFullName.Text = "";
            txtDealerMobileNo.Text = "";
            txtDealerEmailid.Text = "";
            txtLoanCode.Text = "";
            txtLoanAmount.Text = "";
            txtDownPayment.Text = "";
            txtLoanEMIAmount.Text = "";
            txtTenure.Text = "";
            txtInterestRate.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtCreditScore.Text = "";
            txtremarks.Text = "";
            //txtProcessingfees.Text ="";
            txtLoanStatus.Text = "";
            //txtLoanCreatedBy.Text = "";
            txtBrandName.Text = "";
            txtModelName.Text = "";
            txtModelVariant.Text = "";
            txtIMEI1Number.Text = "";


        }

        private void GridBind()
        {
            try
            {
                // ================= BASIC PARAMS =================
                objLoan.Mode = "GET";
                objLoan.RecordStatus = "Disbursed";   // default
                objLoan.SettlementType =
                    ddlaction.SelectedValue == "ALL" ? null : ddlaction.SelectedValue;

                // Reset optional filters (VERY IMPORTANT)
                objLoan.Loanid = null;
                objLoan.Customercode = null;
                objLoan.Dealrercode = null;

                string criteria = ddlLoanCriteria.SelectedValue.Trim().ToUpper();
                string value = txtLoanSettlementvalue.Text.Trim();

                // ================= APPLY FILTERS =================
                if (criteria != "ALL" && !string.IsNullOrEmpty(value))
                {
                    switch (criteria)
                    {
                        case "LOANCODE":
                            objLoan.Loanid = value;
                            break;

                        case "CUSTOMERCODE":
                            objLoan.Customercode = value;
                            break;

                        case "DEALERID":
                            objLoan.Dealrercode = value;
                            break;

                        case "LOANSTATUS":   // fixed typo
                            objLoan.RecordStatus = value;
                            break;
                    }
                }

                // ================= GET DATA =================
                dt = objLoan.GetLoanSettlementDetails();

                grdLoanSettlementDetails.DataSource = dt;
                grdLoanSettlementDetails.DataBind();

                // ================= UI FEEDBACK =================
                if (dt != null && dt.Rows.Count > 0)
                {
                   // Div_LoanSettlementSearch.Visible = true;

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "success",
                        "toastr.success('Records Retrieved Successfully', 'Success');",
                        true
                    );
                }
                else
                {
                   // Div_LoanSettlementSearch.Visible = false;

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "nodata",
                        "toastr.warning('No Data Found', 'Info');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "error",
                    $"toastr.error('{ex.Message.Replace("'", "")}', 'Error');",
                    true
                );
            }
        }


        protected void btnLoansettlementSearch_Click(object sender, EventArgs e)
        {
            GridBind();
        }

  

        //protected void grdLoanSettlementDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridBind();

        //    if (e.NewPageIndex >= 0 && e.NewPageIndex < grdLoanSettlementDetails.PageCount)
        //    {
        //        grdLoanSettlementDetails.PageIndex = e.NewPageIndex;
        //        GridBind();
        //    }
        //}




        //private void GetHoldingAmt()
        //{
        //    try
        //    {
        //        DataTable dt = objTransfertoAgent.getCompanyholdamount();

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            // Assuming you're interested in the first row's ReserveValue
        //            string reserveValue = Convert.ToString(dt.Rows[0]["ReserveValue"]);

        //            // Assign it to the hidden field
        //            hdnReserveValue.Value = reserveValue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteExceptionLog(ex);
        //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        private void EMISettelment()
        {
            try
            {
                objLoan.Customercode= txtCustomerCode.Text;
                objLoan.RetailerCode= txtDealerid.Text;
                objLoan.Loancode= txtLoanCode.Text;
                objLoan.EMIAmount= Convert.ToDecimal(txtLoanEMIAmount.Text);
                objLoan.Fine= Convert.ToDecimal(txtTotalLateFine.Text);
              //  objLoan.InterestAmount= Convert.ToDecimal(txtInterestAmount.Text);
                objLoan.PaymentMode=ddlPaymentMode.SelectedItem.Text;
                objLoan.PaidAmount = Convert.ToDecimal(txtTotalDueEMIAmount.Text);

                var result = objLoan.LoanSettlementEMI();
            }
              catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
            }
        }
        private void TransfertoAgentDeposit()
        {
            try
            {
                GenerateRefrenceId();
                // Set up the user details
                objTransfertoAgent.RegistrationId = txtDealerid.Text;
                objTransfertoAgent.TransferFrom = txtDealerid.Text;
                objTransfertoAgent.TransferTo ="Admin";
                objTransfertoAgent.Remark = "Payin Deposit";
                objTransfertoAgent.TransferAmt = Convert.ToDecimal(txtTotalDueEMIAmount.Text.Trim());
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
                objTransfertoAgent.Amt_Transfer_TransID = RefrenceId;
                //objTransfertoAgent.Holdingamt = hdnReserveValue.Value;
                objTransfertoAgent.ApprovedStatus = "Approved";
                objTransfertoAgent.TransertoAgent();



                //objTransfertoAgent.RefrenceId = RefrenceId;
                //objTransfertoAgent.ApprovedStatus = "Approved";
                //objTransfertoAgent.DepositUpdate();


            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
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
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex); // Log general exception
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
        }

        protected void grdLoanSettlementDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                string loanCode = e.CommandArgument.ToString();
                string actionType = ddlaction.SelectedValue; // settlement / foreclosure

                Div_Loansettelmentform.Visible = true;
                Div_LoanSettlementSearch.Visible = false;

                LoadLoanForEdit(loanCode, actionType);
            }
        }

        private void LoadLoanForEdit(string loanCode, string actionType)
        {
            string mode = actionType == "settlement"
                            ? "editsettlement"
                            : "editforeclosure";

            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("usp_GetSettlementLoan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", mode);
                cmd.Parameters.AddWithValue("@LoanCode", loanCode);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return;

                    /* ================= CUSTOMER ================= */
                    txtCustomerCode.Text = dr["CustomerCode"].ToString();
                    txtCustomerFullName.Text = dr["CustomerFullName"].ToString();
                    txtCustomerMobileNo.Text = dr["CustomerMobileNo"].ToString();
                    txtCustomerEmailid.Text = dr["CustomerEmailID"].ToString();

                    /* ================= DEALER ================= */
                    txtDealerid.Text = dr["DealerCode"].ToString();
                    txtDealerFullName.Text = dr["DealerFullName"].ToString();
                    txtDealerMobileNo.Text = dr["DealerMobileNo"].ToString();
                    txtDealerEmailid.Text = dr["DealerEmailID"].ToString();

                    /* ================= LOAN ================= */
                    decimal loanAmount = Convert.ToDecimal(dr["LoanAmount"]);
                    decimal emiAmount = Convert.ToDecimal(dr["EMIAmount"]);
                    int tenure = Convert.ToInt32(dr["Tenure"]);
                    int paidEMI = Convert.ToInt32(dr["PaidEMI"]);
                    int dueEMI = Convert.ToInt32(dr["DuesEMI"]);

                    txtLoanCode.Text = dr["LoanCode"].ToString();
                    txtLoanAmount.Text = loanAmount.ToString("0.00");
                     txtDownPayment.Text = dr["DownPayment"].ToString();
                    txtLoanEMIAmount.Text = emiAmount.ToString("0.00");
                    txtTenure.Text = tenure.ToString();
                    txtInterestRate.Text = dr["InterestRate"].ToString();
                    txtStartDate.Text = Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd");
                    txtEndDate.Text = Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd");
                    txtTotalPaidEMIMonths.Text = paidEMI.ToString();
                    txtTotalDueEMIMonths.Text = dueEMI.ToString();
                    txtLoanStatus.Text = dr["Status"].ToString();
                    txtCreditScore.Text = dr["creditScore"].ToString();
                    txtProcessingFees.Text = dr["ProcessingFees"].ToString();

                    /* ================= PRODUCT ================= */
                    txtBrandName.Text = dr["BrandName"].ToString();
                    txtModelName.Text = dr["ModelName"].ToString();
                    txtModelVariant.Text = dr["VariantName"].ToString();
                    txtIMEI1Number.Text = dr["IMEINumber"].ToString();

                    /* ================= CHARGE ================= */
                    string chargeType = dr["ChargeType"].ToString();
                    decimal chargeValue = Convert.ToDecimal(dr["ChargeValue"]);

                    txtChargeType.Text = chargeType;
                    txtChargeValue.Text = chargeValue.ToString("0.00");

                    /* ================= SUMMARY COMMON ================= */
                    decimal totalPaidAmount = dr["PaidAmount"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["PaidAmount"]);

                    txtTotalPaidAmount.Text = totalPaidAmount.ToString("0.00");

                    /* ================= FORECLOSURE ================= */
                    if (actionType == "foreclosure")
                    {
                        chkApplyForeclosureCharge.Checked = true;
                        chkApplyForeclosureCharge.Visible = true;
                        chkApplySettlementCharge.Visible = false;
                        chkApplyLateFine.Visible = false;
                   
                        // Hidden fields for JS (realtime UX)
                        hfLoanAmount.Value = loanAmount.ToString();
                        hfTenure.Value = tenure.ToString();
                        hfDueEMI.Value = dueEMI.ToString();
                        hfChargeType.Value = chargeType;
                        hfChargeValue.Value = chargeValue.ToString();
                        hfActionType.Value = actionType;

                        // No late fine
                        txtLateFinePerMonth.Text = "0.00";
                        txtTotalLateFine.Text = "0.00";
                        txttotgrandefine.Text = "0.00";

                        // Principal-based foreclosure
                        decimal principalPerEmi = loanAmount / tenure;
                        decimal remainingPrincipal = principalPerEmi * dueEMI;

                        decimal charge = 0;
                        if (chkApplyForeclosureCharge.Checked)
                        {
                            if (chargeType == "Percentage")
                                charge = remainingPrincipal * (chargeValue / 100);
                            else if (chargeType == "Flat")
                                charge = chargeValue;
                        }

                        decimal finalAmount = remainingPrincipal + charge;

                        decimal totalduesamt = dueEMI* emiAmount;
                        chkApplyLateFine.Visible = false;
                      
                        // Bind summary
                        txtRemainingPrincipal.Text = remainingPrincipal.ToString("0.00");
                        txtTotalCharge.Text = charge.ToString("0.00");
                        txtFinalPayableAmount.Text = finalAmount.ToString("0.00");
                        txtTotalDueEMIAmount.Text = totalduesamt.ToString("0.00");
                        ddlLoanSettlement.SelectedValue = "Foreclosure";
                        ddlLoanSettlement.Enabled = false;
                        lblTotalCharge.Text = "Total foreclosure  Charge";
                    }
                    /* ================= SETTLEMENT ================= */
                    else // SETTLEMENT
                    {

           
                        chkApplyForeclosureCharge.Visible = false;
                        chkApplySettlementCharge.Visible = true;
                        chkApplyLateFine.Visible = true;

                        chkApplySettlementCharge.Checked = true;
                        chkApplyLateFine.Checked = true;
                    

                        // Hidden fields for JS
                        hfDueEMI.Value = dueEMI.ToString();
                        hfChargeType.Value = chargeType;
                        hfChargeValue.Value = chargeValue.ToString();
                        hfActionType.Value = actionType;

                        decimal lateFine = Convert.ToDecimal(dr["LateFine"]);
                        decimal baseDue = emiAmount * dueEMI;

                        decimal totalLateFine = chkApplyLateFine.Checked
                            ? lateFine * dueEMI
                            : 0;

                        decimal subTotal = baseDue + totalLateFine;

                        decimal charge = 0;
                        if (chkApplySettlementCharge.Checked)
                        {
                            if (chargeType == "Percentage")
                                charge = subTotal * (chargeValue / 100);
                            else if (chargeType == "Flat")
                                charge = chargeValue;
                        }

                        decimal finalAmount = subTotal + charge;
                        decimal totalduesamt = dueEMI * emiAmount;

                        // Bind
                        lblRemainingPrincipal.Text = "Remaining Due Emi";
                        lblTotalCharge.Text = "Total Settlement  Charge";
                        txtLateFinePerMonth.Text = lateFine.ToString("0.00");
                        txtTotalLateFine.Text = totalLateFine.ToString("0.00");
                        txttotgrandefine.Text = totalLateFine.ToString("0.00");
                        txtRemainingPrincipal.Text = baseDue.ToString("0.00");
                        txtTotalCharge.Text = charge.ToString("0.00");
                        txtFinalPayableAmount.Text = finalAmount.ToString("0.00");
                        txtTotalDueEMIAmount.Text = totalduesamt.ToString("0.00");

                        ddlLoanSettlement.SelectedValue = "Settlement";
                        ddlLoanSettlement.Enabled = false;
                    }


                    txtLateFinePerMonth.ReadOnly = true;
                    txtTotalLateFine.ReadOnly = true;
                }
            }
        }





        //protected void grdLoanSettlementDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "EditRow")
        //    {
        //        LinkButton btn = (LinkButton)e.CommandSource;
        //        GridViewRow row = (GridViewRow)btn.NamingContainer;

        //        //string loanCode = e.CommandArgument.ToString();
        //        int rowIndex = row.RowIndex;

        //        Div_Loansettelmentform.Visible = true;
        //        Div_LoanSettlementSearch.Visible = false;

        //        string SrNO = row.Cells[1].Text;
        //        string CustomerCode = row.Cells[2].Text;
        //        string CustomerFullName = row.Cells[3].Text;
        //        string CustomerMobileNo = row.Cells[4].Text;
        //        string CustomerEmailID = row.Cells[5].Text;
        //        string CustomerActiveStatus = row.Cells[6].Text;
        //        string DealerCode = row.Cells[7].Text;
        //        string DealerFullName = row.Cells[8].Text;
        //        string DealerMobileNo = row.Cells[9].Text;
        //        string DealerEmailID = row.Cells[10].Text;
        //        string DealerActiveStatus = row.Cells[11].Text;
        //        string LoanCode = row.Cells[12].Text;
        //        string LoanAmount  = row.Cells[13].Text;
        //        string DownPayment = row.Cells[14].Text;
        //        string EMIAmount = row.Cells[15].Text;
        //        string Tenure = row.Cells[16].Text;
        //        string InterestRate = row.Cells[17].Text;
        //        string StartDate = row.Cells[18].Text;
        //        string EndDate = row.Cells[19].Text;
        //        string IMEINumber = row.Cells[20].Text;
        //        string Status  = row.Cells[21].Text;
        //        string PaidEMI = row.Cells[22].Text;
        //        string DuesEMI = row.Cells[23].Text;
        //        string ModelName = row.Cells[24].Text;
        //        string BrandName = row.Cells[25].Text;
        //        string VariantName = row.Cells[26].Text;
        //        string RecordStatus = row.Cells[27].Text;
        //        string ProcessingFees = row.Cells[28].Text;
        //        string InterestAmt = row.Cells[29].Text;
        //        string Remarks = row.Cells[30].Text;
        //        string creditScore = row.Cells[31].Text;
        //        string LateFine = row.Cells[32].Text;

        //        txtCustomerCode.Text = CustomerCode;
        //        txtCustomerFullName.Text = CustomerFullName;
        //        txtCustomerMobileNo.Text = CustomerMobileNo;
        //        txtCustomerEmailid.Text = CustomerEmailID;

        //        txtDealerid.Text = DealerCode;
        //        txtDealerFullName.Text = DealerFullName;
        //        txtDealerMobileNo.Text = DealerMobileNo;
        //        txtDealerEmailid.Text = DealerEmailID;

        //        txtLoanCode.Text = LoanCode;
        //        txtLoanAmount.Text = LoanAmount;
        //        txtDownPayment.Text = DownPayment;
        //        txtLoanEMIAmount.Text = EMIAmount;
        //        txtTenure.Text = Tenure;
        //        txtInterestRate.Text = InterestRate;
        //        txtStartDate.Text = StartDate;
        //        txtEndDate.Text = EndDate;
        //        txtTotalDueEMIMonths.Text = DuesEMI;
        //        txtTotalPaidEMIMonths.Text = PaidEMI;
        //        txtProcessingFees.Text = ProcessingFees;
        //        txtInterestAmount.Text = InterestAmt;
        //        int duesEmi = Convert.ToInt32(DuesEMI);
        //        decimal emiAmount = Convert.ToDecimal(EMIAmount);            
        //        txtLoanStatus.Text = Status;
        //        txtCreditScore.Text = creditScore;
        //        txtLateFinePerMonth.Text = LateFine;
        //        decimal lateEmi = Convert.ToDecimal(LateFine);
        //        decimal totalLateFine = lateEmi * duesEmi;
        //        txtTotalLateFine.Text = (totalLateFine).ToString();
        //        txtLateFinePerMonth.ReadOnly = true;
        //        txtTotalLateFine.ReadOnly = true;
        //        txtTotalDueEMIAmount.Text = ((duesEmi * emiAmount)+ totalLateFine).ToString();

        //        txtBrandName.Text = BrandName;
        //        txtModelName.Text = ModelName;
        //        txtModelVariant.Text = VariantName;
        //        txtIMEI1Number.Text = IMEINumber;
        //    }
        //}


    }
}