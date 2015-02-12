using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.Common.Logging;

namespace IAS.DataServices.Helpers
{
    public class PhaseStringToDateHelper
    {
        /// <summary>
        /// Convert String To DateTime
        /// </summary>
        /// <param name="stringDate">String DateTime </param>
        /// <param name="format">Format  for StringDate</param>
        /// <param name="cultureString">Set Culture  ค.ศ.= "en-GB", พ.ศ.="th-TH"</param>
        /// <returns></returns>
        public static DateTime ParseDate(String stringDate, String format, String cultureString = "th-TH") 
        {
            DateTime date;
                                 
            if (!DateTime.TryParseExact(stringDate, format, System.Globalization.CultureInfo.GetCultureInfo(cultureString), 
                                                        System.Globalization.DateTimeStyles.None, out date))
            {
                //date = DateTime.MinValue;
                LoggerFactory.CreateLog().LogError("ไม่สามารถ แปลงวันทึ่จาก " + stringDate + " ด้วย format " + format + " ได้");
                throw new ApplicationException("ไม่สามารถ แปลงวันทึ่จาก " + stringDate + " ด้วย format " + format + " ได้");
               
            }
            return date;
        }
    }
}