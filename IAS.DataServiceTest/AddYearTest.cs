using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class AddYearTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DateTime currdate = new DateTime(2012, 5, 29);

            DateTime nextdate = currdate.AddYears(1).AddSeconds(-1);

            Assert.AreEqual(nextdate, new DateTime(2013, 5, 28,23,59,59));

        }
    }
}
