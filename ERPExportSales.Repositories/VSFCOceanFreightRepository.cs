using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ERPExportSales.Repositories
{
    public class VSFCOceanFreightRepository : RepositoryBase<VSFCOceanFreight>, IVSFCOceanFreightRepository
    {
        public VSFCOceanFreightRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IVSFCOceanFreightRepository : IRepository<VSFCOceanFreight>
    {

    }
}