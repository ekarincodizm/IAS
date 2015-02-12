using IAS.DataServices.Exam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using System.Data;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for IExamServiceTest and is intended
    ///to contain all IExamServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IExamServiceTest
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
        internal virtual IExamService CreateIExamService()
        {
            // TODO: Instantiate an appropriate concrete class.
            ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();
            IExamService target = new ExamService(ctx);
            return target;
        }

        /// <summary>
        ///A test for AddHoliday
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AddHolidayTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            GBHoliday holiday = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual ;
            actual = target.AddHoliday(holiday);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddLicenseType
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AddLicenseTypeTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            LicenseTypet licensetype = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.AddLicenseType(licensetype);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddSubject
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AddSubjectTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            Subjectr subject = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.AddSubject(subject);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CanChangeExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void CanChangeExamTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.CanChangeExam(testingNo, examPlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteExamTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.DeleteExam(testingNo, examPlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteHoliday
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteHolidayTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string date = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.DeleteHoliday(date);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteLicensetype
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteLicensetypeTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string licensecode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.DeleteLicensetype(licensecode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteSubject
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteSubjectTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            Subjectr subject = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.DeleteSubject(subject);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExamResultUploadToSubmit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ExamResultUploadToSubmitTest()
        {
            //IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            //string groupId = string.Empty; // TODO: Initialize to an appropriate value
            //string userId = string.Empty; // TODO: Initialize to an appropriate value
            //Nullable<DateTime> expireDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            //ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            //ResponseMessage<bool> actual;
            //actual = target.ExamResultUploadToSubmit(groupId, userId, expireDate);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExamResultUploadToSubmitNew
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        //public void ExamResultUploadToSubmitNewTest()
        //{
        //    IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
        //    string groupId = string.Empty; // TODO: Initialize to an appropriate value
        //    string userId = string.Empty; // TODO: Initialize to an appropriate value
        //    Nullable<DateTime> expireDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
        //    ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
        //    ResponseMessage<bool> actual;
        //    actual = target.ExamResultUploadToSubmitNew(groupId, userId, expireDate);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for GetAgentTypeList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAgentTypeListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ResponseService<List<AgentType>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<AgentType>> actual;
            actual = target.GetAgentTypeList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByCriteriaTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            string licenseTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string agentType = string.Empty; // TODO: Initialize to an appropriate value
            string yearMonth = string.Empty; // TODO: Initialize to an appropriate value
            string timeCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            int resultPage = 0; // TODO: Initialize to an appropriate value
            int PageSize = 0; // TODO: Initialize to an appropriate value
            bool CountAgain = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamByCriteria(examPlaceGroupCode, examPlaceCode, licenseTypeCode, agentType, yearMonth, timeCode, testingDate, resultPage, PageSize, CountAgain);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByCriteriaDefault
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByCriteriaDefaultTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            string licenseTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string agentType = string.Empty; // TODO: Initialize to an appropriate value
            string yearMonth = string.Empty; // TODO: Initialize to an appropriate value
            string timeCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            int resultPage = 0; // TODO: Initialize to an appropriate value
            int PageSize = 0; // TODO: Initialize to an appropriate value
            bool CountAgain = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamByCriteriaDefault(examPlaceGroupCode, examPlaceCode, licenseTypeCode, agentType, yearMonth, timeCode, testingDate, resultPage, PageSize, CountAgain);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByTestCenter
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByTestCenterTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            string licenseTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string yearMonth = string.Empty; // TODO: Initialize to an appropriate value
            string timeCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            string compcode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamByTestCenter(examPlaceGroupCode, examPlaceCode, licenseTypeCode, yearMonth, timeCode, testingDate, compcode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByTestingNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByTestingNoTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<ExamSchedule> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<ExamSchedule> actual;
            actual = target.GetExamByTestingNo(testingNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByTestingNoAndPlaceCode
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByTestingNoAndPlaceCodeTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string placeCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<ExamSchedule> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<ExamSchedule> actual;
            actual = target.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamByYearMonth
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamByYearMonthTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string yearMonth = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<DateTime>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<DateTime>> actual;
            actual = target.GetExamByYearMonth(yearMonth);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamFee
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamFeeTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetExamFee();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamMonthByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamMonthByCriteriaTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            string licenseTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string yearMonth = string.Empty; // TODO: Initialize to an appropriate value
            string timeCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingDate = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamMonthByCriteria(examPlaceGroupCode, examPlaceCode, licenseTypeCode, yearMonth, timeCode, testingDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamResultTempEdit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamResultTempEditTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string uploadGroupNo = string.Empty; // TODO: Initialize to an appropriate value
            string seqNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<ExamResultTempEdit> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<ExamResultTempEdit> actual;
            actual = target.GetExamResultTempEdit(uploadGroupNo, seqNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamResultUploadByGroupId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamResultUploadByGroupIdTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string groupId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadHeader, ExamResultTemp>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadHeader, ExamResultTemp>> actual;
            actual = target.GetExamResultUploadByGroupId(groupId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamRoom
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamRoomTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamRoom();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHoliday
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetHolidayTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string date = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<GBHoliday> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<GBHoliday> actual;
            actual = target.GetHoliday(date);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHolidayList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetHolidayListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            int page = 0; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            ResponseService<List<GBHoliday>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<GBHoliday>> actual;
            actual = target.GetHolidayList(page, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetLicenseList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetLicenseListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string agentType = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<LicenseTypet>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<LicenseTypet>> actual;
            actual = target.GetLicenseList(agentType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetLicensetypeList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetLicensetypeListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ResponseService<List<LicenseTyperDropDrown>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<LicenseTyperDropDrown>> actual;
            actual = target.GetLicensetypeList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSeatAmount
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSeatAmountTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.GetSeatAmount(examPlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubject_List
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSubject_ListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string lic_type_code = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetSubject_List(lic_type_code);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubjectrList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetSubjectrListTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string licensecode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<Subjectr>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<Subjectr>> actual;
            actual = target.GetSubjectrList(licensecode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertAndCheckExamResultUpload
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertAndCheckExamResultUploadTest()
        {
            //IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            //UploadData data = null; // TODO: Initialize to an appropriate value
            //string fileName = string.Empty; // TODO: Initialize to an appropriate value
            //string userId = string.Empty; // TODO: Initialize to an appropriate value
            //ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>> expected = null; // TODO: Initialize to an appropriate value
            //ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>> actual;
            //actual = target.InsertAndCheckExamResultUpload(data, fileName, userId, "");
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertExamTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ExamSchedule ent = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.InsertExam(ent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertExamRoom
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertExamRoomTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ConfigExamRoom ent = null; // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.InsertExamRoom(ent, userProfile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsRightTestingNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void IsRightTestingNoTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.IsRightTestingNo(testingNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SearchHoliday
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SearchHolidayTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            string search = string.Empty; // TODO: Initialize to an appropriate value
            int page = 0; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            ResponseService<List<GBHoliday>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<GBHoliday>> actual;
            actual = target.SearchHoliday(search, page, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateExam
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateExamTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ExamSchedule ent = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateExam(ent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateExamResultEdit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateExamResultEditTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ExamResultTempEdit exam = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateExamResultEdit(exam);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateExamRoom
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateExamRoomTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            ConfigExamRoom ent = null; // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateExamRoom(ent, userProfile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateHoliday
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateHolidayTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            GBHoliday holidate = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.UpdateHoliday(holidate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateLicenseType
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateLicenseTypeTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            LicenseTypet licensetype = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.UpdateLicenseType(licensetype);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateSubject
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateSubjectTest()
        {
            IExamService target = CreateIExamService(); // TODO: Initialize to an appropriate value
            Subjectr subject = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.UpdateSubject(subject);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
