using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class LoanApproval : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLLoanApproval objLoan = new BLLoanApproval();
        DataTable Loandt = new DataTable();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                GetHoldingAmt();
                GridBind();
            }

           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objLoan.Mode = "APPROVED";
            objLoan.Loanid = txtLoanCode.Text.ToUpper().Trim();
            objLoan.RecordStatus = ddlLoanApproval.SelectedValue.ToUpper().Trim();
            objLoan.Remarks = txtremarks.Text.Trim();

           var result= objLoan.GetLoanDetails();
            string status = result.Rows[0]["Success"].ToString();
            string statusMsg = result.Rows[0]["LoanMsg"].ToString();
            if (result !=null)
            {
          
               if (status=="200")
                {
                   // TransfertoAgentDeposit();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{statusMsg}');", true);
                 
                    Div_LaonApproval.Visible = false;

                 
                    Div_LoanSearch.Visible = true;
                    GridBind();

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
            Claer();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {  // change by pinky 
           // ondate 15-01-2026
           //Response.Redirect("../CommonPages/Home.aspx");


            Div_LaonApproval.Visible = false;


            Div_LoanSearch.Visible = true;
            GridBind();
        }

        private void Claer()
        {
            txtCustomerCode.Text = "";
            txtCustomerFullName.Text = "";
            txtCustomerMobileNo.Text = "";
            txtCustomerEmailid.Text = "";
            txtDealerid.Text = "";
            txtDealerFullName.Text = "";
            txtDealerMobileNo.Text = "";
            txtDealerEmailid.Text = "";
            txtLoanCode.Text = "";
            txtLoanAmount.Text = "";
            txtDownPayment.Text = "";
            txtLoanEMIAmount.Text = "";
            txtTenure.Text = "";
            txtInterestRate.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtCreditScore.Text = "";
            txtremarks.Text = "";
            //txtProcessingfees.Text ="";
            txtLoanStatus.Text = "";
            txtLoanCreatedBy.Text = "";
            txtBrandName.Text = "";
            txtModelName.Text = "";
            txtModelVariant.Text = "";
            txtIMEI1Number.Text = "";
            // txtIMEI2Number.Text ="";
            imgCustPhoto.ImageUrl = "../Images/image icon.png";
            imgAadharfrontphoto.ImageUrl = "../Images/image icon.png";
            imgAadharbackphoto.ImageUrl = "../Images/image icon.png";
            imgPan.ImageUrl = "../Images/image icon.png";
            imgIMEI1.ImageUrl = "../Images/image icon.png";
            imgIMEI2.ImageUrl = "../Images/image icon.png";
            imgIMEI.ImageUrl = "../Images/image icon.png";
            imgInvoice.ImageUrl = "../Images/image icon.png";

        }

        private void  GridBind()
        {


            try
            {
                objLoan.Mode = "GET";
                objLoan.RecordStatus = "Pending";
                objLoan.OnlineMode = ddlMode.SelectedValue;

         
                if (ddlLoanCriteria.SelectedItem.Text.ToUpper().Trim() == "All".ToUpper().Trim())
                {
                    Loandt = objLoan.GetLoanDetails();
                }
                else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "Loancode".ToUpper().Trim())
                {
                    objLoan.Loanid = txtLoanvalue.Text.ToUpper().Trim();
                    Loandt = objLoan.GetLoanDetails();
                }
                else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "Customercode".ToUpper().Trim())
                {
                    objLoan.Customercode = txtLoanvalue.Text.Trim();
                    Loandt = objLoan.GetLoanDetails();
                }
                else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "dealerid".ToUpper().Trim())
                {
                    objLoan.Dealrercode = txtLoanvalue.Text.Trim();
                    Loandt = objLoan.GetLoanDetails();
                }
                else if (ddlLoanCriteria.SelectedValue.ToUpper().Trim() == "loanstatrus".ToUpper().Trim())
                {
                    objLoan.RecordStatus = txtLoanvalue.Text.Trim();
                    Loandt = objLoan.GetLoanDetails();
                }

                txtLoanvalue.Text = string.Empty;

                
                grdLoanDetails.DataSource = Loandt;

                if (Loandt.Rows.Count > 0)
                {
                    grdLoanDetails.DataBind();
                    string SucessMessage = "Records Retrieve Successfully";

                    // toastr.success must be lowercase
                    string script = $"toastr.success('{SucessMessage}', 'Success');";

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "showToastrSuccess",
                        script,
                        true
                    );
                }
                else
                {
                    //lblEMINoData.Text = "No Data Found !!";
                    grdLoanDetails.DataBind();
                    grdLoanDetails.DataSource = null;

                    string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");


                    string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript(
                   this,
                   this.GetType(),
                   "showToastrError",
                   script,
                   true
               );
                }




                //// Calculate pagination count
                //int totalRecords = Loandt.Rows.Count;
                //int startRecord = (grdLoanDetails.PageIndex * grdLoanDetails.PageSize) + 1;
                //int endRecord = startRecord + grdLoanDetails.PageSize - 1;

                //if (endRecord > totalRecords)
                //    endRecord = totalRecords;

                //if (totalRecords > 0)
                //    lblLoanRcordCount.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
                //else
                //    lblLoanRcordCount.Text = "No records found";

         
            }
            catch (Exception ex)
            {
                // lblTotalEmicollection.Text = "Error";
            }

        }

        protected void btnLoanSearch_Click(object sender, EventArgs e)
        {
            GridBind();
        }

        protected void grdLoanDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
                return;

            if (e.CommandName == "EditRow")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                if (row.RowIndex >= 0)
                {
             
                    string Loancode = grdLoanDetails.DataKeys[row.RowIndex]["LoanCode"].ToString();
                    //int brandId = Convert.ToInt32(gvModels.DataKeys[row.RowIndex].Values["BrandID"]);
                    objLoan.Mode = "GET";
                    objLoan.Loanid = Loancode;
                    Loandt = objLoan.GetLoanDetails();

                   

                     string CustomerCode = Loandt.Rows[0]["CustomerCode"].ToString(); 
                     string CustomerFullName = Loandt.Rows[0]["CustomerFullName"].ToString();
                     string CustomerMobileNo = Loandt.Rows[0]["MobileNo"].ToString();
                     string CustomerEmailID = Loandt.Rows[0]["EmailID"].ToString();
                     string DealerCode = Loandt.Rows[0]["DealerCode"].ToString();
                     string DealerFullName = Loandt.Rows[0]["DealerFullName"].ToString();
                     string DealerMobileNo = Loandt.Rows[0]["DealerMobileNo"].ToString();
                     string DealerEmailID = Loandt.Rows[0]["DealerEmailID"].ToString();


                    txtLoanCode.Text = Loandt.Rows[0]["LoanCode"].ToString();
                    txtLoanAmount.Text = Loandt.Rows[0]["LoanAmount"].ToString();
                    txtDownPayment.Text = Loandt.Rows[0]["DownPayment"].ToString();
                    txtLoanEMIAmount.Text = Loandt.Rows[0]["EMIAmount"].ToString();
                    txtTenure.Text = Loandt.Rows[0]["Tenure"].ToString();
                    txtInterestRate.Text = Loandt.Rows[0]["InterestRate"].ToString();
                    txtStartDate.Text = Loandt.Rows[0]["StartDate"].ToString();
                    txtEndDate.Text = Loandt.Rows[0]["EndDate"].ToString();
                    txtCreditScore.Text= Loandt.Rows[0]["creditScore"].ToString();
                    //txtProcessingfees.Text = Loandt.Rows[0]["EmailID"].ToString();
                    txtLoanStatus.Text = Loandt.Rows[0]["Status"].ToString();
                    txtLoanCreatedBy.Text = Loandt.Rows[0]["DealerFullName"].ToString();
                    txtBrandName.Text = Loandt.Rows[0]["BrandName"].ToString();
                    txtModelName.Text = Loandt.Rows[0]["ModelName"].ToString();
                    txtModelVariant.Text = Loandt.Rows[0]["ModelVariant"].ToString();
                    txtIMEI1Number.Text = Loandt.Rows[0]["IMEINumber"].ToString();
                    txtremarks.Text = Loandt.Rows[0]["Remarks"].ToString();
                    // txtIMEI2Number.Text = Loandt.Rows[0]["EmailID"].ToString();
                    imgAadharfrontphoto.ImageUrl= ImageUrl+Loandt.Rows[0]["CustAadharPhoto_Path"].ToString();
                    //Aadharfrontimgzoom.Src = imgAadharfrontphoto.ImageUrl;
                    imgAadharbackphoto.ImageUrl = ImageUrl+Loandt.Rows[0]["CustAadharBackPhoto_Path"].ToString();
                    //Aadharbackimgzoom.Src = imgAadharbackphoto.ImageUrl;
                    imgIMEI1.ImageUrl = ImageUrl+Loandt.Rows[0]["IMEINumber1_SealPhotoPath"].ToString();
                    //IMEI1imgzoom.Src = imgIMEI1.ImageUrl;
                    imgIMEI2.ImageUrl = ImageUrl + Loandt.Rows[0]["IMEINumber1_SealPhotoPath"].ToString();
                    //IMEI2imgzoom.Src = imgIMEI2.ImageUrl;
                    imgInvoice.ImageUrl = ImageUrl + Loandt.Rows[0]["Invoive_Path"].ToString();
                    //Invoiceimgzoom.Src = imgInvoice.ImageUrl;
                    imgPan.ImageUrl = ImageUrl + Loandt.Rows[0]["CustPanNumberPhoto_Path"].ToString();
                    //Panimgzoom.Src = imgPan.ImageUrl;
                    imgIMEI.ImageUrl = ImageUrl + Loandt.Rows[0]["IMEINumber_PhotoPath"].ToString();
                   // IMEIimgzoom.Src = imgIMEI.ImageUrl;
                    imgCustPhoto.ImageUrl = ImageUrl + Loandt.Rows[0]["CustPhoto_path"].ToString();
                    //Custimgzoom.Src = imgCustPhoto.ImageUrl;

                    txtCustomerCode.Text = CustomerCode;
                    txtCustomerFullName.Text = CustomerFullName;
                    txtCustomerMobileNo.Text = CustomerMobileNo;
                    txtCustomerEmailid.Text = CustomerEmailID;
                    txtDealerid.Text = DealerCode;
                    txtDealerFullName.Text = DealerFullName;
                    txtDealerMobileNo.Text = DealerMobileNo;
                    txtDealerEmailid.Text = DealerEmailID;

                }

                Div_LaonApproval.Visible = true;
                Div_LoanSearch.Visible = false;
              //  grdLoanDetails.Visible = false;
            }
            //if (e.CommandName == "ActiveStatusRow")
            //{
            //    LinkButton lnk = (LinkButton)e.CommandSource;
            //    GridViewRow row = (GridViewRow)lnk.NamingContainer;
            //    if (row.RowIndex >= 0)
            //    {

            //        int rid = Convert.ToInt32(e.CommandArgument);
            //        string currentStatus = gvModels.DataKeys[row.RowIndex]["ActiveStatus"].ToString();
            //        string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

            //        //hfModelID.Value = rid.ToString();
            //        string brandName = row.Cells[1].Text.Trim();
            //        string modelName = row.Cells[2].Text.Trim();
            //        string remark = row.Cells[3].Text.Trim();
            //        string activeStatus = newStatus;
            //        objDeviceMaster.rid = rid;
            //        objDeviceMaster.activestatus = activeStatus;
            //        objDeviceMaster.task = "Active_YN";
            //        var result = objDeviceMaster.CRUDDeviceModelMaster();
            //        ViewState["ReturnMsg"] = result.ReturnCode;
            //        ViewState["ReturnMsg"] = result.ReturnMsg;

            //        if (result.ReturnCode == "200")
            //        {
            //            ShowMessage(result.ReturnMsg);
            //        }
            //        else
            //        {
            //            ShowMessage(result.ReturnMsg);
            //        }
            //        LoadGrid();
            //    }
            //}
        }


        protected void grdLoanDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvVariants.PageIndex = e.NewPageIndex;
            //BindGrid();
            GridBind();

            if (e.NewPageIndex >= 0 && e.NewPageIndex < grdLoanDetails.PageCount)
            {
                grdLoanDetails.PageIndex = e.NewPageIndex;
                GridBind();
            }
        }


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

        private void GetHoldingAmt()
        {
            try
            {
                DataTable dt = objTransfertoAgent.getCompanyholdamount();

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Assuming you're interested in the first row's ReserveValue
                    string reserveValue = Convert.ToString(dt.Rows[0]["ReserveValue"]);

                    // Assign it to the hidden field
                    hdnReserveValue.Value = reserveValue;
                }
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


    }
}