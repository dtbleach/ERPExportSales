namespace ERPExportSales.Crawler.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class API访问令牌
    {
        public int id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        public string token { get; set; }

        public DateTime expire { get; set; }

        public string refresh_token { get; set; }
    }
}
