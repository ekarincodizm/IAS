using IAS.DataServices.Applicant;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using System.Data;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for ApplicantServiceTest and is intended
    ///to contain all ApplicantServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ApplicantServiceTest
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
        ///A test for InsertSingleApplicant
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertSingleApplicantTest()
        {
            ApplicantService target = new ApplicantService(); // TODO: Initialize to an appropriate value
            List<ApplicantTemp> app = new List<ApplicantTemp>(); // TODO: Initialize to an appropriate value
            //List<string> testNo = new List<string>(); // TODO: Initialize to an appropriate value
            string[] testNo = new string[] { "570247","570246","570245","570244" };
            DateTime[] Edate = new DateTime[] { Convert.ToDateTime("16/06/2557"), Convert.ToDateTime("26/06/2557"), Convert.ToDateTime("25/06/2557"), Convert.ToDateTime("14/06/2557") };
            string[] examP = new string[] { "10111", "10224", "10224", "10224" };
            int xx = 4;
            for (int i = 0; i < xx; i++)
            {
                ApplicantTemp ent = new ApplicantTemp();

                //DTO.ApplicantTemp ent = new ApplicantTemp();
                    ent.TESTING_NO = testNo[i];
                    ent.TESTING_DATE = Edate[i];
                    ent.EXAM_PLACE_CODE = examP[i];
                    ent.APPLY_DATE = DateTime.Today;
                    ent.INSUR_COMP_CODE = "1006";
                    ent.USER_ID = "131101133556905";
                    ent.ID_CARD_NO = "7303043711071"; 
                    ent.RUN_NO = Convert.ToString(i + 1);
                    app.Add(ent);
                
            }

            string userId = "131101133556905"; // TODO: Initialize to an appropriate value
            ResponseService<string> expected = new ResponseService<string>(); // TODO: Initialize to an appropriate value
            ResponseService<string> actual;
            actual = target.InsertSingleApplicant(app, userId);
            Assert.AreEqual("999999570500000865", actual.DataResponse);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

//DELETE FROM AG_APPLICANT_T WHERE id_card_no = '7303043711071'
//and testing_no = '570247';
//DELETE FROM AG_APPLICANT_T WHERE id_card_no = '7303043711071'
//and testing_no = '570246';
//DELETE FROM AG_APPLICANT_T WHERE id_card_no = '7303043711071'
//and testing_no = '570245';
//DELETE FROM AG_APPLICANT_T WHERE id_card_no = '7303043711071'
//and testing_no = '570244';

//DELETE FROM ag_ias_payment_g_t WHERE CREATED_BY = '131101133556905';
//DELETE FROM ag_ias_subpayment_h_t WHERE CREATED_BY = '131101133556905';
//DELETE FROM ag_ias_subpayment_d_t WHERE USER_ID = '131101133556905';

        /// <summary>
        ///A test for GetApplicantById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantByIdTest()
        {
            ApplicantService target = new ApplicantService(); // TODO: Initialize to an appropriate value
            string applicantCode = "1"; // TODO: Initialize to an appropriate value
            string testingNo = "579906"; // TODO: Initialize to an appropriate value
            string examPlaceCode = "10111"; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantById(applicantCode, testingNo, examPlaceCode);
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
            ApplicantService target = new ApplicantService(); // TODO: Initialize to an appropriate value
            RegistrationType userRegType = DTO.RegistrationType.OIC; // TODO: Initialize to an appropriate value
            string compCode = ""; // TODO: Initialize to an appropriate value
            string idCard = ""; ; // TODO: Initialize to an appropriate value
            string testingNo = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = Convert.ToDateTime("2557/07/04");// TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = Convert.ToDateTime("2557/10/01"); // TODO: Initialize to an appropriate value
            string paymentNo = string.Empty; // TODO: Initialize to an appropriate value
            string billNo = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 1; // TODO: Initialize to an appropriate value
            int recordPerPage = 20; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            string license = string.Empty; // TODO: Initialize to an appropriate value
            string time = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceGroupCode = string.Empty; // TODO: Initialize to an appropriate value
            string examPlaceCode = string.Empty; // TODO: Initialize to an appropriate value
            string chequeNo = string.Empty; // TODO: Initialize to an appropriate value
            string examResult = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startCandidates = Convert.ToDateTime("2557/05/26"); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endCandidates = Convert.ToDateTime("2557/07/04"); // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantByCriteria(userRegType, compCode, idCard, testingNo, firstName, lastName, startDate, toDate, paymentNo, billNo, pageNo, recordPerPage, Count, license, time, examPlaceGroupCode, examPlaceCode, chequeNo, examResult, startCandidates, endCandidates);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetApplicantFromTestingNoForManageApplicant
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetApplicantFromTestingNoForManageApplicantTest()
        {
            ApplicantService target = new ApplicantService(); // TODO: Initialize to an appropriate value
            string TestingNo = "570480"; // TODO: Initialize to an appropriate value
            string ConSQL = string.Empty; // TODO: Initialize to an appropriate value
            int resultPage = 0; // TODO: Initialize to an appropriate value
            int PAGE_SIZE = 0; // TODO: Initialize to an appropriate value
            bool Count = false; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetApplicantFromTestingNoForManageApplicant(TestingNo, ConSQL, resultPage, PAGE_SIZE, Count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamRoomByTestingNoforManage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamRoomByTestingNoforManageTest()
        {
            ApplicantService target = new ApplicantService(); // TODO: Initialize to an appropriate value
            string testingNo = "570279"; // TODO: Initialize to an appropriate value
            string PlaceCode = "%"; // TODO: Initialize to an appropriate value
            ResponseService<List<DataItem>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<DataItem>> actual;
            actual = target.GetExamRoomByTestingNoforManage(testingNo, PlaceCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
