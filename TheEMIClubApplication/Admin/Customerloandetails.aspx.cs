using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using AVFramework;
using TheEMIClubApplication.AppCode;


namespace TheEMIClubApplication.Admin
{
    public partial class Customerloandetails : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortalCommon.BindDropDownList(ddlRetailer, "GetRetailer", string.Empty,
                     string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");
                //  LoadRetailers();
                LoadLoanDetails();
            }
            

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblModelRecordCount.Text = "";
            LoadLoanDetails();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Clear all filters
            txtLoanCode.Text = "";
            txtCustomerCode.Text = "";
            //txtRetailerCode.Text = "";
           // txtEmployeeCode.Text = "";
            txtBrandName.Text = "";
            txtModelName.Text = "";
            txtVariantName.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";

            LoadLoanDetails();
        }
        protected void gvLoanDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvLoanDetails.PageIndex = e.NewPageIndex;

            // Rebind the data to the GridView
            LoadLoanDetails();
        }
        private void LoadLoanDetails()
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SearchCustomerLoanSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@LoanCode", string.IsNullOrEmpty(txtLoanCode.Text.Trim()) ? (object)DBNull.Value : txtLoanCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@CustomerCode", string.IsNullOrEmpty(txtCustomerCode.Text.Trim()) ? (object)DBNull.Value : txtCustomerCode.Text.Trim());
                    var selectedValue = ddlRetailer.SelectedValue;

                    if (string.IsNullOrEmpty(selectedValue?.Trim()) || selectedValue.Trim() == "0")
                    {
                        cmd.Parameters.AddWithValue("@RetailerCode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RetailerCode", selectedValue.Trim());
                    }

                    cmd.Parameters.AddWithValue("@BrandName", string.IsNullOrEmpty(txtBrandName.Text.Trim()) ? (object)DBNull.Value : txtBrandName.Text.Trim());
                    cmd.Parameters.AddWithValue("@ModelName", string.IsNullOrEmpty(txtModelName.Text.Trim()) ? (object)DBNull.Value : txtModelName.Text.Trim());
                    cmd.Parameters.AddWithValue("@VariantName", string.IsNullOrEmpty(txtVariantName.Text.Trim()) ? (object)DBNull.Value : txtVariantName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(ddlStatus.SelectedValue) ? (object)DBNull.Value : ddlStatus.SelectedValue);

                    if (DateTime.TryParse(txtFromDate.Text, out DateTime fromDate))
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    else
                        cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);

                    if (DateTime.TryParse(txtToDate.Text, out DateTime toDate))
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                    else
                        cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            gvLoanDetails.DataSource = dt;
                            gvLoanDetails.DataBind();
                        }
                        else
                        {
                            lblModelRecordCount.Text = "No records found";
                            // No records found
                            gvLoanDetails.DataSource = null;
                            gvLoanDetails.DataBind();

                            //// Show error using toastr
                            //string errorMessage = Common.GetMessageFromXMLFile("MSG1001"); // e.g., "No records found."
                            //string script = $"toastr.error('{HttpUtility.JavaScriptStringEncode(errorMessage)}', 'Error');";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "showToastrError", script, true);
                        }
                    }
                }
            }
        }

    }
}