using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        public void DateTimeCompair()
        {
            DateTime d1 = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);

            Assert.AreEqual(d1.Date, DateTime.Now.Date);
        }
    }
}
