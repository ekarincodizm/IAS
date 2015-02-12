using IAS.DataServices.Person;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for PersonService_RandomPasswordTest and is intended
    ///to contain all PersonService_RandomPasswordTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonService_RandomPasswordTest
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
        ///A test for GeneratePassword
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GeneratePasswordTest()
        {
            PersonService.RandomPassword target = new PersonService.RandomPassword(); // TODO: Initialize to an appropriate value
            bool useUpper = true; // TODO: Initialize to an appropriate value
            bool userLower = true; // TODO: Initialize to an appropriate value
            bool userNumbers = true; // TODO: Initialize to an appropriate value
            bool useSymbols = true; // TODO: Initialize to an appropriate value
            int passwordLength = 8; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GeneratePassword(useUpper, userLower, userNumbers, useSymbols, passwordLength);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
