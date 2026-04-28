using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TheEMIClubApplication.AppCode;

namespace TheEMIClubApplication.BussinessLayer
{

    public class BLCompanyDetails
    {
        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;
        public int RID { get; set; }                 
        public string CompanyName { get; set; }
        public string ClientCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string LogoPath { get; set; }
        public string CompanyType { get; set; }
        public string Website { get; set; }
        public string EUser { get; set; }
        public DateTime? EDate { get; set; }
        public string MUser { get; set; }
        public DateTime? MDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Membershipfee { get; set; }
        public string Latefine { get; set; }
        public string ReserveType { get; set; }
        public decimal Reservevalues { get; set; }
        public Int32 gracevalues { get; set; }
        public string ServiceChargetype { get; set; }
        public decimal ServiceChargeValue { get; set; }

        public string SettlementChargeType { get; set; }
        public decimal SettlementChargeValue { get; set; }

        public string ForeclosureChargeType { get; set; }
        public decimal ForeclosureChargeValue { get; set; }

        public decimal MiniholdamountRequestvalue { get; set; }


        public DataTable getCompanyDetail(out int returnCode, out string returnMsg)
        {
             try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[4];
                //m_sqlParam[0] = new SqlParameter("@Mode", Mode);
                //m_sqlParam[1] = new SqlParameter("@RID", RID);
                //m_sqlParam[2] = new SqlParameter("@Customercode", Customercode);
                //m_sqlParam[3] = new SqlParameter("@Loancode", Loancode);

                m_sqlParam[0] = new SqlParameter("@Status", Status);
                m_sqlParam[1] = new SqlParameter("@ClientCode", AppSessions.companycode);

                m_sqlParam[2] = new SqlParameter("@ReturnCode", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                m_sqlParam[3] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                DataTable dt = objDBTask.ExecuteDataTable("usp_GetMstCompany", m_sqlParam, "MstCompany");

                // Get output parameters
                returnCode = Convert.ToInt32(m_sqlParam[2].Value);
                returnMsg = m_sqlParam[3].Value.ToString();

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
        public DataTable UpdateCompanyDetail(out int returnCode, out string returnMsg)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[19];

                sqlParams[0] = new SqlParameter("@Firstname", FirstName);
                sqlParams[1] = new SqlParameter("@Lastname", LastName);
                sqlParams[2] = new SqlParameter("@Website", Website);
                sqlParams[3] = new SqlParameter("@Membershipfee", Membershipfee);
                sqlParams[4] = new SqlParameter("@Latefinefee", Latefine);
                sqlParams[5] = new SqlParameter("@Clientcode", ClientCode);
                sqlParams[6] = new SqlParameter("@CompanyLogo_Path", LogoPath);
                sqlParams[7] = new SqlParameter("@ReserveType", ReserveType);
                sqlParams[8] = new SqlParameter("@ReserveValue", Reservevalues);
                sqlParams[9] = new SqlParameter("@ServiceChargeType", ServiceChargetype);
                sqlParams[10] = new SqlParameter("@ServiceChargeValue", ServiceChargeValue);
                sqlParams[11] = new SqlParameter("@GraceValues", gracevalues);

                // 🔥 NEW : Settlement & Foreclosure
                sqlParams[12] = new SqlParameter("@SettlementChargeType", SettlementChargeType);
                sqlParams[13] = new SqlParameter("@SettlementChargeValue", SettlementChargeValue);
                sqlParams[14] = new SqlParameter("@ForeclosureChargeType", ForeclosureChargeType);
                sqlParams[15] = new SqlParameter("@ForeclosureChargeValue", ForeclosureChargeValue);
                sqlParams[16] = new SqlParameter("@MiniholdamountRequestvalue", MiniholdamountRequestvalue);

                sqlParams[17] = new SqlParameter("@ReturnCode", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                sqlParams[18] = new SqlParameter("@ReturnMsg", SqlDbType.NVarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };

                DataTable dt = objDBTask.ExecuteDataTable(
                    "usp_UpdateMstCompany",
                    sqlParams,
                    "MstCompany"
                );

                returnCode = Convert.ToInt32(sqlParams[17].Value);
                returnMsg = Convert.ToString(sqlParams[18].Value);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}