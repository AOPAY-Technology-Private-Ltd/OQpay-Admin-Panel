using AVFramework;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.com.hisoftware.api2;
using Telerik.Web.UI.ExportInfrastructure;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.Model;
using static System.Net.Mime.MediaTypeNames;


namespace TheEMIClubApplication.MasterPage
{
    public partial class CustomerMaster : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLCustomerMaster objCustomerMst = new BLCustomerMaster();
       
        protected void Page_Load(object sender, EventArgs e) 
        {
            if(!IsPostBack)
                
            {

                ////objCustomerMst.RID = int.TryParse(Request.QueryString["edocelor"], out int rid) ? rid : (int?)null;
                ////objCustomerMst.RID = CryptoUtil.DecryptTripleDES(objCustomerMst.RID);
            
                string encryptedRID = Server.UrlDecode(Request.QueryString["edocelor"]);
                if (encryptedRID !=null)
                {
                    //string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
                    //if (int.TryParse(decryptedRID, out int rid))
                    if (int.TryParse(encryptedRID, out int rid))
                    {
                        objCustomerMst.RID = rid;
                        BindGrid();
                       
                    }
                    else
                    {
                        objCustomerMst.RID = null; 
                    }
                }
                else
                {
                    BindGrid();

                    //PortalCommon.BindDropDownList(ddlStateName, Constants.MstFlag_StateList, Constants.MstFlag_Countryid
                    //       , string.Empty, string.Empty, string.Empty, string.Empty, "-- Select State --");
                    //PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL,
                    //string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                }

            }
        }

        private void BindGrid()
        {
            objCustomerMst.Mode = "GET";

            DataTable dt = objCustomerMst.GetCustomerDeviceMst();

            txtColor.Text = dt.Rows[0]["color"].ToString();
            txtCustomercode.Text = dt.Rows[0]["CustomerCode"].ToString();
            txtFirstname.Text = dt.Rows[0]["FirstName"].ToString();
            txtMiddlename.Text = dt.Rows[0]["MiddleName"].ToString();
            txtLastname.Text = dt.Rows[0]["LastName"].ToString();
            txtPrimaruMobileno.Text = dt.Rows[0]["PrimaryMobileNumber"].ToString();
            txtEmail.Text = dt.Rows[0]["EMailID"].ToString();
            txtFlatno.Text = dt.Rows[0]["FlatNo"].ToString();
            txtAreaSector.Text = dt.Rows[0]["AearSector"].ToString();
            txtCurrentAddress.Text = dt.Rows[0]["CurrentAddress"].ToString();
            txtPinCode.Text = dt.Rows[0]["PinCode"].ToString();
            txtCountry.Text = dt.Rows[0]["Country"].ToString();
            txtState.Text = dt.Rows[0]["StateName"].ToString();
            txtCity.Text = dt.Rows[0]["CityName"].ToString();
            txtBrand.Text = dt.Rows[0]["BrandName"].ToString();
            txtmodel.Text = dt.Rows[0]["ModelName"].ToString();
            txtVariant.Text = dt.Rows[0]["ModelVariant"].ToString();
            txtPrimaryVerified.Text= dt.Rows[0]["PrimaryMobileVerified"].ToString();
           // txtPrimaryOTP.Text= dt.Rows[0]["PrimaryOTP"].ToString();
            txtAltMobile.Text= dt.Rows[0]["AlternateMobileNumber"].ToString();
            //txtAltOTP.Text= dt.Rows[0]["AlternateMobileOTP"].ToString();
            //txtAltVerified.Text = dt.Rows[0]["PAlternateMobileVerified"].ToString();
            txtAaadharno.Text = dt.Rows[0]["AadharNumber"].ToString();
            txtAadharVerified.Text = dt.Rows[0]["AadharNumberVerified"].ToString();
            txtPan.Text = dt.Rows[0]["PANNumber"].ToString();
            txtPANVerified.Text = dt.Rows[0]["PANNumberVerified"].ToString();
            txtSellingPrice.Text = dt.Rows[0]["SellingPrice"].ToString();
            txtdownPayment.Text = dt.Rows[0]["DownPayment"].ToString();
            txtTenure.Text = dt.Rows[0]["Tenure"].ToString();
            txtEMIAmount.Text = dt.Rows[0]["EMIAmount"].ToString();
            txtIMEIOne.Text = dt.Rows[0]["IMEINumber1"].ToString();
            txtIMEITwo.Text = dt.Rows[0]["IMEINumber2"].ToString();
            txtAccountno.Text = dt.Rows[0]["AccountNumber"].ToString();
            txtIFSC.Text = dt.Rows[0]["BankIFSCCode"].ToString();
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
            txtBank.Text = dt.Rows[0]["BankName"].ToString();
            txtAccountType.Text = dt.Rows[0]["AccountType"].ToString();
            txtRefName.Text = dt.Rows[0]["RefName"].ToString();
            txtRefMobileno.Text = dt.Rows[0]["RefmobileNo"].ToString();
            txtRefRelationship.Text = dt.Rows[0]["RefRelationShip"].ToString();
            txtRefAddress.Text = dt.Rows[0]["RefAddress"].ToString();
            //txtCibilScore.Text= dt.Rows[0]["CibilScore"].ToString();
            decimal CibilScore = Convert.ToDecimal(dt.Rows[0]["CibilScore"]);
            txtCibilScore.Text= CibilScore.ToString();
            //txtDebitCreditCard.Text = dt.Rows[0]["DebitOrCreditCard"].ToString();
            //txtUPIMandate.Text = dt.Rows[0]["UPIMandate"].ToString();

            imgCustPhoto.ImageUrl = ImageUrl + dt.Rows[0]["CustPhoto_path"].ToString();
            //Custimgzoom.Src = imgCustPhoto.ImageUrl;

            imgAadharfrontphoto.ImageUrl = ImageUrl + dt.Rows[0]["CustAadharPhoto_Path"].ToString();
            //Aadharfrontimgzoom.Src = imgAadharfrontphoto.ImageUrl;

            imgAadharbackphoto.ImageUrl = ImageUrl + dt.Rows[0]["CustAadharBackPhoto_Path"].ToString();
            //Aadharbackimgzoom.Src = imgAadharbackphoto.ImageUrl;

            imgIMEI.ImageUrl = ImageUrl + dt.Rows[0]["IMEINumber_PhotoPath"].ToString();
            //IMEIimgzoom.Src = imgIMEI.ImageUrl;

            imgIMEI1.ImageUrl = ImageUrl + dt.Rows[0]["IMEINumber1_SealPhotoPath"].ToString();
            //IMEI1imgzoom.Src = imgIMEI1.ImageUrl;

            imgIMEI2.ImageUrl = ImageUrl + dt.Rows[0]["IMEINumber2_SealPhotoPath"].ToString();
            //IMEI2imgzoom.Src = imgIMEI2.ImageUrl;

            imgInvoice.ImageUrl = ImageUrl + dt.Rows[0]["Invoive_Path"].ToString();
            //Invoiceimgzoom.Src = imgInvoice.ImageUrl;

            imgPan.ImageUrl = ImageUrl + dt.Rows[0]["CustPanNumberPhoto_Path"].ToString();

            string cibilScoreDetails = dt.Rows[0]["CibilApiResponse"].ToString();

            string status = dt.Rows[0]["ActiveStatus"].ToString();

            ddlGracePeriod.SelectedValue = dt.Rows[0]["CustomerGracePeriodDays"].ToString();

            //Panimgzoom.Src = imgPan.ImageUrl;
            //s tring country = dt.Rows[0]["Country"].ToString();
            //string state = dt.Rows[0]["StateName"].ToString();
            //string city = dt.Rows[0]["CityName"].ToString();
            //string brand = dt.Rows[0]["BrandName"].ToString();
            //string model = dt.Rows[0]["ModelName"].ToString();
            //string variant = dt.Rows[0]["ModelVariant"].ToString();

            //if (!string.IsNullOrEmpty(country) && country != null)
            //{
            //    PortalCommon.BindDropDownList(ddlCountryName, Constants.MstFlag_CountryList, country
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlCountryName.SelectedItem.Text = country.ToString();

            //}
            //if (!string.IsNullOrEmpty(state) && state != null)
            //{
            //    PortalCommon.BindDropDownList(ddlStateName, Constants.MstFlag_StateList, state
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlStateName.SelectedItem.Text = state.ToString();

            //}
            //if (!string.IsNullOrEmpty(city) && city != null)
            //{
            //    PortalCommon.BindDropDownList(ddlStateName, Constants.MstFlag_CityList, city
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlCityName.SelectedItem.Text = city.ToString();

            //}
            //if (!string.IsNullOrEmpty(brand) && brand != null)
            //{
            //    PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL, brand
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlBrand.SelectedItem.Text = brand.ToString();
            //}
            //if (!string.IsNullOrEmpty(model) && model != null)
            //{
            //    PortalCommon.BindDropDownList(ddlModel, Constants.Flage_ModelNameDDL, model
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlModel.SelectedItem.Text = model.ToString();
            //}
            //if (!string.IsNullOrEmpty(variant) && variant != null)
            //{
            //    PortalCommon.BindDropDownList(ddlVariant, Constants.Flage_VariantNameDDL, variant
            //        , string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //    ddlVariant.SelectedItem.Text = variant.ToString();
            //}
            //  ddlCountryName.SelectedIndex = ddlCountryName.Items.IndexOf(ddlCountryName.Items.FindByText(dt.Rows[0]["Country"].ToString()));
            //ddlCountryName.SelectedItem.Text= dt.Rows[0]["Country"].ToString();
            //ddlCountryName.DataSource = objCustomerMst.GetCustomerDeviceMst();
            //ddlCountryName.DataTextField = dt.Rows[0]["Country"].ToString();
            //ddlCountryName.DataValueField = "CountryID";
            //ddlCountryName.DataBind();
            //ddlCountryName.Items.Insert(0, new ListItem(ddlCountryName.Text, ""));

            //txtAadhar.Text = dt.Rows[0]["AadharNumber"].ToString();
            //txtPAN.Text = dt.Rows[0]["PANNumber"].ToString();
            //txtSellingPrice.Text = dt.Rows[0]["SellingPrice"].ToString();
            //txtDownPayment.Text = dt.Rows[0]["DownPayment"].ToString();
            //txtTenure.Text = dt.Rows[0]["Tenure"].ToString();
            //txtEMI.Text = dt.Rows[0]["EMIAmount"].ToString();
            //txtIMEI1.Text = dt.Rows[0]["IMEINumber1"].ToString();
            //txtIMEI2.Text = dt.Rows[0]["IMEINumber2"].ToString();
            //txtAccountNo.Text = dt.Rows[0]["AccountNumber"].ToString();
            //txtIFSC.Text = dt.Rows[0]["BankIFSCCode"].ToString();
            //txtBankName.Text = dt.Rows[0]["BankName"].ToString();
            //txtRefName.Text = dt.Rows[0]["RefName"].ToString();
            //txtRefRelation.Text = dt.Rows[0]["RefRelationShip"].ToString();
            //txtRefMobile.Text = dt.Rows[0]["RefmobileNo"].ToString();

            //gvDevices.DataSource = dt;
            //gvDevices.DataBind();


            if (CibilScore < 500 && status=="Pending")
            {
                btnAction.Visible = true;

            //  btnRejected.Visible = true;
            }
        }

        //protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    string ddlStateid = ddlStateName.SelectedValue;

        //    try
        //    {
        //        PortalCommon.BindDropDownList(ddlCityName, Constants.MstFlag_CityList, ddlStateid
        //            , string.Empty, string.Empty, string.Empty, string.Empty, "-- Select City--");
        //    }
        //    catch( Exception ex)
        //    {

        //    }
        //}

        //protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string brandname = ddlBrand.SelectedItem.Text;

        //    try
        //    {
        //        PortalCommon.BindDropDownList(ddlModel, Constants.Flage_ModelNameDDL, brandname
        //            , string.Empty, string.Empty, string.Empty, string.Empty,"-- Select Model --");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string brandname = ddlBrand.SelectedItem.Text;

        //    try
        //    {
        //        PortalCommon.BindDropDownList(ddlVariant, Constants.Flage_VariantNameDDL, brandname
        //            , string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Variant --");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnUpdateGracePeriod_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlGracePeriod.SelectedValue))
                {
                    SweetAlert("warning", "Please select grace period days");
                    return;
                }

                int graceDays = Convert.ToInt32(ddlGracePeriod.SelectedValue);

                // 🔑 SET VALUES
                objCustomerMst.customercode = txtCustomercode.Text.Trim(); // or from hidden field / session
                objCustomerMst.graceday = graceDays;

                // 🔁 CALL DB METHOD
                DataTable dt = objCustomerMst.UpdateCustomerGracetime();

                // ✅ CHECK RESULT
                if (dt != null && dt.Rows.Count > 0)
                {
                    int rowsAffected = Convert.ToInt32(dt.Rows[0]["RowsAffected"]);

                    if (rowsAffected > 0)
                    {
                        SweetAlert("success", $"Grace period updated to {graceDays} days");
                    }
                    else
                    {
                        SweetAlert("warning", "No record updated. Please verify customer.");
                    }
                }
                else
                {
                    SweetAlert("error", "Something went wrong while updating grace period");
                }
            }
            catch (Exception ex)
            {
                SweetAlert("error", "Error: " + ex.Message);
            }
        }



        protected void gvDevices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Page")
                    return;

                //LinkButton lnk = (LinkButton)e.CommandSource;
                //GridViewRow row = (GridViewRow)lnk.NamingContainer;
                //if (row.RowIndex >= 0)
                //{
                //if (row.Cells.Count >0)
                //{

                //int rid = Convert.ToInt32(gvDevices.DataKeys[row.RowIndex].Value);

                //string brandName = row.Cells[0].Text.Trim();
                //string modelName = row.Cells[1].Text.Trim();
                //string variantName = row.Cells[2].Text.Trim();
                //string color = row.Cells[3].Text.Trim();
                //string sellingPrice = row.Cells[4].Text.Replace("₹", "").Replace(",", "").Trim();
                //string downPayment = row.Cells[5].Text.Trim();
                //string tenure = row.Cells[6].Text.Trim();
                //string emiAmount = row.Cells[7].Text.Trim();
                //string imei1 = row.Cells[8].Text.Trim();
                //string imei1SealPhotoPath = row.Cells[9].Text.Trim();
                //string imei2 = row.Cells[10].Text.Trim();
                //string imei2SealPhotoPath = row.Cells[11].Text.Trim();
                //string invoicePath = row.Cells[12].Text.Trim();
                //string imeiPhotoPath = row.Cells[13].Text.Trim();
                //string accountNumber = row.Cells[14].Text.Trim();
                //string ifsc = row.Cells[15].Text.Trim();
                //string bankName = row.Cells[16].Text.Trim();
                //string accountType = row.Cells[17].Text.Trim();
                //string branchName = row.Cells[18].Text.Trim();
                //string refName = row.Cells[19].Text.Trim();
                //string refRelationship = row.Cells[20].Text.Trim();
                //string refMobileNo = row.Cells[21].Text.Trim();
                //string refAddress = row.Cells[22].Text.Trim();
                //string debitOrCreditCard = row.Cells[23].Text.Trim();
                //string upiMandate = row.Cells[24].Text.Trim();
                //string createdBy = row.Cells[25].Text.Trim();
                //string createdAt = row.Cells[26].Text.Trim();
                //string activeStatus = row.Cells[27].Text.Trim();

                //}
                //}
                if (e.CommandName == "EditRow")
                {


                    //int rid = Convert.ToInt32(e.CommandArgument);
                    //objCustomerMst.RID = rid;
                    //objCustomerMst.Mode = "GET";
                    //DataTable dt = objCustomerMst.GetCustomerDeviceMst();
                    //if (dt.Rows.Count > 0)
                    //{
                    //    DataRow dr = dt.Rows[0];

                    //    txtFirstName.Text = dr["FirstName"].ToString();
                    //    txtMiddleName.Text = dr["MiddleName"].ToString();
                    //    txtLastName.Text = dr["LastName"].ToString();
                    //    txtPrimaryMobile.Text = dr["PrimaryMobileNumber"].ToString();
                    //    txtEmail.Text = dr["EMailID"].ToString();
                    //    ddlBrand.SelectedValue = dr["BrandId"].ToString();   // Assuming Brand dropdown
                    //    ddlModel.SelectedValue = dr["ModelId"].ToString();
                    //    txtIMEI1.Text = dr["IMEINumber1"].ToString();
                    //    txtAccountNo.Text = dr["AccountNumber"].ToString();
                    //    txtAadhar.Text = dr["AadharNumber"].ToString();
                    //    txtPAN.Text = dr["PANNumber"].ToString();
                    //    // And so on for all other form fields...

                    //    hfRID.Value = rid.ToString(); // HiddenField to store RID for update
                    //}


                    LinkButton lnk = (LinkButton)e.CommandSource;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;

                    int rowIndex = row.RowIndex;

//                    if (rowIndex >= 0 && rowIndex < gvDevices.DataKeys.Count)
//                    {
//                        // Safe: now you can access DataKeys
//                        string middleName = gvDevices.DataKeys[rowIndex]["MiddleName"]?.ToString();
//                        string lastName = gvDevices.DataKeys[rowIndex]["LastName"]?.ToString();
//                        string altMobile = gvDevices.DataKeys[rowIndex]["AlternateMobileNumber"]?.ToString();
//                        string altOTP = gvDevices.DataKeys[rowIndex]["AlternateMobileOTP"]?.ToString();
//                        string aadhar = gvDevices.DataKeys[rowIndex]["AadharNumber"]?.ToString();
//                        string pan = gvDevices.DataKeys[rowIndex]["PANNumber"]?.ToString();
//                        string color = gvDevices.DataKeys[rowIndex]["color"]?.ToString();
//                        string invoicePath = gvDevices.DataKeys[rowIndex]["Invoive_Path"]?.ToString();
//                        string IMEIPhoto = gvDevices.DataKeys[rowIndex]["IMEINumber_PhotoPath"]?.ToString();
//                        string AccountNo = gvDevices.DataKeys[rowIndex]["AccountNumber"]?.ToString();
//                        string ifscNo = gvDevices.DataKeys[rowIndex]["BankIFSCCode"]?.ToString();
//                        string BankName = gvDevices.DataKeys[rowIndex]["BankName"]?.ToString();
//                        string Accounttype = gvDevices.DataKeys[rowIndex]["AccountType"]?.ToString();
//                        string branchName = gvDevices.DataKeys[rowIndex]["BranchName"]?.ToString();
//                        string refName = gvDevices.DataKeys[rowIndex]["RefName"]?.ToString();
//                        string refRelation = gvDevices.DataKeys[rowIndex]["RefRelationShip"]?.ToString();
//                       string refMobileno = gvDevices.DataKeys[rowIndex]["RefmobileNo"]?.ToString();
//                        string refAddress = gvDevices.DataKeys[rowIndex]["RefAddress"]?.ToString();
//                       string debitOrCedit = gvDevices.DataKeys[rowIndex]["DebitOrCreditCard"]?.ToString();
//                        string Upi = gvDevices.DataKeys[rowIndex]["UPIMandate"]?.ToString();
//                        //hfCreatedBy.Value = gvDevices.DataKeys[rowIndex]["CreatedBy"]?.ToString();
//                        //hfCreatedAt.Value = gvDevices.DataKeys[rowIndex]["CreatedAt"]?.ToString();

//                        // Fill textboxes
                      
//                        txtMiddleName.Text = middleName;
//                        txtLastName.Text = lastName;
//                        //txtAltMobile.Text = altMobile;
//                        //txtAltOTP.Text = altOTP;
//                        txtAadhar.Text = aadhar;
//                        //txtPan.Text = pan;
//                        txtColor.Text = color;
//                        txtCustomerCode.Text = row.Cells[0].Text;
//                        txtFirstName.Text= row.Cells[1].Text;
//                        txtMiddleName.Text = middleName;
//                        txtLastName.Text = lastName;
//                        txtPrimaryMobile   .Text= row.Cells[2].Text;
//txtEmail.Text = row.Cells[3].Text;
//txtFlatNo          .Text="";
//txtAreaSector      .Text="";
//txtCurrentAddress  .Text="";
//txtPinCode         .Text="";
////ddlCountryName     .Text="";
////ddlStateName       .Text="";
////ddlCityName        .Text="";
//txtAadhar          .Text= row.Cells[8].Text;
//txtPAN             .Text= row.Cells[9].Text;
////ddlBrand.SelectedItem.Text= row.Cells[4].Text;
////ddlModel.SelectedItem.Text = row.Cells[5].Text;
////ddlVariant.SelectedItem.Text = row.Cells[6].Text;
//txtSellingPrice    .Text="";
//txtDownPayment     .Text="";
//txtTenure          .Text="";
//txtEMI             .Text="";
//txtIMEI1           .Text= row.Cells[7].Text;
//txtIMEI2           .Text="";
//txtAccountNo       .Text= row.Cells[11].Text;
//txtIFSC            .Text= row.Cells[12].Text;
//txtBankName        .Text= row.Cells[10].Text;
//txtRefName         .Text= refName;
//txtRefRelation     .Text= refRelation;
//txtRefMobile       .Text= refMobileno;
////fuCustomerPhoto    .Text="";
////        .Text="";
//                       // fuInvoice.Text = "";

//                    }
//                    else
//                    {
                    
//                        System.Diagnostics.Debug.WriteLine("Invalid rowIndex: " + rowIndex);
//                    }

                }

            }

            catch (Exception ex)
            {

            }

        }

    

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //string encryptedRID = Request.QueryString["edocelor"];
                ////string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
                //if (int.TryParse(encryptedRID, out int rid))
                //{
                //    objCustomerMst.RID = rid;
                //}
                //else
                //{
                //    objCustomerMst.RID = null;
                //}

                //objCustomerMst.Userid = txtCustomercode.Text.ToString().Trim();
                //objCustomerMst.ActiveStatus = "InActive";
                //objCustomerMst.Mode = "DELETE";
                //var result=objCustomerMst.UpdateCustomerDeviceMst();
                //if (result.Rows.Count > 0)
                //{
                //    if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Deactivated Successfully');", true);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Not deactivated');", true);
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
                //}
                Response.Redirect("../CommonPages/Home.aspx");
            }
            catch (Exception ex)
            {

            }

        }

        //protected void btnDownloadCustPhoto_Click(object sender, EventArgs e)
        //{
        //    if (btnDownloadCustPhoto.Text.ToLower().Trim() == "upload")
        //    {
        //        if (FileUpload1.HasFile)
        //        {
        //            string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
        //            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
        //            {
        //                string fileName = Path.GetFileName(FileUpload1.FileName);
        //                string savePath = Server.MapPath("~/CustomerUploads/" + fileName);
        //                FileUpload1.SaveAs(savePath);
        //                imgCustPhoto.ImageUrl = "~/CustomerUploads/" + fileName;

        //                lblCustPhotoError.Text = "File uploaded successfully.";
        //                lblCustPhotoError.CssClass = "text-success";
        //            }
        //            else
        //            {
        //                lblCustPhotoError.Text = "Only JPG, JPEG, and PNG files are allowed.";
        //                lblCustPhotoError.CssClass = "text-danger";
        //            }
        //        }
        //        else
        //        {
        //            lblCustPhotoError.Text = "Please select an image to upload.";
        //            lblCustPhotoError.CssClass = "text-danger";
        //            FileUpload1.Focus();
        //        }
        //    }
        //    else if (btnDownloadCustPhoto.Text.ToLower().Trim() == "download")
        //    {
        //        string imageUrl = imgCustPhoto.ImageUrl;

        //        try
        //        {
        //            string fileName = Path.GetFileName(imageUrl);

        //            // Check if it's a virtual path
        //            if (!imageUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        //            {
        //                imageUrl = ResolveUrl(imageUrl);
        //                string serverPath = Server.MapPath(imageUrl);
        //                byte[] fileBytes = File.ReadAllBytes(serverPath);

        //                Response.Clear();
        //                Response.ContentType = "application/octet-stream";
        //                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
        //                Response.BinaryWrite(fileBytes);
        //                Response.End();
        //            }
        //            else
        //            {
        //                using (WebClient client = new WebClient())
        //                {
        //                    byte[] fileBytes = client.DownloadData(imageUrl);
        //                    Response.Clear();
        //                    Response.ContentType = "application/octet-stream";
        //                    Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
        //                    Response.BinaryWrite(fileBytes);
        //                    Response.End();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblCustPhotoError.Text = "Download failed: " + ex.Message;
        //            lblCustPhotoError.CssClass = "text-danger";
        //        }
        //    }
        //}

        protected void btnDownloadCustPhoto_Click(object sender, EventArgs e)
        {
            //string filePath = imgCustPhoto.ImageUrl;//.Replace("https://oqapi.bos.center", ""); // Get path from hidden field

            //string filePath = imagepath;

            //if (!string.IsNullOrEmpty(filePath) && File.Exists(Server.MapPath(filePath)))
            //{
            //    string fullPath = Server.MapPath(filePath);
            //    string fileName = Path.GetFileName(fullPath);

            //    Response.Clear();
            //    Response.ContentType = "application/octet-stream";
            //    Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            //    Response.TransmitFile(fullPath);
            //    Response.End();
            //}

            //if (btnDownloadCustPhoto.Text.ToLower().Trim() == "upload")
            //{
            //    string file = FileUpload1.FileName;
            //    if (file != null)
            //    {
            //        lblCustPhotoError.Text = "Please Upload the image file";
            //        lblCustPhotoError.CssClass = "text-danger";
            //        FileUpload1.Focus();
            //    }
            //    else
            //    {

            //    }

            //}

            //if (btnDownloadCustPhoto.Text.ToLower().Trim() == "download")
            //{
                string imageUrl = imgCustPhoto.ImageUrl; // Full external URL
                using (WebClient client = new WebClient())
                {
                    byte[] fileBytes = client.DownloadData(imageUrl);
                    string fileName = Path.GetFileName(imageUrl);

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.BinaryWrite(fileBytes);
                    Response.End();
                }
           // }


        }
        protected void btnRemoveCustPhoto_Click(object sender, EventArgs e)
        {
            imgCustPhoto.ImageUrl = "../Images/image icon.png";
            btnDownloadCustPhoto.Text = "Upload";
            //string filePath = imgCustPhoto.ImageUrl;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); 
            //    }
             
            //}
        }
        protected void btnDownloadAadharfrontphoto_Click(object sender, EventArgs e)
        {
            string imageUrl = imgAadharfrontphoto.ImageUrl; 
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveAadharfrontphoto_Click(object sender, EventArgs e)
        {
            imgAadharfrontphoto.ImageUrl = "../Images/image icon.png";
            btnDownloadAadharfrontphoto.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    //hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}
        }
        protected void btnDownloadAadharbackphoto_Click(object sender, EventArgs e)
        {
            string imageUrl = imgAadharbackphoto.ImageUrl;
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveAadharbackphoto_Click(object sender, EventArgs e)
        {
            imgAadharbackphoto.ImageUrl = "../Images/image icon.png";
            btnDownloadAadharbackphoto.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    //hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}
        }
        protected void btnDownloadIMEI1_Click(object sender, EventArgs e)
        {
            string imageUrl = imgIMEI1.ImageUrl; 
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveIMEI1_Click(object sender, EventArgs e)
        {
            imgIMEI1.ImageUrl = "../Images/image icon.png";
            btnDownloadIMEI1.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}

        }
        protected void btnDownloadIMEI2_Click(object sender, EventArgs e)
        {
            string imageUrl = imgIMEI2.ImageUrl; 
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveIMEI2_Click(object sender, EventArgs e)
        {
            imgIMEI2.ImageUrl = "../Images/image icon.png";
            btnDownloadIMEI2.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}
        }
        protected void btnDownloadIMEI_Click(object sender, EventArgs e)
        {
            string imageUrl = imgIMEI.ImageUrl; 
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveIMEI_Click(object sender, EventArgs e)
        {
            imgIMEI.ImageUrl = "../Images/image icon.png";
            btnDownloadIMEI.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}
        }
        protected void btnDownloadInvoice_Click(object sender, EventArgs e)
        {
            string imageUrl = imgInvoice.ImageUrl;
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }
        protected void btnRemoveInvoice_Click(object sender, EventArgs e)
        {
            imgInvoice.ImageUrl = "../Images/image icon.png";
            btnDownloadInvoice.Text = "Upload";
            //string filePath = hfCustPhotoPath.Value;

            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    string fullPath = Server.MapPath(filePath);

            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath); // Delete file
            //    }

            //    hfCustPhotoPath.Value = ""; // Clear hidden field
            //    imgCustPhoto.ImageUrl = ""; // Remove image preview
            //}
        }

        private void SweetAlert(string type, string message)
        {
            string script;

            if (type == "success")
            {
                script = "showSuccess('" + message + "')";
            }
            else if (type == "warning")
            {
                script = "showWarning('" + message + "')";
            }
            else if (type == "error")
            {
                script = "showError('" + message + "')";
            }
            else
            {
                script = "showError('" + message + "')";
            }

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                Guid.NewGuid().ToString(),
                script,
                true
            );
        }



        protected void Action_Click(object sender, EventArgs e)
        {
            try
            {
                string action = ((Button)sender).CommandArgument;

                if (!int.TryParse(Request.QueryString["edocelor"], out int rid))
                {
                    SweetAlert("warning", "Invalid request");
                    return;
                }

                objCustomerMst.RID = rid;
                objCustomerMst.Userid = txtCustomercode.Text.Trim();
                objCustomerMst.Mode = action;

                var result = objCustomerMst.UpdateCustomerDeviceMst();

                if (result?.Rows.Count > 0 &&
                    result.Rows[0]["Statuss"].ToString() == "SUCCESS")
                {
                    SweetAlert(
                        "success",
                        action == "APPROVED"
                            ? "Approved successfully"
                            : "Rejected successfully"
                    );
                }
                else
                {
                    SweetAlert("error", "Operation failed");
                }

                BindGrid();
            }
            catch
            {
                SweetAlert("error", "Something went wrong");
            }
        }



        //protected void btnApproveed_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string encryptedRID = Request.QueryString["edocelor"];
        //        //string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
        //        if (int.TryParse(encryptedRID, out int rid))
        //        {
        //            objCustomerMst.RID = rid;
        //        }
        //        else
        //        {
        //            objCustomerMst.RID = null;
        //        }

        //        objCustomerMst.Userid = txtCustomercode.Text;
        //        objCustomerMst.Mode = "APPROVED";
        //        var result = objCustomerMst.UpdateCustomerDeviceMst();
        //        if (result.Rows.Count>0)
        //        {

        //            if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Approved Successfully');", true);
        //            }
        //           else
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Not Approved');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
        //        }
        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void btnRejected_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string encryptedRID = Request.QueryString["edocelor"];
        //        //string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
        //        if (int.TryParse(encryptedRID, out int rid))
        //        {
        //            objCustomerMst.RID = rid;
        //        }
        //        else
        //        {
        //            objCustomerMst.RID = null;
        //        }

        //        objCustomerMst.Userid = txtCustomercode.Text;
        //        objCustomerMst.Mode = "Rejected";
        //        var result = objCustomerMst.UpdateCustomerDeviceMst();
        //        if (result.Rows.Count > 0)
        //        {

        //            if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Rejected Successfully');", true);
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Not Rejected');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
        //        }
        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string encryptedRID = Request.QueryString["edocelor"];
                //string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
                if (int.TryParse(encryptedRID, out int rid))
                {
                    objCustomerMst.RID = rid;
                }
                else
                {
                    objCustomerMst.RID = null;
                }


                objCustomerMst.FirstName = txtFirstname.Text.ToString().Trim();
                objCustomerMst.MiddleName = txtMiddlename.Text.ToString().Trim();
                objCustomerMst.LastName = txtLastname.Text.ToString().Trim();
                objCustomerMst.PrimaryMobileNumber = txtPrimaruMobileno.Text.ToString().Trim();
                //objCustomerMst.PrimaryOTP = txtPrimaryOTP.Text.ToString().Trim();
                objCustomerMst.PrimaryMobileVerified = txtPrimaryVerified.Text.ToString().Trim();
                //objCustomerMst.AlternateMobileNumber = txtAltMobile.Text.ToString().Trim();
                //objCustomerMst.AlternateMobileOTP = txtAltOTP.Text.ToString().Trim(); ;
                //objCustomerMst.PAlternateMobileVerified = txtAltVerified.Text.ToString().Trim();
                objCustomerMst.EMailID = txtEmail.Text.ToString().Trim();
                objCustomerMst.FlatNo = txtFlatno.Text.ToString().Trim();
                objCustomerMst.AearSector = txtAreaSector.Text.ToString().Trim();
                objCustomerMst.CurrentAddress = txtCurrentAddress.Text.ToString().Trim();
                objCustomerMst.PinCode = txtPinCode.Text.ToString().Trim();
                objCustomerMst.Country = txtCountry.Text.ToString().Trim();
                objCustomerMst.StateName = txtState.Text.ToString().Trim();
                objCustomerMst.CityName = txtCity.Text.ToString().Trim();
                objCustomerMst.BrandName = txtBrand.Text.ToString().Trim();
                objCustomerMst.ModelName = txtmodel.Text.ToString().Trim();
                objCustomerMst.ModelVariant = txtVariant.Text.ToString().Trim();

                //if (ddlCountryName.SelectedItem != null)
                //{
                //    objCustomerMst.Country = ddlCountryName.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.Country = ddlCountryName.SelectedItem.Text;
                //}


                //if (ddlStateName.SelectedItem != null)
                //{
                //    objCustomerMst.StateName = ddlStateName.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.StateName = "";
                //}

                //if (ddlCityName.SelectedItem != null)
                //{
                //    objCustomerMst.CityName = ddlCityName.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.CityName = "";
                //}

                //if (ddlBrand.SelectedItem != null)
                //{
                //    objCustomerMst.BrandName = ddlBrand.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.BrandName = "";
                //}

                //if (ddlModel.SelectedItem != null)
                //{
                //    objCustomerMst.ModelName = ddlModel.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.BrandName = "";
                //}

                //if (ddlVariant.SelectedItem != null)
                //{
                //    objCustomerMst.ModelVariant = ddlVariant.SelectedItem.Text;
                //}
                //else
                //{
                //    objCustomerMst.ModelVariant = "";
                //}


                objCustomerMst.AadharNumber = txtAaadharno.Text.ToString().Trim();
                objCustomerMst.AadharNumberVerified = txtAadharVerified.Text.ToString().Trim();
                objCustomerMst.PANNumber = txtPan.Text.ToString().Trim();
                objCustomerMst.PANNumberVerified = txtPANVerified.Text.ToString().Trim();
                objCustomerMst.CustPhoto_path = imgCustPhoto.ImageUrl;
                //objCustomerMst.IMEINumber2_SealPhotoPath = "";
                //objCustomerMst.Invoive_Path = "";
                //objCustomerMst.IMEINumber_PhotoPath = "";
                //objCustomerMst.IMEINumber1_SealPhotoPath = "";
                objCustomerMst.Color = txtColor.Text.ToString().Trim();
                objCustomerMst.SellingPrice = txtSellingPrice.Text.ToString().Trim();
                objCustomerMst.DownPayment = txtdownPayment.Text.ToString().Trim();
                objCustomerMst.Tenure = txtTenure.Text.ToString().Trim();
                objCustomerMst.EMIAmount = txtEMIAmount.Text.ToString().Trim();
                objCustomerMst.IMEINumber1 = txtIMEIOne.Text.ToString().Trim();
                objCustomerMst.IMEINumber2 = txtIMEITwo.Text.ToString().Trim();
                objCustomerMst.AccountNumber = txtAccountno.Text.ToString().Trim();
                objCustomerMst.BankIFSCCode = txtIFSC.Text.ToString().Trim();
                objCustomerMst.BankName = txtBank.Text.ToString().Trim();
                objCustomerMst.AccountType = txtAccountType.Text.ToString().Trim();
                objCustomerMst.BranchName = txtBranchName.Text.ToString().Trim();
                objCustomerMst.RefName = txtRefName.Text.ToString().Trim();
                objCustomerMst.RefRelationShip = txtRefRelationship.Text.ToString().Trim();
                objCustomerMst.RefmobileNo = txtRefMobileno.Text.ToString().Trim();
                objCustomerMst.RefAddress = txtRefAddress.Text.ToString().Trim();
                //objCustomerMst.DebitOrCreditCard = txtDebitCreditCard.Text.ToString().Trim();
                //objCustomerMst.UPIMandate = txtUPIMandate.Text.ToString().Trim();
                objCustomerMst.Mode = "UPDATE";
                var result = objCustomerMst.UpdateCustomerDeviceMst();
                if (result.Rows.Count > 0)
                {
                    if (result.Rows[0]["Statuss"].ToString() == "SUCCESS")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Update Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Not Update');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Opps Something worng');", true);
                }
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDownloadPan_Click(object sender, EventArgs e)
        {
            string imageUrl = imgPan.ImageUrl;
            using (WebClient client = new WebClient())
            {
                byte[] fileBytes = client.DownloadData(imageUrl);
                string fileName = Path.GetFileName(imageUrl);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }

        protected void btnRemovePan_Click(object sender, EventArgs e)
        {

        }
        protected void btnCibilDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string encryptedRID = Request.QueryString["edocelor"];
                if (int.TryParse(encryptedRID, out int rid))
                    objCustomerMst.RID = rid;
                else
                    objCustomerMst.RID = null;

                objCustomerMst.Mode = "GET";
                DataTable dt = objCustomerMst.GetCustomerDeviceMst();

                if (dt == null || dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('No customer data found.');", true);
                    return;
                }

                string cibilJson = dt.Rows[0]["CibilApiResponse"]?.ToString();
                if (string.IsNullOrEmpty(cibilJson))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL Report Not found.');", true);
                    return;
                }

                JObject jsonObj = JObject.Parse(cibilJson);
                var profile = jsonObj["result"]?["result_json"]?["INProfileResponse"];
                if (profile == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL structure invalid.');", true);
                    return;
                }

                string fileName = "CIBIL_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                // Prepare Response for Download
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                    PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();

                    // Fonts
                    var headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
                    var sectionFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                    var keyFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                    var valueFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);

                    // Title
                    doc.Add(new Paragraph("CIBIL Detailed Report", headerFont));
                    doc.Add(new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), valueFont));
                    doc.Add(new Paragraph("\n"));

                    // Helper function
                    PdfPTable CreateTable(Dictionary<string, string> rows)
                    {
                        PdfPTable table = new PdfPTable(2);
                        table.WidthPercentage = 100;
                        table.SpacingBefore = 5f;
                        table.SpacingAfter = 10f;

                        foreach (var kv in rows)
                        {
                            PdfPCell cellKey = new PdfPCell(new Phrase(kv.Key, keyFont));
                            PdfPCell cellVal = new PdfPCell(new Phrase(kv.Value ?? "N/A", valueFont));
                            cellKey.BackgroundColor = new BaseColor(235, 235, 235);
                            cellKey.BorderWidth = 0.5f;
                            cellVal.BorderWidth = 0.5f;
                            table.AddCell(cellKey);
                            table.AddCell(cellVal);
                        }

                        return table;
                    }

                    // CREDIT PROFILE HEADER
                    var header = profile["CreditProfileHeader"];
                    if (header != null)
                    {
                        doc.Add(new Paragraph("CREDIT PROFILE HEADER", sectionFont));
                        var rows = new Dictionary<string, string>
                {
                    { "Report Number", (string)header["ReportNumber"] },
                    { "Report Date", (string)header["ReportDate"] },
                    { "Report Time", (string)header["ReportTime"] },
                    { "Subscriber Name", (string)header["Subscriber_Name"] },
                    { "Version", (string)header["Version"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // CAIS SUMMARY
                    var caisSummary = profile["CAIS_Summary"];
                    if (caisSummary != null)
                    {
                        doc.Add(new Paragraph("CAIS SUMMARY", sectionFont));

                        var creditAcc = caisSummary["Credit_Account"];
                        if (creditAcc != null)
                        {
                            var rows = new Dictionary<string, string>
                    {
                        { "Active Accounts", (string)creditAcc["CreditAccountActive"] },
                        { "Closed Accounts", (string)creditAcc["CreditAccountClosed"] },
                        { "Default Accounts", (string)creditAcc["CreditAccountDefault"] },
                        { "Total Accounts", (string)creditAcc["CreditAccountTotal"] }
                    };
                            doc.Add(CreateTable(rows));
                        }

                        var balance = caisSummary["Total_Outstanding_Balance"];
                        if (balance != null)
                        {
                            var rows = new Dictionary<string, string>
                    {
                        { "Outstanding (All)", (string)balance["Outstanding_Balance_All"] },
                        { "Secured", (string)balance["Outstanding_Balance_Secured"] },
                        { "Unsecured", (string)balance["Outstanding_Balance_UnSecured"] },
                        { "Unsecured %", (string)balance["Outstanding_Balance_UnSecured_Percentage"] }
                    };
                            doc.Add(CreateTable(rows));
                        }
                    }

                    // CAPS SUMMARY
                    var capsSummary = profile["CAPS"]?["CAPS_Summary"];
                    var nonCreditCaps = profile["NonCreditCAPS"]?["NonCreditCAPS_Summary"];
                    var totalCaps = profile["TotalCAPS_Summary"];
                    if (capsSummary != null || nonCreditCaps != null || totalCaps != null)
                    {
                        doc.Add(new Paragraph("CAPS SUMMARY", sectionFont));

                        if (capsSummary != null)
                        {
                            var rows = capsSummary.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                        if (nonCreditCaps != null)
                        {
                            doc.Add(new Paragraph("Non-Credit CAPS", keyFont));
                            var rows = nonCreditCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                        if (totalCaps != null)
                        {
                            doc.Add(new Paragraph("Total CAPS Summary", keyFont));
                            var rows = totalCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                    }

                    // CURRENT APPLICATION
                    var currentApp = profile["Current_Application"]?["Current_Application_Details"];
                    if (currentApp != null)
                    {
                        doc.Add(new Paragraph("CURRENT APPLICATION", sectionFont));

                        var rows = new Dictionary<string, string>
                {
                    { "Applicant Name", (string)currentApp["Current_Applicant_Details"]?["First_Name"] },
                    { "PAN", (string)currentApp["Current_Applicant_Details"]?["IncomeTaxPan"] },
                    { "Mobile", (string)currentApp["Current_Applicant_Details"]?["MobilePhoneNumber"] },
                    { "Amount Financed", (string)currentApp["Amount_Financed"] },
                    { "Income", (string)currentApp["Current_Other_Details"]?["Income"] },
                    { "Duration (Months)", (string)currentApp["Duration_Of_Agreement"] },
                    { "Enquiry Reason", (string)currentApp["Enquiry_Reason"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // SCORE
                    var score = profile["SCORE"];
                    if (score != null)
                    {
                        doc.Add(new Paragraph("CIBIL SCORE", sectionFont));
                        var rows = new Dictionary<string, string>
                {
                    { "Bureau Score", (string)score["BureauScore"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // CAIS ACCOUNT DETAILS
                    var accountDetails = profile["CAIS_Account"]?["CAIS_Account_DETAILS"];
                    if (accountDetails != null)
                    {
                        int idx = 1;
                        foreach (var acc in accountDetails)
                        {
                            doc.Add(new Paragraph($"ACCOUNT #{idx++}", sectionFont));
                            var rows = new Dictionary<string, string>
                    {
                        { "Subscriber Name", (string)acc["Subscriber_Name"] },
                        { "Account Number", (string)acc["Account_Number"] },
                        { "Account Type", (string)acc["Account_Type"] },
                        { "Status", (string)acc["Account_Status"] },
                        { "Current Balance", (string)acc["Current_Balance"] },
                        { "Loan Amount", (string)acc["Highest_Credit_or_Original_Loan_Amount"] },
                        { "Open Date", (string)acc["Open_Date"] },
                        { "Date Closed", (string)acc["Date_Closed"] }
                    };
                            doc.Add(CreateTable(rows));
                        }
                    }

                    doc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error generating PDF: " + ex.Message.Replace("'", "") + "');", true);
            }
        }

        //protected void btnCibilDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string encryptedRID = Request.QueryString["edocelor"];
        //        if (int.TryParse(encryptedRID, out int rid))
        //            objCustomerMst.RID = rid;
        //        else
        //            objCustomerMst.RID = null;

        //        objCustomerMst.Mode = "GET";
        //        DataTable dt = objCustomerMst.GetCustomerDeviceMst();

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('No customer data found.');", true);
        //            return;
        //        }

        //        string cibilJson = dt.Rows[0]["CibilApiResponse"]?.ToString();
        //        if (string.IsNullOrEmpty(cibilJson))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL Report Not found.');", true);
        //            return;
        //        }

        //        JObject jsonObj = JObject.Parse(cibilJson);
        //        var profile = jsonObj["result"]?["result_json"]?["INProfileResponse"];

        //        if (profile == null)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL structure invalid.');", true);
        //            return;
        //        }

        //        string fileName = "CIBIL_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //        string folder = Server.MapPath("~/Reports/");
        //        Directory.CreateDirectory(folder);
        //        string filePath = Path.Combine(folder, fileName);

        //        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
        //            PdfWriter.GetInstance(doc, stream);
        //            doc.Open();

        //            // Fonts
        //            var headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
        //            var sectionFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
        //            var keyFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
        //            var valueFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);

        //            // Title
        //            doc.Add(new Paragraph("CIBIL Detailed Report", headerFont));
        //            doc.Add(new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), valueFont));
        //            doc.Add(new Paragraph("\n"));

        //            // Helper function
        //            PdfPTable CreateTable(Dictionary<string, string> rows)
        //            {
        //                PdfPTable table = new PdfPTable(2);
        //                table.WidthPercentage = 100;
        //                table.SpacingBefore = 5f;
        //                table.SpacingAfter = 10f;

        //                foreach (var kv in rows)
        //                {
        //                    PdfPCell cellKey = new PdfPCell(new Phrase(kv.Key, keyFont));
        //                    PdfPCell cellVal = new PdfPCell(new Phrase(kv.Value ?? "N/A", valueFont));
        //                    cellKey.BackgroundColor = new BaseColor(235, 235, 235);
        //                    cellKey.BorderWidth = 0.5f;
        //                    cellVal.BorderWidth = 0.5f;
        //                    table.AddCell(cellKey);
        //                    table.AddCell(cellVal);
        //                }

        //                return table;
        //            }

        //            // 1. Credit Profile Header
        //            var header = profile["CreditProfileHeader"];
        //            if (header != null)
        //            {
        //                doc.Add(new Paragraph("CREDIT PROFILE HEADER", sectionFont));
        //                var rows = new Dictionary<string, string>
        //        {
        //            { "Report Number", (string)header["ReportNumber"] },
        //            { "Report Date", (string)header["ReportDate"] },
        //            { "Report Time", (string)header["ReportTime"] },
        //            { "Subscriber Name", (string)header["Subscriber_Name"] },
        //            { "Version", (string)header["Version"] }
        //        };
        //                doc.Add(CreateTable(rows));
        //            }

        //            // 2. CAIS Summary
        //            var caisSummary = profile["CAIS_Summary"];
        //            if (caisSummary != null)
        //            {
        //                doc.Add(new Paragraph("CAIS SUMMARY", sectionFont));

        //                var creditAcc = caisSummary["Credit_Account"];
        //                if (creditAcc != null)
        //                {
        //                    var rows = new Dictionary<string, string>
        //            {
        //                { "Active Accounts", (string)creditAcc["CreditAccountActive"] },
        //                { "Closed Accounts", (string)creditAcc["CreditAccountClosed"] },
        //                { "Default Accounts", (string)creditAcc["CreditAccountDefault"] },
        //                { "Total Accounts", (string)creditAcc["CreditAccountTotal"] }
        //            };
        //                    doc.Add(CreateTable(rows));
        //                }

        //                var balance = caisSummary["Total_Outstanding_Balance"];
        //                if (balance != null)
        //                {
        //                    var rows = new Dictionary<string, string>
        //            {
        //                { "Outstanding (All)", (string)balance["Outstanding_Balance_All"] },
        //                { "Secured", (string)balance["Outstanding_Balance_Secured"] },
        //                { "Unsecured", (string)balance["Outstanding_Balance_UnSecured"] },
        //                { "Unsecured %", (string)balance["Outstanding_Balance_UnSecured_Percentage"] }
        //            };
        //                    doc.Add(CreateTable(rows));
        //                }
        //            }

        //            // 3. CAPS Summary
        //            var capsSummary = profile["CAPS"]?["CAPS_Summary"];
        //            var nonCreditCaps = profile["NonCreditCAPS"]?["NonCreditCAPS_Summary"];
        //            var totalCaps = profile["TotalCAPS_Summary"];
        //            if (capsSummary != null || nonCreditCaps != null || totalCaps != null)
        //            {
        //                doc.Add(new Paragraph("CAPS SUMMARY", sectionFont));

        //                if (capsSummary != null)
        //                {
        //                    var rows = capsSummary.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
        //                    doc.Add(CreateTable(rows));
        //                }
        //                if (nonCreditCaps != null)
        //                {
        //                    doc.Add(new Paragraph("Non-Credit CAPS", keyFont));
        //                    var rows = nonCreditCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
        //                    doc.Add(CreateTable(rows));
        //                }
        //                if (totalCaps != null)
        //                {
        //                    doc.Add(new Paragraph("Total CAPS Summary", keyFont));
        //                    var rows = totalCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
        //                    doc.Add(CreateTable(rows));
        //                }
        //            }

        //            // 4. Current Application
        //            var currentApp = profile["Current_Application"]?["Current_Application_Details"];
        //            if (currentApp != null)
        //            {
        //                doc.Add(new Paragraph("CURRENT APPLICATION", sectionFont));

        //                var rows = new Dictionary<string, string>
        //        {
        //            { "Applicant Name", (string)currentApp["Current_Applicant_Details"]?["First_Name"] },
        //            { "PAN", (string)currentApp["Current_Applicant_Details"]?["IncomeTaxPan"] },
        //            { "Mobile", (string)currentApp["Current_Applicant_Details"]?["MobilePhoneNumber"] },
        //            { "Amount Financed", (string)currentApp["Amount_Financed"] },
        //            { "Income", (string)currentApp["Current_Other_Details"]?["Income"] },
        //            { "Duration (Months)", (string)currentApp["Duration_Of_Agreement"] },
        //            { "Enquiry Reason", (string)currentApp["Enquiry_Reason"] }
        //        };
        //                doc.Add(CreateTable(rows));
        //            }

        //            // 5. Score
        //            var score = profile["SCORE"];
        //            if (score != null)
        //            {
        //                doc.Add(new Paragraph("CIBIL SCORE", sectionFont));
        //                var rows = new Dictionary<string, string>
        //        {
        //            { "Bureau Score", (string)score["BureauScore"] }
        //        };
        //                doc.Add(CreateTable(rows));
        //            }

        //            // 6. CAIS Account Details
        //            var accountDetails = profile["CAIS_Account"]?["CAIS_Account_DETAILS"];
        //            if (accountDetails != null)
        //            {
        //                int idx = 1;
        //                foreach (var acc in accountDetails)
        //                {
        //                    doc.Add(new Paragraph($"ACCOUNT #{idx++}", sectionFont));
        //                    var rows = new Dictionary<string, string>
        //            {
        //                { "Subscriber Name", (string)acc["Subscriber_Name"] },
        //                { "Account Number", (string)acc["Account_Number"] },
        //                { "Account Type", (string)acc["Account_Type"] },
        //                { "Status", (string)acc["Account_Status"] },
        //                { "Current Balance", (string)acc["Current_Balance"] },
        //                { "Loan Amount", (string)acc["Highest_Credit_or_Original_Loan_Amount"] },
        //                { "Open Date", (string)acc["Open_Date"] },
        //                { "Date Closed", (string)acc["Date_Closed"] }
        //            };
        //                    doc.Add(CreateTable(rows));
        //                }
        //            }

        //            doc.Close();
        //        }

        //        Response.ContentType = "application/pdf";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        Response.TransmitFile(filePath);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error generating PDF: " + ex.Message.Replace("'", "") + "');", true);
        //    }
        //}

        //protected void btnCibilDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string encryptedRID = Request.QueryString["edocelor"];
        //        if (int.TryParse(encryptedRID, out int rid))
        //            objCustomerMst.RID = rid;
        //        else
        //            objCustomerMst.RID = null;

        //        objCustomerMst.Mode = "GET";
        //        DataTable dt = objCustomerMst.GetCustomerDeviceMst();

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No customer data found.');", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('No customer data found.');", true);
        //            return;
        //        }

        //        string cibilJson = dt.Rows[0]["CibilApiResponse"]?.ToString();
        //        if (string.IsNullOrEmpty(cibilJson))
        //        {
        //            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CIBIL Report Not found.');", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL Report Not found.');", true);
        //            return;
        //        }

        //        JObject jsonObj = JObject.Parse(cibilJson);
        //        var profile = jsonObj["result"]?["result_json"]?["INProfileResponse"];

        //        if (profile == null)
        //        {
        //            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CIBIL structure invalid.');", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL structure invalid.');", true);
        //            return;
        //        }

        //        string fileName = "CIBIL_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //        string folder = Server.MapPath("~/Reports/");
        //        Directory.CreateDirectory(folder);
        //        string filePath = Path.Combine(folder, fileName);

        //        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
        //            PdfWriter.GetInstance(doc, stream);
        //            doc.Open();

        //            iTextSharp.text.Font headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
        //            iTextSharp.text.Font sectionFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
        //            iTextSharp.text.Font keyFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
        //            iTextSharp.text.Font valueFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);

        //            doc.Add(new Paragraph("CIBIL Detailed Report", headerFont));
        //            doc.Add(new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), valueFont));
        //            doc.Add(new Paragraph("\n"));

        //            //  1. Credit Profile Header
        //            var header = profile["CreditProfileHeader"];
        //            if (header != null)
        //            {
        //                doc.Add(new Paragraph("CREDIT PROFILE HEADER", sectionFont));
        //                doc.Add(new Paragraph("Report Number: " + header["ReportNumber"], valueFont));
        //                doc.Add(new Paragraph("Report Date: " + header["ReportDate"], valueFont));
        //                doc.Add(new Paragraph("Report Time: " + header["ReportTime"], valueFont));
        //                doc.Add(new Paragraph("Subscriber Name: " + header["Subscriber_Name"], valueFont));
        //                doc.Add(new Paragraph("Version: " + header["Version"], valueFont));
        //                doc.Add(new Paragraph("\n"));
        //            }

        //            //  2. CAIS Summary
        //            var caisSummary = profile["CAIS_Summary"];
        //            if (caisSummary != null)
        //            {
        //                doc.Add(new Paragraph("CAIS SUMMARY", sectionFont));

        //                var creditAcc = caisSummary["Credit_Account"];
        //                if (creditAcc != null)
        //                {
        //                    doc.Add(new Paragraph("Credit Accounts:", keyFont));
        //                    doc.Add(new Paragraph($"Active: {creditAcc["CreditAccountActive"]}", valueFont));
        //                    doc.Add(new Paragraph($"Closed: {creditAcc["CreditAccountClosed"]}", valueFont));
        //                    doc.Add(new Paragraph($"Default: {creditAcc["CreditAccountDefault"]}", valueFont));
        //                    doc.Add(new Paragraph($"Total: {creditAcc["CreditAccountTotal"]}", valueFont));
        //                    doc.Add(new Paragraph("\n"));
        //                }

        //                var balance = caisSummary["Total_Outstanding_Balance"];
        //                if (balance != null)
        //                {
        //                    doc.Add(new Paragraph("Outstanding Balances:", keyFont));
        //                    doc.Add(new Paragraph($"All: {balance["Outstanding_Balance_All"]}", valueFont));
        //                    doc.Add(new Paragraph($"Secured: {balance["Outstanding_Balance_Secured"]}", valueFont));
        //                    doc.Add(new Paragraph($"Unsecured: {balance["Outstanding_Balance_UnSecured"]}", valueFont));
        //                    doc.Add(new Paragraph($"Unsecured %: {balance["Outstanding_Balance_UnSecured_Percentage"]}", valueFont));
        //                    doc.Add(new Paragraph("\n"));
        //                }
        //            }

        //            //  3. CAPS Summary
        //            var capsSummary = profile["CAPS"]?["CAPS_Summary"];
        //            var nonCreditCaps = profile["NonCreditCAPS"]?["NonCreditCAPS_Summary"];
        //            var totalCaps = profile["TotalCAPS_Summary"];
        //            if (capsSummary != null || nonCreditCaps != null)
        //            {
        //                doc.Add(new Paragraph("CAPS SUMMARY", sectionFont));
        //                if (capsSummary != null)
        //                {
        //                    doc.Add(new Paragraph("Credit CAPS:", keyFont));
        //                    foreach (var prop in capsSummary.Children<JProperty>())
        //                        doc.Add(new Paragraph($"{prop.Name}: {prop.Value}", valueFont));
        //                }
        //                if (nonCreditCaps != null)
        //                {
        //                    doc.Add(new Paragraph("\nNon-Credit CAPS:", keyFont));
        //                    foreach (var prop in nonCreditCaps.Children<JProperty>())
        //                        doc.Add(new Paragraph($"{prop.Name}: {prop.Value}", valueFont));
        //                }
        //                if (totalCaps != null)
        //                {
        //                    doc.Add(new Paragraph("\nTotal CAPS Summary:", keyFont));
        //                    foreach (var prop in totalCaps.Children<JProperty>())
        //                        doc.Add(new Paragraph($"{prop.Name}: {prop.Value}", valueFont));
        //                }
        //                doc.Add(new Paragraph("\n"));
        //            }

        //            //  4. Current Application
        //            var currentApp = profile["Current_Application"]?["Current_Application_Details"];
        //            if (currentApp != null)
        //            {
        //                doc.Add(new Paragraph("CURRENT APPLICATION", sectionFont));
        //                doc.Add(new Paragraph($"Applicant: {currentApp["Current_Applicant_Details"]?["First_Name"]}", valueFont));
        //                doc.Add(new Paragraph($"PAN: {currentApp["Current_Applicant_Details"]?["IncomeTaxPan"]}", valueFont));
        //                doc.Add(new Paragraph($"Mobile: {currentApp["Current_Applicant_Details"]?["MobilePhoneNumber"]}", valueFont));
        //                doc.Add(new Paragraph($"Amount Financed: {currentApp["Amount_Financed"]}", valueFont));
        //                doc.Add(new Paragraph($"Income: {currentApp["Current_Other_Details"]?["Income"]}", valueFont));
        //                doc.Add(new Paragraph($"Duration: {currentApp["Duration_Of_Agreement"]}", valueFont));
        //                doc.Add(new Paragraph($"Enquiry Reason: {currentApp["Enquiry_Reason"]}", valueFont));
        //                doc.Add(new Paragraph("\n"));
        //            }


        //            var score = profile["SCORE"];
        //            if (score != null)
        //            {
        //                doc.Add(new Paragraph("CIBIL SCORE", sectionFont));
        //                doc.Add(new Paragraph("Bureau Score: " + score["BureauScore"], valueFont));
        //                doc.Add(new Paragraph("\n"));
        //            }

        //            //  6. CAIS Account Details
        //            var accountDetails = profile["CAIS_Account"]?["CAIS_Account_DETAILS"];
        //            if (accountDetails != null)
        //            {
        //                int idx = 1;
        //                foreach (var acc in accountDetails)
        //                {
        //                    doc.Add(new Paragraph($"ACCOUNT #{idx++}", sectionFont));
        //                    doc.Add(new Paragraph($"Subscriber Name: {acc["Subscriber_Name"]}", valueFont));
        //                    doc.Add(new Paragraph($"Account Number: {acc["Account_Number"]}", valueFont));
        //                    doc.Add(new Paragraph($"Account Type: {acc["Account_Type"]}", valueFont));
        //                    doc.Add(new Paragraph($"Status: {acc["Account_Status"]}", valueFont));
        //                    doc.Add(new Paragraph($"Current Balance: {acc["Current_Balance"]}", valueFont));
        //                    doc.Add(new Paragraph($"Loan Amount: {acc["Highest_Credit_or_Original_Loan_Amount"]}", valueFont));
        //                    doc.Add(new Paragraph($"Open Date: {acc["Open_Date"]}", valueFont));
        //                    doc.Add(new Paragraph($"Date Closed: {acc["Date_Closed"]}", valueFont));
        //                    doc.Add(new Paragraph("\n"));
        //                }
        //            }

        //            doc.Close();
        //        }

        //        Response.ContentType = "application/pdf";
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        Response.TransmitFile(filePath);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error generating PDF: " + ex.Message.Replace("'", "") + "');", true);
        //    }
        //}


        //protected void btnCibilDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string encryptedRID = Request.QueryString["edocelor"];
        //        if (int.TryParse(encryptedRID, out int rid))
        //        {
        //            objCustomerMst.RID = rid;

        //        }
        //        else
        //        {
        //            objCustomerMst.RID = null;
        //        }

        //        // Re-bind or ensure data is available
        //        //objCustomerMst.Mode = "GET";
        //        //DataTable dt = objCustomerMst.GetCustomerDeviceMst();

        //        //if (dt.Rows.Count == 0)
        //        //{
        //        //    //lblCibilMessage.Text = "No customer record found.";
        //        //    return;
        //        //}

        //        //string cibilData = dt.Rows[0]["CibilApiResponse"]?.ToString();

        //        //if (string.IsNullOrEmpty(cibilData))
        //        //{
        //        //   // lblCibilMessage.Text = "CIBIL data not available for this customer.";
        //        //    return;
        //        //}

        //        //// Generate PDF from CIBIL data
        //        //GenerateCibilPdf(cibilData);
        //        objCustomerMst.Mode = "GET";
        //        DataTable dt = objCustomerMst.GetCustomerDeviceMst();
        //        string json = dt.Rows[0]["CibilApiResponse"].ToString();

        //        //GenerateCibilPdf(json);

        //        if (string.IsNullOrEmpty(json))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CIBIL data not found!');", true);
        //            return;
        //        }

        //        CibilRoot cibilData = null;

        //        try
        //        {
        //            cibilData = JsonConvert.DeserializeObject<CibilRoot>(json);

        //        }
        //        catch (Exception)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid CIBIL data format!');", true);
        //            return;
        //        }

        //        if (cibilData == null || cibilData.result == null)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CIBIL response is empty!');", true);
        //            return;
        //        }

        //        string score = cibilData.result.result_json.INProfileResponse.SCORE.BureauScore ?? "N/A";
        //        string totalCreditAccounts = cibilData.result.result_json?.INProfileResponse?.CAIS_Summary?.Credit_Account?.CreditAccountTotal ?? "N/A";
        //        string totalOutstanding = cibilData.result.result_json?.INProfileResponse?.CAIS_Summary?.Total_Outstanding_Balance?.Outstanding_Balance_All ?? "N/A";


        //        GenerateCibilPdf(score,totalCreditAccounts, totalOutstanding);

        //    }
        //    catch (Exception ex)
        //    {
        //        //lblCibilMessage.Text = "Error generating CIBIL PDF: " + ex.Message;
        //    }
        //}





        //private void GenerateCibilPdf(string cibilJson)
        //{
        //    try
        //    {
        //        // Format JSON with indentation
        //        string formattedJson;
        //        try
        //        {
        //            var parsedJson = Newtonsoft.Json.Linq.JToken.Parse(cibilJson);
        //            formattedJson = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
        //        }
        //        catch
        //        {
        //            formattedJson = cibilJson; // fallback if invalid JSON
        //        }

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
        //            PdfWriter.GetInstance(document, ms);
        //            document.Open();

        //            // Define fonts
        //            iTextSharp.text.Font titleFont = FontFactory.GetFont("Helvetica-Bold", 16f);
        //            iTextSharp.text.Font contentFont = FontFactory.GetFont("Courier", 10f);

        //            // Title section
        //            document.Add(new Paragraph("CIBIL Report", titleFont));
        //            document.Add(new Paragraph("Generated on: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm")));
        //            document.Add(new Paragraph("-------------------------------------------------------------"));
        //            document.Add(new Paragraph("\n"));

        //            // Add formatted JSON text
        //            Paragraph jsonParagraph = new Paragraph(formattedJson, contentFont);
        //            jsonParagraph.Alignment = Element.ALIGN_LEFT;
        //            document.Add(jsonParagraph);

        //            document.Close();

        //            // Return as download
        //            byte[] bytes = ms.ToArray();
        //            Response.Clear();
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-disposition", "attachment;filename=CibilReport.pdf");
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.BinaryWrite(bytes);
        //            Response.End();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception if needed
        //        // lblCibilMessage.Text = "Error generating CIBIL PDF: " + ex.Message;
        //    }
        //}

        //private void AddIndentation(Document doc, int level)
        //{
        //    // Create indentation by adding a small blank paragraph
        //    if (level > 0)
        //    {
        //        string indent = new string(' ', level * 4); // 4 spaces per level
        //        Paragraph spacer = new Paragraph(indent, FontFactory.GetFont("Helvetica", 8f));
        //        spacer.Leading = 0;
        //        doc.Add(spacer);
        //    }
        //}

        //private void AddJsonToPdf(Document doc, JToken token, iTextSharp.text.Font keyFont, iTextSharp.text.Font valueFont, iTextSharp.text.Font sectionFont, int level)
        //{
        //    if (token is JObject obj)
        //    {
        //        foreach (var property in obj.Properties())
        //        {
        //            AddIndentation(doc, level);
        //            doc.Add(new Paragraph(property.Name + ":", keyFont));

        //            if (property.Value is JObject || property.Value is JArray)
        //            {
        //                AddJsonToPdf(doc, property.Value, keyFont, valueFont, sectionFont, level + 1);
        //            }
        //            else
        //            {
        //                AddIndentation(doc, level + 1);
        //                doc.Add(new Paragraph(property.Value?.ToString() ?? "null", valueFont));
        //            }
        //        }
        //    }
        //    else if (token is JArray arr)
        //    {
        //        int index = 1;
        //        foreach (var item in arr)
        //        {
        //            AddIndentation(doc, level);
        //            doc.Add(new Paragraph($"[{index}] Array Item", sectionFont));
        //            AddJsonToPdf(doc, item, keyFont, valueFont, sectionFont, level + 1);
        //            index++;
        //        }
        //    }
        //    else if (token is JValue val)
        //    {
        //        AddIndentation(doc, level);
        //        doc.Add(new Paragraph(val?.ToString() ?? "null", valueFont));
        //    }
        //}



        ///Pdf code
        ///
        //private void GenerateCibilPdf(string score, string accounts, string outstanding)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A4);
        //        PdfWriter.GetInstance(document, ms);
        //        document.Open();

        //        var fontHeader = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD);
        //        var fontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL);

        //        document.Add(new Paragraph("CIBIL Credit Report", fontHeader));
        //        document.Add(new Paragraph("Generated Date: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), fontNormal));
        //        document.Add(new Paragraph("------------------------------------------------------"));
        //        document.Add(new Paragraph($"CIBIL Score: {score}", fontNormal));
        //        document.Add(new Paragraph($"Total Credit Accounts: {accounts}", fontNormal));
        //        document.Add(new Paragraph($"Total Outstanding Balance: ₹{outstanding}", fontNormal));

        //        document.Close();

        //        byte[] bytes = ms.ToArray();
        //        Response.Clear();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=CibilReport.pdf");
        //        Response.Buffer = true;
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.BinaryWrite(bytes);
        //        Response.End();
        //    }
        //}





    }
}