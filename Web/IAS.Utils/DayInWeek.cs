using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public static class DayInWeek
    {
        private static System.Globalization.CultureInfo cultureInfo =
                    System.Threading.Thread.CurrentThread.CurrentCulture;
        public static DateTime GetFirstDayOfWeek(this DateTime dayInWeek)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
        public static DateTime GetLastDayOfWeek(this DateTime dayInWeek)
        {
            DayOfWeek lastDay = (cultureInfo.DateTimeFormat.FirstDayOfWeek - 1);
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != lastDay)
                firstDayInWeek = firstDayInWeek.AddDays(1);

            return firstDayInWeek;
        }
    }
}
