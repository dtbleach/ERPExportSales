using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Configuration
{
    public interface IExportSalesConfiguration
    {
        string ExportSalesDbConnectionString { get; }
    }
}
