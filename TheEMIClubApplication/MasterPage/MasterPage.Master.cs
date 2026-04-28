using TheEMIClubApplication.AppCode;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.Model;
using System.Collections;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Drawing;
using System.Data;
using System.Reflection.Emit;
using Telerik.Web.UI.ExportInfrastructure;
using System.Configuration;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using RestSharp.Extensions;
using System.Net.NetworkInformation;

namespace TheEMIClubApplication.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        //GlobalVariable GV = new GlobalVariable("ADMIN");
        BLCompanyDetails objCMP = new BLCompanyDetails();
        int returnCode;
        string returnMsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetCompanyDetails();
                APPMenuBase objAppMenuBase = new APPMenuBase();
                objAppMenuBase.LoginId = AppSessions.SessionLoginId;
                objAppMenuBase.ApplicationCode = Constants.ApplicationCode;

                DataSet dsAMMenu = objAppMenuBase.GetMenu();
                RenderMenu_Repeaters(dsAMMenu);

            }
            if (AppSessions.SessionLoginId == "ADMIN")
            {
                // Log for debugging

                BindNotifications();
            }


            //if (!IsPostBack)
            //{
            //    try
            //    {
            //        if (AppSessions.SessionLoginId.Length == 0)
            //        {
            //            //Session Expired OR Session NOT Created
            //            string urlPageFrom = Request.Url.ToString();
            //            urlPageFrom = CryptoUtil.EncryptTripleDES(urlPageFrom);
            //            Response.Redirect("../" + Constants.Path_LoginPage + "?morfegaplru=" + urlPageFrom);
            //        }
            //        else
            //        {

            //            WalletCalculations();

            //            try
            //            {

            //                //spnMessage.InnerText = string.Empty;
            //                //Calculation Debit Balance
            //                BLWalletmanage objCompany = new BLWalletmanage();
            //                objCompany.parmUserName = AppSessions.SessionLoginId;

            //                Hashtable htFeatureDetail = objCompany.changethems();


            //                thems = htFeatureDetail["Themschange"].ToString();
            //                if (thems == "")
            //                {
            //                    theam_link.Href = "~/CSS/Default-CSS/Inner-pages/innerStyleOne.css";

            //                }
            //                else

            //                {
            //                    theam_link.Href = thems;
            //                }
            //            }
            //            catch (SqlException sqlEx)
            //            {
            //                throw sqlEx;
            //            }
            //            catch (Exception ex)
            //            {
            //                throw ex;
            //            }
            //            //Check page's accessibility
            //            //PortalCommon.CheckPageAccessibility(PortalCommon.PageLevel.Inner);
            //        }
            //    }
            //    catch (ApplicationException appEx)
            //    {
            //        Common.WriteExceptionLog(appEx);
            //    }
            //}
        }

        protected void ParentMenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var drv = (System.Data.DataRowView)e.Item.DataItem;
                var phChild = (PlaceHolder)e.Item.FindControl("phChildMenu");
                var parentLink = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("lnkParent");

                bool hasChildren = drv.Row.GetChildRows("ChildRelation").Length > 0;

                if (hasChildren)
                {
                    // Show children and set collapse attributes
                    phChild.Visible = true;
                    parentLink.HRef = "javascript:void(0);";
                    parentLink.Attributes["data-toggle"] = "collapse";
                    parentLink.Attributes["data-target"] = "#menu_" + drv["ChildMenuCode"];
                }
                else
                {
                    // No children → make it a real link
                    phChild.Visible = false;
                    parentLink.HRef = drv["NavigateURL"].ToString() + "?selectedParent=" + drv["ChildMenuCode"].ToString();
                }
            }
        }



        private void RenderMenu_Repeaters(DataSet dsMenu)
        {
            try
            {
                if (dsMenu != null && dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0)
                {
                    dsMenu.DataSetName = "AMMenus";
                    dsMenu.Tables[0].TableName = "AMMenu";

                    // ✅ Ensure required columns exist
                    if (!dsMenu.Tables["AMMenu"].Columns.Contains("ChildMenuCode") ||
                        !dsMenu.Tables["AMMenu"].Columns.Contains("ParentMenuCode"))
                    {
                        throw new Exception("MenuCode or ParentMenuCode column is missing in dataset.");
                    }

                    // ✅ Create Relation (Parent → Child) if not already present
                    if (!dsMenu.Relations.Contains("ChildRelation"))
                    {
                        DataRelation relation = new DataRelation(
                            "ChildRelation",
                            dsMenu.Tables["AMMenu"].Columns["ChildMenuCode"],         // parent
                            dsMenu.Tables["AMMenu"].Columns["ParentMenuCode"],  // child
                            false
                        );
                        dsMenu.Relations.Add(relation);
                    }

                    // ✅ Get top-level menu items (ParentMenuCode is NULL or empty string)
                    DataView dvParents = new DataView(dsMenu.Tables["AMMenu"]);
                    dvParents.RowFilter = "ParentMenuCode IS NULL OR ParentMenuCode = ''";

                    ParentMenuRepeater.DataSource = dvParents;
                    ParentMenuRepeater.DataBind();
                }
                else
                {
                    ParentMenuRepeater.DataSource = null;
                    ParentMenuRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                // log ex.Message
                throw;
            }
            finally
            {
                dsMenu?.Dispose();
            }
        }


        //private void BindNotifications()
        //{
        //    string assignedTo = AppSessions.SessionLoginId;
        //    List<FollowUpNotification> notifications = NotificationService.GetNotifications(assignedTo);
        //    NotificationRepeater.DataSource = notifications;
        //    NotificationRepeater.DataBind();

        //    // Update notification count
        //    notificationBadge.InnerText = notifications.Count.ToString();

        //    // Set visibility of the bell icon based on notifications count
        //    notificationBell.Visible = notifications.Count > 0;
        //}

        private void BindNotifications()
        {
            string assignedTo = AppSessions.SessionLoginId;
            List<FollowUpNotification> notifications = NotificationService.GetNotifications();
            NotificationRepeater.DataSource = notifications;
            NotificationRepeater.DataBind();

            // Update notification count
            notificationBadge.InnerText = notifications.Count.ToString();

            // Set visibility of the bell icon based on notifications count
            notificationBell.Visible = notifications.Count > 0;
        }

        public string GetEncryptedLeadUrl(string LoanID)
        {
            if (!string.IsNullOrEmpty(LoanID))
            {
                // Encrypt the LeadID using TripleDES
                string encryptedLeadId = CryptoUtil.EncryptTripleDES(LoanID);
               // Admin / MakePaymentApproval.aspx
               // Return the full URL with the encrypted LeadID
                return "../Admin/MakePaymentApproval.aspx?edocelor=" + encryptedLeadId;
            }
            return "#"; // In case of null or empty LeadID
        }

        private void GetCompanyDetails()
        {
            objCMP.State = "Active";
      

            DataTable dtCompany = objCMP.getCompanyDetail(out returnCode, out returnMsg);

            if (returnCode == 1 && dtCompany.Rows.Count > 0)
            {

                DataRow row = dtCompany.Rows[0];

                spncompanyName.InnerText = row["CompanyName"].ToString();
                //txtClientCode.Text = row["ClientCode"].ToString();
                //txtAddress.Text = row["Address"].ToString();
                //txtCity.Text = row["City"].ToString();
                //txtState.Text = row["State"].ToString();
                //txtCountry.Text = row["Country"].ToString();
                //txtZip.Text = row["Zip"].ToString();
                //txtPhone.Text = row["Phone"].ToString();
                //txtFirstName.Text = row["FirstName"].ToString();
                //txtLastName.Text = row["LastName"].ToString();
                //txtEmail.Text = row["Email"].ToString();
                //ddlStatus.SelectedValue = row["Status"].ToString();
                //txtCompanyType.Text = row["CompanyType"].ToString();
                //txtWebsite.Text = row["Website"].ToString();
                //txtUsername.Text = row["Username"].ToString();
                //txtPassword.Text = row["Password"].ToString();


                // Optional hidden fields
                //hfRID.Value = row["RID"].ToString();
                //hfCreateDate.Value = row["createDate"].ToString();
                //hfLastUpdate.Value = row["lastUpdate"].ToString();
                //hfEUser.Value = row["EUser"].ToString();
                //hfEDate.Value = row["EDate"].ToString();
                //hfMUser.Value = row["MUser"].ToString();
                //hfMDate.Value = row["MDate"].ToString();
                //txtLogoPath.Text = row["LogoPath"].ToString();
                Mst_imglogo.Src= row["LogoPath"].ToString();

                //lblMessage.Text = "Company loaded.";
                //lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                //lblMessage.Text = returnMsg;
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        //public void WalletCalculations()
        //{
        //    try
        //    {
        //        string group = AppSessions.SessionUserRoleCode;
        //        //string DBName = "BosManagerdb";
        //        //string FromAmount = "";
        //        //string ToAmount = "";
        //        //string Payoutamount = "";
        //        //string PendingAmount = "";

        //        if (group.Trim().ToUpper() == "SUPERADMIN")
        //        {
        //            adminDebitBal();
        //            admincreditBal();
        //            adminDisputeBal(); 

        //        }
        //        else
        //        {
        //            creditBal();
        //            DebitBal();
        //            Disputeamt();
        //            //NotifictionAllert();
        //            NotifictionAllert();
        //        }                

        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteExceptionLog(ex);
        //        //spn.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        //private void DebitBal()
        //{
        //    try
        //    {

        //        //spnMessage.InnerText = string.Empty;
        //        //Calculation Debit Balance
        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "DebitBalance";
        //        Hashtable htFeatureDetail = objCompany.wallletcalulation();

        //        //Calculation Credit Balance
        //        //objCompany = new BLWalletmanage();
        //        //objCompany.parmUserName = AppSessions.SessionLoginId;
        //        //objCompany.paramflag = "CreditBalance";
        //        //Hashtable htCreditBalance = objCompany.wallletcalulation();
        //        //double DebitBalance = 0;

        //        //if (htFeatureDetail.Count > 0 && (AppSessions.SessionLoginId == "BOS-2108"))
        //        //{
        //        //    DebitBalance = Convert.ToDouble(htCreditBalance["CreditBalance"]) - Convert.ToDouble(htFeatureDetail["DebitBalance"]);
        //        //    lblDebitBalance.Text = DebitBalance.ToString();
        //        //}
        //        //else
        //        //{
        //            lblDebitBalance.Text =Convert.ToDouble(htFeatureDetail["DebitBalance"]).ToString("0.00");
        //        //}

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //string thems = "";
        //private void changethesm()
        //{


        //}
        //private void adminDebitBal()
        //{
        //    try
        //    {

        //        //spnMessage.InnerText = string.Empty;
        //        //Calculation Debit Balance
        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "DebitBalance";
        //        Hashtable htFeatureDetail = objCompany.Admincalulation();


        //        lblDebitBalance.Text = htFeatureDetail["DebitBalance"].ToString();


        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private void admincreditBal()
        //{
        //    try
        //    {

        //        //spnMessage.InnerText = string.Empty;
        //        //Calculation Debit Balance
        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "CreditBalance";
        //        Hashtable htFeatureDetail = objCompany.Admincalulation();


        //        lblCreditBalance.Text = htFeatureDetail["CreditBalance"].ToString();


        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        //private void adminDisputeBal()
        //{
        //    try
        //    {

        //        //spnMessage.InnerText = string.Empty;
        //        //Calculation Debit Balance
        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "DisputeBalance";
        //        Hashtable htFeatureDetail = objCompany.Admincalulation();


        //        if (htFeatureDetail.Count > 0)
        //        {
        //            lblDisputeAmount.Text = Convert.ToString(htFeatureDetail["DisputeBalance"]).Trim();


        //        }
        //        if (lblDisputeAmount.Text == "0")
        //        {
        //            lblDisputeAmount.Visible = false;
        //            lblDispute.Visible = false;
        //        }
        //        else
        //        {
        //            lblDisputeAmount.Visible = true;
        //            lblDispute.Visible = true;
        //        }


        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //private void creditBal()
        //{
        //    try
        //    {


        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "CreditBalance";

        //        Hashtable htFeatureDetail = objCompany.wallletcalulation();


        //        if (htFeatureDetail.Count > 0)
        //        {
        //            lblCreditBalance.Text = Convert.ToString(htFeatureDetail["CreditBalance"]).Trim();

        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        ////private void NotifictionAllert()
        ////{
        ////    try
        ////    {


        ////        BLWalletmanage objCompany = new BLWalletmanage();
        ////        objCompany.parmUserName = AppSessions.SessionLoginId;


        ////        Hashtable htFeatureDetail = objCompany.Notificationallert();


        ////        if (htFeatureDetail.Count > 0)
        ////        {

        ////            lbltifaction.Text = Convert.ToString(htFeatureDetail["TransferToMsg"]).Trim();
        ////            if (lbltifaction.Text.Length > 0)
        ////            {

        ////                //string hex = "#00FF00";
        ////                //Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        ////                create_project.Disabled = false;

        ////                BellNoticaction.Attributes.Add("style", "color: #ff0000;");
        ////                BellNoticaction.InnerText = htFeatureDetail.Count.ToString();



        ////            }

        ////        }

        ////    }
        ////    catch (SqlException sqlEx)
        ////    {
        ////        throw sqlEx;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }

        ////}
        //int count = 0;

        //private void NotifictionAllert()
        //{
        //    using (var connection = new SqlConnection(getConnectionString()))
        //    {
        //        try
        //        {

        //            SqlCommand sql_cmnd = new SqlCommand("usp_noticicationAllert", connection);
        //            sql_cmnd.CommandType = System.Data.CommandType.StoredProcedure;

        //            sql_cmnd.Parameters.AddWithValue("@ParamUser", SqlDbType.NVarChar).Value = AppSessions.SessionLoginId;



        //            connection.Open();
        //            sql_cmnd.ExecuteNonQuery();
        //            DataTable dt=new DataTable();
        //            SqlDataAdapter dr = new SqlDataAdapter(sql_cmnd);
        //            dr.Fill(dt);
        //            count = Convert.ToInt32(dt.Rows.Count.ToString());
        //            Notification1.Text = count.ToString();
        //            Notification2.Text = count.ToString();
        //            r1.DataSource=dt;
        //            r1.DataBind();

        //            connection.Close();
        //            if (dt.Rows.Count == 0)
        //            {
        //                message.Visible = false;
        //                Notification1.Visible= false;
        //                Notification2.Visible= false;

        //            }

        //        }
        //        catch (SqlException sqlEx)
        //        {
        //            Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //           // spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //        }
        //    }

        //}
        //public string gettwentyCh(object myvales)
        //{
        //    string a;
        //   string more  = "Read More";



        //  //  state=System.Drawing.Color.Red.ToString();


        //    a = Convert.ToString(myvales.ToString());
        //    string b = "";
        //    if (a.Length >40)
        //    {
        //        b = a.Substring(0, 40);

        //        return b.ToString() + " " + more; 
        //    }
        //    else 
        //    {
        //        b = a.ToString();
        //        return b.ToString();
        //    }
        //}
        //private void Disputeamt()
        //{
        //    try
        //    {


        //        BLWalletmanage objCompany = new BLWalletmanage();
        //        objCompany.parmUserName = AppSessions.SessionLoginId;
        //        objCompany.paramflag = "Dispute";

        //        Hashtable htFeatureDetail = objCompany.wallletcalulation();


        //        if (htFeatureDetail.Count > 0)
        //        {
        //            lblDisputeAmount.Text = Convert.ToString(htFeatureDetail["Disputeamt"]).Trim();


        //        }
        //        if (lblDisputeAmount.Text == "0")
        //        {
        //            lblDisputeAmount.Visible = false;
        //            lblDispute.Visible = false;
        //        }
        //        else
        //        {
        //            lblDisputeAmount.Visible = true;
        //            lblDispute.Visible = true;
        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //protected void btn_Themecolor_black_Click(object sender, EventArgs e)
        //{
        //    BlackThems();

        //}
        //protected void btn_Themecolor_yellow_Click(object sender, EventArgs e)
        //{
        //    yellowThems();

        //}
        //protected void btn_Themecolor_darkblue_Click(object sender, EventArgs e)
        //{
        //    blueThems();

        //}

        //protected void btn_Themecolor_bydefault_Click(object sender, EventArgs e)
        //{
        //    bydefaultThems();

        //}
        ////protected void Timer1_Tick(object sender, EventArgs e)
        ////{
        ////    NotifictionAllert();
        ////}

        //private void BlackThems ()
        //{
        //    try
        //    {
        //        BLWalletmanage objManageUser = new BLWalletmanage();
        //        objManageUser.parmUserName = AppSessions.SessionLoginId;
        //        objManageUser.Thmesapply = "~/CSS/Default-CSS/Inner-pages/innerStyleBlackOne.css";




        //        short retVal = objManageUser.Updatethems();


        //        if (retVal == 1)
        //        {



        //            Response.Redirect("../CommonPages/Home.aspx");


        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteExceptionLog(ex);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        //private void blueThems()
        //{
        //    try
        //    {
        //        BLWalletmanage objManageUser = new BLWalletmanage();
        //        objManageUser.parmUserName = AppSessions.SessionLoginId;
        //        objManageUser.Thmesapply = "~/CSS/Default-CSS/Inner-pages/innerStyleBlueOne.css";




        //        short retVal = objManageUser.Updatethems();


        //        if (retVal == 1)
        //        {



        //            Response.Redirect("../CommonPages/Home.aspx");


        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        //Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.WriteExceptionLog(ex);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}

        //private void bydefaultThems()
        //{
        //    try
        //    {
        //        BLWalletmanage objManageUser = new BLWalletmanage();
        //        objManageUser.parmUserName = AppSessions.SessionLoginId;
        //        objManageUser.Thmesapply = "~/CSS/Default-CSS/Inner-pages/innerStyleOne.css";




        //        short retVal = objManageUser.Updatethems();


        //        if (retVal == 1)
        //        {



        //            Response.Redirect("../CommonPages/Home.aspx");


        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        //Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.WriteExceptionLog(ex);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //}


        //private void yellowThems()
        //{
        //    try
        //    {
        //        BLWalletmanage objManageUser = new BLWalletmanage();
        //        objManageUser.parmUserName = AppSessions.SessionLoginId;
        //        objManageUser.Thmesapply = "~/CSS/Default-CSS/Inner-pages/innerStyleYellowOne.css";




        //        short retVal = objManageUser.Updatethems();


        //        if (retVal == 1)
        //        {



        //            Response.Redirect("../CommonPages/Home.aspx");


        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        //Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.WriteExceptionLog(ex);
        //        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        //    }


        //}
        ////private void ShowNotification()
        ////{
        ////    // spnMessage.InnerText = string.Empty;
        ////    BLCommon ReportDetails = new BLCommon();
        ////    try
        ////    {
        ////        ReportDetails.LoginId = AppSessions.SessionLoginId;


        ////        DataTable dtCompany = ReportDetails.ShownotifactionAllert();
        ////        if (dtCompany.Rows.Count > 0)
        ////        {
        ////            gvShowDetails.PageSize = PortalCommon.GetGridPageSize;
        ////            gvShowDetails.DataSource = dtCompany;
        ////            gvShowDetails.DataBind();

        ////        }

        ////        else
        ////        {
        ////            //MSG1001 - No record(s) found..
        ////            gvShowDetails.DataSource = null;
        ////            gvShowDetails.DataBind();
        ////            spnMessage.InnerText = Common.GetMessageFromXMLFile("MSG1001");
        ////            spnMessage.Attributes.Add("class", Constants.MessageCSS);
        ////            return;
        ////        }
        ////    }
        ////    catch (SqlException sqlEx)
        ////    {
        ////        Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        ////        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Common.WriteExceptionLog(ex);
        ////        spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        ////    }
        ////    finally
        ////    {
        ////        ReportDetails = null;
        ////    }

        ////}

        ////protected void create_project_ServerClick(object sender, EventArgs e)
        ////{
        ////    try
        ////    {

        ////        ModalPopupExtender3.Show();
        ////        ShowNotification();



        ////    }
        ////    catch (SqlException sqlEx)
        ////    {
        ////        Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
        ////        //spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
        ////    }
        ////}

        public static string getConnectionString()
        {
            string ConnectionString = string.Empty;
            return ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

    }
}