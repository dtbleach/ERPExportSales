using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;

namespace ERPExportSales.Services
{
    public class ExportSalesService : IExportSalesService
    {

        public IVExportSalesOceanFreightRepository vExportSalesOceanFreightRepository;

        public IVPublicHolidayRepository vPublicHolidayRepository;

        public IIPWhiteListRepository ipWhiteListRepository;

        public IEmployeeRepository employeeRepository;

        public IOrderRepository orderRepository;

        public IDatabaseFactory databaseFactory;

        public IUnitOfWork unitOfWork;

        public ExportSalesService(IDatabaseFactory databaseFactory, IUnitOfWork unitOfWork, IVExportSalesOceanFreightRepository vExportSalesOceanFreightRepository, IVPublicHolidayRepository vPublicHolidayRepository, IIPWhiteListRepository ipWhiteListRepository, IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
        {
            this.databaseFactory = databaseFactory;
            this.unitOfWork = unitOfWork;
            this.vExportSalesOceanFreightRepository = vExportSalesOceanFreightRepository;
            this.vPublicHolidayRepository = vPublicHolidayRepository;
            this.ipWhiteListRepository = ipWhiteListRepository;
            this.orderRepository = orderRepository;
            this.employeeRepository = employeeRepository;
        }

        public IList<VExportSalesOceanFreight> GetExportSalesOceanFreight(int customerID)
        {
            var vExportSalesOceanFreight = vExportSalesOceanFreightRepository.GetMany(p => p.CustomerID == customerID);

            return vExportSalesOceanFreight != null ? vExportSalesOceanFreight.ToList() : null;
        }

        public IList<VPublicHoliday> GetPublicHoliday()
        {
            var vPublicHoliday = vPublicHolidayRepository.GetAll();
            return vPublicHoliday != null ? vPublicHoliday.ToList() : null;
        }

        public bool RequestIPWhiteList(string ip)
        {
            var ipWhiteList = ipWhiteListRepository.GetMany(p => p.IP == ip);
            bool flag = false;
            if (ipWhiteList != null)
            {
                if (ipWhiteList.Count() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public IList<Order> GetOrdersByEmployeeName(string name, int depid)
        {
            var db = databaseFactory.Get();
            int level = db.GetEmployeeLevel(name);

            if (level == 1)
            {
                var orders = orderRepository.GetMany(p => p.Sales == name || p.Maker == name);
                return orders != null ? orders.ToList() : null;
            }
            else if (level == 2)
            {
                var orders = orderRepository.GetMany(p => p.DepID == depid);
                return orders != null ? orders.ToList() : null;
            }

            return new List<Order>();
        }

        public IList<Order> GetOrdersByCustomerID(int customerID)
        {
            var orders = new List<Order>();
            return orders;
        }
    }
}
