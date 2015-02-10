using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WM.AspNetMvc.Startup))]
namespace WM.AspNetMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
