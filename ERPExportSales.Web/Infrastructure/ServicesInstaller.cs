using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ERPExportSales.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Web.Infrastructure
{
    public class ServicesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<IExportSalesUserService>()
                      .ImplementedBy<ExportSalesUserService>()
                      .LifestylePerWebRequest());

            container.Register(Component.For<IEmployeeService>()
                     .ImplementedBy<EmployeeService>()
                     .LifestylePerWebRequest());

            container.Register(Component.For<IExportSalesLoginTokenService>()
                .ImplementedBy<ExportSalesLoginTokenService>()
                .LifestylePerWebRequest());

            container.Register(Component.For<IExportSalesService>()
             .ImplementedBy<ExportSalesService>()
             .LifestylePerWebRequest());

        }
    }
}
