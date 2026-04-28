using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.Admin
{
    public partial class CreateVariantMaster : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLDeviceMaster objBL = new BLDeviceMaster();
        BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ✅ Session check
                if (AppSessions.SessionLoginId == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                // Bind brands
                BindBrands();

                // Initialize variants for first load
                ViewState["Variants"] = new List<Variant> { new Variant() };
                BindVariantTable();

                // If editing existing variant (RID in query string)
                if (!string.IsNullOrWhiteSpace(Request.QueryString["groupid"]))
                {
                    try
                    {
                        string rid= HttpUtility.UrlDecode(Request.QueryString["groupid"].Trim());
                        hdfgroupid.Value = rid;
                        LoadVariantData(rid); 
                        // Your method to load existing variant details
                    }
                    catch (Exception ex)
                    {
                        // Optional: log error or show a message
                        // lblMessage.Text = "Invalid or corrupted link.";
                    }
                }
            }
            else
            {
                // 🔄 Rebind to retain dynamic rows on postback
                BindVariantTable();
            }
        }

        private void LoadVariantData(string groupid)
        {
            objBL.task = "Reterive";
            objBL.groupid = Convert.ToInt32(groupid);

            DataTable dt = objBL.GetDeviceVariantMaster1();

            if (dt != null && dt.Rows.Count > 0)
            {
                // ✅ Use first row for common product-level data
                DataRow firstRow = dt.Rows[0];

                hfRID.Value = firstRow["RID"].ToString();

                // 1️⃣ Brand Dropdown
                PortalCommon.BindDropDownList(
                    ddlBrand,
                    Constants.Flage_BrandNameDDL,
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    "-- Select Brand --"
                );

                ListItem brandItem = ddlBrand.Items.FindByText(firstRow["BrandName"].ToString());
                if (brandItem != null)
                {
                    ddlBrand.ClearSelection();
                    brandItem.Selected = true;
                }

                // 2️⃣ Model Dropdown (after brand selection)
                PortalCommon.BindDropDownList(
                    ddlModel,
                    Constants.Flage_ModelNameDDL,
                    ddlBrand.SelectedItem.Text,
                    string.Empty, string.Empty, string.Empty, string.Empty,
                    "-- Select Model --"
                );

                ListItem modelItem = ddlModel.Items.FindByText(firstRow["ModelName"].ToString());
                if (modelItem != null)
                {
                    ddlModel.ClearSelection();
                    modelItem.Selected = true;
                }

            
                txtRemark.Text = firstRow["Remark"].ToString();

                ListItem statusItem = ddlActiveStatus.Items.FindByValue(firstRow["ActiveStatus"].ToString());
                if (statusItem != null)
                {
                    ddlActiveStatus.ClearSelection();
                    statusItem.Selected = true;
                }

                
                imgPreview.ImageUrl = firstRow["ImagePath"].ToString();

                

                btnSave.Visible = false;
                btnUpdate.Visible = true;

                // ✅ 4️⃣ Load all variant rows into table
                var variants = new List<Variant>();
                foreach (DataRow row in dt.Rows)
                {
                    variants.Add(new Variant
                    {
                        RID = row["RID"] != DBNull.Value ? Convert.ToInt32(row["RID"]) : 0,
                        VariantName = row["VariantName"].ToString(),
                        Color = row["avlbColors"].ToString(),
                        MRPPrice = row["MrpPrice"].ToString(),
                        SellingPrice = row["SellingPrice"].ToString(),
                        DownPayment = row["DownPaymentPerc"].ToString(),
                        Interest = row["InterestPerc"].ToString(),
                        Tenure = row["Tenure"].ToString(),
                        ProcessingFees = row["ProcessingFees"].ToString()
                    });
                }

                ViewState["Variants"] = variants;
                BindVariantTable();
            }
        }

        //private void LoadVariantData(string rid)
        //{
        //    objBL.task = "Reterive";
        //    objBL.rid = Convert.ToInt32(rid);

        //    DataTable dt = objBL.GetDeviceVariantMaster();

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataRow row = dt.Rows[0];

        //        // Hidden RID
        //        hfRID.Value = row["RID"].ToString();

        //        // 1️⃣ Bind Brand dropdown
        //        PortalCommon.BindDropDownList(
        //            ddlBrand,
        //            Constants.Flage_BrandNameDDL,
        //            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
        //            "-- Select Brand --"
        //        );

        //        // 2️⃣ Set selected Brand by Text
        //        ListItem brandItem = ddlBrand.Items.FindByText(row["BrandName"].ToString());
        //        if (brandItem != null)
        //        {
        //            ddlBrand.ClearSelection();
        //            brandItem.Selected = true;
        //        }

        //        // 3️⃣ Bind Model dropdown for selected brand
        //        PortalCommon.BindDropDownList(
        //            ddlModel,
        //            Constants.Flage_ModelNameDDL,
        //            ddlBrand.SelectedItem.Text,   // Brand name (since no ID)
        //            string.Empty, string.Empty, string.Empty, string.Empty,
        //            "-- Select Model --"
        //        );

        //        // 4️⃣ Set selected Model by Text
        //        ListItem modelItem = ddlModel.Items.FindByText(row["ModelName"].ToString());
        //        if (modelItem != null)
        //        {
        //            ddlModel.ClearSelection();
        //            modelItem.Selected = true;
        //        }

        //        // Variant name
        //        txtVariantName.Text = row["VariantName"].ToString();

        //        // Prices
        //        txtSellingPrice.Text = row["SellingPrice"].ToString();
        //        txtMRPPrice.Text = row["MrpPrice"].ToString();

        //        // Remark & Status
        //        txtRemark.Text = row["Remark"].ToString();
        //        ListItem statusItem = ddlActiveStatus.Items.FindByValue(row["ActiveStatus"].ToString());
        //        if (statusItem != null)
        //        {
        //            ddlActiveStatus.ClearSelection();
        //            statusItem.Selected = true;
        //        }

        //        // Other details
        //        txtColors.Text = row["avlbColors"].ToString();
        //        imgPreview.ImageUrl = row["ImagePath"].ToString();

        //        // Loan/Finance fields
        //        txtDownPaymentPerc.Text = row["DownPaymentPerc"].ToString();
        //        txtInterestPerc.Text = row["InterestPerc"].ToString();
        //        txtTenure.Text = row["Tenure"].ToString();
        //        txtProcessingFees.Text = row["ProcessingFees"].ToString();
        //        btnSave.Visible = false;
        //        btnUpdate.Visible = true;

        //    }
        //}




        protected void btnAddVariant_Click(object sender, EventArgs e)
        {
            var variants = new List<Variant>();

            // Save current rows
            for (int i = 1; i < tblVariants.Rows.Count; i++) // Skip header
            {
                TableRow row = tblVariants.Rows[i];
                variants.Add(new Variant
                {
                    RID = int.TryParse(((TextBox)row.Cells[0].Controls[0]).Text, out int ridVal) ? ridVal : 0,
                    VariantName = ((TextBox)row.Cells[1].Controls[0]).Text.Trim(),
                    Color = ((TextBox)row.Cells[2].Controls[0]).Text.Trim(),
                    MRPPrice = ((TextBox)row.Cells[3].Controls[0]).Text.Trim(),
                    SellingPrice = ((TextBox)row.Cells[4].Controls[0]).Text.Trim(),
                    DownPayment = ((TextBox)row.Cells[5].Controls[0]).Text.Trim(),
                    Interest = ((TextBox)row.Cells[6].Controls[0]).Text.Trim(),
                    Tenure = ((TextBox)row.Cells[7].Controls[0]).Text.Trim(),
                    ProcessingFees = ((TextBox)row.Cells[8].Controls[0]).Text.Trim()
                });
            }

            // Add new empty row
            variants.Add(new Variant());
            ViewState["Variants"] = variants;
            BindVariantTable();
        }


        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var variants = ViewState["Variants"] as List<Variant> ?? new List<Variant>();
            Button btn = (Button)sender;
            int index = int.Parse(btn.CommandArgument);

            if (index >= 0 && index < variants.Count)
            {
                var selectedItem = variants[index];

                // 🔴 Call SP only if record exists in DB
                if (selectedItem.RID > 0)
                {
                    string returnCode = "";
                    string returnMsg = "";

                    using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_Delete_Variant", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@RID", selectedItem.RID);
                            cmd.Parameters.AddWithValue("@groupID", HttpUtility.UrlDecode(Request.QueryString["groupid"].Trim()));

                            SqlParameter outCode = new SqlParameter("@p_ReturnCode", SqlDbType.VarChar, 10)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outCode);

                            SqlParameter outMsg = new SqlParameter("@p_ReturnMsg", SqlDbType.VarChar, 255)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outMsg);

                            con.Open();
                            cmd.ExecuteNonQuery();

                            returnCode = outCode.Value.ToString();
                            returnMsg = outMsg.Value.ToString();
                        }
                    }

                    // ❌ If DB failed → stop removal
                    if (returnCode != "200")
                    {
                        // You can show message here
                        return;
                    }
                }
                // ✅ Remove from ViewState (UI)
                variants.RemoveAt(index);
            }
            ViewState["Variants"] = variants;
            BindVariantTable();
        }

        private void BindVariantTable()
        {
            var variants = ViewState["Variants"] as List<Variant> ?? new List<Variant>();
            tblVariants.Rows.Clear();

            // ===== HEADER ROW =====
            TableHeaderRow headerRow = new TableHeaderRow();

            // ✅ RID header now visible
            TableHeaderCell hRID = new TableHeaderCell { Text = "RID", Visible = false };
            headerRow.Cells.Add(hRID);

            headerRow.Cells.Add(new TableHeaderCell { Text = "Variant Name" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Available Colors" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "MRP Price" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Selling Price" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Down Payment %" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Interest %" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Tenure (Months)" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Processing Fees" });
            headerRow.Cells.Add(new TableHeaderCell { Text = "Action" });

            tblVariants.Rows.Add(headerRow);

            // ===== DATA ROWS =====
            for (int i = 0; i < variants.Count; i++)
            {
                TableRow row = new TableRow();

                // ✅ RID now visible (read-only)
                TextBox txtRID = new TextBox
                {
                    ID = $"txtRID_{i}",
                    CssClass = "form-control text-center",
                    Text = variants[i].RID.ToString(),
                    ReadOnly = true,
                    Width = Unit.Pixel(80)
                };

                // Create cell and hide it visually
                TableCell ridCell = new TableCell();
                ridCell.Controls.Add(txtRID);
                ridCell.Attributes["style"] = "display:none;"; // hide column

                row.Cells.Add(ridCell);

                // Other visible fields
                TextBox txtVariant = new TextBox { ID = $"txtVariant_{i}", CssClass = "form-control", Text = variants[i].VariantName };
                TextBox txtColor = new TextBox { ID = $"txtColor_{i}", CssClass = "form-control", Text = variants[i].Color };
                TextBox txtMRP = new TextBox { ID = $"txtMRP_{i}", CssClass = "form-control", Text = variants[i].MRPPrice };
                TextBox txtSelling = new TextBox { ID = $"txtSelling_{i}", CssClass = "form-control", Text = variants[i].SellingPrice };
                TextBox txtDown = new TextBox { ID = $"txtDown_{i}", CssClass = "form-control", Text = variants[i].DownPayment };
                TextBox txtInterest = new TextBox { ID = $"txtInterest_{i}", CssClass = "form-control", Text = variants[i].Interest };
                TextBox txtTenure = new TextBox { ID = $"txtTenure_{i}", CssClass = "form-control", Text = variants[i].Tenure };
                TextBox txtFees = new TextBox { ID = $"txtFees_{i}", CssClass = "form-control", Text = variants[i].ProcessingFees };

                row.Cells.Add(new TableCell { Controls = { txtVariant } });
                row.Cells.Add(new TableCell { Controls = { txtColor } });
                row.Cells.Add(new TableCell { Controls = { txtMRP } });
                row.Cells.Add(new TableCell { Controls = { txtSelling } });
                row.Cells.Add(new TableCell { Controls = { txtDown } });
                row.Cells.Add(new TableCell { Controls = { txtInterest } });
                row.Cells.Add(new TableCell { Controls = { txtTenure } });
                row.Cells.Add(new TableCell { Controls = { txtFees } });

                // Remove button
                Button btnRemove = new Button
                {
                    ID = $"btnRemove_{i}",
                    Text = "Remove",
                    CssClass = "btn btn-danger btn-sm",
                    CommandArgument = i.ToString()
                };
                btnRemove.Click += BtnRemove_Click;
                row.Cells.Add(new TableCell { Controls = { btnRemove } });

                tblVariants.Rows.Add(row);
            }
        }



        [Serializable]
        public class Variant
        {
            public int RID { get; set; } = 0;
            public string VariantName { get; set; } = "";
            public string Color { get; set; } = "";
            public string MRPPrice { get; set; } = "";
            public string SellingPrice { get; set; } = "";
            public string DownPayment { get; set; } = "";
            public string Interest { get; set; } = "";
            public string Tenure { get; set; } = "";
            public string ProcessingFees { get; set; } = "";
            public string groupID { get; set; } = "";
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrandID = ddlBrand.SelectedItem.Text;

            if (!string.IsNullOrEmpty(selectedBrandID) && selectedBrandID != "-- Select Brand --")
            {
                PortalCommon.BindDropDownList(ddlModel, Constants.Flage_ModelNameDDL,
                    selectedBrandID, string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Model --");
            }
          
        }
        private void BindBrands()
        {
            PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Brand --");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string finalResult = string.Empty;
            decimal v_MRPprice = 0;
            decimal v_SellingPrice = 0;

            //v_MRPprice = Convert.ToDecimal(txtMRPPrice.Text);
            //v_SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);

            objBL.userid = "Admin";

            //Page.Validate();

            //if (!Page.IsValid)
            //{

            //    return;
            //}

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
                //if (v_SellingPrice > v_MRPprice)
                //{
                //    lblSellingValidation.Text = "* Selling Price cannot be greater than MRP Price.";
                //    return;
                //}
                //decimal downPayment = Convert.ToDecimal(txtDownPaymentPerc.Text);
                //if (downPayment < 0 || downPayment > 100)
                //{
                //    lblDownpaymentvalidation.Text = "* Downpayment must be between 0% and 100%.";
                //    return;
                //}
                //else
                //{
                //    lblDownpaymentvalidation.Text = "";
                //}


                //decimal interest = Convert.ToDecimal(txtInterestPerc.Text);
                //if (interest < 0 || interest > 100)
                //{
                //    lblintrestvaliadtion.Text = "* Interest rate must be between 0% and 100%.";
                //    return;
                //}
                //else
                //{
                //    lblintrestvaliadtion.Text = "";
                //}


                //int tenure = Convert.ToInt32(txtTenure.Text);
                //if (tenure <= 0 || tenure > 120)
                //{
                //    lbltanurevaliadtion.Text = "* Tenure must be between 1 and 120 months.";
                //    return;
                //}
                //else
                //{
                //    lbltanurevaliadtion.Text = "";
                //}



                finalResult = opration("INS");
            }
            else
            {

              //  objBL.groupid = Convert.ToInt32(hfRID.Value);
                finalResult = opration("UPD");
            }

            if (finalResult == "200")
            {
                ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "popup",
                "ShowPopupAfterSave('Admin','Variant saved successfully!');",
                true
            );
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string finalResult = string.Empty;
           
            finalResult = opration("UPD");
            if (finalResult == "200")
            {
                string message = ViewState["ReturnMsg"] != null ? ViewState["ReturnMsg"].ToString() : "Operation completed.";

                // Encode it to avoid breaking JS if it has quotes
                message = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", "");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "popup",
                    $"ShowPopupAfterSave('Admin','{message}');",
                    true
                );

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);

                //ClearFields();
                //btnSave.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);

            }
        }

        //private string opration(string tasktype)
        //{
        //    objBL.task = tasktype;
        //    objBL.rid = string.IsNullOrEmpty(hfRID.Value) ? 0 : Convert.ToInt32(hfRID.Value);
        //    objBL.Brandname = ddlBrand.SelectedItem.Text;
        //    objBL.Brandmodelname = ddlModel.SelectedItem.Text;
        //    objBL.VariantName = txtVariantName.Text.Trim();
        //    objBL.SellingPrice = string.IsNullOrEmpty(txtSellingPrice.Text) ? 0 : Convert.ToDecimal(txtSellingPrice.Text.Trim());
        //    objBL.Mrpprice = string.IsNullOrEmpty(txtMRPPrice.Text) ? 0 : Convert.ToDecimal(txtMRPPrice.Text.Trim());
        //    objBL.avlbColors = txtColors.Text.Trim();
        //    objBL.DownPaymentPerc = txtDownPaymentPerc.Text.Trim();
        //    objBL.InterestPerc = txtInterestPerc.Text.Trim();
        //    objBL.Tenure = txtTenure.Text.Trim();
        //    objBL.ProcessingFees = txtProcessingFees.Text.Trim();
        //    objBL.remark = txtRemark.Text.Trim();
        //    objBL.activestatus = ddlActiveStatus.SelectedValue;
        //    objBL.userid = "Admin";
        //    objBL.groupid = 1;


        //    // ✅ Preserve old image if no new one uploaded
        //    if (ViewState["UploadedImagePath"] != null)
        //    {
        //        objBL.ImagePath = ViewState["UploadedImagePath"].ToString();
        //    }
        //    else
        //    {
        //        objBL.ImagePath = imgPreview.ImageUrl;   // use existing image
        //    }

        //    var result = objBL.CRUDDeviceVariantMaster();
        //    ViewState["ReturnMsg"] = result.ReturnMsg;
        //    return result.ReturnCode;
        //}


        public static int GRUOPID;

        private void GenerateReferenceId()
        {
            try
            {
                // Input series code (can be from a TextBox if needed)
                string inputSeriesCode = "Transactionid";

                // Output parameter
                int outputSeriesCode = 0;

                // Create instance of your business layer
                BLTransfertoAgent objTransfertoAgent = new BLTransfertoAgent();

                // Call RowCount method which generates unique number
                string result = objTransfertoAgent.RowCounts(inputSeriesCode, out outputSeriesCode);

                // Assign the generated integer to the static variable
                if (outputSeriesCode > 0)
                {
                    GRUOPID = outputSeriesCode;
                }
                else
                {
                    // Handle scenario when RowCount fails
                  //  spnMessage.InnerHtml = "Failed to generate group ID.";
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
               // spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
              //  spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        public int GetNextGroupID()
        {
            int nextGroupID = 1; // Default to 1 if table is empty

            try
            {
                // Get connection string from config
                string connStr = ConfigurationManager.AppSettings["ConnectionString"];

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetLastGroupID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        // Execute the stored procedure
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int lastGroupID))
                        {
                            nextGroupID = lastGroupID + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it or rethrow)
                throw new Exception("Error retrieving next groupID: " + ex.Message);
            }

            return nextGroupID;
        }

        private string opration(string tasktype)
        {
            objBL.task = tasktype;
           // objBL.rid = string.IsNullOrEmpty(hfRID.Value) ? 0 : Convert.ToInt32(hfRID.Value);
            objBL.Brandname = ddlBrand.SelectedItem.Text;
            objBL.Brandmodelname = ddlModel.SelectedItem.Text;
            objBL.activestatus = ddlActiveStatus.SelectedValue;
            objBL.remark = txtRemark.Text.Trim();
            objBL.userid = "Admin";
            if (tasktype == "INS")
            {
                objBL.groupid = GetNextGroupID();
            }
            if (tasktype == "UPD")
            {
                objBL.groupid = Convert.ToInt32(hdfgroupid.Value);
            }


            // Preserve old image if no new one uploaded
            objBL.ImagePath = ViewState["UploadedImagePath"] != null
                ? ViewState["UploadedImagePath"].ToString()
                : imgPreview.ImageUrl;

            string lastReturnCode = "";
            string lastReturnMsg = "";

            // Loop through all variants in the table
            for (int i = 1; i < tblVariants.Rows.Count; i++) // skip header
            {
                TableRow row = tblVariants.Rows[i];
                objBL.rid = Convert.ToInt32(((TextBox)row.Cells[0].Controls[0]).Text.Trim());
                objBL.VariantName = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
                objBL.avlbColors = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
                objBL.Mrpprice = string.IsNullOrEmpty(((TextBox)row.Cells[3].Controls[0]).Text)
                    ? 0 : Convert.ToDecimal(((TextBox)row.Cells[3].Controls[0]).Text.Trim());
                objBL.SellingPrice = string.IsNullOrEmpty(((TextBox)row.Cells[4].Controls[0]).Text)
                    ? 0 : Convert.ToDecimal(((TextBox)row.Cells[4].Controls[0]).Text.Trim());
                objBL.DownPaymentPerc = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
                objBL.InterestPerc = ((TextBox)row.Cells[6].Controls[0]).Text.Trim();
                objBL.Tenure = ((TextBox)row.Cells[7].Controls[0]).Text.Trim();
                objBL.ProcessingFees = ((TextBox)row.Cells[8].Controls[0]).Text.Trim();
                if (tasktype == "upd")
                {
                    objBL.groupid = Convert.ToInt32(HttpUtility.UrlDecode(Request.QueryString["groupid"].Trim()));
                }

                // Call existing CRUD for each variant
                var result = objBL.CRUDDeviceVariantMaster1();
                lastReturnCode = result.ReturnCode;
                lastReturnMsg = result.ReturnMsg;
            }

            // Save last return message in ViewState
            ViewState["ReturnMsg"] = lastReturnMsg;
            return lastReturnCode;
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Redirect to the ManageVariant.aspx page
            Response.Redirect("ManageVariant.aspx");
        }

        //private void ClearFields()
        //{
        //    hfRID.Value = string.Empty;
        //    //ddlBrand.Items.Insert(0, new ListItem("--Select Model--", ""));
        //    ddlBrand.SelectedIndex = 0;
        //    //ddlModel.Items.Insert(0, new ListItem("--Select Model--", ""));
        //    ddlModel.SelectedIndex = 0;
        //    txtTenure.Text = "";
        //    txtVariantName.Text = "";
        //    txtRemark.Text = "";
        //    txtColors.Text = "";
        //    txtSellingPrice.Text = "";
        //    txtDownPaymentPerc.Text = "";
        //    txtInterestPerc.Text = "";
        //    txtProcessingFees.Text = "";
        //    ddlActiveStatus.SelectedValue = "Y";
        //    ViewState["UploadedImagePath"] = null;
        //    imgPreview.ImageUrl = "~/Images/Variants/Default/defaultimg.png";
        //    txtMRPPrice.Text = "";
        //    btnSave.Visible = true;
        //    btnUpdate.Visible = false;
        //    lblSellingValidation.Text = "";
        //    lbltanurevaliadtion.Text = "";
        //    lblSellingValidation.Text = "";
        //    lblDownpaymentvalidation.Text = "";
        //    lblintrestvaliadtion.Text = "";
        //    //imgPreview.Visible = false;
        //    // btnSave.Text = "Save";
        //}
    }
}