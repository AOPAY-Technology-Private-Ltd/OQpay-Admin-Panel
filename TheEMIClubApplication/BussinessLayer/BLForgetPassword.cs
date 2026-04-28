using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLForgetPassword
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        public string emailid { get; set; }
        public string Newpassword { get; set; }
        public string Username { get; set; }
        public string Companyname { get; set; }



        public (string ReturnCode, string ReturnMsg) AdminForgatPassword()
        {
            try
            {

                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@Email", emailid);
                m_sqlParam[1] = new SqlParameter("@Username", Username);
                m_sqlParam[2] = new SqlParameter("@HashPass", Newpassword);
                m_sqlParam[3] = new SqlParameter("@ReturnCode", SqlDbType.VarChar, 10);
                m_sqlParam[3].Direction = ParameterDirection.Output;
                m_sqlParam[4] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[4].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_Adminforgatepassword", m_sqlParam);
                string returnCode = Convert.ToString(m_sqlParam[3].Value);
                string returnMsg = Convert.ToString(m_sqlParam[4].Value);
                return (returnCode, returnMsg);
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