using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class BankTransaction
    {
        public string Id { get; set; }
        public string SequenceNo { get; set; }
        public string AccountNo { get; set; }
        public string PaymentDate { get; set; }
        public string CustomerName { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ChequeNo { get; set; }
        public decimal Amount { get; set; }
        public string ErrorMessage { get; set; }

        public String ExpireDate { get; set; }

        public Int32 Status { get; set; }
        
        public String ChangeRef1 { get; set; }
        public String ChangeAmount{ get; set; }

        public Boolean Selected { get; set; }
        public String DescGetData { get; set; }
    }
}
