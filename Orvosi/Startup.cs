using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Orvosi.Startup))]
namespace Orvosi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
