using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BM.Web.Settings.Startup))]
namespace BM.Web.Settings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
