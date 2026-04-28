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
    public partial class TransactionReport : System.Web.UI.Page
    {
        BLReports ReportDetails = new BLReports();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortalCommon.BindDropDownList(ddluser, "GetRetailer", string.Empty,
                     string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");
                //  LoadRetailers();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Create object of your business layer class
                var objReport = new BLReports();

                // Pass values from filters
                objReport.FromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim())
                        ? (DateTime?)null
                        : DateTime.Parse(txtFromDate.Text.Trim());

                objReport.ToDate = string.IsNullOrEmpty(txtToDate.Text.Trim())
                                        ? (DateTime?)null
                                        : DateTime.Parse(txtToDate.Text.Trim());
                objReport.Flag = ddlReports.SelectedValue; // or get from dropdown if needed
                objReport.Flag1 = ddlStatus.SelectedValue;
                objReport.parmUserName = ddluser.SelectedValue;

                // Call business layer method
                DataTable dt = objReport.ShowPayoutReportDatewise();

                // Bind result to GridView
                gvTransactionReport.DataSource = dt;
                gvTransactionReport.DataBind();

                // Show a message if no records
                if (dt.Rows.Count == 0)
                {
                    gvTransactionReport.EmptyDataText = "No records found for the selected filters.";
                }
            }
            catch (Exception ex)
            {
                // handle error (log or show friendly message)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Error: " + ex.Message + "');", true);
            }
        }
        protected void gvTransactionReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactionReport.PageIndex = e.NewPageIndex;
            btnSearch_Click(null, null); // reload data with current filters
        }

        protected void gvTransactionReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Status column

                // Credit & Debit columns styling
                int crColIndex = GetColumnIndexByName(e.Row, "CR");
                if (crColIndex >= 0)
                {
                    e.Row.Cells[crColIndex].CssClass = "text-success fw-bold text-end";
                }

                int drColIndex = GetColumnIndexByName(e.Row, "DR");
                if (drColIndex >= 0)
                {
                    e.Row.Cells[drColIndex].CssClass = "text-danger fw-bold text-end";
                }
            }
        }

        // Helper method to find column index by name
        private int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text.Equals(columnName, StringComparison.OrdinalIgnoreCase) ||
                    row.Cells[i].Controls.OfType<DataBoundLiteralControl>()
                        .Any(c => c.Text.Contains(columnName)))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}