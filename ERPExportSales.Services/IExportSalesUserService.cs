using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public interface IExportSalesUserService
    {
       BizResult<bool> Login(string loginName, string password);
    }
}
