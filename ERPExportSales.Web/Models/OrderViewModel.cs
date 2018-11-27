using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Models
{
    public class OrderViewModel
    {
        public Guid FID { get; set; }

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

        public string InvoiceNo { get; set; }

        public string PackingList { get; set; }

        public string BLCopy { get; set; }

        public string QualityReport { get; set; }

        public string Customer { get; set; }

        public string SCNoHref { get; set; }

        public string InvoiceHref { get; set; }

        public string PackingHref { get; set; }

        public string BLNoHref { get; set; }

        public string QRHref { get; set; }


    }
}