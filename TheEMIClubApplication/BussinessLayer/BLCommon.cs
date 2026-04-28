using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
//using iTextSharp.tool.xml.html;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLCommon
    {
        //DBTask objDBTask = new DBTask();    
        private static byte[] IV_192 = new byte[] {
        0x37, 0x67, 0xf6, 0x4f, 0x24, 0x63, 0xa7, 3, 0x2a, 5, 0x3e, 0x53, 0xb8, 7, 0xd1, 13,
        0x91, 0x17, 200, 0x3a, 0xad, 10, 0x79, 0xde
     };
        private static byte[] KEY_192 = new byte[] {
        0x2a, 0x10, 0x5d, 0x9c, 0x4e, 4, 0xda, 0x20, 15, 0xa7, 0x2c, 80, 0x1a, 250, 0x9b, 0x70,
        2, 0x5e, 11, 0xcc, 0x77, 0x23, 0xb8, 0xc5
     };

        #region Global Variables

        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        #endregion Global Variables

        #region Constructor
        public BLCommon()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion Constructor

        #region Constants
        public static readonly string DATETIME_FORMAT_REPORT = "yyyy-MM-dd";
        public static readonly string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static readonly DateTime DATETIME_NULL = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(DATETIME_FORMAT));

        public static readonly DateTime DATETIME_NULL_REPORT = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(DATETIME_FORMAT_REPORT));
        public static readonly DateTime DATETIME_NULL_REPORTTO = Convert.ToDateTime(new DateTime(2050, 1, 1).ToString(DATETIME_FORMAT_REPORT));
        public static readonly string FW_DATETIME_FORMAT = "yyyy-mm-dd";



        #endregion Constants

        #region Member Variables

        private string m_flag;
        private string m_flagName;
        private string m_flagType;
        private string m_flagValue;

        private string m_configuratorName;

        private string m_loginId;
        private string m_stateCode;

        private string m_lovCode;
        private string m_mode;
        private string m_masterCode;

        private string m_flag1;
        private string m_flag2;
        private string m_flag3;
        private string m_flag4;
        private string m_flag5;




        #endregion Member Variables

        #region Property

        public string Flag { get { return m_flag; } set { m_flag = value; } }
        public string FlagName { get { return m_flagName; } set { m_flagName = value; } }
        public string FlagType { get { return m_flagType; } set { m_flagType = value; } }
        public string FlagValue { get { return m_flagValue; } set { m_flagValue = value; } }

        public string ConfiguratorName { get { return m_configuratorName; } set { m_configuratorName = value; } }

        public string LoginId { get { return m_loginId; } set { m_loginId = value; } }
        public string StateCode { get { return m_stateCode; } set { m_stateCode = value; } }

        public string LovCode { get { return m_lovCode; } set { m_lovCode = value; } }
        public string Mode { get { return m_mode; } set { m_mode = value; } }
        public string MasterCode { get { return m_masterCode; } set { m_masterCode = value; } }

        public string Flag1 { get { return m_flag1; } set { m_flag1 = value; } }
        public string Flag2 { get { return m_flag2; } set { m_flag2 = value; } }
        public string Flag3 { get { return m_flag3; } set { m_flag3 = value; } }
        public string Flag4 { get { return m_flag4; } set { m_flag4 = value; } }
        public string Flag5 { get { return m_flag5; } set { m_flag5 = value; } }


        #endregion Property

        #region GetHomePageComponents
        /// <summary>
        /// Method to get home page tickers for AFMManager.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetHomePageComponents()
        {
            try
            {
                return objDBTask.ExecuteHashtable("usp_GetHomePageComponents_HTML");
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion GetHomePageComponents

        #region GetApplicationList

        /// <summary>
        /// Method to get Applications list.
        /// </summary>
        /// <param name="ddlName">Pass dropdown name.</param>
        /// <param name="selectOption">Pass default select option.</param>
        public void GetApplicationList(DropDownList ddlName, string selectOption)
        {
            try
            {
                objDBTask.BindDropDownList("usp_GetApplicationList", "ApplicationName", "ApplicationCode", ddlName, selectOption);
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
            finally
            {
                objDBTask = null;
            }
        }

        #endregion GetApplicationList

        #region Get Code Master
        /// <summary>
        /// Method for get code master for bind dropdown
        /// </summary>
        /// <returns>void</returns>
        public void GetCodeMaster(DropDownList ddlCodeMaster, string selectedText)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmFlagCode", m_flag);
                objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", ddlCodeMaster, selectedText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region bind city list

        /// <summary>
        /// Method to bind city list
        /// </summary>
        /// <returns></returns>
        public void BindCityList(System.Web.UI.WebControls.DropDownList ddlCityList, string selectedText)
        {
            try
            {

                objDBTask.BindDropDownList("usp_GetCityList", "CityName", "CityCode", ddlCityList, selectedText);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion bind city list

        #region Get Config Value
        public string GetConfigValue()
        {
            try
            {
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@parmFlagName", m_flagName);
                m_sqlParam[1] = new SqlParameter("@parmFlagType", m_flagType);
                return Convert.ToString(objDBTask.ExecuteScalarQuery("usp_GetConfiguratorValue", m_sqlParam));
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion

        #region Get Code Master(radio button list)
        /// <summary>
        /// Method for get code master for bind radio button list
        /// </summary>
        /// <returns>void</returns>
        public void GetCodeMaster(RadioButtonList rblCodeMaster)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmFlagCode", m_flag);
                objDBTask.BindRadioButtonList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", rblCodeMaster, "CodeMaster");

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
        #endregion

        #region Get Code Master 
        /// <summary>
        /// Method for get code master for bind dropdown (with code reference , used for sorting filtering with column name)
        /// </summary>
        /// <returns>void</returns>
        public void GetCodeMasterReference(DropDownList ddlCodeMaster, string selectedText)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmFlagCode", m_flag);
                objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "CodeReference", ddlCodeMaster, selectedText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetLOVForYNFlags

        /// <summary>
        /// Method to Bind DropDown for YN.
        /// </summary>
        /// <returns></returns>
        public void GetLOVForYNFlags(DropDownList ddlControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmLOVCode", m_lovCode);
                objDBTask.BindDropDownList("usp_GetLOVForYNFlags", m_sqlParam, "FlagText", "FlagValue", ddlControlId, "--All--");
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion GetLOVForYNFlags

        #region GetRuleNConfigValue

        /// <summary>
        /// Method to get value from MstRuleNConfig_FLT
        /// </summary>
        /// <returns>An object, to handle all Data Types.</returns>
        public object GetRuleNConfigValue()
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmConfiguratorName", m_configuratorName);
                m_sqlParam[1] = new SqlParameter("@parmFlagType", m_flagType);
                m_sqlParam[2] = new SqlParameter("@parmConfigOutValue", SqlDbType.VarChar, 50);
                m_sqlParam[2].Direction = ParameterDirection.Output;
                objDBTask.ExecuteScalarQuery("usp_GetRuleNConfigValue", m_sqlParam);
                return (m_sqlParam[2].Value);
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

        #endregion GetRuleNConfigValue

        #region GetApplicationNameList

        /// <summary>
        /// Method to Bind Application Name.
        /// </summary>
        /// <returns></returns>
        public void GetApplicationNameList(DropDownList ddlControlId)
        {
            try
            {
                objDBTask.BindDropDownList("usp_GetApplicationList", "ApplicationName", "ApplicationCode", ddlControlId, "-Select-");
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

        #endregion GetApplicationNameList

        #region BindDropDownList

        /// <summary>
        /// Method to bind Drop Down list
        /// </summary>
        /// <returns></returns>
        public void BindDropDownList(System.Web.UI.WebControls.DropDownList ddlEntity, string selectedText)
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFlag", m_flag);
                m_sqlParam[1] = new SqlParameter("@parmFlag1", m_flag1);
                m_sqlParam[2] = new SqlParameter("@parmFlag2", m_flag2);
                m_sqlParam[3] = new SqlParameter("@parmFlag3", m_flag3);
                m_sqlParam[4] = new SqlParameter("@parmFlag4", m_flag4);


                objDBTask.BindDropDownList("usp_BindDropDownList", m_sqlParam, "DisplayText", "DisplayValue", ddlEntity, selectedText);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion BindDropDownList


        #region BindCheckBoxList

        /// <summary>
        /// Method to bind CheckBox list
        /// </summary>
        /// <returns></returns>
        public void BindCheckBoxList(System.Web.UI.WebControls.CheckBoxList chklEntity)
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFlag", m_flag);
                m_sqlParam[1] = new SqlParameter("@parmFlag1", m_flag1);
                m_sqlParam[2] = new SqlParameter("@parmFlag2", m_flag2);
                m_sqlParam[3] = new SqlParameter("@parmFlag3", m_flag3);
                m_sqlParam[4] = new SqlParameter("@parmFlag4", m_flag4);
                //objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", ddlCodeMaster, selectedText);

                objDBTask.BindCheckBoxList("usp_BindDropDownList", m_sqlParam, "DisplayText", "DisplayValue", chklEntity, "checkBoxList");
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion BindCheckBoxList

        //Naim Khan, 08-Jan-2015, Start
        #region BindRadioButtonList

        /// <summary>
        /// Method to bind Radio Button List
        /// </summary>
        /// <returns></returns>
        public void BindRadioButtonList(System.Web.UI.WebControls.RadioButtonList rblEntity)
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFlag", m_flag);
                m_sqlParam[1] = new SqlParameter("@parmFlag1", m_flag1);
                m_sqlParam[2] = new SqlParameter("@parmFlag2", m_flag2);
                m_sqlParam[3] = new SqlParameter("@parmFlag3", m_flag3);
                m_sqlParam[4] = new SqlParameter("@parmFlag4", m_flag4);
                //objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", ddlCodeMaster, selectedText);

                objDBTask.BindRadioButtonList("usp_BindDropDownList", m_sqlParam, "DisplayText", "DisplayValue", rblEntity, "radioButtonList");
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion BindRadioButtonList

        //Naim Khan, 08-Jan-2015, End


        #region GetMenuList

        /// <summary>
        /// Method to Bind Parent Menu Name List.
        /// </summary>
        /// <returns></returns>
        public void GetParentMenuList(CheckBoxList chkMenuList, string applicationCode)
        {
            try
            {
                m_sqlParam = new SqlParameter[3];
                m_sqlParam[0] = new SqlParameter("@parmApplicationCode", applicationCode);
                m_sqlParam[1] = new SqlParameter("@parmMenuCode", string.Empty);
                m_sqlParam[2] = new SqlParameter("@parmMode", "INS");
                objDBTask.BindCheckBoxList("usp_GetParentMenu", m_sqlParam, "MenuName", "MenuCode", chkMenuList, "ParentChildMenuList");
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

        #endregion GetParentMenuList


        #region BindCheckBoxList

        /// <summary>
        /// Method to bind CheckBox list
        /// </summary>
        /// <returns></returns>
        public DataTable GetDropDownData()
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFlag", m_flag);
                m_sqlParam[1] = new SqlParameter("@parmFlag1", m_flag1);
                m_sqlParam[2] = new SqlParameter("@parmFlag2", m_flag2);
                m_sqlParam[3] = new SqlParameter("@parmFlag3", m_flag3);
                m_sqlParam[4] = new SqlParameter("@parmFlag4", m_flag4);
                //objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", ddlCodeMaster, selectedText);

                return objDBTask.ExecuteDataTable("usp_BindDropDownList", m_sqlParam, "DropDownData");
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion BindCheckBoxList

        //Naim Khan, 12-Oct-2022, Start
        #region BindDropDownList

        /// <summary>
        /// Method to bind Drop Down list
        /// </summary>
        /// <returns></returns>
        public void BindTelericComboList(Telerik.Web.UI.RadComboBox ddlEntity, string selectedText)
        {
            try
            {
                m_sqlParam = new SqlParameter[5];
                m_sqlParam[0] = new SqlParameter("@parmFlag", m_flag);
                m_sqlParam[1] = new SqlParameter("@parmFlag1", m_flag1);
                m_sqlParam[2] = new SqlParameter("@parmFlag2", m_flag2);
                m_sqlParam[3] = new SqlParameter("@parmFlag3", m_flag3);
                m_sqlParam[4] = new SqlParameter("@parmFlag4", m_flag4);
                //objDBTask.BindDropDownList("usp_GetCodeMaster", m_sqlParam, "CodeDesc", "ValueCode", ddlCodeMaster, selectedText);

                objDBTask.BindTelericComboList("usp_BindDropDownList", m_sqlParam, "DisplayText", "DisplayValue", ddlEntity, selectedText);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDBTask = null;
            }
        }
        #endregion BindDropDownList

        #region GetLOVForYNFlags

        /// <summary>
        /// Method to Bind DropDown for YN.
        /// </summary>
        /// <returns></returns>
        public void GetLOVForYNFlags(RadComboBox ddlControlId)
        {
            try
            {
                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@parmLOVCode", m_lovCode);
                objDBTask.BindTelericComboList("usp_GetLOVForYNFlags", m_sqlParam, "FlagText", "FlagValue", ddlControlId, "-All-");
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion GetLOVForYNFlags

        //Naim Khan, 12-Oct-2022, End
        public static string EncryptTripleDES(string value)
        {
            string str = string.Empty;
            try
            {
                if (value.Length > 0)
                {
                    TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();

                    MemoryStream stream = new MemoryStream();
                    CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(stream2);
                    writer.Write(value);
                    writer.Flush();
                    stream2.FlushFinalBlock();
                    stream.Flush();
                    str = Convert.ToBase64String(stream.GetBuffer(), 0, Convert.ToInt32(stream.Length));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            //byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            //string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            //return returnValue;
            return str;
        }
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        internal void GetApplicationNameList(RadComboBox ddlApplicationName)
        {
            throw new NotImplementedException();
        }
        public static string getConnectionString()
        {
            string ConnectionString = string.Empty;
            return ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
        public DataTable GetTodaySummary(string Puser,string Flags)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter sqlDa = null;
            DataTable dtTodaySummary = null;

            using (connection = new SqlConnection(getConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_TodayBalanceforChartnew";
                command.Parameters.Add("@ParamUser", SqlDbType.VarChar).Value = Puser;
                command.Parameters.Add("@ParamFlag", SqlDbType.VarChar).Value = Flags;
                sqlDa = new SqlDataAdapter(command);
                dtTodaySummary = new DataTable();
                sqlDa.Fill(dtTodaySummary);
            }
            return dtTodaySummary;
        }

        public DataTable GetmonthSummarychart(string Puser, string Flags)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter sqlDa = null;
            DataTable dtTodaySummary = null;

            using (connection = new SqlConnection(getConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_MonthwiseBalanceforChart";
                command.Parameters.Add("@ParamUser", SqlDbType.VarChar).Value = Puser;
                command.Parameters.Add("@ParamFlag", SqlDbType.VarChar).Value = Flags;
                sqlDa = new SqlDataAdapter(command);
                dtTodaySummary = new DataTable();
                sqlDa.Fill(dtTodaySummary);
            }
            return dtTodaySummary;
        }

        public DataTable ShownotifactionAllert()
        {
            try
            {

                m_sqlParam = new SqlParameter[1];
                m_sqlParam[0] = new SqlParameter("@ParamUser", LoginId);
              
                return objDBTask.ExecuteDataTable("usp_noticicationAllert", m_sqlParam, "");
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