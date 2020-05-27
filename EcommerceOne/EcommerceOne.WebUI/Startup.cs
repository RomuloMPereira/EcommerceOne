using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceOne.WebUI.Startup))]
namespace EcommerceOne.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
