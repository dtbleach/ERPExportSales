using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("客户", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("fID")]
        public int fID { get; set; }

        [Column("名称")]
        public string Name { get; set; }

        [Column("账号")]
        public string LoginName { get; set; }

        [Column("密码")]
        public byte[] Password { get; set; }
    }
}
