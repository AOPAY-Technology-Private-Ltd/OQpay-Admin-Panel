using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class CreateMenu : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 if (!IsPostBack)
                {
                    //GetApplicationNameList();
                    PortalCommon.BindDropDownList(ddlApplicationName, Constants.MstFlag_ApplicationListDDL, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016


                    //GetMenuHaltCodeList();               
                    //PortalCommon.BindDropDownList(ddlMenuHaltCode, Constants.Menu_Halt_Code, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016

                    spnUpdateMenu.InnerText = Constants.Create_Menu;//"Create Menu";
                    GetValidationMessage();

                    //PortalCommon.BindDropDownList(ddlFeature, Constants.MstFlag_MenuFeatureList, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");

                    if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["menucode"], null)) &&
                       Request.QueryString["menucode"].Trim().Length > 0)
                    {
                        //If coming from some other page. redierct to that same page.
                        objManageUser.MenuCode = Request.QueryString["menucode"];
                        objManageUser.MenuCode = CryptoUtil.DecryptTripleDES(objManageUser.MenuCode.Replace(" ", "+"));

                        string menuCode = objManageUser.MenuCode;

                        spnUpdateMenu.InnerText = Constants.Update_Menu;//"Update Menu";
                        btnSaveMenu.Text = Constants.ManageRole_BtnUpdateMode_Text;//"Update";
                        btnClearMenu.Text = Constants.ManageMenu_BtnCancelMode_Text;//"Cancel";

                        //trActiveStatus.Visible=true; 
                        spnDisplayOrder.Visible = false;


                        //GetParentMenuList();
                        GetActive_YNList();

                        //code for bind dropdown in update mode.
                        //objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;                        

                        GetMenuDetailForEditMode(menuCode);
                        ddlApplicationName.Enabled = false;
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

        #endregion PageLoad

        #region GetApplicationNameList

        private void GetApplicationNameList()
        {
            BLCommon objCommon = new BLCommon();

            try
            {
                objCommon.GetApplicationNameList(ddlApplicationName);
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

        #region GetDisplayOrder

        private void GetDisplayOrder(string applicationCode)
        {
            BLManageUser objManageUser = new BLManageUser();
            Hashtable htDisplayOrder = new Hashtable();
            try
            {
                objManageUser.ApplicationCode = applicationCode;
                htDisplayOrder = objManageUser.GetDisplayOrder();

                if (htDisplayOrder.Count > 0)
                {
                    spnDisplayOrder.InnerText = "(Last given Display Order is: " + Convert.ToString(htDisplayOrder["DisplayOrder"]) + ")";
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

        #endregion GetDisplayOrder

        #region GetParentMenuList

        private void GetParentMenuList(string appCode, string menuCode, string mode)
        {
            BLManageUser objManageUser = new BLManageUser();

            try
            {
                spnMessage.InnerHtml = string.Empty;
                objManageUser.ApplicationCode = appCode;
                objManageUser.MenuCode = menuCode;
                objManageUser.Mode = mode;
                //objManageUser.GetParentMenuList(ddlParentMenu);
                PortalCommon.BindDropDownList(ddlParentMenu, Constants.MstFlag_ParentMenu, objManageUser.ApplicationCode, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");//Added By : Chetna 07/09/2016
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

        #endregion GetParentMenuList

        protected void ddlApplicationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetParentMenuList(ddlApplicationName.SelectedValue, string.Empty, Constants.Insert_Mode);
                GetDisplayOrder(ddlApplicationName.SelectedValue);
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

        #region CreateMenu

        private void CreateMenus()
        {
            BLManageUser objManageUser = new BLManageUser();
            try
            {
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                objManageUser.MenuName = txtMenuName.Text.Trim();
                objManageUser.NavigateURL = txtNavigateURL.Text.Trim();
                objManageUser.ParentMenuCode = ddlParentMenu.SelectedValue;
                objManageUser.ToolTip = txtToolTip.Text.Trim();
                objManageUser.Description = txtMenuDescription.Text.Trim();
                objManageUser.DisplayOrder = txtDisplayOrder.Text.Trim();
                //objManageUser.MenuHaltCode = ddlMenuHaltCode.SelectedValue;
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                //objManageUser.SubscriptionFeature = ddlFeature.SelectedIndex > 0 ? ddlFeature.SelectedValue : null;

                short retVal = objManageUser.CreateMenu();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "Y", string.Empty, Constants.Activity_Create_Menu,
                        Constants.Activity_Create_Menu, Constants.Activity_Success);

                    ClearAll();
                    spnDisplayOrder.InnerText = string.Empty;
                    //INF1018 - Menu Created Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1018");
                    string menuCode = objManageUser.MenuCode;
                    string menuName = txtMenuName.Text.Trim();
                    string menuCode_menuName = menuCode + " (" + menuName + ")";

                }
                else if (retVal == 2)
                {
                    //ERR1048 - MenuName already exist.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1048");
                    //dvMenuAssignmentSection.Visible = false;
                }
                else //if (retVal == 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "N", string.Empty, Constants.Activity_Create_Menu,
                        Constants.Activity_Create_Menu, Constants.Activity_Failed);

                    //ERR1030 - Role(s) could not created.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1030");
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

        #endregion CreateMenu

        protected void btnSaveMenu_Click(object sender, EventArgs e)
        {
            try
             {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["menucode"], null)) &&
                       Request.QueryString["menucode"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    objManageUser.MenuCode = Request.QueryString["menucode"].Trim();
                    objManageUser.MenuCode = CryptoUtil.DecryptTripleDES(objManageUser.MenuCode.Replace(" ", "+"));

                    UpdateMenu();
                }
                else
                {
                    CreateMenus();
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

        #region GetMenuDetailForEditMode

        private void GetMenuDetailForEditMode(string menuCode)
        {
            BLManageUser objManageUser = new BLManageUser();
            Hashtable htEditMenu = new Hashtable();
            try
            {
                objManageUser.MenuCode = menuCode;

                htEditMenu = objManageUser.GetMenuDetailForEditMode();

                if (htEditMenu.Count > 0)
                {
                    ddlApplicationName.SelectedValue = Convert.ToString(htEditMenu["ApplicationCode"]);

                    //call the function to bind the parent menu dropdown.
                    GetParentMenuList(ddlApplicationName.SelectedValue, objManageUser.MenuCode, Constants.Update_Mode);

                    txtMenuName.Text = Convert.ToString(htEditMenu["MenuName"]);
                    txtMenuDescription.Text = Convert.ToString(htEditMenu["Description"]);
                    txtToolTip.Text = Convert.ToString(htEditMenu["ToolTip"]);
                    txtNavigateURL.Text = Convert.ToString(htEditMenu["NavigateURL"]);
                    ddlParentMenu.SelectedValue = Convert.ToString(htEditMenu["ParentMenuCode"]);
                    txtDisplayOrder.Text = Convert.ToString(htEditMenu["DisplayOrder"]);
                    //ddlMenuHaltCode.SelectedValue = Convert.ToString(htEditMenu["MenuHaltCode"]);
                    ddlActiveStatus.SelectedValue = Convert.ToString(htEditMenu["Active_YN"]);
                    //ddlFeature.SelectedValue = Convert.ToString(htEditMenu["FeatureCode"]);
                    objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                    //objManageUser.GetParentMenuList(ddlParentMenu);

                    //ddlApplicationName.Enabled = false;
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

        #endregion GetMenuDetailForEditMode

        #region UpdateMenu

        private void UpdateMenu()
        {
            BLManageUser objManageUser = new BLManageUser();
            try
            {
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                objManageUser.MenuCode = Request.QueryString["menucode"].Trim();
                objManageUser.MenuCode = CryptoUtil.DecryptTripleDES(objManageUser.MenuCode.Replace(" ", "+"));
                objManageUser.MenuName = txtMenuName.Text.Trim();
                objManageUser.NavigateURL = txtNavigateURL.Text.Trim();
                objManageUser.ParentMenuCode = ddlParentMenu.SelectedValue;
                objManageUser.ToolTip = txtToolTip.Text.Trim();
                objManageUser.Description = txtMenuDescription.Text.Trim();
                objManageUser.DisplayOrder = txtDisplayOrder.Text.Trim();
                //objManageUser.MenuHaltCode = ddlMenuHaltCode.SelectedValue;
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.Active_YN = (ddlActiveStatus.SelectedValue == "0") ? "Y" : ddlActiveStatus.SelectedValue;
                //objManageUser.SubscriptionFeature = ddlFeature.SelectedIndex > 0 ? ddlFeature.SelectedValue : null;

                short retVal = objManageUser.UpdateMenu();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "Y", string.Empty, Constants.Activity_Update_Menu,
                        Constants.Activity_Update_Menu, Constants.Activity_Success);

                    //INF1025 - Menu Details updated Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1025");
                    string menuCode = objManageUser.MenuCode;
                    string menuName = txtMenuName.Text.Trim();
                    string menuCode_menuName = menuCode + " (" + menuName + ")";
                }
                else if (retVal == 2)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "N", string.Empty, Constants.Activity_Update_Menu,
                        Constants.Activity_Update_Menu, Constants.Activity_Failed);

                    //ERR1026 - You can not assign a Menu as Parent of its Parent.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1026");
                }
                else //if (retVal == 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Menu_Master, "N", string.Empty, Constants.Activity_Update_Menu,
                        Constants.Activity_Update_Menu, Constants.Activity_Failed);

                    //ERR1015 - Menu could not updated..
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1015");
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

        #endregion UpdateMenu

        #region ClearAll

        private void ClearAll()
        {
            try
            {
                Common.ClearFields(this, "0");
                txtMenuName.Text = string.Empty;
                txtMenuDescription.Text = string.Empty;
                txtNavigateURL.Text = string.Empty;
                txtToolTip.Text = string.Empty;
                txtDisplayOrder.Text = string.Empty;
                //spnMessage.InnerHtml = string.Empty;
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

        protected void btnClearMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["menucode"], null)) &&
                       Request.QueryString["menucode"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.

                    Server.Transfer("../" + Constants.Path_ManageMenu, false);
                }
                else
                {
                    ClearAll();
                    spnMessage.InnerHtml = string.Empty;
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

        #region GetValidationMessage
        /// <summary>
        /// Method for Validation Message.
        /// </summary>
        private void GetValidationMessage()
        {
            try
            {
                valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013"));//Validation Error(s)...
                valReqApplicationName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024");//Please select Application.
                valRegDisplayOrder.ErrorMessage = Common.GetMessageFromXMLFile("VAL1029"); //Please enter valid number only.
                valReqMenuName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1030"); //Please enter Menu Name.
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

        #endregion GetValidationMessage

        #region GetActive_YNList

        private void GetActive_YNList()
        {
            BLCommon objCommon = new BLCommon();

            try
            {
                objCommon.LovCode = "ACT";
                objCommon.GetLOVForYNFlags(ddlActiveStatus);
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

        #endregion GetActive_YNList
    }
}