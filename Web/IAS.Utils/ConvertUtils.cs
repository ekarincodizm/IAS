using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IAS.Utils
{
    public static class ConvertCustom
    {
        public static T Parse<T>(this object obj) where T : IConvertible
        {
            if (obj == null)
                return default(T);
            else
                try
                {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                catch
                {
                    return default(T);
                }
        }

        public static bool ToBool(this object source)
        {
            return source.Parse<bool>();
        }

        public static int ToInt(this object source)
        {
            return source.Parse<int>();
        }

        public static long ToLong(this object source)
        {
            return source.Parse<long>();
        }

        public static double ToDouble(this object source)
        {
            return source.Parse<double>();
        }

        public static decimal ToDecimal(this object source)
        {
            return source.Parse<decimal>();
        }

        public static decimal ToDecimal(this object source, int position)
        {
            return Math.Round(source.Parse<decimal>(), position);
        }

        public static float ToFloat(this object source)
        {
            return source.Parse<float>();
        }

        public static Single ToSingle(this object source)
        {
            return source.Parse<Single>();
        }

        public static short ToShort(this object source)
        {
            return source.Parse<short>();
        }

        public static char ToChar(this object source)
        {
            return source.Parse<char>();
        }

        public static string CutComma(this string anyString)
        {
            return anyString.Replace(",", "");
        }

        public static DataTable ToDataTable<T>(this List<T> sourceList) where T : class
        {
            DataTable dt = new DataTable();

            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");

                dt.Columns.Add(dc);

                foreach (T item in sourceList)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;

                    dt.Rows.Add(dr);
                }
            }

            else
            {

                PropertyInfo[] piT = typeof(T).GetProperties();

                foreach (PropertyInfo pi in piT)
                {
                    DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);

                    dt.Columns.Add(dc);
                }

                foreach (T item in sourceList)
                {
                    DataRow dr = dt.NewRow();

                    for (int property = 0; property < dt.Columns.Count; property++)
                    {
                        dr[property] = piT[property].GetValue(item, null);
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static DateTime ToDateByStringDDMMYYYY_And_Slash(this string strDate)
        {
            try
            {
                string[] ary = strDate.Split('/');
                string dd = ary[0];
                string mm = ary[1];
                string yy = ary[2];
                return new DateTime(yy.ToInt(), mm.ToInt(), dd.ToInt());
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }

        public static DateTime StringToDate(this string strDate)
        {
            DateTime resultDate = new DateTime(1, 1, 1);
            try
            {
                var res = DateTime.TryParse(strDate, out resultDate);
            }
            catch
            {

            }
            return resultDate;
        }

        public static int GetEnumValue<T>(this T anyEnum)
        {
            return (int)Enum.Parse(typeof(T), Enum.GetName(typeof(T), anyEnum));
        }

        public static string GetEnumValueString<T>(this T anyEnum)
        {
            return Enum.Parse(typeof(T), Enum.GetName(typeof(T), anyEnum)).ToString();
        }

        public static bool IsNumber(this string anyString)
        {
            return anyString.ToCharArray().All(c => char.IsNumber(c));
        }

        /// <summary>
        /// หารายการเริ่มต้นของข้อมูลในหน้า
        /// </summary>
        /// <param name="pageNo">หน้าที่</param>
        /// <param name="recordPerPage">จำนวนรายการข้อมูลต่อหน้า</param>
        /// <returns></returns>
        public static int StartRowNumber(this int pageNo, int recordPerPage)
        {
            return ((pageNo - 1) * recordPerPage) + 1;
        }

        /// <summary>
        /// หารายการสุดท้ายของข้อมูลในหน้า
        /// </summary>
        /// <param name="pageNo">หน้าที่</param>
        /// <param name="recordPerPage">จำนวนรายการข้อมูลต่อหน้า</param>
        /// <returns></returns>
        public static int ToRowNumber(this int pageNo, int recordPerPage)
        {
            return ((pageNo - 1) * recordPerPage) + recordPerPage;
        }

        /// <summary>
        /// ตรวจสอบสัญลักษณ์ ' ถ้ามีให้แทนที่ด้วย ''''
        /// </summary>
        /// <param name="anyString">ข้อความ</param>
        /// <returns>ข้อความ</returns>
        public static string ReplaceQuote(this string anyString)
        {
            return anyString.IndexOf("'") != -1 ? anyString.Replace("'", "''''") : anyString;
        }

        /// <summary>
        /// ตรวจสอบสัญลักษณ์ ' ถ้ามีให้เอาออก
        /// </summary>
        /// <param name="anyString">ข้อความ</param>
        /// <returns>ข้อความ</returns>
        public static string ClearQuote(this string anyString)
        {
            return anyString.IndexOf("'") != -1 ? anyString.Replace("'", "") : anyString;
        }

        /// <summary>
        /// สำหรับเคลียร์ " และ , ใน CSV เช่น "99,999" เป็น 99999
        /// </summary>
        /// <param name="anyStr">any string</param>
        /// <returns>string</returns>
        public static string ClearQuoteInCSV(this string anyStr)
        {
            char[] aryChar = anyStr.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int i = 0;
            bool foundQoute = false;
            while (i < aryChar.Length)
            {
                char curChar = aryChar[i];
                if (curChar != '"' && !foundQoute)
                {
                    sb.Append(curChar);
                }
                else if (curChar == '"' && !foundQoute)
                {
                    foundQoute = true;
                }
                else if (curChar == ',' && foundQoute)
                {

                }
                else if (curChar == '"' && foundQoute)
                {
                    foundQoute = false;
                }
                else
                {
                    sb.Append(curChar);
                }

                i++;
            }
            return sb.ToString();
        }

        public static string ConvertToTxtThai(string Ddate,char tempTXT)
        {
            string reDate = "";
            string[] split = Ddate.Split(tempTXT);
            if (split.Length == 3)
            {
                switch (split[1])
                {
                    case "1":
                    case "01":
                        reDate = " มกราคม ";
                        break;
                    case "2":
                    case "02":
                        reDate = " กุมภาพันธ์ ";
                        break;
                    case "3":
                    case "03":
                        reDate = " มีนาคม ";
                        break;
                    case "4":
                    case "04":
                        reDate = " เมษายน ";
                        break;
                    case "5":
                    case "05":
                        reDate = " พฤษภาคม ";
                        break;
                    case "6":
                    case "06":
                        reDate = " มิถุนายน ";
                        break;
                    case "7":
                    case "07":
                        reDate = " กรกฎาคม ";
                        break;
                    case "8":
                    case "08":
                        reDate = " สิงหาคม ";
                        break;
                    case "9":
                    case "09":
                        reDate = " กันยายน ";
                        break;
                    case "10":
                        reDate = " ตุลาคม ";
                        break;
                    case "11":
                        reDate = " พฤศจิกายน ";
                        break;
                    default:
                        reDate = " ธันวาคม ";
                        break;
                }
                reDate = split[0]  + reDate +  split[2];
            }
            return reDate;
        }
    }
}
