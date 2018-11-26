using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    public class LoginStatus
    {

        [Key]
        public int ID { get; set; }

        public int Login { get; set; }

        public int UserType { get; set; }
    }
}
