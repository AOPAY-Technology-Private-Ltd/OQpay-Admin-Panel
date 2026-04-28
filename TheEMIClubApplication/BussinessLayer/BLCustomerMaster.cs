using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using AVFramework;

namespace TheEMIClubApplication.BussinessLayer
{
 
    public class BLCustomerMaster
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;
        public string Mode { get; set; }
        public int? RID { get; set; }
        public string Userid { get; set; }

        // Personal Info
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string PrimaryOTP { get; set; }
        public string PrimaryMobileVerified { get; set; }
        public string AlternateMobileNumber { get; set; }
        public string AlternateMobileOTP { get; set; }
        public string PAlternateMobileVerified { get; set; }
        public string EMailID { get; set; }

        // Address
        public string FlatNo { get; set; }
        public string AearSector { get; set; }
        public string CurrentAddress { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        // Identity
        public string AadharNumber { get; set; }
        public string AadharNumberVerified { get; set; }
        public string PANNumber { get; set; }
        public string PANNumberVerified { get; set; }
        public string CustPhoto_path { get; set; }

        // Device Info
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ModelVariant { get; set; }
        public string Color { get; set; }
        public string SellingPrice { get; set; }
        public string DownPayment { get; set; }
        public string Tenure { get; set; }
        public string EMIAmount { get; set; }
        public string IMEINumber1 { get; set; }
        public string IMEINumber1_SealPhotoPath { get; set; }
        public string IMEINumber2 { get; set; }
        public string IMEINumber2_SealPhotoPath { get; set; }
        public string Invoive_Path { get; set; }
        public string IMEINumber_PhotoPath { get; set; }

        // Banking Info
        public string AccountNumber { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string BranchName { get; set; }

        // Reference Info
        public string RefName { get; set; }
        public string RefRelationShip { get; set; }
        public string RefmobileNo { get; set; }
        public string RefAddress { get; set; }

        // Flags
        public string DebitOrCreditCard { get; set; }
        public string UPIMandate { get; set; }

        // Audit
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }



        public string customercode { get; set; }
        public int graceday { get; set; }


        public DataTable GetCustomerDeviceMst()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[12];
                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@RID", RID);
                m_sqlParam[2] = new SqlParameter("@FirstName", FirstName);
                m_sqlParam[3] = new SqlParameter("@PrimaryMobileNumber", PrimaryMobileNumber);
                m_sqlParam[4] = new SqlParameter("@EMailID", EMailID);
                m_sqlParam[5] = new SqlParameter("@AadharNumber", AadharNumber);
                m_sqlParam[6] = new SqlParameter("@PANNumber", PANNumber);
                m_sqlParam[7] = new SqlParameter("@BrandName", BranchName);
                m_sqlParam[8] = new SqlParameter("@ModelName", ModelName);
                m_sqlParam[9] = new SqlParameter("@AccountNumber", AccountNumber);
                m_sqlParam[10] = new SqlParameter("@IMEINumber1", IMEINumber1);
                m_sqlParam[11] = new SqlParameter("@IMEINumber2", IMEINumber2);
                m_sqlParam[11] = new SqlParameter("@PrimaryOTP", ActiveStatus);
                //m_sqlParam[11] = new SqlParameter("@InterestPerc", InterestPerc);
                //m_sqlParam[12] = new SqlParameter("@Tenure", Tenure);
                //m_sqlParam[13] = new SqlParameter("@ProcessingFees", ProcessingFees);


                //m_sqlParam[15] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10)
                //{
                //    Direction = ParameterDirection.Output
                //};
                //SqlParameter returnMsg = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255)
                //{
                //    Direction = ParameterDirection.Output
                //};

                //objDBTask.DBCommandTimeOut = 800;
                //Array.Resize(ref m_sqlParam, 17);
                //m_sqlParam[16] = returnMsg;



                return objDBTask.ExecuteDataTable("sp_QOFinance_ManageCustomer", m_sqlParam, "DeviceVariantMaster");
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

        public DataTable UpdateCustomerDeviceMst()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[]
                {
            new SqlParameter("@Mode", Mode),
            new SqlParameter("@RID", RID),
            new SqlParameter("@FirstName", FirstName),
            new SqlParameter("@MiddleName", MiddleName),
            new SqlParameter("@LastName", LastName),
            new SqlParameter("@PrimaryMobileNumber", PrimaryMobileNumber),
            new SqlParameter("@PrimaryOTP", PrimaryOTP),
            new SqlParameter("@PrimaryMobileVerified", PrimaryMobileVerified),
            new SqlParameter("@AlternateMobileNumber", AlternateMobileNumber),
            new SqlParameter("@AlternateMobileOTP", AlternateMobileOTP),
            new SqlParameter("@PAlternateMobileVerified", PAlternateMobileVerified),
            new SqlParameter("@EMailID", EMailID),
            new SqlParameter("@FlatNo", FlatNo),
            new SqlParameter("@AearSector", AearSector),
            new SqlParameter("@CurrentAddress", CurrentAddress),
            new SqlParameter("@PinCode", PinCode),
            new SqlParameter("@Country", Country),
            new SqlParameter("@StateName", StateName),
            new SqlParameter("@CityName", CityName),
            new SqlParameter("@AadharNumber", AadharNumber),
            new SqlParameter("@AadharNumberVerified", AadharNumberVerified),
            new SqlParameter("@PANNumber", PANNumber),
            new SqlParameter("@PANNumberVerified", PANNumberVerified),
            new SqlParameter("@CustPhoto_path", CustPhoto_path),
            new SqlParameter("@BrandName", BrandName),
            new SqlParameter("@ModelName", ModelName),
            new SqlParameter("@ModelVariant", ModelVariant),
            new SqlParameter("@color", Color),
            new SqlParameter("@SellingPrice", SellingPrice),
            new SqlParameter("@DownPayment", DownPayment),
            new SqlParameter("@Tenure", Tenure),
            new SqlParameter("@EMIAmount", EMIAmount),
            new SqlParameter("@IMEINumber1", IMEINumber1),
            new SqlParameter("@IMEINumber1_SealPhotoPath", IMEINumber1_SealPhotoPath),
            new SqlParameter("@IMEINumber2", IMEINumber2),
            new SqlParameter("@IMEINumber2_SealPhotoPath", IMEINumber2_SealPhotoPath),
            new SqlParameter("@Invoive_Path", Invoive_Path),
            new SqlParameter("@IMEINumber_PhotoPath", IMEINumber_PhotoPath),
            new SqlParameter("@AccountNumber", AccountNumber),
            new SqlParameter("@BankIFSCCode", BankIFSCCode),
            new SqlParameter("@BankName", BankName),
            new SqlParameter("@AccountType", AccountType),
            new SqlParameter("@BranchName", BranchName),
            new SqlParameter("@RefName", RefName),
            new SqlParameter("@RefRelationShip", RefRelationShip),
            new SqlParameter("@RefmobileNo", RefmobileNo),
            new SqlParameter("@RefAddress", RefAddress),
            new SqlParameter("@DebitOrCreditCard", DebitOrCreditCard),
            new SqlParameter("@UPIMandate", UPIMandate)
                };


                return objDBTask.ExecuteDataTable("sp_QOFinance_ManageCustomer", m_sqlParam, "DeviceVariantMaster");
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }



        public DataTable UpdateCustomerGracetime()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[]
                {
            new SqlParameter("@CustomerCode", customercode),
            new SqlParameter("@GracePeriodDays", graceday),

         
                };


                return objDBTask.ExecuteDataTable("usp_UpdateCustomerEMIGracePeriod", m_sqlParam, "DeviceVariantMaster");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }




}