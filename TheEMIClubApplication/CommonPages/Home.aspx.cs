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
//using System.Windows.Forms;
using System.Drawing;
using Org.BouncyCastle.Asn1.Crmf;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Services;
//using static System.Windows.Forms.LinkLabel;



namespace TheEMIClubApplication.CommonPages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   
                    string group = AppSessions.SessionUserRoleCode;
                    string Admingroup = AppSessions.SessionLoginId;

                    if (AppSessions.SessionLoginId != null)
                    {
                        if (group.Trim().ToUpper() == "SUPERADMIN")
                        {
                          //  GetRecentLoanApplicationforadmin();
                        }
                        else
                        {
                            if (Admingroup.Trim().ToUpper().Contains("ADMIN"))
                            {
                                Div_Dealerdashboard.Visible = true;
                                Div_Employeedashboard.Visible = false;
                            //    GetRecentLoanApplicationforuser();
                              //  DashboardBidData();
                              //  CustomerBindGrid();
                                EMIDetailsBindGrid();
                                CurrentEMIBindGrid();
                                //  DashboardBidData1();
                                LoadLoanSummary();
                                LoadLoanSummary1();
                                divemidetails.Style["display"] = "block";
                                //btnvisibalCustomer.Visible = true;
                               // btnbvisibalDealer.Visible = true;
                            }
                            else if(Admingroup.StartsWith("EMP"))
                            {
                               
                                Div_Dealerdashboard.Visible = false;
                                Div_Employeedashboard.Visible = true;
                                string employeeCode = AppSessions.SessionLoginId;
                                BindEmployeeMetrics(employeeCode);
                                BindEmployeeCharts(employeeCode);
                                div1.Visible = true;

                                BindgridAssign();
                                if (!string.IsNullOrEmpty(Request.QueryString["msg"]))
                                {
                                    string encMessage = Request.QueryString["msg"]; // already decoded
                                    string message = CryptoUtil.DecryptTripleDES(encMessage);

                                    ScriptManager.RegisterStartupScript(
                                        this,
                                        this.GetType(),
                                        "Popup",
                                        "ShowPopup('', '" + message.Replace("'", "\\'") + "');",
                                        true
                                    );
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Login.aspx");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
                    }

                }



            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                // spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }

        #region GetApplicationDetailForEdit
        private void BindEmployeeMetrics(string employeeCode)
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_GetEmployeeDashboardMetrics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                // Total Collected EMI
                cmd.Parameters.AddWithValue("@TaskType", "TotalCollectedEMI");
                lblEmpTotalCollectedEMI.InnerText = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N2");

                // Total Pending EMI
                cmd.Parameters["@TaskType"].Value = "TotalPendingEMI";
                lblEmpTotalPendingEMI.InnerText = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N2");

                // Total Overdue EMI
                cmd.Parameters["@TaskType"].Value = "OverdueEMITotal";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblEmpTotalOverdueEMI.InnerText = Convert.ToDecimal(reader["TotalOverdueAmount"]).ToString("N2");
                    }
                }

                // Total Loans Assigned
                cmd.Parameters["@TaskType"].Value = "TotalLoansAssigned";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblEmpTotalLoansAssigned.InnerText = Convert.ToInt32(reader["TotalAssignedLoans"]).ToString();
                    }
                }
            }
        }

        private void BindEmployeeCharts(string employeeCode)
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_GetEmployeeDashboardMetrics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                // Month-wise Collected EMI
                cmd.Parameters.AddWithValue("@TaskType", "MonthWiseCollectedEMI");
                DataTable dtCollected = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtCollected);
                }

                // Month-wise Pending EMI
                cmd.Parameters["@TaskType"].Value = "MonthWisePendingEMI";
                DataTable dtPending = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtPending);
                }

                // Convert DataTables to JSON for Chart.js
                string collectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dtCollected);
                string pendingJson = Newtonsoft.Json.JsonConvert.SerializeObject(dtPending);

                ClientScript.RegisterStartupScript(this.GetType(), "chartEmpData", $@"
            var collectedData = {collectedJson};
            var pendingData = {pendingJson};

            var ctxCollected = document.getElementById('chartEmpCollectedEMI').getContext('2d');
            new Chart(ctxCollected, {{
                type: 'bar',
                data: {{
                    labels: collectedData.map(x => x.Month + '/' + x.Year),
                    datasets: [{{
                        label: 'Collected EMI',
                        data: collectedData.map(x => x.CollectedEMI),
                        backgroundColor: 'rgba(40, 167, 69, 0.7)'
                    }}]
                }},
                options: {{ responsive: true }}
            }});

            var ctxPending = document.getElementById('chartEmpPendingEMI').getContext('2d');
            new Chart(ctxPending, {{
                type: 'bar',
                data: {{
                    labels: pendingData.map(x => x.Month + '/' + x.Year),
                    datasets: [{{
                        label: 'Pending EMI',
                        data: pendingData.map(x => x.PendingEMI),
                        backgroundColor: 'rgba(255, 193, 7, 0.7)'
                    }}]
                }},
                options: {{ responsive: true }}
            }});
        ", true);
            }
        }


        #endregion GetApplicationDetailForEdit




        private void Applicationcalculation()
        {
            try
            {

                //spnMessage.InnerText = string.Empty;
                //Calculation Debit Balance
                BLManageUser Application = new BLManageUser();

                Hashtable htFeatureDetail = Application.CountTotal();


                //lblapplication.Text = htFeatureDetail["Application"].ToString();
                //lblRejected.Text = htFeatureDetail["Rejected"].ToString();
                //   lblDisbursement.Text = htFeatureDetail["Disbursement"].ToString();


            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void todayPaidCollection()
        {
            try
            {

                //spnMessage.InnerText = string.Empty;
                //Calculation Debit Balance
                BLPersonalDetails objManageUsers = new BLPersonalDetails();

                objManageUsers.Flag = "Paid";
                Hashtable htFeatureDetail = objManageUsers.Collectioncalu();


                //lblpaidamt.Text =Convert.ToString(htFeatureDetail["PaidAmount"]);
                //if (lblpaidamt.Text == "")
                //{
                //    lblpaidamt.Text = "0.00";
                //}

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void todayDues()
        {
            try
            {

                //spnMessage.InnerText = string.Empty;
                //Calculation Debit Balance
                BLPersonalDetails objManageUsers = new BLPersonalDetails();

                objManageUsers.Flag = "Due";
                Hashtable htFeatureDetail = objManageUsers.Collectioncalu();


                //lbldues.Text = htFeatureDetail["DueAmt"].ToString();
                //if (lbldues.Text == "")
                //{
                //    lbldues.Text = "0.00";
                //}


            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      


 



        protected void btnEMISearch_Click(object sender, EventArgs e)
        {
            EMIDetailsBindGrid();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            CurrentEMIBindGrid();

            string script = $@"
    var el = document.getElementById('{currentemi.ClientID}');
    if (el) {{
        el.scrollIntoView({{ behavior: 'smooth', block: 'end' }});
    }}";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ScrollToGrid", script, true);
        }



        private void EMIDetailsBindGrid()
        {
            BLEMIDetails objEMI = new BLEMIDetails();
            DataTable EMIdt = new DataTable();

            try
            {
                objEMI.Mode = "GET";

                if (ddlEmiCriteria.SelectedItem.Text == "All")
                {
                    EMIdt = objEMI.GetEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Emiid")
                {
                    //objEMI. = Convert.ToInt32(txtEmivalue.Text.Trim());
                    EMIdt = objEMI.GetEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "loanid")
                {
                    objEMI.Loancode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "userid")
                {
                    objEMI.Customercode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Active")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Active";
                    EMIdt = objEMI.GetEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Inactive")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Inactive";
                    EMIdt = objEMI.GetEMIDetails();
                }



                grdEmaiDetails.DataSource = EMIdt;

                if (EMIdt.Rows.Count > 0)
                {
                    grdEmaiDetails.DataBind();
                }
                else
                {
                    //lblEMINoData.Text = "No Data Found !!";
                    grdEmaiDetails.DataBind();
                }




                // Calculate pagination count
                int totalRecords = EMIdt.Rows.Count;
                int startRecord = (grdEmaiDetails.PageIndex * grdEmaiDetails.PageSize) + 1;
                int endRecord = startRecord + grdEmaiDetails.PageSize - 1;

                if (endRecord > totalRecords)
                    endRecord = totalRecords;

                if (totalRecords > 0)
                    lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
                else
                    lblEMINoData.Text = "No records found";

                //if (EMIdt.Rows.Count > 0)
                //{
                //    int grandTotal = 0;

                //    for (int i = 0; i < EMIdt.Rows.Count; i++)
                //    {
                //        object objPaidAmount = EMIdt.Rows[i]["PaidAmount"];
                //        object objPaidEMI = EMIdt.Rows[i]["PaidEMI"];

                //        int paidAmount = (objPaidAmount == DBNull.Value) ? 0 : Convert.ToInt32(objPaidAmount);
                //        int paidEMI = (objPaidEMI == DBNull.Value) ? 0 : Convert.ToInt32(objPaidEMI);

                //        int total = paidAmount * paidEMI;
                //        grandTotal += total;
                //    }

                //    lblPendingEmi.Text = grandTotal.ToString();
                //    lblTotalEmicollection.Text = grandTotal.ToString();
                //}
                //else
                //{
                //    lblPendingEmi.Text = "0";
                //    lblTotalEmicollection.Text = "0";
                //}
            }
            catch (Exception ex)
            {
                // lblTotalEmicollection.Text = "Error";
            }

        }
        private void CurrentEMIBindGrid()
        {
            BLEMIDetails objEMI = new BLEMIDetails();
            DataTable EMIdt = new DataTable();

            try
            {
         
                objEMI.casetype = ddlcasetype.SelectedValue;


                if (DropDownList2.SelectedItem.Text == "All")
                {
                    EMIdt = objEMI.GetCurrentEMIDetailscurrentoverdues();
                }
               
                else if (DropDownList2.SelectedValue == "loanid")
                {
                    objEMI.Loancode = TextBox2.Text.Trim();
                    EMIdt = objEMI.GetCurrentEMIDetailscurrentoverdues();
                }
                else if (DropDownList2.SelectedValue == "userid")
                {
                    objEMI.Customercode = TextBox2.Text.Trim();
                    EMIdt = objEMI.GetCurrentEMIDetailscurrentoverdues();
                }
                //else if (DropDownList2.SelectedValue == "Active")
                //{
                //    txtEmivalue.Text = "";
                //    objEMI.RecordStatus = "Active";
                //    EMIdt = objEMI.GetCurrentEMIDetails();
                //}
                //else if (DropDownList2.SelectedValue == "Inactive")
                //{
                //    txtEmivalue.Text = "";
                //    objEMI.RecordStatus = "Inactive";
                //    EMIdt = objEMI.GetCurrentEMIDetails();
                //}



                GridView_CurrentEMI.DataSource = EMIdt;

                if (EMIdt.Rows.Count > 0)
                {
                    GridView_CurrentEMI.DataBind();
                }
                else
                {
                    //lblEMINoData.Text = "No Data Found !!";
                    GridView_CurrentEMI.DataBind();
                }




                // Calculate pagination count
                int totalRecords = EMIdt.Rows.Count;
                int startRecord = (grdEmaiDetails.PageIndex * grdEmaiDetails.PageSize) + 1;
                int endRecord = startRecord + grdEmaiDetails.PageSize - 1;

                if (endRecord > totalRecords)
                    endRecord = totalRecords;

                if (totalRecords > 0)
                    lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
                else
                    lblEMINoData.Text = "No records found";

                //if (EMIdt.Rows.Count > 0)
                //{
                //    int grandTotal = 0;

                //    for (int i = 0; i < EMIdt.Rows.Count; i++)
                //    {
                //        object objPaidAmount = EMIdt.Rows[i]["PaidAmount"];
                //        object objPaidEMI = EMIdt.Rows[i]["PaidEMI"];

                //        int paidAmount = (objPaidAmount == DBNull.Value) ? 0 : Convert.ToInt32(objPaidAmount);
                //        int paidEMI = (objPaidEMI == DBNull.Value) ? 0 : Convert.ToInt32(objPaidEMI);

                //        int total = paidAmount * paidEMI;
                //        grandTotal += total;
                //    }

                //    lblPendingEmi.Text = grandTotal.ToString();
                //    lblTotalEmicollection.Text = grandTotal.ToString();
                //}
                //else
                //{
                //    lblPendingEmi.Text = "0";
                //    lblTotalEmicollection.Text = "0";
                //}
            }
            catch (Exception ex)
            {
                // lblTotalEmicollection.Text = "Error";
            }

        }
        //private void DashboardBidData()
        //{

        //    BLEMIDetails objdb = new BLEMIDetails();
        //    DataTable dt = new DataTable();

        //    objdb.Mode = "DealerCount";
        //    dt = objdb.GetDashboardData();

        //    //lbldealercount.Text = dt.Rows[0][0].ToString();
        //    if (dt.Rows.Count > 0)
        //    {
        //        object value = dt.Rows[0][0];

        //        if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
        //        {
        //            lbldealercount.Text = "0.00";
        //        }
        //        else
        //        {
        //            lbldealercount.Text = Convert.ToDecimal(value).ToString();
        //            //lblPendingEmi.Text = Convert.ToDecimal(value).ToString("N2");
        //        }
        //    }
        //    else
        //    {
        //        lbldealercount.Text = "0.00";
        //    }

        //    objdb.Mode = "CustomerCount";
        //    dt = objdb.GetDashboardData();
        //    //lblcustomercount.Text= dt.Rows[0][0].ToString();
        //    if (dt.Rows.Count > 0)
        //    {
        //        object value = dt.Rows[0][0];

        //        if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
        //        {
        //            lblcustomercount.Text = "0";
        //        }
        //        else
        //        {
        //            lblcustomercount.Text = Convert.ToInt32(value).ToString();
        //            //lblPendingEmi.Text = Convert.ToDecimal(value).ToString("N2");
        //        }
        //    }
        //    else
        //    {
        //        lblcustomercount.Text = "0.00";
        //    }

        //    objdb.Mode = "PendingEMI";
        //    dt = objdb.GetDashboardData();
        //    //if (dt.Rows.Count>0)
        //    //{
        //    //    if (dt.Rows[0]== null)
        //    //    {
        //    //        lblPendingEmi.Text = "0.00".ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        lblPendingEmi.Text = dt.Rows[0][0].ToString();
        //    //    }
        //    //}
        //    if (dt.Rows.Count > 0)
        //    {
        //        object value = dt.Rows[0][0];

        //        if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
        //        {
        //            lblPendingEmi.Text = "0.00";
        //        }
        //        else
        //        {
        //            lblPendingEmi.Text = Convert.ToDecimal(value).ToString("N2");
        //        }
        //    }
        //    else
        //    {
        //        lblPendingEmi.Text = "0.00";
        //    }

        //    objdb.Mode = "TotalEMI";
        //    dt = objdb.GetDashboardData();

        //    if (dt.Rows.Count > 0)
        //    {
        //        object value = dt.Rows[0][0];

        //        if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
        //        {
        //            lblTotalEmicollection.Text = "0.00";
        //        }
        //        else
        //        {
        //            lblTotalEmicollection.Text = Convert.ToDecimal(value).ToString("N2");
        //            //lblPendingEmi.Text = Convert.ToDecimal(value).ToString("N2");
        //        }
        //    }
        //    else
        //    {
        //        lblTotalEmicollection.Text = "0.00";
        //    }

        //}

        protected void grdEmaiDetails_DataBound(object sender, EventArgs e)
        {
            GridViewRow pagerRow = grdEmaiDetails.BottomPagerRow;
            if (pagerRow != null)
            {
                pagerRow.Cells[0].Attributes["class"] = "text-center";

                for (int i = 0; i < pagerRow.Cells[0].Controls.Count; i++)
                {
                    if (pagerRow.Cells[0].Controls[i] is LinkButton lbtn)
                    {
                        lbtn.CssClass = "btn btn-outline-primary btn-sm mx-1";
                    }
                    else if (pagerRow.Cells[0].Controls[i] is Label lbl)
                    {
                        lbl.CssClass = "btn btn-primary btn-sm mx-1 disabled";
                    }
                }
            }
        }

        protected void rptPager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                grdEmaiDetails.PageIndex = Convert.ToInt32(e.CommandArgument);
                EMIDetailsBindGrid(); // <-- your method to bind data to GridView
            }
        }


        protected void grdEmaiDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EditRow")))
                {



                    string tempName = Convert.ToString(e.CommandArgument);
                    //string[] tempNameArr = tempName.Split(new char[] { '|' });
                    //string loanid = tempNameArr[0];
                    string encryptedRID = CryptoUtil.EncryptTripleDES(tempName);
                    string safeEncrypted = HttpUtility.UrlEncode(encryptedRID);
                    if (e.CommandName.Equals("EditRow"))
                    {
                        //string temp = loanid;
                        Response.Redirect("../Admin/EMIDetails.aspx?edocelor=" + safeEncrypted, false);
                    }
                }
                if ((e.CommandName.Equals("AssignRow")))
                {



                    string tempName = Convert.ToString(e.CommandArgument);
                    //string[] tempNameArr = tempName.Split(new char[] { '|' });
                    //string loanid = tempNameArr[0];
                    string encryptedRID = CryptoUtil.EncryptTripleDES(tempName);
                    string safeEncrypted = HttpUtility.UrlEncode(encryptedRID);
                    if (e.CommandName.Equals("AssignRow"))
                    {
                        //string temp = loanid;
                        Response.Redirect("../Admin/AssignLoanApplication.aspx?edocelor=" + safeEncrypted, false);
                    }
                }
            }

            catch (Exception ex)
            {
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

        protected void grdEmaiDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "LoanStatus")?.ToString();

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");

                if (status != null && status.ToLower() == "approve")
                {
                    lnkEdit.Visible = false;
                }
                else
                {
                    lnkEdit.Visible = true;
                }
            }

     
        }
        protected void grdEmaiDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                // Clear existing pager cells
                e.Row.Cells.Clear();

                // Create a new cell with colspan=2
                TableCell pagerCell = new TableCell();
                pagerCell.ColumnSpan = 2;
                pagerCell.HorizontalAlign = HorizontalAlign.Center;

                // Add the pager controls (GridView builds them in e.Row.Cells[0].Controls)
                GridViewRow pagerRow = e.Row;
                foreach (Control ctrl in pagerRow.Controls)
                {
                    pagerCell.Controls.Add(ctrl);
                }

                e.Row.Cells.Add(pagerCell);
            }
        }



        protected void grdEmaiDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EMIDetailsBindGrid();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < grdEmaiDetails.PageCount)
            {
                grdEmaiDetails.PageIndex = e.NewPageIndex;
                EMIDetailsBindGrid();
            }
            //grdEmaiDetails.PageIndex = e.NewPageIndex;
            //EMIDetailsBindGrid();
        }

        //protected void btnvisibalCustomer_Click(object sender, EventArgs e)
        //{
        //    btnvisibalCustomer.CssClass = "toggle-btn active";
        //    btnbvisibalDealer.CssClass = "toggle-btn";
        
        //    Response.Redirect("~/Admin/ManageCustomer.aspx");
        //}

        //protected void btnbvisibalDealer_Click(object sender, EventArgs e)
        //{
        //    btnvisibalCustomer.CssClass = "toggle-btn";
        //    btnbvisibalDealer.CssClass = "toggle-btn active";
        //    Response.Redirect("~/Admin/ManageRetailer.aspx");

          

        //}

        //private void SetButtonHighlight()
        //{
        //    string selected = ViewState["SelectedUser"] as string;

        //    if (selected == "Customer")
        //    {
        //        btnvisibalCustomer.CssClass = "btn btn-success me-2"; // Highlighted
        //        btnbvisibalDealer.CssClass = "btn btn-outline-secondary me-2"; // Not selected
        //    }
        //    else if (selected == "Dealer")
        //    {
        //        btnvisibalCustomer.CssClass = "btn btn-outline-secondary me-2";
        //        btnbvisibalDealer.CssClass = "btn btn-success me-2";
        //    }
        //}



        //protected void grdEmaiDetails_DataBound(object sender, EventArgs e)
        //{
        //    GridViewRow pagerRow = grdEmaiDetails.BottomPagerRow;
        //    if (pagerRow != null)
        //    {
        //        LinkButton lnkPrevious = (LinkButton)pagerRow.FindControl("lnkPrevious");
        //        LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

        //        if (lnkPrevious != null)
        //            lnkPrevious.Visible = grdEmaiDetails.PageIndex > 0;

        //        if (lnkNext != null)
        //            lnkNext.Visible = grdEmaiDetails.PageIndex < grdEmaiDetails.PageCount - 1;
        //    }
        //}


        private void BindgridAssign()
        {
            BLEMIDetails objEMI = new BLEMIDetails();
            DataTable EMIdt = new DataTable();

            try
            {
                objEMI.Mode = "GET";
                objEMI.Customercode = AppSessions.SessionLoginId;

                if (ddlEmiCriteria.SelectedItem.Text == "All")
                {
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Emiid")
                {
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "loanid")
                {
                    objEMI.Loancode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "userid")
                {
                    objEMI.Customercode = txtEmivalue.Text.Trim();
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Active")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Active";
                    EMIdt = objEMI.GetAssignEMIDetails();
                }
                else if (ddlEmiCriteria.SelectedValue == "Inactive")
                {
                    txtEmivalue.Text = "";
                    objEMI.RecordStatus = "Inactive";
                    EMIdt = objEMI.GetAssignEMIDetails();
                }

                // Filter rows where NextDueDate is either before or within the current month
                if (EMIdt.Rows.Count > 0)
                {
                    DateTime currentDate = DateTime.Now;

                    // Create a new DataTable to store filtered results
                    DataTable filteredData = EMIdt.Clone(); // Clone structure of original DataTable

                    foreach (DataRow row in EMIdt.Rows)
                    {
                        if (DateTime.TryParse(row["NextDueDate"].ToString(), out DateTime nextDueDate))
                        {
                            // Check if the NextDueDate is before or within the current month
                            if (nextDueDate <= currentDate)
                            {
                                filteredData.ImportRow(row);
                            }
                        }
                    }

                    grdAssignView.DataSource = filteredData;

                    if (filteredData.Rows.Count > 0)
                    {
                        grdAssignView.DataBind();
                    }
                    else
                    {
                        
                        lblEMINoData.Text = "No records found for the selected filter.";
                    }

                    // Calculate pagination count
                    int totalRecords = filteredData.Rows.Count;
                    int startRecord = (grdAssignView.PageIndex * grdAssignView.PageSize) + 1;
                    int endRecord = startRecord + grdAssignView.PageSize - 1;

                    if (endRecord > totalRecords)
                        endRecord = totalRecords;

                    if (totalRecords > 0)
                        lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
                    else
                        lblEMINoData.Text = "No records found";
                }

            }
            catch (Exception ex)
            {
                lblEMINoData.Text = "Error: " + ex.Message;
            }
        }


        //private void BindgridAssign()
        //{
        //    BLEMIDetails objEMI = new BLEMIDetails();
        //    DataTable EMIdt = new DataTable();

        //    try
        //    {
        //        objEMI.Mode = "GET";
        //        objEMI.Customercode = AppSessions.SessionLoginId;

        //        if (ddlEmiCriteria.SelectedItem.Text == "All")
        //        {
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }
        //        else if (ddlEmiCriteria.SelectedValue == "Emiid")
        //        {
        //            //objEMI. = Convert.ToInt32(txtEmivalue.Text.Trim());
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }
        //        else if (ddlEmiCriteria.SelectedValue == "loanid")
        //        {
        //            objEMI.Loancode = txtEmivalue.Text.Trim();
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }
        //        else if (ddlEmiCriteria.SelectedValue == "userid")
        //        {
        //            objEMI.Customercode = txtEmivalue.Text.Trim();
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }
        //        else if (ddlEmiCriteria.SelectedValue == "Active")
        //        {
        //            txtEmivalue.Text = "";
        //            objEMI.RecordStatus = "Active";
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }
        //        else if (ddlEmiCriteria.SelectedValue == "Inactive")
        //        {
        //            txtEmivalue.Text = "";
        //            objEMI.RecordStatus = "Inactive";
        //            EMIdt = objEMI.GetAssignEMIDetails();
        //        }



        //        grdAssignView.DataSource = EMIdt;

        //        if (EMIdt.Rows.Count > 0)
        //        {
        //            grdAssignView.DataBind();
        //        }
        //        else
        //        {
        //            //lblEMINoData.Text = "No Data Found !!";
        //            //grdAssignView.DataBind();
        //        }




        //        // Calculate pagination count
        //        int totalRecords = EMIdt.Rows.Count;
        //        int startRecord = (grdAssignView.PageIndex * grdAssignView.PageSize) + 1;
        //        int endRecord = startRecord + grdAssignView.PageSize - 1;

        //        if (endRecord > totalRecords)
        //            endRecord = totalRecords;

        //        if (totalRecords > 0)
        //            lblEMINoData.Text = $"Showing {startRecord}–{endRecord} of {totalRecords} records";
        //        else
        //            lblEMINoData.Text = "No records found";

        //    }
        //    catch (Exception ex)
        //    {
        //        // lblTotalEmicollection.Text = "Error";
        //    }

        //}


        private void LoadLoanSummary()
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskType", "LoanSummary");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        decimal totalLoanReleased = dr["TotalLoanReleased"] != DBNull.Value ? Convert.ToDecimal(dr["TotalLoanReleased"]) : 0;
                        decimal totalCollectedEMI = dr["TotalCollectedEMI"] != DBNull.Value ? Convert.ToDecimal(dr["TotalCollectedEMI"]) : 0;
                        decimal totalPendingEMI = dr["TotalPendingEMI"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPendingEMI"]) : 0;

                        // Set data attributes instead of final text
                        lblTotalLoanReleased.Attributes["data-value"] = totalLoanReleased.ToString();
                        lblTotalCollectedEMI.Attributes["data-value"] = totalCollectedEMI.ToString();
                        lblTotalPendingEMI.Attributes["data-value"] = totalPendingEMI.ToString();
                    }
                    dr.Close();
                }
            }
        }


        private void LoadLoanSummary1()
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskType", "OverdueEMItotal");

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        decimal totalOverdueAmount = dr["TotalOverdueAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalOverdueAmount"]) : 0;
                      //  decimal totalOverdueEMICount = dr["TotalOverdueEMICount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalOverdueEMICount"]) : 0;

                        lblOverdueAmount.InnerText = totalOverdueAmount.ToString("N2");
                      //  lblOverdueCount.InnerText = totalOverdueEMICount.ToString("N2");
       
                    }
                    dr.Close();
                }
            }
        }
        [WebMethod]
        public static object GetMetrics()
        {
            string connStr = ConfigurationManager.AppSettings["ConnectionString"];

            // Default response object
            object result = new
            {
                TotaltodayCollectedEMI = 0m,
                TotaltotalPendingEMI = 0m,
                MonthWiseCollectedEMI = new List<object>(),
                MonthWisePendingEMI = new List<object>(),
                CustomerLoanSummary = new List<object>(),
                TopRetailers = new List<object>(),
                OverdueEMI = new List<object>(),
                //TopRetailers = new List<object>(),
                TotalRetailer = 0m,
                Totalcustomer = 0m,

                TotalActiveLoans = 0m,
                TotalActiveLoanAmount = 0m,
                TotalClosedLoans = 0,
                TotalForeclosureLoans = 0,
                TotalSettlementLoans = 0,

                TotalProcessfee = 0m,
                Totalinterrestamt = 0m,
                Totalmembershipfee = 0m,
                TotalLatefine = 0m
            };

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // =======================
                // Scalar Helper
                // =======================
                Func<string, decimal> GetScalar = (task) =>
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskType", task);
                        object val = cmd.ExecuteScalar();
                        return val != DBNull.Value && val != null ? Convert.ToDecimal(val) : 0m;
                    }
                };

                // =======================
                // Table Helper
                // =======================
                Func<string, List<object>> GetTable = (task) =>
                {
                    List<object> list = new List<object>();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskType", task);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Dictionary<string, object> obj = new Dictionary<string, object>();
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    obj[dr.GetName(i)] = dr[i];
                                }
                                list.Add(obj);
                            }
                        }
                    }
                    return list;
                };

                // =======================
                // Read Active Loan Metrics
                // =======================
                decimal totalActiveLoans = 0m;
                decimal totalActiveLoanAmount = 0m;
                int totalClosedLoans = 0;
                int totalForeclosureLoans = 0;
                int totalSettlementLoans = 0;

                var activeLoanData = GetTable("TotalActiveLoans");

                if (activeLoanData.Count > 0)
                {
                    var row = activeLoanData[0] as Dictionary<string, object>;

                    if (row != null)
                    {
                        totalActiveLoans = row["TotalActiveLoans"] != DBNull.Value
                            ? Convert.ToDecimal(row["TotalActiveLoans"]) : 0;

                        totalActiveLoanAmount = row["TotalActiveLoanAmount"] != DBNull.Value
                            ? Convert.ToDecimal(row["TotalActiveLoanAmount"]) : 0;

                        totalClosedLoans = row["TotalClosedLoans"] != DBNull.Value
                            ? Convert.ToInt32(row["TotalClosedLoans"]) : 0;

                        totalForeclosureLoans = row["TotalForeclosureLoans"] != DBNull.Value
                            ? Convert.ToInt32(row["TotalForeclosureLoans"]) : 0;

                        totalSettlementLoans = row["TotalSettlementLoans"] != DBNull.Value
                            ? Convert.ToInt32(row["TotalSettlementLoans"]) : 0;
                    }
                }

                // =======================
                // Final Dashboard Result
                // =======================
                result = new
                {
                    TotaltodayCollectedEMI = GetScalar("TodayCollectedEMI"),
                    TotaltotalPendingEMI = GetScalar("TodayPendingEMI"),

                    MonthWiseCollectedEMI = GetTable("MonthWiseCollectedEMI"),
                    MonthWisePendingEMI = GetTable("MonthWisePendingEMI"),
                    CustomerLoanSummary = GetTable("CustomerLoanSummary"),
                    TopRetailers = GetTable("TopRetailers"),
                    OverdueEMI = GetTable("OverdueEMI"),
                   // TopRetailers = GetTable("TopRetailers"),

                    TotalRetailer = GetScalar("TotalRetailers"),
                    Totalcustomer = GetScalar("TotalCustomers"),

                    TotalActiveLoans = totalActiveLoans,
                    TotalActiveLoanAmount = totalActiveLoanAmount,
                    TotalClosedLoans = totalClosedLoans,
                    TotalForeclosureLoans = totalForeclosureLoans,
                    TotalSettlementLoans = totalSettlementLoans,

                    TotalProcessfee = GetScalar("TotalProcesscharge"),
                    Totalinterrestamt = GetScalar("TotalInterest"),
                    Totalmembershipfee = GetScalar("Totalmembership"),
                    TotalLatefine = GetScalar("TotalLatefee")
                };
            }

            return result;
        }


        //[WebMethod]
        //public static object GetMetrics()
        //{
        //    string connStr = ConfigurationManager.AppSettings["ConnectionString"];

        //    // Initialize with decimal literals (0m) to avoid int/decimal mismatch
        //    var result = new
        //    {
        //        TotaltodayCollectedEMI = 0m,
        //        TotaltotalPendingEMI = 0m,
        //        MonthWiseCollectedEMI = new List<object>(),
        //        MonthWisePendingEMI = new List<object>(),
        //        CustomerLoanSummary = new List<object>(),
        //        OverdueEMI = new List<object>(),
        //        TopRetailers = new List<object>(),
        //        TotalRetailer = 0m,
        //        Totalcustomer = 0m,

        //        TotalActiveLoans = 0m,
        //        TotalActiveLoanAmount = 0m






        //    };

        //    using (SqlConnection con = new SqlConnection(connStr))
        //    {
        //        con.Open();

        //        // Helper function to fetch single scalar values as decimal
        //        Func<string, decimal> GetScalar = (task) =>
        //        {
        //            using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@TaskType", task);
        //                object val = cmd.ExecuteScalar();
        //                return val != DBNull.Value ? Convert.ToDecimal(val) : 0m;
        //            }
        //        };

        //        // Helper function to fetch
        //
        //
        //        results
        //        Func<string, List<object>> GetTable = (task) =>
        //        {
        //            var list = new List<object>();
        //            using (SqlCommand cmd = new SqlCommand("usp_GetAdminDashboardMetrics", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@TaskType", task);
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        var obj = new Dictionary<string, object>();
        //                        for (int i = 0; i < dr.FieldCount; i++)
        //                            obj[dr.GetName(i)] = dr[i];
        //                        list.Add(obj);
        //                    }
        //                }
        //            }
        //            return list;
        //        };
        //        var activeLoanData = GetTable("TotalActiveLoans");
        //        decimal totalActiveLoans = 0m;
        //        decimal totalActiveLoanAmount = 0m;

        //        if (activeLoanData.Count > 0)
        //        {
        //            var row = activeLoanData[0] as Dictionary<string, object>;
        //            totalActiveLoans = Convert.ToDecimal(row["TotalActiveLoans"]);
        //            totalActiveLoanAmount = Convert.ToDecimal(row["TotalActiveLoanAmount"]);
        //        }
        //        // Assign all metrics
        //        result = new
        //        {
        //            TotaltodayCollectedEMI = GetScalar("TodayCollectedEMI"),
        //            TotaltotalPendingEMI = GetScalar("TodayPendingEMI"),
        //            MonthWiseCollectedEMI = GetTable("MonthWiseCollectedEMI"),
        //            MonthWisePendingEMI = GetTable("MonthWisePendingEMI"),
        //            CustomerLoanSummary = GetTable("CustomerLoanSummary"),
        //            OverdueEMI = GetTable("OverdueEMI"),
        //            TopRetailers = GetTable("TopRetailers"),
        //            TotalRetailer = GetScalar("TotalRetailers"),
        //            Totalcustomer = GetScalar("TotalCustomers"),




        //            //lblTotalloan = GetScalar("Totalactiveloans"),
        //            //lblTotalamount = GetScalar("Totalactiveloanamount"),

        //        };
        //    }

        //    return result;
        //}
        protected void grdAssignView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EditRow")) || (e.CommandName.Equals("ASG")) || (e.CommandName.Equals("COLL")) || (e.CommandName.Equals("Followup")))
                {
                    string tempName = Convert.ToString(e.CommandArgument);
                    string[] tempNameArr = tempName.Split(new char[] { '|' });
                    string loanid = tempNameArr[0];

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    string emiAmt = ((HiddenField)row.FindControl("hfEMIAmt")).Value;  // Using HiddenField for EMI Amt
                    int paidEMI = Convert.ToInt32(((HiddenField)row.FindControl("hfPaidEMI")).Value);  // Using HiddenField for Paid EMI
                    int updatedPaidEMI = paidEMI + 1;

                    if (e.CommandName.Equals("EditRow"))
                    {
                        string temp = loanid;
                        Response.Redirect("../Admin/EMIDetails.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }

                    else if (e.CommandName.Equals("ASG"))
                    {
                        string temp = loanid;
                        Response.Redirect("../CustomerDetails/AssignApplicationToUser.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }

                    else if (e.CommandName.Equals("COLL"))
                    {
                        string temp = loanid;
                        Response.Redirect("../Collection/Collection.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid), false);
                    }


                    else if (e.CommandName.Equals("Followup"))
                    {
                        Response.Redirect("../Admin/LoanFollowUp.aspx?edocelor=" + CryptoUtil.EncryptTripleDES(loanid) +
                                          "&emiAmt=" + emiAmt + "&paidEMI=" + updatedPaidEMI, false);
                    }

                }

            }

            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

        protected void GridView_CurrentEMI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if ((e.CommandName.Equals("EditRow")))
                {

                    string tempName = Convert.ToString(e.CommandArgument);
                    //string[] tempNameArr = tempName.Split(new char[] { '|' });
                    //string loanid = tempNameArr[0];
                    string encryptedRID = CryptoUtil.EncryptTripleDES(tempName);
                    string safeEncrypted = HttpUtility.UrlEncode(encryptedRID);
                    if (e.CommandName.Equals("EditRow"))
                    {
                        //string temp = loanid;
                        Response.Redirect("../Admin/EMIDetails.aspx?edocelor=" + safeEncrypted, false);
                    }
                }
                if ((e.CommandName.Equals("AssignRow")))
                {



                    string tempName = Convert.ToString(e.CommandArgument);
                    //string[] tempNameArr = tempName.Split(new char[] { '|' });
                    //string loanid = tempNameArr[0];
                    string encryptedRID = CryptoUtil.EncryptTripleDES(tempName);
                    string safeEncrypted = HttpUtility.UrlEncode(encryptedRID);
                    if (e.CommandName.Equals("AssignRow"))
                    {
                        //string temp = loanid;
                        Response.Redirect("../Admin/AssignLoanApplication.aspx?edocelor=" + safeEncrypted, false);
                    }
                }
            }

            catch (Exception ex)
            {
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner_3, "ERR1007");
            }
        }

        protected void GridView_CurrentEMI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CurrentEMIBindGrid();
            if (e.NewPageIndex >= 0 && e.NewPageIndex < GridView_CurrentEMI.PageCount)
            {
                GridView_CurrentEMI.PageIndex = e.NewPageIndex;
                CurrentEMIBindGrid();
            }
        }
    }
}