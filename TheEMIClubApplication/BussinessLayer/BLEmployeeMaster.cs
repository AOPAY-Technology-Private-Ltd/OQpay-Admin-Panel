using AVFramework;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static QRCoder.PayloadGenerator;
using System.Web.UI.WebControls;
using Twilio;
using iTextSharp.text.pdf;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLEmployeeMaster
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        public int empid { get; set; }
        public string Task { get; set; }
        public string codeType { get; set; }
        public string Empcode { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string MobileNo { get; set; }
        public string Emailid { get; set; }
        public string EmpPassword { get; set; }
        public string EmpAddress { get; set; }
        public string EmpAadharno { get; set; }
        public string EmpPanNo { get; set; }
        public string ActiveStatus { get; set; }
        public string Createby { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Username { get; set; }

        public int ReturnCode { get; set; }            
        public string ReturnMsg { get; set; }

        public DataTable CreateEmployee()
        {
            try
            {
                m_sqlParam = new SqlParameter[13];

                //m_sqlParam[0] = new SqlParameter("@Id", empid);
                m_sqlParam[0] = new SqlParameter("@Mode", Task);
                m_sqlParam[1] = new SqlParameter("@FirstName", EmpFirstName);
                m_sqlParam[2] = new SqlParameter("@LastName", EmpLastName);
                m_sqlParam[3] = new SqlParameter("@MobileNo", MobileNo);
                m_sqlParam[4] = new SqlParameter("@EmailID", Emailid);
                m_sqlParam[5] = new SqlParameter("@CustPassword", EmpPassword);
                m_sqlParam[6] = new SqlParameter("@Address", EmpAddress);
                m_sqlParam[7] = new SqlParameter("@AadharNumber", EmpAadharno);
                m_sqlParam[8] = new SqlParameter("@PANNumber", EmpPanNo);
                m_sqlParam[9] = new SqlParameter("@CreatedBy", Username);
                m_sqlParam[10] = new SqlParameter("@Id", empid);
                m_sqlParam[11] = new SqlParameter("@CustomerCode", Empcode);

                m_sqlParam[12] = new SqlParameter("@Status", ActiveStatus);

                // Output parameters
                //m_sqlParam[8] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                //m_sqlParam[8].Direction = ParameterDirection.Output;
                //m_sqlParam[9] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                //m_sqlParam[9].Direction = ParameterDirection.Output;


                //// Retrieve and return output
                //ReturnCode = Convert.ToInt16(m_sqlParam[8].Value);
                //ReturnMsg = Convert.ToString(m_sqlParam[9].Value);
                return objDBTask.ExecuteDataTable("usp_QOFinance_Employee", m_sqlParam, "Registration");


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