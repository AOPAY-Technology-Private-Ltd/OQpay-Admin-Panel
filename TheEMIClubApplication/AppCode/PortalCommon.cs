using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AVFramework;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TheEMIClubApplication.BussinessLayer;
using System.Net.Mail;
using System.Net;
using Telerik.Web.UI;
using System.Security.Cryptography;

namespace TheEMIClubApplication.AppCode
{
    public class PortalCommon
    { 
    string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
    string FromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();
    string ccid = ConfigurationManager.AppSettings["ccid"].ToString();

    #region Global Variables

    //DBTask objDBTask = new DBTask();    
    private static byte[] IV_192 = new byte[] {
        0x37, 0x67, 0xf6, 0x4f, 0x24, 0x63, 0xa7, 3, 0x2a, 5, 0x3e, 0x53, 0xb8, 7, 0xd1, 13,
        0x91, 0x17, 200, 0x3a, 0xad, 10, 0x79, 0xde
     };
    private static byte[] KEY_192 = new byte[] {
        0x2a, 0x10, 0x5d, 0x9c, 0x4e, 4, 0xda, 0x20, 15, 0xa7, 0x2c, 80, 0x1a, 250, 0x9b, 0x70,
        2, 0x5e, 11, 0xcc, 0x77, 0x23, 0xb8, 0xc5
     };

    #endregion Global Variables

    #region Constructor
    public PortalCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion Constructor

    #region Enums

    /// <summary>
    /// Enum to specify message category, to prefix image accordingly in the message text.
    /// </summary>
    private enum MessageCategory
    {
        Information = 1,
        Validation = 2,
        Error = 3,
        Information1 = 4,
    }
    /// <summary>
    /// Specify page's position on which to display this message, whether this page is located at ROOT or it is an INNER page.
    /// </summary>
    public enum PageLevel
    {
        Root = 0,
        Inner = 1,
        Inner_2 = 2,
        Inner_3 = 3,
        Inner_4 = 4,
        Inner_5 = 5
    }

    #endregion Enums

    #region Global Properties

