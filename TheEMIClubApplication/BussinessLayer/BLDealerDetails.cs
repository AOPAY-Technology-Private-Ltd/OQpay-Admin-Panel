using AVFramework;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace TheEMIClubApplication.BussinessLayer
{

    public class BLDealerDetails
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;
        public string Mode { get; set; }                   // For example: "CREATE"
        public string LoginId { get; set; }                // Maps to @parmLoginId
        public string Status { get; set; }                 // Maps to @parmStatus
        public string MobileNo { get; set; }               // Maps to @parmMobileNo
        public string Address { get; set; }                // Maps to @parmAddress
        public string Email { get; set; }                  // Maps to @parmEmail
        public string FirstName { get; set; }              // Maps to @parmFirstName
        public string LastName { get; set; }               // Maps to @parmLastName

        public string storeName { get; set; }
        public string storeAddress { get; set; }
        public string profilephoto { get; set; }
        public string aadhaarFrontphoto { get; set; }
        public string aadhaarBackphoto { get; set; }
        public string cancelChequephoto { get; set; }
        public string storeFrontphoto { get; set; }
        public string comapnyDocphoto { get; set; }

        // Optional: Output values from stored procedure
        public int ReturnCode { get; set; }              // From @ReturnCode OUTPUT
        public string ReturnMsg { get; set; }

        public DataTable RetailerStatusUpdate()
        {
            try
            {
                m_sqlParam = new SqlParameter[16];

                m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                m_sqlParam[1] = new SqlParameter("@parmLoginId", LoginId);
                m_sqlParam[2] = new SqlParameter("@parmStatus", Status);
                m_sqlParam[3] = new SqlParameter("@parmMobileNo", MobileNo);
                m_sqlParam[4] = new SqlParameter("@parmAddress", Address);
                m_sqlParam[5] = new SqlParameter("@parmEmail", Email);
                m_sqlParam[6] = new SqlParameter("@parmFirstName", FirstName);
                m_sqlParam[7] = new SqlParameter("@parmLastName", LastName);
                m_sqlParam[8] = new SqlParameter("@StoreName", storeName);
                m_sqlParam[9] = new SqlParameter("@StoreAddress", storeAddress);
                m_sqlParam[10] = new SqlParameter("@Profile_Photo", profilephoto);
                m_sqlParam[11] = new SqlParameter("@Adhaar_front_photo", aadhaarFrontphoto);
                m_sqlParam[12] = new SqlParameter("@Adhaar_back_Photo", aadhaarBackphoto);
                m_sqlParam[13] = new SqlParameter("@cancle_cheque_Photo", cancelChequephoto);
                m_sqlParam[14] = new SqlParameter("@store_front_Photo", storeFrontphoto);
                m_sqlParam[15] = new SqlParameter("@company_doc_Photo",comapnyDocphoto);

                // Output parameters
                //m_sqlParam[8] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                //m_sqlParam[8].Direction = ParameterDirection.Output;
                //m_sqlParam[9] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                //m_sqlParam[9].Direction = ParameterDirection.Output;


                //// Retrieve and return output
                //ReturnCode = Convert.ToInt16(m_sqlParam[8].Value);
                //ReturnMsg = Convert.ToString(m_sqlParam[9].Value);
                return objDBTask.ExecuteDataTable("usp_ApproveUpdateActiveDealer", m_sqlParam, "Registration");


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