using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;

using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System.Drawing;
using System.Web.UI;
using System.Configuration;
using System.Text;
using System.Web.Services;
//using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Twilio;


namespace TheEMIClubApplication.Reports
{
    public partial class Reports : System.Web.UI.Page
    {

        BLReports ReportDetails = new BLReports();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    PortalCommon.BindDropDownList(dtpmode, Constants.MstFlag_Report, AppSessions.SessionLoginId, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");
                   // PortalCommon.BindDropDownList(ddlcustomer, Constants.MstFlag_ReportUser, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "-Select-");
                }   
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
             //   spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }



        //private void GetCollectionDetails()
        //{

        //    DataTable dtManageDisbursement = new DataTable();

        //    try
        //    {

        //        objDisbursement.LoanID = Request.QueryString["edocelor"].Trim();
        //        objDisbursement.LoanID = CryptoUtil.DecryptTripleDES(objDisbursement.LoanID.Replace(" ", "+"));


        //        dtManageDisbursement = objDisbursement.GetEmiCollection();

        //        if (dtManageDisbursement.Rows.Count > 0)
        //        {
        //            spnMessage.InnerHtml = string.Empty;
        //            gvreport.PageSize = PortalCommon.GetGridPageSize;
        //            gvreport.DataSource = dtManageDisbursement;
        //            gvreport.DataBind();
        //            for (int i = 0; i < gvreport.Rows.Count; i++)
        //            {
        //                if (gvreport.Rows[i].Cells[6].Text == "Due")//showing Activate?
        //                {


        //                    gvreport.Rows[i].Cells[6].ForeColor = Color.Red;
        //                    gvreport.Rows[i].Cells[6].Font.Bold = true;


        //                }
        //                else
        //                {
        //                    gvreport.Rows[i].Cells[6].ForeColor = Color.Green;
        //                    gvreport.Rows[i].Cells[6].Font.Bold = true;
        //                }





        //            }

        //        }
        //        else
        //        {
        //            //MSG1001 - No Record(s) Found.
        //            spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
        //            spnMessage.Attributes.Add("class", Constants.MessageCSS);
        //            gvreport.DataSource = null;
        //            gvreport.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //  objDisbursement.Dispose();
        //        objDisbursement = null;
        //    }
        //}



        protected void dtpmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtpmode.Text.Trim() == "SignUp Report")
            {
                ddlMode.Enabled = false;

            }

         
            else if (dtpmode.Text.Trim() == "Loan Status Report")
            {

                ddlMode.Enabled = true;
              
                Approved.Enabled = true;
                Pending.Enabled = true;
                Rejected.Enabled = true;
                Assign.Enabled= true;
                Disbursements.Enabled = true;
                All.Enabled = true;
                Due.Enabled = false;
                Paid.Enabled = false;






            }
           
            else if (dtpmode.Text.Trim() == "EMI Collection Report")
            {
                ddlMode.Enabled = true;

                Approved.Enabled = true;
                Pending.Enabled = true;
                Rejected.Enabled = false;
                Assign.Enabled = false;
                Disbursements.Enabled = false;
                All.Enabled = true;
                Due.Enabled = false;
                Paid.Enabled = false;

            }


            else if (dtpmode.Text.Trim() == "Disbursement Report")
            {
                ddlMode.Enabled = false;

                Approved.Enabled = false;
                Pending.Enabled = false;
                Rejected.Enabled = false;
                Assign.Enabled = false;
                Disbursements.Enabled = false;
                Due.Enabled = false;
                Paid.Enabled = false;
                All.Enabled= false;

            }

