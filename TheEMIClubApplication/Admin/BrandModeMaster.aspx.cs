using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.MasterPage
{
    public partial class BrandModeMaster : System.Web.UI.Page
    {
        BLDeviceMaster objDeviceMaster = new BLDeviceMaster();
        string finalResult = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSessions.SessionLoginId != null)
                {
                    LoadGrid();
                    BindBrandDropdown();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

                
            }



        }


        private void BindBrandDropdown()
        {
            PortalCommon.BindDropDownList(ddlBrand, Constants.Flage_BrandNameDDL, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //if ( btnSave.Text.ToUpper() == "save".ToUpper())
            //{

            //    if (ddlBrand.SelectedItem.Text == "-- Select --" || ddlBrand.SelectedItem.Text == "")
            //    {
            //        lblBrand.Text = "* Select Brand";
            //    }
            //    else
            //    {
            //        finalResult = opration(objDeviceMaster.task = "INS");
            //        if (finalResult != null)
            //        {
            //            if (finalResult == "200")
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            //                LoadGrid();
            //                Clear();

            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);

            //            }

            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            //        }
            //    }


            //}
            //else
            //{

            //    objDeviceMaster.rid = Convert.ToInt32(hfModelID.Value);
            //    finalResult = opration(objDeviceMaster.task = "UPD");
            //    if (finalResult != null)
            //    {
            //        if (finalResult == "200")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            //            LoadGrid();
            //            Clear();
            //            btnSave.Text = "Save";
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);

            //        }

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            //    }

            //}
            //BindBrandDropdown();
            finalResult = opration(objDeviceMaster.task = "INS");
            //if(ddlBrand.SelectedItem.Text.Trim().ToLower()=="-- Select --".Trim().ToLower())
            //{
            //    lblBrand.Text = "* Select Brand";
            //    return;
            //}
            if (finalResult != null)
            {
                if (finalResult == "200")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                    LoadGrid();
                    Clear();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{ViewState["ReturnMsg"]}');", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{ViewState["ReturnMsg"]}');", true);
            }


        }


        private string opration(string tasktype)
        {
            objDeviceMaster.Brandname = ddlBrand.SelectedItem.Text;
            objDeviceMaster.Brandmodelname = txtModelName.Text.Trim();
            objDeviceMaster.remark = txtRemark.Text.Trim();
            objDeviceMaster.activestatus = ddlActiveStatus.SelectedValue;
            objDeviceMaster.userid = "Admin";
            var result = objDeviceMaster.CRUDDeviceModelMaster();
            ViewState["ReturnMsg"] = result.ReturnMsg;
            return result.ReturnCode;
            //LoadGrid();
           // ShowMessage(result);
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            //ddlBrand.Items.Insert(0, new ListItem("-- Select --"));
            //ddlBrand.SelectedIndex = 0;
            //if (ddlBrand.Items.Count == 0)
            //{
            //    ddlBrand.Items.Add(new ListItem("-- Select --", "0"));
            //}
            //if (ddlBrand.Items.FindByText("-- Select --") == null)
            //{
            //    ddlBrand.Items.Insert(0, new ListItem("-- Select --", "0"));
            //}
            //ddlBrand.SelectedIndex = 0;

            if (ddlBrand.Items.Count == 0 || ddlBrand.Items.FindByText("-- Select --") == null)
            {
                BindBrandDropdown(); 
            }
            ddlBrand.SelectedIndex = 0;
            txtModelName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            ddlActiveStatus.SelectedIndex = 0;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            lblgridError.Text = string.Empty;
            lblgridError.CssClass = string.Empty;

        }
        private void LoadGrid()
        {
            DataTable dt = objDeviceMaster.GetAllDeviceModels();

            if (dt.Rows.Count > 0)
            {
                lblgridError.Text = string.Empty;
                lblgridError.CssClass = string.Empty;

                gvModels.DataSource = dt;
                gvModels.DataBind();

                // You can clear the message or leave blank if no need to show anything
                lblModelRecordCount.Text = string.Empty;
            }
            else
            {
                lblModelRecordCount.Text = "No records found";
                gvModels.DataSource = null;
                gvModels.DataBind();
            }
        }


        protected void gvModels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
                return;

            if (e.CommandName == "EditRow")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                if (row.RowIndex >= 0)
                {
                    
                    int rid = Convert.ToInt32(gvModels.DataKeys[row.RowIndex].Value);
                    //int brandId = Convert.ToInt32(gvModels.DataKeys[row.RowIndex].Values["BrandID"]);
                    hfModelID.Value = rid.ToString();
                    objDeviceMaster.rid = rid;
                    DataTable dt = objDeviceMaster.GetAllDeviceModels();

                    if (dt.Rows.Count > 0)
                    {
                        string brandName = dt.Rows[0]["BrandName"].ToString();
                        ddlBrand.ClearSelection();
                        // find and select the brand
                        ListItem item = ddlBrand.Items.FindByText(brandName);
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            ddlBrand.SelectedIndex = 0;
                        }

                        txtModelName.Text = dt.Rows[0]["ModelName"].ToString();
                        txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                        ddlActiveStatus.SelectedValue = dt.Rows[0]["ActiveStatus"].ToString();
                    }
                    else
                    {
                        if (ddlBrand.Items.Count > 0)
                            ddlBrand.SelectedIndex = 0;

                        txtModelName.Text = "";
                        txtRemark.Text = "";
                        ddlActiveStatus.SelectedValue = "Active";
                    }




                    //string rowId = row.Cells[0].Text.Trim();
                    //string brandName = row.Cells[2].Text.Trim();
                    //string modelName = row.Cells[3].Text.Trim();
                    //string remark = row.Cells[4].Text.Trim();
                    //string activeStatus = row.Cells[5].Text.Trim();
                    //ddlBrand.SelectedItem.Text = brandName;
                    //txtModelName.Text = modelName;
                    //txtRemark.Text = remark;
                    //ddlActiveStatus.SelectedValue = activeStatus;
                    //ddlBrand.ClearSelection();


                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            if (e.CommandName == "ActiveStatusRow")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                if (row.RowIndex >= 0)
                {

                    int rid = Convert.ToInt32(e.CommandArgument);
                    string currentStatus = gvModels.DataKeys[row.RowIndex]["ActiveStatus"].ToString();
                    string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

                    //hfModelID.Value = rid.ToString();
                    string brandName = row.Cells[2].Text.Trim();
                    string modelName = row.Cells[3].Text.Trim();
                    string remark = row.Cells[4].Text.Trim();
                    string activeStatus = newStatus;
                    objDeviceMaster.rid = rid;
                    objDeviceMaster.activestatus = activeStatus;
                    objDeviceMaster.task = "Active_YN";
                    var result = objDeviceMaster.CRUDDeviceModelMaster();
                    ViewState["ReturnMsg"] = result.ReturnCode;
                    ViewState["ReturnMsg"] = result.ReturnMsg;

                    if (result.ReturnCode == "200")
                    {
                        ShowMessage(result.ReturnMsg);
                    }
                    else
                    {
                        ShowMessage(result.ReturnMsg);
                    }
                    LoadGrid();
                }
            }
        }

        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);
        }
        protected void gvModels_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            //gvModels.PageIndex = e.NewPageIndex;
            //LoadGrid();

            LoadGrid();

            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvModels.PageCount)
            {
                gvModels.PageIndex = e.NewPageIndex;
                LoadGrid();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        { 
            
            objDeviceMaster.rid = Convert.ToInt32(hfModelID.Value);
            //objDeviceMaster.Brandname = ddlBrand.SelectedValue;
            //objDeviceMaster.Brandmodelname = txtModelName.Text.Trim();
            //objDeviceMaster.remark = txtRemark.Text.Trim();
            //objDeviceMaster.activestatus = ddlActiveStatus.SelectedValue; 
            finalResult = opration(objDeviceMaster.task = "UPD");
            if (finalResult != null)
            {
                if (finalResult == "200")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                    LoadGrid();
                    Clear();
               
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(ddlSearch.SelectedValue== "BrandName")
            {
                objDeviceMaster.Brandname = txtvalues.Text;
            }
            else if(ddlSearch.SelectedValue == "ModelName")
            {
                objDeviceMaster.Brandmodelname = txtvalues.Text;
            }
            else if (ddlSearch.SelectedValue == "Remark")
            {
                objDeviceMaster.remark= txtvalues.Text;
            }
            else if (ddlSearch.SelectedValue == "Active")
            {
                objDeviceMaster.activestatus = "Active";
            }
            else if (ddlSearch.SelectedValue == "Inactive")
            {
                objDeviceMaster.activestatus = "Inactive";
            }
            else 
            {
                objDeviceMaster.rid = 0;
            }
            LoadGrid();
            txtvalues.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
        }

        protected void gvModels_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gvModels.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvModels.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvModels.PageIndex < gvModels.PageCount - 1;
            }
        }
    }
}