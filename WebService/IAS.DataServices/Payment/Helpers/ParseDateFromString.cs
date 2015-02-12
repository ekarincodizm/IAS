using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Helpers
{
    public class ParseDateFromString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerDate">'ddMMyyyy'</param>
        /// <returns></returns>
        public static DateTime ParseDateHeaderBank(String headerDate) 
        {
            string[] format = { "ddMMyyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(headerDate, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                date = DateTime.MinValue;
            }
            return date;
        }
 
    }
}