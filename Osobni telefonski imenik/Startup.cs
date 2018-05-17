using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Osobni_telefonski_imenik.Startup))]
namespace Osobni_telefonski_imenik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
