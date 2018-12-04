using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("外销电商_爬虫数据USDCNY", Schema = "dbo")]
    public class USDCNY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Column("publishdate")]
        public string PublishDate { get; set; }

        [Column("source")]
        public string Source { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("type")]
        public int Type { get; set; }
    }
}
