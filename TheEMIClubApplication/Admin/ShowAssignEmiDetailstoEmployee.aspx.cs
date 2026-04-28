using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.Admin
{
    public partial class ShowAssignEmiDetailstoEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindgridAssign();
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            BindgridAssign();
        }


        protected void grdAssignView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAssignView.PageIndex = e.NewPageIndex;
            BindgridAssign();
        }

        protected void grdAssignView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "test1" || e.CommandName == "ASG" ||
                    e.CommandName == "COLL" || e.CommandName == "Followup")
                {
                    string loanid = e.CommandArgument.ToString();

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                    string emiAmt = ((HiddenField)row.FindControl("hfEMIAmt")).Value;
                    string paidEmiValue = ((HiddenField)row.FindControl("hfPaidEMI")).Value;

                    int paidEMI = 0;
                    int.TryParse(paidEmiValue, out paidEMI);

                    int updatedPaidEMI = paidEMI + 1;

                    if (e.CommandName == "test1")
                    {
                        Response.Redirect("../Admin/EMIDetails.aspx?edocelor=" +
                            CryptoUtil.EncryptTripleDES(loanid), false);
                    }
                    else if (e.CommandName == "ASG")
                    {
                        Response.Redirect("../CustomerDetails/AssignApplicationToUser.aspx?edocelor=" +
                            CryptoUtil.EncryptTripleDES(loanid), false);
                    }
                    else if (e.CommandName == "COLL")
                    {
                        Response.Redirect("../Collection/Collection.aspx?edocelor=" +
                            CryptoUtil.EncryptTripleDES(loanid), false);
                    }
                    else if (e.CommandName.Equals("Followup"))
                    {
                        Response.Redirect("../Admin/LoanFollowUp.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid) +
                                          "&emiAmt=" + emiAmt + "&paidEMI=" + updatedPaidEMI, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(
                    PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

        private void CalculateAndBindKpis(DataTable dt)
        {
            int totalLoans = dt.Rows.Count;
            decimal totalPaidAmount = 0;
            decimal pendingEmiAmount = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal emiAmt = Convert.ToDecimal(row["EMIAmount"]);
                int paidEmi = Convert.ToInt32(row["PaidEMI"]);
                int tenure = Convert.ToInt32(row["Tenure"]);

                totalPaidAmount += (emiAmt * paidEmi);
                pendingEmiAmount += (emiAmt * (tenure - paidEmi));
            }

            BindKpiCards(totalLoans, totalPaidAmount, pendingEmiAmount);
        }

        private void BindKpiCards(int totalLoans, decimal totalPaid, decimal pendingEmi)
        {
            lblTotalLoans.Text = totalLoans.ToString();
            lblTotalPaid.Text = totalPaid.ToString("N2");
            lblPendingEmi.Text = pendingEmi.ToString("N2");
        }


        private void BindgridAssign()
        {
            BLEMIDetails objEMI = new BLEMIDetails();
            DataTable EMIdt;

            try
            {
                // ================= BASE REQUEST =================
                objEMI.Mode = "GET";
                objEMI.Customercode = AppSessions.SessionLoginId; // employee scope
            

                string criteria = ddlsearch.SelectedValue;
                string searchValue = TextBox1.Text.Trim();

            
                // ================= GET DATA =================
                EMIdt = objEMI.GetAssignEMIDetails();

                if (EMIdt == null || EMIdt.Rows.Count == 0)
                {
                    BindEmpty();
                    return;
                }

                // ================= USER ID FILTER (CustomerCode) =================
                if (criteria == "userid" && !string.IsNullOrEmpty(searchValue))
                {
                    var userRows = EMIdt.AsEnumerable()
                        .Where(r => r["CustomerCode"] != DBNull.Value &&
                                    r["CustomerCode"].ToString()
                                    .Equals(searchValue, StringComparison.OrdinalIgnoreCase));

                    if (userRows.Any())
                        EMIdt = userRows.CopyToDataTable();
                    else
                    {
                        BindEmpty();
                        return;
                    }
                }


                if (criteria == "loanid" && !string.IsNullOrEmpty(searchValue))
                {
                    var userRows = EMIdt.AsEnumerable()
                        .Where(r => r["LoanCode"] != DBNull.Value &&
                                    r["LoanCode"].ToString()
                                    .Equals(searchValue, StringComparison.OrdinalIgnoreCase));

                    if (userRows.Any())
                        EMIdt = userRows.CopyToDataTable();
                    else
                    {
                        BindEmpty();
                        return;
                    }
                }


                // ================= STATUS FILTER =================
                if (criteria == "Active" || criteria == "Inactive")
                {
                    var statusRows = EMIdt.AsEnumerable()
                        .Where(r => r["RecordStatus"] != DBNull.Value &&
                                    r["RecordStatus"].ToString()
                                    .Equals(criteria, StringComparison.OrdinalIgnoreCase));

                    if (statusRows.Any())
                        EMIdt = statusRows.CopyToDataTable();
                    else
                    {
                        BindEmpty();
                        return;
                    }
                }

                // ================= GRID BIND =================
                grdAssignView.DataSource = EMIdt;
                grdAssignView.DataBind();

                // ================= PAGINATION TEXT =================
                int totalRecords = EMIdt.Rows.Count;
                int startRecord = (grdAssignView.PageIndex * grdAssignView.PageSize) + 1;
                int endRecord = Math.Min(startRecord + grdAssignView.PageSize - 1, totalRecords);

                lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";

                // ================= KPI =================
                CalculateAndBindKpis(EMIdt);
            }
            catch (Exception ex)
            {
                BindEmpty();
            }
        }


        private void BindEmpty()
        {
            grdAssignView.DataSource = null;
            grdAssignView.DataBind();
            lblEMINoData.Text = "No records found";
            BindKpiCards(0, 0, 0);
        }



    }
}