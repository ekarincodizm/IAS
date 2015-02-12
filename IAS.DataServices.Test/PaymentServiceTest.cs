using IAS.DataServices.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using System.Data;
using IAS.Utils;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for PaymentServiceTest and is intended
    ///to contain all PaymentServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PaymentServiceTest
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
        ///A test for GetExamHistoryByID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamHistoryByIDTest()
        {
            PaymentService_Accessor target = new PaymentService_Accessor(); // TODO: Initialize to an appropriate value
            string idCard = "3500700477390"; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            
            string actual;
            ExamHistory ex = new ExamHistory();
            expected = "502123";
            actual = target.GetExamHistoryByID(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Insert12T
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Insert12TTest()
        {
            PaymentService target = new PaymentService(); // TODO: Initialize to an appropriate value
            List<GenLicense> GenLicense = new List<GenLicense>(); // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;

            int count = 3;
            for (int i = 0; i < count; i++)
            {
                if (i == 1)
                {
                    DTO.GenLicense ent = new GenLicense
                    {
                        UPLOAD_GROUP_NO = "140606113652129",
                        SEQ_NO = "0001",
                        PAYMENT_NO = "",
                        HEAD_REQUEST_NO = "",
                        USER_ID = "130923093821787"
                    };
                    GenLicense.Add(ent);
                }
                else if(i == 2)
                {
                    DTO.GenLicense ent = new GenLicense
                    {
                        UPLOAD_GROUP_NO = "131030140236506",
                        SEQ_NO = "0001",
                        PAYMENT_NO = "",
                        HEAD_REQUEST_NO = "",
                        USER_ID = "130923093821787"
                    };
                    GenLicense.Add(ent);
                }
                else
                {
                    DTO.GenLicense ent = new GenLicense
                    {
                        UPLOAD_GROUP_NO = "131114153339591",
                        SEQ_NO = "0002",
                        PAYMENT_NO = "",
                        HEAD_REQUEST_NO = "",
                        USER_ID = "130923093821787"
                    };
                    GenLicense.Add(ent);
                }

                
            }



            actual = target.Insert12T(GenLicense);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Auto_CancelAppNoPay
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
       
        public void Auto_CancelAppNoPayTest()
        {
            PaymentService target = new PaymentService(); // TODO: Initialize to an appropriate value
            DateTime stDate = DateTime.Now.AddDays(-10); // TODO: Initialize to an appropriate value
            DateTime enDate = DateTime.Now; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.Auto_CancelAppNoPay(stDate, enDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentDetailByGroup
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentDetailByGroupTest()
        {
            PaymentService target = new PaymentService(); // TODO: Initialize to an appropriate value
            int type = 0; // TODO: Initialize to an appropriate value
            string Gcode = "111"; // TODO: Initialize to an appropriate value
            string Ccode = "10111"; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = Convert.ToDateTime("20140704"); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = Convert.ToDateTime("20140704"); // TODO: Initialize to an appropriate value
            int RowPerPage = 20; // TODO: Initialize to an appropriate value
            int num_page = 1; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            string CompCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentDetailByGroup(type, Gcode, Ccode, startDate, toDate, RowPerPage, num_page, Count, CompCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantNoPaymentHeadder
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantNoPaymentHeadderTest()
        {
            PaymentService target = new PaymentService(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = Convert.ToDateTime("2557/07/03"); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = Convert.ToDateTime("2557/07/07"); // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            string testNo = "%"; // TODO: Initialize to an appropriate value
            string ExamPlaceCode = "%";  // TODO: Initialize to an appropriate value
            int resultPage = 1; // TODO: Initialize to an appropriate value
            int PageSize = 20; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            bool IsAuto = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantNoPaymentHeadder(startDate, toDate, testingDate, testNo, ExamPlaceCode, resultPage, PageSize, Count, IsAuto);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

       

        /// <summary>
        ///A test for GetApplicantNoPayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantNoPaymentTest()
        {
            PaymentService target = new PaymentService(); // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            string testing_no = ""; // TODO: Initialize to an appropriate value
            string examPlace = ""; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = Convert.ToDateTime("2557/07/03"); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = Convert.ToDateTime("2557/07/07"); // TODO: Initialize to an appropriate value
            string GroupNo = "999999570700000006"; // TODO: Initialize to an appropriate value
            int resultPage = 1; // TODO: Initialize to an appropriate value
            int PageSizeDetail = 20; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantNoPayment(testingDate, testing_no, examPlace, startDate, toDate, GroupNo, resultPage, PageSizeDetail, Count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

      
    }
}
