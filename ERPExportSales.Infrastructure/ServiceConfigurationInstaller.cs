using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ERPExportSales.Infrastructure
{
	public class ServiceConfigurationInstaller : IWindsorInstaller
	{
		#region IWindsorInstaller
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//container.Register(Component.For<IOpsPortalConfiguration>().ImplementedBy<OpsPortalConfiguration>().LifestyleSingleton());
		}

		#endregion
	}
}