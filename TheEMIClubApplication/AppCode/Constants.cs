using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.AppCode
{
    public class Constants
    {
        /*
        ---------------------------------------------------------------------------------------------------
             Created By      : Narendar Yadav
             Date            : 22-02-2024
             Description     : This class contains all constants used in the application.
                               No need to create object of this class. All constants will be accessible by class name.
        ---------------------------------------------------------------------------------------------------
        */

        public Constants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /*
        _______________________________________________________________________________________________________________

             Created By      : Praveen Yadav
             Date            : 25-07-2025
             Description     : Only flage related to Device dropdown
                               
        _______________________________________________________________________________________________________________
        */

        public const string Flage_BrandNameDDL = "BrandName";
        public const string Flage_ModelNameDDL = "ModelName";
        public const string Flage_VariantNameDDL = "VariantName";

        public const string EncryptionAlgo_SHA1 = "SHA1";

        public const string ApplicationCode = "AM";

        public const string ApplicationCode_AFMSuit = "AFM";
        public const string PLoginCookieName = "evreserpymnigol_tlf";

        //ASPX Page's Path
        public const string Path_LoginPage = "Login.aspx";
        public const string Path_ChangePswdPage = "CommonPages/ChangePassword.aspx";
        public const string Path_ForgotPswdPage = "CommonPages/ForgotPassword.aspx";
        public const string Path_HomePage = "CommonPages/Home.aspx";
        public const string Path_UnAuthorizedPage = "CommonPages/UnAuthorized.aspx";
        public const string Path_SiteUnderMaintenancePage = "CommonPages/SiteUnderMaintenance.aspx";

        public const string Path_OrderDetail = "CommonPages/OrderDetail.aspx";



        //CSS 
        public const string SelectedGridRowColor = "RowColor";
        public const string MessageCSS = "BigText_Red";

        //From Date Uses on Pop Calender
        public const string StartDateOfCalender = "01-Jan-2009";

        public const int StartDateOfCalender_DateOfBirth_AddYear = -60;
        public const int EndDateOfCalender_DateOfBirth_AddYear = -18;

        public const int StartDateOfCalender_DateOfRelieving_AddMonth = -6;
        public const int EndDateOfCalender_DateOfRelieving_AddMonth = 6;

        //For MasterCode 
        public const string MasterCode_AttendanceStatus = "EAD";
        public const string MasterCode_Gendor = "GEN";
        public const string LovFlag_ActiveStatus = "ACT";

        //Selected Value
        public const string SelectedValue_Gendor = "M";

        //DropDown Zero Index Text
        public const string DropDownZeroIndex_Text = "--Select--";

        //HTML Template/Page's Path
        public const string Path_TempltForgotPswd = "HTMLTemplates/ForgotPswdMail.htm";
        public const string Path_TempltLoginCredential = "HTMLTemplates/LoginCredential.html"; //Narendra Yadav, 
        public const string Path_TempltAFMSuitResetPassword = "HTMLTemplates/AFMSuitResetPassword.html"; //Narendra Yadav, 

        public const string Path_TempltAManagerLoginCredential = "HTMLTemplates/AManagerLoginCredential.html"; //Narendra Yadav, 
        public const string Path_TempltAManagerMaintenanceMail = "HTMLTemplates/MaintenanceMail.html"; //Narendra Yadav, 
        public const string Path_TempltAManagerChangePassword = "HTMLTemplates/ChangePassword.html"; //Narendra Yadav
        public const string Path_TempltAManagerCompletionMaintenanceMail = "HTMLTemplates/CompletionMaintenanceMail.html";//Narendra Yadav, 
        public const string Path_TempltAManagerMaintenanceOnHoldMail = "HTMLTemplates/MaintenanceOnHold.html";//Narendra Yadav, 10-oct-2016

        //Activity Name - Task Module
        public const string Activity_CheckPageAccessibility = "Check Page Accessibility Module";
        public const string Activity_Success = "Success";
        public const string Activity_Failed = "Failed";
        public const string Activity_Login = "Login";
        public const string Activity_Logout = "Logout";
        public const string Activity_ChangePswd = "Change Password";
        public const string Activity_ForgotPswd = "Forgot Password";
        public const string Activity_PreserveMyLogin = "Preserve My Login";

        //Log Action
        public const string LogAction_LoginPreserved = "Login Preserved";

        //Program Code - 6 chars only.
        public const string PrgCode_Login = "Log001";
        public const string PrgCode_ChangePswd = "CP001";
        public const string PrgCode_ForgotPswd = "FP001";
        public const string PrgCode_Logout = "Log002";
        public const string PrgCode_PreserveMyLogin = "PML001";
        public const string PrgCode_AutoLogin = "Log003"; //In Case of 'LoginPreserved'




        public const string DDL_AllOption = "ALL"; //Show All

        public const string Path_UserImages = "Images/UserImages";
        public const string Path_UserThumbImages = "Images/UserImages/ThumbnailImages";



        public const string Flag_ShowEmployee = "SHOW";
        public const string Flag_CreateEmployee = "INSERT";
        public const string Flag_UpdateEmployee = "UPDATE";
        public const string Flag_EmployeeDetail = "EMPDETAIL";



        public const string Flag_ActivateEmployee = "ACTIVATE";
        public const string Flag_DeactivateEmployee = "DEACTIVATE";


        //  public const string Image_Avatar = "Avatar.png";
        public const string ImageExtensions_Allowed = "AllowedImageExtensions";




        //  public const string EmpImage_HeightText = "userImageHeight";
        // public const string EmpImage_WidthText = "userImageWidth";

        //public const string EmpThumbImage_HeightText = "UserThumbImageHeight";
        //public const string EmpThumbImage_WidthText = "UserThumbImageWidth";

        public const string Image_SizeText = "UserImageSize";

        public const string ConfigName_FirstCharOfEmpCodeText = "AllowedFirstCharOfEmpCode";


        public const string Image_ThumbAvatar = "AvatarThumb.png";



        public const string LOVOrderStatus_ShowAllValue = "SHOW";
        public const string LOVOrderStatus_ShowOnlyConfirmValue = "CONFIRM";
        public const string LOVOrderStatus_ShowOnlyCancelValue = "CANCEL";




        public const string Report_SearchButtonText = "Go";

        public const string PageFlag = "P";
        public const string WaybillDetailReport_ExcelFileName = "WaybillDetailReport.xls";
        public const string PrgCode_CreateDummyOrder = "CDOD01";
        public const string PrgCode_UpdateDummyOrder = "UDOD01";
        public const string Flag_Said2Contain = "PR";

        public const string Flag_ActivateCity = "ACTIVATE";
        public const string Flag_DeactivateCity = "DEACTIVATE";

        public const string Flag_ShowState = "SHOW";
        public const string Flag_CreateState = "INSERT";
        public const string Flag_UpdateState = "UPDATE";


        // public const string Flag_CreateMode = "INSERT";
        // public const string Flag_UpdateMode = "UPDATE";
        public const string Flag_ShowBranch = "SHOW";
        public const string Caption_Update = "Update";
        public const string Caption_Cancel = "Cancel";

        public const string IsActive = "Activate?";
        public const string IsRejected = "Reject?";

        public const string Assign = "Assign";
        public const string Processing = "Processing";
        public const string Approved = "Approved";
        public const string Disbursement = "Disbursement";
        public const string Rejected = "Rejected";
        public const string Pending = "Pending";
        public const string Closed = "Closed";



        public const string PrgCode_ManageApplication = "APP001";
        public const string PrgCode_AlertConfig = "AC001";
        public const string PrgCode_MasterCode = "MC001";
        public const string PrgCode_External_Users = "ECU001";
        public const string PrgCode_Emp_Group = "EGL001";// Employee Group Link.
        public const string PrgCode_Data_Scope = "DS001"; // Dara Scope.
        public const string PrgCode_Menu_Master = "MM001";
        public const string PrgCode_Role_Master = "RM001";
        public const string PrgCode_Manage_User = "MU001";
        public const string PrgCode_Vehicle_Group_Link = "VGL001";
        public const string PrgCode_Vehicle_Group_Right_Link = "VGRL01";
        public const string PrgCode_Emp_Common_Link = "ECL001";

        public const string Insert_Mode = "INT";
        public const string Update_Mode = "UPT";


        public const string ManageRole_BtnUpdateMode_Text = "Update"; //Show All
        public const string Menu_Halt_Code = "MHC";
        public const string Application_Code = "APT";
        public const string Vehicle_Group = "EVG";

        public const string Alert_Type_Code = "ATC";
        public const string Alert_Category_Code = "ACC";
        public const string Alert_Content_Type = "ACT";
        public const string Alert_Sub_Category = "ASC";
        public const string Alert_User_Category = "AUC";
        public const string Alert_Group_Type = "AGT";

        public const string Activity_LogAction_Insert = "INSERT";
        public const string Activity_LogAction_Update = "UPDATE";
        public const string Activity_Update_Manage_User_Details = "Update Manage User Details";


        public const string Activity_Create_Application = "Create Application";
        public const string Activity_Update_Application = "Update Application";

        public const string Create_Application = "Create Application";
        public const string Update_Application = "Update Application";

        public const string ManageMenu_BtnCancelMode_Text = "Cancel";
        public const string Create_Menu = "Create Menu";
        public const string Update_Menu = "Update Menu";
        public const string Create_Role = "Create Role";
        public const string Update_Role = "Edit Role";
        public const string Create_api = "Create API/Service Master";

        public const string Path_ManageApplication = "MembershipPages/ManageApplication.aspx";
        public const string Active_YN = "ACT";//for Active_YN List.

        public const string Activity_Create_Role = "Create Role";
        public const string Activity_Update_Role = "Update Role";
        public const string Activity_Assign_Menus = "Assign Menu under a Role";
        public const string Path_ManageRole = "MembershipPages/ManageRole.aspx";
        public const string Path_CreateRole = "CreateRole.aspx";
        // public const string Activity_Update_Menu = "Update Menu";
        public const string DeactivateSuperRole_YN = "DeactivateSuperRole_YN";
        public const string TEXT = "TEXT";

        public const string Activity_Create_Menu = "Create Menu";

        public const string Activity_Update_Menu = "Update Menu";
        public const string Path_ManageMenu = "MembershipPages/ManageMenu.aspx";


        public const string Activity_Create_User = "Create User";
        public const string Activity_Assign_Applications = "Assign Applications";
        public const string Activity_Assign_Roles = "Assign Roles";
        public const string Activity_Update_Alert_Config_Act_Inact = "Update Alert Config (Act or Inact)";
        public const string Activity_Update_Application_Act_Inact = "Update Alert Config (Act or Inact)";
        public const string Activity_Update_Master_Code_Act_Inact = "Update MasterCode (Act or Inact)";
        public const string Activity_Update_User_Act_Inact = "Update User (Act or Inact)";
        public const string Activity_Update_Assigned_Applications = "Update Assigned Applications";
        public const string Activity_Update_Assigned_Roles = "Update Assigned Roles";
        public const string Activity_Reset_Password = "Reset Password";
        public const string Activity_Assign_Regions = "Assign Regions";
        public const string Activity_Update_Assign_Regions = "Update Assign Regions";
        public const string Activity_Assign_Zones = "Assign Zones";
        public const string Activity_Update_Assign_Zones = "Update Assign Zones";
        public const string Activity_Assign_Cities = "Assign Cities";
        public const string Activity_Update_Assign_Cities = "Update Assign Cities";
        public const string Activity_Assign_All = "Assign All";
        public const string Activity_Update_Assign_All = "Update Assign All";
        public const string Activity_Assign_Vehicles = "Assign Vehicles for a group";
        public const string Activity_Update_Assign_Vehicles = "Update Assign Vehicles";
        public const string Activity_Assign_Vehicles_To_User = "Assign Vehicles for a user";
        public const string Activity_Update_Assign_Vehicles_To_User = "Update Assign Vehicles for a user";

        public const string Activity_Update_Role_Act_Inact = "Update Role (Act or Inact)";
        public const string Activity_Update_Menu_Act_Inact = "Update Menu (Act or Inact)";
        public const string Activity_Assign_Users_To_CommonLink = "Assign Users to group by Common Link";
        public const string Activity_Update_Assigned_Users_To_CommonLink = "Update Assigned Users to group by Common Link";


        //Log Action

        public const string Path_TempltNewUserCreation = "HTMLTemplates/NewUserMail.htm";

        //Narendra Yadav, 22-Sep-2015, Start

        public const string PrgCode_CreateCompany = "CM0001";
        public const string PrgCode_ManageCompany = "MC0001";
        public const string Flag_CreateMode = "INSERT";
        public const string Flag_UpdateMode = "UPDATE";
        public const string Flag_ShowMode = "SHOW";
        public const string Flag_DetailMode = "DETAIL";
        public const string Flag_ActivateMode = "ACTIVATE";
        public const string Flag_DeactivateMode = "DEACTIVATE";
        public const string Flag_RenewMode = "RENEW";
        public const string Flag_RPASSWORD = "RPASSWORD";
        public const string Flag_Reschedule = "RESCHE";
        public const string Flag_StatusChange = "SATCHG";
        public const string Flag_DeleteCompany = "DELETECOMPANY"; //Narendra Yadav,



        public const string PrgCode_CreateCityPopup = "CCP001";

        public const string PrgCode_CreateZipPopup = "CZP001";

        public const string Popup_City = "CITYPP";
        public const string Popup_Zip = "CITYZIP";


        public const string PrgCode_ManageCompanyTypeCode = "MCT001";

        public const string PrgCode_ManageFeatureDetail = "MFD001";
        public const string PrgCode_ManageSubscriptionMenu = "MSM001";




        public const string MstFlag_CountryList = "COUNTRY";
        public const string MstFlag_Countryid = "2";

        public const string MstFlag_DemoCountryList = "DEMOCOUNTRY";

        public const string MstFlag_StateList = "STATE";

        public const string MstFlag_CityList = "CITY";

        public const string MstFlag_PostalCodeList = "PIN";

        public const string MstFlag_CompanyType = "CTYPE";

        public const string Path_CreateCompany = "CreateCompany.aspx";

        public const string Path_CreateCompany1 = "CreateCompanyN.aspx";  // Narendra Yadav

        public const string Path_EditLicense = "EditLicenseDetails.aspx";
        public const string Path_ViewDetails = "ViewDetails.aspx";
        public const string Path_ViewHistory = "ViewHistory.aspx";
        public const string Path_ManageLicenseActivation = "ManageLicenseActivation.aspx";

        public const string Path_LoginForSupport = "http://localhost:4130/Design/Login/Login.aspx"; //Narendra   

        public const string MasterCodeList_PaymentMode = "PMD";

        public const string MasterCodeList_PaymentType = "PMT";

        public const string MasterCodeList_BillingPeriod = "BLP";

        public const string MstFlag_MasterCode = "MCODE";

        public const string Path_ManageCompany = "ManageCompany.aspx";

        public const string Path_ManageCompanyType = "ManageSubscription.aspx";

        public const string Path_ManageCompanyTypeFromSMenu = "../MasterModules/ManageSubscription.aspx";

        public const string Path_CreateSubscription = "CreateSubscription.aspx";
        public const string Path_View_moreReports = "../Reports/TransactionReport.aspx";

        public const string Path_SubscriptionFeatureLink = "SubscriptionFeatureLink.aspx";

        public const string Path_CompanyFeatureLink = "CompanyFeatureLink.aspx";

        public const string Path_CreateFeature = "CreateFeature.aspx";

        public const string Path_SubscriptionMenuLink = "../MembershipPages/SubscriptionMenuLink.aspx";

        public const string MasterCodeList_FeatureType = "FTP";

        public const string PageRedirectMode_Insert = "I";
        public const string PageRedirectMode_Update = "U";
        public const string PageRedirectMode_Renew = "R";

        public const string PageRedirectMode_Reschedule = "RS";
        public const string PageRedirectMode_StatusChange = "SC";
        public const string PageRedirectMode_Resend = "RE";
        public const string PageRedirectMode_RemarkModify = "RM";
        public const string PageRedirectMode_RemarkDelete = "RD";

        public const string PrgCode_SubscriptionFeatureLinkDetail = "SFLD01";

        public const string MstFlag_CountryBasedFeatureList = "CBF";

        public const string PrgCode_CompanyFeatureValidityDetail = "CFVD01";

        public const string MstFlag_SubscriptionFeatureList = "SFLIST";

        public const string MstFlag_SubscriptionFeatureListSMSDirection = "SMSDIRECTION"; // Narendra Yadav, 17-June-2016

        public const string MstFlag_MenuFeatureList = "MFEATURE";

        public const string MstFlag_SubscriptionMenu = "SUBSMENU";

        public const string MstFlag_CurrenyList = "CURRENCY";

        public const string MstFlag_CurrenyListISO = "CURRENCYISO"; // Narendra Yadav, 09-June-2016

        public const string Flag_SupportCompany = "SUPPORTCOMPANY"; //Narendra Yadav, 09-June-2016

        public const string MstFlag_CurrenyByCountryID = "CByCountryID";


        public const string MstFlag_AllCompanyType = "ACTYPE"; // Narendra Yadav, 23-Nov-2015

        public const string Feature_IndependentWithValue = "FTP04"; // Narendra Yadav 03-Dec-2015
        public const string Feature_IndependentWithValue2 = "FTP01"; // Narendra Yadav 03-Dec-2015

        public const string Feature_MobileApp = "F0061";

        public const string Flag_IntegrateFeature = "IFEATURE"; //05-Dec-2015

        public const string Service_Twilio = "SE001"; //05-Dec-2015

        public const string Flag_HeadDetail = "HEADDETAIL"; // 09-Dec-2015

        public const string Flag_INSERTSID = "INSERTSID"; // 09-Dec-2015

        public const string Flag_UPDATESID = "UPDATESID"; // 09-Dec-2015

        public const string MasterCodeList_PhoneDivision = "DIV"; // 09-Dec-2015
        public const string MstFlag_CountryISDList = "COUNTRYISD"; // 11-Dec-2015

        public const string PrgCode_ManageMaintenanceDetail = "MMD001"; //14-Dec-2015
        public const string Path_ManageMaintenanceDetail = "ManageMaintenance.aspx";
        public const string Path_CreateMaintenanceDetail = "CreateWebsiteMaintenance.aspx";

        public const string MstFlag_ApplicationList = "APPLICATION"; // 21-Dec-2015

        public const string MstFlag_MenuScope = "MSCOPE"; // 21-Dec-2015


        public const string MstFlag_CompanyIPDetail = "CIP"; // Narendra Yadav, 08-Jan-2016

        public const string CompanyIPDetail_Restricted = "CIP02"; // Narendra Yadav, 11-Jan-2016
        public const string CompanyIPDetail_Universal = "CIP01"; // Narendra Yadav, 11-Jan-2016

        //Narendra Yadav, 12-Jan-2015, Start
        public const string Image_Avatar = "Avatar.png";
        //public const string ImageExtensions_Allowed = "AllowedImageExtensions";
        public const string PageName_AddEmployee = "AddEmployee.aspx";
        public const string PageName_ManageEmployee = "ManageEmployeeDetail.aspx";
        public const string ConfigName_PreviousDaysOfRelieving = "PreviousDaysCounterForRelieving";
        public const string ConfigName_NextDaysOfRelieving = "NextDaysCounterForRelieving";
        public const string ConfigValueDataType_Relieving = "DECIMAL";

        public const string EmpImage_HeightText = "userImageHeight";
        public const string EmpImage_WidthText = "userImageWidth";

        public const string EmpThumbImage_HeightText = "UserThumbImageHeight";
        public const string EmpThumbImage_WidthText = "UserThumbImageWidth";

        // public const string Image_SizeText = "UserImageSize";


        public const string MstFlag_DesignationList = "DESIG";
        public const string MstFlag_DepartmentList = "DEPART";

        public const string PrgCode_ManageEmployeeDetail = "MED001";

        public const string Path_ManageEmployee = "ManageEmployee.aspx";
        public const string Path_CreateEmployee = "CreateEmployee.aspx";



        //Narendra Yadav, 12-Jan-2015, End


        public const string MstFlag_CompanyCode = "COMPCODE"; //Narendra Yadav 02/Sept/2016 -- Show company names in checkboxlist for maintenance page
        public const string MstFlag_MaintenanceTypeStatus = "MTS"; //Narendra Yadav 21/Sept/2016 -- Show Maintenance Type Status in checkboxlist for maintenance page
                                                                   //Narendra Yadav, 06-Sept-2016
        public const string Flag_UpdateMail = "UPDATEMAIL";
        public const string Flag_Resend = "RESEND";
        public const string Flag_Delete = "DELETE";

        //Narendra Yadav, 15-Sept-2016
        public const string Path_ReleaseNote = "CreateReleaseNote.aspx";  // Narendra Yadav 15-Sept-2016
        public const string Path_ManageReleaseNote = "ReleaseNote.aspx"; //Narendra Yadav 15-Sept-2016
        public const string Flag_DeleteReleaseNote = "DELETE";
        public const string Flag_StatusReleaseNote = "STATUS";
        public const string MstFlag_ReleaseNote = "RELEASE"; // Narendra Yadav, 15-Sept-2016

        public const string MstFlag_Employee = "EMP"; // Narendra Yadav, 03-Nov-2016
        public const string MstFlag_ApplicationListDDL = "APPLIST"; // Narendra Yadav, 03-Nov-2016
        public const string MstFlag_EmployeeSalutation = "EMPSALU"; // Narendra Yadav, 03-Nov-2016
        public const string MstFlag_ParentMenu = "PARMENU"; // Narendra Yadav, 03-Nov-2016

        //Narendra yadav,24-08-2023

        public const string MstFlag_userforwallet = "USER";
        public const string MstFlag_Feature = "Feature";
        public const string MstFlag_userSetting = "SETTING";
        public const string MstFlag_PRODUCT = "PRODUCT";
        public const string MstFlag_Report = "Reports";
        public const string MstFlag_cantentcategory = "CantentCategory";
        public const string MstFlag_city = "Citys";
        public const string MstFlag_bankUser = "bankUser";
        public const string MstFlag_ReportUser = "ReportCust";
        




    }
}