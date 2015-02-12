using IAS.DataServices.Applicant;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Data;
using System.Collections.Generic;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for IApplicantServiceTest and is intended
    ///to contain all IApplicantServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IApplicantServiceTest
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
        internal virtual IApplicantService CreateIApplicantService()
        {
            // TODO: Instantiate an appropriate concrete class.
            ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();
            IApplicantService target = new ApplicantService(ctx);
            return target;
        }

        /// <summary>
        ///A test for ApplicantGroupUploadToSubmit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ApplicantGroupUploadToSubmitTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string groupId = string.Empty; // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.ApplicantGroupUploadToSubmit(groupId, userProfile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.Delete(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantByCriteriaTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            RegistrationType userRegType = new RegistrationType(); // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string paymentNo = string.Empty; // TODO: Initialize to an appropriate value
            string billNo = string.Empty; // TODO: Initialize to an appropriate value
            int RowPerPage = 0; // TODO: Initialize to an appropriate value
            int pageNum = 0; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            string license = string.Empty;// TODO: Initialize to an appropriate value
            string time = string.Empty;// TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty;
            string examPlaceCode  = string.Empty;
            string chequeNo  = string.Empty;
            string examResult  = string.Empty;
            Nullable<DateTime> startCandidates = new Nullable<DateTime>();
            Nullable<DateTime> endCandidates = new Nullable<DateTime>();
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantByCriteria(userRegType, compCode, idCard, testingNo, firstName, lastName, startDate, toDate, paymentNo, billNo, RowPerPage, pageNum, Count,license,time, examPlaceGroupCode,  examPlaceCode,  chequeNo,  examResult, startCandidates, endCandidates);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantByIdTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string applicantCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantById(applicantCode, testingNo, examPlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantGroupUploadByGroupUploadNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantGroupUploadByGroupUploadNoTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string groupUploadNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadHeader, ApplicantTemp>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadHeader, ApplicantTemp>> actual;
            actual = target.GetApplicantGroupUploadByGroupUploadNo(groupUploadNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantInfo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantInfoTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string applicantCode = string.Empty; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            int RowPerPage = 0; // TODO: Initialize to an appropriate value
            int num_page = 0; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            ResponseService<ApplicantInfo> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<ApplicantInfo> actual;
            actual = target.GetApplicantInfo(applicantCode, testingNo, examPlaceCode, RowPerPage, num_page, Count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantUploadTempById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantUploadTempByIdTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string uploadGroupNo = string.Empty; // TODO: Initialize to an appropriate value
            string seqNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<ApplicantTemp> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<ApplicantTemp> actual;
            actual = target.GetApplicantUploadTempById(uploadGroupNo, seqNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetByIdTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Applicant> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Applicant> actual;
            actual = target.GetById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            DTO.Applicant appl = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.Insert(appl);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertAndCheckApplicantGroupUpload
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertAndCheckApplicantGroupUploadTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            UploadData data = null; // TODO: Initialize to an appropriate value
            string fileName = string.Empty; // TODO: Initialize to an appropriate value
            RegistrationType regType = new RegistrationType(); // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            UserProfile userProfile = null; // TODO: Initialize to an appropriate value
            ResponseService<SummaryReceiveApplicant> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<SummaryReceiveApplicant> actual;
            //actual = target.InsertAndCheckApplicantGroupUpload(data, fileName, regType, testingNo,"", userProfile);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertSingleApplicant
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertSingleApplicantTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            List<ApplicantTemp> app = null; // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.InsertSingleApplicant(app, userId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            DTO.Applicant appl = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.Update(appl);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateApplicantGroupUpload
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateApplicantGroupUploadTest()
        {
            IApplicantService target = CreateIApplicantService(); // TODO: Initialize to an appropriate value
            ApplicantTemp exam = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateApplicantGroupUpload(exam);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
