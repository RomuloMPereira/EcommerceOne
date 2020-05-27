using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Teste.Identity.Startup))]
namespace Teste.Identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
