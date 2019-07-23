using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public interface IExportSalesService
    {
        IList<VSFCOceanFreight> GetOceanFreight();
        IList<VExportSalesOceanFreight> GetExportSalesOceanFreightByCustomerID(int customerID);
        IList<VExportSalesOceanFreight> GetExportSalesOceanFreightByEmployee(string name);
        IList<VPublicHoliday> GetPublicHoliday();
        bool RequestIPWhiteList(string ip);
        IList<Order> GetOrdersByEmployeeName(string name, int depId,int pageSize, int pageNum, string pono, string scno, string invoiceno,string customer);
        IList<Order> GetOrdersByCustomerID(int customerID, int pageSize, int pageNum, string pono, string scno, string invoiceno);
    }
}
