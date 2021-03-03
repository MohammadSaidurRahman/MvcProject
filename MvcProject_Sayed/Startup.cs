using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProject_Sayed.Startup))]
namespace MvcProject_Sayed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
