using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using RestSharp.Extensions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.UI.HtmlControls;
using System.IO;


namespace TheEMIClubApplication.CustomerDetails
{
    public partial class PersonalDetails : System.Web.UI.Page
    {
        BLPersonalDetails objManageUser = new BLPersonalDetails();
        DataTable dtManageUser = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {

                          //PortalCommon.BindDropDownList(ddlcallerName, Constants.MstFlag_bankUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");
                          //  PortalCommon.BindDropDownList(ddlverification, Constants.MstFlag_bankUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");


                   
                        //If coming from some other page.. redierct to that same page.
                        objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                        objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));
                     
                        GetuserDetail();
                    // GetCustmercode();
                    //lblloanid.Text = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));
                    BindGridView(objManageUser.LoanID);
                  


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

        private void BindGridView(string loanid)
        {
            // Replace "YourConnectionString" with your actual SQL Server connection string
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            // Replace "GetImagesWithFilter" with your actual stored procedure name
            string storedProcedureName = "usp_Getdoucmentlist";

            List<ImageItem> images = GetImageItemsFromDatabase(connectionString, storedProcedureName, loanid);

            //gvImages.DataSource = images;
            //gvImages.DataBind();
        }

        private List<ImageItem> GetImageItemsFromDatabase(string connectionString, string storedProcedureName, string loanId)
        {
            List<ImageItem> images = new List<ImageItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@loanId", loanId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ImageItem image = new ImageItem
                            {
                                loanId =reader["loanId"].ToString(),
                                FileType = reader["FileType"].ToString(),
                                FileNames = reader["FileNames"].ToString(),
                                DocNumber = reader["DocNumber"].ToString(),
                                FileData = reader["FileData"].ToString()
                            };

                            images.Add(image);
                        }
                    }
                }
            }

            return images;
        }

        public class ImageItem
        {
            public string loanId { get; set; }
            public string FileType { get; set; }
            public string FileNames { get; set; }
            public string FileData { get; set; }
            public string DocNumber { get; set; }
            
        }
        protected void gvImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Retrieve the Image item from the data source
                ImageItem imageItem = (ImageItem)e.Row.DataItem;

                // Find the Image control in the template field
                Image imgDisplay = (Image)e.Row.FindControl("imgDisplay");
               

                if (imgDisplay != null)
                {
                    // Set the image source using the Base64 string
                    imgDisplay.ImageUrl = $"data:image/jpg;base64,{imageItem.FileData}";
                 
                }
                Image img = (Image)e.Row.FindControl("imgDisplay");

                // Set the CSS class for the image
                img.CssClass = "gridImage";
            }
        }

        //private void GetUserDoc()
        //{
        //    Hashtable htManageUser = new Hashtable();
        //    try
        //    {

        //        objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
        //        objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));

        //        htManageUser = objManageUser.ReterivePersonaldoc();

        //        if (htManageUser.Count > 0)
        //        {

        //            GridView1.DataSource = htManageUser;
        //            GridView1.DataBind();


        //        }
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
        #region GetRoleDetail

        private void GetuserDetail()
        {
            Hashtable htManageUser = new Hashtable();
            try
            {

                objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));

                //htManageUser = objManageUser.ReterivePersonalDetails();

                if (htManageUser.Count > 0)
                {
                    string fullName= Convert.ToString(htManageUser["UserName"]);
                    string[] nameParts = fullName.Split(' ');
                    string firstName = "";
                    string lastName = "";
                    if (nameParts.Length > 0)
                        firstName = nameParts[0];

                    if (nameParts.Length > 1)
                        lastName = string.Join(" ", nameParts.Skip(1)); 

                    txtFirstName.Text = firstName;
                    txtLastName.Text = lastName;

                    //txtFirstName.Text = Convert.ToString(htManageUser["FirstName"]);
                    //txtLastName.Text = Convert.ToString(htManageUser["LastName"]);
                    txtCustomerCode.Text= Convert.ToString(htManageUser["CustomerCode"]);
                    txtMobileNo.Text    = Convert.ToString(htManageUser["MobileNo"]);
                    txtEmailID.Text     = Convert.ToString(htManageUser["EmailID"]);
                    txtAddress.Text     = Convert.ToString(htManageUser["Address"]);
                    txtAadharNumber.Text= Convert.ToString(htManageUser["AadharNumber"]);
                    txtPANNumber.Text   = Convert.ToString(htManageUser["PANNumber"]);
                    txtCustomerCode.ReadOnly = true;
                    txtAadharNumber.ReadOnly = true;
                    txtPANNumber.ReadOnly = true;
                   // ddlActiveStatus.Text = Convert.ToString(htManageUser["ActiveStatus"]);

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


        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
        }
        //private void GetCustmercode()
        //{
        //    Hashtable htManageUser = new Hashtable();
        //    try
        //    {


        //        objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
        //        objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));
        //        htManageUser = objManageUser.Reterivecustmercode();

        //        if (htManageUser.Count > 0)
        //        {
        //            lblcustomercode.Text = Convert.ToString(htManageUser["CustCode"]);

        //        }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                objManageUser.Customercode = txtCustomerCode.Text;
                objManageUser.task=ddlActiveStatus.SelectedValue;
               var result= objManageUser.RetailerStatusUpdate();

                if (result ==0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "Approved " + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "Not Approved " + "');", true);
                }

            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                // spnMessage11.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                //spnMessage11.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }

        }
        #endregion GetRoleDetail



        protected void btnCreateEMi_click(object sender, EventArgs e)
        {
            objManageUser.LoanID = Request.QueryString["edocelor"].Trim();

            //lblloanid.Text = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));

            //Response.Redirect("../CustomerDetails/CreateEmi.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(lblloanid.Text), false);

        }
        private void UpdateTelicaller()
        {
            try
            {

                objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));


                //objManageUser.Telicaller = ddlcallerName.SelectedItem.Text.Trim();

