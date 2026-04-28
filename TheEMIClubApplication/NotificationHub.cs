using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheEMIClubApplication.Model;

namespace TheEMIClubApplication
{
    public class NotificationHub : Hub
    {
        public void SendNotification(List<FollowUpNotification> notifications)
        {
            Clients.All.receiveNotification(notifications);
        }
    }
}