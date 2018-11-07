using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("员工", Schema = "dbo")]
    public class Employee
    {
        [Column("姓名")]
        public string Name { get; set; }

        [Column("登录名")]
        public string LoginName { get; set; }

        [Column("密码")]
        public string Password { get; set; }
    }
}
