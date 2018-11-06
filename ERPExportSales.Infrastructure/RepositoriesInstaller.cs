using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;


namespace ERPExportSales.Infrastructure
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //container.Register(Component.For<IUnitOfWork>()
            //     .ImplementedBy<OpsPortalUnitOfWork>()
            //     .LifestylePerWebRequest());
        }
    }
}