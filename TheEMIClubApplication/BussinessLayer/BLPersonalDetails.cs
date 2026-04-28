using AVFramework;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using static iTextSharp.text.pdf.PdfDiv;
using Telerik.Web.UI.ExportInfrastructure;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Telerik.Web.UI.com.hisoftware.api2;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLPersonalDetails
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;        
        public string Usercode { get; set; }
        public string stringimage { get; set; }
        public string Assigncode { get; set; }
        public string LoanStatus { get; set; }


        public string accounttype { get; set; }
        public int rid { get; set; }

        public string Telicaller { get; set; }
        public string PhysicalVari { get; set; }
        //  public string Usercode { get; set; }

        public string LoanCategory { get; set; }
        public string LoanType { get; set; }

        public DateTime EMIStartDate { get; set; }
        public DateTime EMIDepositDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal ROI { get; set; }
        public decimal ROS { get; set; }

        public decimal Processingcharge { get; set; }
        public decimal Settlementcharge { get; set; }

        public decimal Othercharge { get; set; }

        public decimal loanamt { get; set; }
        public decimal totalpaidamt { get; set; }
        public decimal InterestAmount { get; set; }

        
        public decimal TotalAmount { get; set; }
        public string EMIStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string LoanID { get; set; }
        public string InsttalmentNo { get; set; }
        public string paramuser { get; set; }

        public decimal Loandisburshmentamt { get; set; }

        public decimal Balance { get; set; }
        public decimal Payableamt { get; set; }
        public DateTime DisburshmentDate { get; set; }
        public DateTime  SettlementDate { get; set; }
        public string monthday { get; set; }

        public string MobileNo { get; set; }

        public string Flag { get; set; }
        public string Name { get; set; }

        public string accountno { get; set; }

        public string ifsccode { get; set; }
        public string bankname { get; set; }

        public string baranchname { get; set; }

     

        public string virtualid { get; set; }

        public string Loantype { get; set; }
        public int LoanDuration { get; set; }

        public string LoanCalType { get; set; }


        public string Modeofpayment { get; set; }
        public string TransactionRefId { get; set; }

        public string Remarks { get; set; }

        public string Reject_YN { get; set; }

        public string Customercode { get; set; }
        public string task { get; set; }
       






        #region GetUserDetail

        /// <summary>
        /// Method to get User Details.
        /// </summary>
        /// <returns></returns>
        public Hashtable ReterivePersonalDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@parmLoginId", LoanID);
                m_sqlParam[1] = new SqlParameter("@parmStatus", "");
                m_sqlParam[2] = new SqlParameter("@parmMobileNo", "");
                m_sqlParam[3] = new SqlParameter("@parmUserName", "");



                return objDBTask.ExecuteHashtable("usp_GetPendingCustomer", m_sqlParam);
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
        public Hashtable ReterivePersonalDetailsWithFlage()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@Flag", "Admin");
                m_sqlParam[1] = new SqlParameter("@parmLoginId", LoanID);
                m_sqlParam[2] = new SqlParameter("@parmStatus", "");
                m_sqlParam[3] = new SqlParameter("@parmMobileNo", "");
                m_sqlParam[4] = new SqlParameter("@parmUserName", "");



                return objDBTask.ExecuteHashtable("usp_GetPendingCustomer", m_sqlParam);
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

        //public Hashtable RetailerIndividualDocumentUpdate(string retailerCode, string columnName, string imagePath)
        //{
        //    try
        //    {
        //        SqlParameter[] m_sqlParam = new SqlParameter[3];
        //        m_sqlParam[0] = new SqlParameter("@RetailerCode", retailerCode);
        //        m_sqlParam[1] = new SqlParameter("@ColumnName", columnName);
        //        m_sqlParam[2] = new SqlParameter("@ImagePath", imagePath);

        //        return objDBTask.ExecuteHashtable("usp_UpdateRetailerDocument", m_sqlParam);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public Hashtable RetailerIndividualDocumentUpdate(string retailerCode, string columnName, string imagePath)
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@RetailerCode", retailerCode);
                m_sqlParam[1] = new SqlParameter("@ColumnName", columnName);
                m_sqlParam[2] = new SqlParameter("@ImagePath", imagePath);

                // Output parameters
                m_sqlParam[3] = new SqlParameter("@ReturnCode", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                m_sqlParam[4] = new SqlParameter("@ReturnMsg", SqlDbType.NVarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };

                objDBTask.ExecuteUpdate("usp_UpdateRetailerDocument", m_sqlParam);

                Hashtable result = new Hashtable
        {
            { "ReturnCode", m_sqlParam[3].Value },
            { "ReturnMsg", m_sqlParam[4].Value }
        };

                return result;
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

        public short RetailerStatusUpdate()
        {
            try
            {

                //m_sqlParam = new SqlParameter[5];

                //m_sqlParam[0] = new SqlParameter("@CustomerCode", Customercode);
                //m_sqlParam[1] = new SqlParameter("@Task", task);
                //m_sqlParam[2] = new SqlParameter("@UpdatedBy", Customercode);
                //m_sqlParam[3] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                //m_sqlParam[3].Direction = ParameterDirection.Output;
                //m_sqlParam[4] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar,255);
                //m_sqlParam[4].Direction = ParameterDirection.Output;

                //objDBTask.ExecuteUpdate("usp_UpdateRetailerStatus", m_sqlParam);

                //return Convert.ToInt16(m_sqlParam[3].Value);
                //// return Convert.ToInt16(m_sqlParam[3].Value);


                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@CustomerCode", Customercode);
                m_sqlParam[1] = new SqlParameter("@Task", task);
                m_sqlParam[2] = new SqlParameter("@UpdatedBy", Customercode);

                // Output parameters
                m_sqlParam[3] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                m_sqlParam[4] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateRetailerStatus", m_sqlParam);

                // Retrieve and return output
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

        public SqlDataReader ReterivePersonaldoc()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@loanId", LoanID);


                return objDBTask.ExecuteDataReader("usp_Getdoucmentlist", m_sqlParam);
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

        #endregion GetUserDetail
        public Hashtable Reterivecustmercode()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@loanid", LoanID);


                return objDBTask.ExecuteHashtable("uspgetusercode", m_sqlParam);
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
        public short UpdatePhysicalTelicaller()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@TrueCallerName", Telicaller);
                m_sqlParam[1] = new SqlParameter("@PhysicalVerificationPerson", PhysicalVari);

                m_sqlParam[2] = new SqlParameter("@merchantId", LoanID);
                m_sqlParam[3] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_UpdatePhysicalVarification]", m_sqlParam);

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

        public short CreateEmiShudule()
        {
            try
            {

                m_sqlParam = new SqlParameter[16];
                m_sqlParam[0] = new SqlParameter("@CustCode", Usercode);
                m_sqlParam[1] = new SqlParameter("@LoanCategory", LoanCategory);
                m_sqlParam[2] = new SqlParameter("@LoanType", LoanType);
                m_sqlParam[3] = new SqlParameter("@EMIStartDate", EMIStartDate);
                m_sqlParam[4] = new SqlParameter("@InstallmentAmount", InstallmentAmount);
                m_sqlParam[5] = new SqlParameter("@ROI", ROI);
                m_sqlParam[6] = new SqlParameter("@InterestAmount", InterestAmount);

                m_sqlParam[7] = new SqlParameter("@TotalAmount", TotalAmount);
                m_sqlParam[8] = new SqlParameter("@EMIStatus", EMIStatus);


             
              

                m_sqlParam[9] = new SqlParameter("@paramuser", paramuser);
                m_sqlParam[10] = new SqlParameter("@ActiveStatus", ActiveStatus);
                m_sqlParam[11] = new SqlParameter("@LoanID", LoanID);
                m_sqlParam[12] = new SqlParameter("@InsttalmentNo", InsttalmentNo);

                m_sqlParam[13] = new SqlParameter("@Amounttodisbursement", Loandisburshmentamt);
                m_sqlParam[14] = new SqlParameter("@TotalNoofmonthday", monthday);

                m_sqlParam[15] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[15].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertemishudule]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[15].Value);

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
        public short AssignApplicationToUser()
        {
            try
            {

                m_sqlParam = new SqlParameter[5];

                m_sqlParam[0] = new SqlParameter("@Assigncode", Assigncode);
                m_sqlParam[1] = new SqlParameter("@LoanId",LoanID);

                m_sqlParam[2] = new SqlParameter("@merchantId",Usercode);
                m_sqlParam[3] = new SqlParameter("@status", LoanStatus);
                m_sqlParam[4] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[4].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_UpdateAssignApplictiontoUser]", m_sqlParam);

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
        public DataTable GetApprovedListForDisbursement()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@MobileNo",MobileNo);
                m_sqlParam[1] = new SqlParameter("@CustName", Name);
                m_sqlParam[2] = new SqlParameter("@Loanid", LoanID);

                return objDBTask.ExecuteDataTable("usp_getApprovedLoanDetailsforDisburshment", m_sqlParam, "ApprovedApplication");
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

        public Hashtable ReterivedisbursementDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@loanId", LoanID);


                return objDBTask.ExecuteHashtable("usp_GetLoanDetailsforDisbursement", m_sqlParam);
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


        public short DisbursementTransaction()
        {
            try
            {

                m_sqlParam = new SqlParameter[24];
           
                m_sqlParam[0] = new SqlParameter("@CustCode", Usercode);
                m_sqlParam[1] = new SqlParameter("@LoanId", LoanID);
                m_sqlParam[2] = new SqlParameter("@DisbursementDate", DisburshmentDate);
                m_sqlParam[3] = new SqlParameter("@LoanAmount", loanamt);
                m_sqlParam[4] = new SqlParameter("@ROI", ROI);
                m_sqlParam[5] = new SqlParameter("@Processingcharge", Processingcharge);

                m_sqlParam[6] = new SqlParameter("@OtherCharge", Othercharge);
                m_sqlParam[7] = new SqlParameter("@DisbursementAmt", Loandisburshmentamt);
                m_sqlParam[8] = new SqlParameter("@DisbursementBy", paramuser);
                m_sqlParam[9] = new SqlParameter("@AccountNo", accountno);
             
                m_sqlParam[10] = new SqlParameter("@BranchName", baranchname);
                m_sqlParam[11] = new SqlParameter("@BankName", bankname);

                m_sqlParam[12] = new SqlParameter("@IfscCode", ifsccode);
               
                m_sqlParam[13] = new SqlParameter("@virtualID", virtualid);
                

                m_sqlParam[14] = new SqlParameter("@LoanDuration", LoanDuration);
                m_sqlParam[15] = new SqlParameter("@LoanCategory", LoanCategory);
                m_sqlParam[16] = new SqlParameter("@LoanCalType", LoanCalType);
                m_sqlParam[17] = new SqlParameter("@ActiveStatus", ActiveStatus);
                m_sqlParam[18] = new SqlParameter("@Loantype", Loantype);
                m_sqlParam[19] = new SqlParameter("@parmUserName", paramuser);

                m_sqlParam[20] = new SqlParameter("@ModeofPayment", Modeofpayment);
                m_sqlParam[21] = new SqlParameter("@Transaction_RefNo", TransactionRefId);
                m_sqlParam[22] = new SqlParameter("@Remarks", Remarks);

                m_sqlParam[23] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[23].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertDisbursementTransaction]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[23].Value);

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

        public DataTable GetEMIPaidRequest()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@MobileNo", MobileNo);
                m_sqlParam[1] = new SqlParameter("@CustName", Name);
                m_sqlParam[2] = new SqlParameter("@Loanid", LoanID);

                return objDBTask.ExecuteDataTable("usp_getEmiPendingRequestDetails", m_sqlParam, "RequestEMI");
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

        public short ApprovedEMI()
        {
            try
            {

                m_sqlParam = new SqlParameter[8];


                m_sqlParam[0] = new SqlParameter("@loanID", LoanID);



                m_sqlParam[1] = new SqlParameter("@custCode", Usercode);
                m_sqlParam[2] = new SqlParameter("@Remarks", Remarks);

                m_sqlParam[3] = new SqlParameter("@ApprovedBy", paramuser);
                m_sqlParam[4] = new SqlParameter("@EMINumber", InsttalmentNo);
                m_sqlParam[5] = new SqlParameter("@ApprovedAmt", InstallmentAmount);
                m_sqlParam[6] = new SqlParameter("@DepositDate", EMIDepositDate);

                m_sqlParam[7] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[7].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_ApprovedEmiRequest]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[7].Value);

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

        public short UpdateQRCode()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmMerchantid", Usercode);
                m_sqlParam[1] = new SqlParameter("@QRCodeImageData", stringimage);

                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateQRCode", m_sqlParam);
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

        public DataTable GetEmiCollection()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
        
                m_sqlParam[0] = new SqlParameter("@Loanid", LoanID);

                return objDBTask.ExecuteDataTable("usp_getEmiCollectionReport", m_sqlParam, "EMICOLLECTION");
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

        public short UpdateApplicationRejected()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@Loanid", LoanID);
                m_sqlParam[1] = new SqlParameter("@parmActive_YN", Reject_YN);
                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("usp_UpdateApplicationRejected", m_sqlParam);
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


        public DataTable GetContactList()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@UserID", Usercode);
          
                return objDBTask.ExecuteDataTable("usp_GetContactList", m_sqlParam, "GetContact");
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


        public DataTable GetlistforSatllement()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@MobileNo", MobileNo);
                m_sqlParam[1] = new SqlParameter("@CustName", Name);
                m_sqlParam[2] = new SqlParameter("@Loanid", LoanID);

                return objDBTask.ExecuteDataTable("usp_getApprovedLoanforSettlement", m_sqlParam, "SatllementApplication");
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

        public Hashtable ReteriveSettlementDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@loanId", LoanID);


                return objDBTask.ExecuteHashtable("usp_GetLoanDetailsSattlement", m_sqlParam);
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


        public short SettlementTransaction()
        {
            try
            {

                m_sqlParam = new SqlParameter[12];

                m_sqlParam[0] = new SqlParameter("@CustCode", Usercode);
                m_sqlParam[1] = new SqlParameter("@LoanId", LoanID);
                m_sqlParam[2] = new SqlParameter("@SettlementDate", SettlementDate);
                m_sqlParam[3] = new SqlParameter("@LoanAmount", loanamt);
                m_sqlParam[4] = new SqlParameter("@ToatalPaidamt", totalpaidamt);
                m_sqlParam[5] = new SqlParameter("@ROS", ROS);
                m_sqlParam[6] = new SqlParameter("@SettlementCharge", Settlementcharge);

                m_sqlParam[7] = new SqlParameter("@Balance", Balance);
                m_sqlParam[8] = new SqlParameter("@PayableAmt", Payableamt);
                m_sqlParam[9] = new SqlParameter("@parmUserName", paramuser);
                m_sqlParam[10] = new SqlParameter("@Remarks", Remarks);



                m_sqlParam[11] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[11].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertSettlementTransaction]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[11].Value);

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

        public Hashtable Collectioncalu()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];


                m_sqlParam[0] = new SqlParameter("@paramflag", Flag);

                return objDBTask.ExecuteHashtable("usp_countcollection", m_sqlParam);
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
        public Hashtable ReterivePersonalforcollection()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@Loanid", LoanID);


                return objDBTask.ExecuteHashtable("usp_getEmiCollectionDet", m_sqlParam);
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