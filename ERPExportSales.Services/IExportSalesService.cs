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
        IList<VExportSalesOceanFreight> GetExportSalesOceanFreight(int customerID);
        IList<VPublicHoliday> GetPublicHoliday();
        bool RequestIPWhiteList(string ip);
        IList<Order> GetOrdersByEmployeeName(string name, int pageSize, int pageNum, string pono, string scno, string invoiceno);
        IList<Order> GetOrdersByCustomerID(int customerID);
    }
}
