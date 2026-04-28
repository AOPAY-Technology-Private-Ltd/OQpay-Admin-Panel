using AVFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;

namespace TheEMIClubApplication.Admin
{
    public partial class ApprovedBankdetails : System.Web.UI.Page
    {
        BLBankdetails Objbank = new BLBankdetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortalCommon.BindDropDownList(ddlRetailer, "GetRetailer", string.Empty,
                     string.Empty, string.Empty, string.Empty, string.Empty, "-- Select --");
                BindBankDetails();
                //  LoadRetailers();
            }
        }
        protected void gvBankDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index
            gvBankDetails.PageIndex = e.NewPageIndex;

            // Rebind the data to refresh the GridView
            BindBankDetails();
        }
        private void BindBankDetails()
        {
           
            try
            {
                Objbank.Action = "Get";

                // Check for blank or "0" and assign null if true
                Objbank.RegistrationID = string.IsNullOrEmpty(ddlRetailer.SelectedValue) || ddlRetailer.SelectedValue == "0"
                                            ? null
                                            : ddlRetailer.SelectedValue;

                Objbank.Mobile_No = string.IsNullOrEmpty(txtMobileNo.Text.Trim()) || txtMobileNo.Text.Trim() == "0"
                                            ? null
                                            : txtMobileNo.Text.Trim();

                Objbank.ApprovedStatus = string.IsNullOrEmpty(ddlApprovedStatus.SelectedValue) || ddlApprovedStatus.SelectedValue == "0"
                                            ? null
                                            : ddlApprovedStatus.SelectedValue;

                // 🔹 Get data from stored procedure
                DataTable dtbank = Objbank.GetBankdetails();

                if (dtbank != null && dtbank.Rows.Count > 0)
                {
                    pnlResults.Visible = true;
                    gvBankDetails.PageSize = PortalCommon.GetGridPageSize;
                    gvBankDetails.DataSource = dtbank;
                    gvBankDetails.DataBind();
                }
                else
                {
                    lblModelRecordCount.Text = "No records found";
                    pnlResults.Visible = false;
                    gvBankDetails.DataSource = null;
                    gvBankDetails.DataBind();



                    //string ErrorMessage = Common.GetMessageFromXMLFile("MSG1001").Replace("'", "\\'");

                    //string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showToastrError", script, true);
                    //// spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001"); // No records found
                    //// spnMessage.Attributes.Add("class", Constants.MessageCSS);
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
                // spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            finally
            {
                // ObjpendingPayout = null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindBankDetails();
        }


        protected void gvBankDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string RID = args[0];
                string currentStatus = args[1];      // "Active" or "Inactive"
                string registrationID = args[2];

                // Decide action based on current status
                if (currentStatus.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    Objbank.Action = "Deactivate";
                    
                }
                else
                {
                    Objbank.Action = "Activate";
           
                }

                Objbank.RID = Convert.ToInt32(RID);
                Objbank.UpdatedBy = AppSessions.SessionLoginId;
                Objbank.RegistrationID = registrationID;


                try
                {
                    bool result = Objbank.ApprovedBankDetails();

                    if (result)
                    {
                        BindBankDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('Status update failed. Please try again.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        $"alert('Error: {ex.Message}');", true);
                }
            }
        }


    }
}