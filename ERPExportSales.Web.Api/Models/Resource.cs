using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Api.Models
{
    [ElasticsearchType(Name = "crawlertable", IdProperty = "id")]
    public class Resource
    {
        [Keyword(Name = "id")]
        public string ID { get; set; }

        [Keyword(Name = "关键字")]
        public string Keyword { get; set; }

        [Text(Name = "库存")]
        public string Stock { get; set; }

        [Text(Name = "品名")]
        public string ProductName { get; set; }

        [Text(Name = "分类")]
        public string Catalog { get; set; }

        [Text(Name = "等级")]
        public string Level { get; set; }

        [Text(Name = "仓库")]
        public string Store { get; set; }

        [Text(Name = "图片")]
        public string Img { get; set; }

        [Text(Name = "包装信息")]
        public string Package { get; set; }

        [Text(Name = "价格")]
        public string Price { get; set; }

        [Text(Name = "更新日期")]
        public string UpdateTime { get; set; }

        [Text(Name = "材质")]
        public string Material { get; set; }

        [Text(Name = "品牌")]
        public string Brand { get; set; }

        [Text(Name = "规格")]
        public string Spec { get; set; }

        [Text(Name = "表面处理")]
        public string SurfaceColor { get; set; }

        [Text(Name = "数据来源")]
        public string DataSource { get; set; }
    
    }
}