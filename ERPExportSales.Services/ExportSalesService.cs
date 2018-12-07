using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;
using System.Data.SqlClient;
using ERPExportSales.Framework;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Data;

namespace ERPExportSales.Services
{
    public class ExportSalesService : IExportSalesService
    {

        public IVExportSalesOceanFreightRepository vExportSalesOceanFreightRepository;

        public IVPublicHolidayRepository vPublicHolidayRepository;

        public IIPWhiteListRepository ipWhiteListRepository;

        public IEmployeeRepository employeeRepository;

        public ICustomerRepository customerRepository;

        public IOrderRepository orderRepository;

        public IDatabaseFactory databaseFactory;

        public IUnitOfWork unitOfWork;

        public ExportSalesService(IDatabaseFactory databaseFactory, IUnitOfWork unitOfWork, IVExportSalesOceanFreightRepository vExportSalesOceanFreightRepository, IVPublicHolidayRepository vPublicHolidayRepository, IIPWhiteListRepository ipWhiteListRepository, IOrderRepository orderRepository, IEmployeeRepository employeeRepository,
            ICustomerRepository customerRepository)
        {
            this.databaseFactory = databaseFactory;
            this.unitOfWork = unitOfWork;
            this.vExportSalesOceanFreightRepository = vExportSalesOceanFreightRepository;
            this.vPublicHolidayRepository = vPublicHolidayRepository;
            this.ipWhiteListRepository = ipWhiteListRepository;
            this.orderRepository = orderRepository;
            this.employeeRepository = employeeRepository;
            this.customerRepository = customerRepository;
        }

        public IList<VExportSalesOceanFreight> GetExportSalesOceanFreightByCustomerID(int customerID)
        {
            var vExportSalesOceanFreight = vExportSalesOceanFreightRepository.GetMany(p => p.CustomerID == customerID);

            return vExportSalesOceanFreight != null ? vExportSalesOceanFreight.ToList() : null;
        }

        public IList<VExportSalesOceanFreight> GetExportSalesOceanFreightByEmployee(string name)
        {
            var db = databaseFactory.Get();
            int level = db.GetEmployeeLevel(name);
            var employee = employeeRepository.Get(p => p.Name == name);
            if (employee == null)
            {
                return new List<VExportSalesOceanFreight>();
            }
            if (level == 1)
            {
                var vExportSalesOceanFreight = vExportSalesOceanFreightRepository.GetMany(p => p.Sales == employee.FID||p.BScrew==employee.FID || p.SScrew==employee.FID);
                return vExportSalesOceanFreight != null ? vExportSalesOceanFreight.ToList() : null;
            }
            else if (level == 2)
            {
                var vExportSalesOceanFreight = vExportSalesOceanFreightRepository.GetMany(p => p.DepID==employee.DepID);
                return vExportSalesOceanFreight != null ? vExportSalesOceanFreight.ToList() : null;
            }
            else if (level == 3)
            {
                var vExportSalesOceanFreight = vExportSalesOceanFreightRepository.GetAll();
                return vExportSalesOceanFreight != null ? vExportSalesOceanFreight.ToList() : null;
            }

            return new List<VExportSalesOceanFreight>();
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

        /// <summary>
        /// 获取外销订单
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="pageSize">一页显示多少条</param>
        /// <param name="pageNum">第几页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public IList<Order> GetOrdersByEmployeeName(string name, int depId,int pageSize, int pageNum, string pono, string scno, string invoiceno,string customer)
        {
            var db = databaseFactory.Get();
            int level = db.GetEmployeeLevel(name);
            SqlParameter paramTop = new SqlParameter("@Top", pageSize);
            SqlParameter paramPageNum = new SqlParameter("@RowNumber", pageNum);
            //SqlParameter paramTotalRecord = new SqlParameter("@TotalRecord", SqlDbType.Int);
            //paramTotalRecord.Direction = System.Data.ParameterDirection.Output;
            string sqlWhere = string.Empty;
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(customer))
            {
                sqlWhere += " and 客户名称 like '%" + customer + "%'";
            }

            if (!string.IsNullOrEmpty(pono))
            {
                sqlWhere += " and [PO No.]='" + pono + "'";
            }

            if (!string.IsNullOrEmpty(scno))
            {
                sqlWhere += " and [SC No.]='" + scno + "'";
            }

            if (!string.IsNullOrEmpty(invoiceno))
            {
                sqlWhere += " and [Invoice No.]='" + invoiceno + "'";
            }

            if (level == 1)
            {
                SqlParameter paramSqlWhere = new SqlParameter("@SqlWhere", "(销售员='" + name + "' or 制单人='" + name + "') " + sqlWhere);
                var orders = db.OrderEntities.SqlQuery("p外销电商_万能分页 @Top,@RowNumber,@SqlWhere", paramTop, paramPageNum, paramSqlWhere).ToList();
                // totalCount = (int)paramTotalRecord.Value;
                return orders != null ? orders.ToList() : null;
            }
            else if (level == 2)
            {
                SqlParameter paramSqlWhere = new SqlParameter("@SqlWhere", "部门ID=" + depId + sqlWhere);
                var orders = db.OrderEntities.SqlQuery("p外销电商_万能分页 @Top,@RowNumber,@SqlWhere", paramTop, paramPageNum, paramSqlWhere).ToList();
                // totalCount = (int)paramTotalRecord.Value;
                return orders != null ? orders.ToList() : null;
            }
            else if (level == 3)
            {

                SqlParameter paramSqlWhere = new SqlParameter("@SqlWhere", "1=1 " + sqlWhere);
                var query = db.OrderEntities.SqlQuery("p外销电商_万能分页 @Top,@RowNumber,@SqlWhere", paramTop, paramPageNum, paramSqlWhere);
                var orders = query.ToList();
                // totalCount = (int)paramTotalRecord.Value;
                return orders != null ? orders.ToList() : null;
            }

            return new List<Order>();
        }

        public IList<Order> GetOrdersByCustomerID(int customerID, int pageSize, int pageNum, string pono, string scno, string invoiceno)
        {
            var db = databaseFactory.Get();
            string sqlWhere = string.Empty;
            StringBuilder str = new StringBuilder();
            SqlParameter paramTop = new SqlParameter("@Top", pageSize);
            SqlParameter paramPageNum = new SqlParameter("@RowNumber", pageNum);
            if (!string.IsNullOrEmpty(pono))
            {
                sqlWhere += " and [PO No.]='" + pono + "'";
            }

            if (!string.IsNullOrEmpty(scno))
            {
                sqlWhere += " and [SC No.]='" + scno + "'";
            }

            if (!string.IsNullOrEmpty(invoiceno))
            {
                sqlWhere += " and [Invoice No.]='" + invoiceno + "'";
            }


            SqlParameter paramSqlWhere = new SqlParameter("@SqlWhere", "客户ID=" + customerID + " " + sqlWhere);
            var orders = db.OrderEntities.SqlQuery("p外销电商_万能分页 @Top,@RowNumber,@SqlWhere", paramTop, paramPageNum, paramSqlWhere).ToList();
            // totalCount = (int)paramTotalRecord.Value;
            return orders != null ? orders.ToList() : null;

        }
    }
}
