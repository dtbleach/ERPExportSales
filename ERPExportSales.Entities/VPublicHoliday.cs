using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("v外销电商_公休日", Schema = "dbo")]
    public class VPublicHoliday
    {
        [Key]
        [Column("ID")]
        public Guid ID { get; set; }

        [Column("Holiday")]
        public string Holiday { get; set; }

        [Column("Date")]
        public string Date { get; set; }
    }
}
