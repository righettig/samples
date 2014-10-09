using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc5DIFriendly_OnlyMvc.Startup))]
namespace Mvc5DIFriendly_OnlyMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
