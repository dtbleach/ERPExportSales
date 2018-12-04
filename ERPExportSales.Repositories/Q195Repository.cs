using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class Q195Repository : RepositoryBase<Q195>, IQ195Repository
    {
        public Q195Repository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IQ195Repository : IRepository<Q195>
    {

    }
}
