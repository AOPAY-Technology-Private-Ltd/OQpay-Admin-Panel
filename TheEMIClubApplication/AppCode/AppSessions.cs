using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.AppCode
{
    public class AppSessions
    {

        #region private fields

        #endregion private fields

        #region public properties

        #region User Specific Sessions    

        /// <summary>
        /// Property to get, set Session["LoginId"] and
        /// stores user's LoginId.
        /// </summary>
        public static string SessionLoginId
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["LoginId"], null)) ?
                    HttpContext.Current.Session["LoginId"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["LoginId"] = value; }
        }

        public static string companycode
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["ClientCode"], null)) ?
                    HttpContext.Current.Session["ClientCode"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["ClientCode"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserFullName"]        
        /// </summary>
        public static string SessionUserFullName
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserFullName"], null)) ?
                    HttpContext.Current.Session["UserFullName"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserFullName"] = value; }
        }

        ///// <summary>
        ///// Property to get, set Session["UserRoleCode"] and
        ///// stores user's RoleCode.         
        ///// </summary>        
        public static string SessionUserRoleCode
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserRoleCode"], null)) ?
                    HttpContext.Current.Session["UserRoleCode"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserRoleCode"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserEmailAddress"]    
        /// stores user's EmailAddress.
        /// </summary>    
        public static string SessionUserEmailAddress
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserEmailAddress"], null)) ?
                    HttpContext.Current.Session["UserEmailAddress"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserEmailAddress"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserSessionId"]        
        /// </summary>  
        /// 

        /// <summary>
        /// Property to get, set Session["UserAddress"]    
        /// stores user's Address,City,State,Country & Pincode.
        /// </summary>    
        public static string SessionUserAddress
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["Address"], null)) ?
                    HttpContext.Current.Session["Address"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["Address"] = value; }
        }
        public static string SessionUserCity
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["City"], null)) ?
                    HttpContext.Current.Session["City"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["City"] = value; }
        }
        public static string SessionUserState
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["State"], null)) ?
                    HttpContext.Current.Session["State"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["State"] = value; }
        }
        public static string SessionUserCountry
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["country"], null)) ?
                    HttpContext.Current.Session["country"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["country"] = value; }
        }
        public static string SessionUserPinCode
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["Zip"], null)) ?
                    HttpContext.Current.Session["Zip"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["Zip"] = value; }
        }
        /// <summary>
        /// Property to get, set Session["UserSessionId"]        
        /// </summary>   
        public static string SessionUserSessionId
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserSessionId"], null)) ?
                    HttpContext.Current.Session["UserSessionId"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserSessionId"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["PreserveMyLogin_YN"]    
        /// stores user's preference (Y/N) to 'Preserve his/her Login'.
        /// PS: Will be used ONLY in case to 'Preserve user's Login'.
        /// </summary>    
        public static string SessionPreserveMyLogin_YN
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["PreserveMyLogin_YN"], null)) ?
                    HttpContext.Current.Session["PreserveMyLogin_YN"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["PreserveMyLogin_YN"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserBranchCode"]    
        /// stores user's BranchCode.
        /// </summary>
        public static string SessionUserBranchCode
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserBranchCode"], null)) ?
                    HttpContext.Current.Session["UserBranchCode"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserBranchCode"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserBranchName"]    
        /// stores user's BranchName.
        /// </summary>
        public static string SessionUserBranchName
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserBranchName"], null)) ?
                    HttpContext.Current.Session["UserBranchName"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserBranchName"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserDataScope"]    
        /// stores user's DataScope.
        /// </summary>
        public static string SessionUserDataScope
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserDataScope"], null)) ?
                    HttpContext.Current.Session["UserDataScope"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserDataScope"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["UserDepartmentCode"]    
        /// stores user's DepartmentCode.
        /// </summary>
        public static string SessionUserDepartmentCode
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["UserDepartmentCode"], null)) ?
                    HttpContext.Current.Session["UserDepartmentCode"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["UserDepartmentCode"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["RedirectToChangePswd"]    
        /// stores user's RedirectToChangePswd_YN flag.
        /// </summary>
        public static string SessionRedirectToChangePswd
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["RedirectToChangePswd"], null)) ?
                    HttpContext.Current.Session["RedirectToChangePswd"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["RedirectToChangePswd"] = value; }
        }

        #endregion User Specific Sessions

        #region Get Machine Details

        /// <summary>
        /// Property to get, set Session["RemoteHostIP"]    
        /// stores user's RemoteHostIP.
        /// </summary>
        public static string SessionRemoteHostIP
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["RemoteHostIP"], null)) ?
                    HttpContext.Current.Session["RemoteHostIP"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["RemoteHostIP"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["RemoteHostName"]    
        /// stores user's RemoteHostName.
        /// </summary>
        public static string SessionRemoteHostName
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["RemoteHostName"], null)) ?
                    HttpContext.Current.Session["RemoteHostName"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["RemoteHostName"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["LocalHostIP"]    
        /// stores user's LocalHostIP.
        /// </summary>
        public static string SessionLocalHostIP
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["LocalHostIP"], null)) ?
                    HttpContext.Current.Session["LocalHostIP"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["LocalHostIP"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["LocalHostName"]    
        /// stores user's LocalHostName.
        /// </summary>
        public static string SessionLocalHostName
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["LocalHostName"], null)) ?
                    HttpContext.Current.Session["LocalHostName"].ToString() : string.Empty;
            }
            set { HttpContext.Current.Session["LocalHostName"] = value; }
        }

        #endregion Get Machine Details

        #region Heavy Data Objects

        ///// <summary>
        ///// Property to get, set Session["ArrNavSeqList"]  
        ///// This Session is used to manage the Navigation Sequence in an Array List.
        ///// Further, used for 'Back' button functionality.
        ///// </summary>
        //public static ArrayList SessionArrNavSeqList
        //{
        //    get
        //    {
        //        return (!Equals(HttpContext.Current.Session["ArrNavSeqList"], null)) ?
        //            (ArrayList)HttpContext.Current.Session["ArrNavSeqList"] : new ArrayList();
        //    }
        //    set { HttpContext.Current.Session["ArrNavSeqList"] = value; }
        //}

        /// <summary>
        /// Property to get, set Session["AppMenuData"]  
        /// This Session is used to store user's menu data.    
        /// </summary>
        public static DataSet SessionAppMenuData
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["AppMenuData"], null)) ?
                    (DataSet)HttpContext.Current.Session["AppMenuData"] : new DataSet();
            }
            set { HttpContext.Current.Session["AppMenuData"] = value; }
        }

        /// <summary>
        /// Property to get, set Session["SearchParameters"]  
        /// This Session is used to store page level search parameters.
        /// </summary>
        public static Hashtable SessionSearchParameters
        {
            get
            {
                return (!Equals(HttpContext.Current.Session["SearchParameters"], null)) ?
                    (Hashtable)HttpContext.Current.Session["SearchParameters"] : new Hashtable();
            }
            set { HttpContext.Current.Session["SearchParameters"] = value; }
        }

        #endregion Heavy Data Objects

        #endregion public properties

        #region public method(s)

        #region Constructor and Destructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AppSessions()
        {
            //
            // TODO: Add constructor logic here
            //            
        }

        /// <summary>
        /// Destructor of class
        /// </summary>
        ~AppSessions()
        { }

        #endregion Constructor and Destructor

        #region RemoveAllSessions

        /// <summary>
        /// Abandon all sessions.
        /// </summary>
        public static void RemoveAllSessions()
        {
            try
            {
                string MM = HttpContext.Current.Request.ApplicationPath;

                //new PortalCommon().InsertActivityLog(AppSessions.SessionLoginId, Constants.Activity_Logout);
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.RemoveAll();

                //HttpContext.Current.Response.Write("<script language=javascript>window.open('" +
                //    HttpContext.Current.Request.ApplicationPath + "/" + Constants.Path_LoginPage + "?edom=tougol','_parent',replace=true);</script>");

                //HttpContext.Current.Response.Write("<script language=javascript>window.open('" +
                // HttpContext.Current.Request.PhysicalApplicationPath + "/" + Constants.Path_LoginPage + "?edom=tougol','_parent',replace=true);</script>");



            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }

        #endregion RemoveAllSessions

        #endregion public method(s)
    }
}