using AVFramework;
using System;
using System.Collections.Generic;
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

namespace TheEMIClubApplication.MasterPage
{
    public partial class ComapnyMst : System.Web.UI.Page
    {
        BLCompanyDetails objCMP = new BLCompanyDetails();
        int returnCode;
        string returnMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                if(AppSessions.SessionLoginId != null)
                {
                    GetCompanyDetails();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void GetCompanyDetails()
        {
            // ✅ Correct property
            objCMP.State = "Active";

            // ✅ Call SP only ONCE
            DataTable dtCompany = objCMP.getCompanyDetail(out returnCode, out returnMsg);

            if (returnCode == 1 && dtCompany != null && dtCompany.Rows.Count > 0)
            {
                DataRow row = dtCompany.Rows[0];

                // ================= BASIC INFO =================
                txtCompanyName.Text = row["CompanyName"]?.ToString();
                txtClientCode.Text = row["ClientCode"]?.ToString();
                txtAddress.Text = row["Address"]?.ToString();
                txtCity.Text = row["City"]?.ToString();
                txtState.Text = row["State"]?.ToString();
                txtCountry.Text = row["Country"]?.ToString();
                txtZip.Text = row["Zip"]?.ToString();
                txtPhone.Text = row["Phone"]?.ToString();
                txtFirstName.Text = row["FirstName"]?.ToString();
                txtLastName.Text = row["LastName"]?.ToString();
                txtEmail.Text = row["Email"]?.ToString();
                txtWebsite.Text = row["Website"]?.ToString();
                txtUsername.Text = row["Username"]?.ToString();

                // ================= DROPDOWNS (SAFE SET) =================
                if (ddlStatus.Items.FindByValue(row["Status"]?.ToString()) != null)
                    ddlStatus.SelectedValue = row["Status"].ToString();

                if (ddlReservetype.Items.FindByValue(row["ReserveType"]?.ToString()) != null)
                    ddlReservetype.SelectedValue = row["ReserveType"].ToString();

                if (ddlServiceChargeType.Items.FindByValue(row["ServicechargeType"]?.ToString()) != null)
                    ddlServiceChargeType.SelectedValue = row["ServicechargeType"].ToString();

                // ================= FINANCIAL =================
                txtMemberShipFees.Text = row["MemberShipFees"]?.ToString();
                txtLatefine.Text = row["LateFine"]?.ToString();
                txtreservevalues.Text = row["ReserveValue"]?.ToString();
                txtgraceperiod.Text = row["GracePeriod"]?.ToString();
                txtServiceCharge.Text = row["ServicechargeValue"]?.ToString();

                // ================= 🔥 SETTLEMENT CHARGE =================
                if (ddlSettlementChargeType.Items.FindByValue(row["SettlementChargeType"]?.ToString()) != null)
                    ddlSettlementChargeType.SelectedValue = row["SettlementChargeType"].ToString();

                txtSettlementCharge.Text = row["SettlementChargeValue"]?.ToString();

                // ================= 🔥 FORECLOSURE CHARGE =================
                if (ddlForeclosureChargeType.Items.FindByValue(row["ForeclosureChargeType"]?.ToString()) != null)
                    ddlForeclosureChargeType.SelectedValue = row["ForeclosureChargeType"].ToString();

                txtForeclosureCharge.Text = row["ForeclosureChargeValue"]?.ToString();
                txtminiholdamtrequest.Text = row["MiniholdamountRequestvalue"]?.ToString();

                // ================= LOGO =================
                if (!string.IsNullOrEmpty(row["logoPath"]?.ToString()))
                    img_Logo.ImageUrl = row["logoPath"].ToString();
            }
            else
            {
                // Optional: toastr / label
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "nodata",
                    $"toastr.warning('{returnMsg}', 'Info');",
                    true
                );
            }
        }

        protected void ddlReservetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReserveValidate();
        }

