using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrystalCarCare.Startup))]
namespace CrystalCarCare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
