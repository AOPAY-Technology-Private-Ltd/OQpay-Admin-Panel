using AVFramework;
using System;
using System.Collections.Generic;
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
    public partial class AssignLoanApplication : System.Web.UI.Page
    {

        BLAssignapplicationLoan AssignApplication = new BLAssignapplicationLoan();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Bindgrid();
                    PortalCommon.BindDropDownList(ddlemployee, "empUser", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }



        protected void btnEMISearch_Click(object sender, EventArgs e)
        {
            Bindgrid();
        }

        private void Bindgrid()
        {
            BLEMIDetails objEMI = new BLEMIDetails();
            DataTable EMIdt = new DataTable();

            try
            {
                objEMI.Mode = "GET";

                if (ddlEmiCriteria.SelectedItem.Text == "All")
                {
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Emiid")
                {
                    //objEMI. = Convert.ToInt32(txtEmivalue.Text.Trim());
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "loanid")
                {
                    objEMI.Loancode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "userid")
                {
                    objEMI.Customercode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Active")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Active";
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Inactive")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Inactive";
                    EMIdt = objEMI.GetAssignEMIDetails();
                }



                grdAssignApplication.DataSource = EMIdt;

                if (EMIdt.Rows.Count > 0)
                {
                    
                    grdAssignApplication.DataBind();
                }
                else
                {
                    //lblEMINoData.Text = "No Data Found !!";
                    grdAssignApplication.DataBind();
                }




                // Calculate pagination count
                int totalRecords = EMIdt.Rows.Count;
                int startRecord = (grdAssignApplication.PageIndex * grdAssignApplication.PageSize) + 1;
                int endRecord = startRecord + grdAssignApplication.PageSize - 1;

                if (endRecord > totalRecords)
                    endRecord = totalRecords;

                if (totalRecords > 0)
                    lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
                else
                    lblEMINoData.Text = "No records found";

            }
            catch (Exception ex)
            {
                // lblTotalEmicollection.Text = "Error";
            }

        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            try
            {

                if (ddlemployee.SelectedItem.Text == "-Select-")
                {
                   spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "VAL1105");
                }
                else
                {
                    for (int i = 0; i < grdAssignApplication.Rows.Count; i++)
                    {
                        if (((CheckBox)grdAssignApplication.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)

                        {
                            AssignApplication.RID = Convert.ToInt32(grdAssignApplication.DataKeys[i].Value); 
                            AssignApplication.Loanid = grdAssignApplication.Rows[i].Cells[2].Text;
                            AssignApplication.Employeecode = ddlemployee.SelectedValue;
                            AssignApplication.Employeename = ddlemployee.SelectedItem.Text;
                            AssignApplication.Dealercode = grdAssignApplication.Rows[i].Cells[3].Text;
                            AssignApplication.Emistatus = "Assign";

                            int retVal = AssignApplication.AssignEmi();

                            if (retVal > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "Loan EMI Application Assigned Successfully to: " + ddlemployee.SelectedItem.Text.Trim() + "');", true);
                            }

                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "Please Try After some Time" + "');", true);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void grdAssignApplication_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string flagStatus = DataBinder.Eval(e.Row.DataItem, "flag_Status")?.ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("CheckBox1");
                LinkButton lnkReassign = (LinkButton)e.Row.FindControl("lnkReassign");

                if (chk != null)
                {
                    if (!string.IsNullOrEmpty(flagStatus) && flagStatus.Equals("Assign", StringComparison.OrdinalIgnoreCase))
                    {
                        // Default: disable checkbox
                        chk.Enabled = false;
                        chk.ToolTip = "Already assigned. Click Reassign to enable.";
                        chk.BackColor = System.Drawing.Color.LightGray;

                        // Show Reassign button
                        if (lnkReassign != null)
                        {
                            lnkReassign.Visible = true;
                        }
                    }
                    else
                    {
                        // Not assigned
                        chk.Enabled = true;
                        chk.ToolTip = "Not assigned";
                        chk.BackColor = System.Drawing.Color.White;

                        if (lnkReassign != null)
                        {
                            lnkReassign.Visible = false;
                        }
                    }
                }
            }
        }




        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");


        }
        protected void grdAssignApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Bindgrid();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < grdAssignApplication.PageCount)
            {
                grdAssignApplication.PageIndex = e.NewPageIndex;
                Bindgrid();
            }
        }

        protected void grdAssignApplication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reassign")
            {
                // Get the row index
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                GridViewRow row = grdAssignApplication.Rows[rowIndex];

                // Enable the checkbox in this row
                CheckBox chk = (CheckBox)row.FindControl("CheckBox1");
                if (chk != null)
                {
                    chk.Enabled = true;
                    chk.BackColor = System.Drawing.Color.LightYellow; // optional highlight
                    chk.ToolTip = "Select to reassign this loan";
                }
            }
        }

    }
}