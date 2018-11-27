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

        public string Encryption64Key
        {
            get { return "JKHk387689##21MMssx3"; }
        }

        public string OrderFolderName
        {
            get { return "订单"; }
        }

        public string InvoiceFolderName
        {
            get
            {
               return "发票";
            }
        }

        public string PackingFolderName
        {
            get
            {
                return "装箱单";
            }
        }

        public string BLFolderName
        {
            get
            {
                return "提单";
            }
        }

        public string QRFolderName
        {
            get
            {
                return "质保书";
            }
        }
    }
}
