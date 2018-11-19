using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Entities
{
    [Table("v外销电商_订单信息", Schema = "dbo")]
    public class Order
    {

        [Key]
        [Column("ID")]
        public int FID { get; set; }

        [Column("PO No.")]
        public string PONo { get; set; }

        [Column("SC No.")]
        public string SCNo { get; set; }

        [Column("Amount")]
        public string Amount { get; set; }

        [Column("ETD")]
        public string ETD { get; set; }

        [Column("Progress")]
        public decimal Progress { get; set; }

        [Column("客户ID")]
        public int CustomerID { get; set; }

        [Column("销售员")]
        public string Sales { get; set; }

        [Column("制单人")]
        public string Maker { get; set; }

        [Column("部门ID")]
        public int DepID { get; set; }

        [Column("Weight (kg)")]
        public decimal Weight { get; set; }

        [Column("Invoice No.")]
        public int InvoiceNo { get; set; }

        [Column("Packing List")]
        public int PackingList { get; set; }

        [Column("BL Copy")]
        public int BLCopy { get; set; }

        [Column("Quality Report")]
        public int QualityReport { get; set; }

    }
}
