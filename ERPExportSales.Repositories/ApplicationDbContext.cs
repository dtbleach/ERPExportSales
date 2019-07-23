using EntityFramework.Functions;
using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public partial class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
          : base("ERP4")
        {
        }

        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ApplicationDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ApplicationDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public virtual void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch
            {
                // TODO:
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // http://stackoverflow.com/questions/5532810/entity-framework-code-first-defining-relationships-keys
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Configurations.Add(new ReleasePlanConfiguration());

            modelBuilder.Conventions.Add(new FunctionConvention<ApplicationDbContext>());
        }



        //private static string GetConnectionString()
        //{
        //    return Bootstrap.ServiceConfiguration.OpsPortalDbConnectionString;
        //}

        public DbSet<Employee> EmployeeEntities { get; set; }

        public DbSet<ExportSalesLoginToken> ExportSalesLoginTokenEntities { get; set; }

        public DbSet<VExportSalesOceanFreight> VExportSalesOceanFreightEntities { get; set; }

        public DbSet<VPublicHoliday> VPublicHolidayEntities { get; set; }

        public DbSet<IPWhiteList> IPWhiteListEntities { get; set; }

        public DbSet<Order> OrderEntities { get; set; }

        public DbSet<Customer> CustomerEntities { get; set; }

        public DbSet<LoginStatus> LoginStatusEntities { get; set; }

        public DbSet<Q195> Q195Entities { get; set; }

        public DbSet<USDCNY> USDCNYEntities { get; set; }

        public DbSet<VSFCOceanFreight> VSFCOceanFreightEntities { get; set; }

    }
}
