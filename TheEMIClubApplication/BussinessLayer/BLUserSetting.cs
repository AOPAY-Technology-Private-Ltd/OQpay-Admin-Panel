using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Web.UI.WebControls;
using System.Web.Util;
//using Telerik.Reporting.Services.Engine;

using Telerik.Web.UI.com.hisoftware.api2;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLUserSetting
    {
        #region Global Variables

        DBTask objDBTask = new DBTask();
        SqlParameter[] m_sqlParam;
        string xmlDoc = string.Empty;

        #endregion

        #region Member Variables
        //private string m_loginId;



       



        private string m_Userid;
        private string m_MessageHeader;
        private string m_MessageBody;
        private string m_paramflag;
        private string m_companycode;
        private string m_crdrenable;


        private int M_noofmissingentry;
        private int M_Nooftransaction;

        private decimal m_minimumamount;
        private decimal m_maximumamount;

        private string m_paramuser;
        private string m_beniid;
        private string m_ApprovedBy;

        private string m_Status;

        private string m_RegistrationId;

        private string m_Remakrs;
        private Int32 M_RID;

        private string m_Invarment;
        private string m_AllowedIP;
        private string m_AllowedIP2;
        private string m_Sender;
        private string m_CallBAckurl;



        #endregion MemberVariable

        #region Property





        public string usercode { get { return m_Userid; } set { m_Userid = value; } }

        public string MessageHeader { get { return m_MessageHeader; } set { m_MessageHeader = value; } }
        public string MessageBody { get { return m_MessageBody; } set { m_MessageBody = value; } }
  




        public string CompanyCode { get { return m_companycode; } set { m_companycode = value; } }
        public string CrRdEnable { get { return m_crdrenable; } set { m_crdrenable = value; } }

        public int NoofmissingEntry { get { return M_noofmissingentry; } set { M_noofmissingentry = value; } }
        public int NoofTransactionEntry { get { return M_Nooftransaction; } set { M_Nooftransaction = value; } }

        public decimal Manimumamount { get { return m_minimumamount; } set { m_minimumamount = value; } }
        public decimal Maximumamount { get { return m_maximumamount; } set { m_maximumamount = value; } }


        public string Reamrks { get { return m_Remakrs; } set { m_Remakrs = value; } }
        public string parmUserName { get { return m_paramuser; } set { m_paramuser = value; } }
        public string BeniId { get { return m_beniid; } set { m_beniid = value; } }

        public Int32 RID { get { return M_RID; } set { M_RID = value; } }

        public string status { get { return m_Status; } set { m_Status = value; } }

        public string invarment { get { return m_Invarment; } set { m_Invarment = value; } }
        public string AllowedIp { get { return m_AllowedIP; } set { m_AllowedIP = value; } }
        public string AllowedIp2 { get { return m_AllowedIP2; } set { m_AllowedIP2 = value; } }

        public string CallBackUrl { get { return m_CallBAckurl; } set { m_CallBAckurl = value; } }
        public string SenderId { get { return m_Sender; } set { m_Sender = value; } }
        public string Paramflag { get { return m_paramflag; } set { m_paramflag = value; } }


        #endregion

        public short InsertUserSetting()
        {
            try
            {

                m_sqlParam = new SqlParameter[15];

                m_sqlParam[0] = new SqlParameter("@UserCode", usercode);
                m_sqlParam[1] = new SqlParameter("@CompanyCode", CompanyCode);
                m_sqlParam[2] = new SqlParameter("@ApprovedEnableYN", CrRdEnable);
                m_sqlParam[3] = new SqlParameter("@NoofMissionEntry", NoofmissingEntry);
                m_sqlParam[4] = new SqlParameter("@NoofTransaction", NoofTransactionEntry);
                m_sqlParam[5] = new SqlParameter("@ParamUser", parmUserName);
                m_sqlParam[6] = new SqlParameter("@Remarks", Reamrks);

                m_sqlParam[7] = new SqlParameter("@MaximumAmt", Maximumamount);
                m_sqlParam[8] = new SqlParameter("@MimimumAmt", Manimumamount);


                m_sqlParam[9] = new SqlParameter("@AllowedIP", AllowedIp);
                m_sqlParam[10] = new SqlParameter("@Invironment",invarment );
                m_sqlParam[11] = new SqlParameter("@Sender_id", SenderId);
                m_sqlParam[12] = new SqlParameter("@CallBackUrl", CallBackUrl);
                m_sqlParam[13] = new SqlParameter("@AllowedIP_2", AllowedIp2);


                m_sqlParam[14] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[14].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertUserSetting]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[14].Value);

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

        public DataTable ShowUserSetting()
        {
            try
            {

                m_sqlParam = new SqlParameter[0];
                return objDBTask.ExecuteDataTable("usp_getUserSetting", m_sqlParam, "");
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

        public Hashtable ReteriveSetting()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@Rid", RID);


                return objDBTask.ExecuteHashtable("usp_getUserSettingReeterive", m_sqlParam);
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

        public Hashtable CredentialsForReport()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@userCode", usercode);


                return objDBTask.ExecuteHashtable("usp_getUsercrediantialforReport", m_sqlParam);
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

        public Hashtable GetAllertmessage()
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@Merchant", usercode);


                return objDBTask.ExecuteHashtable("usp_GetReteriveMessageAllert", m_sqlParam);
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

        public short updateUserSetting()
        {
            try
            {

                m_sqlParam = new SqlParameter[17];

                m_sqlParam[0] = new SqlParameter("@UserCode", usercode);
                m_sqlParam[1] = new SqlParameter("@CompanyCode", CompanyCode);
                m_sqlParam[2] = new SqlParameter("@ApprovedEnableYN", CrRdEnable);
                m_sqlParam[3] = new SqlParameter("@NoofMissionEntry", NoofmissingEntry);
                m_sqlParam[4] = new SqlParameter("@NoofTransaction", NoofTransactionEntry);
                m_sqlParam[5] = new SqlParameter("@ParamUser", parmUserName);
                m_sqlParam[6] = new SqlParameter("@STATUS", status);
                m_sqlParam[7] = new SqlParameter("@Remarks", Reamrks);
                m_sqlParam[8] = new SqlParameter("@Rid", RID);
                m_sqlParam[9] = new SqlParameter("@MaximumAmt", Maximumamount);
                m_sqlParam[10] = new SqlParameter("@MimimumAmt", Manimumamount);

                m_sqlParam[11] = new SqlParameter("@AllowedIP", AllowedIp);
                m_sqlParam[12] = new SqlParameter("@Invironment", invarment);
                m_sqlParam[13] = new SqlParameter("@Sender_id", SenderId);
                m_sqlParam[14] = new SqlParameter("@CallBackUrl", CallBackUrl);
                m_sqlParam[15] = new SqlParameter("@AllowedIP2", AllowedIp2);

                m_sqlParam[16] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[16].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_usersettingupdate]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[16].Value);

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


        public short updatMessageAllert()
        {
            try
            {

                m_sqlParam = new SqlParameter[4];

                m_sqlParam[0] = new SqlParameter("@MessageAllertHeader",MessageHeader);
                m_sqlParam[1] = new SqlParameter("@MessageBody",MessageBody);
           
                m_sqlParam[2] = new SqlParameter("@merchantId",usercode);
                m_sqlParam[3] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[3].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_UpdateAllertMessages]", m_sqlParam);

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

        public DataTable ShowBankdetailsforapproval()
        {
            try
            {

                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@userName", usercode);
                m_sqlParam[1] = new SqlParameter("@paramFlag", Paramflag);
                return objDBTask.ExecuteDataTable("usp_GetBankdetailsbyaccountno", m_sqlParam, "");
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

        public short ActiveAccount()
        {
            try
            {

                m_sqlParam = new SqlParameter[3];

                m_sqlParam[0] = new SqlParameter("@beniId",BeniId);
               
                m_sqlParam[1] = new SqlParameter("@Status", Paramflag);



                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[updateBeneficrydetails]", m_sqlParam);

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