using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class VPAStaticQRRequestModel
    {
        public string RegistrationID { get; set; }
        public string mobileNumber { get; set; }
        public string MerchantCode { get; set; }
    }

    public class VPAStaticQRResponseModel
    {
        public int status_code { get; set; }
        public int responsecode { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public Details details { get; set; }
        public string txnRefranceNo { get; set; }
        public string RegistrationID { get; set; }
    }


    public class Details
    {
        public string qrCode { get; set; }
        public string vpa { get; set; }
        public string subvpa { get; set; }
        public string status { get; set; }
    }

    

}