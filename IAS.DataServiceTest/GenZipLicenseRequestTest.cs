using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.License.LicenseHelpers;

namespace IAS.DataServiceTest
{
    [TestClass]
    public  class GenZipLicenseRequestTest
    {
        [TestMethod]
        public void TestZipFile_NotFound() 
        { 
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            DateTime findDate = new DateTime(2013, 10, 1);
            String zipFilename = GenZipLicenseRequest.StartCompressByPayment(ctx, findDate, "admin", "zip1");

            Assert.AreEqual(zipFilename, String.Empty);
        }

        [TestMethod]
        public void TestZipFile_Found()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            DateTime findDate = new DateTime(2013, 10, 10);
            String zipFilename = GenZipLicenseRequest.StartCompressByPayment(ctx, findDate, "admin", "zip1");

            Assert.AreNotEqual(zipFilename, String.Empty);
        }
    }
}
