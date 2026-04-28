using AVFramework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AjaxControlToolkit;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.Admin
{
    public partial class LoanFollowUp : System.Web.UI.Page
    {
        BLEMIDetails objEMI = new BLEMIDetails();

        BLloanfollup Manageloanfollow = new BLloanfollup();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve and decrypt loanid from the query string
                string encryptedLoanID = Request.QueryString["edocelor"];
                string emiAmt = Request.QueryString["emiAmt"];
                string paidEMI = Request.QueryString["paidEMI"];
                if (!string.IsNullOrEmpty(encryptedLoanID))
                {
                    string decryptedLoanID = CryptoUtil.DecryptTripleDES(encryptedLoanID);
                    BindGrid(decryptedLoanID);
                    txtLoanEMIAmount.Text = emiAmt;
                    txtLoanTenureNo.Text = paidEMI;
                    LeadFollowUpDetails();


                }
            }
        }

        private void BindGrid(string loanid)
        {
            objEMI.Mode = "GET";
            objEMI.Loancode = loanid;

            DataTable dt = objEMI.GetEMIDetails();

            if (dt.Rows.Count > 0)
            {
                txtLoanID.Text = dt.Rows[0]["LoanCode"].ToString();
                txtCustomerName.Text = dt.Rows[0]["FirstName"].ToString() + " " +
                                       dt.Rows[0]["MiddleName"].ToString() + " " +
                                       dt.Rows[0]["LastName"].ToString();

                txtEmailAddress.Text = dt.Rows[0]["EMailID"].ToString();
                txtContactNo.Text = dt.Rows[0]["PrimaryMobileNumber"].ToString();
                txtProductName.Text = (dt.Rows[0]["BrandName"]?.ToString() ?? "") + " " +
                                       (dt.Rows[0]["ModelName"]?.ToString() ?? "") + " " +
                                       (dt.Rows[0]["ModelVariant"]?.ToString() ?? "") + " " +
                                       (dt.Rows[0]["color"]?.ToString() ?? "");
          //      txtLoanEMIAmount.Text =
          //Convert.ToDecimal(dt.Rows[0]["LoanEMIAmount"]).ToString("0.00");

                hdfcustomecode.Value = dt.Rows[0]["CustomerCode"].ToString();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                // Validation: If Lead Status is 'Follow up', then Next Follow Up Date Time is mandatory
                if (ddlFollowUpStatus.SelectedValue != null &&
     ddlFollowUpStatus.SelectedValue != "Paid" &&
     ddlFollowUpStatus.SelectedValue != "Escalated" &&
     string.IsNullOrWhiteSpace(txtNextFollowUpDate.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(
    this.GetType(),
    "Popup",
    "ShowError('Next Follow-Up Date and Time is required when loan Status is Follow up or other than Paid/Escalated');",
    true
);
                    return;
                }




                Manageloanfollow.LoanCode = txtLoanID.Text;

                // Parse both date and time from txtNextFollowUpDateTime
                Manageloanfollow.NextFollowUpdate = string.IsNullOrWhiteSpace(txtNextFollowUpDate.Text.Trim())
                    ? (DateTime?)null
                    : DateTime.ParseExact(txtNextFollowUpDate.Text.Trim(), "dd MMM yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                Manageloanfollow.FollowUpStatus = ddlFollowUpStatus.SelectedValue.Trim();
                Manageloanfollow.FollowUpBy = AppSessions.SessionLoginId;
                Manageloanfollow.FollowUpRemarks = txtFollowUpRemarks.Text.Trim();

                Manageloanfollow.CustomerCode = hdfcustomecode.Value.Trim();
                Manageloanfollow.LoanTenureNo = Convert.ToInt32(txtLoanTenureNo.Text.Trim());
                Manageloanfollow.LoanEmiAmount = Convert.ToDecimal(txtLoanEMIAmount.Text.Trim());
                Manageloanfollow.ProductCode=txtProductName.Text.Trim();

                short retVal = Manageloanfollow.LoanFollowUp();


                if (retVal == 1)
                {
                    string encMessage = HttpUtility.UrlEncode(
     CryptoUtil.EncryptTripleDES("Follow-up added successfully")
 );

                    string url = "~/CommonPages/Home.aspx?msg=" + encMessage;
                    Response.Redirect(url, false);
                    // //string url = Constants.Path_CRMHomePage + "?edom=" + CryptoUtil.EncryptTripleDES(Constants.PageRedirectMode_Update) + "&eman=" + CryptoUtil.EncryptTripleDES(txtLeadID.Text); // I
                    // //Response.Redirect(url);



                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Follow-up added successfully');", true);
                    //// string decryptedLoanID = CryptoUtil.DecryptTripleDES(encryptedLoanID);
                    // BindGrid(txtLoanID.Text.Trim());
                    //// Common.ClearFields(this.Page, "0");

                }

                if (retVal == 0)
                {


                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "Follow-up Not Updated. Try Again After Some time" + "');", true);

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

        protected void gvFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //string encryptedLoanID = Request.QueryString["edocelor"];
            //string decryptedLoanID = CryptoUtil.DecryptTripleDES(encryptedLoanID);
        
            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvFollowUp.PageCount)
            {
                gvFollowUp.PageIndex = e.NewPageIndex;
                LeadFollowUpDetails();
            }
        }
        protected void ddlFollowUpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string status = ddlFollowUpStatus.SelectedValue.Trim();

                // Enable Next Follow-Up Date only if status is NOT "Paid" or "Escalated"
                if (status != "Paid" && status != "Escalated")
                {
                    txtNextFollowUpDate.Enabled = true;
                }
                else
                {
                    txtNextFollowUpDate.Enabled = false;
                    txtNextFollowUpDate.Text = string.Empty; // clear value if disabled
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

        private void LeadFollowUpDetails()
        {
            BLloanfollup objManageUser = new BLloanfollup();
            DataTable dtManageUser = new DataTable();
            string encryptedLoanID = Request.QueryString["edocelor"];
            if (!string.IsNullOrEmpty(encryptedLoanID))
            {
                encryptedLoanID = encryptedLoanID.Replace(" ", "+");
                objManageUser.LoanCode = CryptoUtil.DecryptTripleDES(encryptedLoanID);

            }

            try
            {

                objManageUser.LoanCode = objManageUser.LoanCode;



                dtManageUser = objManageUser.GetloanFollowUpDetails();

                if (dtManageUser.Rows.Count > 0)
                {
                    spnMessage.InnerHtml = string.Empty;
                    gvFollowUp.PageSize = PortalCommon.GetGridPageSize;
                    gvFollowUp.DataSource = dtManageUser;
                    gvFollowUp.DataBind();

                }
                else
                {
                    string errorMessage = Common.GetMessageFromXMLFile("MSG1001");

                    // Display Toastr error message dynamically
                    string script = $"toastr.error('{errorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showToastrError", script, true);
                    gvFollowUp.DataSource = null;
                    gvFollowUp.DataBind();
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
    }
}
