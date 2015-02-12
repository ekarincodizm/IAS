using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class PaymentHtmlHelper
    {
        public static String ResolveDate(String source) 
        {
            string[] format = { "ddMMyyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                return "-";
            }
            return date.ToString("dd/MM/yyyy");
        }

        public static String PhaseKTBMoney(String source)
        {
            String payAmount = source.Insert(source.Length - 2, ".");
            Decimal result = new Decimal();
            if (!Decimal.TryParse(payAmount, out result))
            {
                return  "-";
            }
                                                               
            return result.ToString("#,##0.00");
        }
        public static String PhaseCityBankMoney(String source)  
        {

            Decimal result = new Decimal();
            if (!Decimal.TryParse(source, out result))
            {
                return "-";
            }

            return result.ToString("#,##0.00");
        }
    }
}
