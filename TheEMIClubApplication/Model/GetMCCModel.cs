using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class GetMCCRequestModel
    {
        public string RegistrationID { get; set; }
    }

    public class GetMCCResponseModel
    {
        public bool status { get; set; }
        public object msg { get; set; }
        public object data { get; set; }
        public int statusCode { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
        public string Value { get; set; }
    }
}