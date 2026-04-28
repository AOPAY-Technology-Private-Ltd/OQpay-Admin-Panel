using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.Admin
{
    public partial class ManageDisburshment : System.Web.UI.Page
    {
        BLLoanApproval objLoan = new BLLoanApproval();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomerBindGrid();

                // Check if there is a success message in QueryString
                string msg = Request.QueryString["msg"];
                if (!string.IsNullOrEmpty(msg))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
                }
            }
        }

        private void CustomerBindGrid()
        {
            BLLoanApproval objCustomerMst = new BLLoanApproval();
           

            DataTable dt = objCustomerMst.GetLoanDetailsfordisburshment();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Clear any previous message
                gvDisbursement.DataSource = dt;
                gvDisbursement.DataBind();
            }
            else
            {
                gvDisbursement.DataSource = null;
                gvDisbursement.DataBind();
               // lblCustomerNoData.Text = "No records found";
            }
        }


        protected void gvDisbursement_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DisburseLoan")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string loanCode = args[0];
   

                // Redirect to Disbursement page with Loan & Customer details
                Response.Redirect($"loandisburshment.aspx?LoanCode={loanCode}");
            }
        }
    }
}