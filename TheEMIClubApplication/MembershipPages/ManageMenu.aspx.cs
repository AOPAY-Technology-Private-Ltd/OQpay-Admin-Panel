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
    public partial class ManageMenu : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013"));//Validation Error(s)...
                valReqApplicationName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024");//Please select Application.
                                                                                             // valReqMenuName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1082");//Please select Menu Name.

                if (!IsPostBack)
                {
                    //GetApplicationNameList();
                    PortalCommon.BindDropDownList(ddlApplicationName, Constants.MstFlag_ApplicationListDDL, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016

                    //GetMenuHaltCodeList(); 
                    PortalCommon.BindDropDownList(ddlMenuHaltCode, Constants.Menu_Halt_Code, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016

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

        #region GetApplicationNameList

        private void GetApplicationNameList()
        {
            BLCommon objCommon = new BLCommon();
            try
            {
                //objCommon.GetApplicationNameList(ddlApplicationName);
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



        #region GetMenuHaltCodeList

        private void GetMenuHaltCodeList()
        {
            BLManageUser objManageUser = new BLManageUser();

            try
            {
                objManageUser.MasterCode = Constants.Menu_Halt_Code;//"MHC";
                                                                    //objManageUser.GetMenuHaltCodeList(ddlMenuHaltCode);
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

        #endregion GetMenuHaltCodeList



        #region GetMenuDetailForEdit

        private void GetMenuDetailForEdit()
        {
            DataTable dtManageUser = new DataTable();

            try
            {
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;


                objManageUser.Active_YN = ddlStatus.SelectedValue == "0" ? string.Empty : ddlStatus.SelectedValue;
                objManageUser.MenuName = txtMenuName.Text.Trim();
                objManageUser.MenuHaltCode = ddlMenuHaltCode.SelectedValue == "0" ? string.Empty : ddlMenuHaltCode.SelectedValue;
                objManageUser.NavigateURL = txtNavigateURL.Text.Trim();


                dtManageUser = objManageUser.GetMenuDetailForEdit();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvManageMenu.PageSize = PortalCommon.GetGridPageSize;
                    gvManageMenu.DataSource = dtManageUser;
                    gvManageMenu.DataBind();

                    for (int i = 0; i < gvManageMenu.Rows.Count; i++)
                    {
                        if (((LinkButton)gvManageMenu.Rows[i].Cells[6].FindControl("lnkAction")).Text.Equals(Constants.IsActive))
                        {
                            ((LinkButton)gvManageMenu.Rows[i].Cells[7].FindControl("lnkEdit")).Enabled = false;
                        }
                    }
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvManageMenu.DataSource = null;
                    gvManageMenu.DataBind();
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

        #endregion GetMenuDetailForEdit



        #region ddlApplicationName_SelectedIndexChanged

        //protected void ddlApplicationName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetMenuDetailForEdit();
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

        #endregion ddlApplicationName_SelectedIndexChanged



        protected void gvManageMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvManageMenu.PageIndex = e.NewPageIndex;
                GetMenuDetailForEdit();
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

        protected void gvManageMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EDT")) || (e.CommandName.Equals("ACT")))
                {
                    string tempvar = Convert.ToString(e.CommandArgument);
                    string[] tempVarArr = tempvar.Split(new char[] { '|' });
                    string menuCode = tempVarArr[0];
                    string active_YN = tempVarArr[1];

                    if (e.CommandName.Equals("EDT"))
                    {
                        Response.Redirect("CreateMenu.aspx?menucode=" + CryptoUtil.EncryptTripleDES(menuCode), false);
                    }

                    else if (e.CommandName.Equals("ACT"))
                    {

                        objManageUser.MenuCode = menuCode;

                        if (active_YN.Equals(Constants.IsActive)) //"Active?"
                        {
                            objManageUser.Active_YN = "Y";
                        }
                        else
                        {
                            objManageUser.Active_YN = "N";
                        }

                        short retVal = objManageUser.UpdateMenuActiveOrInactive();

                        if (retVal == 1)
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "Y", string.Empty, Constants.Activity_Update_Menu_Act_Inact,
                            Constants.Activity_Update_Menu_Act_Inact, Constants.Activity_Success);
                        }
                        else
                        {
                            PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "N", string.Empty, Constants.Activity_Update_Menu_Act_Inact,
                            Constants.Activity_Update_Menu_Act_Inact, Constants.Activity_Failed);
                        }

                        GetMenuDetailForEdit();
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
                GetMenuDetailForEdit();
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
                gvManageMenu.DataSource = null;
                gvManageMenu.DataBind();
                ddlApplicationName.SelectedIndex = 0;
                ddlMenuHaltCode.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
                txtMenuName.Text = "";
                txtNavigateURL.Text = "";
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