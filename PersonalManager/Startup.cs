using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalManager.Startup))]
namespace PersonalManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
