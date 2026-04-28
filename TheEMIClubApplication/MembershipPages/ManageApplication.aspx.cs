using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class ManageApplication : System.Web.UI.Page
    {
        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    GetApplicationType();
                    PortalCommon.GetActive_YNList(ddlStatus);
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

        #endregion PageLoad

        #region GetApplicationType

        private void GetApplicationType()
        {
            BLManageUser objManageUser = new BLManageUser();
            try
            {
                objManageUser.MasterCode = Constants.Application_Code;//"APT";
                objManageUser.GetMenuHaltCodeList(ddlApplicationType);
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

        #endregion GetApplicationType

        #region GetApplicationDetailForEdit

        private void GetApplicationDetailForEdit()
        {
            BLManageUser objManageUser = new BLManageUser();
            DataTable dtManageUser = new DataTable();

            try
            {
                //objManageUser.ApplicationCode = txtApplicationCode.Text.Trim();
                objManageUser.ApplicationName = txtApplicationName.Text.Trim();
                objManageUser.Active_YN = ddlStatus.SelectedValue == "0" ? string.Empty : ddlStatus.SelectedValue;
                objManageUser.ApplicationType = ddlApplicationType.SelectedValue == "0" ? string.Empty : ddlApplicationType.SelectedValue;

                dtManageUser = objManageUser.GetApplicationDetailForEdit();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvManageApplication.PageSize = PortalCommon.GetGridPageSize;
                    gvManageApplication.DataSource = dtManageUser;
                    gvManageApplication.DataBind();

                    for (int i = 0; i < gvManageApplication.Rows.Count; i++)
                    {
                        if (((LinkButton)gvManageApplication.Rows[i].Cells[5].FindControl("lnkAction")).Text.Equals(Constants.IsActive))
                        {
                            ((LinkButton)gvManageApplication.Rows[i].Cells[6].FindControl("lnkEdit")).Enabled = false;
                        }
                    }
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvManageApplication.DataSource = null;
                    gvManageApplication.DataBind();
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

        #endregion GetApplicationDetailForEdit

        protected void gvManageApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvManageApplication.PageIndex = e.NewPageIndex;
                GetApplicationDetailForEdit();
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

        protected void gvManageApplication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            BLManageUser objManageUser = new BLManageUser();
            try
            {
                if ((e.CommandName.Equals("EDT")) || (e.CommandName.Equals("ACT")))
                {
                    string tempvar = Convert.ToString(e.CommandArgument);
                    string[] tempVarArr = tempvar.Split(new char[] { '|' });
                    string applicationCode = tempVarArr[0];
                    string active_YN = tempVarArr[1];

                    if (e.CommandName.Equals("EDT"))
                    {
                        Response.Redirect("CreateApplication.aspx?edocnoitacilppa=" + CryptoUtil.EncryptTripleDES(applicationCode), false);
                    }

                    else if (e.CommandName.Equals("ACT"))
                    {

                        objManageUser.ApplicationCode = applicationCode;

                        if (active_YN.Equals(Constants.IsActive)) //"Active?"
                        {
                            objManageUser.Active_YN = "Y";
                        }
                        else
                        {
                            objManageUser.Active_YN = "N";
                        }

                        short retVal = objManageUser.UpdateApplicationActiveOrInactive();

                        if (retVal == 1)
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "Y", string.Empty, Constants.Activity_Update_Application_Act_Inact,
                                Constants.Activity_Update_Application_Act_Inact, Constants.Activity_Success);

                            GetApplicationDetailForEdit();
                        }

                        else if (retVal == 2)
                        {
                            //This Application can not be Deactivated.
                            spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "VAL1081", applicationCode);

                            //string message = "This Application can not be Deactivated.";
                            //this.ClientScript.RegisterStartupScript(typeof(Page), "Popup", "window.alert('" + message + "');", true);
                            //this.Page.RegisterClientScriptBlock("myrest", "<script> window.alert('This Application can not be Deactivated.')</script>");

                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "N", string.Empty, Constants.Activity_Update_Application_Act_Inact,
                                Constants.Activity_Update_Application_Act_Inact, Constants.Activity_Failed);
                        }

                        else
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "N", string.Empty, Constants.Activity_Update_Application_Act_Inact,
                                Constants.Activity_Update_Application_Act_Inact, Constants.Activity_Failed);
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
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }



        #region btnSearch_Click

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetApplicationDetailForEdit();
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

        #endregion btnSearch_Click

        #region btnClear_Click

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Common.ClearFields(this, "0");
                spnMessage.InnerHtml = string.Empty;
                gvManageApplication.DataSource = null;
                gvManageApplication.DataBind();
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #endregion btnClear_Click


    }

}