using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class PhaseAmountHelper
    {
        public static Decimal ConvertStringAmount(String amount)
        {
            amount = amount.Trim();
           amount =  amount.Insert(amount.Length - 2, ".");
            Decimal result;
            if (!Decimal.TryParse(amount, out result)) {
                
            }
            return result;
        }

        public static Decimal ConvertStringAmountCity(String amount)
        {
            amount = amount.Trim();
            Decimal result;
            if (!Decimal.TryParse(amount, out result))
            {

            }
            return result;                   
        }
    }
}