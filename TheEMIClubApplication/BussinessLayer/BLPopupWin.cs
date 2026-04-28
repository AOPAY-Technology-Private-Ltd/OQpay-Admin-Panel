using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;
using System.Collections;
using System.Web.Configuration;
using System.Configuration;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLPopupWin
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        #region Variables

        public string m_Product;
        public string m_category;
        public string m_paramuser;
        #endregion Variables

        //Property and methods implementation (declared in IActivityLog):
        #region Property


        public string StateId { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string Zip { get; set; }

        public string Product { get { return m_Product; } set { m_Product = value; } }
        public string cantentcategory{ get { return m_category; } set { m_category = value; } }
        public string paramuser { get { return m_paramuser; } set { m_paramuser = value; } }


        public string Flag { get; set; }

        private BLAuditLog m_objAuditLog = new BLAuditLog();

        //Containership - Like Inheritence
        public BLAuditLog AuditLog { get { return m_objAuditLog; } set { m_objAuditLog = value; } }


        #endregion Property

        //Method definition          
        #region Method

        #region Manage Popup Win Detail
        /// <summary>
        /// Method For Manage Pop up Detail
        /// </summary>
        /// <returns>string</returns>
        public string ManagePopupWinDetail()
        {
            try
            {
                string xmlDoc = Common.GenerateXML(this);
                m_sqlParam = new SqlParameter[2];
                m_sqlParam[0] = new SqlParameter("@paramXmlDoc", xmlDoc);
                m_sqlParam[1] = new SqlParameter("@paramOut", SqlDbType.VarChar, 50);
                m_sqlParam[1].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800; //int.Parse(ConfigurationManager.AppSettings["CommandTimeOutExpireValue"].ToString());
                objDBTask.ExecuteScalarQuery("usp_ManagePopupWinDetail", m_sqlParam);

                return Convert.ToString(m_sqlParam[1].Value);
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

        #endregion ManagePopupWinDetail


        #endregion Method


        public short InsertProduct()
        {
            try
            {

                m_sqlParam = new SqlParameter[2];

            
                m_sqlParam[0] = new SqlParameter("@ProductName", Product);
             

                m_sqlParam[1] = new SqlParameter("@parmOut", SqlDbType.TinyInt);
                m_sqlParam[1].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertproduct]", m_sqlParam);

                return Convert.ToInt16(m_sqlParam[1].Value);

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

        public short InsertcantentCategory()
        {
            try
            {

                m_sqlParam = new SqlParameter[3];


                m_sqlParam[0] = new SqlParameter("@CategoryName", cantentcategory);
                m_sqlParam[1] = new SqlParameter("@Paramuser", paramuser);


                m_sqlParam[2] = new SqlParameter("@parmOut", SqlDbType.TinyInt);

                m_sqlParam[2].Direction = ParameterDirection.Output;

                objDBTask.ExecuteUpdate("[usp_insertCantentCategory]", m_sqlParam);

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