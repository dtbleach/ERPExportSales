using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Api.Models
{
    public class Email
    {
        public string from { get; set; }
        public string to { get; set; }

        public string title { get; set; }
        public string content { get; set; }
    }
}