using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;

namespace TheEMIClubApplication.Reports
{
    public partial class EmiPendingReports : System.Web.UI.Page
    {
        BLMakePayment objMakepayment = new BLMakePayment();
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }


        protected void btnmakepayment_Click(object sender, EventArgs e)
        {
            // Simply call BindGridView(); filtering is handled inside
            BindGridView();
        }

        private void BindGridView()
        {
            try
            {
                objMakepayment.Mode = "SUMMARY";

                // 🔹 Default filters
                objMakepayment.Customercode = string.Empty;
                objMakepayment.Loancode = string.Empty;
                objMakepayment.Recordstatus = string.Empty;

                // 🔹 Get selected criteria and search value
                string criteria = ddlSearchCriteria.SelectedValue;   // Use Value
                string value = txtSearchValue.Text.Trim();

                // 🔹 Apply filters
                switch (criteria)
                {
                    case "CustomerCode":
                        if (!string.IsNullOrEmpty(value))
                            objMakepayment.Customercode = value;
                        break;

                    case "LoanCode":
                        if (!string.IsNullOrEmpty(value))
                            objMakepayment.Loancode = value;
                        break;

                    case "Pending":
                    case "Approved":
                    case "Rejected":
                        objMakepayment.Recordstatus = criteria;
                        break;

                    case "All":
                    default:
                        // No filters
                        break;
                }

                // 🔹 Bind data
                DataTable dt = objMakepayment.ApproveMakePayment();
                gvMakePayment.DataSource = dt;
                gvMakePayment.DataBind();

                // 🔹 Record count display
                int totalRecords = dt.Rows.Count;
                int startRecord = (gvMakePayment.PageIndex * gvMakePayment.PageSize) + 1;
                int endRecord = Math.Min(startRecord + gvMakePayment.PageSize - 1, totalRecords);

              
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
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "PendingEMI")
            {
                pnlemiSearchCriteria.Visible = true;
                panelmakepayment.Visible = false;
                PortalCommon.BindDropDownList(ddluser, "GetRetailer", string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");

                // BindMonthYearDropdown(); // if needed
                LoadPendingEMI();
            }
            else if (ddlReportType.SelectedValue == "MakePayment")
            {
                pnlemiSearchCriteria.Visible = false;
                panelmakepayment.Visible = true;
                BindGridView();
            }
            else
            {
                pnlemiSearchCriteria.Visible = false;
                panelmakepayment.Visible = false;
            }
        }




        private void BindMonthYearDropdown()
        {
            DateTime selectedDate;
            if (DateTime.TryParse(txtEmiDate.Text, out selectedDate))
            {
                int selectedMonth = selectedDate.Month;
                int selectedYear = selectedDate.Year;
                // use selectedMonth & selectedYear
            }
        }

        private void LoadPendingEMI()
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            DateTime selectedDate = DateTime.Now; // default fallback
            if (!DateTime.TryParse(txtEmiDate.Text, out selectedDate))
            {
                // if no date selected, use today's date as default
                selectedDate = DateTime.Now;
            }

            int selectedMonth = selectedDate.Month;
            int selectedYear = selectedDate.Year;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetPendingEMIReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ use date picker month/year
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);

                    cmd.Parameters.AddWithValue("@CustomerName", string.IsNullOrEmpty(txtCustomerName.Text) ? (object)DBNull.Value : txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerPhone", string.IsNullOrEmpty(txtCustomerPhone.Text) ? (object)DBNull.Value : txtCustomerPhone.Text);
                    cmd.Parameters.AddWithValue(
     "@RetailerCode",
     ddluser.SelectedValue == "0" ? (object)DBNull.Value : ddluser.SelectedValue
 );

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvPendingEMI.DataSource = dt;
                    gvPendingEMI.DataBind();
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPendingEMI();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";
            PortalCommon.BindDropDownList(ddluser, "GetRetailer", string.Empty,
            string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");

            LoadPendingEMI();
        }
    }
}