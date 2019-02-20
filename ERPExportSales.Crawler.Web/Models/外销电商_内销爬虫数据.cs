namespace ERPExportSales.Crawler.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class 外销电商_内销爬虫数据
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(200)]
        public string 材质 { get; set; }

        [StringLength(200)]
        public string 品名 { get; set; }

        [StringLength(200)]
        public string 规格 { get; set; }

        [StringLength(200)]
        public string 表面处理 { get; set; }

        [StringLength(200)]
        public string 分类 { get; set; }

        public string 图片 { get; set; }

        [StringLength(200)]
        public string 品牌 { get; set; }

        [StringLength(200)]
        public string 仓库 { get; set; }

        [StringLength(200)]
        public string 库存 { get; set; }

        [StringLength(200)]
        public string 包装信息 { get; set; }

        [StringLength(200)]
        public string 价格 { get; set; }

        [StringLength(200)]
        public string 数据来源 { get; set; }

        [StringLength(100)]
        public string 更新日期 { get; set; }
    }
}
