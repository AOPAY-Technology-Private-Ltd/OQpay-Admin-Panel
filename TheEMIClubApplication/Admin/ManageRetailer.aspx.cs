using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.AppCode;
using System.Data.SqlClient;

namespace TheEMIClubApplication.Admin
{
    public partial class ManageRetailer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRecentLoanApplicationforadmin();
            }
        }

        protected void gvRecentApplication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("VIEW")) || (e.CommandName.Equals("ASG")) || (e.CommandName.Equals("COLL")))
                {
                    //|| (e.CommandName.Equals("ACT"))


                    string tempName = Convert.ToString(e.CommandArgument);
                    string[] tempNameArr = tempName.Split(new char[] { '|' });
                    string loanid = tempNameArr[0];

                    //  string RID = tempNameArr[1];
                    //  string Phone = tempNameArr[2];

                    if (e.CommandName.Equals("VIEW"))
                    {
                        string temp = loanid;
                        Response.Redirect("../Admin/RetailerDetails.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }

                    else if (e.CommandName.Equals("ASG"))
                    {
                        string temp = loanid;
                        Response.Redirect("../CustomerDetails/AssignApplicationToUser.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }

                    else if (e.CommandName.Equals("COLL"))
                    {
                        string temp = loanid;
                        Response.Redirect("../Collection/Collection.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }

                    //else if (e.CommandName.Equals("ACT"))
                    //{



                    //    objManageUser.UserName = Userid;


                    //    if (active_YN.Equals(Constants.IsActive)) //"Activate?"
                    //    {
                    //        objManageUser.Active_YN = "Y";
                    //    }
                    //    else
                    //    {
                    //        objManageUser.Active_YN = "N";
                    //    }

                    //    UpdateUserActiveOrInactive();
                    //    GetUserDetail();


                    //}


                }

            }

            catch (Exception ex)
            {
                // Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

        protected void gvRecentApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvRecentApplication.PageIndex = e.NewPageIndex;
            GetRecentLoanApplicationforadmin();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvRecentApplication.PageCount)
            {
                gvRecentApplication.PageIndex = e.NewPageIndex;
                //CustomerBindGrid();
            }
        }

        protected void gvRecentApplication_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gvRecentApplication.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvRecentApplication.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvRecentApplication.PageIndex < gvRecentApplication.PageCount - 1;
            }
        }
        private void GetRecentLoanApplicationforuser()
        {
            BLManageUser objManageUser = new BLManageUser();
            DataTable dtManageUser = new DataTable();

            try
            {
                objManageUser.Assignuser = AppSessions.SessionLoginId;
                objManageUser.Flag = !string.IsNullOrEmpty(objManageUser.Assignuser) ? "Admin" : "";

                objManageUser.Name = txtname.Text.Trim();
                objManageUser.MobileNo = txtmobileNo.Text.Trim();
                objManageUser.custCode = txtCode.Text.Trim();
                objManageUser.Status = ddlStatus.SelectedValue == "0" ? string.Empty : ddlStatus.SelectedValue;

                dtManageUser = objManageUser.GetRecentLoanDetails();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvRecentApplication.DataSource = dtManageUser;
                    gvRecentApplication.DataBind();
                }
                else
                {
                    // MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    //spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvRecentApplication.DataSource = null;
                    gvRecentApplication.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtManageUser.Dispose();
                dtManageUser = null;
            }
        }



        protected void btnSearch_click(object sender, EventArgs e)
        {

            try
            {

                spnMessage.InnerText = string.Empty;
                string group = AppSessions.SessionUserRoleCode;

                if (group.Trim().ToUpper() == "SUPERADMIN")
                {
                    GetRecentLoanApplicationforadmin();

                }
                else
                {
                    GetRecentLoanApplicationforuser();

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
        private void GetRecentLoanApplicationforadmin()
        {
            BLManageUser objManageUser = new BLManageUser();
            DataTable dtManageUser = new DataTable();

            try
            {
                objManageUser.Assignuser = "";
                objManageUser.Flag = "Admin";
                objManageUser.Name = txtname.Text.Trim();
                objManageUser.MobileNo = txtmobileNo.Text.Trim();
                objManageUser.custCode = txtCode.Text.Trim();
                objManageUser.Status = ddlStatus.SelectedValue == "0" ? string.Empty : ddlStatus.SelectedValue;



                dtManageUser = objManageUser.GetRecentLoanDetails();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvRecentApplication.PageSize = 15;
                    gvRecentApplication.DataSource = dtManageUser;
                    gvRecentApplication.DataBind();

                    for (int i = 0; i < gvRecentApplication.Rows.Count; i++)
                    {
                        if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Assign))//showing Activate?
                        {

                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;

                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = false;

                            gvRecentApplication.Rows[i].Cells[7].Text = "";

                            gvRecentApplication.Rows[i].Cells[7].ForeColor = Color.Yellow;
                            gvRecentApplication.Rows[i].Cells[5].ForeColor = Color.DarkGoldenrod;
                            gvRecentApplication.Rows[i].Cells[5].Font.Bold = true;

                        }
                        else if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Rejected))
                        {
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = false;
                            //  ((LinkButton)gvRecentApplication.Rows[i].Cells[9].FindControl("Settlement")).Enabled = false;
                            gvRecentApplication.Rows[i].Cells[7].Text = "";

                            gvRecentApplication.Rows[i].Cells[5].ForeColor = Color.Red;
                            gvRecentApplication.Rows[i].Cells[5].Font.Bold = true;
                        }

                        else if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Approved))
                        {
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = true;
                            gvRecentApplication.Rows[i].Cells[7].Text = "";
                            gvRecentApplication.Rows[i].Cells[5].ForeColor = Color.Navy;
                            gvRecentApplication.Rows[i].Cells[5].Font.Bold = true;



                        }


                        else if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Disbursement))
                        {
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = true;

                            gvRecentApplication.Rows[i].Cells[5].ForeColor = Color.Green;
                            gvRecentApplication.Rows[i].Cells[5].Font.Bold = true;
                            gvRecentApplication.Rows[i].Cells[7].Text = "";


                        }

                        else if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Pending))
                        {
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = false;
                            //  ((LinkButton)gvRecentApplication.Rows[i].Cells[9].FindControl("Settlement")).Enabled = false;
                            gvRecentApplication.Rows[i].Cells[5].ForeColor = Color.Maroon;
                            gvRecentApplication.Rows[i].Cells[5].Font.Bold = true;

                        }

                        else if (gvRecentApplication.Rows[i].Cells[5].Text.Equals(Constants.Closed))
                        {

                            ((LinkButton)gvRecentApplication.Rows[i].Cells[6].FindControl("lnkManageview")).Enabled = true;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[7].FindControl("lnkManageassign")).Enabled = false;
                            ((LinkButton)gvRecentApplication.Rows[i].Cells[8].FindControl("lnkcollection")).Enabled = false;

                            gvRecentApplication.Rows[i].Cells[7].Text = "";
                        }




                    }
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvRecentApplication.DataSource = null;
                    gvRecentApplication.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtManageUser.Dispose();
                dtManageUser = null;
            }
        }
    }
}