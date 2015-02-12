using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Payment.Messages
{
    public class BankTransactionTempData
    {
        public AG_IAS_TEMP_PAYMENT_HEADER Header { get; set; }
        public IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL> Details { get; set; }
        public IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL_HIS> DetailHis { get; set; }
        public AG_IAS_TEMP_PAYMENT_TOTAL Total { get; set; }
    }
}