using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class IPWhiteListRepository : RepositoryBase<IPWhiteList>, IIPWhiteListRepository
    {
        public IPWhiteListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IIPWhiteListRepository : IRepository<IPWhiteList>
    {

    }
}
