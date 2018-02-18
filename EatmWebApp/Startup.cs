using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EatmWebApp.Startup))]
namespace EatmWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
