using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;

namespace TheEMIClubApplication.Reports
{
    public partial class LoanEmiStatusReport : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.AppSettings["ConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUsers();
                LoadReport();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        private void GetUsers()
        {
            PortalCommon.BindDropDownList(
                ddluser,
                "GetRetailer",
                "", "", "", "", "",
                ""
            );
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCustomerCode.Text = "";
            ddluser.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            gvLoanEmiReport.DataSource = null;
            gvLoanEmiReport.DataBind();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "ResetAlert",
                "showAlert('info','Filters reset successfully');",
                true
            );
            LoadReport();
        }

        private void LoadReport()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_LoanEMISummaryReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ================= PARAMETERS =================
                    cmd.Parameters.AddWithValue("@CustomerCode",
                        string.IsNullOrWhiteSpace(txtCustomerCode.Text) ? (object)DBNull.Value : txtCustomerCode.Text.Trim());

                    cmd.Parameters.AddWithValue("@RetailerCode",
                        string.IsNullOrWhiteSpace(ddluser.SelectedValue) ? (object)DBNull.Value : ddluser.SelectedValue.Trim());

                    cmd.Parameters.AddWithValue("@FromDate",
                        string.IsNullOrWhiteSpace(txtFromDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtFromDate.Text));

                    cmd.Parameters.AddWithValue("@ToDate",
                        string.IsNullOrWhiteSpace(txtToDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtToDate.Text));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // ================= GRID BIND =================
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
                    // ================= KPI CALCULATIONS =================
                    CalculateKPI(dt);


                }
            }
        }

        private void CalculateKPI(DataTable dt)
        {
            decimal totalPaid = 0;
            decimal pendingAmount = 0;
           // decimal advancePaid = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalPaid += Convert.ToDecimal(row["TotalPaidAmount"]);
                pendingAmount += Convert.ToDecimal(row["PendingEMIAmount"]);
                //advancePaid += Convert.ToDecimal(row["AdvancePaidAmount"])
            }

            lblTotalLoans.Text = dt.Rows.Count.ToString();
            lblTotalPaid.Text = totalPaid.ToString("N2");
            lblPendingAmount.Text = pendingAmount.ToString("N2");
          //  lblAdvancePaid.Text = advancePaid.ToString("N2");
        }


        private void LoadLoanEmISchedule(string loanCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetLoanEmIScheduleWithStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoanCode", loanCode);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            gvEmiDetails.DataSource = dt;
            gvEmiDetails.DataBind();

                if (dt.Rows.Count > 0)
                {
                    // Total EMI count
           
                    decimal totalPaid = 0;
                    decimal totalPending = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        string status = row["EMIStatus"].ToString();
                        decimal emiAmount = Convert.ToDecimal(row["EMIAmount"]);
                        decimal fine = row.Table.Columns.Contains("Fine")
                                        ? Convert.ToDecimal(row["Fine"])
                                        : 0;

                        if (status == "Paid")
                        {
                            totalPaid += emiAmount + fine;   // include fine if needed
                        }
                        else
                        {
                            totalPending += emiAmount;       // pending EMI
                        }
                    }

                    lblpaid.Text = totalPaid.ToString("N2");
                    lblpending.Text = totalPending.ToString("N2");
                }

        }



        protected void gvLoanEmiReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewEMI")
            {
                string loanCode = e.CommandArgument.ToString();

                lblModalLoanCode.Text = loanCode;

                // Load EMI Grid
                LoadLoanEmISchedule(loanCode);

                // ✅ Bootstrap 5 modal open
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "OpenEmiModal",
                    "openEmiModal();",
                    true
                );
            }
        }


        protected void gvEmiDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "EMIStatus").ToString();

                if (status == "Paid")
                    e.Row.CssClass = "table-success";
                else
                    e.Row.CssClass = "table-warning";
            }
        }

    }
}
