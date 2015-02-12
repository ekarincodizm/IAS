using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Helpers
{
    public class PhaseStringToAmountImportFileTemp
    {
        public static String Phase(String paymentAmountText)
        {

            if (paymentAmountText.Contains('.'))
            {
                string[] data = paymentAmountText.Split('.');
                data[0] = data[0].PadLeft(11, '0');
                data[1] = data[1].PadRight(2, '0');
                return String.Format("{0}{1}", data[0], data[1]);
            }
            else {

                paymentAmountText = paymentAmountText.PadLeft(11, '0');
                paymentAmountText = paymentAmountText + "00";
                return paymentAmountText;
            }
              

       
        }
    }
}