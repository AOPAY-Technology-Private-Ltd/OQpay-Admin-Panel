using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class CreateVPARequestModel
    {
        public string apiId { get; set; }
        public string bank_id { get; set; }
        public string partnerReferenceNo { get; set; }
        public string p1_businessName { get; set; }
        public string p2_settlementAccountName { get; set; }
        public string p3_sellerIdentifier { get; set; }
        public string p4_mobileNumber { get; set; }
        public string p5_emailId { get; set; }
        public string p6_mcc { get; set; }
        public string p7_turnoverType { get; set; }
        public string p8_acceptanceType { get; set; }
        public string p9_ownershipType { get; set; }
        public string p10_city { get; set; }
        public string p11_district { get; set; }
        public string p12_stateCode { get; set; }
        public string p13_pincode { get; set; }
        public string p14_pan { get; set; }
        public string p15_gstNumber { get; set; }
        public string p16_settlementAccountNumber { get; set; }
        public string p17_settlementAccountIfsc { get; set; }
        public string p18_Latitude { get; set; }
        public string p19_Longitude { get; set; }
        public string p20_addressLine1 { get; set; }
        public string p21_addressLine2 { get; set; }
        public string p22_LLPIN_CIN { get; set; }
        public string p26_DOB { get; set; }
        public string p27_dOI { get; set; }
        public string p28_websiteURL_AppPackageName { get; set; }
        public string RegistrationID { get; set; }
    }

    public class CreateVPAResponseModel
    {
        public int status_code { get; set; }
        public int responsecode { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public List<object> details { get; set; }
    }
}