    /// <summary>
    /// Property to get GridPageSize from web.config
    /// </summary>
    public static int GetGridPageSize
    {
        get
        {
            try
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["GridPageSize"]))
                {
                    return 10; //default
                }
                else if (Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]) <= 0)
                {
                    return 10; //default
                }
                else
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
                }
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
    }

    #endregion Global Properties

    #region GetValueFromConfigFile

    /// <summary>
    /// Method to get value of given key from web.config file.
    /// </summary>
    /// <param name="keyName">Pass the name of Key (present in web.config)</param>
    /// <returns></returns>
    public static object GetValueFromWebConfigFile(string keyName)
    {
        try
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[keyName]))
            {
                return string.Empty; //default
            }
            else
            {
                return (object)ConfigurationManager.AppSettings[keyName];
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion GetConfigParamValue

    #region GetPathPrefix

    /// <summary>
    /// Method to compute path prefix as Root, ../, ../../, etc 
    /// on the basis os page level wrt to Root of application.
    /// </summary>
    /// <param name="pgLevel">Pass page level, from where you are calling this function.</param>
    /// <returns></returns>
    private static string GetPathPrefix(PageLevel pgLevel)
    {
        string pathPrefix = string.Empty;
        try
        {
            switch (pgLevel)
            {
                case PageLevel.Root:
                    pathPrefix = string.Empty;
                    break;
                case PageLevel.Inner:
                    pathPrefix = "../"; //Level 1
                    break;
                case PageLevel.Inner_2:
                    pathPrefix = "../../"; //Level 2
                    break;
                case PageLevel.Inner_3:
                    pathPrefix = "../../../"; //Level 3
                    break;
                case PageLevel.Inner_4:
                    pathPrefix = "../../../../"; //Level 4
                    break;
                case PageLevel.Inner_5:
                    pathPrefix = "../../../../../"; //Level 5
                    break;
                default:
                    pathPrefix = string.Empty;
                    break;
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return pathPrefix;
    }

    #endregion GetPathPrefix

    #region GetMessageWithImage

    /// <summary>
    /// Method to get message from Message XML file with message specific image and text color.
    /// </summary>
    /// <param name="pageLevel">Pass page's level, whether it is at ROOT, Inner OR Inner's Inner page.
    /// This will support 5 Levels of Inner page(s).</param>
    /// <param name="messageId">Pass messageId i.e. defined in MessageXML file.</param>
    /// <returns>Message text with image.</returns>
    public static string GetMessageWithImage(PageLevel pageLevel, string messageId)
    {
        string retValue = string.Empty;
        try
        {
            string imagePath = string.Empty; //To be defined by MessageCategory.
            string imageTagPrefix = string.Empty; //Image path for ROOT or INNER page(s).
            string finalMsgText = string.Empty;
            string xmlMessageText = Common.GetMessageFromXMLFile(messageId);
            MessageCategory msgCategory = 0;

            //Find Page level to prepare Image tag.
            imageTagPrefix = GetPathPrefix(pageLevel);

            //Determine Message Category from messageId.
            if (messageId.StartsWith("INF")) //Information
            {
                msgCategory = MessageCategory.Information;
            }
            if (messageId.StartsWith("VAL")) //Validation
            {
                msgCategory = MessageCategory.Validation;
            }
            if (messageId.StartsWith("ERR")) //Error
            {
                msgCategory = MessageCategory.Error;
            }

            //Use msgCategory to prepare complete Message with respective image and text color.
            switch (msgCategory)
            {
                case MessageCategory.Information:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1001") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Green'>" + xmlMessageText + "</span>";
                    break;
                case MessageCategory.Validation:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1002") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Orange'>" + xmlMessageText + "</span>";
                    break;
                case MessageCategory.Error:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1003") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Red'>" + xmlMessageText + "</span>";
                    break;
                default:
                    imagePath = string.Empty;
                    finalMsgText = "<span class='BigText'>" + xmlMessageText + "</span>";
                    break;
            }
            //retValue = "<div style='float:left; width:3%;'>" +  imagePath + "</div>" +  "<div style='float:right; width:96%;'>" + finalMsgText + "</div>";
            retValue = "<div style='float:left; width:4%;'>" + imagePath + "</div>" + "<div style='float:right; width:96%; text-align:left'>" + finalMsgText + "</div>";
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    /// <summary>
    /// Method to get message from Message XML file with message specific image and text color.
    /// This method will accept message parameters along with messageId.
    /// </summary>
    /// <param name="pageLevel">Pass page's level, whether it is at ROOT, Inner OR Inner's Inner page.
    /// This will support 5 Levels of Inner page(s).</param>
    /// <param name="messageParams">Pass key (messageId) of message with its parameter(s) (if any).</param>
    /// <returns></returns>
    public static string GetMessageWithImage(PageLevel pageLevel, params string[] messageParams)
    {
        string retValue = string.Empty;
        try
        {
            string imagePath = string.Empty; //To be defined by MessageCategory.
            string imageTagPrefix = string.Empty; //Image path for ROOT or INNER page(s).
            string finalMsgText = string.Empty;
            string messageId = messageParams[0];
            string xmlMessageText = Common.GetMessageByParameters(Common.ApplicationType.WEB, messageParams);
            MessageCategory msgCategory = 0;

            //Find Page level to prepare Image tag.
            imageTagPrefix = GetPathPrefix(pageLevel);

            //Determine Message Category from messageParams[0] item, i.e. messageId.
            if (messageId.StartsWith("INF")) //Information
            {
                msgCategory = MessageCategory.Information;
            }
            if (messageId.StartsWith("VAL")) //Validation
            {
                msgCategory = MessageCategory.Validation;
            }
            if (messageId.StartsWith("ERR")) //Error
            {
                msgCategory = MessageCategory.Error;
            }

            if (messageId.StartsWith("WALL"))
            {
                msgCategory = MessageCategory.Information1;
            }

            //Use msgCategory to prepare complete Message with respective image and text color.
            switch (msgCategory)
            {
                case MessageCategory.Information:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1001") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Green'>" + xmlMessageText + "</span>";
                    break;

                case MessageCategory.Information1:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("WALL1001") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Green'>" + xmlMessageText + "</span>";
                    break;

                case MessageCategory.Validation:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1002") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Orange'>" + xmlMessageText + "</span>";
                    break;
                case MessageCategory.Error:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1003") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Red'>" + xmlMessageText + "</span>";
                    break;
                default:
                    imagePath = string.Empty;
                    finalMsgText = "<span class='BigText'>" + xmlMessageText + "</span>";
                    break;
            }
            retValue = imagePath + finalMsgText;
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    #endregion GetMessageWithImage

    public static string GetMessageWithImage_LoginPage(PageLevel pageLevel, string messageId)
    {
        string retValue = string.Empty;
        try
        {
            string imagePath = string.Empty; //To be defined by MessageCategory.
            string imageTagPrefix = string.Empty; //Image path for ROOT or INNER page(s).
            string finalMsgText = string.Empty;
            string xmlMessageText = Common.GetMessageFromXMLFile(messageId);
            MessageCategory msgCategory = 0;

            //Find Page level to prepare Image tag.
            imageTagPrefix = GetPathPrefix(pageLevel);

            //Determine Message Category from messageId.
            if (messageId.StartsWith("INF")) //Information
            {
                msgCategory = MessageCategory.Information;
            }
            if (messageId.StartsWith("VAL")) //Validation
            {
                msgCategory = MessageCategory.Validation;
            }
            if (messageId.StartsWith("ERR")) //Error
            {
                msgCategory = MessageCategory.Error;
            }

            //Use msgCategory to prepare complete Message with respective image and text color.
            switch (msgCategory)
            {
                case MessageCategory.Information:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1001") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Green'>" + xmlMessageText + "</span>";
                    break;
                case MessageCategory.Validation:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1002") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Orange'>" + xmlMessageText + "</span>";
                    break;
                case MessageCategory.Error:
                    imagePath = "<img src='" + imageTagPrefix + Common.GetMessageFromXMLFile("IMG1003") + "'/>&nbsp;";
                    finalMsgText = "<span class='BigText_Red'>" + xmlMessageText + "</span>";
                    break;
                default:
                    imagePath = string.Empty;
                    finalMsgText = "<span class='BigText'>" + xmlMessageText + "</span>";
                    break;
            }
            //retValue = "<div style='float:left; width:3%;'>" +  imagePath + "</div>" +  "<div style='float:right; width:96%;'>" + finalMsgText + "</div>";
            retValue = "<div style='float:left; width:3%;'>" + imagePath + "</div>" + "<div style='float:right; width:85%; text-align:left'>" + finalMsgText + "</div>";
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    #region ReadFile

    /// <summary>
    /// Method to read given file from specified folder and return its text.
    /// </summary>
    /// <param name="filePathName">Pass file's name-path with respect to your code's current location and ROOT folder.</param>
    /// <returns>File's data as string.</returns>
    public static StringBuilder ReadFile(string filePathName)
    {
        StringBuilder sbFileText = new StringBuilder();

        try
        {
            string fileFullPath = HttpContext.Current.Server.MapPath(filePathName);
            if (File.Exists(fileFullPath))
            {
                StreamReader strmReader = new StreamReader(fileFullPath);
                sbFileText.Append(strmReader.ReadToEnd());
                strmReader.Close();


            }
            else
            {
                throw new Exception(Common.GetMessageByParameters(Common.ApplicationType.WEB, "ERR1003", filePathName, fileFullPath));
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return sbFileText;
    }

    #endregion ReadFile

    #region GetRandomEncryptedPassword

    /// <summary>
    /// Method to generate random password and then encrypt that password.
    /// This method will return two values, one 'Random Password' as out parameter
    /// and other is encrypted value of the 'Random Password' as its return value.
    /// </summary>
    /// <param name="newRandomPassword">Newly generated 'Random Password' as out parameter</param>
    /// <returns>Encrypted value of the 'Random Password'</returns>
    public static string GetRandomEncryptedPassword(out string newRandomPassword)
    {
        RandomValueGenerator objRVG = new RandomValueGenerator(false, true, true, true, 6, 10);
        newRandomPassword = objRVG.Create(); //New Random Password as Plain Text.
                                             //New Encryption Technique
        string newHashPassword = EncryptTripleDES(newRandomPassword);
        //Old Encryption Technique
        //string newHashPassword = CryptoUtil.GetHashEncryptedValue(newRandomPassword, Constants.EncryptionAlgo_SHA1); //"SHA1" | "MD5"
        return newHashPassword;
    }

    #endregion GetRandomEncryptedPassword

    #region GetRandomNumber

    /// <summary>
    /// Method to get Random n(5) digit Number.
    /// </summary>
    /// <returns>n digit number.</returns>
    private long GetRandomNumber()
    {
        try
        {
            //Get 5 digit number
            RandomValueGenerator objRVG = new RandomValueGenerator(false, true, false, false, 5, 5);
            long newNumber = Convert.ToInt64(objRVG.Create());
            return newNumber;
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion GetRandomNumber

    #region Get Machine IP/Host Name

    public static string GetRemoteHostIP()
    {
        string retValue = string.Empty;
        try
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) //Using proxy
            {
                retValue = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  //Returns real client IP.
            }
            else //Not using proxy OR can't get the client IP
            {
                //If it can't get the client IP, it will return proxy IP.
                retValue = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    public static string GetRemoteHostName()
    {
        string retValue = string.Empty;
        try
        {
            retValue = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    public static string GetLocalHostIP()
    {
        string retValue = string.Empty;
        try
        {
            retValue = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    public static string GetLocalHostName()
    {
        string retValue = string.Empty;
        try
        {
            retValue = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retValue;
    }

    #endregion Get Machine IP/Host Name

    #region Save Activity/Audit Log
    /// <summary>
    /// Method to save activity/audit log.
    /// This method is specific to post-login page(s) only.
    /// </summary>
    /// <param name="programCode">Pass Program Code as declared in Constants.</param>
    /// <param name="accessDenial_YN">Pass activity success or failed as Y/N.</param>
    /// <param name="taskModule">Pass activity name/module (if any).</param>
    /// <param name="taskDescription">Pass activity description (if any).</param>
    public static void SaveAuditLog_PostLogin(string programCode, string accessDenial_YN,
        string logCategory, string logAction, string taskModule, string taskDescription)
    {
        BLAuditLog objAuditLog = new BLAuditLog();
        try
        {
            objAuditLog.ApplicationCode = Constants.ApplicationCode;
            objAuditLog.ProgramCode = programCode;
            objAuditLog.LoginId = AppSessions.SessionLoginId;
            objAuditLog.UserSessionId = AppSessions.SessionUserSessionId;
            objAuditLog.AccessDenial = accessDenial_YN;
            objAuditLog.LogCategory = logCategory;
            objAuditLog.LogAction = logAction;
            objAuditLog.TaskModule = taskModule;
            objAuditLog.TaskDescription = taskDescription;

            objAuditLog.RemoteHostIP = AppSessions.SessionRemoteHostIP;
            objAuditLog.RemoteHostName = AppSessions.SessionRemoteHostName;
            objAuditLog.LocalHostIP = AppSessions.SessionLocalHostIP;
            objAuditLog.LocalHostName = AppSessions.SessionLocalHostName;

            objAuditLog.InsertAuditLog();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        finally
        {
            objAuditLog = null;
        }
    }

    /// <summary>
    /// Method to save activity/audit log.
    /// This method is specific to pre-login page(s) only.
    /// </summary>
    /// <param name="programCode">Pass Program Code as declared in Constants.</param>
    /// <param name="accessDenial_YN">Pass activity success or failed as Y/N.</param>
    /// <param name="taskModule">Pass activity name/module (if any).</param>
    /// <param name="taskDescription">Pass activity description (if any).</param>
    /// <param name="userName">Pass userName (if any).</param>    
    public static void SaveAuditLog_PreLogin(string programCode, string accessDenial_YN,
        string logCategory, string logAction, string taskModule, string taskDescription, string userName)
    {
        BLAuditLog objAuditLog = new BLAuditLog();
        try
        {
            objAuditLog.ApplicationCode = Constants.ApplicationCode;
            objAuditLog.ProgramCode = programCode;
            objAuditLog.LoginId = userName;
            objAuditLog.UserSessionId = AppSessions.SessionUserSessionId;
            objAuditLog.AccessDenial = accessDenial_YN;
            objAuditLog.LogCategory = logCategory;
            objAuditLog.LogAction = logAction;
            objAuditLog.TaskModule = taskModule;
            objAuditLog.TaskDescription = taskDescription;

            objAuditLog.RemoteHostIP = GetRemoteHostIP();
            objAuditLog.RemoteHostName = GetRemoteHostName();
            objAuditLog.LocalHostIP = GetLocalHostIP();
            objAuditLog.LocalHostName = GetLocalHostName();

            //objAuditLog.InsertAuditLog();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        finally
        {
            objAuditLog = null;
        }
    }

    #endregion Save Activity/Audit Log

    #region IsThisPageAccessibleToMe

    /// <summary>
    /// Method to check page accessibility for particular user.
    /// </summary>
    /// <param name="pageName">Pass Page name, e.g. Home.aspx.</param>
    /// <returns></returns>
    public static bool IsThisPageAccessibleToMe(string pageName)
    {
        bool retVal = false;
        try
        {
            if (AppSessions.SessionAppMenuData.Tables.Count > 0 &&
                AppSessions.SessionAppMenuData.Tables[0].Rows.Count > 0)
            {
                bool pageFound = false;
                for (int i = 0; i < AppSessions.SessionAppMenuData.Tables[0].Rows.Count; i++)
                {
                    string navigateURL = Convert.ToString(AppSessions.SessionAppMenuData.Tables[0].Rows[i]["NavigateURL"]);
                    if (navigateURL.Contains(pageName))
                    {
                        pageFound = true;
                        break;
                    }
                    else
                    {
                        pageFound = false;
                    }
                }
                if (pageFound)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            else
            {
                retVal = false;
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retVal;
    }

    /// <summary>
    /// Method to check page's accessibility to user, whether this page is accessible to user or not.
    /// </summary>
    /// <param name="pgLevel">Pass Page Level as Root|Inner...., from where you are calling this function.</param>
    public static void CheckPageAccessibility(PageLevel pgLevel)
    {
        try
        {
            bool isAccessible = IsThisPageAccessibleToMe(System.IO.Path.GetFileName(HttpContext.Current.Request.Path));
            if (!isAccessible)
            {
                //Write Activity Log Here
                //MSG1003 - AppSessions.SessionLoginId + " Tried to access the URL '" +
                ////        HttpContext.Current.Request.Path + "', which is not accessible to him."
                SaveAuditLog_PostLogin(DBNull.Value.ToString(), "N", string.Empty, string.Empty,
                    Constants.Activity_CheckPageAccessibility,
                    Common.GetMessageByParameters(Common.ApplicationType.WEB, "MSG1003", AppSessions.SessionLoginId, HttpContext.Current.Request.Path));

                string urlPrefix = string.Empty;
                //Find Page level to prepare urlPrefix.
                urlPrefix = GetPathPrefix(pgLevel);
                HttpContext.Current.Response.Redirect(urlPrefix + Constants.Path_UnAuthorizedPage);
            }
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion IsThisPageAccessibleToMe

    #region Check Site Under Maintenance

    /// <summary>
    /// Method to check, if site/application is under maintenance, then redirect to SiteUnderMaintenance page.
    /// </summary>
    /// <param name="pgLevel">Pass Page Level as Root|Inner...., from where you are calling this function.</param>
    public static void CheckSiteUnderMaintenance(PageLevel pgLevel)
    {
        try
        {
            //if (Equals(ConfigurationManager.AppSettings["SiteUnderMaintenance_YN"].ToUpper(), "Y"))
            //{
            //    string urlPrefix = string.Empty;
            //    //Find Page level to prepare urlPrefix.
            //    urlPrefix = GetPathPrefix(pgLevel);
            //    HttpContext.Current.Response.Redirect(urlPrefix + Constants.Path_SiteUnderMaintenancePage);
            //}
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion Check Site Under Maintenance

    #region Preserve My Login on this Machine

    #region Preserve Login Cookie

    /// <summary>
    /// Method to implement the functionality 'Preserve My Login on this Machine', 
    /// when user clicks the 'Preserve My Login' button on Home Page.
    /// </summary>    
    public static void PreserveLoginCookie()
    {
        try
        {
            WebApp.CookieName = Constants.PLoginCookieName; //AM_PreserveMyLogin (AppCode_PreserveMyLogin)

            WebApp.PreserveMyLogin_YN = AppSessions.SessionPreserveMyLogin_YN;
            WebApp.LoginId = AppSessions.SessionLoginId;
            WebApp.EmailAddress = AppSessions.SessionUserEmailAddress;

            //Will expires, when today's date advances to next day at 12 AM in the Night.
            WebApp.CookieExpiryHours = 24; //Default

            WebApp.PreserveLoginCookie();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion Preserve Login Cookie

    #region Retrieve Login Cookie Values

    /// <summary>
    /// Method to retrieve login cookie values (if they exist in client machine).
    /// </summary>
    public static bool RetrieveLoginCookieValues()
    {
        bool retVal = false;
        try
        {
            WebApp.CookieName = Constants.PLoginCookieName;

            if (WebApp.RetrieveLoginCookieValues())
            {
                AppSessions.SessionPreserveMyLogin_YN = WebApp.PreserveMyLogin_YN;
                AppSessions.SessionLoginId = WebApp.LoginId;
                AppSessions.SessionUserEmailAddress = WebApp.EmailAddress;
                retVal = true;
            }
            else
            {
                retVal = false;
            }


        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        return retVal;
    }

    #endregion Retrieve Login Cookie Values

    #region Remove Login Cookie Values

    /// <summary>
    /// Method to remove login cookie values.
    /// </summary>
    public static void RemoveLoginCookieValues()
    {
        try
        {
            WebApp.CookieName = Constants.PLoginCookieName;
            WebApp.RemoveLoginCookieValues();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
    }

    #endregion Remove Login Cookie Values

    #endregion Preserve My Login on this Machine

    #region ManageUserAppSessionLog

    /// <summary>
    /// Method to to manage user-application session log.
    /// </summary>
    /// <param name="programCode">Pass program code of the module.</param>
    public static void ManageUserAppSessionLog(string programCode)
    {
        BLAuditLog objAuditLog = new BLAuditLog();
        try
        {
            objAuditLog.LoginId = AppSessions.SessionLoginId;
            objAuditLog.ApplicationCode = Constants.ApplicationCode;
            objAuditLog.ProgramCode = programCode;

            objAuditLog.ManageUserAppSessionLog();
        }
        catch (ApplicationException appEx)
        {
            throw appEx;
        }
        finally
        {
            objAuditLog = null;
        }
    }

    #endregion ManageUserAppSessionLog

    #region GetFileUploadErrMessage
    /// <summary>
    /// method to get a error message from file upload control.
    /// </summary>
    /// <param name="errMessage">pass Error Message</param>
    /// <returns></returns>

    public static string GetFileUploadErrMessage(string errMessage)
    {
        try
        {
            string returnErrorMessage = string.Empty;
            returnErrorMessage = "<img src='" + "../" + Common.GetMessageFromXMLFile("IMG1003")
            + "'/>&nbsp;" + "<span class='" + Constants.MessageCSS + "'>" + errMessage + "</span>";
            return returnErrorMessage;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetFileUploadErrMessage

    #region GetMessageForFileUploadMaxSize
    /// <summary>
    /// Method to get a warning message for File Upload Maximum Size.
    /// </summary>
    /// <param name="webConfigKeyValue">Pass web config key Value</param>
    /// <returns>string</returns>
    public static string GetMessageForFileUploadMaxSize(string webConfigKeyValue)
    {
        try
        {
            string ImageMaxSize = string.Empty;
            ImageMaxSize = "Max. " + Convert.ToString(Convert.ToInt32(webConfigKeyValue) / 1024) + " KB allowed.";
            return ImageMaxSize;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetMessageForFileUploadMaxSize

    #region GetFileUploadToolTipMessage
    /// <summary>
    /// Method to get a message in ToolTip for File Upload Control for Allowed Extensions.
    /// </summary>
    /// <param name="webConfigKeyValue">Pass web config key Value</param>
    /// <returns></returns>

    public static string GetFileUploadToolTipMessage(string webConfigKeyValue)
    {
        try
        {
            string extensionMessage = string.Empty;
            //You can upload only .jpg OR .jpeg OR .gif OR .png OR .pdf file(s).
            //extensionMessage = "You can upload only " + webConfigKey.Replace("|", " OR ") + " file(s).";
            extensionMessage = Common.GetMessageByParameters(Common.ApplicationType.WEB, "MSG1009", webConfigKeyValue.Replace("|", " OR "));
            return extensionMessage;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetFileUploadToolTipMessage

    #region ExportToExcel

    /// <summary>
    /// Method to Export Data From DataTable to Excel.
    /// </summary>
    /// <param name="dtDataSource">Pass DataTable object as DataSource.</param>
    /// <param name="fileName">Pass the desired file name you want to export Data.</param>
    public static void ExportDataTableToExcel(DataTable dtDataSource, string fileName)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dtgExport = new DataGrid();
                    dtgExport.DataSource = dtDataSource;
                    dtgExport.DataBind();

                    dtgExport.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    /// <summary>
    /// Method to Export Data From HTML string (Like Back-end HTMLized Reports/Dashboards, etc.) to Excel.
    /// </summary>
    /// <param name="htmlString">Pass Back-end HTMLized string as DataSource.</param>
    /// <param name="fileName">Pass the desired file name you want to export Data.</param>
    public static void ExportHTMLDataToExcel(string htmlString, string fileName)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Clear();
            response.AddHeader("content-disposition", "attachment;filename=\"" + fileName + "\"");
            response.Charset = "";



            response.ContentType = "application/vnd.ms-excel";
            response.Write(htmlString);
            response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion ExportToExcel

    #region BindCityListStateWise

    public static void BindCityListStateWise(DropDownList ddlControlId, string stateCode, string selectedText)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.StateCode = stateCode;
            // objCommon.BindCityListStateWise(ddlControlId, selectedText);
        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion BindCityListStateWise

    #region GetValidationImage
    /// <summary>
    /// method to get a validation Header message with image.
    /// </summary>
    /// <param name="errMessage">pass Error Message</param>
    /// <returns></returns>

    public static string GetValidationImage(string errMessage)
    {
        try
        {
            string returnErrorMessage = string.Empty;
            returnErrorMessage = "<img  src='" + "../" + Common.GetMessageFromXMLFile("IMG1002")
                            + "'/>&nbsp;" + "<span class='" + Constants.MessageCSS + "'>" + errMessage + "</span>";
            return returnErrorMessage;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetValidationImage

    #region GetRandomValue

    /// <summary>
    /// Method to get Random n(5) digit Number.
    /// </summary>
    /// <returns>n digit number.</returns>
    public static long GetRandomValue()
    {
        try
        {
            //Get 5 digit number
            RandomValueGenerator objRVG = new RandomValueGenerator(false, true, false, false, 4, 5);
            long newNumber = Convert.ToInt64(objRVG.Create());
            return newNumber;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetRandomValue

    #region GetRandomValueGUID

    /// <summary>
    /// Method to get Random 8 letters used fro GUID : Chetna 09/06/16
    /// </summary>
    public static string GetRandomValueGUID()
    {
        try
        {
            //Get 5 digit number
            RandomValueGenerator objRVG = new RandomValueGenerator(false, true, true, false, 8, 8);
            string newGUID = Convert.ToString(objRVG.Create());
            return newGUID;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion GetRandomValue

    #region GetActive_YNList

    public static void GetActive_YNList(DropDownList ddlControlId)
    {
        BLCommon objCommon = new BLCommon();

        try
        {
            objCommon.LovCode = Constants.Active_YN; //"ACT"
            objCommon.GetLOVForYNFlags(ddlControlId);
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

    #endregion GetActive_YNList

    #region GetActive_YNList

    public static void GetActive_YNList(RadComboBox ddlControlId)
    {
        BLCommon objCommon = new BLCommon();

        try
        {
            objCommon.LovCode = Constants.Active_YN; //"ACT"
            objCommon.GetLOVForYNFlags(ddlControlId);
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

    #endregion GetActive_YNList

    #region GetRuleNConfigValue

    /// <summary>
    /// Method to get value of given Configurator from MstRuleNConfig_FLT Table.
    /// </summary>
    /// <param name="configuratorName">Pass Configurator Name.</param>
    /// <param name="flagType">Pass Flag Type</param>
    /// <returns>An object type Value.</returns>
    public static object GetRuleNConfigValue(string configuratorName, string flagType)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.ConfiguratorName = configuratorName;
            objCommon.FlagType = flagType;
            return objCommon.GetRuleNConfigValue();
        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion GetRuleNConfigValue


    #region BindDropDownList
    public static void BindDropDownList(DropDownList ddlControlId, string flag, string flag1, string flag2, string flag3, string flag4, string flag5, string selectedText)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.Flag = flag;
            objCommon.Flag1 = flag1;
            objCommon.Flag2 = flag2;
            objCommon.Flag3 = flag3;
            objCommon.Flag4 = flag4;
            objCommon.Flag5 = flag5;

            objCommon.BindDropDownList(ddlControlId, selectedText);

        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion BindCityListStateWise


    #region SendMailViaSMTP
    public static void SendMailViaSMTP(string ToEmail, string Subject, string body, Attachment attachments = null, string CC = null, string BCC = null)
    {
        try
        {

            string _BCC = "";
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(ToEmail));
            if (!string.IsNullOrWhiteSpace(CC))
            {
                mm.CC.Add(new MailAddress(CC));

            }
            if (!string.IsNullOrWhiteSpace(_BCC))
            {
                string[] bccid = _BCC.Split(',');

                foreach (string bccEmailId in bccid)
                {
                    mm.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id  
                }

            }
            mm.Subject = Subject;
            mm.Body = body;
            mm.IsBodyHtml = true;

            if (attachments != null)
                mm.Attachments.Add(attachments);

            mm.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());


            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "webmail.boscenter.in";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromPassword"].ToString());

            #region Show BOS product list to customer reset password mail only.
            if (Subject == "Reset Password Mail") //Show BOS product list to customer reset password mail only.
            {
                try
                {
                    var Boslogo = new Attachment(HttpContext.Current.Server.MapPath("~/Images/logoheader2.png"));
                    mm.Attachments.Add(Boslogo);
                    Boslogo.ContentId = "comLogo";
                    Boslogo.ContentDisposition.Inline = true;
                }
                catch (Exception exInner)
                {
                    //CommonUtility.logger.Error(ex.Message); 
                }

                try
                {
                    var facebook = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_facebook.png"));
                    mm.Attachments.Add(facebook);
                    facebook.ContentId = "facebook";
                    facebook.ContentDisposition.Inline = true;
                }
                catch (Exception exInner)
                {
                    //CommonUtility.logger.Error(ex.Message); 
                }


                try
                {
                    var linkedin = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_linkedin.png"));
                    mm.Attachments.Add(linkedin);
                    linkedin.ContentId = "linkedin";
                    linkedin.ContentDisposition.Inline = true;
                }
                catch (Exception exInner)
                {
                    //CommonUtility.logger.Error(ex.Message); 
                }

                try
                {
                    var youtube = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_youtube.png"));
                    mm.Attachments.Add(youtube);
                    youtube.ContentId = "Youtube";
                    youtube.ContentDisposition.Inline = true;
                }
                catch (Exception exInner)
                {
                    //CommonUtility.logger.Error(ex.Message); 
                }
                //try
                //{
                //    var AvaalLogo = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/more_products.png"));
                //    mm.Attachments.Add(AvaalLogo);
                //    AvaalLogo.ContentId = "prodLogo";
                //    AvaalLogo.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
            }
            else
            {
                #endregion Show bos product list to customer reset password mail only

                #region Welcome mail for ner user at the time of company creation                
                //try
                //{
                //    var BosLogo = new Attachment(HttpContext.Current.Server.MapPath("~/Images/logoheader2.png"));
                //    mm.Attachments.Add(BosLogo);
                //    BosLogo.ContentId = "comLogo";
                //    BosLogo.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var login = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/login.png"));
                //    mm.Attachments.Add(login);
                //    login.ContentId = "login";
                //    login.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var menu = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/menu.png"));
                //    mm.Attachments.Add(menu);
                //    menu.ContentId = "menu";
                //    menu.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var mobile = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/mobile_apps.png"));
                //    mm.Attachments.Add(mobile);
                //    mobile.ContentId = "mobile";
                //    mobile.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var emails = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/invoices_emails.png"));
                //    mm.Attachments.Add(emails);
                //    emails.ContentId = "emails";
                //    emails.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                //try
                //{
                //    var phone = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/phone.png"));
                //    mm.Attachments.Add(phone);
                //    phone.ContentId = "phone";
                //    phone.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var tutorial = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/tutorial.png"));
                //    mm.Attachments.Add(tutorial);
                //    tutorial.ContentId = "tutorial";
                //    tutorial.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var connect = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/connect.png"));
                //    mm.Attachments.Add(connect);
                //    connect.ContentId = "connect";
                //    connect.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                //try
                //{
                //    var appstore = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/app_store.png"));
                //    mm.Attachments.Add(appstore);
                //    appstore.ContentId = "appstore";
                //    appstore.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}
                //try
                //{
                //    var playstore = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/play_store.png"));
                //    mm.Attachments.Add(playstore);
                //    playstore.ContentId = "playstore";
                //    playstore.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                //try
                //{
                //    var facebook = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_facebook.png"));
                //    mm.Attachments.Add(facebook);
                //    facebook.ContentId = "facebook";
                //    facebook.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}


                //try
                //{
                //    var linkedin = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_linkedin.png"));
                //    mm.Attachments.Add(linkedin);
                //    linkedin.ContentId = "linkedin";
                //    linkedin.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                //try
                //{
                //    var youtube = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_youtube.png"));
                //    mm.Attachments.Add(youtube);
                //    youtube.ContentId = "Youtube";
                //    youtube.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                //try
                //{
                //    var googleplus = new Attachment(HttpContext.Current.Server.MapPath("~/Images/Email/icon_googleplus.png"));
                //    mm.Attachments.Add(googleplus);
                //    googleplus.ContentId = "googleplus";
                //    googleplus.ContentDisposition.Inline = true;
                //}
                //catch (Exception exInner)
                //{
                //    //CommonUtility.logger.Error(ex.Message); 
                //}

                #endregion Welcome mail for ner user at the time of company creation
            }
            #region Default images for every mail
            //try
            //{
            //    var AvaalLogo = new Attachment(HttpContext.Current.Server.MapPath("~/Images/logoheader2.png"));
            //    mm.Attachments.Add(AvaalLogo);
            //    AvaalLogo.ContentId = "comLogo";
            //    AvaalLogo.ContentDisposition.Inline = true;
            //}
            //catch (Exception exInner)
            //{
            //    //CommonUtility.logger.Error(ex.Message); 
            //}

            //try
            //{
            //    var Boslogo = new Attachment(HttpContext.Current.Server.MapPath("~/Images/logoname.png"));
            //    mm.Attachments.Add(Boslogo);
            //    Boslogo.ContentId = "BosLogo";
            //    Boslogo.ContentDisposition.Inline = true;
            //}
            //catch (Exception ex)
            //{
            //    //CommonUtility.logger.Error(ex.Message); 
            //}
            #endregion Default images for every mail

            //Below Code for send mail through avaal.com, Start, 27-Mar-2015

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.avaal.com";
            //smtp.Port = 25;
            //smtp.Credentials = new NetworkCredential("", "");
            //smtp.Credentials = new NetworkCredential("avaalIndia@gmail.com", "avaal@india14");

            ////Below Code for send mail through avaal.com, End, 27-Mar-2015
            //smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            smtp.Send(mm);
            mm = null;


        }
        catch (Exception ex)
        {

            //throw ex;
        }
    }

    #endregion SendMailViaSMTP


    #region BindCheckBoxList
    public static void BindCheckBoxList(CheckBoxList chkl, string flag, string flag1, string flag2, string flag3, string flag4, string flag5)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.Flag = flag;
            objCommon.Flag1 = flag1;
            objCommon.Flag2 = flag2;
            objCommon.Flag3 = flag3;
            objCommon.Flag4 = flag4;
            objCommon.Flag5 = flag5;

            objCommon.BindCheckBoxList(chkl);

        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion BindCheckBoxList


    #region GetParentMenuList
    public static void GetParentMenuList(CheckBoxList chkMenuList, string applicationCode)
    {
        BLCommon objCommon = new BLCommon();
        try
        {

            objCommon.GetParentMenuList(chkMenuList, applicationCode);

        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion GetParentMenuList

    //Naim Khan, 12-Oct-2022, Start
    #region BindDropDownList
    public static void BindComboList(RadComboBox ddlControlId, string flag, string flag1, string flag2, string flag3, string flag4, string flag5, string selectedText)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.Flag = flag;
            objCommon.Flag1 = flag1;
            objCommon.Flag2 = flag2;
            objCommon.Flag3 = flag3;
            objCommon.Flag4 = flag4;
            objCommon.Flag5 = flag5;

            objCommon.BindTelericComboList(ddlControlId, selectedText);

        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion BindCityListStateWise

    //Naim Khan, 12-Oct-2022, End

    //Naim Khan, 08-Jan-2015, Start
    #region BindRadioButtonList
    public static void BindRadioButtonList(RadioButtonList rblList, string flag, string flag1, string flag2, string flag3, string flag4, string flag5)
    {
        BLCommon objCommon = new BLCommon();
        try
        {
            objCommon.Flag = flag;
            objCommon.Flag1 = flag1;
            objCommon.Flag2 = flag2;
            objCommon.Flag3 = flag3;
            objCommon.Flag4 = flag4;
            objCommon.Flag5 = flag5;

            objCommon.BindRadioButtonList(rblList);

        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommon = null;
        }
    }

    #endregion BindRadioButtonList

    //Naim Khan, 08-Jan-2015, End

    #region Encryption/Decryption By Algo TRIPLEDES (New Encryption Technique)
    public static string HashStr(string Value)
    {
        MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        byte[] array = Encoding.ASCII.GetBytes(Value);
        array = mD5CryptoServiceProvider.ComputeHash(array);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2").ToLower());
        }
        return stringBuilder.ToString();
    }
    public static string EncryptTripleDES(string value)
    {
        string str = string.Empty;
        try
        {
            if (value.Length > 0)
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();

                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(stream2);
                writer.Write(value);
                writer.Flush();
                stream2.FlushFinalBlock();
                stream.Flush();
                str = Convert.ToBase64String(stream.GetBuffer(), 0, Convert.ToInt32(stream.Length));
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return str;
    }
    public static string DecryptTripleDES(string value)
    {
        string str = string.Empty;
        try
        {
            if (value.Length > 0)
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(value));
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
                str = new StreamReader(stream2).ReadToEnd();
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return str;
    }

    #endregion Encryption/Decryption By Algo TRIPLEDES


    //public void WalletCalculation()
    //{

    //}

}
}