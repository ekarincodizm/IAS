using IAS.DataServices.Person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for PersonServiceTest and is intended
    ///to contain all PersonServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonServiceTest
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
        ///A test for SetOffLineAllStatus
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SetOffLineAllStatusTest()
        {
            PersonService target = new PersonService(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            expected.ResultMessage = true;
            actual = target.SetOffLineAllStatus(userName);
            Assert.AreEqual(expected.ResultMessage, actual.ResultMessage);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }


        /// <summary>
        ///A test for OICAuthenWithADService
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void OICAuthenWithADServiceTest()
        {
            PersonService_Accessor target = new PersonService_Accessor(); // TODO: Initialize to an appropriate value
            string ADUserName = "it-prapatu"; // TODO: Initialize to an appropriate value
            string ADPassword = "abcd12345"; // TODO: Initialize to an appropriate value
            ResponseService<OICADProperties> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<OICADProperties> actual;
            actual = target.OICAuthenWithADService(ADUserName, ADPassword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