        protected void ddlForeclosureChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReserveValidate();
        }

        protected void ddlSettlementChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReserveValidate();
        }

        private void ReserveValidate()
        {
            try
            {
                /* ================= RESERVE ================= */
                switch (ddlReservetype.SelectedValue)
                {
                    case "Not Applicable":
                        txtreservevalues.Enabled = false;
                        txtreservevalues.Text = "0.00";
                        break;

                    case "Flat":
                    case "Percentage":
                        txtreservevalues.Enabled = true;
                        if (string.IsNullOrEmpty(txtreservevalues.Text))
                            txtreservevalues.Text = "0.00";
                        break;
                }

                /* ================= FORECLOSURE ================= */
                switch (ddlForeclosureChargeType.SelectedValue)
                {
                    case "NA":
                        txtForeclosureCharge.Enabled = false;
                        txtForeclosureCharge.Text = "0.00";
                        break;

                    case "Flat":
                    case "Percentage":
                        txtForeclosureCharge.Enabled = true;
                        if (string.IsNullOrEmpty(txtForeclosureCharge.Text))
                            txtForeclosureCharge.Text = "0.00";
                        break;
                }

                /* ================= SETTLEMENT ================= */
                switch (ddlSettlementChargeType.SelectedValue)
                {
                    case "NA":
                        txtSettlementCharge.Enabled = false;
                        txtSettlementCharge.Text = "0.00";
                        break;

                    case "Flat":
                    case "Percentage":
                        txtSettlementCharge.Enabled = true;
                        if (string.IsNullOrEmpty(txtSettlementCharge.Text))
                            txtSettlementCharge.Text = "0.00";
                        break;
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");
        }
        private void ShowToastr(string message, string type = "success", string title = "")
        {
            string script = $"toastr.{type}('{message.Replace("'", "")}', '{title}');";
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                Guid.NewGuid().ToString(),
                script,
                true
            );
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // ================= BASIC =================
                objCMP.FirstName = txtFirstName.Text.Trim();
                objCMP.LastName = txtLastName.Text.Trim();
                objCMP.Website = txtWebsite.Text.Trim();
                objCMP.ClientCode = txtClientCode.Text.Trim();
                objCMP.LogoPath = string.IsNullOrEmpty(img_Logo.ImageUrl) ? null : img_Logo.ImageUrl;

                // ================= FINANCIAL =================
                objCMP.Membershipfee = txtMemberShipFees.Text.Trim();
                objCMP.Latefine = txtLatefine.Text.Trim();

                objCMP.ReserveType = ddlReservetype.SelectedValue;
                objCMP.Reservevalues = decimal.TryParse(txtreservevalues.Text, out decimal reserve) ? reserve : 0;

                objCMP.gracevalues = int.TryParse(txtgraceperiod.Text, out int grace) ? grace : 0;

                objCMP.ServiceChargetype = ddlServiceChargeType.SelectedValue;
                objCMP.ServiceChargeValue = decimal.TryParse(txtServiceCharge.Text, out decimal sc) ? sc : 0;

                // ================= 🔥 SETTLEMENT =================
                objCMP.SettlementChargeType = ddlSettlementChargeType.SelectedValue;
                objCMP.SettlementChargeValue =
                    decimal.TryParse(txtSettlementCharge.Text, out decimal settleVal) ? settleVal : 0;

                // ================= 🔥 FORECLOSURE =================
                objCMP.ForeclosureChargeType = ddlForeclosureChargeType.SelectedValue;
                objCMP.ForeclosureChargeValue =
                    decimal.TryParse(txtForeclosureCharge.Text, out decimal forecloseVal) ? forecloseVal : 0;

                objCMP.MiniholdamountRequestvalue =
                    decimal.TryParse(txtminiholdamtrequest.Text, out decimal miniholdamtrequestVal) ? miniholdamtrequestVal : 0;

                // ================= DB CALL =================
                objCMP.UpdateCompanyDetail(out returnCode, out returnMsg);

                if (returnCode == 1)
                {
                    ScriptManager.RegisterStartupScript(
     this,
     this.GetType(),
     Guid.NewGuid().ToString(),
     "Swal.fire('Success','Data saved successfully','success');",
     true
 );
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "info",
                        $"toastr.warning('{returnMsg}', 'Info');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "error",
                    $"toastr.error('{ex.Message.Replace("'", "")}', 'Error');",
                    true
                );
            }
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //     objCMP.FirstName= txtFirstName.Text;
        //     objCMP.LastName= txtLastName.Text;
        //     objCMP.Latefine= txtLatefine.Text;
        //     objCMP.Membershipfee = txtMemberShipFees.Text ;
        //     objCMP.Website = txtWebsite.Text ;
        //     objCMP.ClientCode= txtClientCode.Text ;
        //     objCMP.ReserveType = ddlReservetype.SelectedValue;
        //     objCMP.Reservevalues = Convert.ToDecimal(txtreservevalues.Text.Trim());
        //     objCMP.LogoPath = img_Logo.ImageUrl;
        //     objCMP.gracevalues = Convert.ToInt32(txtgraceperiod.Text);
        //     objCMP.ServiceChargetype = ddlServiceChargeType.SelectedItem.Text;
        //     objCMP.ServiceChargeValue = Convert.ToDecimal(txtServiceCharge.Text.Trim());


        //    try
        //    {

        //    var responseResult = objCMP.updateCompanyDetail(out returnCode, out returnMsg);
        //    if (responseResult != null && returnCode == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{returnMsg}');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{returnMsg}');", true);
        //    }



        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError( '{ex}'  );", true);

        //    }

        //}

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (fileLogoPath.HasFile)
            {
                try
                {

                    string extension = Path.GetExtension(fileLogoPath.FileName).ToLower();
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
                    string fileName = Path.GetFileName(fileLogoPath.FileName);

                    // Add a unique identifier to prevent name collisions
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    string folderPath = Server.MapPath("~/Images/ComapnyLogo/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fullPath = Path.Combine(folderPath, uniqueFileName);
                    fileLogoPath.SaveAs(fullPath);

                    // Set image preview
                    string relativePath = "~/Images/ComapnyLogo/" + uniqueFileName;
                    img_Logo.ImageUrl = relativePath;
                    //imgPreview.Visible = true;

                    // Save path to ViewState for later use in Save/Update
                    ViewState["UploadedImagePath"] = relativePath;

                    // Optional message
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Image uploaded successfully.');", true);
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

       
    }
}