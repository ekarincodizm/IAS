using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Helpers
{
    public class ParsePaymentAmountImport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerDate">'ddMMyyyy'</param>
        /// <returns></returns>
        public static Decimal Phase(String paymentAmountText)
        {
            String payAmount = paymentAmountText.Insert(paymentAmountText.Length - 2, ".");
            Decimal result = new Decimal();
            if (!Decimal.TryParse(payAmount,out result)) {
                result = 0m; 
            }

            return result;
        }
        public static Decimal PhaseCityBank(String paymentAmountText)
        {

            Decimal result = new Decimal();
            if (!Decimal.TryParse(paymentAmountText, out result))
            {                                                                   
                result = 0m;
            }

            return result;
        }
        public static Boolean TryPhase(String paymentAmountText)
        {
            String payAmount = paymentAmountText.Insert(paymentAmountText.Length - 2, ".");
            Decimal result = new Decimal();
            if (Decimal.TryParse(payAmount, out result))
            {
                return true;
            }
            else
                return false;
        }                                                 
    }
}