            else if (dtpmode.Text.Trim() == "Defaulter List")
            {
                ddlMode.Enabled = false;

                Approved.Enabled = false;
                Pending.Enabled = false;
                Rejected.Enabled = false;
                Assign.Enabled = false;
                Disbursements.Enabled = false;
                Due.Enabled = false;
                Paid.Enabled = false;
                All.Enabled = false;

            }




        }
        private void SignUpReport()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = "";
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();

                
                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();
                
                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void LoanAplyStatusReportAll()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 =ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();
                  
                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void LoanAplyStatusReportPending()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }
        private void LoanAplyStatusReportdisburshment()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }
        private void LoanAplyStatusReportApproval()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void LoanAplyStatusReportAssign()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void LoanAplyStatusReportRejected()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void Disbursementapproved()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void EmiCollectionReportApproved()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void EmiCollectionReportPending()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void EmiCollectionReportall()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }

        private void EMIDefaulterList()
        {
            BLReports ReportDetails = new BLReports();

            try
            {
                if (txtfromdate.Text != "")
                {
                    ReportDetails.FromDate = Convert.ToDateTime(txtfromdate.Text);
                }
                else
                {
                    ReportDetails.FromDate.ToString();
                }
                if (txttodate.Text != "")
                {
                    ReportDetails.ToDate = Convert.ToDateTime(txttodate.Text);
                }
                else
                {
                    ReportDetails.ToDate.ToString();
                }

                DateTime fromDate = BLCommon.DATETIME_NULL_REPORT;
                DateTime toDate = BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.FromDate = DateTime.TryParse(txtfromdate.Text.Trim(), out fromDate) == true ? fromDate : BLCommon.DATETIME_NULL_REPORT;
                ReportDetails.ToDate = DateTime.TryParse(txttodate.Text.Trim(), out toDate) == true ? toDate : BLCommon.DATETIME_NULL_REPORTTO;
                ReportDetails.Flag = dtpmode.Text.Trim().ToString();
                ReportDetails.Flag1 = ddlMode.Text.Trim().ToString();
                ReportDetails.parmUserName = "";


                DataTable dtCompany = ReportDetails.ShowAllreportDatewise();


                if (dtCompany.Rows.Count > 0)
                {
                    gvreport.PageSize = Convert.ToInt16(ddlNoofRecords.Text);
                    gvreport.DataSource = dtCompany;
                    gvreport.DataBind();

                }

                else
                {
                    //MSG1001 - No record(s) found..
                    gvreport.DataSource = null;
                    gvreport.DataBind();

                    spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
                    spnMessage.Attributes.Add("class", Constants.MessageCSS);
                    return;
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
            finally
            {
                ReportDetails = null;
            }

        }
        protected void btnshow_click(object sender, EventArgs e)
        {
            if (dtpmode.Text.Trim() == "SignUp Report")
            {
                SignUpReport();
            }
            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "All"))
            {
                LoanAplyStatusReportAll();
            }
            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "Approved"))
            {
                LoanAplyStatusReportApproval();
            }
            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "Pending"))
            {
                LoanAplyStatusReportPending();
            }
            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "Assign"))
            {
                LoanAplyStatusReportAssign();
            }
            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "Disbursement"))
            {
                LoanAplyStatusReportdisburshment();
            }

            else if ((dtpmode.Text.Trim() == "Loan Status Report") && (ddlMode.Text.Trim() == "Rejected"))
            {
                LoanAplyStatusReportRejected();
            }

            else if (dtpmode.Text.Trim() == "Disbursement Report")
            {
              Disbursementapproved();
            }
            else if ((dtpmode.Text.Trim() == "EMI Collection Report") && (ddlMode.Text.Trim() == "Approved"))
            {
                EmiCollectionReportApproved();
            }

            else if ((dtpmode.Text.Trim() == "EMI Collection Report") && (ddlMode.Text.Trim() == "Pending"))
            {
                EmiCollectionReportPending();
            }

            else if ((dtpmode.Text.Trim() == "EMI Collection Report") && (ddlMode.Text.Trim() == "All"))
            {
                EmiCollectionReportall();
            }

            else if (dtpmode.Text.Trim() == "Defaulter List")
            {
                EMIDefaulterList();
            }

        }
    }

}
