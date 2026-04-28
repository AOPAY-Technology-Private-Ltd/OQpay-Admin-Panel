using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class ManageUser : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();
        DataTable dtManageUser = new DataTable();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                valReqApplication.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024"); //"Please select Application."


                valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013")); //Validation Error(s)...
                valReqNickName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1023"); //Please enter Nick Name.
                valrexNickName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1089");
                valReqholdingamt.ErrorMessage = Common.GetMessageFromXMLFile("VAL1093");
                //valReqAllowedDaysToViewReport.ErrorMessage = Common.GetMessageFromXMLFile("VAL1025"); //Please enter Allowed days to view Report.
                // valRegExpAllowedDaysToViewReport.ErrorMessage = Common.GetMessageFromXMLFile("VAL1026"); //Please enter only non-negative numbers without space.


                GetUserDetail();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        #endregion PageLoad

        #region GetUserDetail

        private void GetUserDetail()
        {
            DataTable dtManageUser = new DataTable();

            //BLAuditLog objAuditLog = new BLAuditLog();
            try
            {
                objManageUser.FromDate = (txtFromDate.Text.Length > 0) ?
                    DateTime.Parse(txtFromDate.Text.Trim()) : BLCommon.DATETIME_NULL;

                objManageUser.ToDate = (txtToDate.Text.Length > 0) ?
                    DateTime.Parse(txtToDate.Text.Trim()) : BLCommon.DATETIME_NULL;

                objManageUser.UserName = txtUserName.Text.Trim();


                dtManageUser = objManageUser.GetUserDetail();
                if (dtManageUser.Rows.Count > 0)
                {
                    spnMsg.InnerText = string.Empty;
                    gvManageUser.PageSize = PortalCommon.GetGridPageSize;
                    gvManageUser.DataSource = dtManageUser;
                    gvManageUser.DataBind();

                    for (int i = 0; i < gvManageUser.Rows.Count; i++)
                    {
                        if (((LinkButton)gvManageUser.Rows[i].Cells[5].FindControl("lnkAction")).Text.Equals(Constants.IsActive))//showing Activate?
                        {
                            ((LinkButton)gvManageUser.Rows[i].Cells[6].FindControl("lnkManageApplication")).Enabled = false;
                            ((LinkButton)gvManageUser.Rows[i].Cells[7].FindControl("lnkManageRole")).Enabled = false;
                            ((LinkButton)gvManageUser.Rows[i].Cells[8].FindControl("lnkResetPassword")).Visible = false;
                            //((LinkButton)gvManageUser.Rows[i].Cells[9].FindControl("lnkEdit")).Enabled = false;
                        }

                        if (gvManageUser.Rows[i].Cells[1].Text.Equals(AppSessions.SessionLoginId))
                        {
                            ((LinkButton)gvManageUser.Rows[i].Cells[5].FindControl("lnkAction")).Enabled = false;
                        }

                    }
                }
                else
                {
                    //MSG1001 - No Records(s) Found.
                    spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "MSG1001");
                    spnMsg.Attributes.Add("class", Constants.MessageCSS);
                    gvManageUser.DataSource = null;
                    gvManageUser.DataBind();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
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

        #endregion GetUserDetail    

        protected void gvManageUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvManageUser.PageIndex = e.NewPageIndex;
                GetUserDetail();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void gvManageUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

                if ((e.CommandName.Equals("APP")) || (e.CommandName.Equals("ROL")) || (e.CommandName.Equals("ACT"))
                    || (e.CommandName.Equals("RSP")) || (e.CommandName.Equals("REPO")) || (e.CommandName.Equals("EDT")))
                {


                    string tempName = Convert.ToString(e.CommandArgument);
                    string[] tempNameArr = tempName.Split(new char[] { '|' });
                    string userName = tempNameArr[0];
                    string userFullName = tempNameArr[1];
                    string active_YN = tempNameArr[2];

                    if (e.CommandName.Equals("APP")) //Coming from 'Manage Application'
                    {
                        string temp = userName;


                        //dvUserDetail.Visible = false;
                        dvManageApplication.Visible = true;
                        dvManageRole.Visible = false;
                        divManageUserDetails.Visible = false;

                        objManageUser.UserName = temp;
                        objManageUser.UserFullName = userFullName;

                        dtManageUser = objManageUser.GetSelectedAppForManageUser();
                        if (dtManageUser.Rows.Count > 0)
                        {
                            spnMsg.InnerText = string.Empty;
                            spnMessageApplication.InnerText = string.Empty;
                            spnMessageApplicationError.InnerText = string.Empty;
                            spnFullNameApplication.InnerText = objManageUser.UserFullName + '-';
                            spnUsername.InnerText = objManageUser.UserName;
                            gvShowSelectedApp.DataSource = dtManageUser;
                            gvShowSelectedApp.DataBind();

                            FindChkBox_gvShowSelectedApp();
                            //gvShowSelectedApp.Columns[3].Visible = false;
                        }
                        else
                        {
                            //MSG1001 - No Records(s) Found.
                            spnMsg.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                            gvShowSelectedApp.DataSource = null;
                            gvShowSelectedApp.DataBind();
                        }
                    }

                   else if (e.CommandName.Equals("REPO")) //Coming from 'Manage Application'
                    {
                        string temp = userName;


                        //dvUserDetail.Visible = false;
                        dvManageReport.Visible = true;
                        dvManageApplication.Visible = false;
                        dvManageRole.Visible = false;
                        divManageUserDetails.Visible = false;

                        objManageUser.UserName = temp;
                        objManageUser.UserFullName = userFullName;

                        dtManageUser = objManageUser.GetSelectedReportForManageUser();
                        if (dtManageUser.Rows.Count > 0)
                        {
                            spnMsg.InnerText = string.Empty;
                            spnMessageReport.InnerText = string.Empty;
                            spnMessageReportsError.InnerText = string.Empty;
                            spnReportName.InnerText = objManageUser.UserFullName + '-';
                            spnUsernames.InnerText = objManageUser.UserName;
                            gvShowSelectedReport.DataSource = dtManageUser;
                            gvShowSelectedReport.DataBind();

                            FindChkBox_gvShowSelectedReport();
                            //gvShowSelectedApp.Columns[3].Visible = false;
                        }
                        else
                        {
                            //MSG1001 - No Records(s) Found.
                            spnMsg.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                            gvShowSelectedReport.DataSource = null;
                            gvShowSelectedReport.DataBind();
                        }
                    }
                    else if (e.CommandName.Equals("ROL")) //Coming from 'Manage Role'
                    {
                        string tempUserName = userName;
                        //Code for Role management of this user.

                        //dvUserDetail.Visible = false;
                        dvManageApplication.Visible = false;
                        dvManageRole.Visible = true;
                        divManageUserDetails.Visible = false;

                        spnRoleMessage.InnerText = string.Empty;

                        gvShowSelectedRole.DataSource = null;
                        gvShowSelectedRole.DataBind();

                        spnFullNameforRole.InnerText = userFullName + '-';
                        spnUsernameForRole.InnerText = userName;
                        objManageUser.UserName = tempUserName;
                        objManageUser.Mode = Constants.Update_Mode;
                        objManageUser.GetSelectedAppList(ddlApplication);

                        spnMessageRole.InnerText = string.Empty;
                        //GetSelectedAppList();                
                    }
                    else if (e.CommandName.Equals("ACT")) // Activate/ Inactive.
                    {
                        dvManageApplication.Visible = false;
                        dvManageRole.Visible = false;
                        divManageUserDetails.Visible = false;

                        objManageUser.UserName = userName;

                        if (active_YN.Equals(Constants.IsActive)) //"Activate?"
                        {
                            objManageUser.Active_YN = "Y";
                        }
                        else
                        {
                            objManageUser.Active_YN = "N";
                        }

                        UpdateUserActiveOrInactive();
                        GetUserDetail();
                    }
                    else if (e.CommandName.Equals("RSP")) // Reset Password.
                    {
                        ResetPassword(userFullName, userName);

                    }

                    else if (e.CommandName.Equals("EDT")) // Reset Password.
                    {
                        dvManageApplication.Visible = false;
                        dvManageRole.Visible = false;
                        divManageUserDetails.Visible = true;

                        spnUserFullforDetails.InnerText = userFullName + " (";
                        spnUsernameforDetails.InnerText = userName + ")";

                        SpnUserDetailMessage.InnerHtml = string.Empty;
                        spnUsername.InnerText = userName;
                        GetUserDetailInEditMode();
                    }

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region ReadMailTemplateAndSendMail

        private short ReadMailTemplateAndSendMail(string loginId, string password, string userFullName, string emailAddress)
        {
            StringBuilder sbMailBody = new StringBuilder();
            BLManageMail objManageMail = new BLManageMail();
            string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            //string amfURL = string.Empty;
            //if (DomainName.Contains("afmsuite.com"))
            //{
            //    amfURL = ConfigurationManager.AppSettings["PRODUCTION"].ToString();
            //}
            //else
            //{
            //    amfURL = ConfigurationManager.AppSettings["DEVELOPMENT"].ToString();
            //}
            short retValue = 0;
            try
            {
                sbMailBody = PortalCommon.ReadFile("../" + Constants.Path_TempltForgotPswd);
                sbMailBody
                        .Replace("@UserFullName", userFullName)
                        .Replace("@LoginId", loginId)
                        //.Replace("@AFMURL", amfURL)
                        .Replace("@Password", password);


                PortalCommon.SendMailViaSMTP(emailAddress, "Reset Password", sbMailBody.ToString());

                //objManageMail.AlertCode = Common.GetMessageFromXMLFile("ALRT1003"); //Reset Password Mail from AMS.
                //objManageMail.EmailBody = sbMailBody.ToString(); //Email Body Content.
                //objManageMail.EmailFlagValue = emailAddress; //User Email Address.

                //retValue = objManageMail.SaveMailToDB();
            }
            catch (Exception ex)
            {
                retValue = 0;
                throw ex;
            }
            finally
            {
                sbMailBody = null;
                objManageMail = null;
            }
            return retValue;
        }

        #endregion ReadMailTemplateAndSendMail

        #region UpdateUserActiveOrInactive

        private void UpdateUserActiveOrInactive()
        {
            try
            {
                //objManageUser.UserName = spnUserNameApplication.InnerText;
                //objManageUser.Active_YN=

                short retVal = objManageUser.UpdateUserActiveOrInactive();
               
                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Update_User_Act_Inact,
                        Constants.Activity_Update_User_Act_Inact, Constants.Activity_Success);

                    //INF1019 - Action completed successfully.
                    spnchange.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1019");
                }

               else if (retVal == 2)
                {

                    spnerror.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1104");
                }

                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Update_User_Act_Inact,
                        Constants.Activity_Update_User_Act_Inact, Constants.Activity_Failed);


                    //ERR1024 - Action could not completed.
                    spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1024");
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion UpdateUserActiveOrInactive

        protected void gvShowSelectedApp_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    if (e.Row.Cells[3].Text.Equals("Y"))
                //    {
                //        ((CheckBox)gvShowSelectedApp.Rows[e.Row.RowIndex].Cells[0].FindControl("chkSelectedApp")).Checked = true;
                //    }
                //}
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region FindChkBox_gvShowSelectedApp

        private void FindChkBox_gvShowSelectedApp()
        {
            try
            {
                for (int i = 0; i < gvShowSelectedApp.Rows.Count; i++)
                {
                    //if (gvShowSelectedApp.Rows[i].Cells[3].Text.Equals("Y"))
                    if (gvShowSelectedApp.DataKeys[i].Value.Equals("Y"))
                    {
                        ((CheckBox)gvShowSelectedApp.Rows[i].Cells[0].FindControl("chkSelectedApp")).Checked = true;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion FindChkBox_gvShowSelectedApp

        #region FindChkBox_gvShowSelectedreport

        private void FindChkBox_gvShowSelectedReport()
        {
            try
            {
                for (int i = 0; i < gvShowSelectedReport.Rows.Count; i++)
                {
                    //if (gvShowSelectedApp.Rows[i].Cells[3].Text.Equals("Y"))
                    if (gvShowSelectedReport.DataKeys[i].Value.Equals("Y"))
                    {
                        ((CheckBox)gvShowSelectedReport.Rows[i].Cells[0].FindControl("chkSelectedReport")).Checked = true;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion FindChkBox_gvShowSelectedreport

        #region UpdateSelectedApplications

        private void UpdateSelectedApplications()
        {
            try
            {
                objManageUser.UserName = spnUsername.InnerText;//gvManageUser.SelectedRow.Cells[1].FindControl("UserName").ToString();
                                                               //gvManageUser.Rows[i].Cells[1].FindControl("UserName").ToString();
                                                               //gvManageUser.SelectedRow.Cells[1].FindControl("UserName").ToString();
                string applicationCode = string.Empty;
                //string appCodeWithComma = string.Empty;

                for (int i = 0; i < gvShowSelectedApp.Rows.Count; i++)
                {
                    if (((CheckBox)gvShowSelectedApp.Rows[i].Cells[0].FindControl("chkSelectedApp")).Checked)
                    {
                        if (applicationCode.Length > 0)
                        {
                            applicationCode = applicationCode + ", " + gvShowSelectedApp.Rows[i].Cells[1].Text;
                        }
                        else
                        {
                            applicationCode = gvShowSelectedApp.Rows[i].Cells[1].Text;
                        }
                        //applicationCode = gvShowSelectedApp.Rows[i].Cells[1].Text;
                        //appCodeWithComma = appCodeWithComma + applicationCode + ",";   
                    }
                }
                //string AppCodewithoutComma = appCodeWithComma.TrimEnd(',');
                objManageUser.ApplicationCode = applicationCode;

                short retVal = objManageUser.UpdateSelectedApplications();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Update_Assigned_Applications,
                        Constants.Activity_Update_Assigned_Applications, Constants.Activity_Success);

                    //INF1009 - Application(s) updated Successfully.
                    spnMessageApplication.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1009");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Update_Assigned_Applications,
                        Constants.Activity_Update_Assigned_Applications, Constants.Activity_Failed);

                    //ERR1011 - Application(s) could not updated.
                    spnMessageApplicationError.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1011");
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion UpdateSelectedApplications

        #region GetSelectedAppList

        private void GetSelectedAppList()
        {
            try
            {

                objManageUser.UserName = (gvManageUser.SelectedRow.Cells[1].Text);//spnMessageRole.InnerText;
                objManageUser.Mode = Constants.Update_Mode;
                objManageUser.GetSelectedAppList(ddlApplication);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion GetSelectedAppList

        #region FindChkBox_gvShowSelectedRole

        private void FindChkBox_gvShowSelectedRole()
        {
            try
            {
                for (int i = 0; i < gvShowSelectedRole.Rows.Count; i++)
                {
                    //if (gvShowSelectedRole.Rows[i].Cells[3].Text.Equals("Y"))
                    if (gvShowSelectedRole.DataKeys[i].Value.Equals("Y"))
                    {
                        ((CheckBox)gvShowSelectedRole.Rows[i].Cells[0].FindControl("chkSelectedRole")).Checked = true;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion FindChkBox_gvShowSelectedRole

        #region UpdateSelectedRoles

        private void UpdateSelectedRoles()
        {
            try
            {
                objManageUser.UserName = spnUsernameForRole.InnerText;
                objManageUser.ApplicationCode = ddlApplication.SelectedValue;

                string roleCode = string.Empty;
                //string roleCodeWithComma = string.Empty;

                for (int i = 0; i < gvShowSelectedRole.Rows.Count; i++)
                {
                    var chk = (CheckBox)gvShowSelectedRole.Rows[i].FindControl("chkSelectedRole");
                    if (chk != null && chk.Checked)
                    {
                        roleCode = gvShowSelectedRole.Rows[i].Cells[1].Text;
                        break; // stop after first match
                    }
                }
                //string roleCodewithoutComma = roleCodeWithComma.TrimEnd(',');
                objManageUser.RoleCode = roleCode;

                short retVal = objManageUser.UpdateSelectedRoles();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Update_Assigned_Roles,
                        Constants.Activity_Update_Assigned_Roles, Constants.Activity_Success);

                    //INF1011 - Role updated Successfully.
                    spnMessageRole.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1011");
                    //GetSelectedAppList();
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Update_Assigned_Roles,
                       Constants.Activity_Update_Assigned_Roles, Constants.Activity_Failed);

                    //ERR1012 - Role(s) could not be updated.
                    spnMessageRole.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1012");
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objManageUser = null;
            }
        }

        #endregion UpdateSelectedRoles

        protected void btnUpdateApplication_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSelectedApplications();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnUpdateRole_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSelectedRoles();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRoleForManageUser();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region GetSelectedRoleForManageUser

        private void GetSelectedRoleForManageUser()
        {
            try
            {
                objManageUser.UserName = spnUsernameForRole.InnerText;
                objManageUser.ApplicationCode = ddlApplication.SelectedValue;
                dtManageUser = objManageUser.GetSelectedRoleForManageUser();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMsg.InnerText = string.Empty;
                    spnRoleMessage.InnerText = string.Empty;
                    spnMessageRole.InnerHtml = string.Empty;
                    //spnUserNameforRole.InnerText = objManageUser.UserName;
                    gvShowSelectedRole.DataSource = dtManageUser;
                    gvShowSelectedRole.DataBind();
                    btnUpdateRole.Enabled = true;

                    FindChkBox_gvShowSelectedRole();

                    //gvShowSelectedRole.Columns[3].Visible = false;
                }
                else
                {

                    //MSG1001 - No Role(s) defined.
                    spnRoleMessage.InnerText = Common.GetMessageFromXMLFile("MSG1009");
                    spnRoleMessage.Attributes.Add("class", Constants.MessageCSS);

                    btnUpdateRole.Enabled = false;

                    gvShowSelectedRole.DataSource = null;
                    gvShowSelectedRole.DataBind();
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion GetSelectedRoleForManageUser

        protected void btnCancelApplication_Click(object sender, EventArgs e)
        {
            try
            {
                dvManageApplication.Visible = false;
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnCancelRole_Click(object sender, EventArgs e)
        {
            try
            {
                dvManageRole.Visible = false;
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                spnResetPassword.Visible = false;
                GetUserDetail();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region ClearAll

        private void ClearAll()
        {
            try
            {
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                txtUserName.Text = string.Empty;
                dvManageApplication.Visible = false;
                dvManageRole.Visible = false;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion ClearAll

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetUserDetail();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region UserActiveOrInactive

        private void UserActiveOrInactive()
        {
            try
            {

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion UserActiveOrInactive

        #region ResetPassword

        private void ResetPassword(string userFullName, string userName)
        {
            try
            {
                Hashtable htResetPswdDetail = new Hashtable();

                objManageUser.UserName = userName;
                string newPlainPassword = "OQ" + Convert.ToString(PortalCommon.GetRandomValue());

                //objManageUser.NewPassword, will store encrypted password/hash password.
                //objManageUser.NewPassword = CryptoUtil.GetHashEncryptedValue(newPlainPassword, Constants.EncryptionAlgo_SHA1);
                //New Encryption Technique
                objManageUser.NewPassword = PortalCommon.GetRandomEncryptedPassword(out newPlainPassword);

                htResetPswdDetail = objManageUser.ResetPassword();

                if (htResetPswdDetail.Count > 0)
                {
                    if (Convert.ToInt16(htResetPswdDetail["RetValue"]) == 1) //Success
                    {
                        string EMailAdd = "";
                        EMailAdd = htResetPswdDetail["EmailAddress"].ToString();
                        //INF1026 - Password Reset successfully.
                        spnResetPassword.Visible = true;
                        //lblUser.Text = userName;
                        lblnewpass.Text = newPlainPassword;
                        string message = lblChnagePassmess.Text.Replace("'", "\\'");
                        string newPass = lblnewpass.Text.Replace("'", "\\'");

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "Popup", $"ShowChangepass('{message}','{newPass}');", true);

                        //spnResetPassword.Visible = true;
                        //spnResetPassword.InnerText = "Password has Reset. New Password for (" + objManageUser.UserName + ") is " + "'" + newPlainPassword + "'";
                        //spnResetPassword.Attributes.Add("class", "BigText_Green");

                        // ReadMailTemplateAndSendMail(userName, newPlainPassword, userFullName, EMailAdd);


                        //}
                    }
                    else
                    {
                        PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Reset_Password,
                        Constants.Activity_Reset_Password, Constants.Activity_Failed);


                        //ERR1033 - Password can't be reset. Please try later.
                        spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1033");
                    }
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Reset_Password,
                       Constants.Activity_Reset_Password, Constants.Activity_Failed);

                    //ERR1033 - Password can't be reset. Please try later.
                    spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1033");
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion ResetPassword



        #region UpdateUserDetails

        private void UpdateUserDetails()
        {
            try
            {
                objManageUser.UserName = spnUsername.InnerText;
                objManageUser.NickName = txtNickName.Text.Trim();
                objManageUser.holdingamt =Convert.ToDecimal(txtholdingAmt.Text.Trim());
          
                objManageUser.holdingRemarks = txtholdimngRemarks.Text.Trim();
                objManageUser.AllowedDaysToViewReport = txtAllowedDaysToViewReport.Text.Trim();


                objManageUser.AuditLog.ProgramCode = Constants.PrgCode_Manage_User;
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;

                //For Audit Log.
                objManageUser.AuditLog.ApplicationCode = Constants.ApplicationCode;
                objManageUser.AuditLog.LocalHostIP = AppSessions.SessionLocalHostIP;
                objManageUser.AuditLog.LocalHostName = AppSessions.SessionLocalHostName;
                objManageUser.AuditLog.LogCategory = string.Empty;
                objManageUser.AuditLog.RemoteHostIP = AppSessions.SessionRemoteHostIP;
                objManageUser.AuditLog.RemoteHostName = AppSessions.SessionRemoteHostName;
                objManageUser.AuditLog.TaskDescription = string.Empty;
                objManageUser.AuditLog.LogAction = Constants.Activity_LogAction_Update;
                objManageUser.AuditLog.TaskModule = Constants.Activity_Update_Manage_User_Details;
                objManageUser.AuditLog.UserSessionId = AppSessions.SessionUserSessionId;


                short retVal = objManageUser.UpdateUserDetails();

                if (retVal == 1)
                {
                    //INF1012 - User details updated Successfully.
                    SpnUserDetailMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1012");

                    GetUserDetail();
                }
                else if (retVal == 2)
                {
                    //ERR1002 - NickName already exists.
                    SpnUserDetailMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1002");
                }
                else if (retVal == 3)
                {
                    //VAL1073 - Allowed days can not exceed Maximum allowed days limit.
                    SpnUserDetailMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "VAL1073", objManageUser.MaxAllowedDaysToViewReport);
                }
                else
                {
                    //ERR1056 - User details could not updated.
                    SpnUserDetailMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1056");
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion UpdateUserDetails

        #region btnUpdateUserDetails_Click   

        protected void btnUpdateUserDetails_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateUserDetails();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #endregion btnUpdateUserDetails_Click

        #region btnCancelUserDetails_Click

        protected void btnCancelUserDetails_Click(object sender, EventArgs e)
        {
            try
            {
                divManageUserDetails.Visible = false;
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #endregion btnCancelUserDetails_Click

        #region GetUserDetailInEditMode

        private void GetUserDetailInEditMode()
        {
            Hashtable htManageUser = new Hashtable();
            try
            {
                objManageUser.UserName = spnUsername.InnerText;

                htManageUser = objManageUser.GetUserDetailInEditMode();

                if (htManageUser.Count > 0)
                {
                    txtNickName.Text = Convert.ToString(htManageUser["NickName"]);
                    //txtholdingAmt.Text = Convert.ToString(htManageUser["holdingamt"]);
                    //txtholdimngRemarks.Text = Convert.ToString(htManageUser["HoldingRemarks"]);
                   // txtAllowedDaysToViewReport.Text = Convert.ToString(htManageUser["AllowedDaysToViewReport"]);
                   
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
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

        #endregion GetUserDetailInEditMode

        #region UpdateSelectedReports

        private void UpdateSelectedReports()
        {
            try
            {
                objManageUser.UserName = spnUsernames.InnerText;//gvManageUser.SelectedRow.Cells[1].FindControl("UserName").ToString();
                                                               //gvManageUser.Rows[i].Cells[1].FindControl("UserName").ToString();
                                                               //gvManageUser.SelectedRow.Cells[1].FindControl("UserName").ToString();
                string applicationCode = string.Empty;
                //string appCodeWithComma = string.Empty;

                for (int i = 0; i < gvShowSelectedReport.Rows.Count; i++)
                {
                    if (((CheckBox)gvShowSelectedReport.Rows[i].Cells[0].FindControl("chkSelectedReport")).Checked)
                    {
                        if (applicationCode.Length > 0)
                        {
                            applicationCode = applicationCode + ", " + gvShowSelectedReport.Rows[i].Cells[1].Text;
                        }
                        else
                        {
                            applicationCode = gvShowSelectedReport.Rows[i].Cells[1].Text;
                        }
                        //applicationCode = gvShowSelectedApp.Rows[i].Cells[1].Text;
                        //appCodeWithComma = appCodeWithComma + applicationCode + ",";   
                    }
                }
                //string AppCodewithoutComma = appCodeWithComma.TrimEnd(',');
                objManageUser.UserReport = applicationCode;

                short retVal = objManageUser.UpdateSelectedReport();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Update_Assigned_Applications,
                        Constants.Activity_Update_Assigned_Applications, Constants.Activity_Success);

                    //INF1009 - Application(s) updated Successfully.
                    spnMessageReport.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1009");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Update_Assigned_Applications,
                        Constants.Activity_Update_Assigned_Applications, Constants.Activity_Failed);

                    //ERR1011 - Application(s) could not updated.
                    spnMessageReportsError.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1011");
                }

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion UpdateSelectedReports

        protected void btnUpdateReports_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSelectedReports();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
    }
}
