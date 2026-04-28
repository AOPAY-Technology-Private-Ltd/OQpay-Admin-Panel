using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AVFramework;

namespace TheEMIClubApplication.BussinessLayer
{
    public class BLManageMail
    {

        #region Member Variables
        //Member declaration:         
        private string m_alertCode = string.Empty;
        private string m_emailBody = string.Empty;

        private string m_emailFlag = string.Empty;
        private string m_emailFlagValue = string.Empty;

        #endregion

        #region Property

        public string AlertCode { get { return m_alertCode; } set { m_alertCode = value; } }
        public string EmailBody { get { return m_emailBody; } set { m_emailBody = value; } }

        public string EmailFlag { get { return m_emailFlag; } set { m_emailFlag = value; } }
        public string EmailFlagValue { get { return m_emailFlagValue; } set { m_emailFlagValue = value; } }

        #endregion Property
    }
}