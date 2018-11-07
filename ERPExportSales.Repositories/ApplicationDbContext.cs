using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public class ApplicationDbContext:DbContext
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

        static ApplicationDbContext()
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
            modelBuilder.Conventions.Add(new FunctionsConvention("dbo", typeof(Functions)));
        }



        //private static string GetConnectionString()
        //{
        //    return Bootstrap.ServiceConfiguration.OpsPortalDbConnectionString;
        //}

        //public DbSet<ReleaseJobEntity> ReleaseJobEntities { get; set; }

        //public DbSet<ReleasePlanEntity> ReleasePlanEntities { get; set; }

        //public DbSet<JenkinsJobEntity> JenkinsJobEntities { get; set; }
    }
}
