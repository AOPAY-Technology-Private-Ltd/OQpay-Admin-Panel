using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class PayoutRequestModelv3
    {
        public string accountNumber { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string beneficiaryIFSC { get; set; }
        public string beneficiaryName { get; set; }
        public string emailID { get; set; }
        public string mobileNo { get; set; }
        public string registrationID { get; set; }
        public string tranrefID { get; set; }
    }

}