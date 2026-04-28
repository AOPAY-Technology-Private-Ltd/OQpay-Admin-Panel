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
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Util;
using Telerik.Web.UI.com.hisoftware.api2;
using static QRCoder.PayloadGenerator;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLBankdetails
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;



        public string Beneficieiy_ID { get; set; }
        public string Mobile_No { get; set; }
        public string AccountHolder_Name { get; set; }
        public string Account_No { get; set; }
        public string IFSC_Code { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Brancaddress { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string UpdatedBy { get; set; }
        public string ApprovedStatus { get; set; }
        public string RegistrationID { get; set; }
        public string Action { get; set; }
        public int? RID { get; set; }

        public string qrcode_Path { get; set; }
        public string upi_intend { get; set; }

        public DataTable GetBankdetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[6];

                m_sqlParam[0] = new SqlParameter("@Action", Action);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@MobileNumber ", Mobile_No);
                m_sqlParam[3] = new SqlParameter("@ActiveStatus", ApprovedStatus);
                m_sqlParam[4] = new SqlParameter("@RegistrationID", RegistrationID);
                m_sqlParam[5] = new SqlParameter("@ModifiedBy", UpdatedBy);



                return objDBTask.ExecuteDataTable("usp_ManageBeneficiaryDetails", m_sqlParam, "GetBankDertails");
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


        public DataTable Getadminbankdetails()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[2];

                m_sqlParam[0] = new SqlParameter("@Task", Action);
                m_sqlParam[1] = new SqlParameter("@RID", RID);




                return objDBTask.ExecuteDataTable("usp_AdminBankMaster_CRUD", m_sqlParam, "GetBankDertails");
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


        public short Addadminbankdetails()
        {
            try
            {

                m_sqlParam = new SqlParameter[13];

                
                m_sqlParam[0] = new SqlParameter("@Task", Action);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@AccountNumber", Account_No);

                m_sqlParam[3] = new SqlParameter("@AccountName", AccountHolder_Name);
                m_sqlParam[4] = new SqlParameter("@BankName", BankName);
                m_sqlParam[5] = new SqlParameter("@IFSCCode", IFSC_Code);
                m_sqlParam[6] = new SqlParameter("@BranchName", BranchName);
                m_sqlParam[7] = new SqlParameter("@BranchAddress", Brancaddress);
                m_sqlParam[8] = new SqlParameter("@ActiveStatus", ApprovedStatus);
                m_sqlParam[9] = new SqlParameter("@CreatedBy", UpdatedBy);

                m_sqlParam[10] = new SqlParameter("@QRCode_Path", qrcode_Path);
                m_sqlParam[11] = new SqlParameter("@Upi_intend", upi_intend);

                m_sqlParam[12] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[12].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_AdminBankMaster_CRUD]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[12].Value);

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
        public bool ApprovedBankDetails()
        {
            try
            {
                m_sqlParam = new SqlParameter[4];
                m_sqlParam[0] = new SqlParameter("@Action", Action);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@ModifiedBy", UpdatedBy);
                m_sqlParam[3] = new SqlParameter("@RegistrationID", RegistrationID);

                object result = objDBTask.ExecuteUpdate("[usp_ManageBeneficiaryDetails]", m_sqlParam);

                int rowsAffected = (result != null && result != DBNull.Value)
                    ? Convert.ToInt32(result)
                    : 0;

                // ✅ consider -1 also as success
                return rowsAffected > 0 || rowsAffected == -1;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }





        //    public void TransertoAgent()
        //    {
        //        try
        ////        {
        ////            @Action VARCHAR(10),  --'Insert', 'Update', 'Activate', 'Deactivate', 'Get'
        ////@Beneficieiy_ID NVARCHAR(250) = NULL,  
        ////@Mobile_No NVARCHAR(250) = NULL,
        ////@AccountHolder_Name NVARCHAR(250) = NULL,
        ////@Account_No NVARCHAR(250) = NULL,
        ////@IFSC_Code NVARCHAR(250) = NULL,
        ////@BankName NVARCHAR(250) = NULL,  
        ////@BranchName NVARCHAR(250) = NULL,  
        ////@Email NVARCHAR(250) = NULL,
        ////@Address NVARCHAR(250) = NULL,
        ////@City NVARCHAR(250) = NULL,
        ////@State NVARCHAR(250) = NULL,
        ////@PinCode NVARCHAR(250) = NULL,
        ////@UpdatedBy NVARCHAR(250) = NULL,
        ////@ApprovedStatus VARCHAR(50) = NULL,
        ////@RegistrationID VARCHAR(50) = NULL

        //            m_sqlParam = new SqlParameter[21];
        //            m_sqlParam[0] = new SqlParameter("@Action", RegistrationId);
        //            m_sqlParam[1] = new SqlParameter("@Beneficieiy_ID", TransferFrom);
        //            m_sqlParam[2] = new SqlParameter("@Mobile_No", TransferTo);
        //            m_sqlParam[3] = new SqlParameter("@AccountHolder_Name", TransferAmt);
        //            m_sqlParam[4] = new SqlParameter("@transferToMsg", TransferToMsg);
        //            m_sqlParam[5] = new SqlParameter("@transferFromMsg", TransferFromMsg);
        //            m_sqlParam[6] = new SqlParameter("@amountType", Amount_Type);
        //            m_sqlParam[7] = new SqlParameter("@ActualCommissionAmount", ActualCommissionAmount);
        //            m_sqlParam[8] = new SqlParameter("@GSTAmount", GSTAMT);
        //            m_sqlParam[9] = new SqlParameter("@TDSAmount", TdsAmt);
        //            m_sqlParam[10] = new SqlParameter("@commissionWithoutGST", commissionWithoutGST);

        //            m_sqlParam[11] = new SqlParameter("@remark", Remark);
        //            m_sqlParam[12] = new SqlParameter("@TransIpAddress", TransIpAddress);
        //            m_sqlParam[13] = new SqlParameter("@ServiceschargeAmount", Services_charge_Amt);
        //            m_sqlParam[14] = new SqlParameter("@ServiceschargeGSTAmount", Services_charge_GSTAmt);
        //            m_sqlParam[15] = new SqlParameter("@ServiceschargeWithoutGST", Services_charge_Without_GST);
        //            m_sqlParam[16] = new SqlParameter("@customerCommission", customerCommission);
        //            m_sqlParam[17] = new SqlParameter("@customerCommissionGST", customerCommissionGST);

        //            m_sqlParam[18] = new SqlParameter("@customerCommission_withoutGST", customerCommissionWithoutGST);
        //            m_sqlParam[19] = new SqlParameter("@Amt_Transfer_TransID", Amt_Transfer_TransID);

        //            m_sqlParam[20] = new SqlParameter("@HoldingAmount", Holdingamt);




        //            objDBTask.ExecuteUpdate("[usp_TransferAmtToAgent]", m_sqlParam);

        //        }
        //        catch (SqlException sqlEx)
        //        {
        //            throw sqlEx;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
    }
}