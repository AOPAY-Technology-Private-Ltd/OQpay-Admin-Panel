using AVFramework;
using iTextSharp.text.pdf.qrcode;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLAssignapplicationLoan
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        public int RID { get; set; }
        public string Loanid { get; set; }
        public string Dealercode { get; set; }
        public string Employeecode { get; set; }
        public string Employeename { get; set; }
        public string Emistatus { get; set; }


        public short AssignEmi()
        {
            try
            {

                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@EMIid", RID);
                m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
                m_sqlParam[2] = new SqlParameter("@EmiStatus", Emistatus);
                m_sqlParam[3] = new SqlParameter("@EmpCode", Employeecode);
                m_sqlParam[4] = new SqlParameter("@returnCode", SqlDbType.TinyInt);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_AssignEMIToEmployee", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[4].Value);

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