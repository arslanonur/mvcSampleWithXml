using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjSaki.Startup))]
namespace ProjSaki
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
