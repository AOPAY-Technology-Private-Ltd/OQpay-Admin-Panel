using System;
using System.Data.SqlClient;
using System.Web;
using AVFramework;
using System.Text;
using System.Configuration;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.AppCode;

namespace TheEMIClubApplication.CommonPages
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Form.DefaultFocus = txtOldPassword.UniqueID;
                this.Form.DefaultButton = btnChangePswd.UniqueID;

                //Menu should not appear in Change Password Page.
                //this.Master.FindControl("lnkChangePassword").Visible = false;
                this.Master.FindControl("AMDateLoginHeader1").FindControl("lnkChangePassword").Visible = false;

                if (!IsPostBack)
                {
                    GetValidationMessage();
                }
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        private void GetValidationMessage()
        {
            try
            {
                //VAL1013 - Validation Error(s)...
                valSummary.HeaderText = Common.GetMessageFromXMLFile("VAL1013");

                //VAL1014 - Please enter Old Password.
                valReqOldPassword.ErrorMessage = Common.GetMessageFromXMLFile("VAL1014");

                //VAL1015 - Please enter New Password.
                valReqNewPassword.ErrorMessage = Common.GetMessageFromXMLFile("VAL1015");

                //VAL1016 - Please re-enter New Password.
                valReqConfPassword.ErrorMessage = Common.GetMessageFromXMLFile("VAL1016");

                //VAL1017 - New Password and Confirm Password should be same.
                valComparePassword.ErrorMessage = Common.GetMessageFromXMLFile("VAL1017");

                //VAL1018 - Password should be consist of {0} to {1} characters.....
                valRegExpNewPassword.ErrorMessage = Common.GetMessageByParameters(Common.ApplicationType.WEB, "VAL1018", "6", "20");
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        protected void btnChangePswd_Click(object sender, EventArgs e)
        {
            BLManageUser objManageUser = new BLManageUser();
            try
            {
                string UserType = "";
                Session["Pwd"] = txtNewPassword.Text;
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.Password = PortalCommon.EncryptTripleDES(txtOldPassword.Text.Trim());
                objManageUser.NewPassword = PortalCommon.EncryptTripleDES(txtNewPassword.Text.Trim());
                if (objManageUser.AuditLog.LoginId.Contains("EMP"))
                {
                    UserType = "EMPLOYEE";
                }
                else if (objManageUser.AuditLog.LoginId.Contains("ADMIN"))
                {
                    UserType = "ADMIN";
                }
                else
                {
                    UserType = "CUSTOMER";
                }

                //Old Encryption Technique
                //objManageUser.Password = CryptoUtil.GetHashEncryptedValue(txtOldPassword.Text.Trim(), Constants.EncryptionAlgo_SHA1);
                //objManageUser.NewPassword = CryptoUtil.GetHashEncryptedValue(txtNewPassword.Text.Trim(), Constants.EncryptionAlgo_SHA1);
                objManageUser.AuditLog.ProgramCode = Constants.PrgCode_ChangePswd;

                string retValue = objManageUser.ChangePassword(UserType);
                //string retValue = "1|E0002|Charu Singla|no-reply@avaal.com|avaal1234"; //-- For Testing Purpose

                string[] retVal = retValue.Split('|');

                if (retVal[0] == "BOS-200") //Success
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ChangePswd, "Y", "L", Constants.Activity_ChangePswd, Constants.Activity_ChangePswd, Constants.Activity_Success);
                    string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    string amfURL = string.Empty;
                    //if (DomainName.Contains("afmsuite.com"))
                    //{
                    //    amfURL = ConfigurationManager.AppSettings["PRODUCTION"].ToString();
                    //}
                    //else
                    //{
                        amfURL = ConfigurationManager.AppSettings["AEDURL"].ToString();
                    //}
                    StringBuilder sbMailBody = new StringBuilder();
                    //sbMailBody = PortalCommon.ReadFile("../" + Constants.Path_TempltAManagerChangePassword);
                    //sbMailBody
                    //    .Replace("@UserFullName", (retVal[2]))
                    //    .Replace("@LoginId", (retVal[1]))
                    //    .Replace("@AEDURL", amfURL)
                    //    .Replace("@Password", (Session["Pwd"].ToString()));
                   // System.Net.Mail.Attachment att =
                    //PortalCommon.SendMailViaSMTP(retVal[3], "Change Password Mail", sbMailBody.ToString());
                   // Response.Write(sbMailBody);
                    //INF1004 - Password changed successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1004");
                    Session.RemoveAll();
                    divReLogin.Style["display"] = "block";
                    btnChangePswd.Enabled = false;
                    btnClear.Enabled = false;
                }
                else if (retValue == "2") //Failed
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ChangePswd, "N", "L", Constants.Activity_ChangePswd, Constants.Activity_ChangePswd, Constants.Activity_Failed);
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1008");
                    divReLogin.Style["display"] = "block";
                }
                else if (retValue == "3") //Failed
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ChangePswd, "N", "L", Constants.Activity_ChangePswd, Constants.Activity_ChangePswd, Constants.Activity_Failed);
                    //ERR1073 - Old Password should not be match with last 3 passwords.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1073");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ChangePswd, "N", "L", Constants.Activity_ChangePswd, Constants.Activity_ChangePswd, Constants.Activity_Failed);
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1074");
                    divReLogin.Style["display"] = "none";
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1001");
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1001");
            }
            finally
            {
                objManageUser = null;
            }
        }



        #region btnClear_Click

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Common.ClearFields(this, "0");
            spnMessage.InnerHtml = string.Empty;
        }

        #endregion btnClear_Click
    }
}