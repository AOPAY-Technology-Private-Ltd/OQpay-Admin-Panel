using System;
using System.Data;
using System.Data.SqlClient;
using AVFramework;

namespace TheEMIClubApplication.BussinessLayer
{
    public class APPMenuBase //: IMenuBase
    {
        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;

        //Member declaration:        
        private string m_loginId = string.Empty;
        private string m_applicationCode = string.Empty;

        //Property and methods implementation

        #region MenuBase Members

        public string LoginId
        {
            get
            {
                return m_loginId;
            }
            set
            {
                m_loginId = value;
            }
        }

        public string ApplicationCode
        {
            get
            {
                return m_applicationCode;
            }
            set
            {
                m_applicationCode = value;
            }
        }
        public DataSet GetMenu()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", m_loginId);
                m_sqlParam[1] = new SqlParameter("@parmAppCode", m_applicationCode);               
                return objDBTask.ExecuteDataSet("usp_GetMenu", m_sqlParam, "AVMenu");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion MenuBase Members
    }
}