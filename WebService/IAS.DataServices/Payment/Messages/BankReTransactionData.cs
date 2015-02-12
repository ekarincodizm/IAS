using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Payment.Messages
{
    public class BankReTransactionData
    {
        public IEnumerable<AG_IAS_PAYMENT_DETAIL> Details { get; set; }
        public IEnumerable<AG_IAS_PAYMENT_DETAIL_HIS> DetailHis { get; set; }
    }
}                                                                          