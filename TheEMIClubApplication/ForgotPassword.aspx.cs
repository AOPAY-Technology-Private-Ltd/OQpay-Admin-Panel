using AVFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.com.hisoftware.api2;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        //ApiHelpers ApiHelpers = new ApiHelpers();
        BLForgetPassword objForgetPass = new BLForgetPassword();

        //public ErrorLogging logging = new ErrorLogging();
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsPostBack)
            //    {
            //        GetValidationMessage();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Common.WriteExceptionLog(ex);
            //    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            //}
        }

        protected void ForgetPassword()
        {
            objForgetPass.emailid = txtEmail.Text;
            objForgetPass.Username = txtUserid.Text;
            string simplepassword = txtNewpassword.Text;
            objForgetPass.Newpassword = PortalCommon.GetRandomEncryptedPassword(out simplepassword);
            try
            {
                var response = objForgetPass.AdminForgatPassword();
                string ReturnCode = response.ReturnCode.ToString();
                string Returnmsg = response.ReturnMsg.ToString();
                if (ReturnCode != null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + Returnmsg + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + Returnmsg + "');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + ex + "');", true);
            }
        }
        //protected void ForgetPassword()
        //{
        //    try
        //    {
        //        ForgetPasswordResponse userResponse = new ForgetPasswordResponse();
        //        ForgotPasswordRequest model = new ForgotPasswordRequest();
        //        model.emailAddress = txtEmail.Text.Trim();
        //        model.userID = txtUserid.Text.Trim();
        //        string newPlainPassword = string.Empty;
        //        model.parmHashPassword = PortalCommon.GetRandomEncryptedPassword(out newPlainPassword);
        //        userResponse = ApiHelpers.ForgotPassword(model);
        //        if (userResponse.isSuccess == true)
        //        {
        //            //string breakTag = "<br>";
        //            short retVal = ReadMailTemplateAndSendMail(txtUserid.Text, newPlainPassword, "", txtEmail.Text);
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + userResponse.returnMessage + "');", true);
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + userResponse.returnMessage + "');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected void btnRequestPassword_Click(object sender, EventArgs e)
        {
            try
            {
                ForgetPassword();
            }

            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
            }

        }

        //#region ReadMailTemplateAndSendMail

        //private short ReadMailTemplateAndSendMail(string loginId, string password, string userFullName, string emailAddress)
        //{
        //    StringBuilder sbMailBody = new StringBuilder();
        //    BLManageMail objManageMail = new BLManageMail();

        //    string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        //    //string amfURL = string.Empty;
        //    //if (DomainName.Contains("afmsuite.com"))
        //    //{
        //    //    amfURL = ConfigurationManager.AppSettings["PRODUCTION"].ToString();
        //    //}
        //    //else
        //    //{
        //    //    amfURL = ConfigurationManager.AppSettings["DEVELOPMENT"].ToString();
        //    //}

        //    short retValue = 0;
        //    try
        //    {

        //        sbMailBody = PortalCommon.ReadFile("../" + Constants.Path_TempltForgotPswd);
        //        sbMailBody
        //                .Replace("@UserFullName", userFullName)
        //                .Replace("@LoginId", loginId)
        //                //.Replace("@AFMURL", amfURL)
        //                .Replace("@Password", password)
        //                 .Replace("@IPAddress", AppSessions.SessionRemoteHostIP = PortalCommon.GetRemoteHostIP())
        //                 .Replace("@URL", Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/");




        //        objManageMail.AlertCode = string.Empty;
        //        objManageMail.EmailBody = sbMailBody.ToString();
        //        objManageMail.EmailFlagValue = emailAddress; 


        //        PortalCommon.SendMailViaSMTP(emailAddress, "Reset Password Mail", sbMailBody.ToString());
        //        retValue = 1;
        //    }
        //    catch (ApplicationException appEx)
        //    {
        //        retValue = 0;
        //        throw appEx;
        //    }
        //    finally
        //    {
        //        sbMailBody = null;
        //        objManageMail = null;
        //    }
        //    return retValue;
        //}

        //#endregion ReadMailTemplateAndSendMail
    }
}