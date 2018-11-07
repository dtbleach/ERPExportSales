

namespace ERPExportSales.Repositories
{
    using System;
    using System.Data.Entity;

    public class FunctionsConvention<T> : FunctionsConvention
        where T : DbContext
    {
        public FunctionsConvention(string defaultSchema)
            : base(defaultSchema, typeof(T))
        {
        }
    }
}