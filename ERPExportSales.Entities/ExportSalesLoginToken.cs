using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("外销登录令牌", Schema = "dbo")]
    public class ExportSalesLoginToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("fID")]
        public int FID { get; set; }

        [Column("姓名")]
        public string UserName { get; set; }

        [Column("登录令牌")]
        public string Token { get; set; }

        [Column("过期时间")]
        public DateTime ExpiresTime { get; set; }

        [Column("是否记住")]
        public int Remember { get; set; }

        [Column("用户类型")]
        public int? UserType { get; set; }
    }
}
