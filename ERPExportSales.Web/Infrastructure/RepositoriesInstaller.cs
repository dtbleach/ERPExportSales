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

            container.Register(Component.For<IIPWhiteListRepository>()
                .ImplementedBy<IPWhiteListRepository>()
                .LifestyleTransient());

            container.Register(Component.For<IOrderRepository>()
              .ImplementedBy<OrderRepository>()
              .LifestyleTransient());

            container.Register(Component.For<ICustomerRepository>()
                .ImplementedBy<CustomerRepository>()
                .LifestyleTransient());

            container.Register(Component.For<IUSDCNYRepository>()
               .ImplementedBy<USDCNYRepository>()
               .LifestyleTransient());

            container.Register(Component.For<IQ195Repository>()
               .ImplementedBy<Q195Repository>()
               .LifestyleTransient());

        }
    }
}