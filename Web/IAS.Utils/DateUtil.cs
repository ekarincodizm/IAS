using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IAS.Utils
{
    public static class DateUtil
    {

        private static  CultureInfo enUS = new CultureInfo("en-US");
        private static CultureInfo thTH = new CultureInfo("th-TH"); 

        private static string CurrentYearEng
        {
            get
            {
                return DateTime.Today.Year.ToString("0000");
            }
        }

        public static string yyyyMMdd_HHmm_Now
        {
            get
            {
                return CurrentYearEng + DateTime.Now.ToString("MMdd HH:mm");
                //return "20120530 16:45";  //for test
            }
        }

        public static string yyyyMMdd_Now
        {
            get
            {
                return CurrentYearEng + DateTime.Now.ToString("MMdd");
            }
        }

        public static string yyMMdd_Now
        {
            get
            {
                return CurrentYearEng.Substring(2, 2) + DateTime.Now.ToString("MMdd");
            }
        }

        public static string dd_MM_yyyy_Now
        {
            get
            {
                return DateTime.Now.ToString("dd/MM/") + CurrentYearEng;
            }
        }
        public static string HHmm_Now
        {
            get
            {
                return DateTime.Now.ToString("HH:mm");
            }
        }

        public static string HHmmss_Now
        {
            get
            {
                return DateTime.Now.ToString("HH:mm:ss");
            }
        }

        //Tob Edit 12022013//
        public static string dd_MMMM_yyyy_Now_TH
        {
            get
            {
                return DateTime.Now.ToString("dd MMMM yyyy");
            }
        }
        //Tob Edit 12022013//


        //Tob Edit 02042013//
        public static string dd_MM_yyyy_Now_TH
        {
            get
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        //Tob Edit 02042013//

        public static DateTime String_dd_MM_yyyy_ToDate(this string strDate, char separate, bool sourceYearIsThai)
        {
            string[] dates = strDate.Split(separate);
            int day = dates.Length > 0 ? dates[0].ToInt() : 1;
            int month = dates.Length > 1 ? dates[1].ToInt() : 1;
            int year = dates.Length > 2 ? dates[2].ToInt() : 1;
            year = sourceYearIsThai ? year - 543 : year;

            return new DateTime(year, month, day);
        }

        public static string ToString_yyyyMMdd(this DateTime date)
        {
            return date.Year.ToString("0000") + date.Date.ToString("MMdd");
        }

        public static List<DataItem> GetMonthList(string firstItem)
        {
            List<DataItem> list = new List<DataItem>();
            if (firstItem != string.Empty) list.Add(new DataItem { Id = "", Name = firstItem });
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("th-TH");
            for (int i = 1; i <= 12; i++)
            {
                DateTime date = new DateTime(DateTime.Now.Year, i, 1);
                list.Add(new DataItem { Id = i.ToString("00"), Name = date.ToString("MMMM", ci) });
            }
            return list;
        }

        public static List<DataItem> GetShortMonthList(string firstItem)
        {
            List<DataItem> list = new List<DataItem>();
            if (firstItem != string.Empty) list.Add(new DataItem { Id = "", Name = firstItem });
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("th-TH");
            for (int i = 1; i <= 12; i++)
            {
                DateTime date = new DateTime(DateTime.Now.Year, i, 1);
                list.Add(new DataItem { Id = i.ToString("00"), Name = date.ToString("MMM", ci) });
            }
            return list;
        }


        /// <summary>
        /// แปลงวันที่เป็นจำนวนวัน
        /// </summary>
        /// <param name="anyDate">ข้อมูลประเภท DateTime</param>
        /// <returns>จำนวนวันนับจากวันที่ 1 เดือน 1 ปี 1</returns>
        private static DateTime firstDate = new DateTime(1, 1, 1);
        public static int ToNumberDays(this DateTime anyEngDate)
        {
            return anyEngDate.Subtract(firstDate).Days + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatDate"> Format date simple: "M/dd/yyyy hh:mm"</param>
        /// <param name="dateString"> value expacke</param>
        /// <param name="culture">culture type  "en-US", "th-TH"</param>
        /// <returns></returns>
        public static bool ValidateDateFormatString(String formatDate, String dateString, String culture = "en-US") 
        {
            CultureInfo cultureInfo = new CultureInfo(culture);
            if (culture == "th-TH") {
                cultureInfo = thTH;
            }
          

            DateTime dateValue;                              

            if (DateTime.TryParseExact(dateString, "M/dd/yyyy hh:mm", cultureInfo,
                            DateTimeStyles.None, out dateValue))
                return true;
            else
                return false;

        }

    }

    public class DataItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
