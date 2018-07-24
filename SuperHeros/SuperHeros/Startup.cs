using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperHeros.Startup))]
namespace SuperHeros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
