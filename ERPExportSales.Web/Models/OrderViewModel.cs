using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Models
{
    public class OrderViewModel
    {
        public int FID { get; set; }

        public string PONo { get; set; }

        public string SCNo { get; set; }

        public string Amount { get; set; }

        public string ETD { get; set; }

        public decimal Progress { get; set; }

        public int CustomerID { get; set; }

        public string Sales { get; set; }

        public string Maker { get; set; }

        public int DepID { get; set; }

        public decimal Weight { get; set; }

        public int InvoiceNo { get; set; }

        public int PackingList { get; set; }

        public int BLCopy { get; set; }

        public int QualityReport { get; set; }
    }
}