using AVFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf.qrcode;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLLoanApproval
    {
  
            private DBTask objDBTask = new DBTask();
            private SqlParameter[] m_sqlParam;
            public string Mode { get; set; }
            public string Customercode { get; set; }
            public string Loancode { get; set; }
            public string Dealrercode { get; set; }

            public string Name { get; set; }
            public string Loanid { get; set; }
       
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
            public string Remarks { get; set; }

            public string OnlineMode { get; set; }

            public string PaymentMode { get; set; }
            public Decimal Fine { get; set; }
            public Decimal PaidAmount { get; set; }
            public Decimal EMIAmount { get; set; }
            
            public string RetailerCode { get; set; }
            public string EmpCode { get; set; }
        public string PaidEMI { get; set; }
        public string DuesEMI { get; set; }
        public string SettlementType { get; set; }

        public decimal SettlementOrForeclosureCharg { get; set; }

        public decimal FinalPaidamt { get; set; }
        public decimal FineAmount { get; set; }

        public string TxnNo { get; set; }
        public DateTime TxnDate { get; set; }
        public string BankName { get; set; }

        public DataTable LoanSettlementEMI()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[11];

                m_sqlParam[0] = new SqlParameter("@LoanCode", Loanid);
                m_sqlParam[1] = new SqlParameter("@EMIAmount", EMIAmount);
                m_sqlParam[2] = new SqlParameter("@PaidAmount", PaidAmount);
                m_sqlParam[3] = new SqlParameter("@PaymentMode", PaymentMode);
                m_sqlParam[4] = new SqlParameter("@UTRNumber", UTRNumber);
                m_sqlParam[5] = new SqlParameter("@Remarks", Remarks);
                m_sqlParam[6] = new SqlParameter("@Customercode", Customercode);
                m_sqlParam[7] = new SqlParameter("@RetailerCode", RetailerCode);
                m_sqlParam[8] = new SqlParameter("@EmployeeCode", EmpCode);
                m_sqlParam[9] = new SqlParameter("@Fine", Fine);
               m_sqlParam[10] = new SqlParameter("@InterestAmt", InterestAmount);
              

                return objDBTask.ExecuteDataTable("usp_SettlementEMI", m_sqlParam, "LoanMaster");
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

        public DataTable GetLoanSettlementDetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[16];

                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
                m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                m_sqlParam[3] = new SqlParameter("@RetailerCode", Dealrercode);
                m_sqlParam[4] = new SqlParameter("@RecordStatus", RecordStatus);
                m_sqlParam[5] = new SqlParameter("@Remarks", Remarks);
                m_sqlParam[6] = new SqlParameter("@PaidEMI", PaidEMI);
                m_sqlParam[7] = new SqlParameter("@DuesEMI", DuesEMI);
                m_sqlParam[8] = new SqlParameter("@SettlementType", SettlementType);
                // 🔹 Newly Added Parameters
                m_sqlParam[9] = new SqlParameter("@SettlementOrForeclosureCharg", SettlementOrForeclosureCharg);
                m_sqlParam[10] = new SqlParameter("@FineAmount", FineAmount);
                m_sqlParam[11] = new SqlParameter("@PaymentMode", PaymentMode);
                m_sqlParam[12] = new SqlParameter("@TxnNo", string.IsNullOrEmpty(TxnNo) ? (object)DBNull.Value : TxnNo);
                m_sqlParam[13] = new SqlParameter("@TxnDate", TxnDate == DateTime.MinValue ? (object)DBNull.Value : TxnDate);
                m_sqlParam[14] = new SqlParameter("@BankName", string.IsNullOrEmpty(BankName) ? (object)DBNull.Value : BankName);
                m_sqlParam[15] = new SqlParameter("@FinalPaidAmount", FinalPaidamt);

                return objDBTask.ExecuteDataTable(
                    "usp_GetSettlementLoan",
                    m_sqlParam,
                    "LoanMaster"
                );
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetLoanDetails()
            {
                try
                {
                    SqlParameter[] m_sqlParam = new SqlParameter[8];

                    m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                    m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
                    m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                    m_sqlParam[3] = new SqlParameter("@RetailerCode", Dealrercode);
                    m_sqlParam[4] = new SqlParameter("@RecordStatus", RecordStatus);
                    m_sqlParam[5] = new SqlParameter("@Remarks",Remarks );
                    m_sqlParam[6] = new SqlParameter("@DisbursmentRemarks", null);
                    m_sqlParam[7] = new SqlParameter("@OnlineMode", OnlineMode);
                //m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                //m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                //m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                //m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                //m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);



                return objDBTask.ExecuteDataTable("usp_GetApprovedLoan", m_sqlParam, "LoanMaster");
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


        public DataTable LoanDisbusted()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[7];

                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
                m_sqlParam[2] = new SqlParameter("@CustomerCode", Customercode);
                m_sqlParam[3] = new SqlParameter("@RetailerCode", Dealrercode);
                m_sqlParam[4] = new SqlParameter("@RecordStatus", RecordStatus);
                m_sqlParam[5] = new SqlParameter("@Remarks",null);
                m_sqlParam[6] = new SqlParameter("@DisbursmentRemarks", Remarks);
                //m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                //m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                //m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                //m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                //m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);



                return objDBTask.ExecuteDataTable("usp_GetApprovedLoan", m_sqlParam, "LoanMaster");
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

        public DataTable GetLoanDetailsfordisburshment()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[2];

                m_sqlParam[0] = new SqlParameter("@CustomerCode ", Customercode);
                m_sqlParam[1] = new SqlParameter("@LoanCode", Loanid);
              
                //m_sqlParam[5] = new SqlParameter("@IMEINumber", IMEINumber);
                //m_sqlParam[6] = new SqlParameter("@LoanStatus", LoanStatus);
                //m_sqlParam[7] = new SqlParameter("@UTRNumber", UTRNumber);
                //m_sqlParam[8] = new SqlParameter("@PaymentActiveStatus", PaymentActiveStatus);
                //m_sqlParam[9] = new SqlParameter("@RecordStatus", RecordStatus);



                return objDBTask.ExecuteDataTable("usp_GetCustomerLoanDetailsfordisburshment", m_sqlParam, "LoanMaster");
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