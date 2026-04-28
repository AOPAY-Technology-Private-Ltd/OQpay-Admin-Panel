using TheEMIClubApplication.AppCode;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.UserControls
{
    public partial class AMDateLoginHeader : System.Web.UI.UserControl
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    spnDate.InnerText = string.Format("{0:dddd, MMMM dd, yyyy}", System.DateTime.Now.Date); //Eg. Wednesday, August 20, 2015               

                    spnUserName.InnerText =
                        (AppSessions.SessionUserFullName.Length > 0) ? AppSessions.SessionUserFullName : "Guest";
                    spnBranchName.InnerText = string.Empty;
                    //(AppSessions.SessionUserBranchName.Length > 0) ? (" (" + AppSessions.SessionUserBranchName + ")") : string.Empty;

                    lnkHome.Visible = (AppSessions.SessionLoginId.Length > 0) ? true : false;
                    lnkLogout.Visible = (AppSessions.SessionLoginId.Length > 0) ? true : false;

                    //Do not show 'Home' link on Home page.
                    if (Request.Url.ToString().Contains(Constants.Path_HomePage))
                    {
                        //LinkButton lnkHome1 = (LinkButton)this.Master.FindControl("ucFWDateLoginHeader").FindControl("lnkHome");
                        lnkHome.Visible = false;
                    }

                    //Check Login Preserved status:
                    spnLoginPreserved.Visible = (AppSessions.SessionPreserveMyLogin_YN.Length > 0) ? true : false;
                    if ((AppSessions.SessionLoginId.Length > 0) &&
                        (AppSessions.SessionPreserveMyLogin_YN.Length == 0))
                    {
                        lnkPreserveMyLogin.Visible = true;
                        spnLoginPreserved.Visible = false;
                    }
                }
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx);
            }
        }

        #endregion Page_Load

        #region Logout

        protected void Logout(object sender, EventArgs e)
        {
            //Manage User's Application-Session Log
            //PortalCommon.ManageUserAppSessionLog(Constants.PrgCode_Logout);

            //Insert Activity Log here --Start
            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Logout, "Y", "L", Constants.Activity_Logout, Constants.Activity_Logout,
                Constants.Activity_Success);
            //Insert Activity Log here --End

            //Remove 'Preserved login cookies' (if any)
            //PortalCommon.RemoveLoginCookieValues();

            //Remove all sessions.
            AppSessions.RemoveAllSessions();

            Response.Redirect("../" + Constants.Path_LoginPage + "?edom=tougol");
        }

        #endregion Logout

        #region GoToHomePage

        protected void GoToHomePage(object sender, EventArgs e)
        {
            Response.Redirect("../" + Constants.Path_HomePage);
        }

        #endregion GoToHomePage

        #region PreserveMyLogin

        protected void PreserveMyLogin(object sender, EventArgs e)
        {
            try
            {
                if (AppSessions.SessionLoginId.Length > 0)
                {
                    //If User session alive.....
                    AppSessions.SessionPreserveMyLogin_YN = "Y";
                    PortalCommon.PreserveLoginCookie();

                    //Manage User's Application-Session Log
                    PortalCommon.ManageUserAppSessionLog(Constants.PrgCode_PreserveMyLogin);

                    //Insert Activity Log here --Start
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_PreserveMyLogin, "Y", string.Empty, Constants.LogAction_LoginPreserved,
                        Constants.Activity_PreserveMyLogin, Constants.Activity_Success);
                    //Insert Activity Log here --End

                    //lnkPreserveMyLogin.Visible = false;
                    // spnLoginPreserved.Visible = true;
                }
            }
            catch (ApplicationException appEx)
            {
                Common.WriteExceptionLog(appEx);
            }
        }

        #endregion PreserveMyLogin

        #region ChangePassword

        public void ChangePassword(object sender, EventArgs e)
        {
            Response.Redirect("../" + Constants.Path_ChangePswdPage);
        }

        #endregion ChangePassword

        protected void lnkProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Admin/ComapnyMst.aspx");
        }
    }
}