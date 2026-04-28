using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace TheEMIClubApplication.BussinessLayer
{
    [Serializable]
    public class BLManageUser
    {

        #region Global Variables

        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;
        string xmlDoc = string.Empty;

        #endregion

        #region Member Variables
        //private string m_loginId;
        private string m_password;
        private string m_newPassword = string.Empty;
        private string m_emailAddress;
        private string m_userFullName;
        private string m_nickName;
        private decimal m_holdingamt;
        private string m_holdingRemarks;
        private string m_userName;
        private string m_MobileNo;
        private string m_Flag;

        private string m_Assignuser;
        private string m_applicationCode;
        private string m_reports;
        private string m_roleCode;
        private string m_roleName;
        private string m_roleDesc;

        private string m_menuScope;
        private string m_menuCode;
        private string m_menuName;
        private string m_mode;
        private string m_navigateURL;
        private string m_parentMenuCode;
        private string m_toolTip;
        private string m_description;
        private string m_displayOrder;
        private string m_menuHaltCode;
        private string m_masterCode;

        private string m_applicationName;
        private string m_applicationDesc;
        private string m_deployLocation;
        private string m_applicationType;
        private string m_applicationURL;
        private string m_primaryDBName;
        private string m_dBCode;
        private string m_prgCode;

        private string m_active_YN;

        private string m_allowedDaysToViewReport;
        private string m_maxAllowedDaysToViewReport;



        private DateTime m_fromDate = BLCommon.DATETIME_NULL;
        private DateTime m_toDate = BLCommon.DATETIME_NULL;

        private string m_subscriptionFeature;



        //Containership - Like Inheritence
        private BLAuditLog m_objAuditLog = new BLAuditLog();

        #endregion MemberVariable

        #region Property

        public string SubscriptionFeature { get { return m_subscriptionFeature; } set { m_subscriptionFeature = value; } }
        public string Password { get { return m_password; } set { m_password = value; } }
        public string NewPassword { get { return m_newPassword; } set { m_newPassword = value; } }
        public string EmailAddress { get { return m_emailAddress; } set { m_emailAddress = value; } }
        public string UserFullName { get { return m_userFullName; } set { m_userFullName = value; } }
        public string NickName { get { return m_nickName; } set { m_nickName = value; } }
        public decimal holdingamt { get { return m_holdingamt; } set { m_holdingamt = value; } }
        public string holdingRemarks { get { return m_holdingRemarks; } set { m_holdingRemarks = value; } }
        public string UserName { get { return m_userName; } set { m_userName = value; } }
        public string Assignuser { get { return m_Assignuser; } set { m_Assignuser = value; } }
        public string ApplicationCode { get { return m_applicationCode; } set { m_applicationCode = value; } }
        public string UserReport { get { return m_reports; } set { m_reports = value; } }
        public string RoleCode { get { return m_roleCode; } set { m_roleCode = value; } }
        public string RoleName { get { return m_roleName; } set { m_roleName = value; } }
        public string RoleDescription { get { return m_roleDesc; } set { m_roleDesc = value; } }

        public string MenuScope { get { return m_menuScope; } set { m_menuScope = value; } }
        public string MenuCode { get { return m_menuCode; } set { m_menuCode = value; } }
        public string MenuName { get { return m_menuName; } set { m_menuName = value; } }
        public string Mode { get { return m_mode; } set { m_mode = value; } }
        public string NavigateURL { get { return m_navigateURL; } set { m_navigateURL = value; } }
        public string ParentMenuCode { get { return m_parentMenuCode; } set { m_parentMenuCode = value; } }
        public string ToolTip { get { return m_toolTip; } set { m_toolTip = value; } }
        public string Description { get { return m_description; } set { m_description = value; } }
        public string DisplayOrder { get { return m_displayOrder; } set { m_displayOrder = value; } }
        public string MenuHaltCode { get { return m_menuHaltCode; } set { m_menuHaltCode = value; } }
        public string MasterCode { get { return m_masterCode; } set { m_masterCode = value; } }

        public string ApplicationName { get { return m_applicationName; } set { m_applicationName = value; } }
        public string ApplicationDesc { get { return m_applicationDesc; } set { m_applicationDesc = value; } }
        public string DeployLocation { get { return m_deployLocation; } set { m_deployLocation = value; } }
        public string ApplicationType { get { return m_applicationType; } set { m_applicationType = value; } }
        public string ApplicationURL { get { return m_applicationURL; } set { m_applicationURL = value; } }
        public string PrimaryDBName { get { return m_primaryDBName; } set { m_primaryDBName = value; } }
        public string DBCode { get { return m_dBCode; } set { m_dBCode = value; } }
        public string PrgCode { get { return m_prgCode; } set { m_prgCode = value; } }

        public string Active_YN { get { return m_active_YN; } set { m_active_YN = value; } }

        public string AllowedDaysToViewReport { get { return m_allowedDaysToViewReport; } set { m_allowedDaysToViewReport = value; } }
        public string MaxAllowedDaysToViewReport { get { return m_maxAllowedDaysToViewReport; } set { m_maxAllowedDaysToViewReport = value; } }

        public DateTime FromDate { get { return m_fromDate; } set { m_fromDate = value; } }
        public DateTime ToDate { get { return m_toDate; } set { m_toDate = value; } }

        public BLAuditLog AuditLog { get { return m_objAuditLog; } set { m_objAuditLog = value; } }

        public string MobileNo { get { return m_MobileNo; } set { m_MobileNo = value; } }
        public string Flag { get { return m_Flag; } set { m_Flag = value; } }

        public string Name { get; set; }
        public string custCode { get; set; }
        public string Status { get; set; }

        #endregion 

        //Method definition          
        #region Method


        #region UserLogin

        /// <summary>
        /// Method for user login.
        /// </summary>
        /// <returns></returns>
        public Hashtable UserLogin()
        {
            try
            {   
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmPassword", m_password);

                return objDBTask.ExecuteHashtable("usp_AdminLogin", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        public Hashtable EMPLogin()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmPassword", m_password);

                return objDBTask.ExecuteHashtable("usp_EmpLogin", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }


        #endregion UserLogin

        #region UserAutoLogin
        /// <summary>
        /// Method for user auto login, in case of 'Preserve My Login'.
        /// </summary>
        /// <returns></returns>
        public Hashtable UserAutoLogin()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmEmailAddress", m_emailAddress);
                return objDBTask.ExecuteHashtable("usp_UserAutoLogin", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion UserAutoLogin

        #region ChangePassword

        /// <summary>
        /// Method for Change Password.
        /// </summary>
        /// <returns></returns>
        public string ChangePassword(string userType)
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmOldPassword", m_password);
                m_sqlParam[2] = new SqlParameter("@parmNewPassword", m_newPassword);
                m_sqlParam[3] = new SqlParameter("@parmUserType", userType);
                //m_sqlParam[3] = new SqlParameter("@parmProgramCode", m_objAuditLog.ProgramCode);
                m_sqlParam[4] = new SqlParameter("@parmOut", SqlDbType.VarChar, 255);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_ChangePassword", m_sqlParam);
                return Convert.ToString(m_sqlParam[4].Value);

            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        #endregion ChangePassword

        #region ForgotPassword

        /// <summary>
        /// Method to manage forgot password.
        /// </summary>
        /// <returns>Hashtable</returns>
        public Hashtable ForgotPassword()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmEmailAddress", m_emailAddress);
                m_sqlParam[2] = new SqlParameter("@parmHashPassword", m_newPassword); //Encrypted Password

                return objDBTask.ExecuteHashtable("usp_ForgotPassword", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion ForgotPassword

        #region GetEmployeeListForCreateUser

        /// <summary>
        /// Method to Bind Employee Name.
        /// </summary>
        /// <returns></returns>
        public void GetEmployeeListForCreateUser(DropDownList ddlControlId)
        {
            try
            {
                objDBTask.BindDropDownList("usp_GetEmployeeList", "EmployeeName", "EmployeeCode", ddlControlId, "Select Employee");
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

        #endregion GetEmployeeListForCreateUser

        #region GetApplicationNameList

        /// <summary>
        /// Method to Bind Application Name.
        /// </summary>
        /// <returns></returns>
        public void GetApplicationNameList(CheckBoxList cblControlId)
        {
            try
            {
                objDBTask.BindCheckBoxList("usp_GetApplicationList", "ApplicationName", "ApplicationCode", cblControlId, "Select Application");
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

        #region GetSelectedAppList

        /// <summary>
        /// Method to Bind Selected Application List Name.
        /// </summary>
        /// <returns></returns>
        public void GetSelectedAppList(DropDownList ddlControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmMode", m_mode);
                objDBTask.BindDropDownList("usp_GetSelectedApplications", m_sqlParam, "ApplicationName", "ApplicationCode", ddlControlId, "Select Application");
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

        #endregion GetSelectedAppList

        #region SaveSelectedApplications

        /// <summary>
        /// Method for Insert Selected Applications.
        /// </summary>
        /// <returns></returns>
        public short SaveSelectedApplications()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[2] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[3] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_InsertSelectedApplications", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[3].Value);
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

        #endregion SaveSelectedApplications

        #region GetRoleList

        /// <summary>
        /// Method to Bind Role Name.
        /// </summary>
        /// <returns></returns>
        public void GetRoleList(CheckBoxList cblControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                objDBTask.BindCheckBoxList("usp_GetRoleList", m_sqlParam, "RoleName", "RoleCode", cblControlId, "Select Role");
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

        #endregion GetRoleList

        #region SaveSelectedRoles

        /// <summary>
        /// Method for Insert Selected Roles.
        /// </summary>
        /// <returns></returns>
        public short SaveSelectedRoles()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_InsertSelectedRoles", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion SaveSelectedRoles

        #region CreateRole

        /// <summary>
        /// Method for Create Role.
        /// </summary>
        /// <returns></returns>
        public short CreateRole()
        {
            try
            {
                m_sqlParam = new SqlParameter[7];
                m_sqlParam[0] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmRoleName", m_roleName);
                m_sqlParam[2] = new SqlParameter("@parmRoleDesc", m_roleDesc);
                m_sqlParam[3] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[4] = new SqlParameter("@parmMenuScope", m_menuScope);
                m_sqlParam[5] = new SqlParameter("@parmOutRoleCode", SqlDbType.Char, 5);
                m_sqlParam[5].Direction = ParameterDirection.Output;
                m_sqlParam[6] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[6].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_CreateRole", m_sqlParam);
                m_roleCode = Convert.ToString(m_sqlParam[5].Value); //@parmOutRoleCode
                return Convert.ToInt16(m_sqlParam[6].Value);

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

        #region GetMenuScope

        /// <summary>
        /// Method to Bind Menu Scope.
        /// </summary>
        /// <returns></returns>
        public void GetMenuScope(DropDownList ddlControlId)
        {
            try
            {
                objDBTask.BindDropDownList("usp_GetMenuScope", "MenuScopeName", "MenuScope", ddlControlId, "Select");
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

        #region GetUserDetail

        /// <summary>
        /// Method to get User Details.
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmFromDate", m_fromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", m_toDate);
                m_sqlParam[2] = new SqlParameter("@parmUserName", m_userName);
                return objDBTask.ExecuteDataTable("usp_GetUserDetailForManageUser", m_sqlParam, "UserDetail");
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

        #endregion GetUserDetail

        #region GetUserDetail

        /// <summary>
        /// Method to get User Details.
        /// </summary>
        /// <returns></returns>
        public DataTable EditUserDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@Name", UserName);
                m_sqlParam[1] = new SqlParameter("@email", EmailAddress);
                m_sqlParam[2] = new SqlParameter("@Phone", MobileNo);
                m_sqlParam[3] = new SqlParameter("@flag",Flag );
                return objDBTask.ExecuteDataTable("usp_GetUserListforuser", m_sqlParam, "UserDetail");
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

        #endregion GetUserDetail


        #region GetUserDetail

        /// <summary>
        /// Method to get User Details.
        /// </summary>
        /// <returns></returns>
        public Hashtable EditUserDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@Name", UserName);
                m_sqlParam[1] = new SqlParameter("@email", EmailAddress);
                m_sqlParam[2] = new SqlParameter("@Phone", MobileNo);
                m_sqlParam[3] = new SqlParameter("@flag", Flag);
                return objDBTask.ExecuteHashtable("usp_GetUserListforuser", m_sqlParam);
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

        #endregion GetUserDetail

        #region UpdateUserActiveOrInactive

        /// <summary>
        /// Method for Update User (Active/Inactive).
        /// </summary>
        /// <returns></returns>
        public short UpdateUserActiveOrInactive()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateUserActiveOrInactive", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #region GetSelectedAppForManageUser

        /// <summary>
        /// Method to get Selected Applications.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectedAppForManageUser()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                return objDBTask.ExecuteDataTable("usp_GetSelectedAppForManageUser", m_sqlParam, "AppDetail");
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

        #endregion GetSelectedAppForManageUser

        #region UpdateSelectedApplications

        /// <summary>
        /// Method for Update Selected Applications.
        /// </summary>
        /// <returns></returns>
        public short UpdateSelectedApplications()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmAppCodes", m_applicationCode);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateUserAppLink", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateSelectedApplications

        #region UpdateSelectedReport

        /// <summary>
        /// Method for Update Selected Applications.
        /// </summary>
        /// <returns></returns>
        public short UpdateSelectedReport()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmReport", m_reports);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateUserReportLink", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateSelectedApplications

        #region GetSelectedRoleForManageUser

        /// <summary>
        /// Method to get Selected Roles.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectedRoleForManageUser()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                return objDBTask.ExecuteDataTable("usp_GetSelectedRoleForManageUser", m_sqlParam, "RoleDetail");
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

        #endregion GetSelectedRoleForManageUser

        #region UpdateSelectedRoles

        /// <summary>
        /// Method for Update Selected Roles.
        /// </summary>
        /// <returns></returns>
        public short UpdateSelectedRoles()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[2] = new SqlParameter("@parmRoleCodes", m_roleCode);
                m_sqlParam[3] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateUserRoleLink", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[3].Value);
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

        #endregion UpdateSelectedRoles

        #region GetMenuListByApp

        /// <summary>
        /// Method to Bind Menu Name.
        /// </summary>
        /// <returns></returns>
        public void GetMenuListByApp(CheckBoxList cblControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                objDBTask.BindCheckBoxList("usp_GetMenuByApplication", m_sqlParam, "MenuName", "MenuCode", cblControlId, "Select Menu");
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

        #region SaveSelectedMenus

        /// <summary>
        /// Method for Insert Selected Menus.
        /// </summary>
        /// <returns></returns>
        public short SaveSelectedMenus()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuCode", m_menuCode);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_InsertSelectedMenus", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #region GetRoleDetailForEdit

        /// <summary>
        /// Method to Bind Role Name and MenuScope.
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoleDetailForEdit()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmRoleName", m_roleName);
                m_sqlParam[2] = new SqlParameter("@parmStatus", m_active_YN);
                m_sqlParam[3] = new SqlParameter("@parmMenuScope", m_menuScope);
                return objDBTask.ExecuteDataTable("usp_GetRoleDetailForManageRole", m_sqlParam, "RoleMenuScopeDetail");
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

        #endregion GetRoleDetailForEdit

        #region GetRoleDetail
        /// <summary>
        /// Method to return Role Details for Edit.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetRoleDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                return objDBTask.ExecuteHashtable("usp_GetRoleDetailForUpdate", m_sqlParam);
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

        #region UpdateRoleActiveOrInactive

        /// <summary>
        /// Method for Update Role (Active/Inactive).
        /// </summary>
        /// <returns></returns>
        public short UpdateRoleActiveOrInactive()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[1] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateRoleActiveOrInactive", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateRoleActiveOrInactive

        #region GetMenuDetail
        /// <summary>
        /// Method to return Menu Details for Edit.
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                return objDBTask.ExecuteDataTable("usp_GetMenuDetailsForUpdate", m_sqlParam, "MenuDetails");
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

        #endregion GetMenuDetail

        #region GetParentMenuList

        /// <summary>
        /// Method to Bind Parent Menu Name List.
        /// </summary>
        /// <returns></returns>
        public void GetParentMenuList(DropDownList ddlControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuCode", m_menuCode);
                m_sqlParam[2] = new SqlParameter("@parmMode", m_mode);
                objDBTask.BindDropDownList("usp_GetParentMenu", m_sqlParam, "MenuName", "MenuCode", ddlControlId, "Select Menu");
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

        #region GetDisplayOrder
        /// <summary>
        /// Method to get Display Order of the Menu.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetDisplayOrder()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                return objDBTask.ExecuteHashtable("usp_GetDisplayOrder", m_sqlParam);
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

        #region GetMenuHaltCodeList

        /// <summary>
        /// Method to Bind Menu Halt Name List.
        /// </summary>
        /// <returns></returns>
        public void GetMenuHaltCodeList(DropDownList ddlControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmMasterCode", m_masterCode);
                objDBTask.BindDropDownList("usp_GetMasterCodeList", m_sqlParam, "CodeName", "ValueCode", ddlControlId, "Select");
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

        /// <summary>
        /// Method for Create Menu.
        /// </summary>
        /// <returns></returns>
        public short CreateMenu()
        {
            try
            {
                m_sqlParam = new SqlParameter[11];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuName", m_menuName);
                m_sqlParam[2] = new SqlParameter("@parmNavigateURL", m_navigateURL);
                m_sqlParam[3] = new SqlParameter("@parmParentMenuCode", m_parentMenuCode);
                m_sqlParam[4] = new SqlParameter("@parmToolTip", m_toolTip);
                m_sqlParam[5] = new SqlParameter("@parmDescription", m_description);
                m_sqlParam[6] = new SqlParameter("@parmDisplayOrder", m_displayOrder);
                //m_sqlParam[7] = new SqlParameter("@parmMenuHaltCode", m_menuHaltCode);
                m_sqlParam[7] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[8] = new SqlParameter("@parmFeatureCode", m_subscriptionFeature);
                m_sqlParam[9] = new SqlParameter("@parmOutMenuCode", SqlDbType.Char, 6);
                m_sqlParam[9].Direction = ParameterDirection.Output;
                m_sqlParam[10] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[10].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_CreateMenu", m_sqlParam);
                m_menuCode = Convert.ToString(m_sqlParam[9].Value); //@parmOutMenuCode
                return Convert.ToInt16(m_sqlParam[10].Value);

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

        #region GetMenuDetailForEdit

        /// <summary>
        /// Method to Bind Menu Detail.
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuDetailForEdit()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuName", m_menuName);
                m_sqlParam[2] = new SqlParameter("@parmStatus", m_active_YN);
                m_sqlParam[3] = new SqlParameter("@parmMenuHaltCode", m_menuHaltCode);
                m_sqlParam[4] = new SqlParameter("@parmNavigateURL", m_navigateURL);
                return objDBTask.ExecuteDataTable("usp_GetMenuList", m_sqlParam, "MenuDetail");
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

        #endregion GetMenuDetailForEdit

        #region GetMenuDetailForEditMode
        /// <summary>
        /// Method to return Menu Details for Edit.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetMenuDetailForEditMode()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmMenuCode", m_menuCode);
                return objDBTask.ExecuteHashtable("usp_GetMenuDetailForEditMode", m_sqlParam);
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

        #region UpdateMenuActiveOrInactive

        /// <summary>
        /// Method for Update Menu (Active/Inactive).
        /// </summary>
        /// <returns></returns>
        public short UpdateMenuActiveOrInactive()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmMenuCode", m_menuCode);
                m_sqlParam[1] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateMenuActiveOrInactive", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateMenuActiveOrInactive

        #region UpdateMenu

        /// <summary>
        /// Method for Update Menu.
        /// </summary>
        /// <returns></returns>
        public short UpdateMenu()
        {
            try
            {
                m_sqlParam = new SqlParameter[13];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuCode", m_menuCode);
                m_sqlParam[2] = new SqlParameter("@parmMenuName", m_menuName);
                m_sqlParam[3] = new SqlParameter("@parmNavigateURL", m_navigateURL);
                m_sqlParam[4] = new SqlParameter("@parmParentMenuCode", m_parentMenuCode);
                m_sqlParam[5] = new SqlParameter("@parmToolTip", m_toolTip);
                m_sqlParam[6] = new SqlParameter("@parmDescription", m_description);
                m_sqlParam[7] = new SqlParameter("@parmDisplayOrder", m_displayOrder);
                m_sqlParam[8] = new SqlParameter("@parmMenuHaltCode", m_menuHaltCode);
                m_sqlParam[9] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[10] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[11] = new SqlParameter("@parmFeatureCode", m_subscriptionFeature);


                m_sqlParam[12] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[12].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateMenu", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[12].Value);

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

        #region UpdateRoleDetail

        /// <summary>
        /// Method for Update Selected Roles.
        /// </summary>
        /// <returns></returns>
        public short UpdateRoleDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[6];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[2] = new SqlParameter("@parmRoleName", m_roleName);
                m_sqlParam[3] = new SqlParameter("@parmRoleDesc", m_roleDesc);
                m_sqlParam[4] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[5] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[5].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateRoleDetails", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[5].Value);
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

        #region UpdateMenuDetail

        /// <summary>
        /// Method for Update Selected Menus.
        /// </summary>
        /// <returns></returns>
        public short UpdateMenuDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmRoleCode", m_roleCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuCodes", m_menuCode);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateRoleMenuLink", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateMenuDetail

        #region CreateApplication

        /// <summary>
        /// Method for Create Application.
        /// </summary>
        /// <returns></returns>
        public short CreateApplication()
        {
            try
            {
                m_sqlParam = new SqlParameter[11];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[2] = new SqlParameter("@parmApplicationName", m_applicationName);
                m_sqlParam[3] = new SqlParameter("@parmApplicationDesc", m_applicationDesc);
                m_sqlParam[4] = new SqlParameter("@parmDeployLocation", m_deployLocation);
                m_sqlParam[5] = new SqlParameter("@parmApplicationType", m_applicationType);
                m_sqlParam[6] = new SqlParameter("@parmApplicationURL", m_applicationURL);
                m_sqlParam[7] = new SqlParameter("@parmPrgCode", m_prgCode);
                m_sqlParam[8] = new SqlParameter("@parmPrimaryDBName", m_primaryDBName);
                m_sqlParam[9] = new SqlParameter("@parmDBCode", m_dBCode);
                m_sqlParam[10] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[10].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_CreateApplication", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[10].Value);

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

        #endregion CreateApplication

        #region GetApplicationDetailForEdit

        /// <summary>
        /// Method to Bind Application Name Detail.
        /// </summary>
        /// <returns></returns>
        public DataTable GetApplicationDetailForEdit()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                //m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[0] = new SqlParameter("@parmApplicationName", m_applicationName);
                m_sqlParam[1] = new SqlParameter("@parmStatus", m_active_YN);
                m_sqlParam[2] = new SqlParameter("@parmApplicationType", m_applicationType);
                return objDBTask.ExecuteDataTable("usp_GetApplicationListForEdit", m_sqlParam, "ApplicationsDetail");
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

        #endregion GetRoleDetailForEdit

        #region GetApplicationFullDetailForUpdate
        /// <summary>
        /// Method to return Application Details for update.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetApplicationFullDetailForUpdate()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                return objDBTask.ExecuteHashtable("usp_GetApplicationFullDetailForUpdate", m_sqlParam);
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

        #region UpdateApplicationActiveOrInactive

        /// <summary>
        /// Method for Update Application (Active/Inactive).
        /// </summary>
        /// <returns></returns>
        public short UpdateApplicationActiveOrInactive()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateApplicationActiveOrInactive", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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

        #endregion UpdateApplicationActiveOrInactive

        #region 


        /// <summary>
        /// Method for Update MstApplication_AMS.
        /// </summary>
        /// <returns></returns>
        public short UpdateApplicationDetail()
        {
            try
            {
                m_sqlParam = new SqlParameter[11];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmApplicationName", m_applicationName);
                m_sqlParam[2] = new SqlParameter("@parmApplicationDesc", m_applicationDesc);
                m_sqlParam[3] = new SqlParameter("@parmDeployLocation", m_deployLocation);
                m_sqlParam[4] = new SqlParameter("@parmApplicationType", m_applicationType);
                m_sqlParam[5] = new SqlParameter("@parmApplicationURL", m_applicationURL);
                m_sqlParam[6] = new SqlParameter("@parmPrgCode", m_prgCode);
                m_sqlParam[7] = new SqlParameter("@parmActive_YN", m_active_YN);
                m_sqlParam[8] = new SqlParameter("@parmPrimaryDBName", m_primaryDBName);
                m_sqlParam[9] = new SqlParameter("@parmDBCode", m_dBCode);
                m_sqlParam[10] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[10].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateApplicationFullDetails", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[10].Value);
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

        #region ResetPassword

        /// <summary>
        /// Method to manage reset password.
        /// </summary>
        /// <returns>Hashtable</returns>
        public Hashtable ResetPassword()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_userName);
                m_sqlParam[1] = new SqlParameter("@parmHashPassword", m_newPassword); //New Encrypted Password

                //Call to SP 'usp_ResetPassword' of dbAppMgmt.
                return objDBTask.ExecuteHashtable("usp_ResetPassword", m_sqlParam);
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
        #endregion ResetPassword

        #region UpdateUserDetails

        /// <summary>
        /// Method to update user details.
        /// </summary>
        /// <returns>a short value - int16.</returns>
        public short UpdateUserDetails()
        {
            try
            {
                xmlDoc = Common.GenerateXML(this);

                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmXmlDoc", xmlDoc);
                m_sqlParam[1] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[1].Direction = ParameterDirection.Output;
                m_sqlParam[2] = new SqlParameter("@parmOutVal", SqlDbType.Int);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateUserDetails", m_sqlParam);
                m_maxAllowedDaysToViewReport = Convert.ToString(m_sqlParam[2].Value);
                return Convert.ToInt16(m_sqlParam[1].Value);

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

        #endregion UpdateUserDetails

        #region GetUserDetailInEditMode

        /// <summary>
        /// Method to get User Details like NickName, Allowed days to view report.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetUserDetailInEditMode()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                return objDBTask.ExecuteHashtable("usp_GetUserDetailInEditMode", m_sqlParam);
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

        #endregion GetUserDetailInEditMode

        #region CreateUser

        /// <summary>
        /// Method for Create User.
        /// </summary>
        /// <returns></returns>
        public short CreateUser()
        {
            try
            {
                m_sqlParam = new SqlParameter[6];
                m_sqlParam[0] = new SqlParameter("@parmEUser", m_objAuditLog.LoginId);
                m_sqlParam[1] = new SqlParameter("@parmUserName", m_userName);
                m_sqlParam[2] = new SqlParameter("@parmPassword", m_password);
                m_sqlParam[3] = new SqlParameter("@parmNickName", m_nickName);
                m_sqlParam[4] = new SqlParameter("@parmProgramCode", m_prgCode);
                m_sqlParam[5] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[5].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_CreateUser", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[5].Value);

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

        #endregion CreateUser

        #region GetEmailAddress

        /// <summary>
        /// Method to get Email Address.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetEmailAddress()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmEmpCode", m_userName);
                return objDBTask.ExecuteHashtable("usp_GetEmailAddress", m_sqlParam);
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

        internal void GetMenuScope(RadComboBox ddlMenuScope)
        {
            throw new NotImplementedException();
        }

        #endregion GetEmailAddress

        #endregion Method       

        #region GetSelectedReportForManageUser

        /// <summary>
        /// Method to get Selected Applications.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectedReportForManageUser()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_userName);
                return objDBTask.ExecuteDataTable("usp_GetSelectedReportForManageUser", m_sqlParam, " ReportDetail");
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

        #endregion GetSelectedReportForManageUser

        // Reterive Recent Application Loan Details
        #region GetApplicationDetailForEdit

        /// <summary>
        /// Method to Bind Application Name Detail.
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecentLoanDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                //m_sqlParam[0] = new SqlParameter("@Assignuser",Assignuser );
                //m_sqlParam[1] = new SqlParameter("@flag", Flag);
                m_sqlParam[0] = new SqlParameter("@flag", Flag);
                m_sqlParam[1] = new SqlParameter("@parmMobileNo", MobileNo);
                m_sqlParam[2] = new SqlParameter("@parmUserName", Name);
                m_sqlParam[3] = new SqlParameter("@parmLoginId", custCode);
                m_sqlParam[4] = new SqlParameter("@parmStatus", Status);

                return objDBTask.ExecuteDataTable("usp_GetPendingCustomer", m_sqlParam, "RecentApplication");
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

        #endregion GetRoleDetailForEdit

        #region GetApplicationDetailForEdit

        /// <summary>
        /// Method to Bind Application Name Detail.
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecentLoanDetailsforassign()
        {
            try
            {
                m_sqlParam = new SqlParameter[0];
          

                return objDBTask.ExecuteDataTable("usp_getapplicationforAssign", m_sqlParam, "RecentApplication");
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

        #endregion GetRoleDetailForEdit

        public Hashtable CountTotal()
        {
            try
            {
                m_sqlParam = new SqlParameter[0];


                return objDBTask.ExecuteHashtable("usp_count", m_sqlParam);
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



    }
}