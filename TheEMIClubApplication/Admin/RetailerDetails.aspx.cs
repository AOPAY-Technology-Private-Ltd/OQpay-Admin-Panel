using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.com.hisoftware.api2;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class RetailerDetails : System.Web.UI.Page
    {
        BLPersonalDetails objManageUser = new BLPersonalDetails();
        BLDealerDetails bLDealerDetails = new BLDealerDetails();
        BLPersonalDetails retailerBL = new BLPersonalDetails();
        string Image_BaseUrl = ConfigurationManager.AppSettings["ImageBaseUrl"].ToString();

        DataTable dtManageUser = new DataTable();
        public int ReturnCode { get; set; }              // From @ReturnCode OUTPUT
        public string ReturnMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (AppSessions.SessionLoginId != null)
                    {
                        //PortalCommon.BindDropDownList(ddlcallerName, Constants.MstFlag_bankUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");
                        //  PortalCommon.BindDropDownList(ddlverification, Constants.MstFlag_bankUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");
                        //If coming from some other page.. redierct to that same page.
                        objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                        objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));

                        GetuserDetail();
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
                    }


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

                objManageUser.LoanID = Request.QueryString["edocelor"].Trim();
                objManageUser.LoanID = CryptoUtil.DecryptTripleDES(objManageUser.LoanID.Replace(" ", "+"));

                htManageUser = objManageUser.ReterivePersonalDetailsWithFlage();

                //if (htManageUser.Count > 0)
                //{
                    string fullName = Convert.ToString(htManageUser["UserName"]);
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
                    txtCustomerCode.Text = Convert.ToString(htManageUser["CustomerCode"]);
                    txtMobileNo.Text = Convert.ToString(htManageUser["MobileNo"]);
                    txtEmailID.Text = Convert.ToString(htManageUser["EmailID"]);
                    txtAddress.Text = Convert.ToString(htManageUser["Address"]);
                    txtAadharNumber.Text = Convert.ToString(htManageUser["AadharNumber"]);
                    txtPANNumber.Text = Convert.ToString(htManageUser["PANNumber"]);
                    txtStoreName.Text= Convert.ToString(htManageUser["StoreName"]);
                    txtStoreAddress.Text = Convert.ToString(htManageUser["StoreAddress"]);
                string AadhaarBackPhoto = Convert.ToString(htManageUser["Adhaar_back_Photo"]);
                string AadhaarFrontPhoto = Convert.ToString(htManageUser["Adhaar_front_photo"]);
                string CancelChequePhoto =  Convert.ToString(htManageUser["cancle_cheque_Photo"]);
                string CompanyDocumentPhoto = Convert.ToString(htManageUser["company_doc_Photo"]);
                string ProfilePhoto= Convert.ToString(htManageUser["Profile_Photo"]);
                string StoreFrontPhoto =   Convert.ToString(htManageUser["store_front_Photo"]);


                    if (AadhaarBackPhoto==string.Empty)
                     {
                        img_AadhaarBackPhoto.ImageUrl = "../Images/image icon.png";
                     }
                    else
                    {
                       img_AadhaarBackPhoto.ImageUrl = Image_BaseUrl+ AadhaarBackPhoto;
                    }
                if (AadhaarFrontPhoto == string.Empty)
                {
                    img_AadhaarFrontPhoto.ImageUrl = "../Images/image icon.png";
                }
                else
                {
                    img_AadhaarFrontPhoto.ImageUrl = Image_BaseUrl + AadhaarFrontPhoto;
                }

                if (CancelChequePhoto== string.Empty)
                {
                    img_CancelChequePhoto.ImageUrl = "../Images/image icon.png";
                }
                else
                {
                    img_CancelChequePhoto.ImageUrl = Image_BaseUrl + CancelChequePhoto;
                }

                if (CompanyDocumentPhoto == string.Empty)
                {
                    img_CompanyDocumentPhoto.ImageUrl = "../Images/image icon.png";
                }
                else
                {
                    img_CompanyDocumentPhoto.ImageUrl = Image_BaseUrl + CompanyDocumentPhoto;
                 
                }

                if (ProfilePhoto == string.Empty)
                {
                    img_ProfilePhoto.ImageUrl = "../Images/image icon.png";
                }
                else
                {
                    img_ProfilePhoto.ImageUrl = Image_BaseUrl + ProfilePhoto;
                   
                }
                if (StoreFrontPhoto == string.Empty)
                {
                    img_StoreFrontPhoto.ImageUrl = "../Images/image icon.png";
                }
                else
                {
                    img_StoreFrontPhoto.ImageUrl = Image_BaseUrl + StoreFrontPhoto;
                }



                


                txtCustomerCode.ReadOnly = true;
                    txtAadharNumber.ReadOnly = true;
                    txtPANNumber.ReadOnly = true;
                    // ddlActiveStatus.Text = Convert.ToString(htManageUser["ActiveStatus"]);

                //}
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                bLDealerDetails.Mode = "UPDATE";
                bLDealerDetails.LoginId = txtCustomerCode.Text;
                bLDealerDetails.Status = ddlActiveStatus.SelectedItem.Text;
                bLDealerDetails.FirstName = txtFirstName.Text;
                bLDealerDetails.LastName = txtLastName.Text;
                bLDealerDetails.MobileNo = txtMobileNo.Text;
                bLDealerDetails.Email = txtEmailID.Text;
                bLDealerDetails.Address = txtAddress.Text;
                bLDealerDetails.storeName = txtStoreName.Text;
                bLDealerDetails.storeAddress = txtStoreAddress.Text;

                string imgProfilePhoto = img_ProfilePhoto.ImageUrl;
                string imgAadharfrontPhoto = img_AadhaarFrontPhoto.ImageUrl;
                string imgAadhaarBackPhoto = img_AadhaarBackPhoto.ImageUrl;
                string imgCancelChequePhoto = img_CancelChequePhoto.ImageUrl;
                string imgCompanyDocumentPhoto = img_CompanyDocumentPhoto.ImageUrl;
                string imgStoreFrontPhoto = img_StoreFrontPhoto.ImageUrl;

                string imgProfilePhotoUrl = imgProfilePhoto.Replace("https://api.oqpay.in", "");
                string imgAadharfrontPhotoUrl = imgAadharfrontPhoto.Replace("https://api.oqpay.in", "");
                string imgAadhaarBackPhotoUrl = imgAadhaarBackPhoto.Replace("https://api.oqpay.in", "");
                string imgCancelChequePhotoUrl = imgCancelChequePhoto.Replace("https://api.oqpay.in", "");
                string imgCompanyDocumentPhotoUrl = imgCompanyDocumentPhoto.Replace("https://api.oqpay.in", "");
                string imgStoreFrontPhotoUrl = imgStoreFrontPhoto.Replace("https://api.oqpay.in", "");



                bLDealerDetails.profilephoto = imgProfilePhotoUrl;
                bLDealerDetails.aadhaarFrontphoto = imgAadharfrontPhotoUrl;
                bLDealerDetails.aadhaarBackphoto = imgAadhaarBackPhotoUrl;
                bLDealerDetails.cancelChequephoto = imgCancelChequePhotoUrl;
                bLDealerDetails.comapnyDocphoto = imgCompanyDocumentPhotoUrl;
                bLDealerDetails.storeFrontphoto = imgStoreFrontPhotoUrl;



                var result = bLDealerDetails.RetailerStatusUpdate();

                if (result.Rows.Count > 0)
                {
                    string msg = result.Rows[0]["Message"].ToString();
                    if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
                        Response.Redirect("../CommonPages/Home.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
                }
                GetuserDetail();

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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bLDealerDetails.Mode = ddlActiveStatus.SelectedItem.Text;
                bLDealerDetails.LoginId = txtCustomerCode.Text;
                bLDealerDetails.Status = ddlActiveStatus.SelectedValue;
                bLDealerDetails.FirstName = txtFirstName.Text;
                bLDealerDetails.LastName = txtLastName.Text;
                bLDealerDetails.MobileNo = txtMobileNo.Text;
                bLDealerDetails.Email = txtEmailID.Text;
                bLDealerDetails.Address = txtAddress.Text;



                var result = bLDealerDetails.RetailerStatusUpdate();

                if (result.Rows.Count > 0)
                {
                    string msg = result.Rows[0]["Message"].ToString();

                    if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
                    }
                    GetuserDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
                }
                GetuserDetail();

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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");
        }




        ////protected void btn_AadhaarFrontPhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuAadhaarFront.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuAadhaarFront.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuAadhaarFront.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuAadhaarFront.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_AadhaarFrontPhoto.ImageUrl = relativePath;
        ////                img_AadhaarFrontPhoto.Visible = true;


        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "Adhaar_front_photo";
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);
               
        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }

        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}

        ////protected void btn_ProfilePhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuProfilePhoto.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuProfilePhoto.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuProfilePhoto.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuProfilePhoto.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_ProfilePhoto.ImageUrl = relativePath;
        ////                //img_AadhaarFrontPhoto.Visible = true;

        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "Profile_Photo";
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);
        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}

        ////protected void btn_AadhaarBackPhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuAadhaarBack.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuAadhaarBack.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuAadhaarBack.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuAadhaarBack.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_AadhaarBackPhoto.ImageUrl = relativePath;
        ////                //img_AadhaarFrontPhoto.Visible = true;
        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "Adhaar_back_Photo";  
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);

        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}

        ////protected void btn_CancelChequePhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuCancelCheque.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuCancelCheque.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuCancelCheque.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuCancelCheque.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_CancelChequePhoto.ImageUrl = relativePath;


        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "cancle_cheque_Photo";
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);

        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}

        ////protected void btn_StoreFrontPhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuStoreFront.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuStoreFront.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuStoreFront.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuStoreFront.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_StoreFrontPhoto.ImageUrl = relativePath;
        ////                img_StoreFrontPhoto.Visible = true;


        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "store_front_Photo";
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);

        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}

        ////protected void btn_CompanyDocumentPhotoUpload_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (fuCompanyDoc.HasFile)
        ////        {
        ////            try
        ////            {
        ////                // Validate file extension
        ////                string extension = Path.GetExtension(fuCompanyDoc.FileName).ToLower();
        ////                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        ////                if (!allowedExtensions.Contains(extension))
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        ////                        true
        ////                    );
        ////                    return;
        ////                }

        ////                // Generate unique file name
        ////                string fileName = Path.GetFileName(fuCompanyDoc.FileName);
        ////                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        ////                // Create folder if not exists
        ////                string folderPath = Server.MapPath("/RetailerDocumnet/");
        ////                if (!Directory.Exists(folderPath))
        ////                    Directory.CreateDirectory(folderPath);

        ////                // Save file to the server
        ////                string fullPath = Path.Combine(folderPath, uniqueFileName);
        ////                fuCompanyDoc.SaveAs(fullPath);

        ////                // Create relative URL for display
        ////                string relativePath = "/RetailerDocumnet/" + uniqueFileName;
        ////                img_CompanyDocumentPhoto.ImageUrl = relativePath;
        ////                img_CompanyDocumentPhoto.Visible = true;

        ////                string retailerCode = txtCustomerCode.Text.Trim();
        ////                string columnName = "company_doc_Photo";
        ////                string imagePath = relativePath;
        ////                Hashtable result = retailerBL.RetailerIndividualDocumentUpdate(retailerCode, columnName, imagePath);

        ////                if (result != null && result.ContainsKey("ReturnCode"))
        ////                {
        ////                    int returnCode = Convert.ToInt32(result["ReturnCode"]);
        ////                    string returnMsg = result["ReturnMsg"].ToString();

        ////                    if (returnCode == 1)
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowPopup('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                    else
        ////                    {
        ////                        ScriptManager.RegisterStartupScript(
        ////                            this,
        ////                            this.GetType(),
        ////                            "Popup",
        ////                            $"ShowError('{returnMsg}');",
        ////                            true
        ////                        );
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    ScriptManager.RegisterStartupScript(
        ////                        this,
        ////                        this.GetType(),
        ////                        "Popup",
        ////                        "ShowError('Unexpected error: no result returned from database.');",
        ////                        true
        ////                    );
        ////                }
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error: {ex.Message}');", true);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Please select an image to upload.');", true);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        //spnMessage.InnerHtml = "Error: " + ex.Message;
        ////        //spnMessage.Attributes["class"] = "text-danger";
        ////    }
        ////}


        protected void lnk_ProfilePhotoDownload_Click(object sender, EventArgs e)
        {
           
            try
            {

                string imageUrl = img_ProfilePhoto.ImageUrl;

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }

        protected void lnk_AadhaarFrontPhotoDownload_Click(object sender, EventArgs e)
        {
            
            try
            {

                string imageUrl = img_AadhaarFrontPhoto.ImageUrl;

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }

        protected void lnk_AadhaarBackPhotoDownload_Click(object sender, EventArgs e)
        {
            
            try
            {

                string imageUrl = img_AadhaarBackPhoto.ImageUrl;

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }

        protected void lnk_CancelChequePhotoDownload_Click(object sender, EventArgs e)
        {
           
            try
            {

                string imageUrl = img_CancelChequePhoto.ImageUrl;

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }

        protected void lnk_StoreFrontPhotoDownload_Click(object sender, EventArgs e)
        {

            try
            {

                string imageUrl = img_StoreFrontPhoto.ImageUrl;

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }
        protected void lnk_CompanyDocumentPhotoDownload_Click(object sender, EventArgs e)
        {
            try
            {
            
                string imageUrl = img_CompanyDocumentPhoto.ImageUrl;

                //if (imageUrl == "")
                //    imageUrl = "";
                //else
                //{
                //    imageUrl
                //}

                string filePath = Server.MapPath(imageUrl);

                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                        "ShowError('File not found on server.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                    $"ShowError('Error downloading file: {ex.Message}');", true);
            }
        }


    }
}