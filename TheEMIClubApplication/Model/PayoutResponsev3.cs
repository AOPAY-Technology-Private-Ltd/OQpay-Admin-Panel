using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class InitiateAuthGenericFundTransferAPIResp
    {
        public MetaData metaData { get; set; }
        public ResourceData resourceData { get; set; }
    }

    public class MetaData
    {
        public string status { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public DateTime time { get; set; }
    }

    public class ResourceData
    {
        public string status { get; set; }
        public string transactionReferenceNo { get; set; }
        public string transactionID { get; set; }
        public object beneficiaryName { get; set; }
    }

    public class PayoutResponsev3
    {
        public InitiateAuthGenericFundTransferAPIResp initiateAuthGenericFundTransferAPIResp { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string value { get; set; }
    }

}