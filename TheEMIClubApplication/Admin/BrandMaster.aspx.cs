using AVFramework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TheEMIClubApplication.MasterPage
{
    public partial class BrandMaster : System.Web.UI.Page
    {
        BLDeviceMaster objDeviceMaster = new BLDeviceMaster();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                if (AppSessions.SessionLoginId != null)
                {
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }  
        }

        protected void btnSave_Click(object sender, EventArgs e)
        
        {
            objDeviceMaster.userid = "Admin";
            var finalResult =string.Empty;


           if  (btnSave.Text.ToUpper() == "UPDATE".ToUpper())
            {
                objDeviceMaster.rid = Convert.ToInt32(hfRID.Value);
                objDeviceMaster.task = "UPD";
                objDeviceMaster.remark = txtRemark.Text.Trim();
                objDeviceMaster.Brandname = txtBrandName.Text.Trim();
                objDeviceMaster.activestatus = ddlActiveStatus.SelectedValue;
                var result = objDeviceMaster.CRUDDeviceBrandMaster();
                string ReturnCode = result.ReturnCode.ToString();
                string Returnmsg = result.ReturnMsg.ToString();

                if (ReturnCode == "200")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{Returnmsg}');", true);
                    LoadGrid();
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPage",
                        $"ShowError('{Returnmsg}');", true);
                }

            }
           else  
            {
                 //finalResult = opration(objDeviceMaster.task = "INS");
                objDeviceMaster.task = "INS";
                objDeviceMaster.remark = txtRemark.Text.Trim();
                objDeviceMaster.Brandname = txtBrandName.Text.Trim();
                objDeviceMaster.activestatus = ddlActiveStatus.SelectedValue;
                var result = objDeviceMaster.CRUDDeviceBrandMaster();
                string ReturnCode = result.ReturnCode.ToString();
                string Returnmsg = result.ReturnMsg.ToString();
                if (ReturnCode=="200")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{Returnmsg}');", true);
                    LoadGrid();
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPage",
    $"ShowError('{Returnmsg}');", true);
                }
            }

        }

        private string opration(string tasktype)
        {
            objDeviceMaster.userid = "Admin";
            //objDeviceMaster.remark = txtRemark.Text.Trim();
            //objDeviceMaster.Brandname = txtBrandName.Text.Trim();
            //objDeviceMaster.activestatus = ddlActiveStatus.SelectedValue;

            var result = objDeviceMaster.CRUDDeviceBrandMaster();

            ViewState["ReturnMsg"] = result.ReturnMsg; 

            return result.ReturnCode;
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtBrandName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            ddlActiveStatus.SelectedIndex = 0;
            btnSave.Text = "Save";
            btnSave.CssClass = "btn btn-primary  btn-sm px-4 mr-2";
            txtSearchBrand.Text = string.Empty;
            lblgridError.Text = string.Empty;
            lblgridError.CssClass= string.Empty;
        }



        protected void gvBrands_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
                return;

            if (e.CommandName == "EditRow")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                if (row.RowIndex >=0)
                {
                    int rid = Convert.ToInt32(gvBrands.DataKeys[row.RowIndex].Value);
                    hfRID.Value = rid.ToString();
                    string brandName = row.Cells[2].Text.Trim();
                    string remark = row.Cells[3].Text.Trim();
                    string activeStatus = row.Cells[4].Text.Trim();
                    txtBrandName.Text = brandName;
                    txtRemark.Text = remark;
                    ddlActiveStatus.SelectedValue = activeStatus;
                    btnSave.Text = "Update";
                    btnSave.CssClass = "btn btn-success btn-sm px-4 mr-2";
                }
                else
                {

                }
    
            }
            if (e.CommandName == "DeActiveRow")
            {


                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;

                if (row.RowIndex >= 0)
                {

                    int rid = Convert.ToInt32(e.CommandArgument);
                    string currentStatus = gvBrands.DataKeys[row.RowIndex]["ActiveStatus"].ToString();
                    //string currentStatus = statusObj != null ? statusObj.ToString() : "Inactive";


                    //int rid = Convert.ToInt32(gvBrands.DataKeys[row.RowIndex].Value);
                    //string currentStatus = row.Cells[3].Text.Trim(); 

                    string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

                    objDeviceMaster.rid = rid;
                    objDeviceMaster.task = "Active_YN";
                    objDeviceMaster.activestatus = newStatus;
                    //objDeviceMaster.userid = ""; 
                    //objDeviceMaster.Brandname = ""; 
                    //objDeviceMaster.remark = "";
                    var result = objDeviceMaster.CRUDDeviceBrandMaster();
                    ViewState["ReturnMsg"] = result.ReturnMsg;
                    ViewState["ReturnCode"] = result.ReturnCode;

                    if (result.ReturnCode=="200")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{ViewState["ReturnMsg"]}');", true);
                    }

                    LoadGrid();
                }
                else
                {

                }

            }

        }


        protected void gvBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvBrands.PageIndex = e.NewPageIndex;
            //LoadGrid();
            LoadGrid();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvBrands.PageCount)
            {
                gvBrands.PageIndex = e.NewPageIndex;
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            DataTable dtDeviceMaster = new DataTable();
            objDeviceMaster.task = "GET";
            dtDeviceMaster = objDeviceMaster.GetAllBrands();

            if (dtDeviceMaster.Rows.Count > 0)
            {
                lblgridError.Text = string.Empty;
                lblgridError.CssClass = string.Empty;

                gvBrands.DataSource = dtDeviceMaster;
                gvBrands.DataBind();
            }
            else
            {
                lblBrandRecordCount.Text = "No records found";
                gvBrands.DataBind();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtDeviceMaster = new DataTable();
            objDeviceMaster.task = "GET";
            objDeviceMaster.Brandname = txtSearchBrand.Text;
            dtDeviceMaster = objDeviceMaster.GetAllBrands();
            if (dtDeviceMaster.Rows.Count > 0)
            {
                lblgridError.Text = string.Empty;
                lblgridError.CssClass = string.Empty;
                gvBrands.DataSource = dtDeviceMaster;
                gvBrands.DataBind();
            }
            else
            {
                lblgridError.Text = "* No Record(s) Found !";
                lblgridError.CssClass = "text-danger";
                gvBrands.DataBind();
            }
            txtSearchBrand.Text = string.Empty;
        }

        protected void gvBrands_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gvBrands.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvBrands.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvBrands.PageIndex < gvBrands.PageCount - 1;
            }
        }
    }
}