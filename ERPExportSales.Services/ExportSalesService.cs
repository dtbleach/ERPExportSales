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

        public IUnitOfWork unitOfWork;

        public ExportSalesService(IUnitOfWork unitOfWork, IVExportSalesOceanFreightRepository vExportSalesOceanFreightRepository, IVPublicHolidayRepository vPublicHolidayRepository)
        {
            this.unitOfWork = unitOfWork;
            this.vExportSalesOceanFreightRepository = vExportSalesOceanFreightRepository;
            this.vPublicHolidayRepository = vPublicHolidayRepository;
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
    }
}
