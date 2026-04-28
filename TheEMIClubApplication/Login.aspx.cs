using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.Model;

namespace TheEMIClubApplication
{
    public partial class Login : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {

        //        try
        //        {
        //            //Response.Write(CryptoUtil.GetHashEncryptedValue("avaal1812", Constants.EncryptionAlgo_SHA1));
        //            PortalCommon.CheckSiteUnderMaintenance(PortalCommon.PageLevel.Root);
        //            AppSessions.SessionUserSessionId = Session.SessionID; //For Activity/Audit Log.


        //            if (Equals(Request.QueryString["edom"], "tougol"))
        //            {

        //                spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "INF1001");

        //            }
        //            if (!Page.IsPostBack)
        //            {
        //                if (Request.Cookies["UserCode"] != null)
        //                {
        //                    txtUserName.Text = Request.Cookies["UserCode"].Value;

        //                    ClientScript.RegisterStartupScript(GetType(), "hwa", "txtpwdValue('" + PortalCommon.DecryptTripleDES(Request.Cookies["UserPwd"].Value) + "','logout');", true);
        //                    Response.Write(txtPassword.Text);
        //                    chkRememberMe.Checked = true;
        //                }
        //            }

        //        }
        //        catch (Exception Ex)
        //        {
        //            Common.WriteExceptionLog(Ex);
        //            spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
        //        }

        //    }
        //}

        protected void Page_Load(object sender, EventArgs e) 
             {
            if (!IsPostBack)
            {
                try
                {
                    PortalCommon.CheckSiteUnderMaintenance(PortalCommon.PageLevel.Root);
                    AppSessions.SessionUserSessionId = Session.SessionID; // Activity/Audit Log

                    // **Force Clear Session and Cookies on Login Page Load**
                    if (Session["UserID"] != null || Request.UrlReferrer != null)
                    {
                        AppSessions.RemoveAllSessions();
                        Session.Clear();
                        Session.Abandon();

                        // **Expire Cookies**
                        if (Request.Cookies["UserCode"] != null)
                        {
                            HttpCookie userCodeCookie = new HttpCookie("UserCode");
                            userCodeCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(userCodeCookie);
                        }

                        if (Request.Cookies["UserPwd"] != null)
                        {
                            HttpCookie userPwdCookie = new HttpCookie("UserPwd");
                            userPwdCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(userPwdCookie);
                        }

                        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1); // Expire session cookie
                    }

                    if (Equals(Request.QueryString["edom"], "tougol"))
                    {
                        spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "INF1001");
                        AppSessions.RemoveAllSessions();
                    }

                    if (Request.Cookies["UserCode"] != null)
                    {
                        txtUserName.Text = Request.Cookies["UserCode"].Value;

                        // **Only Autofill Password if "Remember Me" was Checked**
                        if (Request.Cookies["UserPwd"] != null)
                        {
                            string decryptedPassword = PortalCommon.DecryptTripleDES(Request.Cookies["UserPwd"].Value);
                            ClientScript.RegisterStartupScript(GetType(), "hwa",
                                "txtpwdValue('" + decryptedPassword + "','logout');", true);
                        }

                        chkRememberMe.Checked = true;
                    }
                }
                catch (Exception Ex)
                {
                    Common.WriteExceptionLog(Ex);
                    spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
                }
            }

            // **Prevent Browser Caching**
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            //if (!IsPostBack)
            //{


            //    try
            //    {

            //        PortalCommon.CheckSiteUnderMaintenance(PortalCommon.PageLevel.Root);
            //        AppSessions.SessionUserSessionId = Session.SessionID; //For Activity/Audit Log.

            //        //byte[] b = Convert.FromBase64String("54E8C804F6174CC04A36600D52AE09EAA82D6744");
            //        //string decryptedPassword = System.Text.ASCIIEncoding.ASCII.GetString(b);
            //        //Response.Write(decryptedPassword);

            //        if (Equals(Request.QueryString["edom"], "tougol"))
            //        {
            //            //INF1001 - You have successfully Logged Out from the Application
            //            spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "INF1002");
            //            AppSessions.RemoveAllSessions();
            //            // spnLoginMsg.Visible = true;
            //        }
            //        if (!Page.IsPostBack)
            //        {

            //            if (Request.Cookies["UserCode"] != null)
            //            {
            //                txtUserName.Text = Request.Cookies["UserCode"].Value;
            //                //txtPassword.Text = Request.Cookies["UserPwd"].Value;
            //                //txtPassword.Attributes.Add("value", Request.Cookies["UserPwd"].Value);

