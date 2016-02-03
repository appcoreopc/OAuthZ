
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OAuthZ.Startup))]

namespace OAuthZ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app);
            ConfigureOAuth(app);
        }
    }
}
