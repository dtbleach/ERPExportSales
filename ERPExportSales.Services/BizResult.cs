using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public class BizResult<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
