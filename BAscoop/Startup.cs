using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BAscoop.Startup))]
namespace BAscoop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
