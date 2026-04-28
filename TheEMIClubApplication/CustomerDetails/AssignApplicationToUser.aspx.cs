using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.ExportInfrastructure;
using System.Drawing;



namespace TheEMIClubApplication.CustomerDetails
{
    public partial class AssignApplicationToUser : System.Web.UI.Page
    {
        BLPersonalDetails assignapplication = new BLPersonalDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PortalCommon.BindDropDownList(ddlemployee, Constants.MstFlag_bankUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");

                    GetRecentLoanAppforAssign();

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
          
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");


        }
        private void GetRecentLoanAppforAssign()
        {
            BLManageUser objManageUser = new BLManageUser();
            DataTable dtManageUser = new DataTable();

            try
            {
                


                dtManageUser = objManageUser.GetRecentLoanDetailsforassign();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvAssignApplication.PageSize = PortalCommon.GetGridPageSize;
                    gvAssignApplication.DataSource = dtManageUser;
                    gvAssignApplication.DataBind();


                    // FindChkBox_gvAssignApplication();
                    for (int i = 0; i < gvAssignApplication.Rows.Count; i++)
                    {
                        if (gvAssignApplication.Rows[i].Cells[5].Text.Equals(Constants.IsActive))//showing Activate?
                        {
                            ((LinkButton)gvAssignApplication.Rows[i].Cells[6].FindControl("lnkManageApplication")).Enabled = false;
                            ((LinkButton)gvAssignApplication.Rows[i].Cells[7].FindControl("lnkManageRole")).Enabled = false;
                           
                        }

                       

                    }
                }
                else
                {
                    //MSG1001 - No Record(s) Found.
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    gvAssignApplication.DataSource = null;
                    gvAssignApplication.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtManageUser.Dispose();
                dtManageUser = null;
            }
        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            try
            {
              
                if (ddlemployee.SelectedItem.Text == "-Select-")
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "VAL1105");
                }
                else
                {
                    for (int i = 0; i < gvAssignApplication.Rows.Count; i++)
                    {
                        if (((CheckBox)gvAssignApplication.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)

                        {
                            assignapplication.LoanID = gvAssignApplication.Rows[i].Cells[3].Text;
                            assignapplication.Assigncode = ddlemployee.SelectedValue;
                            assignapplication.Usercode = gvAssignApplication.Rows[i].Cells[2].Text;
                            assignapplication.LoanStatus = "Assign";
                           
                                int retVal = assignapplication.AssignApplicationToUser();

                            if (retVal > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "Loan Application Assigned Successfully to: " + ddlemployee.SelectedItem.Text.Trim() + "');", true);
                            }

                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "Please Try After some Time" + "');", true);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }      
       

    }
}