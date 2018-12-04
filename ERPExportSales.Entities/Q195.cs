using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("外销电商_Q195爬虫数据", Schema = "dbo")]
    public class Q195
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Column("publishdate")]
        public string PublishDate { get; set; }

        [Column("href")]
        public string Href { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("city")]
        public string City { get; set; }
    }
}
