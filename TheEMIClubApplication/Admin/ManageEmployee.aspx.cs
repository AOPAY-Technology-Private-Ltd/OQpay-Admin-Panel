using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.MasterPage
{
    public partial class ManageEmployee : System.Web.UI.Page
    {
        BLEmployeeMaster objEmp = new BLEmployeeMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GridBind();
            }
        }


        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int serialNo = e.Row.RowIndex + 1 + (gvEmployee.PageIndex * gvEmployee.PageSize);
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                lblSNo.Text = serialNo.ToString();
            }
        }





            protected void btnClear_Click(object sender, EventArgs e)
        {
            //Clear();
        }



        private void GridBind()
        {
            objEmp.Task = "GET";
            DataTable dt = objEmp.CreateEmployee();
            gvEmployee.DataSource = dt;
            gvEmployee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Clear existing filter values
            objEmp.Empcode = "";
            objEmp.EmpFirstName = "";
            objEmp.MobileNo = "";
            objEmp.Emailid = "";
            objEmp.EmpAadharno = "";
            objEmp.EmpPanNo = "";

            // Set search criteria
            string selectedValue = ddlSearch.SelectedValue.ToUpper().Trim();
            string searchText = txtvalues.Text.ToUpper().Trim();

            if (selectedValue == "ALL")
            {
                objEmp.Task = "GET";
            }
            else if (selectedValue == "EMPLOYEECODE")
            {
                objEmp.Empcode = searchText;
            }
            else if (selectedValue == "FULLNAME")
            {
                objEmp.EmpFirstName = searchText;
            }
            else if (selectedValue == "MOBILENO")
            {
                objEmp.MobileNo = searchText;
            }
            else if (selectedValue == "EMAILID")
            {
                objEmp.Emailid = searchText;
            }
            else if (selectedValue == "AADHARNO")
            {
                objEmp.EmpAadharno = searchText;
            }
            else if (selectedValue == "PANNO")
            {
                objEmp.EmpPanNo = searchText;
            }

            txtvalues.Text = string.Empty;

            // Get data
            objEmp.Task = "GET";
            DataTable dt = objEmp.CreateEmployee();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvEmployee.DataSource = dt;
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();

                // Toastr error message when no records found
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
        }

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

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex; // set new page
            GridBind(); // bind your data
        }

        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                string empid = e.CommandArgument.ToString();

                // Redirect to CreateVariantMaster.aspx with RID in querystring
                Response.Redirect("CreateEmployee.aspx?EMPID=" + empid, false);
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
        //        else if (e.CommandName == "ActiveStatusRow")
        //        {
        //            //objEmp.rid = rid;
        //            objEmp.ActiveStatus = ddlActiveStatus.SelectedValue ;
        //            DataTable dt = objEmp.CreateEmployee();

        //        }
        //    }
        //    else
        //    {

        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}
    }
}