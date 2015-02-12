using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Payment.TransactionBanking.KTB;

namespace IAS.DataServices.Payment.Mapper
{
    public static class KTBFileDetailMapper
    {
        public static IEnumerable<DTO.BankTransaction> ConvertToBankTransactions(this IEnumerable<BankFileDetail> ktbFileDetails) 
        {
            IList<DTO.BankTransaction> bankTransactions = new List<DTO.BankTransaction>();
            foreach (BankFileDetail item in ktbFileDetails)
            {
                bankTransactions.Add(new DTO.BankTransaction() { 
                    Id = item.ID,
                    SequenceNo = item.SequenceNo,
                    PaymentDate = String.Format("{0}/{1}/{2}", item.PAYMENT_DATE.Substring(0, 2), item.PAYMENT_DATE.Substring(2, 2), item.PAYMENT_DATE.Substring(4, 4)),
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