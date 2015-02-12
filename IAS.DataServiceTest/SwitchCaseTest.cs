using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class SwitchCaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(ActivateLicense("01", "1378"));
            Assert.IsTrue(ActivateLicense("01", "1378"));

            Assert.IsTrue(ActivateLicense("02", "2378"));
            Assert.IsTrue(ActivateLicense("05", "2378"));
            Assert.IsTrue(ActivateLicense("06", "2378"));
            Assert.IsTrue(ActivateLicense("08", "2378"));

            Assert.IsTrue(ActivateLicense("03", "3378"));
            Assert.IsTrue(ActivateLicense("04", "3378"));

            Assert.IsFalse(ActivateLicense("01", "3378"));
            Assert.IsFalse(ActivateLicense("11", "3378"));

        }

        private Boolean ActivateLicense(String licenseTypeCode, String InsuranceCompCode)
        {
            switch (licenseTypeCode)
            {
                case "01": 
                case "07": return (InsuranceCompCode.Substring(0, 1) == "1");

                case "02":
                case "05":
                case "06":
                case "08": return (InsuranceCompCode.Substring(0, 1) == "2");

                case "03":
                case "04": return (InsuranceCompCode.Substring(0, 1) == "3");

                default:
                    return false;
            }

        }
    }
}
