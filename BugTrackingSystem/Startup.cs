using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTrackingSystem.Startup))]
namespace BugTrackingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
