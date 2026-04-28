using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.Admin
{
    public partial class ManageHoldAmount : System.Web.UI.Page
    {
        BLReports objData = new BLReports();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUsers();
                BindGrid();
                CheckBox1.Checked = false;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                //ddlReports_SelectedIndexChanged(ddlReports, EventArgs.Empty);
            }
        }

        private void GetUsers()
        {
            PortalCommon.BindDropDownList(ddluser, "GetRetailer", "", "", "", "", "", "All Record");
        }

        private void BindGrid()
        {
            objData.SearchMode = "HoldAmountReport";

            dt = objData.AllTransactionReports();

            if (dt.Rows.Count > 0)
            {

                gvAllReport.DataSource = dt;
                gvAllReport.DataBind();

            }
            else
            {
                gvAllReport.EmptyDataText = "No records found.";
                gvAllReport.DataBind();
            }


        }

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

        protected void gvAllReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "PayHoldAmount")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string RetailerCode = gvAllReport.DataKeys[index]["RetailerCode"].ToString();

                Response.Redirect($"HoldAmount_Payout.aspx?id={RetailerCode}", false);
            }
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

        }



    }
}