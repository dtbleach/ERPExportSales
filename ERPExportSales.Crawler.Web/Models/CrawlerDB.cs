namespace ERPExportSales.Crawler.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CrawlerDB : DbContext
    {
        public CrawlerDB()
            : base("name=CrawlerDB")
        {
        }

        public virtual DbSet<API访问令牌> API访问令牌 { get; set; }
        public virtual DbSet<外销电商_内销爬虫数据> 外销电商_内销爬虫数据 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
