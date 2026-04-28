using AVFramework;
//using BOSManagerWebApp.SOWHTMLPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLBranch
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        private string m_BranchName;
        private string m_City;
        private string m_Location;
        private string m_ActiveStatus;
        private string m_Paramuser;

        public string BranchName { get { return m_BranchName; } set { m_BranchName = value; } }
        public string City { get { return m_City; } set { m_City = value; } }
        public string Location { get { return m_Location; } set { m_Location = value; } }
        public string ActiveStatus { get { return m_ActiveStatus; } set { m_ActiveStatus = value; } }
        public string Paramuser { get { return m_Paramuser; } set { m_Paramuser = value; } }
        
        public short AddBranch()
        {
            try
            {
                m_sqlParam = new SqlParameter[6];
                m_sqlParam[0] = new SqlParameter("@BranchName", BranchName);
                m_sqlParam[1] = new SqlParameter("@City", City);
                m_sqlParam[2] = new SqlParameter("@Location", Location);
                m_sqlParam[3] = new SqlParameter("@ActiveStatus", ActiveStatus);
                m_sqlParam[4] = new SqlParameter("@Paramuser",Paramuser);


                m_sqlParam[5] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[5].Direction = ParameterDirection.Output;
             
                objDBTask.ExecuteUpdate("usp_insertBranch", m_sqlParam);
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

        public Hashtable Reterivelastuser()
        {
            try
            {
                m_sqlParam = new SqlParameter[0];



                return objDBTask.ExecuteHashtable("usp_lastuserid", m_sqlParam);
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