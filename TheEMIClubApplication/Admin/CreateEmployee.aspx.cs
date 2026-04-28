using Microsoft.AspNet.SignalR.Messaging;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class CreateEmployee : System.Web.UI.Page
    {
        BLEmployeeMaster objEmp = new BLEmployeeMaster();
        bool profile = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["EMPID"] != null)
                {
                    string rid = Request.QueryString["EMPID"];
                    LoadVariantData(rid);
                }
                else
                {
                    profile = true;
                    LoadVariantData(AppSessions.SessionLoginId.ToUpper());
                }
               // GridBind();
            }
        }
        private void ProfileReadOnly()
        {
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtMobileNo.ReadOnly = true;
            txtEmailID.ReadOnly = true;
            txtAadhar.ReadOnly = true;
            txtPAN.ReadOnly = true;
            ddlActiveStatus.Enabled = false;
            btnUpdate.Visible = false;
        }

        protected void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                objEmp.EmpFirstName = txtFirstName.Text;
                objEmp.EmpLastName = txtLastName.Text;
                objEmp.MobileNo = txtMobileNo.Text;
                objEmp.EmpPanNo= txtPAN.Text;
                objEmp.EmpAadharno=txtAadhar.Text;
                objEmp.CreateEmployee();
            }
            catch (Exception ex)
            {

            }
        }
        //protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int serialNo = e.Row.RowIndex + 1 + (gvEmployee.PageIndex * gvEmployee.PageSize);
        //        Label lblSNo = (Label)e.Row.FindControl("lblSNo");
        //        lblSNo.Text = serialNo.ToString();
        //    }
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 🔹 Force validation for specific group (important)
            Page.Validate(); // 🔥 validate everything

            if (!Page.IsValid)
            {
                return;
            }

            else
            {
                try
                {
                    objEmp.Task = "INSERT";
                    objEmp.EmpFirstName = txtFirstName.Text;
                    objEmp.EmpLastName = txtLastName.Text;
                    objEmp.Emailid = txtEmailID.Text;
                    objEmp.MobileNo = txtMobileNo.Text;
                    string Emppassword = txtPassword.Text;
                    objEmp.EmpPassword = PortalCommon.EncryptTripleDES(Emppassword);
                    objEmp.EmpAddress = txtAddress.Text;
                    objEmp.EmpPanNo = txtPAN.Text;
                    objEmp.EmpAadharno = txtAadhar.Text;
                    objEmp.Username = AppSessions.SessionLoginId;
                    objEmp.ActiveStatus = ddlActiveStatus.SelectedValue;
                    var createResponse = objEmp.CreateEmployee();
                    if (createResponse.Rows[0]["ReturnCode"].ToString() == "200".Trim())
                    {
                        string message = createResponse.Rows[0]["ReturnMsg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{message}');", true);
                        // GridBind();
                        Clear();
                    }
                    else
                    {
                        string message = createResponse.Rows[0]["ReturnMsg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{message}');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{ex.Message}');", true);
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // 🔹 Force validation for specific group (important)
            Page.Validate(); // 🔥 validate everything

            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                try
                {
                    objEmp.Task = "UPDATE";
                    objEmp.Empcode = txtCustomerCode.Text;
                    objEmp.EmpFirstName = txtFirstName.Text;
                    objEmp.EmpLastName = txtLastName.Text;
                    objEmp.Emailid = txtEmailID.Text;
                    objEmp.MobileNo = txtMobileNo.Text;
                    //string Emppassword = txtPassword.Text;
                    //objEmp.EmpPassword = PortalCommon.EncryptTripleDES(Emppassword);
                    objEmp.EmpAddress = txtAddress.Text;
                    objEmp.EmpPanNo = txtPAN.Text;
                    objEmp.EmpAadharno = txtAadhar.Text;
                    objEmp.ActiveStatus = ddlActiveStatus.SelectedValue;
                    var createResponse = objEmp.CreateEmployee();
                    if (createResponse.Rows[0]["ReturnCode"].ToString() == "200".Trim())
                    {
                        string message = createResponse.Rows[0]["ReturnMsg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup( '{message}');", true);
                        //GridBind();                    
                        Clear();
                        if (profile)
                        {
                            Response.Redirect("../CommonPages/Home.aspx");
                        }
                    }
                    else
                    {
                        string message = createResponse.Rows[0]["ReturnMsg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{message}');", true);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('{ex.Message}');", true);
                }
            }
        }
            protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            if (AppSessions.SessionLoginId.ToUpper().Contains("EMP"))
            {
                Response.Redirect("../CommonPages/Home.aspx",false);
            }
            else
            {
                Response.Redirect("ManageEmployee.aspx", false);
            }
        

        }

        private void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPAN.Text = "";
            txtEmailID.Text = "";
            txtAadhar.Text = "";
            txtAddress.Text = "";
            txtCustomerCode.Text = "";
            txtAddress.Text = "";
            txtMobileNo.Text = "";
            txtPassword.Text = "";
           // btnSave.Visible = true;
           // btnUpdate.Visible = false;
            
        }

        //private void GridBind()
        //{
        //    objEmp.Task = "GET";
        //    DataTable dt = objEmp.CreateEmployee();
        //    gvEmployee.DataSource = dt;
        //    gvEmployee.DataBind();
        //}


        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (ddlSearch.SelectedValue.ToUpper().Trim() == "ALL".ToUpper().Trim())
        //    {
        //        objEmp.Task = "GET";
        //        gvEmployee.DataSource = objEmp.CreateEmployee();
        //        gvEmployee.DataBind();

        //    }
        //    if (ddlSearch.SelectedValue.ToUpper().Trim() == "EmployeeCode".ToUpper().Trim())
        //    {
        //        objEmp.Empcode = txtvalues.Text.ToUpper().Trim();

        //    }
        //    if (ddlSearch.SelectedValue.ToUpper().Trim() == "FullName".ToUpper().Trim())
        //    {
        //        objEmp.EmpFirstName = txtvalues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearch.SelectedValue.ToUpper() == "MobileNo".ToUpper().Trim())
        //    {
        //        objEmp.MobileNo = txtvalues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearch.SelectedValue.ToUpper() == "Emailid".ToUpper().Trim())
        //    {
        //        objEmp.Emailid = txtvalues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearch.SelectedValue.ToUpper() == "AadharNo".ToUpper().Trim())
        //    {
        //        objEmp.EmpAadharno = txtvalues.Text.ToUpper().Trim();
        //    }
        //    if (ddlSearch.SelectedValue.ToUpper() == "PanNo".ToUpper().Trim())
        //    {
        //        objEmp.EmpPanNo = txtvalues.Text.ToUpper().Trim();
        //    }
        //    txtvalues.Text = string.Empty;

        //    objEmp.Task = "GET";
        //    gvEmployee.DataSource = objEmp.CreateEmployee();
        //    gvEmployee.DataBind();
        //}

        //protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvEmployee.PageIndex = e.NewPageIndex; // set new page
        //    GridBind(); // bind your data
        //}

        private void LoadVariantData(string rid)
        {
            string descPass = string.Empty;
            objEmp.Task = "GET";
            if (profile == false)
            {
                objEmp.empid = Convert.ToInt32(rid);
            }
            else
            {
                objEmp.Empcode = AppSessions.SessionLoginId.ToUpper();
            }
            DataTable dt = objEmp.CreateEmployee();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Hidden RID
                txtCustomerCode.Text = dt.Rows[0]["EmployeeCode"].ToString();
                txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtAadhar.Text = dt.Rows[0]["AadharNumber"].ToString();
                txtPAN.Text = dt.Rows[0]["PANNumber"].ToString();
                //descPass = dt.Rows[0]["CustPassword"].ToString();
                descPass = PortalCommon.DecryptTripleDES(dt.Rows[0]["CustPassword"].ToString());
                //txtPassword.Text = descPass;
                txtPassword.Attributes["value"] = descPass;
                txtPassword.Text = descPass;
                txtPassword.ReadOnly = true;
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                if (profile)
                {
                    ProfileReadOnly();
                }

            }
        }
        //protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //        return;

        //    LinkButton lnk = (LinkButton)e.CommandSource;
        //    GridViewRow row = (GridViewRow)lnk.NamingContainer;
        //    if (row.RowIndex >= 0)
        //    {
        //        string descPass = string.Empty;
        //        int rid = Convert.ToInt32(gvEmployee.DataKeys[row.RowIndex].Value);
        //        objEmp.Task = "GET";
        //        objEmp.empid = rid;
               

        //        if (e.CommandName == "EditRow")
        //        {
        //            DataTable dt = objEmp.CreateEmployee();
        //            txtCustomerCode.Text = dt.Rows[0]["EmployeeCode"].ToString();
        //            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
        //            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
        //            txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
        //            txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
        //            txtAddress.Text= dt.Rows[0]["Address"].ToString();
        //            txtAadhar.Text = dt.Rows[0]["AadharNumber"].ToString();
        //            txtPAN.Text = dt.Rows[0]["PANNumber"].ToString();
        //            //descPass = dt.Rows[0]["CustPassword"].ToString();
        //            descPass = PortalCommon.DecryptTripleDES(dt.Rows[0]["CustPassword"].ToString());
        //            //txtPassword.Text = descPass;
        //            txtPassword.Attributes["value"] = descPass;
        //            txtPassword.ReadOnly = true;
        //            btnSave.Visible = false;
        //            btnUpdate.Visible = true;
        //        }
                
        //    }
        //    else
        //    {

        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}
    }
}