using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using AVFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using RestSharp.Extensions;
using System.IO;


namespace TheEMIClubApplication.CustomerDetails
{
    public partial class CreateEmi : System.Web.UI.Page
    {
        BLPersonalDetails createemis = new BLPersonalDetails();
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtLoanAmount.Text.Length == 0 || txtLoanAmount.Text.Trim() == string.Empty)
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1116");
                    txtLoanAmount.Focus();
                    txtLoanAmount.Text = "0.00";

                }
                else if (txtAnnualInterestRate.Text.Length == 0 || txtAnnualInterestRate.Text.Trim() == string.Empty)
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1115");
                    txtAnnualInterestRate.Focus();
                    txtAnnualInterestRate.Text = "0.00";

                }

                else if (txtLoanTermInYears.Text.Length == 0 || txtLoanTermInYears.Text.Trim() == string.Empty)
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1117");
                    txtLoanTermInYears.Focus();
                    txtLoanTermInYears.Text = "0.00";

                }

                else if (txtprocessingcharge.Text.Length == 0 || txtprocessingcharge.Text.Trim() == string.Empty)
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1118");
                    txtprocessingcharge.Focus();
                    txtprocessingcharge.Text = "0.00";

                }

                else if (txtothercharge.Text.Length == 0 || txtothercharge.Text.Trim() == string.Empty)
                {
                    spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1119");
                    txtothercharge.Focus();
                    txtothercharge.Text = "0.00";

                }

                
             
                else
                {
                    spnMessage.InnerText = string.Empty;
                    decimal processingcharge = decimal.Parse(txtprocessingcharge.Text);
                    decimal otherCharge = decimal.Parse(txtothercharge.Text);



                    decimal loanAmount = decimal.Parse(txtLoanAmount.Text);
                    decimal annualInterestRate = decimal.Parse(txtAnnualInterestRate.Text.Trim());
                    int loanTermInMonths = int.Parse(txtLoanTermInYears.Text);
                    int emiType = int.Parse(ddlEmiType.SelectedValue);
                    int calculationType = int.Parse(ddlCalculationType.SelectedValue);
                    DateTime emidate = Convert.ToDateTime(txtEmistartDate.Text);
                    decimal tottalamount = (loanAmount + processingcharge + otherCharge);
                    decimal transferAmount = (loanAmount - (processingcharge + otherCharge));
                    EmiController emiController = new EmiController();
                    EmiModel model = emiController.CalculateEmi(loanAmount, annualInterestRate, loanTermInMonths, emiType, calculationType, emidate);
                    summary.Visible = true;
                    lblResult.Text = $"{model.Emi:C} per installment";
                    lblloanAmount.Text = $"{loanAmount:C}";


                    // Display Payment Schedule in GridView
                    gvPaymentSchedule.DataSource = model.PaymentSchedule;
                    gvPaymentSchedule.DataBind();
                    decimal Principalamt = 0;
                    decimal intrest = 0;
                    decimal Totalemi = 0;

                    for (int i = 0; i <= gvPaymentSchedule.Rows.Count - 1; i++)
                    {
                        // gvTransactionDetails.Rows[i].Cells[1].Text =i.ToString()+1;

                        if (Convert.ToString(gvPaymentSchedule.Rows[i].Cells[2].Text) == "")
                        {
                            gvPaymentSchedule.Rows[i].Cells[2].Text = "0";
                        }

                        if (Convert.ToString(gvPaymentSchedule.Rows[i].Cells[3].Text) == "")
                        {
                            gvPaymentSchedule.Rows[i].Cells[3].Text = "0";
                        }
                        if (Convert.ToString(gvPaymentSchedule.Rows[i].Cells[4].Text) == "")
                        {
                            gvPaymentSchedule.Rows[i].Cells[4].Text = "0";
                        }




                        gvPaymentSchedule.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        gvPaymentSchedule.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        gvPaymentSchedule.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Right;


                        Principalamt = Principalamt + Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[2].Text.Replace("₹", "").Replace(",", ""));
                        intrest = intrest + Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[3].Text.Replace("₹", "").Replace(",", ""));
                        Totalemi = Totalemi + Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[4].Text.Replace("₹", "").Replace(",", ""));

                        gvPaymentSchedule.FooterRow.Cells[1].Text = "Total";
                        gvPaymentSchedule.FooterRow.Cells[2].Text = Convert.ToString(Principalamt.ToString("C"));

                        gvPaymentSchedule.FooterRow.Cells[3].Text = Convert.ToString(intrest.ToString("C"));

                        gvPaymentSchedule.FooterRow.Cells[4].Text = Convert.ToString(Totalemi.ToString("C"));
                        gvPaymentSchedule.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                        gvPaymentSchedule.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        gvPaymentSchedule.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        gvPaymentSchedule.Visible = true;
                        totalinterst.Text = $"{intrest:C}";
                        txtLoanTermInYears.Text = $"{loanTermInMonths}";
                        if (calculationType == 1)
                        {
                            lbltermtype.Text = "Month";
                        }
                        if (calculationType == 2)
                        {
                            lbltermtype.Text = "Day";
                        }
                        lbllonaterm.Text = $"{annualInterestRate}";
                        totalpayment.Text = $"{tottalamount:C}";
                        lbltransferamt.Text = $"{transferAmount:C}";
                        btnCreateEmi.Visible= true ;
                        lbltermtype.Text= $"{ddlEmiType.SelectedItem.Text}";
                        lblloaninterstrate.Text= $"{annualInterestRate}";
                    }
                    btncancel.Visible= true ;
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CommonPages/Home.aspx");

         
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {



                    //If coming from some other page.. redierct to that same page.
                    createemis.LoanID = Request.QueryString["edocelor"].Trim();
                    createemis.LoanID = CryptoUtil.DecryptTripleDES(createemis.LoanID.Replace(" ", "+"));
                    GetuserDetail();

                    txtEmistartDate.Text = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");



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
          
            // runningTotal = new decimal[5]; // Assuming there are 5 columns (excluding EMI Number and Due Date)
        }
        protected void gvPaymentSchedule_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

        }
        private void GetuserDetail()
        {
            Hashtable htManageUser = new Hashtable();
            try
            {

                createemis.LoanID = Request.QueryString["edocelor"].Trim();
                createemis.LoanID = CryptoUtil.DecryptTripleDES(createemis.LoanID.Replace(" ", "+"));
                htManageUser = createemis.ReterivePersonalDetails();

                if (htManageUser.Count > 0)
                {
                    txtLoanAmount.Text = Convert.ToString(htManageUser["LoanAmount"]);
                    txtLoanTermInYears.Text = Convert.ToString(htManageUser["LoanTenure"]);
                    lbl_custmerid.Text = Convert.ToString(htManageUser["CustCode"]);

                    txtEmistartDate.Text = System.DateTime.Now.AddDays(30).ToShortDateString();
                    //txtdob.Text = Convert.ToString(htManageUser["DOB"]);
                    //txtMobile.Text = Convert.ToString(htManageUser["phone"]);
                    //txtIncome.Text = Convert.ToString(htManageUser["SalaryAmount"]);
                    //txtemployment.Text = Convert.ToString(htManageUser["EmployementType"]);
                    //txtAddress.Text = Convert.ToString(htManageUser["ADDRESS"]);
                    //txtPincode.Text = Convert.ToString(htManageUser["PinCode"]);

                    //txtAccountNumber.Text = Convert.ToString(htManageUser["AccountNumber"]);
                    //txtbankname.Text = Convert.ToString(htManageUser["BankName"]);
                    //txtBranch.Text = Convert.ToString(htManageUser["BranchName"]);
                    //txtIfsc.Text = Convert.ToString(htManageUser["IFSCCode"]);

                    //ddlcallerName.SelectedItem.Text = Convert.ToString(htManageUser["Telecallername"]);
                    //ddlverification.SelectedItem.Text = Convert.ToString(htManageUser["PhysicalVerificationPerson"]);

                    //if ((ddlcallerName.SelectedItem.Text == "" || ddlcallerName.Text == null) && (ddlverification.SelectedItem.Text == ""))
                    //{
                    //    btnSave.Visible = true;
                    //    ddlcallerName.Enabled = true;
                    //    ddlverification.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnSave.Visible = false;
                    //    ddlcallerName.Enabled = false;
                    //    ddlverification.Enabled = false;
                    //    btnCreateEMi.Visible = true;
                    //    btnSave.Visible = false;
                    //}

                    //txtloantype.Text = Convert.ToString(htManageUser["LoanType"]);
                    //txtloanamount.Text = Convert.ToString(htManageUser["LoanAmount"]);
                    //txtloanDuration.Text = Convert.ToString(htManageUser["LoanTenure"]);




                }
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
        protected void btnCreateEmi_Click(object sender, EventArgs e)
        {
            try
            {

              

                for (int i = 0; i < gvPaymentSchedule.Rows.Count; i++)
                {
                    createemis.LoanID = Request.QueryString["edocelor"].Trim();
                    createemis.LoanID = CryptoUtil.DecryptTripleDES(createemis.LoanID.Replace(" ", "+"));
                    createemis.Usercode = lbl_custmerid.Text.Trim();
                 
                    createemis.LoanCategory = ddlEmiType.SelectedItem.Text;
                    createemis.LoanType = ddlCalculationType.SelectedItem.Text;
                    createemis.EMIStartDate = Convert.ToDateTime(gvPaymentSchedule.Rows[i].Cells[1].Text);
                    createemis.EMIStatus = "Due";
                    createemis.InsttalmentNo = gvPaymentSchedule.Rows[i].Cells[0].Text.Trim();

                    createemis.TotalAmount = Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[4].Text.Replace("₹", "").Replace(",", ""));

                    createemis.ROI = Convert.ToDecimal(txtAnnualInterestRate.Text);
                    createemis.InterestAmount = Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[3].Text.Replace("₹", "").Replace(",", ""));
                    createemis.InstallmentAmount = Convert.ToDecimal(gvPaymentSchedule.Rows[i].Cells[2].Text.Replace("₹", "").Replace(",", ""));
                    createemis.Loandisburshmentamt = Convert.ToDecimal(txtLoanAmount.Text.Trim());
                    createemis.monthday=txtLoanTermInYears.Text;


                    createemis.paramuser = AppSessions.SessionLoginId;
                    createemis.ActiveStatus = "Active";



                    short retVal = createemis.CreateEmiShudule();


                    if (retVal == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "Your EMI schedule Created successfully. We will send you further status updates." + "');", true);
                       
                    }

                    if (retVal == 0)
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" + "Try Again After Some time" + "');", true);



                    }

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
        }
    }













    public class EmiModel
    {
        public decimal LoanAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int LoanTermInMonths { get; set; }
        public int EmiType { get; set; }
        public int CalculationType { get; set; }
        public decimal Emi { get; set; }
        public DateTime Emidate { get; set; }
        public List<PaymentScheduleItem> PaymentSchedule { get; set; }
    }

    public class PaymentScheduleItem
    {
        public int EmiNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TotalEmi { get; set; }
        public decimal RunningTotal { get; set; }




    }

    public class EmiController
    {
        public EmiModel CalculateEmi(decimal loanAmount, decimal annualInterestRate, int loanTermInMonths, int emiType, int calculationType, DateTime emidate)
        {
            decimal monthlyInterestRate = annualInterestRate / 12 / 100;
            int totalPayments = loanTermInMonths;

            EmiModel model = new EmiModel
            {
                LoanAmount = loanAmount,
                AnnualInterestRate = annualInterestRate,
                LoanTermInMonths = loanTermInMonths,
                EmiType = emiType,
                CalculationType = calculationType,
                Emidate = emidate,


                PaymentSchedule = new List<PaymentScheduleItem>()
            };

            decimal emi = CalculateEmiAmount(loanAmount, monthlyInterestRate, totalPayments, emiType);
            model.Emi = emi;

            decimal remainingLoanAmount = loanAmount;

            for (int i = 1; i <= totalPayments; i++)
            {
                DateTime dueDate = emidate.AddMonths(i - 1);
                //DateTime dueDate =  DateTime.Now.AddMonths(i);
                if (calculationType == 2) // Day-wise
                                          //dueDate = DateTime.Now.AddDays(i);
                    dueDate = emidate.AddDays(i - 1);
                decimal interest=0;
                decimal principal=0;
                if (emiType == 2)
                {
                    interest = remainingLoanAmount * monthlyInterestRate;
                    principal = emi - interest;
                    remainingLoanAmount -= principal;
                }
                if (emiType == 1)
                {
                    interest = remainingLoanAmount * monthlyInterestRate;
                    principal = emi - interest;
                }
              

                model.PaymentSchedule.Add(new PaymentScheduleItem
                {
                    EmiNumber = i,
                    DueDate = dueDate,
                    PrincipalAmount = principal,
                    InterestRate = interest,
                    TotalEmi = emi,
                    RunningTotal = emi * i

                });

            }

            return model;
        }

        private decimal CalculateEmiAmount(decimal loanAmount, decimal monthlyInterestRate, int totalPayments, int emiType)
        {
            if (emiType == 1) // Flat EMI
            {
                return (loanAmount + loanAmount * monthlyInterestRate * totalPayments) / totalPayments;
            }
            else if (emiType == 2) // Reducing EMI
            {
                return (loanAmount * monthlyInterestRate) / (decimal)(1 - Math.Pow((double)(1 + monthlyInterestRate), -totalPayments));
            }

            return 0;
        }

      
    }
}

      

    
