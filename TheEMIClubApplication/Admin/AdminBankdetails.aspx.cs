using AjaxControlToolkit;
using AVFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.Model;
using Twilio;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Threading.Tasks;
using TheEMIClubApplication.ApiLog.ErrorLog;

namespace TheEMIClubApplication.Admin
{
    public partial class AdminBankdetails : System.Web.UI.Page
    {
        string MerchentCode = ConfigurationManager.AppSettings["OQMerchnarcode"].ToString();
        string QRBaseurl = ConfigurationManager.AppSettings["QRCodeBaseUrl"].ToString();

        BLBankdetails Objbank = new BLBankdetails();

        WriteLog writeLog = new WriteLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBankDetails();
                //  LoadRetailers();
            }
        }
        protected void chk_GenerateQR_CheckedChanged(object sender, EventArgs e)
        {
            qrRow.Visible = chk_GenerateQR.Checked;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool vpaSuccess = true;

                if (chk_GenerateQR.Checked)
                {
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "GetLocationScript", "getLocation();", true);

               
                    vpaSuccess = CreateVPA();
                }

               
                if (vpaSuccess)
                {
                    CreateAdminbankdetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(), "Error", "toastr.error('VPA Creation Failed. Bank details not saved.');", true);
                }


            }
            catch (Exception ex)
            {
                // Handle and show error
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger fw-bold";
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               updateAdminbankdetails();
            }
            catch (Exception ex)
            {
                // Handle and show error
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger fw-bold";
            }
        }


        private void BindBankDetails()
        {

            try
            {
                Objbank.Action = "SHOW";

              
                // 🔹 Get data from stored procedure
                DataTable dtbank = Objbank.Getadminbankdetails();
               
                if (dtbank != null && dtbank.Rows.Count > 0)
                {
                   
                    gvBanks.PageSize = PortalCommon.GetGridPageSize;
                    gvBanks.DataSource = dtbank;
                    gvBanks.DataBind();

                }
                else
                {
                    lblMessage.Text = "No records found";

                    gvBanks.DataSource = null;
                    gvBanks.DataBind();



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

        protected void gvBanks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rid = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditBank")
            {
                Objbank.RID = rid;
                Objbank.Action = "GET";

                DataTable dtBank = Objbank.Getadminbankdetails();

                if (dtBank != null && dtBank.Rows.Count > 0)
                {
                    DataRow dr = dtBank.Rows[0];

                    hfRID.Value = dr["RID"].ToString();
                    txtAccountNumber.Text = dr["AccountNumber"].ToString();
                    txtAccountName.Text = dr["AccountName"].ToString();
                    txtBankName.Text = dr["BankName"].ToString();
                    txtIFSCCode.Text = dr["IFSCCode"].ToString();
                    txtBranchName.Text = dr["BranchName"].ToString();
                    txtBranchAddress.Text = dr["BranchAddress"].ToString();
                    ddlActiveStatus.SelectedValue = dr["ActiveStatus"].ToString();
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    // Optional: Show modal for editing if you’re using a popup
                    ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "$('#bankModal').modal('show');", true);
                }
            }
            else if (e.CommandName == "DeleteBank")
            {
                try
                {

                    Objbank.Action = "DEL";
                    Objbank.RID = rid;




                    short retVal = Objbank.Addadminbankdetails();

                    if (retVal == 1)
                    {
                        string message = "Bank Deleted successfully!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);
                        BindBankDetails();
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
        }



        private void CreateAdminbankdetails()
        {
            try
            {
                
                Objbank.Action= "INS";
                Objbank.Brancaddress = txtBranchAddress.Text.Trim();
                Objbank.BankName = txtBankName.Text.Trim();
                Objbank.BranchName = txtBranchName.Text.Trim();
                Objbank.AccountHolder_Name = txtAccountName.Text.Trim(); ;

                Objbank.UpdatedBy = AppSessions.SessionLoginId;
                Objbank.IFSC_Code = txtIFSCCode.Text;
                Objbank.ApprovedStatus = ddlActiveStatus.SelectedValue;
                Objbank.Account_No = txtAccountNumber.Text.Trim();
                Objbank.upi_intend = "asddff";
                Objbank.qrcode_Path = img_qrimage.ImageUrl;




                short retVal = Objbank.Addadminbankdetails();

                if (retVal == 1)
                {
                    string message = "Bank addd successfully!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);
                    clear();
                    BindBankDetails();
                }

                else if (retVal == 2) 
                {
                    string message = " Duplicate Account Number found!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPage", $"ShowError('{message}');", true);


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

        private void updateAdminbankdetails()
        {
            try
            {

                Objbank.Action = "UPD";
                Objbank.RID =Convert.ToInt32(hfRID.Value);
                Objbank.Brancaddress = txtBranchAddress.Text.Trim();
                Objbank.BankName = txtBankName.Text.Trim();
                Objbank.BranchName = txtBranchName.Text.Trim();
                Objbank.AccountHolder_Name = txtAccountName.Text.Trim(); ;

                Objbank.UpdatedBy = AppSessions.SessionLoginId;
                Objbank.IFSC_Code = txtIFSCCode.Text;
                Objbank.ApprovedStatus = ddlActiveStatus.SelectedValue;
                Objbank.Account_No = txtAccountNumber.Text.Trim();

                Objbank.upi_intend = "";
                Objbank.qrcode_Path = img_qrimage.ImageUrl;


                short retVal = Objbank.Addadminbankdetails();

                if (retVal == 1)
                {
                    string message = "Bank Update successfully!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", $"ShowPopup('{message}');", true);

                }

                else if (retVal == 2)
                {
                    string message = " Duplicate Account Number found!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPage", $"ShowError('{message}');", true);


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

        public static (string lat, string lon) GetLocationFromIP()
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            string url = $"http://ip-api.com/json/{ip}";

            using (var client = new WebClient())
            {
                string json = client.DownloadString(url);
                JObject data = JObject.Parse(json);

                string lat = data["lat"]?.ToString();
                string lon = data["lon"]?.ToString();

                return (lat, lon);
            }
        }

     

        //public static async Task<string> GetStateCodeFromOSM(double lat, double lon)
        //{
        //    string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}&addressdetails=1";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

        //        var json = JObject.Parse(await client.GetStringAsync(url));

        //        var address = json["address"];
        //        if (address != null)
        //        {

        //            return address["state"]?.ToString();
        //        }
        //    }

        //    return null;
        //}
        //public static async Task<string> GetStateCode(double lat, double lon)
        //{
        //    string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}&addressdetails=1";

        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        //            var json = JObject.Parse(await client.GetStringAsync(url));

        //            var address = json["address"];
        //            if (address != null)
        //            {
        //                string stateName = address["state"]?.ToString();
        //                return stateName;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log error
        //    }

        //    return "";
        //}
        public static JObject GetStateCode(double lat, double lon)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}&addressdetails=1";

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0"; // Required by Nominatim

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        
                        string json = reader.ReadToEnd();
                        JObject data = JObject.Parse(json);

                        //return (JObject)data["address"];

                        var address = data["address"];
                        if (address != null)
                        {
                            return data["address"] as JObject;
                            //string state = address["state"]?.ToString();
                            //return state ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // handle or log error
                return null;
            }

            return null;
        }

        private bool CreateVPA()
        {
            try
            {


             string latitude = hdnLat.Value;
            string longitude = hdnLon.Value;

            JObject Address =  GetStateCode(Convert.ToDouble(latitude), Convert.ToDouble(longitude));

            string city = Address["city"].ToString();
            string district = Address["city_district"].ToString();
            string pincode = Address["postcode"].ToString();
            string state = Address?["state"]?.ToString();
            string stateCode = Address?["ISO3166-2-lvl4"]?.ToString();
 

            //string ApiUrl = "https://api.aopay.in";

            CreateVPARequestModel createVPARequestModel = new CreateVPARequestModel
            {
                apiId = "20261",
                bank_id = "2",
                partnerReferenceNo = "sample string 3",
                p1_businessName = txtbusinessName.Text.Trim(),
                p2_settlementAccountName = txtAccountName.Text.Trim(),
                p3_sellerIdentifier = txtsellerIdentifier.Text.Trim(),
                p4_mobileNumber = txtMobileNo.Text.Trim(),
                p5_emailId = txtEmailid.Text.Trim(),
                p6_mcc = "6012",
                p7_turnoverType = "SMALL",
                p8_acceptanceType = "OFFLINE",
                p9_ownershipType = "PROPRIETARY",
                p10_city = city,
                p11_district = district,
                p12_stateCode = "08",
                p13_pincode = pincode,
                p14_pan = "",
                p15_gstNumber = "",
                p16_settlementAccountNumber = txtAccountNumber.Text.Trim(),
                p17_settlementAccountIfsc = txtIFSCCode.Text.Trim(),
                p18_Latitude = latitude,
                p19_Longitude = longitude,
                p20_addressLine1 = AppSessions.SessionUserAddress,
                p21_addressLine2 = AppSessions.SessionUserAddress,
                p22_LLPIN_CIN = "",
                p26_DOB = "28/05/1987",
                p27_dOI = "01/02/2024",
                p28_websiteURL_AppPackageName = "www.boscenter.in",
                RegistrationID = MerchentCode

            };

            string input = JsonConvert.SerializeObject(createVPARequestModel);
            writeLog.WriteErrorLogs("VPA API Request: " + input);

            var client = new RestSharp.RestClient(QRBaseurl + "/api/AOP/SNBOSCreateVPA");
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
   
            request.AddParameter("application/json", input, ParameterType.RequestBody);
            var response = client.Execute(request);

            writeLog.WriteErrorLogs("VPA API Response: " + response.Content);

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var apiResponse = JsonConvert.DeserializeObject<CreateVPAResponseModel>(response.Content);

            //    if(apiResponse.status==true)
            //    {
            //        string SucessMessage = apiResponse.message;

            //        StaticQRCode();
            //        // toastr.success must be lowercase
            //        string script = $"toastr.success('{SucessMessage}', 'Success');";

            //        ScriptManager.RegisterStartupScript(this,this.GetType(), "showToastrSuccess",script, true);
            //    }
            //    else
            //    {
            //        StaticQRCode();
            //        string ErrorMessage = apiResponse.message;

            //        // toastr.success must be lowercase
                 
            //        string script = $"toastr.error('{ErrorMessage}', 'Error');";
            //        ScriptManager.RegisterStartupScript(this,this.GetType(),"showToastrError",script,true);

            //    }

            //}
            //else
            //{
       
            //    string error = response.ErrorMessage;
            //}


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var apiResponse = JsonConvert.DeserializeObject<CreateVPAResponseModel>(response.Content);

                    if (apiResponse.status == true)
                    {
                        StaticQRCode();
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "Success", $"toastr.success('{apiResponse.message}');", true);

                        return true;  
                    }
                    else
                    {
                       
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "Error", $"toastr.error('{apiResponse.message}');", true);

                        return false; 
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void StaticQRCode()
        {
            VPAStaticQRRequestModel requestModel = new VPAStaticQRRequestModel
            {
                RegistrationID = MerchentCode,
                mobileNumber = txtMobileNo.Text.Trim(),
                MerchantCode = MerchentCode
                //RegistrationID = "AOP-554",
                //mobileNumber = "8929898904",
                //MerchantCode = "AOP-554"
              
            };

            //string apiUrl = "https://api.aopay.in";

            string input = JsonConvert.SerializeObject(requestModel);

            var client = new RestClient(QRBaseurl + "/api/AOP/BOSVPAStaticQR");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", input, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

       

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
              
                var apiResponse = JsonConvert.DeserializeObject<VPAStaticQRResponseModel>(response.Content);

                if(apiResponse.status !=false)
                {
                    string upiQRString = apiResponse.details.qrCode;

                    GenerateQRCode(upiQRString);
                }
                else
                {
                    string ErrorMessage = apiResponse.message;

                    // toastr.success must be lowercase

                    string script = $"toastr.error('{ErrorMessage}', 'Error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showToastrError", script, true);

                }
            }
            else
            {
                
                string error = response.ErrorMessage;
            }

        }

        public void GenerateQRCode(string qrString)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300,
                    Margin = 1
                }
            };

            // File name (you can also use RegistrationID or VPA)
            string fileName = "upi_qr.png";
            string folderPath = Server.MapPath("~/QRImages/");
            string savePath = Path.Combine(folderPath, fileName);

            // Check if folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Check if file exists
            if (!File.Exists(savePath))
            {
                using (Bitmap qrBitmap = writer.Write(qrString))
                {
                    qrBitmap.Save(savePath, ImageFormat.Png);
                }
            }

            // Display image
            img_qrimage.ImageUrl = "~/QRImages/" + fileName;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
         clear();
        }

        private void clear()
        {
            txtAccountName.Text = "";
            txtAccountNumber.Text = "";
            txtIFSCCode.Text = "";
            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtBranchAddress.Text = "";
            txtbusinessName.Text = "";
            txtsellerIdentifier.Text = "";
            txtMobileNo.Text = "";
            txtEmailid.Text = "";
            ddlActiveStatus.SelectedIndex = 0;
            chk_GenerateQR.Checked = false;
            qrRow.Visible = false;
        }

        //public string GetMCC()
        //{
        //    GetMCCRequestModel getMCCRequestModel = new GetMCCRequestModel
        //    {
        //        RegistrationID = MerchentCode
        //    };

        //    string ApiUrl = "https://api.aopay.in/api/AOP/V1/AEPS/GetMccList";

        //    string input = JsonConvert.SerializeObject(getMCCRequestModel);

        //    var client = new RestClient(ApiUrl);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/json", input, ParameterType.RequestBody);

        //    IRestResponse response = client.Execute(request);

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var apiResponse = JsonConvert.DeserializeObject<GetMCCResponseModel>(response.Content);
        //    }
        //    else
        //    {
        //        // Error handling
        //    }
        //}

    }
}