using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.ApiLog.ErrorLog
{
    public class WriteLog
    {
        public void WriteErrorLogs(string message)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/LogResponse/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string logFile = Path.Combine(folderPath, "VPALog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                using (StreamWriter sw = new StreamWriter(logFile, true))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "] " + message);
                }
            }
            catch { }
        }

    }
}