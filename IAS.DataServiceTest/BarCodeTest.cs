using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class BarCodeTest
    {
        [TestMethod]
        public void TestCritiaReturn()
        {
            StringBuilder sb = new StringBuilder();

            String hexString = "0e";
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = hexString.Substring(i, 2);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }

            String cr = sb.ToString();
        }

        [TestMethod]
        public void TestCritiaReturnBy_Replece() {
            String code = "|099400064092700 999999561000000052 12102013 150000";

            String result = code.Replace(" ", System.Environment.NewLine);


        }

    }
}
