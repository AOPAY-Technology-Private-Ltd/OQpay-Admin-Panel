using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class CreateRole : System.Web.UI.Page
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
                    PortalCommon.BindDropDownList(ddlApplicationName, Constants.MstFlag_ApplicationListDDL, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);//Added By : Chetna 07/09/2016

                    //GetMenuScope();                
                    PortalCommon.BindDropDownList(ddlMenuScope, Constants.MstFlag_MenuScope, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "");//Added By : Chetna 07/09/2016
                    ListItem item = ddlMenuScope.Items.FindByText("ALL");
                    if (item != null)
                    {
                        ddlMenuScope.Items.Remove(item);
                    }
                    ddlMenuScope.Items.Insert(0, new ListItem("-Select-", ""));
                    GetValidationMessage();

                    spnUpdateRole.InnerText = Constants.Create_Role;


                    if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                           Request.QueryString["edocelor"].Trim().Length > 0)
                    {
                        //If coming from some other page.. redierct to that same page.
                        objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                        objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));
                        int mod4 = objManageUser.RoleCode.Length % 4;
                        if (mod4 > 0)
                        {
                            objManageUser.RoleCode += new string('=', 4 - mod4);
                        }

                        spnUpdateRole.InnerText = Constants.Update_Role;
                        btnSaveRole.Text = Constants.ManageRole_BtnUpdateMode_Text;

                        //if (!IsPostBack)
                        //{
                        //    GetActive_YNList();
                        //}

                        GetRoleDetail();
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
                //objCommon.GetApplicationNameList(ddlApplicationName);
                //ddlApplicationName.SelectedValue = "AM ";

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

        #region GetMenuScope

        private void GetMenuScope()
        {
            try
            {
                objManageUser.GetMenuScope(ddlMenuScope);
                ddlMenuScope.SelectedValue = "P";
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

        #endregion GetMenuScope

        #region CreateRole

        private void CreateRoles()
        {
            try
            {
                //    if (txtRoleName.Text.Length == 0)
                //    {
                //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
                //    }
                //    else if (ddlMenuScope.SelectedIndex == 0)
                //    {
                //        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner,"ERR1007");
                //    }
                //    else
                //    {
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                objManageUser.RoleName = txtRoleName.Text.Trim();
                objManageUser.RoleDescription = txtRoleDescription.Text.Trim();
                objManageUser.MenuScope = ddlMenuScope.SelectedValue;

                short retVal = objManageUser.CreateRole();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Create_Role,
                        Constants.Activity_Create_Role, Constants.Activity_Success);

                    //INF1015 - Role Created Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1015");
                    string roleCode = objManageUser.RoleCode;
                    string roleName = txtRoleName.Text.Trim();
                    string roleCode_roleName = roleCode + " (" + roleName + ")";
                    spnRoleNameAndCode.InnerText = roleCode_roleName;

                    if (ddlMenuScope.SelectedValue.Equals("P"))
                    {
                        //ddlApplicationName.Enabled = false;
                        //txtRoleName.Enabled = false;
                        //txtRoleDescription.Enabled = false;
                        //ddlMenuScope.Enabled = false;
                        //btnSaveRole.Enabled = false;
                        //btnClearRole.Enabled = false;
                        pnlCreateRoleSection.Enabled = false;
                        //dvCreateRoleSection.Disabled = true;

                        dvMenuAssignmentSection.Visible = true;
                        dvCheckBoxList.Visible = true;

                        GetMenuListByApp();
                    }
                    else
                    {
                        Common.ClearFields(this, "0");
                        txtRoleDescription.Text = string.Empty;
                        txtRoleName.Text = string.Empty;
                    }
                }
                else if (retVal == 2)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Create_Role,
                        Constants.Activity_Create_Role, Constants.Activity_Failed);

                    //ERR1013 - RoleName already exist.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1013");
                    dvMenuAssignmentSection.Visible = false;
                }
                else //if (retVal == 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Create_Role,
                        Constants.Activity_Create_Role, Constants.Activity_Failed);

                    //ERR1030 - Role could not be created.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1030");
                }
                //}
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

        #endregion CreateRole

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                       Request.QueryString["edocelor"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                    objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));

                    UpdateOnlyRoleDetail();
                }
                else
                {
                    CreateRoles();

                    if (ddlMenuScope.SelectedValue.Equals("P"))
                    {
                        ddlApplicationName.Enabled = false;
                        txtRoleName.Enabled = false;
                        txtRoleDescription.Enabled = false;
                        ddlMenuScope.Enabled = false;
                        btnSaveRole.Enabled = false;
                        btnClearRole.Enabled = false;

                        dvMenuAssignmentSection.Visible = true;

                        GetMenuListByApp();
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

        protected void btnClearRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                       Request.QueryString["edocelor"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                    objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));

                    Server.Transfer("../" + Constants.Path_ManageRole);

                }
                else
                {
                    ClearAll();

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

        #region GetMenuListByApp

        private void GetMenuListByApp()
        {
            try
            {
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                objManageUser.GetMenuListByApp(cblMenu);
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

        #endregion GetMenuListByApp

        #region ClearAll

        private void ClearAll()
        {
            try
            {
               // Common.ClearFields(this, "0");
                txtRoleName.Text = string.Empty;
                txtRoleDescription.Text = string.Empty;
                spnMessage.InnerText = string.Empty;
                //valReqMenuScope.ErrorMessage = string.Empty;
                //valReqApplicationName.ErrorMessage = string.Empty;
                //valReqRoleName.ErrorMessage = string.Empty;

               // ddlApplicationName.SelectedValue = "AM";

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        #endregion ClearAll

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                       Request.QueryString["edocelor"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                    objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));
                    UpdateRoleDetail();
                    //GetRoleDetail();
                }
                else
                {
                    SaveSelectedMenus();
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

        #region SaveSelectedMenus

        private void SaveSelectedMenus()
        {
            try
            {
                objManageUser.RoleCode = spnRoleNameAndCode.InnerText;

                string menuCode = string.Empty;
                string menuCodeWithComma = string.Empty;

                for (int i = 0; i < cblMenu.Items.Count; i++)
                {
                    if (cblMenu.Items[i].Selected)
                    {
                        menuCode = cblMenu.Items[i].Value;
                        menuCodeWithComma = menuCodeWithComma + menuCode + ",";
                    }
                }
                string menuCodewithoutComma = menuCodeWithComma.TrimEnd(',');
                objManageUser.MenuCode = menuCodewithoutComma;

                short retVal = objManageUser.SaveSelectedMenus();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Assign_Menus,
                        Constants.Activity_Assign_Menus, Constants.Activity_Success);

                    //INF1016 - Menu(s) saved Successfully.
                    spnMessageMenu.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1016");
                    spnMessage.InnerHtml = string.Empty;
                    btnSave.Enabled = false;
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Assign_Menus,
                        Constants.Activity_Assign_Menus, Constants.Activity_Failed);

                    //ERR1020 - Menu(s) not saved.
                    spnMessageMenu.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1020");
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

        #endregion SaveSelectedMenus

        #region GetRoleDetail

        private void GetRoleDetail()
        {
            Hashtable htEditRole = new Hashtable();
            try
            {
                objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));


                htEditRole = objManageUser.GetRoleDetail();

                if (htEditRole.Count > 0)
                {
                    ddlApplicationName.SelectedValue = Convert.ToString(htEditRole["ApplicationCode"]);
                    ddlApplicationName.SelectedValue = Convert.ToString(htEditRole["ApplicationCode"]);
                    txtRoleName.Text = Convert.ToString(htEditRole["RoleName"]);
                    txtRoleDescription.Text = Convert.ToString(htEditRole["RoleDesc"]);
                    ddlMenuScope.SelectedValue = Convert.ToString(htEditRole["MenuScope"]);
                    // ddlActiveStatus.SelectedValue = Convert.ToString(htEditRole["Active_YN"]);
                    ddlApplicationName.Enabled = false;
                    ddlMenuScope.Enabled = false;

                    if (ddlMenuScope.SelectedValue.Equals("A"))
                    {
                        dvMenuAssignmentSection.Visible = false;
                        btnSaveRole.Visible = true;

                        btnCancel.Visible = true;
                        btnCancel.Visible = false;
                        btnClearRole.Text = Constants.ManageMenu_BtnCancelMode_Text;
                    }
                    else //if (ddlMenuScope.SelectedValue.Equals("P"))
                    {
                        dvMenuAssignmentSection.Visible = true;
                        dvGridView.Visible = true;
                        cblMenu.Visible = false;
                        btnSave.Text = Constants.ManageRole_BtnUpdateMode_Text;// "Update";
                        btnSaveRole.Visible = false;
                        btnClearRole.Visible = false;
                        btnCancel.Visible = false;


                        DataTable dtEditMenu = new DataTable();

                        objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                        dtEditMenu = objManageUser.GetMenuDetail();

                        if (dtEditMenu.Rows.Count > 0)
                        {
                            gvShowSelectedMenus.DataSource = dtEditMenu;
                            gvShowSelectedMenus.DataBind();

                            FindChkBox_gvShowSelectedMenus();

                            spnRoleNameAndCode.InnerText = txtRoleName.Text.Trim() + " (" + objManageUser.RoleCode + ")";
                        }
                    }
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

        #endregion GetRoleDetail

        #region FindChkBox_gvShowSelectedMenus

        private void FindChkBox_gvShowSelectedMenus()
        {
            try
            {
                for (int i = 0; i < gvShowSelectedMenus.Rows.Count; i++)
                {
                    //if (gvShowSelectedMenus.Rows[i].Cells[3].Text.Equals("Y"))
                    if (gvShowSelectedMenus.DataKeys[i].Value.Equals("Y"))
                    {
                        ((CheckBox)gvShowSelectedMenus.Rows[i].Cells[0].FindControl("chkSelectedMenus")).Checked = true;
                    }
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
        #endregion FindChkBox_gvShowSelectedMenus

        #region UpdateRoleDetail

        private void UpdateRoleDetail()
        {
            try
            {
                objManageUser.RoleName = txtRoleName.Text.Trim();
                objManageUser.RoleDescription = txtRoleDescription.Text.Trim();
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                //objManageUser.Active_YN = (ddlActiveStatus.SelectedValue == "0") ? "Y" : ddlActiveStatus.SelectedValue;            
                objManageUser.Active_YN = "Y";

                int retVal = objManageUser.UpdateRoleDetail();


                if (retVal > 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Update_Role,
                        Constants.Activity_Update_Role, Constants.Activity_Success);

                    UpdateSelectedMenu();
                    //INF1017 - Role/Menu link Updated Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1017");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Update_Role,
                            Constants.Activity_Update_Role, Constants.Activity_Failed);

                    //ERR1016 - Role/Menu link could not updated.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1016");
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

        #endregion UpdateRoleDetail

        #region UpdateOnlyRoleDetail

        private void UpdateOnlyRoleDetail()
        {
            try
            {
                objManageUser.RoleName = txtRoleName.Text.Trim();
                objManageUser.RoleDescription = txtRoleDescription.Text.Trim();
                objManageUser.ApplicationCode = ddlApplicationName.SelectedValue;
                //objManageUser.Active_YN = (ddlActiveStatus.SelectedValue == "0") ? "Y" : ddlActiveStatus.SelectedValue;
                objManageUser.Active_YN = "Y";

                int retVal = objManageUser.UpdateRoleDetail();


                if (retVal > 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Update_Role,
                            Constants.Activity_Update_Role, Constants.Activity_Success);

                    //INF1024 - Role Details Updated Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1024");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Update_Role,
                                Constants.Activity_Update_Role, Constants.Activity_Failed);

                    //ERR1032 - Role Details could not updated.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1032");
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

        #endregion UpdateOnlyRoleDetail

        #region UpdateSelectedMenu

        private void UpdateSelectedMenu()
        {
            try
            {
                objManageUser.RoleCode = Request.QueryString["edocelor"].Trim();
                objManageUser.RoleCode = CryptoUtil.DecryptTripleDES(objManageUser.RoleCode.Replace(" ", "+"));
                string menuCode = string.Empty;
                string menuCodeWithComma = string.Empty;


                //for (int i = 0; i < gvShowSelectedMenus.Rows.Count; i++)
                //{
                //    if (gvShowSelectedMenus.Rows[i].Cells[].)
                //    {
                //        menuCode = cblMenu.Items[i].Value;
                //        menuCodeWithComma = menuCodeWithComma + menuCode + ",";
                //    }
                //}
                //string menuCodewithoutComma = menuCodeWithComma.TrimEnd(',');
                //objManageUser.MenuCode = menuCodewithoutComma;
                for (int i = 0; i < gvShowSelectedMenus.Rows.Count; i++)
                {
                    if (((CheckBox)gvShowSelectedMenus.Rows[i].Cells[0].FindControl("chkSelectedMenus")).Checked)
                    {
                        menuCode = gvShowSelectedMenus.Rows[i].Cells[1].Text;
                        menuCodeWithComma = menuCodeWithComma + menuCode + ",";
                    }
                }
                string menuCodewithoutComma = menuCodeWithComma.TrimEnd(',');
                objManageUser.MenuCode = menuCodewithoutComma;

                int retVal = objManageUser.UpdateMenuDetail();


                if (retVal > 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "Y", string.Empty, Constants.Activity_Update_Menu,
                            Constants.Activity_Update_Menu, Constants.Activity_Success);
                    //INF1012 - Role/Menu link Updated Successfully.
                    //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1012");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Role_Master, "N", string.Empty, Constants.Activity_Update_Menu,
                            Constants.Activity_Update_Menu, Constants.Activity_Failed);
                    //ERR1016 - Role/Menu link could not updated.
                    //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1016");
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

        #endregion UpdateSelectedMenu

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                           Request.QueryString["edocelor"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.

                    Server.Transfer("../" + Constants.Path_ManageRole);
                }
                else
                {
                    Server.Transfer("../" + Constants.Path_ManageRole);
                    return;
                    btnSave.Enabled = true;
                    ClearAll();
                    pnlCreateRoleSection.Enabled = true;


                    dvMenuAssignmentSection.Visible = false;
                    //btnSaveRole.Enabled = true;
                    //btnClearRole.Enabled = true;
                    //txtRoleDescription.Enabled = true;
                    //txtRoleName.Enabled = true;
                    //ddlApplicationName.Enabled = true;
                    //ddlMenuScope.Enabled = true;
                    spnMessageMenu.InnerHtml = string.Empty;
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

        //#region GetActive_YNList

        //private void GetActive_YNList()
        //{
        //    BLCommon objCommon = new BLCommon();

        //    try
        //    {
        //        objCommon.LovCode = "ACT";
        //        objCommon.GetLOVForYNFlags(ddlActiveStatus);
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

        //#endregion GetActive_YNList

        #region GetValidationMessage

        private void GetValidationMessage()
        {
            try
            {
                valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013"));//Validation Error(s)...

                valReqApplicationName.ErrorMessage = Common.GetMessageByParameters(Common.ApplicationType.WEB, "VAL1065", "Application Name");
                valReqMenuScope.ErrorMessage = Common.GetMessageByParameters(Common.ApplicationType.WEB, "VAL1065", "Menu Scope");
                valReqRoleName.ErrorMessage = Common.GetMessageByParameters(Common.ApplicationType.WEB, "VAL1065", "Role Name");

                //valReqApplicationName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024");//Please select Application.
                //valReqMenuScope.ErrorMessage = Common.GetMessageFromXMLFile("VAL1027");//Please select Menu Scope.
                //valReqRoleName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1028");//"Please enter Role Name";
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

        #region btnCancel_Click1
        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            try
            {
                Server.Transfer("../" + Constants.Path_ManageRole);
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        #endregion btnCancel_Click1
    }
}