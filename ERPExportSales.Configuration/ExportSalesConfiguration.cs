using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Configuration
{
    public class ExportSalesConfiguration : IExportSalesConfiguration
    {
        private const string ExportSales_DB = "";
        public string ExportSalesDbConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
