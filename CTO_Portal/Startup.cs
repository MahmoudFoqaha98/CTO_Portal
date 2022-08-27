using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CTO_Portal.Startup))]
namespace CTO_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