            //                ClientScript.RegisterStartupScript(GetType(), "hwa", "txtpwdValue('" + PortalCommon.DecryptTripleDES(Request.Cookies["UserPwd"].Value) + "','logout');", true);
            //                Response.Write(txtPassword.Text);
            //                chkRememberMe.Checked = true;
            //            }
            //        }

            //    }
            //    catch (Exception Ex)
            //    {
            //        Common.WriteExceptionLog(Ex);
            //        spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
            //    }

            //}
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
            if (cptCaptcha.UserValidated)
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                lblErrorMessage.Text = "Valid text";
            }
            else
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text = "InValid Text";
            }
        }
        private void UserLogin()
        {
            #region Login Code

            BLManageUser objManageUser = new BLManageUser();
            Hashtable htUserLoginDetail = new Hashtable();
            try
            {
                if (chkRememberMe.Checked)
                {
                    Response.Cookies["UserCode"].Value = txtUserName.Text.ToUpper().Trim();
                    Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(-1);
                }
                objManageUser.AuditLog.LoginId = txtUserName.Text.ToLower().Trim();                         
                objManageUser.Password = PortalCommon.EncryptTripleDES(txtPassword.Text.Trim());
                //objManageUser.Password = PortalCommon.DecryptTripleDES(txtPassword.Text.Trim());
                htUserLoginDetail = objManageUser.UserLogin(); //User authentication
                if (htUserLoginDetail.Count == 0)
                {
                    //Insert Activity Log here --Start
                    PortalCommon.SaveAuditLog_PreLogin(Constants.PrgCode_Login, "N", "L", Constants.Activity_Login,
                        Constants.Activity_Login, Constants.Activity_Failed, objManageUser.AuditLog.LoginId);
                    //Insert Activity Log here --End

                    //ERR1001 - Invalid Username/Password.                
                    spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1001");
                    return;
                }
                else //if (htUserLoginDetail.Count > 0) //Login Success
                {
                    if (chkRememberMe.Checked)
                    {
                        Response.Cookies["UserPwd"].Value = PortalCommon.EncryptTripleDES(txtPassword.Text.ToString());
                        Response.Cookies["UserPwd"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserPwd"].Expires = DateTime.Now.AddDays(-1);
                    }
                    //Set AppSessions variable       
                    AppSessions.SessionLoginId = objManageUser.AuditLog.LoginId.ToUpper();
                    AppSessions.SessionUserDataScope = Convert.ToString(htUserLoginDetail["UserDataScope"]);
                    AppSessions.SessionUserFullName = Convert.ToString(htUserLoginDetail["UserFullName"]);
                    AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["EmailAddress"]);
                    AppSessions.companycode = Convert.ToString(htUserLoginDetail["ClientCode"]);
                    //AppSessions. = Convert.ToString(htUserLoginDetail["Address"]);
                    //AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["AadharNumber"]);
                    //AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["PANNumber"]);

                    AppSessions.SessionRedirectToChangePswd = Convert.ToString(htUserLoginDetail["RedirectToChangePswd_YN"]);
                    //Added SessionUserRoleCode for the purpose of having user role powers - 26-Oct-2016
                    AppSessions.SessionUserRoleCode = Convert.ToString(htUserLoginDetail["RoleName"]);

                    //Retrieve Client's machine details for activity log.
                    AppSessions.SessionRemoteHostIP = PortalCommon.GetRemoteHostIP();
                    AppSessions.SessionRemoteHostName = PortalCommon.GetRemoteHostName();
                    AppSessions.SessionLocalHostIP = PortalCommon.GetLocalHostIP();
                    AppSessions.SessionLocalHostName = PortalCommon.GetLocalHostName();

                    //Get User's Menu
                    GetMenuData();

                    Response.Redirect(Constants.Path_HomePage, false);
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx);
                spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
            }
            finally
            {
                htUserLoginDetail = null;
                objManageUser = null;
            }

            #endregion Login Code
        }

        private void EmpLogin()
        {

            BLManageUser objManageUser = new BLManageUser();
            Hashtable htUserLoginDetail = new Hashtable();
            try
            {
                if (chkRememberMe.Checked)
                {
                    Response.Cookies["UserCode"].Value = txtUserName.Text.ToUpper().Trim();
                    Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserCode"].Expires = DateTime.Now.AddDays(-1);
                }

                objManageUser.AuditLog.LoginId = txtUserName.Text.ToLower().Trim();
                objManageUser.Password = PortalCommon.EncryptTripleDES(txtPassword.Text.Trim());
                //string epassword = PortalCommon.DecryptTripleDES(txtPassword.Text.Trim());
                htUserLoginDetail = objManageUser.EMPLogin(); //User authentication
                if (htUserLoginDetail.Count == 0)
                {
                    //Insert Activity Log here --Start
                    PortalCommon.SaveAuditLog_PreLogin(Constants.PrgCode_Login, "N", "L", Constants.Activity_Login,
                        Constants.Activity_Login, Constants.Activity_Failed, objManageUser.AuditLog.LoginId);
                    //Insert Activity Log here --End

                    //ERR1001 - Invalid Username/Password.                
                    spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1001");
                    return;
                }
                else //if (htUserLoginDetail.Count > 0) //Login Success
                {
                    if (chkRememberMe.Checked)
                    {
                        Response.Cookies["UserPwd"].Value = PortalCommon.EncryptTripleDES(txtPassword.Text.ToString());
                        Response.Cookies["UserPwd"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserPwd"].Expires = DateTime.Now.AddDays(-1);
                    }
                    //Set AppSessions variable       
                    AppSessions.SessionLoginId = objManageUser.AuditLog.LoginId.ToUpper();
                    AppSessions.SessionUserDataScope = Convert.ToString(htUserLoginDetail["UserDataScope"]);
                    AppSessions.SessionUserFullName = Convert.ToString(htUserLoginDetail["UserFullName"]);
                    AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["EmailAddress"]);
                    //AppSessions. = Convert.ToString(htUserLoginDetail["Address"]);
                    //AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["AadharNumber"]);
                    //AppSessions.SessionUserEmailAddress = Convert.ToString(htUserLoginDetail["PANNumber"]);

                    AppSessions.SessionRedirectToChangePswd = Convert.ToString(htUserLoginDetail["RedirectToChangePswd_YN"]);
                    //Added SessionUserRoleCode for the purpose of having user role powers - 26-Oct-2016
                    AppSessions.SessionUserRoleCode = Convert.ToString(htUserLoginDetail["RoleName"]);

                    //Retrieve Client's machine details for activity log.
                    AppSessions.SessionRemoteHostIP = PortalCommon.GetRemoteHostIP();
                    AppSessions.SessionRemoteHostName = PortalCommon.GetRemoteHostName();
                    AppSessions.SessionLocalHostIP = PortalCommon.GetLocalHostIP();
                    AppSessions.SessionLocalHostName = PortalCommon.GetLocalHostName();

                    //Get User's Menu
                    GetMenuData();

                    Response.Redirect(Constants.Path_HomePage, false);
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx);
                spnLoginMsg.InnerHtml = PortalCommon.GetMessageWithImage_LoginPage(PortalCommon.PageLevel.Root, "ERR1007");
            }
            finally
            {
                htUserLoginDetail = null;
                objManageUser = null;
            }
        }

        #region GetMenuData

        /// <summary>
        /// Method to get user's menu data and store it in session.
        /// Further this data will be used to check user's accessibility of particular page.
        /// So we are storing and holding user's menu data in session.
        /// </summary>
        private void GetMenuData()
        {
            APPMenuBase objAppMenuBase = new APPMenuBase(); //Object of Business Class
            try
            {
                objAppMenuBase.LoginId = AppSessions.SessionLoginId;
                objAppMenuBase.ApplicationCode = Constants.ApplicationCode;
                AppSessions.SessionAppMenuData = objAppMenuBase.GetMenu();
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
            finally
            {
                objAppMenuBase = null;
            }
        }

        #endregion GetMenuData
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = "";
            lblErrorMessage.Text = "";
            try
            {
                if (txtCaptcha.Text.Trim() == string.Empty)
                {
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "InValid Captcha Code";
                }
                else
                {

                    cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
                    if (cptCaptcha.UserValidated)
                    {
                        Username = txtUserName.Text.Trim();
                        if (!Username.ToUpper().Trim().Contains("ADMIN".ToUpper().Trim()))
                        {
                            //string prefix = new string(Username.TakeWhile(char.IsLetter).ToArray());
                            //string numberPart = new string(Username.SkipWhile(char.IsLetter).ToArray());
                            //int number = int.Parse(numberPart);
                            EmpLogin();
                        }
                        else
                        {
                            UserLogin();
                        }
                    }
                    else
                    {
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        lblErrorMessage.Text = "InValid Captcha Code";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //#region Encrypt value of password client side
        //[WebMethod]
        //public static string GetEncryptedPass(string pwd)
        //{
        //    string newpass = "";
        //    newpass = PortalCommon.EncryptTripleDES(pwd);
        //    return newpass;
        //}
        //#endregion Encrypt value of password client side

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}