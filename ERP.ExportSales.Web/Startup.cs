using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP.ExportSales.Web.Startup))]
namespace ERP.ExportSales.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
