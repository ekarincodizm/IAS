using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class IdCard
    {
        public static bool Verify(string idCard)
        {
            bool result = false;
            string digit = string.Empty;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (idCard.ToCharArray().All(c => char.IsNumber(c)))
            {
                //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
                if (idCard.Trim().Length == 13)
                {
                    int sumValue = 0;
                    for (int i = 0; i < idCard.Length - 1; i++)
                    {
                        sumValue += int.Parse(idCard[i].ToString()) * (13 - i);
                    }

                    int v = 11 - (sumValue % 11);
                    if (v.ToString().Length == 2)
                    {
                        digit = v.ToString().Substring(1, 1);
                    }
                    else
                    {
                        digit = v.ToString();
                    }
                    result = idCard[12].ToString() == digit;
                }
            }

            return result;
        }
    }
}
