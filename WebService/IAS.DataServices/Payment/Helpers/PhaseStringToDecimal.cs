using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Helpers
{
    public class PhaseStringToDecimal
    {
        public static Decimal Phase(String paymentAmountText)
        {
            Decimal amount;
            if (Decimal.TryParse(paymentAmountText, out amount)) {
                if(paymentAmountText.Contains('.'))
                    return Convert.ToDecimal(paymentAmountText);
                else
                    return Convert.ToDecimal(paymentAmountText.Insert(paymentAmountText.Length - 2, "."));
                    
            } return 0m;
            
        }
    }
}