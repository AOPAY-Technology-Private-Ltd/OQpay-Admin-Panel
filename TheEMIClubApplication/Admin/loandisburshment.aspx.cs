using AVFramework;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;
using System.Web.UI.HtmlControls;
using System.Net.Http;

namespace TheEMIClubApplication.Admin
{
    public partial class loandisburshment : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLLoanApproval objLoan = new BLLoanApproval();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(! IsPostBack)
            {
                string loanCode = Request.QueryString["LoanCode"];
                string customerCode = Request.QueryString["CustomerCode"];

                if (!string.IsNullOrEmpty(loanCode))
                {
                    LoadLoanDisbursementDetails(loanCode);
                }
                else
                {

                }
            }
  
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objLoan.Mode = "Disbursed";
            objLoan.Loanid = txtLoanCode.Text.ToUpper().Trim();
            objLoan.RecordStatus = "Disbursed";
            objLoan.Remarks = txtDisbustmentRemark.Text;
            var result = objLoan.LoanDisbusted();
            string status = result.Rows[0]["Success"].ToString();
            string statusMsg = result.Rows[0]["LoanMsg"].ToString();
            if (result != null)
            {

                if (status == "200")
                {
                    TransfertoAgentDeposit();
                    Response.Redirect("ManageDisburshment.aspx?msg=" + Server.UrlEncode(statusMsg));


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{statusMsg}');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{statusMsg}');", true);
            }
            //Claer();
        }

        protected void btnRejectDisburshmenmt_Click(object sender, EventArgs e)
        {
            objLoan.Mode = "Disbursed";
            objLoan.Loanid = txtLoanCode.Text.ToUpper().Trim();
            objLoan.RecordStatus = "Disbursed";
            objLoan.Remarks = txtDisbustmentRemark.Text;
            var result = objLoan.LoanDisbusted();
            string status = result.Rows[0]["Success"].ToString();
            string statusMsg = "Loan disbursement has been rejected.";
            if (result != null)
            {
                if (status == "200")
                {
                    //TransfertoAgentDeposit();
                    Response.Redirect("ManageDisburshment.aspx?msg=" + Server.UrlEncode(statusMsg));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{statusMsg}');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{statusMsg}');", true);
            }
            //Claer();
        }
        private void TransfertoAgentDeposit()
        {
            try
            {
                GenerateRefrenceId();
                // Set up the user details

                decimal TotalDeductions =
    Convert.ToDecimal(txtprocessingFees.Text.Trim()) +
    Convert.ToDecimal(txtMembershipfees.Text.Trim()) +
    Convert.ToDecimal(lblHoldAmt.Text);

                decimal remarksAmt = (Convert.ToDecimal(txtLoanAmount.Text) - Convert.ToDecimal(TotalDeductions));

                objTransfertoAgent.RegistrationId = txtDealerid.Text;
                objTransfertoAgent.TransferFrom = "Admin";
                objTransfertoAgent.TransferTo = txtDealerid.Text;
                objTransfertoAgent.Remark = "The Amount is "+ remarksAmt + " Credited Against the Loan ID : " + txtLoanCode.Text + " and Product : "+ txtBrandName.Text + " " +  txtModelVariant.Text ;
                objTransfertoAgent.TransferAmt = Convert.ToDecimal(txtLoanAmount.Text.Trim());
                objTransfertoAgent.TransferFromMsg = "The Amount is " + remarksAmt + " Credited Against the Loan ID : " + txtLoanCode.Text + " and Product : " + txtBrandName.Text + " " + txtModelVariant.Text;
                objTransfertoAgent.TransferToMsg = "The Amount is " + remarksAmt + " Credited Against the Loan ID : " + txtLoanCode.Text + " and Product : " + txtBrandName.Text + " " + txtModelVariant.Text;
                objTransfertoAgent.Amount_Type = "Deposit";
                objTransfertoAgent.ActualCommissionAmount = Convert.ToDecimal("0.00");
                objTransfertoAgent.GSTAMT = Convert.ToDecimal("0.00");
                objTransfertoAgent.TdsAmt = Convert.ToDecimal("0.00");
                objTransfertoAgent.commissionWithoutGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.TransIpAddress = AppSessions.SessionLocalHostIP;
                objTransfertoAgent.Services_charge_Amt = Convert.ToDecimal("0.00");
                objTransfertoAgent.Services_charge_GSTAmt = Convert.ToDecimal("0.00");
                objTransfertoAgent.Services_charge_Without_GST = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommission = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommissionGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.customerCommissionWithoutGST = Convert.ToDecimal("0.00");
                objTransfertoAgent.Amt_Transfer_TransID = RefrenceId;
                //objTransfertoAgent.Holdingamt = hdnReserveValue.Value;
                objTransfertoAgent.Holdingamt = lblHoldAmt.Text;                
                objTransfertoAgent.Actual_Transaction_Amount = (Convert.ToDecimal(txtLoanAmount.Text.Trim()) - Convert.ToDecimal(TotalDeductions));
                objTransfertoAgent.ProcessingFees = Convert.ToDecimal(txtprocessingFees.Text.Trim());
                objTransfertoAgent.MembershipFees = Convert.ToDecimal(txtMembershipfees.Text.Trim());
                objTransfertoAgent.ApporveRemakrs = txtDisbustmentRemark.Text;
                objTransfertoAgent.LoanID = txtLoanCode.Text.Trim();
                objTransfertoAgent.TransertoAgent();
                objTransfertoAgent.RefrenceId = RefrenceId;
                objTransfertoAgent.ApprovedStatus = "Approved";
                objTransfertoAgent.DepositUpdate();
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        public static string @RefrenceId;
        private void GenerateRefrenceId()
        {
            try
            {
                // Assuming you have a text box for the input series code
                string inputSeriesCode = "Transactionid"; // Get the series code from a TextBox

                // Output parameter to capture the generated series code
                string outputSeriesCode;

                // Call the RowCount method
                string result = objTransfertoAgent.RowCount(inputSeriesCode, out outputSeriesCode);

                // Check if the result is valid and display the output
                if (!string.IsNullOrEmpty(outputSeriesCode))
                {

                    @RefrenceId = outputSeriesCode; // Display the output series code in a TextBox
                }
                else
                {

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB); // Log SQL exception
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex); // Log general exception
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007"); // Display error message
            }
        }
        private void LoadLoanDisbursementDetails(string loanCode)
        {
            try
            {
         
                objLoan.Loanid = loanCode;
                DataTable Loandt = objLoan.GetLoanDetailsfordisburshment();
                if (Loandt != null && Loandt.Rows.Count > 0)
                {
              
                    DataRow row = Loandt.Rows[0];
                    // --- Loan Information ---
                    // Loan details (in exact order from SP)
                    txtLoanCode.Text = row["LoanCode"].ToString();
                    txtCustomerCode.Text = row["CustomerCode"].ToString();
                    txtLoanAmount.Text = row["LoanAmount"].ToString();
                    txtDownPayment.Text = row["DownPayment"].ToString();
                    txtLoanEMIAmount.Text = row["EMIAmount"].ToString();
                    txtprocessingFees.Text = row["ProcessingFees"].ToString();
                    txtMembershipfees.Text = row["MemberShip"].ToString();
                    txtTenure.Text = row["Tenure"].ToString();
                    txtInterestRate.Text = row["InterestRate"].ToString();
                    txtStartDate.Text = Convert.ToDateTime(row["StartDate"]).ToString("dd-MMM-yyyy");
                    txtEndDate.Text = Convert.ToDateTime(row["EndDate"]).ToString("dd-MMM-yyyy");
                    txtIMEI1Number.Text = row["IMEINumber"].ToString();
                    txtLoanStatus.Text = row["LoanStatus"].ToString();
                    txtBrandName.Text = row["BrandName"].ToString();
                    txtModelName.Text = row["ModelName"].ToString();
                    txtModelVariant.Text = row["VariantName"].ToString();
                    txtCreditScore.Text = row["creditScore"].ToString();
                    // Optional fields, bind if needed
                    // row["Loan_Color"], row["LoanCreatedBy"], row["LoanCreatedAt"], etc.

                    // --- Customer Information ---
                    txtCustomerFullName.Text = row["FullName"].ToString();
                    txtCustomerMobileNo.Text = row["PrimaryMobileNumber"].ToString();
                    txtCustomerEmailid.Text = row["EMailID"].ToString();

                    // --- Dealer Info ---
                    // Based on your old code, you're using Dealer info,
                    // but it's not part of this SP — likely from another table or previously joined.
                    // You may want to comment or handle conditionally:
                    txtDealerid.Text = row["RetailerCode"].ToString();
                    txtDealerFullName.Text = row["DealerFullName"].ToString();
                    txtDealerMobileNo.Text = row["DealerMobileNo"].ToString();
                    txtDealerEmailid.Text = row["DealerEmailID"].ToString();

                    // --- Document Images ---
                    imgIMEI1.ImageUrl = ImageUrl + row["IMEINumber1_SealPhotoPath"].ToString();
                    imgIMEI2.ImageUrl = ImageUrl + row["IMEINumber2_SealPhotoPath"].ToString();
                    imgInvoice.ImageUrl = ImageUrl + row["Invoive_Path"].ToString();
                    imgIMEI.ImageUrl = ImageUrl + row["IMEINumber_PhotoPath"].ToString();
                    lblHoldAmt.Text= row["ReserveValue"].ToString();
                    // Additional (optional) customer document images:
                    // imgAadhar.ImageUrl = ImageUrl + row["CustAadharPhoto_Path"].ToString();
                    // imgAadharBack.ImageUrl = ImageUrl + row["CustAadharBackPhoto_Path"].ToString();
                    // imgPAN.ImageUrl = ImageUrl + row["CustPanNumberPhoto_Path"].ToString();
                }

                else
                {
                    // Show message if no data found
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No loan details found.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", $"alert('Error loading loan details: {ex.Message}');", true);
            }
        }
        private void GridBind()
        {


            //try
            //{
            //    objLoan.Mode = "GET";

            //    objLoan.RecordStatus = "Pending";
            //    if (ddlLoanCriteria.SelectedItem.Text.ToUpper().Trim() == "All".ToUpper().Trim())
            //    {
            //        Loandt = objLoan.GetLoanDetails();
            //    }
            //    else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "Loancode".ToUpper().Trim())
            //    {
            //        objLoan.Loanid = txtLoanvalue.Text.ToUpper().Trim();
            //        Loandt = objLoan.GetLoanDetails();
            //    }
            //    else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "Customercode".ToUpper().Trim())
            //    {
            //        objLoan.Customercode = txtLoanvalue.Text.Trim();
            //        Loandt = objLoan.GetLoanDetails();
            //    }
            //    else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "dealerid".ToUpper().Trim())
            //    {
            //        objLoan.Dealrercode = txtLoanvalue.Text.Trim();
            //        Loandt = objLoan.GetLoanDetails();
            //    }
            //    else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "loanstatrus".ToUpper().Trim())
            //    {
            //        objLoan.RecordStatus = txtLoanvalue.Text.Trim();
            //        Loandt = objLoan.GetLoanDetails();
            //    }

            //    txtLoanvalue.Text = string.Empty;


            //    grdLoanDetails.DataSource = Loandt;

            //    if (Loandt.Rows.Count > 0)
            //    {
            //        grdLoanDetails.DataBind();
            //        string SucessMessage = "Records Retrieve Successfully";

            //        // toastr.success must be lowercase
            //        string script = $"toastr.success('{SucessMessage}', 'Success');";

            //        ScriptManager.RegisterStartupScript(
            //            this,
            //            this.GetType(),
            //            "showToastrSuccess",
            //            script,
            //            true
            //        );
            //    }
            //    else
            //    {
            //        //lblEMINoData.Text = "No Data Found !!";
            //        grdLoanDetails.DataBind();
            //        grdLoanDetails.DataSource = null;

            //        string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");


            //        string script = $"toastr.error('{ErrorMessage}', 'Error');";
            //        ScriptManager.RegisterStartupScript(
            //       this,
            //       this.GetType(),
            //       "showToastrError",
            //       script,
            //       true
            //   );
            //    }




            //    //// Calculate pagination count
            //    //int totalRecords = Loandt.Rows.Count;
            //    //int startRecord = (grdLoanDetails.PageIndex * grdLoanDetails.PageSize) + 1;
            //    //int endRecord = startRecord + grdLoanDetails.PageSize - 1;

            //    //if (endRecord > totalRecords)
            //    //    endRecord = totalRecords;

            //    //if (totalRecords > 0)
            //    //    lblLoanRcordCount.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
            //    //else
            //    //    lblLoanRcordCount.Text = "No records found";


            //}
            //catch (Exception ex)
            //{
            //    // lblTotalEmicollection.Text = "Error";
            //}

        }

        //protected void btnUpdateIMEI1_Click(object sender, EventArgs e)
        //{
        //    string customerCode = txtCustomerCode.Text;
        //    string filePath = "../Uploads/IMEI1_Seal_" + customerCode + ".jpg";  // Example path

        //    CustomerPhotoUpdater updater = new CustomerPhotoUpdater();
        //    bool result = updater.UpdateCustomerPhotoPath(customerCode, "IMEINumber1_SealPhotoPath", filePath);

        //    if (result)
        //        lblMessage.Text = "IMEI 1 Seal Photo updated successfully!";
        //    else
        //        lblMessage.Text = "No record updated. Please check Customer Code.";
        //}

        //protected void btnUploadIMEI1_Click(object sender, EventArgs e)
        //{
        //    if (fuIMEI1.HasFile)
        //    {
        //        string folderPath = Server.MapPath("~/Uploads/IMEI/");
        //        if (!Directory.Exists(folderPath))
        //            Directory.CreateDirectory(folderPath);

        //        string fileName = Path.GetFileName(fuIMEI1.FileName);
        //        string savePath = Path.Combine(folderPath, fileName);
        //        fuIMEI1.SaveAs(savePath);

        //        imgIMEI1.ImageUrl = "~/Uploads/IMEI/" + fileName;
        //        btnRemoveIMEI1.Visible = true;
        //    }
        //}

   

        //protected async void btnUpdateIMEI1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string customerCode = txtCustomerCode.Text.Trim();


        //        if (fuIMEI1.HasFile)
        //        {
        //            try
        //            {
        //                string extension = Path.GetExtension(fuIMEI1.FileName).ToLower();
        //                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        //                if (!allowedExtensions.Contains(extension))
        //                {
        //                    ScriptManager.RegisterStartupScript(
        //                        this,
        //                        this.GetType(),
        //                        "Popup",
        //                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        //                        true
        //                    );
        //                    return;
        //                }
        //                string fileName = Path.GetFileName(fuIMEI1.FileName);      
        //                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        //                string folderPath = Server.MapPath("/IMEIPhotos/");
        //                if (!Directory.Exists(folderPath))
        //                    Directory.CreateDirectory(folderPath);

        //                string fullPath = Path.Combine(folderPath, uniqueFileName);
        //                fuIMEI1.SaveAs(fullPath);

                 
        //                string relativePath = "/IMEIPhotos/" + uniqueFileName;
        //                imgIMEI1.ImageUrl = relativePath;
        //                imgIMEI1.Visible = true;

        //            }
        //            catch (Exception ex)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Error: {ex.Message}');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Please select an image to upload.');", true);
        //        }


        //        CustomerPhotoUpdater updater = new CustomerPhotoUpdater();

        //        // Call async method
        //        bool success = await updater.UpdateCustomerPhotoPathAsync(customerCode, "IMEINumber1_SealPhotoPath", imgIMEI2.ImageUrl);

        //        spnMessage.InnerHtml = success
        //            ? "IMEI 1 Seal Photo uploaded successfully!"
        //            : "Database update failed.";
        //        spnMessage.Attributes["class"] = success ? "text-success" : "text-danger";
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{msg}');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMessage.Text = "Error: " + ex.Message;
        //        //lblMessage.CssClass = "text-danger";
        //    }
        //}

        //protected async void btnUploadIMEI2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string customerCode = txtCustomerCode.Text.Trim();


        //        if (fuIMEI2.HasFile)
        //        {
        //            try
        //            {
        //                string extension = Path.GetExtension(fuIMEI2.FileName).ToLower();
        //                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

        //                if (!allowedExtensions.Contains(extension))
        //                {
        //                    ScriptManager.RegisterStartupScript(
        //                        this,
        //                        this.GetType(),
        //                        "Popup",
        //                        "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
        //                        true
        //                    );
        //                    return;
        //                }
        //                string fileName = Path.GetFileName(fuIMEI2.FileName);
        //                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

        //                string folderPath = Server.MapPath("/IMEIPhotos/");
        //                if (!Directory.Exists(folderPath))
        //                    Directory.CreateDirectory(folderPath);

        //                string fullPath = Path.Combine(folderPath, uniqueFileName);
        //                fuIMEI2.SaveAs(fullPath);


        //                string relativePath = "/IMEIPhotos/" + uniqueFileName;
        //                imgIMEI2.ImageUrl = relativePath;
        //                imgIMEI2.Visible = true;

        //            }
        //            catch (Exception ex)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Error: {ex.Message}');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Please select an image to upload.');", true);
        //        }


        //        CustomerPhotoUpdater updater = new CustomerPhotoUpdater();

        //        // Call async method
       //       bool success = await updater.UpdateCustomerPhotoPathAsync(customerCode, "IMEINumber2_SealPhotoPath", imgIMEI2.ImageUrl );

        //        spnMessage.InnerHtml = success
        //            ? "IMEI 2 Seal Photo uploaded successfully!"
        //            : "Database update failed.";
        //        spnMessage.Attributes["class"] = success ? "text-success" : "text-danger";
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMessage.Text = "Error: " + ex.Message;
        //        //lblMessage.CssClass = "text-danger";
        //    }
        //}

  
        protected async void btnUpdateIMEI1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerCode = txtCustomerCode.Text.Trim();

                if (fuIMEI1.HasFile)
                {
                    string extension = Path.GetExtension(fuIMEI1.FileName).ToLower();
                    string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "Popup",
                            "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
                            true
                        );
                        return;
                    }

                  
                    string fileName = Path.GetFileName(fuIMEI1.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                   
                    byte[] fileBytes = fuIMEI1.FileBytes;

                    using (var httpClient = new HttpClient())
                    {
                        using (var form = new MultipartFormDataContent())
                        {
                            form.Add(new StringContent(customerCode), "CustomerCode");
                            form.Add(new StringContent("IMEINumber1_SealPhotoPath"), "ColumnName");
                            form.Add(new StreamContent(new MemoryStream(fileBytes)), "Image_FileName", uniqueFileName);

                            HttpResponseMessage response = await httpClient.PostAsync("https://api.oqpay.in/api/V1/OQFinance/UpdateCustomerPhotoPath", form);

                            if (response.IsSuccessStatusCode)
                            {
                                spnMessage.InnerHtml = "IMEI 1 Seal Photo uploaded successfully!";
                                spnMessage.Attributes["class"] = "text-success";

                                LoadLoanDisbursementDetails(txtLoanCode.Text);
                                // Optionally show the uploaded image if API returns path
                                //string uploadedUrl = $"https://api.oqpay.in/IMEIPhotos/{uniqueFileName}";
                                //imgIMEI1.ImageUrl = uploadedUrl;
                                //imgIMEI1.Visible = true;
                            }
                            else
                            {
                                string errorMsg = await response.Content.ReadAsStringAsync();
                                spnMessage.InnerHtml = "Database update failed: " + errorMsg;
                                spnMessage.Attributes["class"] = "text-danger";
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Please select an image to upload.');", true);
                }
            }
            catch (Exception ex)
            {
                spnMessage.InnerHtml = "Error: " + ex.Message;
                spnMessage.Attributes["class"] = "text-danger";
            }
        }

        protected async void btnUploadIMEI2_Click(object sender, EventArgs e)
        {
            try
            {
                string customerCode = txtCustomerCode.Text.Trim();

                if (fuIMEI2.HasFile)
                {
                    try
                    {
                        string extension = Path.GetExtension(fuIMEI2.FileName).ToLower();
                        string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                        if (!allowedExtensions.Contains(extension))
                        {
                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "Popup",
                                "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
                                true
                            );
                            return;
                        }

                        // Get file details
                        string fileName = Path.GetFileName(fuIMEI2.FileName);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                        // Prepare image for upload (convert file to byte array)
                        byte[] fileBytes = fuIMEI2.FileBytes;

                        using (var httpClient = new HttpClient())
                        {
                            using (var form = new MultipartFormDataContent())
                            {
                                // Add customer code, column name (for database update), and the image file
                                form.Add(new StringContent(customerCode), "CustomerCode");
                                form.Add(new StringContent("IMEINumber2_SealPhotoPath"), "ColumnName");

                                // Convert file bytes to StreamContent
                                var fileContent = new ByteArrayContent(fileBytes);
                                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                                // Add file content to form data (this sends the actual file to the remote server)
                                form.Add(fileContent, "Image_FileName", uniqueFileName);

                                // API endpoint for uploading the image
                                string apiUrl = "https://api.oqpay.in/api/V1/OQFinance/UpdateCustomerPhotoPath";

                                // Call the API
                                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, form);

                                if (response.IsSuccessStatusCode)
                                {
                                    spnMessage.InnerHtml = "IMEI 2 Seal Photo uploaded successfully!";
                                    spnMessage.Attributes["class"] = "text-success";

                                    LoadLoanDisbursementDetails(txtLoanCode.Text);
                                    // Optionally, handle response (e.g., display uploaded image or update DB)
                                    //string uploadedImageUrl = $"https://api.oqpay.in/IMEIPhotos/{uniqueFileName}"; // Modify if the response returns the path
                                    //imgIMEI2.ImageUrl = uploadedImageUrl;
                                    //imgIMEI2.Visible = true;
                                }
                                else
                                {
                                    string errorMsg = await response.Content.ReadAsStringAsync();
                                    spnMessage.InnerHtml = "Database update failed: " + errorMsg;
                                    spnMessage.Attributes["class"] = "text-danger";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Error: {ex.Message}');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Please select an image to upload.');", true);
                }
            }
            catch (Exception ex)
            {
                spnMessage.InnerHtml = "Error: " + ex.Message;
                spnMessage.Attributes["class"] = "text-danger";
            }
        }

        protected async void btnUploadIMEI_Click(object sender, EventArgs e)
        {
            try
            {
                string customerCode = txtCustomerCode.Text.Trim();

                if (fuIMEI.HasFile)
                {
                    try
                    {
                        string extension = Path.GetExtension(fuIMEI.FileName).ToLower();
                        string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                        if (!allowedExtensions.Contains(extension))
                        {
                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "Popup",
                                "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
                                true
                            );
                            return;
                        }

                        // Get file details
                        string fileName = Path.GetFileName(fuIMEI.FileName);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                        // Prepare image for upload (convert file to byte array)
                        byte[] fileBytes = fuIMEI.FileBytes;

                        using (var httpClient = new HttpClient())
                        {
                            using (var form = new MultipartFormDataContent())
                            {
                                // Add customer code, column name (for database update), and the image file
                                form.Add(new StringContent(customerCode), "CustomerCode");
                                form.Add(new StringContent("IMEINumber_PhotoPath"), "ColumnName");

                                // Convert file bytes to StreamContent
                                var fileContent = new ByteArrayContent(fileBytes);
                                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                                // Add file content to form data (this sends the actual file to the remote server)
                                form.Add(fileContent, "Image_FileName", uniqueFileName);

                                // API endpoint for uploading the image
                                string apiUrl = "https://api.oqpay.in/api/V1/OQFinance/UpdateCustomerPhotoPath";

                                // Call the API
                                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, form);

                                if (response.IsSuccessStatusCode)
                                {
                                    spnMessage.InnerHtml = "IMEI Photo uploaded successfully!";
                                    spnMessage.Attributes["class"] = "text-success";

                                    LoadLoanDisbursementDetails(txtLoanCode.Text);
                                    // Optionally, handle response (e.g., display uploaded image or update DB)
                                    //string uploadedImageUrl = $"https://api.oqpay.in/IMEIPhotos/{uniqueFileName}"; // Modify if the response returns the path
                                    //imgIMEI2.ImageUrl = uploadedImageUrl;
                                    //imgIMEI2.Visible = true;
                                }
                                else
                                {
                                    string errorMsg = await response.Content.ReadAsStringAsync();
                                    spnMessage.InnerHtml = "Database update failed: " + errorMsg;
                                    spnMessage.Attributes["class"] = "text-danger";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Error: {ex.Message}');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Please select an image to upload.');", true);
                }
            }
            catch (Exception ex)
            {
                spnMessage.InnerHtml = "Error: " + ex.Message;
                spnMessage.Attributes["class"] = "text-danger";
            }

        }

        protected async void btnUploadInvoice_Click(object sender, EventArgs e)
        {
          
            try
            {
                string customerCode = txtCustomerCode.Text.Trim();

                if (fuInvoice.HasFile)
                {
                    try
                    {
                        string extension = Path.GetExtension(fuInvoice.FileName).ToLower();
                        string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                        if (!allowedExtensions.Contains(extension))
                        {
                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "Popup",
                                "ShowPopup('Only PNG, JPG, and JPEG formats are allowed.');",
                                true
                            );
                            return;
                        }

                        // Get file details
                        string fileName = Path.GetFileName(fuInvoice.FileName);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                        // Prepare image for upload (convert file to byte array)
                        byte[] fileBytes = fuInvoice.FileBytes;

                        using (var httpClient = new HttpClient())
                        {
                            using (var form = new MultipartFormDataContent())
                            {
                                // Add customer code, column name (for database update), and the image file
                                form.Add(new StringContent(customerCode), "CustomerCode");
                                form.Add(new StringContent("Invoive_Path"), "ColumnName");

                                // Convert file bytes to StreamContent
                                var fileContent = new ByteArrayContent(fileBytes);
                                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                                // Add file content to form data (this sends the actual file to the remote server)
                                form.Add(fileContent, "Image_FileName", uniqueFileName);

                                // API endpoint for uploading the image
                                string apiUrl = "https://api.oqpay.in/api/V1/OQFinance/UpdateCustomerPhotoPath";

                                // Call the API
                                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, form);

                                if (response.IsSuccessStatusCode)
                                {
                                    spnMessage.InnerHtml = "Invoive Photo uploaded successfully!";
                                    spnMessage.Attributes["class"] = "text-success";

                                    LoadLoanDisbursementDetails(txtLoanCode.Text);
                                    // Optionally, handle response (e.g., display uploaded image or update DB)
                                    //string uploadedImageUrl = $"https://api.oqpay.in/IMEIPhotos/{uniqueFileName}"; // Modify if the response returns the path
                                    //imgIMEI2.ImageUrl = uploadedImageUrl;
                                    //imgIMEI2.Visible = true;
                                }
                                else
                                {
                                    string errorMsg = await response.Content.ReadAsStringAsync();
                                    spnMessage.InnerHtml = "Database update failed: " + errorMsg;
                                    spnMessage.Attributes["class"] = "text-danger";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Error: {ex.Message}');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Please select an image to upload.');", true);
                }
            }
            catch (Exception ex)
            {
                spnMessage.InnerHtml = "Error: " + ex.Message;
                spnMessage.Attributes["class"] = "text-danger";
            }

        }


    }
}