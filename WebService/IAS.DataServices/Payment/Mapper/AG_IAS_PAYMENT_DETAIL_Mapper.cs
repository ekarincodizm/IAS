using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_PAYMENT_DETAIL_Mapper
    {
        public static IEnumerable<DTO.BankTransaction> ConvertToBankTransactions(this IEnumerable<AG_IAS_PAYMENT_DETAIL> ktbFileDetails)
        {
            IList<DTO.BankTransaction> bankTransactions = new List<DTO.BankTransaction>();
            Int32 seq = 0;
            foreach (AG_IAS_PAYMENT_DETAIL item in ktbFileDetails)
            {
                seq++;
                bankTransactions.Add(new DTO.BankTransaction()
                {
                    Id = item.ID,
                    SequenceNo = seq.ToString(),
                    PaymentDate =  ((DateTime)item.PAYMENT_DATE).ToString("dd/MM/yyyy"), // , item.PAYMENT_DATE.Substring(2, 2), item.PAYMENT_DATE.Substring(4, 4)),
                    CustomerName = item.CUSTOMER_NAME,
                    Ref1 = item.CUSTOMER_NO_REF1,
                    Ref2 = item.REF2,
                    ChequeNo = item.CHEQUE_NO,
                    Amount = PhaseAmountHelper.ConvertStringAmount(item.AMOUNT)
                });
            }

            return bankTransactions;
        }
    }
}