using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CNNTwitterCrawler.Startup))]
namespace CNNTwitterCrawler
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
