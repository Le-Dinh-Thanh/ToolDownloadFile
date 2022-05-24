using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toolVanDao.Data
{
    public class InvoiceObject
    {
        public string InvoiceNumber { get; set; }
        public string MauSo { get; set; }
        public string KyHieu { get; set; }
        public bool Selected { get; set; }
        public string Value { get; set; }
        public DateTime ngay_hd { get; set; }
        public string InvInvoiceAuthId { get; set; }
        public string SBM { get; set; }
        public string hoadon68id { get; set; }
        public string shdon { get; set; }
    }
}
