using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLSignup
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;        
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNo { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Ipaddress { get; set; }
        public string Signup()
        {
            try
            {
                m_sqlParam = new SqlParameter[9];
                m_sqlParam[0] = new SqlParameter("@CompanyCode", CompanyCode);
                m_sqlParam[1] = new SqlParameter("@CompanyName", CompanyName);
                m_sqlParam[2] = new SqlParameter("@FirstName", FirstName);
                m_sqlParam[3] = new SqlParameter("@LastName", LastName);
                m_sqlParam[4] = new SqlParameter("@EmailID", EmailID);
                m_sqlParam[5] = new SqlParameter("@PhoneNo", PhoneNo);
                m_sqlParam[6] = new SqlParameter("@UserID", UserID);
                m_sqlParam[7] = new SqlParameter("@Password", Password);              
                m_sqlParam[8] = new SqlParameter("@paramOut", SqlDbType.VarChar, 50);
                m_sqlParam[8].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_Signup", m_sqlParam);
                return Convert.ToString(m_sqlParam[8].Value);
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


        public string CreateUser()
        {
            try
            {
                m_sqlParam = new SqlParameter[7];
                m_sqlParam[0] = new SqlParameter("@FirstName", FirstName);
                m_sqlParam[1] = new SqlParameter("@LastName", LastName);
                m_sqlParam[2] = new SqlParameter("@EmailID", EmailID);
                m_sqlParam[3] = new SqlParameter("@PhoneNo", PhoneNo);
                m_sqlParam[4] = new SqlParameter("@Password", Password);
                m_sqlParam[5] = new SqlParameter("@IpAddress", Ipaddress);
              
                m_sqlParam[6] = new SqlParameter("@ReturnCode", SqlDbType.VarChar, 50);
                m_sqlParam[6].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_UserRegistration", m_sqlParam);
                return Convert.ToString(m_sqlParam[6].Value);
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