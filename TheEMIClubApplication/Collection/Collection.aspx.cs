using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;

using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System.Drawing;
using System.Web.UI;
using System.Configuration;
using System.Text;
using System.Web.Services;
//using BOSManagerWebApp.BussinessLayer;
using System.Collections;
using System.Xml.Linq;

namespace TheEMIClubApplication.Collection
{
    public partial class Collection : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        BLPersonalDetails objDisbursement = new BLPersonalDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    //If coming from some other page.. redierct to that same page.
                    objDisbursement.LoanID = Request.QueryString["edocelor"].Trim();
                    objDisbursement.LoanID = CryptoUtil.DecryptTripleDES(objDisbursement.LoanID.Replace(" ", "+"));
                    GetCollectionDetails();
                    //GetuserDetail();




                }


            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                // spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }


        private void GetuserDetail()
        {
            Hashtable htManageUser = new Hashtable();
            try
            {
               

                htManageUser = objDisbursement.ReterivePersonalforcollection();

                if (htManageUser.Count > 0)
                {
                    txtcustmerName.Text = Convert.ToString(htManageUser["Name"]);

                    txtMobileNo.Text = Convert.ToString(htManageUser["phone"]);
                   




                }
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
        private void GetCollectionDetails()
        {
         
            DataTable dtManageDisbursement = new DataTable();

            try
            {

                objDisbursement.LoanID = Request.QueryString["edocelor"].Trim();
                objDisbursement.LoanID = CryptoUtil.DecryptTripleDES(objDisbursement.LoanID.Replace(" ", "+"));


                dtManageDisbursement = objDisbursement.GetEmiCollection();

                if (dtManageDisbursement.Rows.Count > 0)
                {
                  
                    spnMessage.InnerHtml = string.Empty;
                    gvcollection.PageSize = PortalCommon.GetGridPageSize;
                    gvcollection.DataSource = dtManageDisbursement;
                    gvcollection.DataBind();
                    GetuserDetail();
                    for (int i = 0; i < gvcollection.Rows.Count; i++)
                    {
                        if (gvcollection.Rows[i].Cells[6].Text == "Due")//showing Activate?
                        {


                            gvcollection.Rows[i].Cells[6].ForeColor = Color.Red;
                            gvcollection.Rows[i].Cells[6].Font.Bold = true;


                        }
                        else
                        {
                            gvcollection.Rows[i].Cells[6].ForeColor = Color.Green;
                            gvcollection.Rows[i].Cells[6].Font.Bold = true;
                        }





                    }

                }
               
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvcollection.DataSource = null;
                    gvcollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //  objDisbursement.Dispose();
                objDisbursement = null;
            }
        }

       


    }

}
