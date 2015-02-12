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
    ///This is a test class for ExamServiceTest and is intended
    ///to contain all ExamServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExamServiceTest
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
        ///A test for ExamResultUploadToSubmitNew
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ExamResultUploadToSubmitNewTest()
        {
            ExamService target = new ExamService(); // TODO: Initialize to an appropriate value
            string groupId = "140610115925558"; // TODO: Initialize to an appropriate value
            string userId = "130923093821787"; // TODO: Initialize to an appropriate value
            Nullable<DateTime> expireDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            expireDate = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", "10/6/2557"));
            string TestingNo = "570242"; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            expected.ResultMessage = true;
            ResponseMessage<bool> actual;
            actual = target.ExamResultUploadToSubmitNew(groupId, userId, expireDate, TestingNo);
            Assert.AreEqual(expected.ResultMessage, actual.ResultMessage);
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
            ExamService target = new ExamService(); // TODO: Initialize to an appropriate value
            UploadData data = new UploadData(); // TODO: Initialize to an appropriate value

            data.Body = new System.Collections.Generic.List<string>();
            data.Header = string.Empty;
            data.Body.Add("1|3|3250700270577|นาย|อริยะ |สาพิมพ์|123 หมู่ 11||10|12/03/2533|M|05||28/05/2014|P|20|70|25|25|");
            data.Header ="Hสมาคม [111]                                                                     031022427/06/25575601";

            string fileName = "exam_result_10062014_03.txt"; // TODO: Initialize to an appropriate value
            string userId = "130923093821787"; // TODO: Initialize to an appropriate value
            string Default_TESTING_NO = "570242"; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadResultHeader, ExamResultTemp>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<UploadResult<UploadResultHeader, ExamResultTemp>> actual;
            actual = target.InsertAndCheckExamResultUpload(fileName, userId, Default_TESTING_NO);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamRoomByPlaceCodeAndTimeCode
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamRoomByPlaceCodeAndTimeCodeTest()
        {
            ExamService target = new ExamService(); // TODO: Initialize to an appropriate value
            string code = "10111"; // TODO: Initialize to an appropriate value
            string Timetxt = "06.00-07.00"; // TODO: Initialize to an appropriate value
            string dDate = "26/06/2557"; // TODO: Initialize to an appropriate value


            List<ExamSubLicense> oldCode = new List<ExamSubLicense>();
            //oldCode.Add(new ExamSubLicense
            //{
            //    EXAM_ROOM_CODE = "1019",
            //    NUMBER_SEAT_ROOM = 2,
            //    ROOM_NAME = "อาคารช่วงเกษตรศิลป์",
            //    SEAT_AMOUNT = 3,
            //    TESTING_NO ="570442" ,
            //    USER_DATE = DateTime.Now,
            //    USER_ID="130923093821787",                
            //});

            //oldCode.Add(new ExamSubLicense
            //{
            //    EXAM_ROOM_CODE = "102",
            //    NUMBER_SEAT_ROOM = 58,
            //    ROOM_NAME = "ห้องสอบของคปภ.01",
            //    SEAT_AMOUNT = 90,
            //    TESTING_NO = "570442",
            //    USER_DATE = DateTime.Now,
            //    USER_ID = "130923093821787",
            //});

            oldCode.Add(new ExamSubLicense
            {
                EXAM_ROOM_CODE = "104",
                NUMBER_SEAT_ROOM = 40,
                ROOM_NAME = "ห้องสอบที่ 104",
                SEAT_AMOUNT = 100,
                TESTING_NO = "570442",
                USER_DATE = DateTime.Now,
                USER_ID = "130923093821787",
            });

            oldCode.Add(new ExamSubLicense
            {
                EXAM_ROOM_CODE = "111111",
                NUMBER_SEAT_ROOM = 50,
                ROOM_NAME = "ห้องสอบของคปภ.",
                SEAT_AMOUNT = 60,
                TESTING_NO = "570442",
                USER_DATE = DateTime.Now,
                USER_ID = "130923093821787",
            });

            bool Del = true; // TODO: Initialize to an appropriate value
            string testingNoo = "570442"; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetExamRoomByPlaceCodeAndTimeCode(code, Timetxt, dDate, oldCode, Del, testingNoo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
