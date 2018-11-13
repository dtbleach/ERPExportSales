using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class VExportSalesOceanFreightRepository : RepositoryBase<VExportSalesOceanFreight>, IVExportSalesOceanFreightRepository
    {
        public VExportSalesOceanFreightRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IVExportSalesOceanFreightRepository : IRepository<VExportSalesOceanFreight>
    {

    }
}
