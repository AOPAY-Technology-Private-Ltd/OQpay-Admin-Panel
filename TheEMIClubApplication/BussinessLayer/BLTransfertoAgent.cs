using AVFramework;
using iTextSharp.text.pdf.qrcode;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Util;
using Telerik.Web.UI.com.hisoftware.api2;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLTransfertoAgent
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        private DateTime m_TransactionDate = BLCommon.DATETIME_NULL;
        private string m_TransferFrom;
        private string m_TransferTo;
        private decimal m_TransferAmt;
        private DateTime m_RecordDateTime = BLCommon.DATETIME_NULL;
        private string m_UpdatedBy;
        private DateTime m_UpdatedOn;
        private string m_Remark;
        private string m_TransferFromMsg;
        private string m_TransferToMsg;
        private string m_Amount_Type;
        private string m_Amt_Transfer_TransID;
        private decimal m_Actual_Transaction_Amount;
        private decimal m_ProcessingFees;
        private decimal m_MembershipFees;


        private decimal m_Services_charge_Amt;

        private decimal m_Services_charge_GSTAmt;

        private decimal m_Services_charge_Without_GST;

        private string m_TransIpAddress;
        private string m_API_TransId;

        private string m_benifecrydetails;
        private string m_Mobileno;



        private string m_RefrenceID;

        private string m_PaymentMode;
        private DateTime m_PaymentDate;
        private string m_DepositBankName;

        private string m_BranchCode_ChecqueNo;

        private string m_AccountNumber;
        private string m_IfscCode;
        private string m_AccountName;

        private string m_TransactionID;

        private string m_DocumentPath;
        private string m_ApprovedBy;

        private string m_ApporvedStatus;

        private string m_RegistrationId;

        private string m_ApporveRemakrs;
        private string m_LoanID;
        private decimal M_GSTAMT;
        private decimal M_Tdsamt;
        

        private decimal M_SACOMMISSION;
        private decimal m_commissionwithoutgst;
        private decimal m_Amount;
        private decimal m_Servicescharge;
        private decimal m_Serviceschargewithoutgst;
        private decimal m_Actualtransferamt;
        private decimal m_ActualCommissionAmount;
        private string m_UpiPayRef;
        private string m_OprationType;
        private string m_parmUserName;
        private string m_paramflag;
        private string m_ref_id;

        
        private decimal M_customerCommission;
        private decimal M_commissionWithoutGST;
        private decimal m_CommissionWithGST;
        private decimal m_customerCommission_withoutGST;
        private DateTime m_fromDate = BLCommon.DATETIME_NULL;
        private DateTime m_toDate = BLCommon.DATETIME_NULL_REPORTTO;
        public string refid { get { return m_ref_id; } set { m_ref_id = value; } }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string RefrenceId { get { return m_RefrenceID; } set { m_RefrenceID = value; } }
        public string PaymentMode { get { return m_PaymentMode; } set { m_PaymentMode = value; } }
        public string DepositBankName { get { return m_DepositBankName; } set { m_DepositBankName = value; } }
        public DateTime PaymentDate { get { return m_PaymentDate; } set { m_PaymentDate = value; } }
        public string BranchCode_ChecqueNo { get { return m_BranchCode_ChecqueNo; } set { m_BranchCode_ChecqueNo = value; } }
        public string TransactionID { get { return m_TransactionID; } set { m_TransactionID = value; } }
        public string DocumentPath { get { return m_DocumentPath; } set { m_DocumentPath = value; } }
        public string ApprovedBy { get { return m_ApprovedBy; } set { m_ApprovedBy = value; } }
        public string ApporvedStatus { get { return m_ApporvedStatus; } set { m_ApporvedStatus = value; } }

        public string RegistrationId { get { return m_RegistrationId; } set { m_RegistrationId = value; } }
        public string ApporveRemakrs { get { return m_ApporveRemakrs; } set { m_ApporveRemakrs = value; } }

        public string LoanID { get { return m_LoanID; } set { m_LoanID = value; } }

        public string TransferFromMsg { get { return m_TransferFromMsg; } set { m_TransferFromMsg = value; } }
        public string TransferToMsg { get { return m_TransferToMsg; } set { m_TransferToMsg = value; } }


        public string Amount_Type { get { return m_Amount_Type; } set { m_Amount_Type = value; } }
        public string Amt_Transfer_TransID { get { return m_Amt_Transfer_TransID; } set { m_Amt_Transfer_TransID = value; } }
        public decimal Actual_Transaction_Amount { get { return m_Actual_Transaction_Amount; } set { m_Actual_Transaction_Amount = value; } }

        public decimal ProcessingFees { get { return m_ProcessingFees; } set { m_ProcessingFees = value; } }

        public decimal MembershipFees { get { return m_MembershipFees; } set { m_MembershipFees = value; } }
        public decimal commissionWithoutGST { get { return M_commissionWithoutGST; } set { M_commissionWithoutGST = value; } }

        public decimal Services_charge_Amt { get { return m_Services_charge_Amt; } set { m_Services_charge_Amt = value; } }

        public decimal Services_charge_GSTAmt { get { return m_Services_charge_GSTAmt; } set { m_Services_charge_GSTAmt = value; } }

        public decimal Services_charge_Without_GST { get { return m_Services_charge_Without_GST; } set { m_Services_charge_Without_GST = value; } }

        public string TransIpAddress { get { return m_TransIpAddress; } set { m_TransIpAddress = value; } }
        public string API_TransId { get { return m_API_TransId; } set { m_API_TransId = value; } }


        public string Remark { get { return m_Remark; } set { m_Remark = value; } }

        public DateTime TransactionDate { get { return m_TransactionDate; } set { m_TransactionDate = value; } }
        public string TransferFrom { get { return m_TransferFrom; } set { m_TransferFrom = value; } }
        public string TransferTo { get { return m_TransferTo; } set { m_TransferTo = value; } }
        public decimal TransferAmt { get { return m_TransferAmt; } set { m_TransferAmt = value; } }

        public decimal GSTAMT { get { return M_GSTAMT; } set { M_GSTAMT = value; } }
        public decimal SACOMMISSION { get { return M_SACOMMISSION; } set { M_SACOMMISSION = value; } }

        public decimal Servicescharges { get { return m_Servicescharge; } set { m_Servicescharge = value; } }

        public decimal Serviceschargeswithoutgst { get { return m_Serviceschargewithoutgst; } set { m_Serviceschargewithoutgst = value; } }


        public decimal ActualCommissionAmount { get { return m_ActualCommissionAmount; } set { m_ActualCommissionAmount = value; } }


        public decimal customerCommission { get { return M_customerCommission; } set { M_customerCommission = value; } }

        public decimal customerCommissionGST { get { return m_CommissionWithGST; } set { m_CommissionWithGST = value; } }

        public decimal customerCommissionWithoutGST { get { return m_customerCommission_withoutGST; } set { m_customerCommission_withoutGST = value; } }

   

        public decimal TdsAmt { get { return M_Tdsamt; } set { M_Tdsamt = value; } }

        public decimal Actualtransferamt { get { return m_Actualtransferamt; } set { m_Actualtransferamt = value; } }
        public string UpiPayRef { get { return m_UpiPayRef; } set { m_UpiPayRef = value; } }

        public DateTime RecordDateTime { get { return m_RecordDateTime; } set { m_RecordDateTime = value; } }
        public string UpdatedBy { get { return m_UpdatedBy; } set { m_UpdatedBy = value; } }
        public DateTime UpdatedOn { get { return m_UpdatedOn; } set { m_UpdatedOn = value; } }
        public string BenefecryDetails { get { return m_benifecrydetails; } set { m_benifecrydetails = value; } }
        public string AccountName { get { return m_AccountName; } set { m_AccountName = value; } }
        public string parmUserName { get { return m_parmUserName; } set { m_parmUserName = value; } }

        public string ParamFlag { get { return m_paramflag; } set { m_paramflag = value; } }
        public string Holdingamt { get; set; }        
        public string ApprovedStatus { get; set; }
        public void TransertoAgent()
        {
            try
            {

                m_sqlParam = new SqlParameter[28];
                m_sqlParam[0] = new SqlParameter("@registrationId", RegistrationId);
                m_sqlParam[1] = new SqlParameter("@transferFrom", TransferFrom);
                m_sqlParam[2] = new SqlParameter("@transferTo", TransferTo);
                m_sqlParam[3] = new SqlParameter("@transferAmount", TransferAmt);
                m_sqlParam[4] = new SqlParameter("@transferToMsg", TransferToMsg);
                m_sqlParam[5] = new SqlParameter("@transferFromMsg", TransferFromMsg);
                m_sqlParam[6] = new SqlParameter("@amountType", Amount_Type);
                m_sqlParam[7] = new SqlParameter("@ActualCommissionAmount",ActualCommissionAmount );
                m_sqlParam[8] = new SqlParameter("@GSTAmount", GSTAMT);
                m_sqlParam[9] = new SqlParameter("@TDSAmount", TdsAmt);
                m_sqlParam[10] = new SqlParameter("@commissionWithoutGST", commissionWithoutGST);
                m_sqlParam[11] = new SqlParameter("@remark", Remark);
                m_sqlParam[12] = new SqlParameter("@TransIpAddress", TransIpAddress);
                m_sqlParam[13] = new SqlParameter("@ServiceschargeAmount", Services_charge_Amt);
                m_sqlParam[14] = new SqlParameter("@ServiceschargeGSTAmount", Services_charge_GSTAmt);
                m_sqlParam[15] = new SqlParameter("@ServiceschargeWithoutGST", Services_charge_Without_GST);
                m_sqlParam[16] = new SqlParameter("@customerCommission",customerCommission );
                m_sqlParam[17] = new SqlParameter("@customerCommissionGST", customerCommissionGST);
                m_sqlParam[18] = new SqlParameter("@customerCommission_withoutGST", customerCommissionWithoutGST);
                m_sqlParam[19] = new SqlParameter("@Amt_Transfer_TransID", Amt_Transfer_TransID);
                m_sqlParam[20] = new SqlParameter("@HoldingAmount", Holdingamt);
                m_sqlParam[21] = new SqlParameter("@Actual_Transaction_Amount", Actual_Transaction_Amount);
                m_sqlParam[22] = new SqlParameter("@ProcessingFees", ProcessingFees );
                m_sqlParam[23] = new SqlParameter("@MembershipFees", MembershipFees);
                m_sqlParam[24] = new SqlParameter("@FlagRemarks",ApporveRemakrs);
                m_sqlParam[25] = new SqlParameter("@LoanID", LoanID);
                m_sqlParam[26] = new SqlParameter("@returnCode", SqlDbType.TinyInt);
                m_sqlParam[26].Direction = ParameterDirection.Output;
                m_sqlParam[27] = new SqlParameter("@returnMsg", SqlDbType.VarChar, 100);
                m_sqlParam[27].Direction = ParameterDirection.Output;               
                objDBTask.ExecuteUpdate("[usp_TransferAmtToAgent]", m_sqlParam);    
              
                string code = m_sqlParam[26].Value.ToString();
                string msg = m_sqlParam[27].Value.ToString();

                //objDBTask.ExecuteUpdate("[usp_TransferAmtToAgent]", m_sqlParam);

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


        public string RowCount(string parmSeriesCode, out string parmOutSeriesCode)
        {
            parmOutSeriesCode = string.Empty;  // Initialize the output parameter
            try
            {
                // Define SQL parameters array
                SqlParameter[] sqlParams = new SqlParameter[2];

                // Input parameter
                sqlParams[0] = new SqlParameter("@CodeType", SqlDbType.NVarChar);
                sqlParams[0].Value = parmSeriesCode;

                // Output parameter
                sqlParams[1] = new SqlParameter("@GeneratedCode", SqlDbType.VarChar, 100);
                sqlParams[1].Direction = ParameterDirection.Output;

                // Ensure objDBTask is initialized
                if (objDBTask == null)
                {
                    throw new InvalidOperationException("Database task object is not initialized.");
                }

                // Execute the stored procedure
                objDBTask.ExecuteUpdate("sp_GenerateCode", sqlParams);

                // Ensure output parameter has been set
                if (sqlParams[1].Value != null)
                {
                    parmOutSeriesCode = sqlParams[1].Value.ToString();
                }
                else
                {
                    throw new InvalidOperationException("Output parameter was not set.");
                }

                return parmOutSeriesCode; // Return the output code
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx; // Re-throw SQL exceptions
            }
            catch (Exception ex)
            {
                throw ex; // Re-throw general exceptions
            }
        }

        public string RowCounts(string parmSeriesCode, out int parmOutSeriesCode)
        {
            parmOutSeriesCode = 0;  // Initialize the output parameter
            try
            {
                // Define SQL parameters array
                SqlParameter[] sqlParams = new SqlParameter[2];

                // Input parameter
                sqlParams[0] = new SqlParameter("@CodeType", SqlDbType.NVarChar);
                sqlParams[0].Value = parmSeriesCode;

                // Output parameter
                sqlParams[1] = new SqlParameter("@GeneratedCode", SqlDbType.VarChar, 100);
                sqlParams[1].Direction = ParameterDirection.Output;

                // Ensure objDBTask is initialized
                if (objDBTask == null)
                {
                    throw new InvalidOperationException("Database task object is not initialized.");
                }

                // Execute the stored procedure
                objDBTask.ExecuteUpdate("sp_GenerateCode", sqlParams);

                // Get output value as string
                string generatedCode = sqlParams[1].Value?.ToString() ?? "0";

                // Try to parse to int
                int.TryParse(generatedCode, out parmOutSeriesCode);

                // Return string value of code
                return generatedCode;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx; // Re-throw SQL exceptions
            }
            catch (Exception ex)
            {
                throw ex; // Re-throw general exceptions
            }
        }


        public DataTable GetPendingPayout()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[3];

                m_sqlParam[0] = new SqlParameter("@FromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@ToDate ", ToDate);
                m_sqlParam[2] = new SqlParameter("@RegistrationId", RegistrationId);



                return objDBTask.ExecuteDataTable("usp_GetPendingPayouts", m_sqlParam, "PendingPayout");
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

        public DataTable GetPendingHoldAmt()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[3];

                m_sqlParam[0] = new SqlParameter("@FromDate", FromDate);
                m_sqlParam[1] = new SqlParameter("@ToDate ", ToDate);
                m_sqlParam[2] = new SqlParameter("@RegistrationId", RegistrationId);



                return objDBTask.ExecuteDataTable("usp_GetPendingHoldamt", m_sqlParam, "PendingHoldamt");
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


        public DataTable getCompanyholdamount()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[0];              
                DataTable dt = objDBTask.ExecuteDataTable("GetAllReserveValues", m_sqlParam, "MstCompany");
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
        public short PayoutApprovedRequest()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@RefrenceID", RefrenceId);
                m_sqlParam[1] = new SqlParameter("@ApprovedBy", ApprovedBy);
                m_sqlParam[2] = new SqlParameter("@ApprovedStatus", ApprovedStatus);
                m_sqlParam[3] = new SqlParameter("@ApproveRemarks", ApporveRemakrs);
                m_sqlParam[4] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_UpdatePaymentAndTransferApproval]", m_sqlParam);
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

        public short HoldAmtApprovedRequest()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@RefrenceID", RefrenceId);
                m_sqlParam[1] = new SqlParameter("@ApprovedBy", ApprovedBy);
                m_sqlParam[2] = new SqlParameter("@ApprovedStatus", ApprovedStatus);
                m_sqlParam[3] = new SqlParameter("@ApproveRemarks", ApporveRemakrs);
                m_sqlParam[4] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_UpdateHoldingAmtApproval]", m_sqlParam);
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


        public short DepositUpdate()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@RefrenceID", RefrenceId);            
                m_sqlParam[1] = new SqlParameter("@ApprovedStatus", ApprovedStatus);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;
                objDBTask.ExecuteUpdate("[usp_UpdateDeposit]", m_sqlParam);
                return Convert.ToInt16(m_sqlParam[2].Value);
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