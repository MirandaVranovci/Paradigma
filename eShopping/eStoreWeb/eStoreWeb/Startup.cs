using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eStoreWeb.Startup))]
namespace eStoreWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
