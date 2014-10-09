using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Mvc5DIFriendly.Startup))]

namespace Mvc5DIFriendly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
