using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    public class FollowUpNotification
    {
        public string LoanCode { get; set; }        // l.LoanCode
        public string CustomerCode { get; set; }    // l.CustomerCode
        public string CustomerName { get; set; }    // CONCAT(c.FirstName, ' ', c.LastName)
        public string Products { get; set; }        // CONCAT(c.ModelName, ' ', c.BrandName,' ',VariantName)
        public string FollowUpRemarks { get; set; } // lfu.FollowUpRemarks
        public decimal EMIAmount { get; set; }      // l.EMIAmount
        public int LoanTenureNo { get; set; }       // lfu.LoanTenureNo
        public string FollowUpStatus { get; set; }  // lfu.FollowUpStatus
        public int FollowUpID { get; set; }         // if still available in select
        public string NextFollowUpDate { get; set; } // keep if procedure still returns
    }
}