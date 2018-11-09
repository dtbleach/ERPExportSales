using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public interface IExportSalesLoginTokenService
    {
        void SaveLoginToken(ExportSalesLoginToken model);

        ExportSalesLoginToken GetExportSalesLoginToken(string username);
    }
}
