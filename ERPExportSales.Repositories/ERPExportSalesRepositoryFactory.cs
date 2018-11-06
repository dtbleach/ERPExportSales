using ERPExportSales.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class ERPExportSalesRepositoryFactory : Disposable, IDatabaseFactory
    {
        private ApplicationDbContext dataContext;

        public ApplicationDbContext Get()
        {
            return dataContext ?? (dataContext = new ApplicationDbContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }

    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext Get();
    }
}
