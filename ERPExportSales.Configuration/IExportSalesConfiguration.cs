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

        string Encryption64Key { get; }

        string OrderFolderName { get; }

        string InvoiceFolderName { get; }

        string PackingFolderName { get;}

        string BLFolderName { get; }

        string QRFolderName { get; }
    }
}
