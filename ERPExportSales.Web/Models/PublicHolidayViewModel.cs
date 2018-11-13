using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Models
{
    public class PublicHolidayViewModel
    {
        public Guid ID { get; set; }

        public string Holiday { get; set; }

        public string Date { get; set; }
    }
}