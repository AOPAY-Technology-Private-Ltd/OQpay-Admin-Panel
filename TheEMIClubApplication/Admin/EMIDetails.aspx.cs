using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.MasterPage
{
    public partial class EMIDetails : System.Web.UI.Page
    {
        BLEMIDetails objEMI = new BLEMIDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string decryptedRID = string.Empty;
                string encryptedRID = Request.QueryString["edocelor"];

                if (!string.IsNullOrEmpty(encryptedRID))
                {
                    try
                    {
                        encryptedRID = HttpUtility.UrlDecode(encryptedRID);
                        encryptedRID = encryptedRID.Replace(" ", "+");

                        while (encryptedRID.Length % 4 != 0) // padding fix
                        {
                            encryptedRID += "=";
                        }

                        decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
                    }
                    catch
                    {
                        decryptedRID = string.Empty; // fallback
                    }
                }

                objEMI.Loanid = decryptedRID;
                BindGrid();
            }
            ////string encryptedRID = Request.QueryString["edocelor"];
            ////string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
            ////string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
            //string decryptedRID = string.Empty;
            //string encryptedRID = Request.QueryString["edocelor"];
            //if (!string.IsNullOrEmpty(encryptedRID))
            //{
            //    //encryptedRID = HttpUtility.UrlDecode(encryptedRID); 
            //     decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
            //}



            //objEMI.Loanid = decryptedRID;
            //BindGrid();
        }

        protected void gvEMIPayments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Page")
                    return;

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //objEMI.ActiveStatus = ddlEmiStatus.SelectedItem.Text.ToString().Trim();
                //objEMI.EmiId = Convert.ToInt32(txtEMIId.Text);
                //objEMI.Customercode=txtCustomerCode.Text;
                //objEMI.Loanid=txtLoanId.Text.ToString().Trim();
                //objEMI.Mode = "UPDATE";
              var result=  objEMI.GetEMIDetails();
                if (result != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../CommonPages/Home.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDownloadReceipt_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemoveReceipt_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void BindGrid()
        {
            objEMI.Mode = "GET";

            DataTable dt = objEMI.GetEMIDetails();

            txtCustomerCode.Text = dt.Rows[0]["CustomerCode"].ToString();
            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            txtMiddleName.Text = dt.Rows[0]["MiddleName"].ToString();
            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
            txtEmailAddress.Text= dt.Rows[0]["EMailID"].ToString();
            txtPhoneNumber.Text = dt.Rows[0]["PrimaryMobileNumber"].ToString();
            txtBrand.Text = dt.Rows[0]["BrandName"].ToString();
            txtProductCode.Text = dt.Rows[0]["BrandName"].ToString();
            txtModel.Text = dt.Rows[0]["ModelName"].ToString();
            txtVariant.Text = dt.Rows[0]["ModelVariant"].ToString();
            txtColor.Text = dt.Rows[0]["color"].ToString();
            txtLoanCode.Text = dt.Rows[0]["LoanCode"].ToString();
            txtLoanAmount.Text = dt.Rows[0]["LoanAmount"].ToString();
            txtDownPayment.Text = dt.Rows[0]["DownPayment"].ToString();
            txtLoanEMIAmount.Text = dt.Rows[0]["LoanEMIAmount"].ToString();
            txtTenure.Text = dt.Rows[0]["Tenure"].ToString();
            txtInterestRate.Text = dt.Rows[0]["InterestRate"].ToString();

            if (dt.Rows[0]["StartDate"] != DBNull.Value)
            {
                txtStartDate.Text  = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("dd-MM-yyyy");
            }
            else
            {
                txtStartDate.Text = "";
            }

            if (dt.Rows[0]["EndDate"] != DBNull.Value)
            {
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("dd-MM-yyyy");
                //txtEndDate.Text = dt.Rows[0]["EndDate"].ToString();
            }
            else
            {
                txtEndDate.Text = "";
            }

        

            txtIMEINumber.Text = dt.Rows[0]["IMEINumber"].ToString();
            txtLoanStatus.Text = dt.Rows[0]["LoanStatus"].ToString();
         //   txtLoanCreatedBy.Text = dt.Rows[0]["LoanCreatedBy"].ToString();
            txtPaidEMI.Text = dt.Rows[0]["PaidEMI"].ToString();
            txtDuesEMI.Text = dt.Rows[0]["DuesEMI"].ToString();
            txtReceivedEMIAmount.Text = dt.Rows[0]["ReceivedEMIAmount"].ToString();

          
            if (dt.Rows[0]["PaymentDate"] != DBNull.Value)
            {
                txtPaymentDate.Text = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]).ToString("dd-MM-yyyy");
                //  txtPaymentDate.Text = dt.Rows[0]["PaymentDate"].ToString();
            }
            else
            {
                txtPaymentDate.Text = "";
            }

            txtPaidAmount.Text = dt.Rows[0]["PaidAmount"].ToString();
            txtPaymentMode.Text = dt.Rows[0]["PaymentMode"].ToString();
           // txtUTRNumber.Text = dt.Rows[0]["UTRNumber"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
           // txtReceiptNo.Text = dt.Rows[0]["ReceiptNo"].ToString();
            string PaymentStatus =  dt.Rows[0]["PaymentActiveStatus"].ToString();
            //ddlPaymentStatus.Text = dt.Rows[0]["PaymentActiveStatus"].ToString();

            if (ddlPaymentStatus.Items.FindByText(PaymentStatus) != null)
            {
                ddlPaymentStatus.SelectedItem.Text = PaymentStatus;
            }
            else
            {
                ddlPaymentStatus.Items.Insert(0, "--Select--");
            }
            string RecordStatus = dt.Rows[0]["RecordStatus"].ToString();
            //ddlRecordStatus.Text = dt.Rows[0]["RecordStatus"].ToString();

            if (ddlRecordStatus.Items.FindByText(RecordStatus) != null)
            {
                ddlRecordStatus.SelectedItem.Text = RecordStatus;
            }
            else
            {
                ddlPaymentStatus.Items.Insert(0, "--Select--");
            }


            string imagereceipt = dt.Rows[0]["ReceiptImage_Path"].ToString();
            if (string.IsNullOrWhiteSpace(imagereceipt) && imagereceipt !=null)
            {
                imgReceiptPreview.ImageUrl = imagereceipt;
            }
            else
            {
                imgReceiptPreview.ImageUrl = "../Images/image icon.png";
            }
            
        }

        protected void btnDownloadReceipt_Click1(object sender, EventArgs e)
        {
            string imageUrl = imgReceiptPreview.ImageUrl;

            if (imageUrl.StartsWith("../Images/image icon.png"))
            {
                lblimgError.Text = "File Not Found !";
            }
            else
            { 
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
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");
        }
    }
}