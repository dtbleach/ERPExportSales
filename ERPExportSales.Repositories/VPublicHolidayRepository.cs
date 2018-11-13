using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class VPublicHolidayRepository : RepositoryBase<VPublicHoliday>, IVPublicHolidayRepository
    {
        public VPublicHolidayRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IVPublicHolidayRepository : IRepository<VPublicHoliday>
    {

    }
}
