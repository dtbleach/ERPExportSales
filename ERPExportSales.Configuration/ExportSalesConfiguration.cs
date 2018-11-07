using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Configuration
{
    public class ExportSalesConfiguration : IExportSalesConfiguration
    {
        private const string ExportSales_DB = "ERP4";
        public string ExportSalesDbConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[ExportSales_DB].ConnectionString;
            }
        }
    }
}
