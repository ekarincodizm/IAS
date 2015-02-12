using IAS.DataServices.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using System.Data;
using IAS.DAL;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for IPaymentServiceTest and is intended
    ///to contain all IPaymentServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IPaymentServiceTest
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

        IAS.DAL.Interfaces.IIASPersonEntities ctx;
        internal virtual IPaymentService CreateIPaymentService()
        {
            // TODO: Instantiate an appropriate concrete class.
            ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();
            IPaymentService target = new PaymentService(ctx);
            return target;
        }

        /// <summary>
        ///A test for AddStatusReceiveCompletetoDB
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AddStatusReceiveCompletetoDBTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string HeadNo = string.Empty; // TODO: Initialize to an appropriate value
            string UID = string.Empty; // TODO: Initialize to an appropriate value
            string strPath = string.Empty; // TODO: Initialize to an appropriate value
            string IDcard = string.Empty; // TODO: Initialize to an appropriate value
            string hashing = string.Empty; // TODO: Initialize to an appropriate value
            Guid G = new Guid(); // TODO: Initialize to an appropriate value
            string receiveNo = string.Empty;
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            Int64 FileSize = 0;
            string actual;
            actual = target.AddStatusReceiveCompletetoDB(HeadNo, UID, strPath, IDcard, hashing, G, receiveNo,FileSize);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CancelApplicantsHeader
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CancelApplicantsHeaderTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<AppNoPay> GroupNo = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.CancelApplicantsHeader(GroupNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreatePayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CreatePaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<DTO.OrderInvoice> reqList = null; // TODO: Initialize to an appropriate value
            string remark = string.Empty; // TODO: Initialize to an appropriate value
            string paymentId = string.Empty; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string groupRequestNo = string.Empty; // TODO: Initialize to an appropriate value
            string groupRequestNoExpected = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.CreatePayment(reqList, remark, paymentId, userId, compCode, out groupRequestNo);
            Assert.AreEqual(groupRequestNoExpected, groupRequestNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreatePdf
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CreatePdfTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string[] fileNames = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.CreatePdf(fileNames);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateReferanceNumber
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CreateReferanceNumberTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            ResponseService<DTO.ReferanceNumber> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.ReferanceNumber> actual;
            actual = target.CreateReferanceNumber();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateZip
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CreateZipTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string parthpdf = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.CreateZip(parthpdf);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GenPaymentNumber
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GenPaymentNumberTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string paymentCodestring = string.Empty; // TODO: Initialize to an appropriate value
            string UID = string.Empty; // TODO: Initialize to an appropriate value
            string receiptNo = string.Empty;
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.GenPaymentNumber(paymentCodestring, UID, receiptNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GenPaymentNumberTable
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GenPaymentNumberTableTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string paymentCodestring = string.Empty; // TODO: Initialize to an appropriate value
            string CountRecord = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GenPaymentNumberTable(compCode, startDate, toDate, paymentCodestring, CountRecord, pageNo, recordPerPage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAllGroupPayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAllGroupPaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string paymentCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetAllGroupPayment(compCode, startDate, toDate, paymentCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantNoPayById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantNoPayByIdTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string applicantCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantNoPayById(applicantCode, testingNo, examPlaceCode);
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
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string GroupNo = string.Empty; // TODO: Initialize to an appropriate value
            int resultPage = 0; // TODO: Initialize to an appropriate value
            int PageSizeDetail = 0; // TODO: Initialize to an appropriate value
            string testing_no = "";
            string examPlace = "";
            string testingDate = "";
            bool Count = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantNoPayment( testingDate, testing_no , examPlace,startDate, toDate, GroupNo, resultPage, PageSizeDetail, Count);
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
            //IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            //Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            //Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            //int resultPage = 0; // TODO: Initialize to an appropriate value
            //int PageSize = 0; // TODO: Initialize to an appropriate value
            //bool Count = false; // TODO: Initialize to an appropriate value
            //ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            //ResponseService<DataSet> actual;
            //actual = target.GetApplicantNoPaymentHeadder(startDate, toDate, resultPage, PageSize, Count);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBillNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetBillNoTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string user_id = string.Empty; // TODO: Initialize to an appropriate value
            string doc_date = string.Empty; // TODO: Initialize to an appropriate value
            string doc_code = string.Empty; // TODO: Initialize to an appropriate value
            string doc_type = string.Empty; // TODO: Initialize to an appropriate value
            string date_mode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetBillNo(user_id, doc_date, doc_code, doc_type, date_mode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCountPaymentDetailByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetCountPaymentDetailByCriteriaTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            string paymentType = string.Empty; // TODO: Initialize to an appropriate value
            string order = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string billNo = string.Empty; // TODO: Initialize to an appropriate value
            string Year = string.Empty;
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetCountPaymentDetailByCriteria(userProfile, paymentType, order, startDate, toDate, idCard, billNo,Year);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDataFromSub_D_T
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetDataFromSub_D_TTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string HeadNo = string.Empty; // TODO: Initialize to an appropriate value
            string UID = string.Empty; // TODO: Initialize to an appropriate value
            string HeadOrDetail = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetDataFromSub_D_T(HeadNo, UID, HeadOrDetail);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDataPayment_BeforeSentToReport
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetDataPayment_BeforeSentToReportTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string H_req_no = string.Empty; // TODO: Initialize to an appropriate value
            string IDcard = string.Empty; // TODO: Initialize to an appropriate value
            string PayNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetDataPayment_BeforeSentToReport(H_req_no, IDcard, PayNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDetailSubPayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetDetailSubPaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string hearReqNo = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string CountRecord = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetDetailSubPayment(hearReqNo, pageNo, recordPerPage, CountRecord);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamCode
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamCodeTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string code = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamCode(code);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetGroupExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetGroupExamTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            int type = 0; // TODO: Initialize to an appropriate value
            string code = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetGroupExam(type, code);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetGroupPayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetGroupPaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> EndDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string UserT = string.Empty; // TODO: Initialize to an appropriate value
            string CompanyCode = string.Empty;
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string Count = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetGroupPayment(compCode, startDate, EndDate, UserT,CompanyCode, pageNo, recordPerPage, Count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetGroupPaymentByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetGroupPaymentByCriteriaTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string paymentCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetGroupPaymentByCriteria(compCode, startDate, toDate, paymentCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetLicenseGroupRequestPaid
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetLicenseGroupRequestPaidTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            RangeDateRequest request = null; // TODO: Initialize to an appropriate value
            ResponseService<IEnumerable<DateTime>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<IEnumerable<DateTime>> actual;
            actual = target.GetLicenseGroupRequestPaid(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentByCriteriaTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            string paymentType = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string billNo = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentByCriteria(userProfile, paymentType, startDate, toDate, idCard, billNo, pageNo, recordPerPage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentDetail
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentDetailTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string applicantCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlace_code = string.Empty; // TODO: Initialize to an appropriate value
            string licenseNo = string.Empty; // TODO: Initialize to an appropriate value
            string renewTime = string.Empty; // TODO: Initialize to an appropriate value
            bool isApplicant = false; // TODO: Initialize to an appropriate value
            ResponseService<PaymentDetail> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<PaymentDetail> actual;
            actual = target.GetPaymentDetail(applicantCode, testingNo, examPlace_code, licenseNo, renewTime, isApplicant);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentDetailByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentDetailByCriteriaTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            string paymentType = string.Empty; // TODO: Initialize to an appropriate value
            string order = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string billNo = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string Year = string.Empty;
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentDetailByCriteria(userProfile, paymentType, order, startDate, toDate, idCard, billNo, pageNo, recordPerPage,Year);
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
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            int type = 0; // TODO: Initialize to an appropriate value
            string Gcode = string.Empty; // TODO: Initialize to an appropriate value
            string Ccode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            int RowPerPage = 0; // TODO: Initialize to an appropriate value
            int num_page = 0; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            string CompCode ="111";
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentDetailByGroup(type, Gcode, Ccode, startDate, toDate, RowPerPage, num_page, Count,CompCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentExpireDay
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentExpireDayTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentExpireDay();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPaymentLicenseAppove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPaymentLicenseAppoveTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string petitonType = string.Empty; // TODO: Initialize to an appropriate value
            string IdCard = string.Empty; // TODO: Initialize to an appropriate value
            string groupNo = string.Empty; // TODO: Initialize to an appropriate value
            DateTime startDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime endDate = new DateTime(); // TODO: Initialize to an appropriate value
            String firstName = String.Empty;
            String lastName = String.Empty;
            string CountPage = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPaymentLicenseAppove(petitonType, IdCard, groupNo, startDate, endDate, firstName, lastName, CountPage, pageNo, recordPerPage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRcvHisDetail
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetRcvHisDetailTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string RcvId = string.Empty; // TODO: Initialize to an appropriate value
            string EventCode = string.Empty; // TODO: Initialize to an appropriate value
            string st_num = string.Empty; // TODO: Initialize to an appropriate value
            string en_num = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetRcvHisDetail(RcvId, EventCode, st_num, en_num);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetReportNumberPrintBill
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetReportNumberPrintBillTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string petitionTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            int resultPage = 0; // TODO: Initialize to an appropriate value
            int PageSize = 0; // TODO: Initialize to an appropriate value
            bool CountAgain = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetReportNumberPrintBill(idCard, petitionTypeCode, firstName, lastName, resultPage, PageSize, CountAgain);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSinglePayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSinglePaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> StartExamDate = new Nullable<DateTime>();
            Nullable<DateTime> EndExamDate = new Nullable<DateTime>();
            string LicenseTypeCode = string.Empty;
            string TestingNo = string.Empty;
            string paymentCode = string.Empty; // TODO: Initialize to an appropriate value
            string para = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string Totalrecoad = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetSinglePayment(compCode, startDate, toDate, paymentCode,StartExamDate, EndExamDate
                                            , LicenseTypeCode, TestingNo, para, pageNo, recordPerPage, Totalrecoad);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubGroup
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSubGroupTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string paymentType = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            string Comcode = string.Empty;
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string CountTotalRecord = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetSubGroup(paymentType, startDate, toDate, userProfile,Comcode, pageNo, recordPerPage, CountTotalRecord);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubPaymentByHeaderRequestNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSubPaymentByHeaderRequestNoTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string hearReqNo = string.Empty; // TODO: Initialize to an appropriate value
            string CountRecord = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetSubPaymentByHeaderRequestNo(hearReqNo, CountRecord, pageNo, recordPerPage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubPaymentHeadByHeadRequestNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSubPaymentHeadByHeadRequestNoTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string headReqNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<SubPaymentHead> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<SubPaymentHead> actual;
            actual = target.GetSubPaymentHeadByHeadRequestNo(headReqNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTempBankTransDetail
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetTempBankTransDetailTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string headerId = string.Empty; // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<BankTransDetail> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<BankTransDetail> actual;
            actual = target.GetTempBankTransDetail(headerId, Id);
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
            AG_IAS_SUBPAYMENT_D_T subpayment = new AG_IAS_SUBPAYMENT_D_T() { 
                        
            };

            ctx.AG_IAS_SUBPAYMENT_D_T.AddObject(subpayment);


            ctx.SaveChanges();

            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<DTO.GenLicense> GroupRequestNo = new List<GenLicense>() ; // TODO: Initialize to an appropriate value
            DTO.GenLicense genLicense = new GenLicense() { 
                            
                        };
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;



            actual = target.Insert12T(GroupRequestNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertAndCheckPaymentUpload
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertAndCheckPaymentUploadTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            UploadData data = null; // TODO: Initialize to an appropriate value
            string fileName = string.Empty; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<SummaryBankTransaction, BankTransaction>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<SummaryBankTransaction, BankTransaction>> actual;
            actual = target.InsertAndCheckPaymentUpload(data, fileName, userId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NewCreatePayment
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void NewCreatePaymentTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<DTO.OrderInvoice> reqList = null; // TODO: Initialize to an appropriate value
            string remark = string.Empty; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string dayExp = string.Empty; // TODO: Initialize to an appropriate value
            string groupRequestNo = string.Empty; // TODO: Initialize to an appropriate value
            string groupRequestNoExpected = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.NewCreatePayment(reqList, remark, userId, compCode, dayExp, out groupRequestNo);
            Assert.AreEqual(groupRequestNoExpected, groupRequestNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PlusPrintDownloadCount
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PlusPrintDownloadCountTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<SubPaymentDetail> subPaymentDetail = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.PlusPrintDownloadCount(subPaymentDetail);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrintDownloadCount
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PrintDownloadCountTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<SubPaymentDetail> subPaymentDetail = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            string id_card = string.Empty;
            string createby = string.Empty;
            actual = target.PrintDownloadCount(subPaymentDetail,id_card,createby);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetSubGroup
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SetSubGroupTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<OrderInvoice> subGroups = null; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string compCodestring = string.Empty; // TODO: Initialize to an appropriate value
            string typeUser = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.SetSubGroup(subGroups, userId, compCodestring, typeUser);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetSubGroupSingleLicense
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SetSubGroupSingleLicenseTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<SubGroupPayment> subGroups = null; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string groupHeaderNo = string.Empty; // TODO: Initialize to an appropriate value
            string groupHeaderNoExpected = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.SetSubGroupSingleLicense(subGroups, userId, compCode, out groupHeaderNo);
            Assert.AreEqual(groupHeaderNoExpected, groupHeaderNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Signature_Img
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Signature_ImgTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string imgPath = string.Empty; // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.Signature_Img(imgPath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SubmitBankTrans
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SubmitBankTransTest()
        {
            //IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            //string groupId = string.Empty; // TODO: Initialize to an appropriate value
            //string userOICId = string.Empty; // TODO: Initialize to an appropriate value
            //ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            //ResponseService<string> actual;
            //actual = target.SubmitBankTrans(groupId, userOICId);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateCountDownload
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateCountDownloadTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string UserId = string.Empty; // TODO: Initialize to an appropriate value
            object FileName = null; // TODO: Initialize to an appropriate value
            string Event = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateCountDownload(UserId, FileName, Event);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdatePaymentExpireDay
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdatePaymentExpireDayTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            List<ConfigPaymentExpireDay> ls = null; // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdatePaymentExpireDay(ls, userProfile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Zip_PrintDownloadCount
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Zip_PrintDownloadCountTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string[] rcvPath = null; // TODO: Initialize to an appropriate value
            string EventZip = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            string id_card = string.Empty;
            string createby = string.Empty;
            actual = target.Zip_PrintDownloadCount(rcvPath, EventZip,id_card,createby);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getBindbillPaymentExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void getBindbillPaymentExamTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string groupRequestNo = string.Empty; // TODO: Initialize to an appropriate value
            string testNo = string.Empty;
            string appCode = string.Empty;
            string examPlaceCode = string.Empty;
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.getBindbillPaymentExam(groupRequestNo, testNo, appCode, examPlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getGroupDetail
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void getGroupDetailTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string group_reuest = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.getGroupDetail(group_reuest);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getNamePaymentBy
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void getNamePaymentByTest()
        {
            IPaymentService target = CreateIPaymentService(); // TODO: Initialize to an appropriate value
            string group_reuest = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.getNamePaymentBy(group_reuest);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
