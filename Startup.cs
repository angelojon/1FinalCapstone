using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1FinalCapstone.Startup))]
namespace _1FinalCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
