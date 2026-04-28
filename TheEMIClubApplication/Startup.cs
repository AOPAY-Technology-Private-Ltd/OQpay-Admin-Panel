using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(TheEMIClubApplication.Startup))]

namespace TheEMIClubApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Map SignalR
            app.MapSignalR();
        }
    }
}
