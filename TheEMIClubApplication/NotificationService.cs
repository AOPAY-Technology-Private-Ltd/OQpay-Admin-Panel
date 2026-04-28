using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using TheEMIClubApplication;
using System.Linq;
using System.Web;
using TheEMIClubApplication.Model;

namespace TheEMIClubApplication
{
    public class NotificationService
    {


    public static void SendFollowUpNotifications()
    {
        List<FollowUpNotification> notifications = GetNotifications();

        if (notifications.Count > 0)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.receiveNotification(notifications);
        }
    }

    public static string getConnectionString()
    {
        return ConfigurationManager.AppSettings["ConnectionString"];
    }

        public static List<FollowUpNotification> GetNotifications()
        {
            List<FollowUpNotification> notifications = new List<FollowUpNotification>();
            string connectionString = getConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetLoanFollowUpNotificationForAdmin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var notification = new FollowUpNotification
                            {
                                LoanCode = reader["LoanCode"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerName = reader["CustomerName"].ToString(),
                                Products = reader["Products"].ToString(),
                                FollowUpRemarks = reader["FollowUpRemarks"].ToString(),
                                EMIAmount = reader["EMIAmount"] != DBNull.Value ? Convert.ToDecimal(reader["EMIAmount"]) : 0,
                                LoanTenureNo = reader["LoanTenureNo"] != DBNull.Value ? Convert.ToInt32(reader["LoanTenureNo"]) : 0,
                                FollowUpStatus = reader["FollowUpStatus"].ToString(),
                                //FollowUpID = reader["FollowUpID"] != DBNull.Value ? Convert.ToInt32(reader["FollowUpID"]) : 0,
                                //NextFollowUpDate = reader["NextFollowUpDate"] != DBNull.Value
                                //    ? Convert.ToDateTime(reader["NextFollowUpDate"]).ToString("dd-MMM-yyyy HH:mm")
                                //    : string.Empty
                            };

                            notifications.Add(notification);
                        }
                    }
                }
            }

            return notifications;
        }

    }
}