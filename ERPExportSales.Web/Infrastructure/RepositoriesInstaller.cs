using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ERPExportSales.Repositories;

namespace ERPExportSales.Web.Infrastructure
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<IUnitOfWork>()
                      .ImplementedBy<ERPExportSalesUnitOfWork>()
                      .LifestylePerWebRequest());

            container.Register(Component.For<IDatabaseFactory>()
                .ImplementedBy<ERPExportSalesDatabaseFactory>()
                .LifestylePerWebRequest());

        }
    }
}