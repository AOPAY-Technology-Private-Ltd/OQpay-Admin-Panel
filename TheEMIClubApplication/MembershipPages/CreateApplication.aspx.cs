using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using TheEMIClubApplication.AppCode;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class CreateApplication : System.Web.UI.Page
    {
        BLManageUser objManageUser = new BLManageUser();

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                spnUpdateApplication.InnerText = Constants.Create_Application;
                this.Form.DefaultFocus = txtApplicationCode.UniqueID;
                this.Form.DefaultButton = btnSaveApplication.UniqueID;

                if (!IsPostBack)
                {
                    GetValidationMessage();
                    GetApplicationTypeCode();
                    //GetlovCodeForActive_YN();

                }
                //ddlActiveStatus.SelectedValue = "Y";
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocnoitacilppa"], null)) &&
                       Request.QueryString["edocnoitacilppa"].Trim().Length > 0)
                {

                    //If coming from some other page.. redierct to that same page.
                    objManageUser.ApplicationCode = Request.QueryString["edocnoitacilppa"].Replace(" ", "+").Trim();
                    objManageUser.ApplicationCode = CryptoUtil.DecryptTripleDES(objManageUser.ApplicationCode.Replace(" ", "+"));
                    spnUpdateApplication.InnerText = Constants.Update_Application;
                    btnSaveApplication.Text = Constants.ManageRole_BtnUpdateMode_Text;
                    btnClearApplication.Text = Constants.ManageMenu_BtnCancelMode_Text;

                    if (!IsPostBack)
                    {
                        GetActive_YNList();
                        GetApplicationFullDetailForUpdate();
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

        #region CreateApplications

        private void CreateApplications()
        {
            try
            {
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.ApplicationCode = txtApplicationCode.Text.Trim();
                objManageUser.ApplicationName = txtApplicationName.Text.Trim();
                objManageUser.ApplicationDesc = txtApplicationDescription.Text.Trim();
                objManageUser.DeployLocation = txtDeployLocation.Text.Trim();
                objManageUser.ApplicationType = ddlApplicationType.SelectedValue;
                objManageUser.ApplicationURL = txtApplicationURL.Text.Trim();
                objManageUser.PrimaryDBName = txtPrimaryDBName.Text.Trim();
                objManageUser.DBCode = txtDBCode.Text.Trim();
                objManageUser.PrgCode = Constants.PrgCode_ManageApplication;

                short retVal = objManageUser.CreateApplication();

                if (retVal == 1)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "Y", string.Empty, Constants.Activity_Create_Application,
                        Constants.Activity_Create_Application, Constants.Activity_Success);

                    //INF1013 - Application Created Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1013");
                    Common.ClearFields(this, "0");
                    txtApplicationCode.Text = string.Empty;
                    txtApplicationName.Text = string.Empty;
                    txtApplicationDescription.Text = string.Empty;
                    txtApplicationURL.Text = string.Empty;
                    txtDeployLocation.Text = string.Empty;
                    txtPrimaryDBName.Text = string.Empty;
                    txtDBCode.Text = string.Empty;
                }
                else if (retVal == 2)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "N", string.Empty, Constants.Activity_Create_Application,
                        Constants.Activity_Create_Application, Constants.Activity_Failed);

                    //ERR1017 - Application already exist.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1017");
                }
                else //if (retVal == 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "N", string.Empty, Constants.Activity_Create_Application,
                        Constants.Activity_Create_Application, Constants.Activity_Failed);

                    //ERR1018 - Application could not created.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1018");
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

        #endregion CreateApplications    

        #region GetApplicationFullDetailForUpdate

        private void GetApplicationFullDetailForUpdate()
        {
            Hashtable htEditApplication = new Hashtable();
            try
            {
                objManageUser.ApplicationCode = Request.QueryString["edocnoitacilppa"].Replace(" ", "+").Trim();
                objManageUser.ApplicationCode = CryptoUtil.DecryptTripleDES(objManageUser.ApplicationCode.Replace(" ", "+"));

                htEditApplication = objManageUser.GetApplicationFullDetailForUpdate();

                if (htEditApplication.Count > 0)
                {
                    txtApplicationCode.Text = Convert.ToString(htEditApplication["ApplicationCode"]);
                    txtApplicationName.Text = Convert.ToString(htEditApplication["AppName"]);
                    txtApplicationDescription.Text = Convert.ToString(htEditApplication["AppDesc"]);
                    txtDeployLocation.Text = Convert.ToString(htEditApplication["DeployLocation"]);
                    ddlApplicationType.SelectedValue = Convert.ToString(htEditApplication["AppType"]).Trim();
                    txtApplicationURL.Text = Convert.ToString(htEditApplication["AppURL"]);
                    //txtPrgCode.Text = Convert.ToString(htEditApplication["PrgCode"]);
                    txtDBCode.Text = Convert.ToString(htEditApplication["DBCode"]);
                    txtPrimaryDBName.Text = Convert.ToString(htEditApplication["PrimaryDBName"]);
                    ddlActiveStatus.SelectedValue = Convert.ToString(htEditApplication["Active_YN"]);
                    txtApplicationCode.Enabled = false;
                }
                else
                {
                    //MSG1001 - No record(s) found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
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

        #endregion GetApplicationFullDetailForUpdate

        #region UpdateApplicationDetail

        private void UpdateApplicationDetail()
        {
            try
            {
                objManageUser.ApplicationCode = Request.QueryString["edocnoitacilppa"].Replace(" ", "+").Trim();
                objManageUser.ApplicationCode = CryptoUtil.DecryptTripleDES(objManageUser.ApplicationCode.Replace(" ", "+"));
                objManageUser.AuditLog.LoginId = AppSessions.SessionLoginId;
                objManageUser.ApplicationName = txtApplicationName.Text.Trim();
                objManageUser.ApplicationDesc = txtApplicationDescription.Text.Trim();
                objManageUser.DeployLocation = txtDeployLocation.Text.Trim();
                objManageUser.ApplicationType = ddlApplicationType.SelectedValue;
                objManageUser.ApplicationURL = txtApplicationURL.Text.Trim();
                objManageUser.Active_YN = (ddlActiveStatus.SelectedValue == "0") ? "Y" : ddlActiveStatus.SelectedValue;
                objManageUser.DBCode = txtDBCode.Text.Trim();
                objManageUser.PrgCode = Constants.PrgCode_ManageApplication;
                objManageUser.PrimaryDBName = txtPrimaryDBName.Text.Trim();

                int retVal = objManageUser.UpdateApplicationDetail();

                if (retVal > 0)
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "Y", string.Empty, Constants.Activity_Update_Application,
                        Constants.Activity_Update_Application, Constants.Activity_Success);

                    //INF1014 - Application Updated Successfully.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "INF1014");
                }
                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_ManageApplication, "N", string.Empty, Constants.Activity_Update_Application,
                        Constants.Activity_Update_Application, Constants.Activity_Failed);

                    //ERR1019 - Application could not updated.
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1019");
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

        #endregion UpdateApplicationDetail

        protected void btnSaveApplication_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocnoitacilppa"], null)) &&
                       Request.QueryString["edocnoitacilppa"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    objManageUser.ApplicationCode = Request.QueryString["edocnoitacilppa"].Replace(" ", "+").Trim();
                    objManageUser.ApplicationCode = CryptoUtil.DecryptTripleDES(objManageUser.ApplicationCode.Replace(" ", "+"));
                    UpdateApplicationDetail();
                }
                else
                {
                    CreateApplications();
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

        #region GetApplicationTypeCode

        private void GetApplicationTypeCode()
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

        #endregion GetApplicationTypeCode

        #region ClearAll

        private void ClearAll()
        {
            try
            {
                Common.ClearFields(this, "0");
                txtApplicationCode.Text = string.Empty;
                txtApplicationName.Text = string.Empty;
                txtApplicationDescription.Text = string.Empty;
                txtApplicationURL.Text = string.Empty;
                txtDeployLocation.Text = string.Empty;
                txtPrimaryDBName.Text = string.Empty;
                txtDBCode.Text = string.Empty;
                spnMessage.InnerHtml = string.Empty;
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

        #region GetValidationMessage
        /// <summary>
        /// Method for validation message(s).
        /// </summary>
        private void GetValidationMessage()
        {
            try
            {
                valSummary.HeaderText = PortalCommon.GetValidationImage(Common.GetMessageFromXMLFile("VAL1013"));//Validation Error(s)...
                valReqApplicationCode.ErrorMessage = Common.GetMessageFromXMLFile("VAL1031");
                valReqApplicationName.ErrorMessage = Common.GetMessageFromXMLFile("VAL1032");
                valReqApplicationType.ErrorMessage = Common.GetMessageFromXMLFile("VAL1024");
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

        protected void btnClearApplication_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocnoitacilppa"], null)) &&
                       Request.QueryString["edocnoitacilppa"].Trim().Length > 0)
                {
                    //If coming from some other page.. redierct to that same page.
                    //objManageUser.ApplicationCode = Request.QueryString["edocnoitacilppa"].Replace(" ", "+").Trim();
                    //objManageUser.ApplicationCode = CryptoUtil.DecryptTripleDES(objManageUser.ApplicationCode.Replace(" ", "+"));

                    Server.Transfer("../" + Constants.Path_ManageApplication, false);

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

        #region GetActive_YNList

        private void GetActive_YNList()
        {
            BLCommon objCommon = new BLCommon();

            try
            {
                objCommon.LovCode = Constants.Active_YN;//"ACT";
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
