﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("员工", Schema = "dbo")]
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("fID")]
        public int FID { get; set; }

        [Column("姓名")]
        public string Name { get; set; }

        [Column("登陆名")]
        public string LoginName { get; set; }

        [Column("登陆密码")]
        public byte[] Password { get; set; }

        [Column("部门ID")]
        public int DepID { get; set; }
    }
}
