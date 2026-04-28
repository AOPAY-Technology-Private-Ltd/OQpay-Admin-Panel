using AVFramework;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using Twilio;
using System.Threading;
using Microsoft.AspNet.SignalR.Hosting;

namespace TheEMIClubApplication.Admin
{
    public partial class ManageCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            if (!IsPostBack)
            {
                CustomerBindGrid();
            }

        }
        protected void btnCustSearch_Click(object sender, EventArgs e)
        {
            BLCustomerMaster objCustomerMst = new BLCustomerMaster();

            try
            {
                string group = AppSessions.SessionUserRoleCode;

                DataTable dtResult = new DataTable();

                if (group.Trim().ToUpper() == "SUPERADMIN")
                {
                    CustomerBindGrid();
                    return;
                }

                objCustomerMst.Mode = "GET";

                switch (ddlCriteria.SelectedItem.Text)
                {
                    case "All":
                        // No filter needed
                        break;
                    case "MobileNo":
                        objCustomerMst.PrimaryMobileNumber = txtValues.Text.Trim();
                        break;
                    case "Name":
                        objCustomerMst.FirstName = txtValues.Text.Trim();
                        break;
                    case "EmailId":
                        objCustomerMst.EMailID = txtValues.Text.Trim();
                        break;
                    case "AadharNo":
                        objCustomerMst.AadharNumber = txtValues.Text.Trim();
                        break;
                    case "PanNo":
                        objCustomerMst.PANNumber = txtValues.Text.Trim();
                        break;
                    case "AccountNo":
                        objCustomerMst.AccountNumber = txtValues.Text.Trim();
                        break;
                    case "IMEINo 1":
                        objCustomerMst.IMEINumber1 = txtValues.Text.Trim();
                        break;
                    case "IMEINo 2":
                        objCustomerMst.IMEINumber2 = txtValues.Text.Trim();
                        break;
                    case "Active":
                        objCustomerMst.ActiveStatus = "Active";
                        break;
                    case "Inactive":
                        objCustomerMst.ActiveStatus = "Inactive";
                        break;
                    default:
                        // Invalid criteria
                        gvDevices.DataSource = null;
                        gvDevices.DataBind();
                        lblCustomerNoData.Text = "No records found";
                        return;
                }

                dtResult = objCustomerMst.GetCustomerDeviceMst();

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    gvDevices.DataSource = dtResult;
                    gvDevices.DataBind();
                    lblCustomerNoData.Text = string.Empty; // Clear message when records found
                }
                else
                {
                    gvDevices.DataSource = null;
                    gvDevices.DataBind();
                    lblCustomerNoData.Text = "No records found";
                }

                txtValues.Text = "";
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                // Optionally show error message to user
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                // Optionally show error message to user
            }
        }

        protected void gvDevices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EditRow")))
                {
                    string tempName = Convert.ToString(e.CommandArgument);
                    string[] tempNameArr = tempName.Split(new char[] { '|' });
                    string loanid = tempNameArr[0];
                    if (e.CommandName.Equals("EditRow"))
                    {
                        string temp = loanid;

                        string urlSafeValue = HttpUtility.UrlEncode(loanid);
                        //string encryptedValue = CryptoUtil.EncryptTripleDES(loanid);
                        //Response.Redirect("../Admin/CustomerMaster.aspx?edocelor="+ CryptoUtil.EncryptTripleDES(urlSafeValue), false);
                        Response.Redirect("../Admin/CustomerMaster.aspx?edocelor=" + urlSafeValue);
                    }

                    LinkButton lnk = (LinkButton)e.CommandSource;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;

                    int rowIndex = row.RowIndex;

                    if (rowIndex >= 0 && rowIndex < gvDevices.DataKeys.Count)
                    {
                        // Safe: now you can access DataKeys
                        string middleName = gvDevices.DataKeys[rowIndex]["MiddleName"]?.ToString();
                        string lastName = gvDevices.DataKeys[rowIndex]["LastName"]?.ToString();
                        string altMobile = gvDevices.DataKeys[rowIndex]["AlternateMobileNumber"]?.ToString();
                        string altOTP = gvDevices.DataKeys[rowIndex]["AlternateMobileOTP"]?.ToString();
                        string aadhar = gvDevices.DataKeys[rowIndex]["AadharNumber"]?.ToString();
                        string pan = gvDevices.DataKeys[rowIndex]["PANNumber"]?.ToString();
                        string color = gvDevices.DataKeys[rowIndex]["color"]?.ToString();
                        string invoicePath = gvDevices.DataKeys[rowIndex]["Invoive_Path"]?.ToString();
                        string IMEIPhoto = gvDevices.DataKeys[rowIndex]["IMEINumber_PhotoPath"]?.ToString();
                        string AccountNo = gvDevices.DataKeys[rowIndex]["AccountNumber"]?.ToString();
                        string ifscNo = gvDevices.DataKeys[rowIndex]["BankIFSCCode"]?.ToString();
                        string BankName = gvDevices.DataKeys[rowIndex]["BankName"]?.ToString();
                        string Accounttype = gvDevices.DataKeys[rowIndex]["AccountType"]?.ToString();
                        string branchName = gvDevices.DataKeys[rowIndex]["BranchName"]?.ToString();
                        string refName = gvDevices.DataKeys[rowIndex]["RefName"]?.ToString();
                        string refRelation = gvDevices.DataKeys[rowIndex]["RefRelationShip"]?.ToString();
                        string refMobileno = gvDevices.DataKeys[rowIndex]["RefmobileNo"]?.ToString();
                        string refAddress = gvDevices.DataKeys[rowIndex]["RefAddress"]?.ToString();
                        string debitOrCedit = gvDevices.DataKeys[rowIndex]["DebitOrCreditCard"]?.ToString();
                        string Upi = gvDevices.DataKeys[rowIndex]["UPIMandate"]?.ToString();


                    }
                    else
                    {

                        System.Diagnostics.Debug.WriteLine("Invalid rowIndex: " + rowIndex);
                    }

                }
                //if (e.CommandName.Equals("DownloadRow"))
                //{
                //    int rid = Convert.ToInt32(e.CommandArgument);
                //    if (rid > 0)
                //    {
                //        // Pass RID to your CIBIL PDF generator method
                //        CibilReportinPdf(rid);
                //    }

                //}

                //if (e.CommandName.Equals("DownloadRow"))
                //{
                //    // Disable async response
                //    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //    if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                //    {
                //        scriptManager.RegisterPostBackControl((LinkButton)e.CommandSource);
                //    }

                //    int rid = Convert.ToInt32(e.CommandArgument);
                //    CibilReportinPdf(rid); 
                //}

                if (e.CommandName.Equals("DownloadRow"))
                {
                    // Disable async response
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    if (scriptManager != null && scriptManager.IsInAsyncPostBack)
                    {
                        scriptManager.RegisterPostBackControl((LinkButton)e.CommandSource); 
                    }

                    int rid = Convert.ToInt32(e.CommandArgument);
                    CibilReportinPdf(rid);
                }

            }

            catch (Exception ex)
            {
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }

        }

        protected void gvDevices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // gvDevices.PageIndex = e.NewPageIndex;
            CustomerBindGrid();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < gvDevices.PageCount)
            {
                gvDevices.PageIndex = e.NewPageIndex;
                CustomerBindGrid();
            }
        }
        protected void gvDevices_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = gvDevices.BottomPagerRow;
            if (pagerRow != null)
            {
                LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                if (lnkPrevious != null)
                    lnkPrevious.Visible = gvDevices.PageIndex > 0;

                if (lnkNext != null)
                    lnkNext.Visible = gvDevices.PageIndex < gvDevices.PageCount - 1;
            }
        }
        private void CustomerBindGrid()
        {
            BLCustomerMaster objCustomerMst = new BLCustomerMaster();
            objCustomerMst.Mode = "GET";

            DataTable dt = objCustomerMst.GetCustomerDeviceMst();

            if (dt != null && dt.Rows.Count > 0)
            {
                lblCustomerNoData.Text = string.Empty; // Clear any previous message
                gvDevices.DataSource = dt;
                gvDevices.DataBind();
            }
            else
            {
                gvDevices.DataSource = null;
                gvDevices.DataBind();
                lblCustomerNoData.Text = "No records found";
            }
        }
        private void CibilReportinPdf(int rid)
        {
            try
            {
                BLCustomerMaster objCustomerMst = new BLCustomerMaster();
                objCustomerMst.RID = rid;
                objCustomerMst.Mode = "GET";
                DataTable dt = objCustomerMst.GetCustomerDeviceMst();

                if (dt == null || dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('No customer data found.');", true);
                    return;
                }

                string cibilJson = dt.Rows[0]["CibilApiResponse"]?.ToString();
                if (string.IsNullOrEmpty(cibilJson))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL Report Not found.');", true);
                    return;
                }

                JObject jsonObj = JObject.Parse(cibilJson);
                var profile = jsonObj["result"]?["result_json"]?["INProfileResponse"];
                if (profile == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('CIBIL structure invalid.');", true);
                    return;
                }

                string fileName = "CIBIL_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                    PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();

                 

                    // Fonts
                    var headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
                    var sectionFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                    var keyFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                    var valueFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);

                    // Title
                    doc.Add(new Paragraph("CIBIL Detailed Report", headerFont));
                    doc.Add(new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), valueFont));
                    doc.Add(new Paragraph("\n"));

                    // Helper to build key-value tables
                    PdfPTable CreateTable(Dictionary<string, string> rows)
                    {
                        PdfPTable table = new PdfPTable(2);
                        table.WidthPercentage = 100;
                        table.SpacingBefore = 5f;
                        table.SpacingAfter = 10f;

                        foreach (var kv in rows)
                        {
                            PdfPCell cellKey = new PdfPCell(new Phrase(kv.Key, keyFont));
                            PdfPCell cellVal = new PdfPCell(new Phrase(kv.Value ?? "N/A", valueFont));
                            cellKey.BackgroundColor = new BaseColor(235, 235, 235);
                            cellKey.BorderWidth = 0.5f;
                            cellVal.BorderWidth = 0.5f;
                            table.AddCell(cellKey);
                            table.AddCell(cellVal);
                        }
                        return table;
                    }

                    // CREDIT PROFILE HEADER
                    var header = profile["CreditProfileHeader"];
                    if (header != null)
                    {
                        doc.Add(new Paragraph("CREDIT PROFILE HEADER", sectionFont));
                        var rows = new Dictionary<string, string>
                {
                    { "Report Number", (string)header["ReportNumber"] },
                    { "Report Date", (string)header["ReportDate"] },
                    { "Report Time", (string)header["ReportTime"] },
                    { "Subscriber Name", (string)header["Subscriber_Name"] },
                    { "Version", (string)header["Version"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // CAIS SUMMARY
                    var caisSummary = profile["CAIS_Summary"];
                    if (caisSummary != null)
                    {
                        doc.Add(new Paragraph("CAIS SUMMARY", sectionFont));

                        var creditAcc = caisSummary["Credit_Account"];
                        if (creditAcc != null)
                        {
                            var rows = new Dictionary<string, string>
                    {
                        { "Active Accounts", (string)creditAcc["CreditAccountActive"] },
                        { "Closed Accounts", (string)creditAcc["CreditAccountClosed"] },
                        { "Default Accounts", (string)creditAcc["CreditAccountDefault"] },
                        { "Total Accounts", (string)creditAcc["CreditAccountTotal"] }
                    };
                            doc.Add(CreateTable(rows));
                        }

                        var balance = caisSummary["Total_Outstanding_Balance"];
                        if (balance != null)
                        {
                            var rows = new Dictionary<string, string>
                    {
                        { "Outstanding (All)", (string)balance["Outstanding_Balance_All"] },
                        { "Secured", (string)balance["Outstanding_Balance_Secured"] },
                        { "Unsecured", (string)balance["Outstanding_Balance_UnSecured"] },
                        { "Unsecured %", (string)balance["Outstanding_Balance_UnSecured_Percentage"] }
                    };
                            doc.Add(CreateTable(rows));
                        }
                    }

                    // CAPS SUMMARY
                    var capsSummary = profile["CAPS"]?["CAPS_Summary"];
                    var nonCreditCaps = profile["NonCreditCAPS"]?["NonCreditCAPS_Summary"];
                    var totalCaps = profile["TotalCAPS_Summary"];
                    if (capsSummary != null || nonCreditCaps != null || totalCaps != null)
                    {
                        doc.Add(new Paragraph("CAPS SUMMARY", sectionFont));

                        if (capsSummary != null)
                        {
                            var rows = capsSummary.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                        if (nonCreditCaps != null)
                        {
                            doc.Add(new Paragraph("Non-Credit CAPS", keyFont));
                            var rows = nonCreditCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                        if (totalCaps != null)
                        {
                            doc.Add(new Paragraph("Total CAPS Summary", keyFont));
                            var rows = totalCaps.Children<JProperty>().ToDictionary(x => x.Name, x => (string)x.Value);
                            doc.Add(CreateTable(rows));
                        }
                    }

                    // CURRENT APPLICATION
                    var currentApp = profile["Current_Application"]?["Current_Application_Details"];
                    if (currentApp != null)
                    {
                        doc.Add(new Paragraph("CURRENT APPLICATION", sectionFont));

                        var rows = new Dictionary<string, string>
                {
                    { "Applicant Name", (string)currentApp["Current_Applicant_Details"]?["First_Name"] },
                    { "PAN", (string)currentApp["Current_Applicant_Details"]?["IncomeTaxPan"] },
                    { "Mobile", (string)currentApp["Current_Applicant_Details"]?["MobilePhoneNumber"] },
                    { "Amount Financed", (string)currentApp["Amount_Financed"] },
                    { "Income", (string)currentApp["Current_Other_Details"]?["Income"] },
                    { "Duration (Months)", (string)currentApp["Duration_Of_Agreement"] },
                    { "Enquiry Reason", (string)currentApp["Enquiry_Reason"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // SCORE
                    var score = profile["SCORE"];
                    if (score != null)
                    {
                        doc.Add(new Paragraph("CIBIL SCORE", sectionFont));
                        var rows = new Dictionary<string, string>
                {
                    { "Bureau Score", (string)score["BureauScore"] }
                };
                        doc.Add(CreateTable(rows));
                    }

                    // CAIS ACCOUNT DETAILS
                    var accountDetails = profile["CAIS_Account"]?["CAIS_Account_DETAILS"];
                    if (accountDetails != null)
                    {
                        int idx = 1;
                        foreach (var acc in accountDetails)
                        {
                            doc.Add(new Paragraph($"ACCOUNT #{idx++}", sectionFont));
                            var rows = new Dictionary<string, string>
                    {
                        { "Subscriber Name", (string)acc["Subscriber_Name"] },
                        { "Account Number", (string)acc["Account_Number"] },
                        { "Account Type", (string)acc["Account_Type"] },
                        { "Status", (string)acc["Account_Status"] },
                        { "Current Balance", (string)acc["Current_Balance"] },
                        { "Loan Amount", (string)acc["Highest_Credit_or_Original_Loan_Amount"] },
                        { "Open Date", (string)acc["Open_Date"] },
                        { "Date Closed", (string)acc["Date_Closed"] }
                    };
                            doc.Add(CreateTable(rows));
                        }
                    }

                    // ✅ Finalize and return file
                    doc.Close();
                    //byte[] bytes = memoryStream.ToArray();

                    //Response.Clear();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    //Response.BinaryWrite(bytes);
                    //Response.Flush();
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //byte[] bytes = ms.ToArray();

                    //Response.Clear();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    //Response.OutputStream.Write(bytes, 0, bytes.Length);
                    //Response.Flush();
                    //Response.End();

                    byte[] bytes = memoryStream.ToArray();

                    // Prepare Response for PDF download
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush(); // Ensure that the file is written to the response stream
                    Response.End();

                }
            }
 
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowError('Error generating PDF: " + ex.Message.Replace("'", "") + "');", true);
            }
        }


    }
}