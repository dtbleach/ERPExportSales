using ERPExportSales.Core;
using ERPExportSales.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Repositories
{
    public class ERPExportSalesDatabaseFactory : Disposable, IDatabaseFactory
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