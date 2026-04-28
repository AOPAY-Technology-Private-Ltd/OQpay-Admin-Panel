using AVFramework;
using iTextSharp.text.pdf;
using RestSharp;
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
    public class BLloanfollup
    {
        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;
        string xmlDoc = string.Empty;

        #region Member Variables
        //private string m_loginId;

        private DateTime m_fromDate = BLCommon.DATETIME_NULL;
        private DateTime m_toDate = BLCommon.DATETIME_NULL;
        private string m_userName;

        private string m_LoanCode;
        private string m_CustomerCode;
        private string m_ProductCode;
        private string m_FollowUpBy;
        private DateTime m_FollowUpDate;

        private DateTime? m_NextFollowUpDate;
        private string m_FollowUpStatus;

        private string m_FollowUpRemarks;
        private int m_LoanTenureNo;
        private decimal m_LoanEmiAmount;








        #endregion MemberVariable

        #region Property

        //LoanCode, CustomerCode, ProductCode, FollowUpBy, FollowUpDate, 
        //    NextFollowUpDate, FollowUpStatus, FollowUpRemarks, LoanTenureNo,LoanEmiAmount
        public string LoanCode { get { return m_LoanCode; } set { m_LoanCode = value; } }
        public string CustomerCode { get { return m_CustomerCode; } set { m_CustomerCode = value; } }
        public string ProductCode { get { return m_ProductCode; } set { m_ProductCode = value; } }
        public DateTime FromDate { get { return m_fromDate; } set { m_fromDate = value; } }
        public DateTime ToDate { get { return m_toDate; } set { m_toDate = value; } }

        public DateTime FollowUpdate { get { return m_FollowUpDate; } set { m_FollowUpDate = value; } }
        public DateTime? NextFollowUpdate { get { return m_NextFollowUpDate; } set { m_NextFollowUpDate = value; } }
        public string FollowUpStatus { get { return m_FollowUpStatus; } set { m_FollowUpStatus = value; } }
        public string FollowUpRemarks { get { return m_FollowUpRemarks; } set { m_FollowUpRemarks = value; } }
        public string FollowUpBy { get { return m_FollowUpBy; } set { m_FollowUpBy = value; } }

        public int LoanTenureNo { get { return m_LoanTenureNo; } set { m_LoanTenureNo = value; } }

        public decimal LoanEmiAmount { get { return m_LoanEmiAmount; } set { m_LoanEmiAmount = value; } }

        #endregion


        public short LoanFollowUp()
        {
            try
            {
                //    @LoanCode, @CustomerCode, @ProductCode, @FollowUpBy, GETDATE(),
                //@NextFollowUpDate, @FollowUpStatus, @FollowUpRemarks, @LoanTenureNo,@LoanEmiAmount
                m_sqlParam = new SqlParameter[10];

                m_sqlParam[0] = new SqlParameter("@LoanCode", LoanCode);

                m_sqlParam[1] = new SqlParameter("@CustomerCode", CustomerCode);
                m_sqlParam[2] = new SqlParameter("@ProductCode", ProductCode);
                m_sqlParam[3] = new SqlParameter("@FollowUpBy", FollowUpBy);
                m_sqlParam[4] = new SqlParameter("@NextFollowUpDate", NextFollowUpdate);
                m_sqlParam[5] = new SqlParameter("@FollowUpStatus", FollowUpStatus);

                m_sqlParam[6] = new SqlParameter("@FollowUpRemarks", FollowUpRemarks);
                m_sqlParam[7] = new SqlParameter("@LoanTenureNo", LoanTenureNo);
                m_sqlParam[8] = new SqlParameter("@LoanEmiAmount", LoanEmiAmount);



                m_sqlParam[9] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[9].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_AddLoanEMIFollowUp]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[9].Value);

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

        public DataTable GetloanFollowUpDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@LoanCode", LoanCode);


                return objDBTask.ExecuteDataTable("usp_GetLoanEMIFollowUpDetails", m_sqlParam, "FollowupLoan");
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
