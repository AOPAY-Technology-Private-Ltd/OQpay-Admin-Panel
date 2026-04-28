using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLAuditLog
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        #region Variables
        //Member declaration:  

        private string m_applicationCode = string.Empty;
        private string m_prgCode = string.Empty;
        private string m_loginId = string.Empty;
        private string m_username = string.Empty;
        private string m_userSessionId = string.Empty;
        private string m_accessDenial = string.Empty;
        private string m_logCategory = string.Empty;
        private string m_logAction = string.Empty;
        //private string m_logDateTime;
        private string m_taskModule = string.Empty;
        private string m_taskDescription = string.Empty;

        private string m_remoteHostIP = string.Empty;
        private string m_remoteHostName = string.Empty;
        private string m_localHostIP = string.Empty;
        private string m_localHostName = string.Empty;

        private string m_oldValue = string.Empty;
        private string m_newValue = string.Empty;

        #endregion Variables

        //Property and methods implementation (declared in IActivityLog):
        #region Property

        public string ApplicationCode { get { return m_applicationCode; } set { m_applicationCode = value; } }
        public string ProgramCode { get { return m_prgCode; } set { m_prgCode = value; } }
        public string LoginId { get { return m_loginId; } set { m_loginId = value; } }

      
        public string UserSessionId { get { return m_userSessionId; } set { m_userSessionId = value; } }
        public string AccessDenial { get { return m_accessDenial; } set { m_accessDenial = value; } }
        public string LogCategory { get { return m_logCategory; } set { m_logCategory = value; } }
        public string LogAction { get { return m_logAction; } set { m_logAction = value; } }
        //public string LogDateTime { get { return m_logDateTime; } set { m_logDateTime = value; } }
        public string TaskModule { get { return m_taskModule; } set { m_taskModule = value; } }
        public string TaskDescription { get { return m_taskDescription; } set { m_taskDescription = value; } }

        public string RemoteHostIP { get { return m_remoteHostIP; } set { m_remoteHostIP = value; } }
        public string RemoteHostName { get { return m_remoteHostName; } set { m_remoteHostName = value; } }
        public string LocalHostIP { get { return m_localHostIP; } set { m_localHostIP = value; } }
        public string LocalHostName { get { return m_localHostName; } set { m_localHostName = value; } }

        public string OldValue { get { return m_oldValue; } set { m_oldValue = value; } }
        public string NewValue { get { return m_newValue; } set { m_newValue = value; } }

        #endregion Property

        //Method definition          
        #region Method

        /// <summary>
        /// Method to insert Activity Log.
        /// </summary>
        /// <returns></returns>
        public void InsertAuditLog()
        {
            try
            {
                m_sqlParam = new SqlParameter[15];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmProgramCode", m_prgCode);
                m_sqlParam[2] = new SqlParameter("@parmLoginId", m_loginId);
                m_sqlParam[3] = new SqlParameter("@parmUserSessionId", m_userSessionId);
                m_sqlParam[4] = new SqlParameter("@parmAccessDenial", m_accessDenial);
                m_sqlParam[5] = new SqlParameter("@parmLogCategory", m_logCategory);
                m_sqlParam[6] = new SqlParameter("@parmLogAction", m_logAction);
                m_sqlParam[7] = new SqlParameter("@parmTaskModule", m_taskModule);
                m_sqlParam[8] = new SqlParameter("@parmTaskDescription", m_taskDescription);
                m_sqlParam[9] = new SqlParameter("@parmRemoteHostIP", m_remoteHostIP);
                m_sqlParam[10] = new SqlParameter("@parmRemoteHostName", m_remoteHostName);
                m_sqlParam[11] = new SqlParameter("@parmLocalHostIP", m_localHostIP);
                m_sqlParam[12] = new SqlParameter("@parmLocalHostName", m_localHostName);
                m_sqlParam[13] = new SqlParameter("@parmOldValue", m_oldValue);
                m_sqlParam[14] = new SqlParameter("@parmNewValue", m_newValue);

                objDBTask.ExecuteUpdate("usp_InsertAuditLog", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        /// <summary>
        /// Method to to manage user-application session log.
        ///	This module will provide functionality to Admin, so that, at any point of time,
        ///	Admin can see, which user is logged in, in which application, OR at any point of time, 
        ///	how many users are logged in, in particular application....etc....
        /// </summary>
        public void ManageUserAppSessionLog()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_loginId);
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);
                m_sqlParam[2] = new SqlParameter("@parmProgramCode", m_prgCode);

                objDBTask.ExecuteUpdate("usp_ManageUserAppSessionLog", m_sqlParam);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        /// <summary>
        /// Method to search and get user application session log.
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserAppSessionLog()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmUserName", m_loginId); //Username
                m_sqlParam[1] = new SqlParameter("@parmApplicationCode", m_applicationCode);

                return objDBTask.ExecuteDataTable("usp_GetUserAppSessionLog", m_sqlParam, "UserAppSessionLog");
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        #endregion Method
    }
}