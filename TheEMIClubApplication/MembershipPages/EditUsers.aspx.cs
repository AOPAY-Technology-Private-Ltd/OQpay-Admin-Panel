using AjaxControlToolkit;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.AppCode;

using System.Collections;

using System.Data;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class EditUsers : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();
        DataTable dtManageUser = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserDetail();
        }
        #region GetUserDetail

        private void GetUserDetail()
        {
            DataTable dtManageUser = new DataTable();

            //BLAuditLog objAuditLog = new BLAuditLog();
            try
            {
                objManageUser.UserName=txtuserid.Text.Trim();
                objManageUser.EmailAddress =txtEmail.Text.Trim();
                   
                objManageUser.MobileNo = txtmobileno.Text.Trim();
                objManageUser.Flag = "Show";


                dtManageUser = objManageUser.EditUserDetail();
                if (dtManageUser.Rows.Count > 0)
                {
                    //spnMsg.InnerText = string.Empty;
                    EditUserDetails.PageSize = PortalCommon.GetGridPageSize;
                    EditUserDetails.DataSource = dtManageUser;
                    EditUserDetails.DataBind();


                    for (int i = 0; i < EditUserDetails.Rows.Count; i++)
                    {
                        if (((LinkButton)EditUserDetails.Rows[i].Cells[5].FindControl("lnkAction")).Text.Equals(Constants.IsActive))
                        {
                            ((LinkButton)EditUserDetails.Rows[i].Cells[6].FindControl("lnkEdit")).Enabled = false;
                        }



                        //}

                    }
                }
                else
                {
                    //MSG1001 - No Records(s) Found.
                    //  spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "MSG1001");
                    // spnMsg.Attributes.Add("class", Constants.MessageCSS);
                    EditUserDetails.DataSource = null;
                    EditUserDetails.DataBind();
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

      

        #region btnSearch_Click

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetUserDetail();
            }

           
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                
            }
        }

        #endregion btnSearch_Click

        protected void EditUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EDT")) || (e.CommandName.Equals("ACT")))
                {
                    //string tempvar = Convert.ToString(e.CommandArgument);
                    //string[] tempVarArr = tempvar.Split(new char[] { '|' });
                    //string Userid = tempVarArr[0];
                    //string userName = tempNameArr[0];

                    string tempName = Convert.ToString(e.CommandArgument);
                    string[] tempNameArr = tempName.Split(new char[] { '|' });
                    string Userid = tempNameArr[0];
                
                    string active_YN = tempNameArr[1];
                    string Phone= tempNameArr[2];

                    if (e.CommandName.Equals("EDT"))
                    {
                        Response.Redirect("CreateUsers.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(Phone), false);
                    }
                    else if (e.CommandName.Equals("ACT"))
                    {
                    


                        objManageUser.UserName = Userid;
                      

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


                }

            }

            catch (Exception ex)
            {
                // Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

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


    }
}