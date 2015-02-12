using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.Utils.Helpers;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class TestGuidOracleAndDotNet
    {
        [TestMethod]
        public void TestGuid() {
            Guid guid = Guid.NewGuid();

            String strGuid = guid.ToString();

            Guid newGuid = new Guid(strGuid);

            Assert.AreEqual(guid, newGuid);
        }

        [TestMethod]
        public void TestOracleRaw16AndGuid() 
        {
            Guid guid = Guid.NewGuid();

            String guidString = guid.ToString();

            String raw16 = GuidOracleHelper.ConventGuidDotNetToOracle(guidString);

            String str = GuidOracleHelper.ConventGuidOracleToDotNet(raw16);

            Guid newGuid = new Guid(str);

            Assert.AreEqual(guid, newGuid);

        }
    }
}
