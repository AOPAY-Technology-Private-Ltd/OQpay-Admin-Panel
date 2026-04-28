using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;

namespace TheEMIClubApplication.Reports
{
    public partial class LoanClosedReport : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.AppSettings["ConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRetailers();
                LoadReport();
            }
        }

        /* ================= FILTER EVENTS ================= */

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //ddlSettlementType.SelectedIndex = 0;
            ddluser.SelectedIndex = 0;
            txtCustomerCode.Text = "";
            txtLoanCode.Text = "";

            gvLoanEmiReport.DataSource = null;
            gvLoanEmiReport.DataBind();

            ResetKPI();

            ScriptManager.RegisterStartupScript(
                this, GetType(),
                "ResetAlert",
                "showAlert('info','Filters reset successfully');",
                true
            );
        }

        /* ================= DATA LOAD ================= */

        private void BindRetailers()
        {
            PortalCommon.BindDropDownList(
                ddluser,
                "GetRetailer",
                "", "", "", "", "", ""
            );
        }

        private void LoadReport()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("usp_GetClosedDetails_All", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

             
                cmd.Parameters.AddWithValue("@RetailerCode",
                    string.IsNullOrEmpty(ddluser.SelectedValue)
                    ? (object)DBNull.Value
                    : ddluser.SelectedValue);

                cmd.Parameters.AddWithValue("@LoanCode",
                    string.IsNullOrEmpty(txtLoanCode.Text)
                    ? (object)DBNull.Value
                    : txtLoanCode.Text.Trim());

                cmd.Parameters.AddWithValue("@CustomerCode",
                    string.IsNullOrEmpty(txtCustomerCode.Text)
                    ? (object)DBNull.Value
                    : txtCustomerCode.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            gvLoanEmiReport.DataSource = dt;
            gvLoanEmiReport.DataBind();

            if (dt == null || dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "NoRecords",
                    "showAlert('warning','No records found!');",
                    true
                );
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "RecordsFound",
                    "showAlert('success','Records loaded successfully');",
                    true
                );
            }
            CalculateKPI(dt);
        }
        protected void gvLoanEmiReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* ================= SR NO (Paging-safe) ================= */
                int srNo =
                    (gvLoanEmiReport.PageIndex * gvLoanEmiReport.PageSize)
                    + e.Row.RowIndex + 1;

                Label lblSrNo = (Label)e.Row.FindControl("lblSrNo");
                if (lblSrNo != null)
                {
                    lblSrNo.Text = srNo.ToString();
                }

                /* ================= RECORD STATUS (ALWAYS CLOSED) ================= */
                Label lblRecordStatus = (Label)e.Row.FindControl("lblRecordStatus");
                if (lblRecordStatus != null)
                {
                    lblRecordStatus.Text = "CLOSED";
                    lblRecordStatus.CssClass += " badge rounded-pill bg-dark text-white px-3 py-2";
                }

                /* ================= CLOSURE STATUS (SETTLEMENT / FORECLOSURE) ================= */
                Label lblClosureStatus = (Label)e.Row.FindControl("lblClosureStatus");
                if (lblClosureStatus != null)
                {
                    string closureStatus = lblClosureStatus.Text.Trim().ToUpper();

                    if (closureStatus == "SETTLEMENT")
                    {
                        lblClosureStatus.CssClass += " badge rounded-pill bg-success text-white px-3 py-2";
                    }
                    else if (closureStatus == "FORECLOSURE")
                    {
                        lblClosureStatus.CssClass += " badge rounded-pill bg-danger text-white px-3 py-2";
                    }
                    else
                    {
                        lblClosureStatus.CssClass += " badge rounded-pill bg-secondary text-white px-3 py-2";
                    }
                }
            }
        }




        /* ================= KPI CALCULATION ================= */
        protected void gvLoanEmiReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoanEmiReport.PageIndex = e.NewPageIndex;
            LoadReport(); // rebind data
        }

        private void CalculateKPI(DataTable dt)
        {
            decimal totalPaid = 0;
            decimal totalFine = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["FinalPaidAmount"] != DBNull.Value)
                    totalPaid += Convert.ToDecimal(row["FinalPaidAmount"]);

                if (row["SettlementOrForeclosureCharg"] != DBNull.Value)
                    totalFine += Convert.ToDecimal(row["SettlementOrForeclosureCharg"]);
            }

            lblTotalLoans.Text = dt.Rows.Count.ToString();
            lblTotalPaid.Text = totalPaid.ToString("N2");
            lblPendingAmount.Text = totalFine.ToString("N2");
        }

        private void ResetKPI()
        {
            lblTotalLoans.Text = "0";
            lblTotalPaid.Text = "0.00";
            lblPendingAmount.Text = "0.00";
        }

        /* ================= EMI MODAL ================= */

        //protected void gvLoanEmiReport_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "ViewEMI")
        //    {
        //        string loanCode = e.CommandArgument.ToString();
        //        lblModalLoanCode.Text = loanCode;

        //        LoadLoanEmISchedule(loanCode);

        //        ScriptManager.RegisterStartupScript(
        //            this, GetType(),
        //            "OpenEmiModal",
        //            "openEmiModal();",
        //            true
        //        );
        //    }
        //}

        private void LoadLoanEmISchedule(string loanCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("sp_GetLoanEmIScheduleWithStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoanCode", loanCode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            gvLoanEmiReport.DataSource = dt;
            gvLoanEmiReport.DataBind();

            decimal totalPaid = 0;
            decimal totalPending = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal emiAmount = Convert.ToDecimal(row["EMIAmount"]);
                decimal fine = row.Table.Columns.Contains("Fine") && row["Fine"] != DBNull.Value
                                ? Convert.ToDecimal(row["Fine"])
                                : 0;

                if (row["EMIStatus"].ToString() == "Paid")
                    totalPaid += emiAmount + fine;
                else
                    totalPending += emiAmount;
            }

            lblTotalPaid.Text = totalPaid.ToString("N2");
            lblPendingAmount.Text = totalPending.ToString("N2");
        }

        protected void gvEmiDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "EMIStatus").ToString();
                e.Row.CssClass = status == "Paid"
                    ? "table-success"
                    : "table-warning";
            }
        }
    }
}
