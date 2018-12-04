using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class USDCNYRepository : RepositoryBase<USDCNY>, IUSDCNYRepository
    {
        public USDCNYRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IUSDCNYRepository : IRepository<USDCNY>
    {

    }
}
