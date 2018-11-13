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

            container.Register(Component.For<IEmployeeRepository>()
               .ImplementedBy<EmployeeRepository>()
               .LifestyleTransient());

            container.Register(Component.For<IExportSalesLoginTokenRepository>()
              .ImplementedBy<ExportSalesLoginTokenRepository>()
              .LifestyleTransient());

            container.Register(Component.For<IVExportSalesOceanFreightRepository>()
              .ImplementedBy<VExportSalesOceanFreightRepository>()
              .LifestyleTransient());

            container.Register(Component.For<IVPublicHolidayRepository>()
                 .ImplementedBy<VPublicHolidayRepository>()
                 .LifestyleTransient());

        }
    }
}