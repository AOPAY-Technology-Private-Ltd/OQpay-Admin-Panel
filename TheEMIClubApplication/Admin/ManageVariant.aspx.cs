using AVFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.Admin
{
    public partial class ManageVariant : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        BLDeviceMaster objBL = new BLDeviceMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (AppSessions.SessionLoginId != null)
                {
                    // BindBrands();
                    // BindEmptyModel();
                    BindGrid();
                    // btnCreateVariant.Visible = true;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }



        }
        protected void gvVariants_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                string groupid = e.CommandArgument.ToString();

                // Redirect to CreateVariantMaster.aspx with RID in querystring
                Response.Redirect("CreateVariantMaster.aspx?groupid=" + groupid, false);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Clear all previous filters
            objBL.Brandname = null;
            objBL.Brandmodelname = null;
            objBL.VariantName = null;
            objBL.avlbColors = null;
            objBL.Mrpprice = null;
            objBL.activestatus = null;

            string selectedValue = ddlSearchValues.SelectedValue.ToUpper().Trim();
            string inputValue = txtSearchValues.Text.ToUpper().Trim();

            if (selectedValue == "ALL")
            {
                // No filters needed, just fetch all
                objBL.task = "GET";
            }
            else
            {
                // Set appropriate filter based on selected value
                switch (selectedValue)
                {
                    case "BRAND":
                        objBL.Brandname = inputValue;
                        break;
                    case "MODEL":
                        objBL.Brandmodelname = inputValue;
                        break;
                    case "VARIANT":
                        objBL.VariantName = inputValue;
                        break;
                    case "COLOR":
                        objBL.avlbColors = inputValue;
                        break;
                    case "PRICE":
                        decimal price;
                        if (decimal.TryParse(inputValue, out price))
                        {
                            objBL.Mrpprice = price;
                        }
                        break;
                    case "STATUS":
                        objBL.activestatus = inputValue;
                        break;
                }

                objBL.task = "GET";
            }

            // Clear search box
            txtSearchValues.Text = string.Empty;

            // Fetch data
            DataTable dt = objBL.GetDeviceVariantMaster1();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvVariants.DataSource = dt;
                gvVariants.DataBind();
            }
            else
            {
                lblModelRecordCount.Text = "No records found";
                gvVariants.DataSource = null;
                gvVariants.DataBind();

                //// Show Toastr error if no records found
                //string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001");
                //string script = $"toastr.error('{ErrorMessage}', 'Error');";
                //ScriptManager.RegisterStartupScript(
                //    this,
                //    this.GetType(),
                //    "showToastrError",
                //    script,
                //    true
                //);
            }
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "all".ToUpper().Trim())
        //    {
        //        objBL.task = "GET";
        //        gvVariants.DataSource = objBL.GetDeviceVariantMaster();
        //        gvVariants.DataBind();

        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "BRAND".ToUpper().Trim())
        //    {
        //        objBL.Brandname = txtSearchValues.Text.ToUpper().Trim();

        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper().Trim() == "Model".ToUpper().Trim())
        //    {
        //        objBL.Brandmodelname = txtSearchValues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper() == "Variant".ToUpper().Trim())
        //    {
        //        objBL.VariantName = txtSearchValues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper() == "Color".ToUpper().Trim())
        //    {
        //        objBL.avlbColors = txtSearchValues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper() == "Price".ToUpper().Trim())
        //    {
        //        objBL.Mrpprice = Convert.ToDecimal(txtSearchValues.Text.ToUpper().Trim());
        //    }
        //    if (ddlSearchValues.SelectedValue.ToUpper() == "Status".ToUpper().Trim())
        //    {
        //        objBL.activestatus = txtSearchValues.Text.ToUpper().Trim();
        //    }
        //    txtSearchValues.Text = string.Empty;

        //    objBL.task = "GET";
        //    gvVariants.DataSource = objBL.GetDeviceVariantMaster();
        //    gvVariants.DataBind();

        //}
        //private void BindBrands()
        //{
        //    PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL,
        //        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-- Select Brand --");
        //}
        protected void BindGrid()
        {
            objBL.task = "GET";
            DataTable dt = objBL.GetDeviceVariantMaster1();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvVariants.DataSource = dt;
                gvVariants.DataBind();
            }
            else
            {
                gvVariants.DataSource = null;
                gvVariants.DataBind();

                // Get error message
                string errorMessage = Common.GetMessageFromXMLFile("MSG1001"); // e.g., "No records found"
                string safeMessage = HttpUtility.JavaScriptStringEncode(errorMessage); // Make JS-safe

                // Show Toastr error
                string script = $"toastr.error('{safeMessage}', 'Error');";
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "showToastrError",
                    script,
                    true
                );
            }
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
    }
}