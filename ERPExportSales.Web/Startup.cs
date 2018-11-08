using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERPExportSales.Web.Startup))]
namespace ERPExportSales.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
