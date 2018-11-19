using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("外销登录IP白名单", Schema = "dbo")]
    public class IPWhiteList
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("fID")]
        public int FID { get; set; }

        [Column("ip")]
        public string IP { get; set; }
    }
}
