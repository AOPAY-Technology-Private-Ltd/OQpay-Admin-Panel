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
    public partial class ManageRole : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //valReqApplicationName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024");//Please select Application.

                if (!IsPostBack)
                {
                    PortalCommon.BindDropDownList(ddlApplicationName, Constants.MstFlag_ApplicationList, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-All-");
                    ddlApplicationName.SelectedValue = "AM";
                    PortalCommon.BindDropDownList(ddlMenuScope, Constants.MstFlag_MenuScope, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-All-");

                    // GetApplicationNameList();
                    PortalCommon.GetActive_YNList(ddlStatus);

                    PortalCommon.GetActive_YNList(ddlStatus);
                    ddlStatus.SelectedValue = "Y";
                    // GetMenuScope(); 
                    GetRoleDetailForEdit();
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

        //#region GetApplicationNameList

        //private void GetApplicationNameList()
        //{
        //    BLCommon objCommon = new BLCommon();
        //    try
        //    {
        //        objCommon.GetApplicationNameList(ddlApplicationName);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion GetApplicationNameList



        //#region GetMenuScope

        //private void GetMenuScope()
        //{
        //    try
        //    {
        //        objManageUser.GetMenuScope(ddlMenuScope);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion GetMenuScope



        #region GetRoleDetailForEdit

        private void GetRoleDetailForEdit()
        {
            DataTable dtManageUser = new DataTable();

            try
            {
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;


                objManageUser.RoleName = txtRoleName.Text.Trim();
                objManageUser.Active_YN = ddlStatus.SelectedValue == "0" ? string.Empty : ddlStatus.SelectedValue;
                objManageUser.MenuScope = ddlMenuScope.SelectedValue == "0" ? string.Empty : ddlMenuScope.SelectedValue;


                dtManageUser = objManageUser.GetRoleDetailForEdit();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvManageRole.PageSize = PortalCommon.GetGridPageSize;
                    gvManageRole.DataSource = dtManageUser;
                    gvManageRole.DataBind();

                    for (int i = 0; i < gvManageRole.Rows.Count; i++)
                    {
                        if (((LinkButton)gvManageRole.Rows[i].Cells[5].FindControl("lnkAction")).Text.Equals(Constants.IsActive))
                        {
                            ((LinkButton)gvManageRole.Rows[i].Cells[6].FindControl("lnkEdit")).Enabled = false;
                        }


                        if (Convert.ToString(PortalCommon.GetRuleNConfigValue(Constants.DeactivateSuperRole_YN, Constants.TEXT)).ToUpper().Equals("N"))
                        {
                            if (gvManageRole.Rows[i].Cells[3].Text.Equals("ALL"))
                            {
                                ((LinkButton)gvManageRole.Rows[i].Cells[5].FindControl("lnkAction")).Enabled = false;

                                //Role having 'ALL' as Menu Scope, can not be Deactivated.
                                ((LinkButton)gvManageRole.Rows[i].Cells[5].FindControl("lnkAction")).ToolTip = Common.GetMessageFromXMLFile("MSG1010");
                            }
                        }
                        //IT09, 21-Feb-2012, End.
                    }
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvManageRole.DataSource = null;
                    gvManageRole.DataBind();
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

        #endregion GetRoleDetailForEdit



        #region ddlApplication_SelectedIndexChanged

        //protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetRoleDetailForEdit();
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
        //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        #endregion ddlApplication_SelectedIndexChanged



        protected void gvManageRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvManageRole.PageIndex = e.NewPageIndex;
                GetRoleDetailForEdit();
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

        protected void gvManageRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EDT")) || (e.CommandName.Equals("ACT")))
                {
                    string tempvar = Convert.ToString(e.CommandArgument);
                    string[] tempVarArr = tempvar.Split(new char[] { '|' });
                    string roleCode = tempVarArr[0];
                    string active_YN = tempVarArr[1];

                    if (e.CommandName.Equals("EDT"))
                    {
                        Response.Redirect("CreateRole.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(roleCode), false);
                    }

                    else if (e.CommandName.Equals("ACT"))
                    {
                        objManageUser.RoleCode = roleCode;

                        if (active_YN.Equals(Constants.IsActive)) //"Active?"
                        {
                            objManageUser.Active_YN = "Y";
                        }
                        else
                        {
                            objManageUser.Active_YN = "N";
                        }

                        short retVal = objManageUser.UpdateRoleActiveOrInactive();

                        if (retVal == 1)
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Update_Role_Act_Inact,
                            Constants.Activity_Update_Role_Act_Inact, Constants.Activity_Success);
                        }
                        else
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Update_Role_Act_Inact,
                            Constants.Activity_Update_Role_Act_Inact, Constants.Activity_Failed);
                        }
                        GetRoleDetailForEdit();
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
                GetRoleDetailForEdit();
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
                gvManageRole.DataSource = null;
                gvManageRole.DataBind();
                spnMessage.InnerHtml = string.Empty;
                ddlApplicationName.SelectedValue = "AM";
                ddlStatus.SelectedValue = "Y";
                GetRoleDetailForEdit();
                // ddlMenuScope.SelectedValue = "P";

            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        #endregion btnClear_Click
        protected void imgbtnPlus_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Path_CreateRole -> MembershipPages/CreateRole.aspx
                Response.Redirect(Constants.Path_CreateRole);
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
    }
}