using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace ERPExportSales.Entities
{
    [Table("v门户_海运费", Schema = "dbo")]
    public class VSFCOceanFreight
    {
        [Key]
        [Column("ID")]
        public Guid ID { get; set; }

        [Column("Port")]
        public string Port { get; set; }

        [Column("Freight")]
        public decimal Freight { get; set; }

        //[Column("Days to port")]
        //public int DaysToPort { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        //[Column("Schedule")]
        //public string Schedule { get; set; }

        [Column("Continent")]
        public string Continent { get; set; }
    }
}