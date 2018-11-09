using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public interface IEmployeeService
    {
        string GetEmployeeSHAPassword(string userName);

        Employee GetEmployee(string userName);
    }
}
