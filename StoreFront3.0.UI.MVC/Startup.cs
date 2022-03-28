using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreFront3._0.UI.MVC.Startup))]
namespace StoreFront3._0.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
