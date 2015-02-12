using IAS.DataServices.DataCenter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for DataCenterServiceTest and is intended
    ///to contain all DataCenterServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataCenterServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetTitleNameFromSex
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetTitleNameFromSexTest()
        {

            DataCenterService target = new DataCenterService(); // TODO: Initialize to an appropriate value
            string sex = "M"; // TODO: Initialize to an appropriate value
            ResponseService<List<TitleName>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<TitleName>> actual;
            actual = target.GetTitleNameFromSex(sex);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSpecialDocType
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSpecialDocTypeTest()
        {
            DataCenterService target = new DataCenterService(); // TODO: Initialize to an appropriate value
            string docStatus = "A"; // TODO: Initialize to an appropriate value
            string trainStatus = "Y"; // TODO: Initialize to an appropriate value
            ResponseService<List<SpecialDocument>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<SpecialDocument>> actual;
            actual = target.GetSpecialDocType(docStatus, trainStatus);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPlaceExamNameById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPlaceExamNameByIdTest()
        {
            DataCenterService target = new DataCenterService(); // TODO: Initialize to an appropriate value
            string placeExamCode = "10111"; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetPlaceExamNameById(placeExamCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAcceptOffNameById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAcceptOffNameByIdTest()
        {
            DataCenterService target = new DataCenterService(); // TODO: Initialize to an appropriate value
            string acceptOffCode = "111"; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetAcceptOffNameById(acceptOffCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCompanyNameByIdToText
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\AIS_NEW_NEW_NEW\\WebService\\IAS.DataServices", "/")]
        [UrlToTest("http://localhost:9999/")]
        public void GetCompanyNameByIdToTextTest()
        {
            DataCenterService target = new DataCenterService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetCompanyNameByIdToText(compCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
