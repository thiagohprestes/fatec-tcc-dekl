using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DEKL.CP.UI.Startup))]

namespace DEKL.CP.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) => ConfigureAuth(app);
    }
}