using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Payment.Messages
{
    public class BankTransactionDataFactory
    {
        public static BankTransactionTempData ConCreate() {
            BankTransactionTempData obj = new BankTransactionTempData();
            obj.Header = new AG_IAS_TEMP_PAYMENT_HEADER();
            obj.Details = new List<AG_IAS_TEMP_PAYMENT_DETAIL>();
            obj.DetailHis = new List<AG_IAS_TEMP_PAYMENT_DETAIL_HIS>();
            obj.Total = new AG_IAS_TEMP_PAYMENT_TOTAL();

            return obj;
        }

        public static BankReTransactionData ConCreateReTrans()
        {
            BankReTransactionData obj = new BankReTransactionData();
            obj.Details = new List<AG_IAS_PAYMENT_DETAIL>();
            obj.DetailHis = new List<AG_IAS_PAYMENT_DETAIL_HIS>();
            return obj;
        }
    }
}