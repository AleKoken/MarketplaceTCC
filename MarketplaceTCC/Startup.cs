using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketplaceTCC.Startup))]
namespace MarketplaceTCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
