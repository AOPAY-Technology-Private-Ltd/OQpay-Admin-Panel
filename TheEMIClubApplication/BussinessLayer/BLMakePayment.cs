using AVFramework;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLMakePayment
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;
        public string Mode { get; set; }
        public int RID { get; set; }
        public string Customercode { get; set; }
        public string Cutomertype { get; set; }
        public string Loancode { get; set; }
        public string NoofPaidEMI { get; set; }
        public string ReceiptimagePath { get; set; }
        public string Activestatus { get; set; }
        public string Recordstatus { get; set; }
        public decimal Paidamount { get; set; }

        public string remarks { get; set; }



        //public DataTable ApproveMakePayment()
        //{
        //    try
        //    {
        //        SqlParameter[] m_sqlParam = new SqlParameter[7];
        //        m_sqlParam[0] = new SqlParameter("@Mode", Mode);
        //        m_sqlParam[1] = new SqlParameter("@RID", RID);
        //        m_sqlParam[2] = new SqlParameter("@Customercode", Customercode);
        //        m_sqlParam[3] = new SqlParameter("@Loancode", Loancode);
        //        m_sqlParam[4] = new SqlParameter("@Recordstatus", Recordstatus);
        //        m_sqlParam[5] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
        //        m_sqlParam[5].Direction = ParameterDirection.Output;
        //        m_sqlParam[6] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
        //        m_sqlParam[6].Direction = ParameterDirection.Output;

        //        return objDBTask.ExecuteDataTable("usp_ApprovedMakePayment", m_sqlParam, "MakePaymentDetails");
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable ApproveMakePayment(out int returnCode, out string returnMsg)
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[9];
                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@NoOfPaidEMI", NoofPaidEMI);
                m_sqlParam[3] = new SqlParameter("@Customercode", Customercode);
                m_sqlParam[4] = new SqlParameter("@Loancode", Loancode);
                m_sqlParam[5] = new SqlParameter("@Recordstatus", Recordstatus);

                m_sqlParam[6] = new SqlParameter("@Remarks", remarks);
               

                m_sqlParam[7] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                m_sqlParam[7].Direction = ParameterDirection.Output;

                m_sqlParam[8] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[8].Direction = ParameterDirection.Output;

                DataTable dt = objDBTask.ExecuteDataTable("usp_ApprovedMakePaymentnew", m_sqlParam, "MakePaymentDetails");

                // Get output parameters
                returnCode = Convert.ToInt32(m_sqlParam[7].Value);
                returnMsg = m_sqlParam[8].Value.ToString();

                return dt;
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

        public DataTable ApproveMakePayment()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[8];
                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@Customercode", Customercode);
                m_sqlParam[3] = new SqlParameter("@Loancode", Loancode);
                m_sqlParam[4] = new SqlParameter("@Recordstatus", Recordstatus);
                m_sqlParam[5] = new SqlParameter("@CustomerType", Cutomertype);

                m_sqlParam[6] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                m_sqlParam[6].Direction = ParameterDirection.Output;

                m_sqlParam[7] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[7].Direction = ParameterDirection.Output;

                DataTable dt = objDBTask.ExecuteDataTable("usp_ApprovedMakePaymentnew", m_sqlParam, "MakePaymentDetails");

                return dt;
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