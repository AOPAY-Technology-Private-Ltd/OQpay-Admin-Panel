using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using TheEMIClubApplication.AppCode;
using RestSharp.Extensions;

namespace TheEMIClubApplication.Reports
{
    public partial class LoanEmiDueswithlocationReport : System.Web.UI.Page
    {
        string ImageUrl = ConfigurationManager.AppSettings["ImageBaseUrl"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUsers();
            }
        }

        #region 🔹 DROPDOWN BINDING
        private void GetUsers()
        {
            PortalCommon.BindDropDownList(
                ddlRetailer,
                "GetRetailer",
                "", "", "", "", "",
                ""
            );
        }
        #endregion

        #region 🔹 SEARCH CLICK (API CALL)
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlRetailer.SelectedValue))
            {
                return;
            }

            var requestObj = new LoanRequest
            {
                retailerCode = ddlRetailer.SelectedValue,
                reportType = ddlReportType.SelectedValue
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string json = JsonConvert.SerializeObject(requestObj);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client
                        .PostAsync("https://api.oqpay.in/api/V1/OQFinance/getloancardfulldetails", content)
                        .Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        gvLoanEmiReport.DataSource = null;
                        gvLoanEmiReport.DataBind();
                        return;
                    }

                    string apiResult = response.Content.ReadAsStringAsync().Result;

                    var resultObj = JsonConvert.DeserializeObject<ApiResponse>(apiResult);

                    if (resultObj != null && resultObj.isSuccess)
                    {
                        gvLoanEmiReport.DataSource = resultObj.data;
                        gvLoanEmiReport.DataBind();

                        // Store for Map modal
                        Session["LoanMapData"] = resultObj.data;
                    }
                    else
                    {
                        gvLoanEmiReport.DataSource = null;
                        gvLoanEmiReport.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                gvLoanEmiReport.DataSource = null;
                gvLoanEmiReport.DataBind();
            }
        }
        #endregion

        #region 🔹 GRID ROW COMMAND (OPEN MAP MODAL)
        protected void gvLoanEmiReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewMap")
            {
                string loanCode = e.CommandArgument.ToString();

                var list = Session["LoanMapData"] as List<LoanResponse>;
                if (list == null || list.Count == 0) return;

                var record = list.FirstOrDefault(x => x.loanCode == loanCode);
                if (record == null) return;

                // BASIC INFO
                lblLoanCode.Text = record.loanCode;
                lblCustomerName.Text = record.customerName;

                // CUSTOMER PHOTO
                if (!string.IsNullOrEmpty(record.customerPhoto))
                {
                    imgCustomerPhoto.ImageUrl = ImageUrl + record.customerPhoto;
                }
                else
                {
                    imgCustomerPhoto.ImageUrl = "~/assets/no-photo.png";
                }

                // LAST LOCATION DATE
                lblLastLocationDate.Text = record.lastLocationDate.HasValue
                    ? record.lastLocationDate.Value.ToString("dd-MMM-yyyy hh:mm tt")
                    : "Location not captured";

                lblipaddress.Text = !string.IsNullOrEmpty(record.ipAddress)
               ? record.ipAddress
               : "IP address not available";

                // LOCATION VALIDATION (SweetAlert)
                if (!record.latitude.HasValue || !record.longitude.HasValue)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "noloc",
                        @"Swal.fire({
                    icon: 'warning',
                    title: 'Location Not Available',
                    text: 'Customer location has not been captured yet.',
                    confirmButtonText: 'OK'
                });",
                        true
                    );
                    return;
                }

                // OPEN MAP
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "showMap",
                    $"showMap({record.latitude.Value}, {record.longitude.Value});",
                    true
                );
            }
        }


        #endregion
    }

    #region 🔹 MODELS

    public class LoanRequest
    {
        public string retailerCode { get; set; }
        public string reportType { get; set; }
    }

    public class ApiResponse
    {
        public bool isSuccess { get; set; }
        public int totalRecords { get; set; }
        public List<LoanResponse> data { get; set; }
    }

    public class LoanResponse
    {
        public string loanStatus { get; set; }
        public string loanCode { get; set; }
        public decimal emiAmount { get; set; }
        public string centerName { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string primaryMobileNumber { get; set; }
        public string alternateMobileNumber { get; set; }
        public string refmobileNo { get; set; }
        public string customerPhoto { get; set; }
        public decimal loanAmount { get; set; }
        public decimal downPayment { get; set; }
        public int tenure { get; set; }
        public int paidEMI { get; set; }
        public int dueEMI { get; set; }
        public DateTime dueDate { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }

        public string ipAddress { get; set; }
        public DateTime? lastLocationDate { get; set; }
    }

    #endregion
}

