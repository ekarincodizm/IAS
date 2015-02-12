using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.Utils;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class CheckSumPaymentNumberTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(Verify("3439900168454"));

            String number  = "99999956100000177";
            String chksum = CheckSumHelper.CheckSum(number);

            Assert.IsTrue(CheckSumHelper.Verify(number + chksum));

        }

        public bool Verify(string idCard)
        {
            bool result = false;
            string digit = string.Empty;
            int digitLength = idCard.Length; 

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (idCard.ToCharArray().All(c => char.IsNumber(c)))
            {
                //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
                if (idCard.Trim().Length == digitLength)
                {
                    int sumValue = 0;
                    for (int i = 0; i < idCard.Length - 1; i++)
                    {
                        sumValue += int.Parse(idCard[i].ToString()) * (digitLength - i);
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
                    result = idCard[digitLength-1].ToString() == digit;
                }
            }

            return result;
        }

        public String CheckSum(string idCard)    
        {
            bool result = false;
            string digit = string.Empty;
            int digitLength = idCard.Length;

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            if (idCard.ToCharArray().All(c => char.IsNumber(c)))
            {
                //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร
                if (idCard.Trim().Length == digitLength)
                {
                    int sumValue = 0;
                    for (int i = 0; i < idCard.Length - 1; i++)
                    {
                        sumValue += int.Parse(idCard[i].ToString()) * (digitLength - i);
                    }

                    int v = 11 - (sumValue % 11);
                    if (v.ToString().Length >= 2)
                    {
                        digit = v.ToString().Substring(1, 1);
                    }
                    else
                    {
                        digit = v.ToString();
                    }
                    return digit;
                    //result = idCard[digitLength - 1].ToString() == digit;
                }
            }

            return digit;
        }
    }
}
