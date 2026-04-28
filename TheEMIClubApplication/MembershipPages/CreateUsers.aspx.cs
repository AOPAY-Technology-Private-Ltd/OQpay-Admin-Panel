using AjaxControlToolkit;
using AVFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using TheEMIClubApplication.BussinessLayer;
using TheEMIClubApplication.AppCode;

using System.Collections;
using System.Data;

namespace TheEMIClubApplication.MembershipPages
{
    public partial class CreateUsers : System.Web.UI.Page
    {
        BLSignup objSignup = new BLSignup();
        BLManageUser objManageUser = new BLManageUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                  


                    if (Request.QueryString.HasKeys() && (!Equals(Request.QueryString["edocelor"], null)) &&
                           Request.QueryString["edocelor"].Trim().Length > 0)
                    {
                        //If coming from some other page.. redierct to that same page.
                        objManageUser.MobileNo = Request.QueryString["edocelor"].Trim();
                        objManageUser.MobileNo = CryptoUtil.DecryptTripleDES(objManageUser.MobileNo.Replace(" ", "+"));

                        GetuserDetail();
                    }
                }


            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }

        }
        protected void btnSave_click(object sender, EventArgs e)
        {
            try
            {
                {
                    //  spnMessage11.InnerText = string.Empty;

                    transferid=string.Empty;
                    objSignup.FirstName = txtfirstName.Text.Trim().ToUpper();
                    objSignup.LastName = txtLastname.Text.Trim().ToUpper();
                    objSignup.EmailID = txtEmailID.Text.Trim().ToUpper();
                    objSignup.PhoneNo = txtMobileNo.Text.Trim().ToUpper();
                    objSignup.Ipaddress = "";






                    objSignup.Password = CryptoUtil.EncryptTripleDES(txtPasword.Text.Trim().ToUpper());
                    string retValue = objSignup.CreateUser();
                    if (retValue.Trim().Equals("BOS-200"))
                    {
                        lastuserid();
                      
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + "user is created successfully with username:" + transferid + "');", true);


                        Common.ClearFields(this.Page, "0");
                  
                    }
                    else if (retValue.Trim().Equals("BOS-300")) // Fail to Creation
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowError('" +"Mobile no Or Email Id Allready Exists" + "');", true);

                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                // spnMessage11.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }
            catch (Exception ex)
            {
                Common.WriteExceptionLog(ex);
                //spnMessage11.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }

        }
        string transferid = "";
        private string lastuserid()
        {
         
            try
            {

                spnMessage.InnerText = string.Empty;
                //objCompany.Flag = Constants.Flag_DetailMode; //Flag_DetailMode - > DETAIL;
                Hashtable htFeatureDetail = objSignup.Reterivelastuser();
                if (htFeatureDetail.Count > 0)
                {
                    transferid = Convert.ToString(htFeatureDetail["UserCode"]).Trim();

                }
                else
                {

                }
            }
            catch (SqlException sqlEx)
            {
                Common.WriteSQLExceptionLog(sqlEx, Common.ApplicationType.WEB);
                spnMessage.InnerHtml = PortalCommon.GetMessageWithImage(PortalCommon.PageLevel.Inner, "ERR1007");
            }

            return transferid;
        }

        #region GetRoleDetail

        private void GetuserDetail()
        {
            Hashtable htManageUser = new Hashtable();
            try
            {
                objManageUser.UserName = "";
                objManageUser.EmailAddress = "";

                objManageUser.MobileNo = Request.QueryString["edocelor"].Trim();
                objManageUser.MobileNo = CryptoUtil.DecryptTripleDES(objManageUser.MobileNo.Replace(" ", "+"));
                objManageUser.Flag = "DETAIL";




                htManageUser=objManageUser.EditUserDetails();

                if (htManageUser.Count>0)
                {
                    txtfirstName.Text = Convert.ToString(htManageUser["firstName"]);
                    txtLastname.Text = Convert.ToString(htManageUser["lastName"]);
                    txtEmailID.Text = Convert.ToString(htManageUser["email"]);
                    txtMobileNo.Text = Convert.ToString(htManageUser["phone"]);
             
                 
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

        #endregion GetRoleDetail
    }
}