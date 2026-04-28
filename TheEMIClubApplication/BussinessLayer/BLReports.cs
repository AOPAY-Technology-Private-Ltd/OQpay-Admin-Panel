using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Web.UI.WebControls;
using System.Web.Util;
//using Telerik.Reporting.Services.Engine;
namespace TheEMIClubApplication.BussinessLayer
{
    public class BLReports
    {

        #region Global Variables

        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;

        #endregion 

        #region Member Variables


        private DateTime? m_fromDate = BLCommon.DATETIME_NULL;
        private DateTime? m_toDate = BLCommon.DATETIME_NULL_REPORTTO;

        private string m_flag = string.Empty;
        private string m_flag1 = string.Empty;
        private string m_reportFlag = string.Empty;

        private string m_amount_type ;
        private string m_coustmer_v_id;
        private string m_parmUserName;
        private string m_TransIpAddress;

        private int m_rid;


      



        //Containership - Like Inheritence
        private BLAuditLog m_objAuditLog = new BLAuditLog();

        #endregion Member Variables

        #region Properties

        public BLAuditLog AuditLog { get { return m_objAuditLog; } set { m_objAuditLog = value; } }

        public DateTime? FromDate { get { return m_fromDate; } set { m_fromDate = value; } }
        public DateTime? ToDate { get { return m_toDate; } set { m_toDate = value; } }

        public string Flag { get { return m_flag; } set { m_flag = value ; } }
        public string Flag1 { get { return m_flag1; } set { m_flag1 = value; } }
        public string ReportFlag { get { return m_reportFlag; } set { m_reportFlag = value; } }

        public string coustmer_vartual_id { get { return m_coustmer_v_id; } set { m_coustmer_v_id = value; } }

        public string amounttype { get { return m_amount_type; } set { m_amount_type = value; } }
        public string parmUserName { get { return m_parmUserName; } set { m_parmUserName = value; } }
        public string TransIpAddress { get { return m_TransIpAddress; } set { m_TransIpAddress = value; } }


        public string CustomerCode { get; set; }
        public string Retailercode { get; set; }
        public string LoanCode { get; set; }
        public string SearchMode { get; set; }
        public string ActiveStatus { get; set; }
        public string ReceiptNo { get; set; }
        public string RecordStatus { get; set; }
        public string Name { get; set; }
        public string PaymentMode { get; set; }
        public string AmountType { get; set; }
        public string TransactionNo { get; set; }
        public string StoreName { get; set; }
        public string MobileNo { get; set; }
        public string Emailid { get; set; }

        #endregion Properties

        #region Methods




        #endregion Methods



        public DataTable AllTransactionReports()
        {
            try
            {
                m_sqlParam = new SqlParameter[16];
                m_sqlParam[0] = new SqlParameter("@SearchMode", SearchMode);
                m_sqlParam[1] = new SqlParameter("@RetailerCode", Retailercode);
                m_sqlParam[2] = new SqlParameter("@CustomerCode", CustomerCode);
                m_sqlParam[3] = new SqlParameter("@LoanCode", LoanCode);
                m_sqlParam[4] = new SqlParameter("@ActiveStatus", ActiveStatus);
                m_sqlParam[5] = new SqlParameter("@ReceiptNo", ReceiptNo);
                m_sqlParam[6] = new SqlParameter("@RecordStatus", RecordStatus);
                m_sqlParam[7] = new SqlParameter("@Name", Name);
                m_sqlParam[8] = new SqlParameter("@PaymentMode", PaymentMode);
                m_sqlParam[9] = new SqlParameter("@FromDate", FromDate);
                m_sqlParam[10] = new SqlParameter("@ToDate", ToDate);
                m_sqlParam[11] = new SqlParameter("@TXNNo", TransactionNo);
                m_sqlParam[12] = new SqlParameter("@AmountType", AmountType);
                m_sqlParam[13] = new SqlParameter("@StoreName", StoreName);
                m_sqlParam[14] = new SqlParameter("@MobileNo", MobileNo);
                m_sqlParam[15] = new SqlParameter("@Emailid", Emailid);

                //m_sqlParam[4] = new SqlParameter("@parmFla2", Flag1);


                return objDBTask.ExecuteDataTable("usp_AllReports", m_sqlParam, "ReportDetails");
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

        public DataTable ShowPayoutReportDatewise()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);
                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);
                m_sqlParam[4] = new SqlParameter("@parmFla2", Flag1);

                return objDBTask.ExecuteDataTable("usp_GetTransactionReport", m_sqlParam, "ReportDetails");
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
        public DataTable ShowTransactiondatewisepayin()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);

                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);


                return objDBTask.ExecuteDataTable("usp_GetTransactionReportDatewisepayin", m_sqlParam, "ReportDetails");
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
        public DataTable ShowTransactiondatewisedispute()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);

                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);


                return objDBTask.ExecuteDataTable("usp_GetTransactionReportDatewisedispute", m_sqlParam, "ReportDetails");
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

        public DataTable ShowTransactiondatewisedrejected()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);

                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);


                return objDBTask.ExecuteDataTable("usp_GetTransactionReportDatewiseRejecte", m_sqlParam, "ReportDetails");
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


        public DataTable ShowTransactiondatewiseall()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);

                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);





                return objDBTask.ExecuteDataTable("usp_GetTransactionReportDatewiseAllTransactin", m_sqlParam, "ReportDetails");
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


        public DataTable ShowPendingApprovalrequest()
        {
            try
            {

                m_sqlParam = new SqlParameter[2];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);



                return objDBTask.ExecuteDataTable("usp_getPendingAprovalReport", m_sqlParam, "ReportDetails");
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


        public DataTable ShowPendingApprovalrequestPayout()
        {
            try
            {

                m_sqlParam = new SqlParameter[2];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);



                return objDBTask.ExecuteDataTable("usp_getPendingAprovalReportPayout", m_sqlParam, "ReportDetails");
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

        //product master  services bind

        public DataTable ShowProductServicesmaster()
        {
            try
            {

                m_sqlParam = new SqlParameter[0];
                return objDBTask.ExecuteDataTable("usp_getServicesproductmaster", m_sqlParam, "");
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


        public short updatedispute()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@customer_virtual_address", coustmer_vartual_id);
                m_sqlParam[1] = new SqlParameter("@ParamUser", parmUserName);
                m_sqlParam[2] = new SqlParameter("@Amount_Type", amounttype);
              
              

                m_sqlParam[3] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[UpdateDisputeEntry]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[3].Value);

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
        //approved  or recjeted by admin

        public short DesputeentryApprovedRejectedadmin()
        {
            try
            {

                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@customer_virtual_address", coustmer_vartual_id);
                m_sqlParam[1] = new SqlParameter("@ParamUser", parmUserName);
                m_sqlParam[2] = new SqlParameter("@IPAddress",TransIpAddress);
                m_sqlParam[3] = new SqlParameter("@flag",Flag);



                m_sqlParam[4] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[ApprovedRejectedDisputeEntry]", m_sqlParam);

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

        //
        //Sigup report

        public DataTable ShowAllreportDatewise()
        {
            try
            {

                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@parmFromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@parmToDate", ToDate);

                m_sqlParam[2] = new SqlParameter("@parmFlag", Flag);
                m_sqlParam[3] = new SqlParameter("@parmUser", parmUserName);
                m_sqlParam[4] = new SqlParameter("@parmFla2", Flag1);


                return objDBTask.ExecuteDataTable("usp_GetAllReport", m_sqlParam, "ReportDetails");
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