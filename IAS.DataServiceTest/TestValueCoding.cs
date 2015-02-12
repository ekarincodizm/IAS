using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class TestValueCoding
    {
        [TestMethod]
        public void TestValueCodingIndexData()
        {
            String fName = "7165527289822_04.jpg";
            String code = fName.Substring(fName.IndexOf('.') - 2, 2);

            Assert.AreEqual("04", code);
        }
    }
}
