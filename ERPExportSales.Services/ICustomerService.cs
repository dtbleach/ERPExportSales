using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(string userName);
        string GetCustomerSHAPassword(string userName);

        BizResult<bool> ChangePassword(string loginName, string oldPassword, string newPassword, string confirmPassword);
    }
}
