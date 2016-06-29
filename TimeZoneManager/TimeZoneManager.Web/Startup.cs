using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeZoneManager.Web.Startup))]
namespace TimeZoneManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
