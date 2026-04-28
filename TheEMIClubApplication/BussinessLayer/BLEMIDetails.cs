using AVFramework;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLEMIDetails
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;
        public string Mode { get; set; }

        public string Customercode { get; set; }
        public string Name { get; set; }
        public string Loanid { get; set; }
        public string Loancode { get; set; }

        public string casetype { get; set; }
        public string IMEINumber { get; set; }
        public string LoanStatus { get; set; }
        public string UTRNumber { get; set; }
        public DateTime? DueDate { get; set; }

        public Decimal PrincipalAmount { get; set; }
        public Decimal InterestAmount { get; set; }
        public Decimal TotalAmount { get; set; }

        public int GracePeriod { get; set; }

        public Decimal PenaltyPerDay { get; set; }
        public string PaymentActiveStatus { get; set; }
        public string RecordStatus { get; set; }
        public int rid { get; set; }
        public string emiRid { get; set; }

        public DataTable GetEMIDetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[11];

                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@Name", Name);
                m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                m_sqlParam[3] = new SqlParameter("@LoanRId", Loanid);
                m_sqlParam[4] = new SqlParameter("@LoanCode", Loancode);
                m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);
                m_sqlParam[10] = new SqlParameter("@EMIrid", emiRid);


                return objDBTask.ExecuteDataTable("usp_LoanEmiDetails", m_sqlParam, "LoanMaster");
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

        public DataTable GetCurrentEMIDetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[1];

                //m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                //m_sqlParam[1] = new SqlParameter("@Name", Name);
                //m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                //m_sqlParam[3] = new SqlParameter("@LoanRId", Loanid);
                m_sqlParam[0] = new SqlParameter("@LoanCode", Loancode);
                //m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                //m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                //m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                //m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                //m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);
                //m_sqlParam[10] = new SqlParameter("@EMIrid", emiRid);


                return objDBTask.ExecuteDataTable("usp_GetCurrentPendingEMIs", m_sqlParam, "LoanMaster");
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


        public DataTable GetCurrentEMIDetailscurrentoverdues()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[2];

                
                m_sqlParam[0] = new SqlParameter("@LoanCode", Loancode);
                m_sqlParam[1] = new SqlParameter("@CaseType", casetype);



                return objDBTask.ExecuteDataTable("usp_GetCurrentPendingEMI_new", m_sqlParam, "LoanMaster");
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

        public DataTable GetAssignEMIDetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[3];

                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
                m_sqlParam[2] = new SqlParameter("@Empcode", Customercode); 

              // return objDBTask.ExecuteDataTable("usp_EMIDetails", m_sqlParam, "LoanMaster");
                return objDBTask.ExecuteDataTable("usp_GetCurrentPendingEMIs", m_sqlParam, "LoanMaster");
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


        public DataTable GetDashboardData()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[1];

                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                //m_sqlParam[1] = new SqlParameter("@Name", Name);
                //m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                //m_sqlParam[3] = new SqlParameter("@LoanRId", Loanid);
                //m_sqlParam[4] = new SqlParameter("@LoanCode", Loancode);
                //m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                //m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                //m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                //m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                //m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);



                return objDBTask.ExecuteDataTable("usp_Dashboard", m_sqlParam, "MakePaymentDetails");
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