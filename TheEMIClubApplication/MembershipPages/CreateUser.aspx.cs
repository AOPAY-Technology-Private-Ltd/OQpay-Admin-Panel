using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class CreateUser : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ApplicationLinkSection.Visible = false;
                    RoleAssignmentSection.Visible = false;
                    //GetEmployeeListForCreateUser();
                    //PortalCommon.BindDropDownList(ddlEmployee, Constants.MstFlag_Employee, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016

                    GetApplicationNameList();

                    valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013"));//Validation Error(s)...//Please select Employee.
                    valReqEmployee.ErrorMessage = Common.GetMessageFromXMLFile("VAL1022");
                    //VAL1021 - Please enter Nick Name.
                    valReqNickName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1023");
                    valrexNickName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1089");
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #endregion PageLoad

        #region GetEmployeeListForCreateUser

        private void GetEmployeeListForCreateUser()
        {  
            try
            {

               // objManageUser.GetEmployeeListForCreateUser(ddlEmployee);


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




        #endregion GetEmployeeListForCreateUser

        #region GetApplicationNameList

        private void GetApplicationNameList()
        {
            try
            {
                objManageUser.GetApplicationNameList(cblApplication);
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

        #endregion GetApplicationNameList

        #region GetRoleList

        private void GetRoleList()
        {
            try
            {
                spnNoRolesDefined.InnerHtml = string.Empty;
                objManageUser.ApplicationCode = ddlApplication.SelectedValue;
                objManageUser.GetRoleList(cblRole);

                btnSaveRole.Enabled = true;

                if (cblRole.Items.Count == 0)
                {
                    //MSG1009 -> No Role Defined
                    spnNoRolesDefined.InnerHtml = Common.GetMessageFromXMLFile("MSG1009");
                    spnNoRolesDefined.Attributes.Add("class", Constants.MessageCSS);
                    btnSaveRole.Enabled = false;
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

        #endregion GetRoleList

        #region GetSelectedAppList

        private void GetSelectedAppList()
        {
            try
            {
                objManageUser.UserName = spnUsernametoGetValue.InnerText;//ddlEmployee.SelectedValue;
                objManageUser.Mode = Constants.Flag_CreateMode;//"INTSERT"
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

        #region CreateUser

        private void CreateUsers()
        {
            if (ddlEmployee.SelectedIndex == 0)
            {                
                spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1085");
            }
            else
            {
                Hashtable htEmail = new Hashtable();
                try
                {
                    objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                    objManageUser.NickName = txtNickName.Text.Trim();
                    objManageUser.UserName = ddlEmployee.SelectedValue;

                    objManageUser.PrgCode = Constants.PrgCode_Manage_User;

                    // string newPlainPassword = string.Empty; //Will be sent to user in Email.


                    string newPlainPassword = "avaal" + Convert.ToString(PortalCommon.GetRandomValue());

                    //objManageUser.NewPassword, will store encrypted password/hash password.
                    //objManageUser.Password = CryptoUtil.GetHashEncryptedValue(newPlainPassword, Constants.EncryptionAlgo_SHA1);
                    objManageUser.Password = PortalCommon.GetRandomEncryptedPassword(out newPlainPassword); //New Encryption Technique

                    //objManageUser.Password, will store encrypted password/hash password.
                    // objManageUser.Password = PortalCommon.GetRandomEncryptedPassword(out newPlainPassword);

                    //htEmail = objManageUser.GetEmailAddress();
                    //objManageUser.EmailAddress = Convert.ToString(htEmail["EmailAddress"]);

                    short retVal = objManageUser.CreateUser();

                    if (retVal == 1)
                    {
                        string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                        string amfURL = string.Empty;
                        //if (DomainName.Contains("afmsuite.com"))
                        //{
                        //    amfURL = ConfigurationManager.AppSettings["PRODUCTION"].ToString();
                        //}
                        //else
                        //{
                        //    amfURL = ConfigurationManager.AppSettings["DEVELOPMENT"].ToString();
                        //}

                        objManageUser.UserName = ddlEmployee.SelectedValue;

                        htEmail = objManageUser.GetEmailAddress();
                        if (htEmail.Count > 0)
                        {
                            objManageUser.EmailAddress = Convert.ToString(htEmail["EmailAddress"]);
                            objManageUser.UserFullName = Convert.ToString(htEmail["EmployeeName"]);

                            //short retValforReadMail = ReadMailTemplateAndSendMail(objManageUser.UserName, newPlainPassword,
                            //   objManageUser.UserFullName, objManageUser.EmailAddress, objManageUser.NickName);
                        }

                        //Sending Email
                        StringBuilder sbMailBody = new StringBuilder();
                        sbMailBody = PortalCommon.ReadFile("../" + Constants.Path_TempltAManagerLoginCredential); //Path_TempltAManagerLoginCredential -> HTMLTemplates/AManagerLoginCredential.html
                        sbMailBody

                                .Replace("@LoginId", ddlEmployee.SelectedValue.Trim())
                                .Replace("@Password", newPlainPassword)
                                .Replace("@AFMURL", amfURL)
                                .Replace("@UserFullName", Convert.ToString(htEmail["EmployeeName"]))
                                .Replace("@UserEmail", Convert.ToString(htEmail["EmailAddress"]));

                        PortalCommon.SendMailViaSMTP(Convert.ToString(htEmail["EmailAddress"]), "Welcome to AED Manager", sbMailBody.ToString());


                        PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Create_User,
                            Constants.Activity_Create_User, Constants.Activity_Success);

                        //objManageUser.UserName = ddlEmployee.SelectedValue;

                        //htEmail = objManageUser.GetEmailAddress();
                        //if (htEmail.Count > 0)
                        //{
                        //    objManageUser.EmailAddress = Convert.ToString(htEmail["EmailAddress"]);
                        //    objManageUser.UserFullName = Convert.ToString(htEmail["EmployeeName"]);

                        //    //short retValforReadMail = ReadMailTemplateAndSendMail(objManageUser.UserName, newPlainPassword,
                        //    //   objManageUser.UserFullName, objManageUser.EmailAddress, objManageUser.NickName);
                        //}

                        //INF1004 - User Created Successfully. Please assign Application.
                        spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1007");
                        CreateUserSection.Visible = false;
                        RoleAssignmentSection.Visible = false;
                        ApplicationLinkSection.Visible = true;
                    }
                    else if (retVal == 0)
                    {
                        PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Create_User,
                            Constants.Activity_Create_User, Constants.Activity_Failed);

                        CreateUserSection.Visible = true;
                        RoleAssignmentSection.Visible = false;
                        ApplicationLinkSection.Visible = false;
                        //ERR1002 - Nickname already exist.
                        spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1002");
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
        }

        #endregion CreateUser

        //#region ReadMailTemplate

        //private string ReadMailTemplate(string userFullName, string loginId, string password)
        //{
        //    StringBuilder sbMailBody = new StringBuilder();

        //    try
        //    {
        //        string templatePath = Server.MapPath("../HtmlTemplates/NewUserMail.htm");
        //        if (File.Exists(templatePath))
        //        {
        //            StreamReader strmReader = new StreamReader(templatePath);
        //            sbMailBody.Append(strmReader.ReadToEnd());
        //            strmReader.Close();

        //            sbMailBody
        //                .Replace("@UserFullName", userFullName)
        //                .Replace("@LoginId", loginId)
        //                .Replace("@Password", password);
        //        }
        //        else
        //        {
        //            //Get Message from XML. ERR1005 - HTML Mail Template not found.
        //            //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage("ERR1005", PortalCommon.PageLevel.Inner);
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return sbMailBody.ToString();
        //}

        //#endregion ReadMailTemplate 

        #region ReadMailTemplateAndSendMail

        private short ReadMailTemplateAndSendMail(string loginId, string password, string userFullName, string emailAddress, string nickName)
        {
            StringBuilder sbMailBody = new StringBuilder();
            BLManageMail objManageMail = new BLManageMail();
            short retValue = 0;
            try
            {
                sbMailBody = PortalCommon.ReadFile("../" + Constants.Path_TempltNewUserCreation); //"HtmlTemplates/NewUserMail.htm");
                sbMailBody
                        .Replace("@UserFullName", userFullName)
                        .Replace("@LoginId", loginId)
                        .Replace("@NickName", nickName)
                        .Replace("@Password", password);

                objManageMail.AlertCode = Common.GetMessageFromXMLFile("ALRT1002"); //"AL0009"; New User Creation Mail.ALRT1002
                objManageMail.EmailBody = sbMailBody.ToString(); //Email Body Content.
                objManageMail.EmailFlagValue = emailAddress; //User Email Address.

                // retValue = objManageMail.SaveMailToDB();
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

        #region SaveSelectedApplications

        private void SaveSelectedApplications()
        {
            try
            {
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.UserName = spnUsernametoGetValue.InnerText;//ddlEmployee.SelectedValue;

                string applicationCode = string.Empty;
                //string appCodeWithComma = string.Empty;

                for (int i = 0; i < cblApplication.Items.Count; i++)
                {
                    if (cblApplication.Items[i].Selected)
                    {
                        if (applicationCode.Length > 0)
                        {
                            applicationCode = applicationCode + ", " + cblApplication.Items[i].Value;
                        }
                        else
                        {
                            applicationCode = cblApplication.Items[i].Value;
                        }
                        //appCodeWithComma = appCodeWithComma + applicationCode + ",";

                        //tempVarCode = tempVarCode + applicationCode + ",";
                        //objManageUser.ApplicationCode = tempVarCode;
                    }
                }
                //string AppCodewithoutComma = appCodeWithComma.TrimEnd(',');
                objManageUser.ApplicationCode = applicationCode; //AppCodewithoutComma;

                short retVal = objManageUser.SaveSelectedApplications();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Assign_Applications,
                        Constants.Activity_Assign_Applications, Constants.Activity_Success);

                    //INF1008 - Application saved Successfully. Please assign Role(s).
                    spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1008");
                    GetSelectedAppList();
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Assign_Applications,
                        Constants.Activity_Assign_Applications, Constants.Activity_Failed);

                    //ERR1004 - Application(s) not saved.
                    spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1004");
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

        #endregion SaveSelectedApplications

        #region SaveSelectedRoles

        private void SaveSelectedRoles()
        {
            try
            {
                objManageUser.UserName = spnUsernametoGetValue.InnerText;//ddlEmployee.SelectedValue;

                string roleCode = string.Empty;
                //string roleCodeWithComma = string.Empty;

                for (int i = 0; i < cblRole.Items.Count; i++)
                {
                    if (cblRole.Items[i].Selected)
                    {
                        if (roleCode.Length > 0)
                        {
                            roleCode = roleCode + "," + cblRole.Items[i].Value;
                        }
                        else
                        {
                            roleCode = cblRole.Items[i].Value;
                        }
                        //roleCode = cblRole.Items[i].Value;
                        //roleCodeWithComma = roleCodeWithComma + roleCode + ",";
                    }
                }
                //string roleCodewithoutComma = roleCodeWithComma.TrimEnd(',');
                objManageUser.RoleCode = roleCode;

                short retVal = objManageUser.SaveSelectedRoles();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "Y", string.Empty, Constants.Activity_Assign_Roles,
                        Constants.Activity_Assign_Roles, Constants.Activity_Success);

                    //INF1006 - Role(s) saved Successfully.
                    spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1006");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Assign_Roles,
                        Constants.Activity_Assign_Roles, Constants.Activity_Failed);

                    //ERR1006 - Role(s) not saved.
                    spnMessageRoleAssignError.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1006");
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

        #endregion SaveSelectedRoles

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                spnMessageError.InnerText = string.Empty;
                CreateUsers();
                spnUser.InnerText = ddlEmployee.SelectedItem.ToString();
                spnUsername.InnerText = ddlEmployee.SelectedItem.ToString();
                spnUsernametoGetValue.InnerText = ddlEmployee.SelectedValue;

            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnCreateUser.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IsSelectedInCheckBox();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region IsSelectedInCheckBox

        private void IsSelectedInCheckBox()
        {
            try
            {
                string strchklist = string.Empty;

                foreach (ListItem li in cblApplication.Items)
                {
                    if (li.Selected)
                    {
                        strchklist += li.Text + " ";
                    }
                }

                if (strchklist == string.Empty)
                {
                    CreateUserSection.Visible = false;
                    ApplicationLinkSection.Visible = true;
                    RoleAssignmentSection.Visible = false;

                    //ERR1005 - No item selected.
                    spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1005");
                }

                else
                {
                    spnMessageAppSection.InnerHtml = string.Empty;

                    SaveSelectedApplications();
                    //INF1005 - Application(s) saved Successfully.
                    //spnMessageApplication.InnerText = (Common.GetMessageFromXMLFile("INF1005"));
                    CreateUserSection.Visible = false;
                    ApplicationLinkSection.Visible = false;
                    RoleAssignmentSection.Visible = true;
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
        #endregion IsSelectedInCheckBox

        #region IsSelectedInCheckBoxForRole

        private void IsSelectedInCheckBoxForRole()
        {
            try
            {
                string strchklist = string.Empty;

                foreach (ListItem li in cblRole.Items)
                {
                    if (li.Selected)
                    {
                        strchklist += li.Text + " ";
                    }
                }

                if (strchklist == string.Empty)
                {
                    CreateUserSection.Visible = false;
                    ApplicationLinkSection.Visible = false;
                    RoleAssignmentSection.Visible = true;

                    //ERR1005 - No item selected.
                    spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1005");
                }

                else
                {
                    spnMessageRoleAssign.InnerHtml = string.Empty;

                    SaveSelectedRoles();
                    cblRole.Visible = false;
                    GetSelectedAppList();
                    //INF1005 - Application(s) saved Successfully.
                    //spnMessageApplication.InnerText = (Common.GetMessageFromXMLFile("INF1005"));
                    CreateUserSection.Visible = false;
                    ApplicationLinkSection.Visible = false;
                    RoleAssignmentSection.Visible = true;
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
        #endregion IsSelectedInCheckBoxForRole

        protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                spnMessageRoleAssign.InnerHtml = string.Empty;
                GetRoleList();
                cblRole.Visible = true;
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessageAppSection.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void cblApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cblApplication.ClearSelection();
        }

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                IsSelectedInCheckBoxForRole();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = true;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Common.ClearFields(this, "0");
                spnMessageAppSection.InnerHtml = string.Empty;
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
            }
        }

        protected void btnClearRole_Click(object sender, EventArgs e)
        {
            try
            {
                //Common.ClearFields(this, "0"); 
                spnNoRolesDefined.InnerHtml = string.Empty;
                ddlApplication.SelectedIndex = 0;
                cblRole.Visible = false;
                spnMessageRoleAssign.InnerHtml = string.Empty;
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessageRoleAssign.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
    }
}