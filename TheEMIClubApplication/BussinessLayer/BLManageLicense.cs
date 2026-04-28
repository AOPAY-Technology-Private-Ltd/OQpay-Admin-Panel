using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Collections;
using System.Web.Configuration;
using System.Configuration;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLManageLicense
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        public int ID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int Copies{ get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ActiveYN { get; set; }
        public DateTime LastUpdate { get; set; }
        public string USERID { get; set; }


        public string UpdateActivateStatus()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@parmID",ID);                
                m_sqlParam[1] = new SqlParameter("@parmStatus", ActiveYN);
                m_sqlParam[2] = new SqlParameter("@parmUserID", USERID);
                m_sqlParam[3] = new SqlParameter("@paramOut", SqlDbType.VarChar, 50);
                m_sqlParam[3].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_UpdateLicense", m_sqlParam);
                return Convert.ToString(m_sqlParam[3].Value);
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