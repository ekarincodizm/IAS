using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class LicenseFileHelper
    {
        public static DateTime PhaseToDate(String source) 
        {
            string[] format = { "dd/MM/yyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                return DateTime.MinValue;
            }
            return date;
   
        }

        public static DateTime? PhaseStartDate(String source)
        {
            string[] format = { "dd/MM/yyyy" };
            DateTime date;
  
            if (String.IsNullOrEmpty(source))
            {
                return null;
            }
            else if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                return DateTime.MinValue;
            }

            return date;
        }


        public static DateTime? PhaseToDateNull(String source)   
        {
            string[] format = { "dd/MM/yyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                return null;
            }
            return date;

        }


        public static string PhaseARDate(String source)
        {
            string[] format = { "dd/MM/yyyy" };
            DateTime date;
            string res = string.Empty;


            if (String.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            else if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                return "NOTARDATE";
            }


            return res;

        }






        public static Decimal PhaseToMoney(String source)
        {
    
            Decimal result = new Decimal();
            if (!Decimal.TryParse(source, out result))
            {
                result = 0m;
            }

            return result;

        }

        public static String PhaseToAR_Answer(String data) {

            return data;
        }

        public static Int32 PhaseToAmount(String source) {
            Int32 result;

            if (Int32.TryParse(source, out result))
                return result;
            else
                return 0;
        }



    
    }
}