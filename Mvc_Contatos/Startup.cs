using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc_Contatos.Startup))]
namespace Mvc_Contatos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