//objManageUser.PhysicalVari = ddlcallerName.SelectedItem.Text.Trim();

                short retVal = objManageUser.UpdatePhysicalTelicaller();


                if (retVal == 1)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "True Caller And Physical Verification Person Updated SusessFully" + "');", true);
                    //ddlcallerName.Enabled = false;
                    //ddlverification.Enabled = false;
                    //btnSave.Visible = false;
                    //btnCreateEMi.Visible = true;
                }

                if (retVal == 0)
                {
                    //ddlcallerName.Enabled = true;
                    //ddlverification.Enabled = true;
                    btnSave.Visible = true;
                    //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "True Caller And Physical Verification Person Updated SusessFully" + "');", true);

                }

            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);

            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);

            }


        }

        private void ApplicationRejected()
        {
            
        }
        protected void btnReject_click(object sender, EventArgs e)
        {
            try
            {

                objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));
                objManageUser.Reject_YN = "Rejected";

                short retVal = objManageUser.UpdateApplicationRejected();

                if (retVal == 1)
                {
                   // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('"  +"Hello "+ txtName.Text  + "We appreciate your interest in securing a loan with us. After careful consideration, we regret to inform you that your loan application has not been approved at this time" + "');", true);
                }

                else if (retVal == 0)
                {

                      ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "You cannot reject this loan application because of the approved or disbursed status of loan." + "');", true);
                    
                }

                else
                {
                    PortalCommon.SaveAuditLog_PostLogin(Constants.PrgCode_Manage_User, "N", string.Empty, Constants.Activity_Update_User_Act_Inact,
                        Constants.Activity_Update_User_Act_Inact, Constants.Activity_Failed);


                    //ERR1024 - Action could not completed.
                    // spnMsg.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1024");
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
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();

        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Contact" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvContactList.GridLines = GridLines.Both;
            gvContactList.HeaderStyle.Font.Bold = true;
            gvContactList.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        private void GetContactLists()
        {
      
     

            try
            {
                lblnorecords.Visible = true;
                lblnorecords.Text = string.Empty;

               // objManageUser.Usercode = lblcustomercode.Text.Trim();
                dtManageUser = objManageUser.GetContactList();

                if (dtManageUser.Rows.Count > 0)
                {

                    gvContactList.PageSize = PortalCommon.GetGridPageSize;
                    gvContactList.DataSource = dtManageUser;
                    gvContactList.DataBind();
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    //spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    //spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    lblnorecords.Visible = true;
                    lblnorecords.Text = "Sorry! No Contact List Founds.";
                    gvContactList.DataSource = null;
                    gvContactList.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtManageUser.Dispose();
                dtManageUser = null;
            }
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {

            GetContactLists();
            // Show the Bootstrap modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#myModal').modal('show');", true);
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

        }
    }
}