using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CC.OnlineShopping.WebApp.Startup))]
namespace CC.OnlineShopping.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
