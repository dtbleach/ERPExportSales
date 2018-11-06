using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace ERPExportSales.Infrastructure
{
    public class ApiControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
			container.Register(Classes.FromThisAssembly()
								.BasedOn<ApiController>()
								.LifestyleTransient());
		}
    }
}