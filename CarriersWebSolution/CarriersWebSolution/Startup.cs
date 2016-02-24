using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarriersWebSolution.Startup))]
namespace CarriersWebSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
