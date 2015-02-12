using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class PayByCheque
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string InvoiceNo { get; set; }
        public string AccountCode { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public double ChequeAmount { get; set; }
        public string ChequeBank { get; set; }
    }
}
