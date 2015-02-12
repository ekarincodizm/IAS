using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class Convert_month
    {
        public static string Num_to_full_string(int numMonth)
        {
            string MonthName=string.Empty;
            switch (numMonth)
            {
                case 1: MonthName = "มกราคม"; break;
                case 2: MonthName = "กุมภาพันธ์"; break;
                case 3: MonthName = "มีนาคม"; break;
                case 4: MonthName = "เมษายน"; break;
                case 5: MonthName = "พฤษภาคม"; break;
                case 6: MonthName = "มิถุนายน"; break;
                case 7: MonthName = "กรกฎาคม"; break;
                case 8: MonthName = "สิงหาคม"; break;
                case 9: MonthName = "กันยายน"; break;
                case 10: MonthName = "ตุลาคม"; break;
                case 11: MonthName = "พฤศจิกายน"; break;
                default: MonthName = "ธันวาคม"; break;
            }
            return MonthName;
        }

    }
}