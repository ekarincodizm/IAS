using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class UserOIC_id_FormatTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            String id = "551004";

            String formatid = String.Format("{0}-{1}-{2}", id.Substring(0,2), id.Substring(2,1), id.Substring(3));

            Assert.AreEqual(formatid,"55-1-004");
        }
    }
}
