using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.MasterPage
{
    public partial class DeviceVariantMaster : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLDeviceMaster objBL = new BLDeviceMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSessions.SessionLoginId != null)
                {
                    BindBrands();
                    BindEmptyModel();
                    BindGrid();
                    btnCreateVariant.Visible = true;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string finalResult = string.Empty;
            decimal v_MRPprice =0;
            decimal v_SellingPrice = 0;

            v_MRPprice = Convert.ToDecimal(txtMRPPrice.Text);
            v_SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);

            objBL.userid = "Admin";

            Page.Validate();

            if (!Page.IsValid)
            {

                return;
            }

            if (btnSave.Text.ToUpper() == "SAVE")
            {
               //if( ddlBrand.SelectedItem.Text == "-- Select Brand --")
               // {
               //     lblErrorBarnd.Text = "* Select Brand";
               //     lblErrorBarnd.CssClass = "text-danger";
               //     return;
               // }
               // if (ddlBrand.SelectedItem.Text == "-- Select Model --")
               // {
               //     lblErrorModel.Text = "* Select Model";
               //     lblErrorModel.CssClass = "text-danger";
               //     return;
               // }
                if(v_SellingPrice >= v_MRPprice)
                {
                    lblSellingValidation.Text = "* Selling Price cannot be greater than MRP Price.";
                    return;
                }
                decimal downPayment = Convert.ToDecimal(txtDownPaymentPerc.Text);
                if (downPayment < 0 || downPayment > 100)
                {
                    lblDownpaymentvalidation.Text = "* Downpayment must be between 0% and 100%.";
                    return;
                }
                else
                {
                    lblDownpaymentvalidation.Text = "";
                }

               
                decimal interest = Convert.ToDecimal(txtInterestPerc.Text);
                if (interest < 0 || interest > 100)
                {
                    lblintrestvaliadtion.Text = "* Interest rate must be between 0% and 100%.";
                    return;
                }
                else
                {
                    lblintrestvaliadtion.Text = "";
                }

                
                int tenure = Convert.ToInt32(txtTenure.Text);
                if (tenure <= 0 || tenure > 120)
                {
                    lbltanurevaliadtion.Text = "* Tenure must be between 1 and 120 months.";
                    return;
                }
                else
                {
                    lbltanurevaliadtion.Text = "";
                }



                finalResult = opration("INS");
            }
            else
            {

                objBL.rid = Convert.ToInt32(hfRID.Value);
                finalResult = opration("UPD");
            }

            if (finalResult == "200")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                BindGrid();
                ClearFields();
                btnSave.Text = "Save";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string finalResult = string.Empty;
            decimal v_MRPprice = 0;
            decimal v_SellingPrice = 0;

            v_MRPprice = Convert.ToDecimal(txtMRPPrice.Text);
            v_SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
            if (v_SellingPrice > v_MRPprice)
            {
                lblSellingValidation.Text = "* Selling Price cannot be greater than MRP Price.";
                return;
            }
            decimal downPayment = Convert.ToDecimal(txtDownPaymentPerc.Text);
            if (downPayment < 0 || downPayment > 100)
            {
                lblDownpaymentvalidation.Text = "* Downpayment must be between 0% and 100%.";
                return;
            }
            else
            {
                lblDownpaymentvalidation.Text = " ";
            }


            decimal interest = Convert.ToDecimal(txtInterestPerc.Text);
            if (interest < 0 || interest > 100)
            {
                lblintrestvaliadtion.Text = "* Interest rate must be between 0% and 100%.";
                return;
            }
            else
            {
                lblintrestvaliadtion.Text = " ";
            }


            int tenure = Convert.ToInt32(txtTenure.Text);
            if (tenure <= 0 || tenure > 120)
            {
                lbltanurevaliadtion.Text = "* Tenure must be between 1 and 120 months.";
                return;
            }
            else
            {
                lbltanurevaliadtion.Text = " ";
            }


            objBL.rid = Convert.ToInt32(hfRID.Value);
            finalResult = opration("UPD");
            if (finalResult == "200")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                gvVariants.Visible = true;
                BindGrid();
                ClearFields();
                btnSave.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                BindGrid();
            }
        }

        private string opration(string tasktype)
        {
            objBL.task = tasktype;
            objBL.rid = string.IsNullOrEmpty(hfRID.Value) ? 0 : Convert.ToInt32(hfRID.Value);
            objBL.Brandname = ddlBrand.SelectedItem.Text;
            objBL.Brandmodelname = ddlModel.SelectedItem.Text;
            objBL.VariantName = txtVariantName.Text.Trim();
            objBL.SellingPrice = string.IsNullOrEmpty(txtSellingPrice.Text) ? 0 : Convert.ToDecimal(txtSellingPrice.Text.Trim());
            objBL.Mrpprice= string.IsNullOrEmpty(txtMRPPrice.Text) ? 0 : Convert.ToDecimal(txtMRPPrice.Text.Trim());
            objBL.avlbColors = txtColors.Text.Trim();
            objBL.DownPaymentPerc = txtDownPaymentPerc.Text.Trim();
            objBL.InterestPerc = txtInterestPerc.Text.Trim();
            objBL.Tenure = txtTenure.Text.Trim();
            objBL.ProcessingFees = txtProcessingFees.Text.Trim();
            objBL.remark = txtRemark.Text.Trim();
            objBL.activestatus = ddlActiveStatus.SelectedValue;
            //objBL.ImagePath = fuImage;
            //imgPreview.ImageUrl= "~/Images/Variants/Default/defaultimg.png";
            objBL.userid = "Admin";

            if (ViewState["UploadedImagePath"] != null)
            {
                objBL.ImagePath = ViewState["UploadedImagePath"].ToString();
            }
            else
            {
                objBL.ImagePath = string.Empty;
            }

            var result = objBL.CRUDDeviceVariantMaster();
            ViewState["ReturnMsg"] = result.ReturnMsg;
            return result.ReturnCode;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (btnClear.Text.ToLower().Trim()=="close".ToLower().Trim())
            {
                Response.Redirect("../CommonPages/Home.aspx");
            }

            ClearFields();
        }

        private void ClearFields()
        {
            hfRID.Value = string.Empty;
            //ddlBrand.Items.Insert(0, new ListItem("--Select Model--", ""));
            ddlBrand.SelectedIndex = 0;
            //ddlModel.Items.Insert(0, new ListItem("--Select Model--", ""));
            ddlModel.SelectedIndex = 0;
            txtTenure.Text = "";
            txtVariantName.Text = "";
            txtRemark.Text = "";
            txtColors.Text = "";
            txtSellingPrice.Text = "";
            txtDownPaymentPerc.Text = "";
            txtInterestPerc.Text = "";
            txtProcessingFees.Text = "";
            ddlActiveStatus.SelectedValue = "Y";
            ViewState["UploadedImagePath"] = null;
            imgPreview.ImageUrl = "~/Images/Variants/Default/defaultimg.png";
            txtMRPPrice.Text = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            lblSellingValidation.Text = "";
            lbltanurevaliadtion.Text = "";
            lblSellingValidation.Text = "";
            lblDownpaymentvalidation.Text = "";
            lblintrestvaliadtion.Text = "";
            //imgPreview.Visible = false;
            // btnSave.Text = "Save";
        }

        protected void gvVariants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
                return;

            LinkButton lnk = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            if (row.RowIndex >= 0)
            {
                int rid = Convert.ToInt32(gvVariants.DataKeys[row.RowIndex].Value);
                string imagepath = gvVariants.DataKeys[row.RowIndex]["ImagePath"].ToString();

                string brandName = row.Cells[0].Text.Trim();
                string modelName = row.Cells[1].Text.Trim();
                string variantName = row.Cells[2].Text.Trim();
                string sellingPrice = row.Cells[3].Text.Replace("₹", "").Replace(",", "").Trim();
                string mrpPrice = row.Cells[4].Text.Replace("₹", "").Replace(",", "").Trim();
                string color = row.Cells[5].Text.Trim();
                string DownPayment = row.Cells[6].Text.Trim();
                string intrest = row.Cells[7].Text.Trim();
                string tenour = row.Cells[8].Text.Trim();
                string proccessingfee = row.Cells[9].Text.Trim();
                string remark = row.Cells[10].Text.Trim();
                //string imagepath = row.Cells[11].Text.Trim();
                //Image img = (Image)row.FindControl("imgDevice");
                //if (img != null)
                //{
                //    imagepath = img.ImageUrl;
                //}
                string activeStatus = row.Cells[12].Text.Trim();

                ddlBrand.ClearSelection();
               
                ListItem Branditem = ddlBrand.Items.FindByText(brandName);
                if (Branditem != null)
                {
                    Branditem.Selected = true;
                }
                else
                {
                    ddlBrand.SelectedIndex = 0;
                }
                ddlModel.ClearSelection();
                ListItem modelitem = ddlBrand.Items.FindByValue(modelName);
                if (modelitem != null)
                {
                    modelitem.Selected = true;
                }
                else
                {
                    ddlModel.SelectedIndex = 0;
                }


                if (e.CommandName == "EditRow")
                {
             
                    hfRID.Value = rid.ToString();
                    ddlBrand.SelectedItem.Text = brandName.Trim();
                  
                    string selectedBrandID = ddlBrand.SelectedItem.Text;

                  
                    if (!string.IsNullOrEmpty(selectedBrandID) && selectedBrandID != "-- Select Brand --")
                    {
                        PortalCommon.BindDropDownList(ddlModel, Constants.Flage_ModelNameDDL, selectedBrandID
                            , string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Model --");
                        ddlModel.SelectedItem.Text = modelName;

                    }
    

                  //  ddlBrand_SelectedIndexChanged(null, null); // to bind model dropdown

                    // Set Inputs
                    txtVariantName.Text = variantName;
                    txtSellingPrice.Text = sellingPrice;
                    txtMRPPrice.Text = mrpPrice;
                    txtRemark.Text = remark;
                    ddlActiveStatus.SelectedValue = activeStatus;

                    // Optional: Fill financial fields and color if they are shown in GridView
                    txtColors.Text = color;
                    txtDownPaymentPerc.Text = DownPayment;
                    txtInterestPerc.Text = intrest;
                    txtTenure.Text = tenour;
                    txtProcessingFees.Text = proccessingfee;
                    btnimgRemove.Visible = true;
                    btnUploadImage.Visible = true;
                    // Optional: Preview image if needed from ImagePath column

                    //objBL.rid = rid;
                    //objBL.task = "GET";
                    //var resilt  =objBL.GetDeviceVariantMaster();





                    //HiddenField hfImagePathControl = row.FindControl("hfImagePath") as HiddenField;
                    //string imagePath = hfImagePathControl != null ? hfImagePathControl.Value?.Trim() : null;
                    if (!string.IsNullOrEmpty(imagepath))
                    {
                        imgPreview.ImageUrl = imagepath;
                        imgPreview.Visible = true;
                        ViewState["UploadedImagePath"] = imagepath;
                    }
                    else
                    {
                        //imgPreview.Visible = false;
                        ViewState["UploadedImagePath"] = "~/Images/Variants/Default/defaultimg.png";
                    }

                    divSeachvariant.Visible = false;
                    divSaveVariant.Style["display"] = "block";
                    gvVariants.Visible = false;
                    btnSave.Visible = false;
                    //lblRecordCount.Visible = false;
                    btnCreateVariant.Visible = false;
                    btnClear.Visible= false;
                    //btnClear.Text = "Close";
                    //btnClear.CssClass = "btn btn-danger";
                    //btnSave.Enabled = false;
                }
                else if (e.CommandName == "ActiveStatusRow")
                {
                    objBL.rid = rid;
                    objBL.activestatus = activeStatus;

                    var result = opration(objBL.task = "Active_YN");

                    if (result != null)
                    {
                        ShowMessage("Active/Inactive status updated successfully.");
                        BindGrid(); // refresh after update
                    }
                }
            }
            else
            {

                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
        private void BindBrands()
        {
            PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Brand --");
        }
        protected void BindGrid()
        {
            objBL.task = "GET";
            //gvVariants.DataSource = objBL.GetDeviceVariantMaster();
            //gvVariants.DataBind();

            DataTable dt = objBL.GetDeviceVariantMaster(); 
            gvVariants.DataSource = dt;
            gvVariants.DataBind();

            // Calculate pagination count
            int totalRecords = dt.Rows.Count; 
            int startRecord = (gvVariants.PageIndex * gvVariants.PageSize) + 1;
            int endRecord = startRecord + gvVariants.PageSize - 1;

            if (endRecord > totalRecords)
                endRecord = totalRecords;

            //if (totalRecords > 0)
            //    lblRecordCount.Text = "Showing {startRecord}–{endRecord} of {totalRecords} records";
            //else
            //    lblRecordCount.Text = "No records found";
        }
        private void BindEmptyModel()
        {
            ddlModel.Items.Clear();
            ddlModel.Items.Insert(0, new ListItem("-- Select Model --", ""));
        }
        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrandID = ddlBrand.SelectedItem.Text;

            if (!string.IsNullOrEmpty(selectedBrandID) && selectedBrandID != "-- Select Brand --")
            {
                PortalCommon.BindDropDownList(ddlModel, Constants.Flage_ModelNameDDL,
                    selectedBrandID, string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Model --");
            }
            else
            {
                BindEmptyModel();
            }
        }

        protected void gvVariants_DataBound(object sender, EventArgs e)
        {
            // Get pager row
            GridViewRow pagerRow = gvVariants.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvVariants.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvVariants.PageIndex < gvVariants.PageCount - 1;
            }
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (fuImage.HasFile)
            {
                try
                {

                    string extension = Path.GetExtension(fuImage.FileName).ToLower();
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
                    string fileName = Path.GetFileName(fuImage.FileName);

                    // Add a unique identifier to prevent name collisions
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    string folderPath = Server.MapPath("~/Images/Variants/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fullPath = Path.Combine(folderPath, uniqueFileName);
                    fuImage.SaveAs(fullPath);

                    // Set image preview
                    string relativePath = "~/Images/Variants/" + uniqueFileName;
                    imgPreview.ImageUrl = relativePath;
                    imgPreview.Visible = true;

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
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('Device Model', '{message}');", true);
        }
        protected void gvVariants_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgVariant = (Image)e.Row.FindControl("productimage");
                string imgPath = DataBinder.Eval(e.Row.DataItem, "ImagePath")?.ToString();

                if (string.IsNullOrEmpty(imgPath))
                {
                    imgVariant.ImageUrl = "~/Images/Variants/Default/defaultimg.png";
                }
                else
                {
                    imgVariant.ImageUrl = imgPath;
                }
            }
        }

        protected void btnimgRemove_Click(object sender, EventArgs e)
        {
            // Set the image to default placeholder
            imgPreview.ImageUrl = "~/Images/Variants/Default/defaultimg.png";
            imgPreview.Visible = true;

            // Clear the ViewState and any saved path if used
            ViewState["UploadedImagePath"] = null;

            // Optional: If you use a hidden field to hold the path
            //hfImagePath.Value = string.Empty;
        }


        protected void gvVariants_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvVariants.PageIndex = e.NewPageIndex;
            //BindGrid();
            BindGrid();

            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvVariants.PageCount)
            {
                gvVariants.PageIndex = e.NewPageIndex;
                BindGrid(); 
            }
        }

        protected void btnCreateVariant_Click(object sender, EventArgs e)
        {
            ClearFields();
            btnClear.Text = "Clear";
            btnClear.CssClass = "btn btn-secondary";
            btnSave.Visible = true;
            gvVariants.Visible=false;
            btnUpdate.Visible = false;
            btnCreateVariant.Visible = false;
            divSaveVariant.Style["display"] = "block";
            divSeachvariant.Style["display"] = "none";
            //lblRecordCount.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "all".ToUpper().Trim())
            {
                objBL.task = "GET";
                gvVariants.DataSource = objBL.GetDeviceVariantMaster();
                gvVariants.DataBind();

            }
            if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "BRAND".ToUpper().Trim())
            {
                objBL.Brandname = txtSearchValues.Text.ToUpper().Trim();

            }
            if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "Model".ToUpper().Trim())
            {
                objBL.Brandmodelname = txtSearchValues.Text.ToUpper().Trim();
            }
            if (ddlSearchValues.SelectedValue.ToUpper() == "Variant".ToUpper().Trim())
            {
                objBL.VariantName = txtSearchValues.Text.ToUpper().Trim();
            }
            if (ddlSearchValues.SelectedValue.ToUpper() == "Color".ToUpper().Trim())
            {
                objBL.avlbColors = txtSearchValues.Text.ToUpper().Trim();
            }
            if (ddlSearchValues.SelectedValue.ToUpper() == "Price".ToUpper().Trim())
            {
                objBL.Mrpprice = Convert.ToDecimal(txtSearchValues.Text.ToUpper().Trim());
            }
            if (ddlSearchValues.SelectedValue.ToUpper() == "Status".ToUpper().Trim())
            {
                objBL.activestatus = txtSearchValues.Text.ToUpper().Trim();
            }
            txtSearchValues.Text = string.Empty;

            objBL.task = "GET";
            gvVariants.DataSource = objBL.GetDeviceVariantMaster();
            gvVariants.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            divSeachvariant.Style["display"] = "block";
            divSaveVariant.Style["display"] = "none";
            gvVariants.Visible = true;
            //divSeachvariant.Visible = true;

        }
    }
}