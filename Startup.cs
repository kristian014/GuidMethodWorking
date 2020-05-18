using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuidMethodWorking.Startup))]
namespace GuidMethodWorking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
