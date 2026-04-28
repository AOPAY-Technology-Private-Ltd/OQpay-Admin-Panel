using AVFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheEMIClubApplication.BussinessLayer;

namespace TheEMIClubApplication.MasterPage
{
    public partial class EMIMaster : System.Web.UI.Page
    {
        BLEMIDetails objEMI = new BLEMIDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            string encryptedRID = Request.QueryString["edocelor"];
            string decryptedRID = CryptoUtil.DecryptTripleDES(encryptedRID);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void gvInstallments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
                return;
        }


    }
}