using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AVFramework;
using System.Collections;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Asn1;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLDeviceMaster
    {
        private DBTask objDBTask = new DBTask();
        private SqlParameter[] m_sqlParam;

        public int rid { get; set; }
        public string Brandname { get; set; }
        public string Brandmodelname { get; set; }
        public string remark { get; set; }
        public string activestatus { get; set; }
        public string userid  { get; set; }
        public string task { get; set; }
        public int groupid { get; set; }


        public string VariantName { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal? Mrpprice { get; set; }
        public string DownPaymentPerc { get; set; }

        public string InterestPerc { get; set; }
        public string Tenure { get; set; }
        public string ProcessingFees { get; set; }

        public string avlbColors { get; set; }
        public string ImagePath { get; set; }
      

        public (string ReturnCode, string ReturnMsg) CRUDDeviceBrandMaster()
        {
            try
            {

                m_sqlParam = new SqlParameter[8];
                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@Remark", remark);
                m_sqlParam[3] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[4] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[5] = new SqlParameter("@Task", task);
                m_sqlParam[6] = new SqlParameter("@ReturnCode", SqlDbType.VarChar ,10);
                m_sqlParam[6].Direction = ParameterDirection.Output;
                m_sqlParam[7] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[7].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_CRUD_DeviceBrandMaster", m_sqlParam);
                //return Convert.ToString(m_sqlParam[6].Value);
                string returnCode = Convert.ToString(m_sqlParam[6].Value);
                string returnMsg = Convert.ToString(m_sqlParam[7].Value);

                return (returnCode, returnMsg);
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

        public (string ReturnCode, string ReturnMsg) CRUDDeviceModelMaster()
        {
            try
            {

                m_sqlParam = new SqlParameter[10];
                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
                m_sqlParam[3] = new SqlParameter("@Remark", remark);
                m_sqlParam[4] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[5] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[6] = new SqlParameter("@CreatedAt", DateTime.Now);
                m_sqlParam[7] = new SqlParameter("@Task", task);
                m_sqlParam[8] = new SqlParameter("@ReturnCode", SqlDbType.Int);
                m_sqlParam[8].Direction = ParameterDirection.Output;
                m_sqlParam[9] = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[9].Direction = ParameterDirection.Output;
                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_CRUD_DeviceModelMaster", m_sqlParam);
                //return Convert.ToString(m_sqlParam[8].Value);
                string returnCode = Convert.ToString(m_sqlParam[8].Value);
                string returnMsg = Convert.ToString(m_sqlParam[9].Value);

                return (returnCode, returnMsg);
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

        public DataTable GetAllBrands()
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@RID", rid);
            sqlParams[1] = new SqlParameter("@BrandName", Brandname);
            sqlParams[2] = new SqlParameter("@Remark", remark);
            sqlParams[3] = new SqlParameter("@ActiveStatus", activestatus);
            sqlParams[4] = new SqlParameter("@CreatedBy", DBNull.Value);
            sqlParams[5] = new SqlParameter("@Task", "GET");
            sqlParams[6] = new SqlParameter("@ReturnCode", SqlDbType.VarChar,10)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter returnMsg = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            Array.Resize(ref sqlParams, 8);
            sqlParams[7] = returnMsg;

            return objDBTask.ExecuteDataTable("usp_CRUD_DeviceBrandMaster", sqlParams, "DeviceBrandMaster");
        }

        public DataTable GetAllBrands( int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@RID", rid);
            sqlParams[1] = new SqlParameter("@BrandName", DBNull.Value);
            sqlParams[2] = new SqlParameter("@Remark", DBNull.Value);
            sqlParams[3] = new SqlParameter("@ActiveStatus", DBNull.Value);
            sqlParams[4] = new SqlParameter("@CreatedBy", DBNull.Value);
            sqlParams[5] = new SqlParameter("@Task", "GET");
            sqlParams[6] = new SqlParameter("@ReturnCode", SqlDbType.VarChar, 10)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter returnMsg = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            Array.Resize(ref sqlParams, 8);
            sqlParams[7] = returnMsg;

            return objDBTask.ExecuteDataTable("usp_CRUD_DeviceBrandMaster", sqlParams, "DeviceBrandMaster");
        }


        public DataTable GetAllDeviceModels()
        {
            
            m_sqlParam = new SqlParameter[9];
            m_sqlParam[0] = new SqlParameter("@RID", rid);
            m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
            m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
            m_sqlParam[3] = new SqlParameter("@Remark", remark);
            m_sqlParam[4] = new SqlParameter("@ActiveStatus", activestatus);
            m_sqlParam[5] = new SqlParameter("@CreatedBy", userid);
            m_sqlParam[6] = new SqlParameter("@CreatedAt", DateTime.Now);
            m_sqlParam[7] = new SqlParameter("@Task", "GET");
            m_sqlParam[8] = new SqlParameter("@ReturnCode", SqlDbType.VarChar, 10)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter returnMsg = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            objDBTask.DBCommandTimeOut = 800;
            Array.Resize(ref m_sqlParam, 10);
            m_sqlParam[9] = returnMsg;

            return objDBTask.ExecuteDataTable("usp_CRUD_DeviceModelMaster", m_sqlParam, "DeviceModelMaster");
        }

        public (string ReturnCode, string ReturnMsg) CRUDDeviceVariantMaster()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[19];

                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
                m_sqlParam[3] = new SqlParameter("@VariantName", VariantName);
                m_sqlParam[4] = new SqlParameter("@SellingPrice", SellingPrice);
                m_sqlParam[5] = new SqlParameter("@MrpPrice", Mrpprice);
                m_sqlParam[6] = new SqlParameter("@Remark", remark);
                m_sqlParam[7] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[8] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[9] = new SqlParameter("@avlbColors", avlbColors);
                m_sqlParam[10] = new SqlParameter("@ImagePath", ImagePath);
                m_sqlParam[11] = new SqlParameter("@DownPaymentPerc", DownPaymentPerc);
                m_sqlParam[12] = new SqlParameter("@InterestPerc", InterestPerc);
                m_sqlParam[13] = new SqlParameter("@Tenure", Tenure);
                m_sqlParam[14] = new SqlParameter("@ProcessingFees", ProcessingFees);
                m_sqlParam[15] = new SqlParameter("@Task", task);

                m_sqlParam[16] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10);
                m_sqlParam[16].Direction = ParameterDirection.Output;

                m_sqlParam[17] = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[17].Direction = ParameterDirection.Output;

                m_sqlParam[18] = new SqlParameter("@groupID", groupid);

                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_CRUD_DeviceVariantMaster", m_sqlParam);

                string returnCode = Convert.ToString(m_sqlParam[16].Value);
                string returnMsg = Convert.ToString(m_sqlParam[17].Value);

                return (returnCode, returnMsg);
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

        public (string ReturnCode, string ReturnMsg) CRUDDeviceVariantMaster1()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[19];

                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
                m_sqlParam[3] = new SqlParameter("@VariantName", VariantName);
                m_sqlParam[4] = new SqlParameter("@SellingPrice", SellingPrice);
                m_sqlParam[5] = new SqlParameter("@MrpPrice", Mrpprice);
                m_sqlParam[6] = new SqlParameter("@Remark", remark);
                m_sqlParam[7] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[8] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[9] = new SqlParameter("@avlbColors", avlbColors);
                m_sqlParam[10] = new SqlParameter("@ImagePath", ImagePath);
                m_sqlParam[11] = new SqlParameter("@DownPaymentPerc", DownPaymentPerc);
                m_sqlParam[12] = new SqlParameter("@InterestPerc", InterestPerc);
                m_sqlParam[13] = new SqlParameter("@Tenure", Tenure);
                m_sqlParam[14] = new SqlParameter("@ProcessingFees", ProcessingFees);
                m_sqlParam[15] = new SqlParameter("@Task", task);

                m_sqlParam[16] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10);
                m_sqlParam[16].Direction = ParameterDirection.Output;

                m_sqlParam[17] = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255);
                m_sqlParam[17].Direction = ParameterDirection.Output;

                m_sqlParam[18] = new SqlParameter("@groupID", groupid);

                objDBTask.DBCommandTimeOut = 800;
                objDBTask.ExecuteScalarQuery("usp_CRUD_DeviceVariantMasternew", m_sqlParam);

                string returnCode = Convert.ToString(m_sqlParam[16].Value);
                string returnMsg = Convert.ToString(m_sqlParam[17].Value);

                return (returnCode, returnMsg);
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




        public DataTable GetDeviceVariantMaster()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[17];

                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
                m_sqlParam[3] = new SqlParameter("@VariantName", VariantName);
                m_sqlParam[4] = new SqlParameter("@SellingPrice", SellingPrice);
                m_sqlParam[5] = new SqlParameter("@Remark", remark);
                m_sqlParam[6] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[7] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[8] = new SqlParameter("@avlbColors", avlbColors);
                m_sqlParam[9] = new SqlParameter("@ImagePath", ImagePath);
                m_sqlParam[10] = new SqlParameter("@DownPaymentPerc", DownPaymentPerc);
                m_sqlParam[11] = new SqlParameter("@InterestPerc", InterestPerc);
                m_sqlParam[12] = new SqlParameter("@Tenure", Tenure);
                m_sqlParam[13] = new SqlParameter("@ProcessingFees", ProcessingFees);
                m_sqlParam[14] = new SqlParameter("@Task", task);

                m_sqlParam[15] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter returnMsg = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };

                objDBTask.DBCommandTimeOut = 800;
                Array.Resize(ref m_sqlParam, 17);
                m_sqlParam[16] = returnMsg;

            

                return objDBTask.ExecuteDataTable("usp_CRUD_DeviceVariantMaster", m_sqlParam, "DeviceVariantMaster");
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

        public DataTable GetDeviceVariantMaster1()
        {
            try
            {
                SqlParameter[] m_sqlParam = new SqlParameter[18];

                m_sqlParam[0] = new SqlParameter("@RID", rid);
                m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
                m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
                m_sqlParam[3] = new SqlParameter("@VariantName", VariantName);
                m_sqlParam[4] = new SqlParameter("@SellingPrice", SellingPrice);
                m_sqlParam[5] = new SqlParameter("@Remark", remark);
                m_sqlParam[6] = new SqlParameter("@ActiveStatus", activestatus);
                m_sqlParam[7] = new SqlParameter("@CreatedBy", userid);
                m_sqlParam[8] = new SqlParameter("@avlbColors", avlbColors);
                m_sqlParam[9] = new SqlParameter("@ImagePath", ImagePath);
                m_sqlParam[10] = new SqlParameter("@DownPaymentPerc", DownPaymentPerc);
                m_sqlParam[11] = new SqlParameter("@InterestPerc", InterestPerc);
                m_sqlParam[12] = new SqlParameter("@Tenure", Tenure);
                m_sqlParam[13] = new SqlParameter("@ProcessingFees", ProcessingFees);
                m_sqlParam[14] = new SqlParameter("@Task", task);
                m_sqlParam[15] = new SqlParameter("@groupID", groupid);

                m_sqlParam[16] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10)
                {
                    Direction = ParameterDirection.Output
                };
                m_sqlParam[17] = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };

                objDBTask.DBCommandTimeOut = 800;

                return objDBTask.ExecuteDataTable("usp_CRUD_DeviceVariantMasternew", m_sqlParam, "DeviceVariantMaster");
            }
            catch (SqlException sqlEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public DataTable GetDeviceVariantMaster1()
        //{
        //    try
        //    {
        //        SqlParameter[] m_sqlParam = new SqlParameter[18];

        //        m_sqlParam[0] = new SqlParameter("@RID", rid);
        //        m_sqlParam[1] = new SqlParameter("@BrandName", Brandname);
        //        m_sqlParam[2] = new SqlParameter("@ModelName", Brandmodelname);
        //        m_sqlParam[3] = new SqlParameter("@VariantName", VariantName);
        //        m_sqlParam[4] = new SqlParameter("@SellingPrice", SellingPrice);
        //        m_sqlParam[5] = new SqlParameter("@Remark", remark);
        //        m_sqlParam[6] = new SqlParameter("@ActiveStatus", activestatus);
        //        m_sqlParam[7] = new SqlParameter("@CreatedBy", userid);
        //        m_sqlParam[8] = new SqlParameter("@avlbColors", avlbColors);
        //        m_sqlParam[9] = new SqlParameter("@ImagePath", ImagePath);
        //        m_sqlParam[10] = new SqlParameter("@DownPaymentPerc", DownPaymentPerc);
        //        m_sqlParam[11] = new SqlParameter("@InterestPerc", InterestPerc);
        //        m_sqlParam[12] = new SqlParameter("@Tenure", Tenure);
        //        m_sqlParam[13] = new SqlParameter("@ProcessingFees", ProcessingFees);
        //        m_sqlParam[14] = new SqlParameter("@Task", task);
        //        m_sqlParam[17] = new SqlParameter("@groupid", groupid);

        //        m_sqlParam[15] = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10)
        //        {
        //            Direction = ParameterDirection.Output
        //        };
        //        SqlParameter returnMsg = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255)
        //        {
        //            Direction = ParameterDirection.Output
        //        };

        //        objDBTask.DBCommandTimeOut = 800;
        //        Array.Resize(ref m_sqlParam, 17);
        //        m_sqlParam[16] = returnMsg;



        //        return objDBTask.ExecuteDataTable("usp_CRUD_DeviceVariantMasternew", m_sqlParam, "DeviceVariantMaster");
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }


}