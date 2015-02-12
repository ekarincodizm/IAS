using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class TestValue
    {
        [TestMethod]
        public void Test_PhaseStringToDecimal_isvalid()
        {
            String amountText = "0000000020000";

            Decimal d = PhaseStringToDecimalPhase(amountText);

            Assert.AreEqual(d, Convert.ToDecimal(200.00));
        }
        private Decimal PhaseStringToDecimalPhase(String paymentAmountText)
        {
            return Convert.ToDecimal(paymentAmountText.Insert(paymentAmountText.Length - 2, "."));
        }


        [TestMethod]
        public void TestDupicate() 
        {
            int[] listOfItems = new[] { 4, 2, 3, 1, 6, 4, 3 };

            var duplicates = listOfItems.GroupBy(i => i).ToList();
            var dt = duplicates.Where(g => g.Count() > 1).ToList();
            var ddd = dt.Select(g => g.Key);


            Assert.AreEqual(ddd.Count(), 2);
        }


        [TestMethod]
        public void TestFileName() { 
            String text1 = @"aaaaaa\bbbbbb\aaaaa.pdf";

            String text2 = "aaaaa.pdf";

            String text3 = "";

            Assert.AreEqual("aaaaa.pdf", FilenameOnly(text1));
            Assert.AreEqual("aaaaa.pdf", FilenameOnly(text2));
            Assert.AreEqual("", FilenameOnly(text3));
        }

        private String FilenameOnly(String filename) {
            Int32 index = filename.LastIndexOf(@"\");
            return  filename.Substring((index > 0) ? index + 1 : 0);   
        }
    }
}
