using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
