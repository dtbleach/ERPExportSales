using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("v外销电商_海运费", Schema = "dbo")]
    public class VExportSalesOceanFreight
    {
        [Key]
        [Column("fID")]
        public int FID { get; set; }

        [Column("Port")]
        public string Port { get; set; }

        [Column("Freight")]
        public decimal Freight { get; set; }

        [Column("Schedule")]
        public string Schedule { get; set; }

        [Column("Days to port")]
        public int DaysToPort { get; set; }

        [Column("客户ID")]
        public int CustomerID { get; set; }
    }
}
