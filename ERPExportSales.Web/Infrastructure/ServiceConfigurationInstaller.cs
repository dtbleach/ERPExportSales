using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ERPExportSales.Configuration;

namespace ERPExportSales.Web.Infrastructure
{
	public class ServiceConfigurationInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IExportSalesConfiguration>().ImplementedBy<ExportSalesConfiguration>().LifestyleSingleton());
		}

		#endregion
	}
}