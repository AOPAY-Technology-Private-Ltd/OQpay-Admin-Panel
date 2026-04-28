using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.Reports
{
    public partial class AllReports : System.Web.UI.Page
    {
        BLReports objData = new BLReports();
        DataTable dt = new DataTable();
        decimal reportTotal = 0;
        int amountColumnIndex = -1;
        string amountColumnName = "";
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                GetUsers();
                BindGrid();
                CheckBox1.Checked = false;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                ddlReports_SelectedIndexChanged(ddlReports, EventArgs.Empty);
            }
        }


        private void GetUsers()
        {
            PortalCommon.BindDropDownList(ddluser, "GetRetailer", "", "", "", "", "", "All Record");
        }


        private void BindGrid()
        {
            reportTotal = 0;
            amountColumnIndex = -1;
            amountColumnName = "";
            objData.SearchMode = ddlReports.SelectedValue;
            dt = objData.AllTransactionReports();

            if (dt.Rows.Count > 0)
            {
                gvAllReport.DataSource = dt;
                gvAllReport.DataBind();
            }
            else
            {
                gvAllReport.EmptyDataText = "No records found for the selected filters.";
                gvAllReport.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                objData.SearchMode = ddlReports.SelectedValue;
                if (ddluser.SelectedValue == "0")
                {
                    objData.Retailercode = "";
                }
                else
                {
                    //objData.Name = ddluser.SelectedValue;
                    objData.Retailercode = ddluser.SelectedValue;
                }
                string selectedParam = ddlSearchdiffrentParams.SelectedValue;
                
                string searchValue = string.IsNullOrWhiteSpace(txtSearchValues.Text) ? null : txtSearchValues.Text.Trim();

                if (!string.IsNullOrEmpty(selectedParam) && selectedParam != "0" && !string.IsNullOrEmpty(searchValue))
                {
                    switch (selectedParam)
                    {
                        case "Customercode":
                            objData.CustomerCode = searchValue;
                            break;

                        case "LoanCode":
                            objData.LoanCode = searchValue;
                            break;

                        case "Name":

                            objData.Name = searchValue;
                            break;

                        case "ReceiptNo":
                            objData.ReceiptNo = searchValue;
                            break;

                        case "PaymentMode":
                            objData.PaymentMode = searchValue;
                            break;

                        case "RecordStatus":
                            objData.RecordStatus = searchValue;
                            break;

                        case "Status":
                            objData.ActiveStatus = searchValue;
                            break;
                        case "MobileNo":
                            objData.MobileNo = searchValue;
                            break;
                        case "EmailId":
                            objData.Emailid = searchValue;
                            break;
                        case "StoreName":
                            objData.StoreName = searchValue;
                            break;
                    }
                }

                // Handle date filters
                checkInDis();

                // Call the stored procedure and bind
                if (!string.IsNullOrEmpty(objData.SearchMode))
                {
                    BindGrid();
                }
                else
                {
                    gvAllReport.EmptyDataText = "Please select a report type.";
                    gvAllReport.DataBind();
                }


            }
            catch (Exception ex)
            {
                gvAllReport.EmptyDataText = "❌ Error: " + ex.Message;
                gvAllReport.DataBind();
            }
        }

        protected void gvAllReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "PayHoldAmount")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = gvAllReport.Rows[index];

            //    string RetailerCode = gvAllReport.DataKeys[index]["RetailerCode"].ToString();

            //    Response.Redirect("HoldAmount_Payout.aspx?id=" + RetailerCode, false);
            //    //bool result = objBL.ReleaseHoldAmount(loanCode);

            //    if (result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
            //            "alert('✅ Hold amount released successfully.');", true);
            //        BindGrid();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
            //            "alert('❌ Failed to release hold amount.');", true);
            //    }
            //}

            if (e.CommandName == "PayHoldAmount")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string RetailerCode = gvAllReport.DataKeys[index]["RetailerCode"].ToString();

                Response.Redirect($"HoldAmount_Payout.aspx?id={RetailerCode}", false);
            }
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //    objData.Retailercode = ddluser.SelectedValue == "All Record" ? null : ddluser.SelectedValue;

        //    try
        //    {
        //        if (ddlReports.SelectedValue == "EMICollection")
        //        {

        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();

        //        }
        //        else if (ddlReports.SelectedValue == "LoanAmountCreated")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "ProcessingCharge")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "MembershipCharge")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "HoldAmountReport")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "WalletAmount")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "LatePaymentCharge")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "InterestAmount")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;


        //            checkInDis();
        //            BindGrid();
        //        }
        //        else if (ddlReports.SelectedValue == "ServiceCharge")
        //        {
        //            objData.SearchMode = ddlReports.SelectedValue;
        //            objData.CustomerCode = txtSearchValues.Text.Trim();
        //            objData.LoanCode = string.Empty;

        //            checkInDis();


        //            BindGrid();
        //        }
        //        else
        //        {

        //        }

        //        clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        gvAllReport.EmptyDataText = "❌ Error: " + ex.Message;
        //        gvAllReport.DataBind();
        //    }

        //}


        private void checkInDis()
        {
            if (CheckBox1.Checked)
            {

                objData.FromDate = null;
                objData.ToDate = null;
            }
            else
            {

                if (!string.IsNullOrEmpty(txtFromDate.Text))
                    objData.FromDate = Convert.ToDateTime(txtFromDate.Text);

                if (!string.IsNullOrEmpty(txtToDate.Text))
                    objData.ToDate = Convert.ToDateTime(txtToDate.Text);
            }
        }

        protected void gvAllReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllReport.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
            }
            else
            {
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }

            //bool disableDates = CheckBox1.Checked;
            //txtFromDate.Enabled = !disableDates;
            //txtToDate.Enabled = !disableDates;
        }

        private void clear()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ddlReports.SelectedIndex = 0;
            ddluser.SelectedIndex = 0;
            txtSearchValues.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlReports.SelectedIndex = 0;
            ddlReports_SelectedIndexChanged(sender, e);
            clear();
        }

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReport = ddlReports.SelectedValue;

            ddlSearchdiffrentParams.Items.Clear();

            ddlSearchdiffrentParams.Items.Add(new ListItem("--Select--", "0"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("Retailercode", "Retailercode"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("Customercode", "Customercode"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("LoanCode", "LoanCode"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("Name", "Name"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("ReceiptNo", "ReceiptNo"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("PaymentMode", "PaymentMode"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("RecordStatus", "RecordStatus"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("MobileNo", "MobileNo"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("EmailId", "EmailId"));
            ddlSearchdiffrentParams.Items.Add(new ListItem("StoreName", "StoreName"));


            if (selectedReport == "EMICollection")
            {
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }

            else if (selectedReport == "LoanAmountCreated")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");

            }
            else if (selectedReport == "ProcessingCharge")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }
            else if (selectedReport == "MembershipCharge")
            {
                ddlSearchdiffrentParams.Items.Remove("LoanCode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("RecordStatus");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }
            else if (selectedReport == "HoldAmountReport")
            {
                ddlSearchdiffrentParams.Items.Remove("LoanCode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("RecordStatus");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }

            else if (selectedReport == "WalletAmount")
            {
                ddlSearchdiffrentParams.Items.Remove("RecordStatus");
                ddlSearchdiffrentParams.Items.Remove("LoanCode");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("Customercode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
                ddlSearchdiffrentParams.Items.Add(new ListItem("TransactionNo", "TransactionNo"));
                ddlSearchdiffrentParams.Items.Add(new ListItem("AmountType", "AmountType"));
            }

            else if (selectedReport == "LatePaymentCharge")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");

            }

            else if (selectedReport == "InterestAmount")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }

            else if (selectedReport == "ServiceCharge")
            {
                ddlSearchdiffrentParams.Items.Remove("LoanCode");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("RecordStatus");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");


            }
            else if (selectedReport == "PayoutReport")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
                ddlSearchdiffrentParams.Items.Remove("MobileNo");
                ddlSearchdiffrentParams.Items.Remove("EmailId");
                ddlSearchdiffrentParams.Items.Remove("StoreName");
            }
            else if (selectedReport == "RetailerwiseLoanReport")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
            }

            else if (selectedReport == "RetailerSettlementReport")
            {
                ddlSearchdiffrentParams.Items.Remove("Name");
                ddlSearchdiffrentParams.Items.Remove("ReceiptNo");
                ddlSearchdiffrentParams.Items.Remove("PaymentMode");
                ddlSearchdiffrentParams.Items.Remove("Retailercode");
            }
            else
            {
                lblErrorMsg.Text = "Please Select Valid Type";

            }
            BindGrid();
        }



        protected void gvAllReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            amountColumnName = GetAmountColumnByReport(ddlReports.SelectedValue);

            if (string.IsNullOrEmpty(amountColumnName))
                return;

            // 🔹 HEADER → find correct column index
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text.Trim()
                        .Equals(amountColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        amountColumnIndex = i;
                        break;
                    }
                }
            }

            // 🔹 DATA ROW → accumulate total
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (amountColumnIndex >= 0)
                {
                    string cellValue = e.Row.Cells[amountColumnIndex]
                                       .Text.Replace("&nbsp;", "").Trim();

                    decimal amount;
                    if (decimal.TryParse(cellValue, out amount))
                    {
                        reportTotal += amount;
                    }
                }
            }

            // 🔹 FOOTER → show total
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (amountColumnIndex >= 0)
                {
                    e.Row.Cells[0].Text = "TOTAL";
                    e.Row.Cells[0].Font.Bold = true;

                    e.Row.Cells[amountColumnIndex].Text = reportTotal.ToString("N2");
                    e.Row.Cells[amountColumnIndex].Font.Bold = true;
                }
            }
        }


        private string GetAmountColumnByReport(string reportName)
        {
            switch (reportName)
            {
                case "ProcessingCharge":
                    return "ProcessingFees";

                case "MembershipCharge":
                    return "MemberShip";

                case "HoldAmountReport":
                    return "HoldingAmount";

                case "WalletAmount":
                    return "TransferAmt";

                case "LatePaymentCharge":
                    return "LatePayment Charge";

                case "InterestAmount":
                    return "InterestAmt";

                case "ServiceCharge":
                    return "Services_charge_Amt";

                case "PayoutReport":
                    return "DR";

                case "RetailerSettlementReport":
                    return "SettlementAmount";

                case "RetailerwiseLoanReport":
                    return "LoanAmount";

                default:
                    return "";
            }
        }

    }
}