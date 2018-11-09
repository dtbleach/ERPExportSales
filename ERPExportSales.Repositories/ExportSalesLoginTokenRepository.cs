using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class ExportSalesLoginTokenRepository : RepositoryBase<ExportSalesLoginToken>, IExportSalesLoginTokenRepository
    {
        public ExportSalesLoginTokenRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IExportSalesLoginTokenRepository : IRepository<ExportSalesLoginToken>
    {

    }
}
