using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUD_MVC.Startup))]
namespace CRUD_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
