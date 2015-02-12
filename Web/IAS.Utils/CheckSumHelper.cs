using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class CheckSumHelper
    {
        public static bool Verify(String source)
        {
            Boolean result = false;
            String digit = String.Empty;
            Int32 digitLength = source.Length;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (source.ToCharArray().All(c => char.IsNumber(c)))
            {
                //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
                if (source.Trim().Length == digitLength)
                {
                    Int32 sumValue = 0;
                    for (Int32 i = 0; i < source.Length - 1; i++)
                    {
                        sumValue += Int32.Parse(source[i].ToString()) * (digitLength - i);
                    }

                    Int32 v = 11 - (sumValue % 11);
                    if (v.ToString().Length == 2)
                    {
                        digit = v.ToString().Substring(1, 1);
                    }
                    else
                    {
                        digit = v.ToString();
                    }
                    result = source[digitLength - 1].ToString() == digit;
                }
            }

            return result;
        }

        public static String CheckSum(String source)
        {
            String digit = string.Empty;
            Int32 digitLength = source.Length;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (source.ToCharArray().All(c => char.IsNumber(c)))
            {
                //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
                if (source.Trim().Length == digitLength)
                {
                    Int32 sumValue = 0;
                    for (Int32 i = 0; i < source.Length - 1; i++)
                    {
                        sumValue += Int32.Parse(source[i].ToString()) * (digitLength - i);
                    }

                    Int32 v = 11 - (sumValue % 11);
                    if (v.ToString().Length >= 2)
                    {
                        digit = v.ToString().Substring(1, 1);
                    }
                    else
                    {
                        digit = v.ToString();
                    }
                    return digit;
                }
            }

            return digit;
        }
    }
}
