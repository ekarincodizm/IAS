using IAS.DataServices.Person;
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
    ///This is a test class for IPersonServiceTest and is intended
    ///to contain all IPersonServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IPersonServiceTest
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
        internal virtual IPersonService CreateIPersonService()
        {
            // TODO: Instantiate an appropriate concrete class.
            ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();
            IPersonService target = new PersonService(ctx);
            return target;
        }

        /// <summary>
        ///A test for Authentication
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AuthenticationTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool IsOIC = false; // TODO: Initialize to an appropriate value
            ResponseService<UserProfile> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<UserProfile> actual;
            actual = target.Authentication(userName, password, IsOIC,"");
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ChangePasswordTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string oldPassword = string.Empty; // TODO: Initialize to an appropriate value
            string newPassword = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.ChangePassword(userId, oldPassword, newPassword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangePasswordTime
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ChangePasswordTimeTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangePasswordTime(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EditPerson
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void EditPersonTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            PersonTemp tmpPerson = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.EditPerson(tmpPerson);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ForgetPasswordRequest
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ForgetPasswordRequestTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.ForgetPasswordRequest(username, email);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttatchFileByPersonId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAttatchFileByPersonIdTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string personId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> actual;
            actual = target.GetAttatchFileByPersonId(personId);
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
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> actual;
            actual = target.GetById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDataTo8Report
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetDataTo8ReportTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string ID = string.Empty; // TODO: Initialize to an appropriate value
            string license_code = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetDataTo8Report(ID, license_code);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPersonByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPersonByCriteriaTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> starDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string IdCard = string.Empty; // TODO: Initialize to an appropriate value
            string memberTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string status = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string para = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPersonByCriteria(firstName, lastName, starDate, toDate, IdCard, memberTypeCode, email, compCode, status, pageNo, recordPerPage, para);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPersonByCriteriaAtPage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPersonByCriteriaAtPageTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            string IdCard = string.Empty; // TODO: Initialize to an appropriate value
            string memberTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string status = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPersonByCriteriaAtPage(firstName, lastName, IdCard, memberTypeCode, email, compCode, status, pageNo, recordPerPage);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPersonTemp
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPersonTempTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<PersonTemp> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<PersonTemp> actual;
            actual = target.GetPersonTemp(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPersonTempEditByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetPersonTempEditByCriteriaTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> starDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> toDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            string IdCard = string.Empty; // TODO: Initialize to an appropriate value
            string memberTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string status = string.Empty; // TODO: Initialize to an appropriate value
            int pageNo = 0; // TODO: Initialize to an appropriate value
            int recordPerPage = 0; // TODO: Initialize to an appropriate value
            string para = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetPersonTempEditByCriteria(firstName, lastName, starDate, toDate, IdCard, memberTypeCode, email, compCode, status, pageNo, recordPerPage, para);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetStatisticResetPassword
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetStatisticResetPasswordTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetStatisticResetPassword(idCard, firstName, lastName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTempAttatchFileByPersonId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetTempAttatchFileByPersonIdTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string personId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> actual;
            actual = target.GetTempAttatchFileByPersonId(personId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserProfile
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetUserProfileTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            string memType = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> actual;
            actual = target.GetUserProfile(Id, memType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserProfileAttatchFileByPersonId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetUserProfileAttatchFileByPersonIdTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string personId = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonAttatchFile>> actual;
            actual = target.GetUserProfileAttatchFileByPersonId(personId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserProfileById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetUserProfileByIdTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> actual;
            actual = target.GetUserProfileById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserProfileByUsername
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetUserProfileByUsernameTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            actual = target.GetUserProfileByUsername(userName, email);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertOIC
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertOICTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string oicEmpNo = string.Empty; // TODO: Initialize to an appropriate value
            string oicUserName = string.Empty; // TODO: Initialize to an appropriate value
            string preNameCode = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            string sex = string.Empty; // TODO: Initialize to an appropriate value
            string oicTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            byte[] sign = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.InsertOIC(oicEmpNo, oicUserName, preNameCode, firstName, lastName, sex, oicTypeCode, sign);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsRightUserOIC
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void IsRightUserOICTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string oicUserName = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.IsRightUserOIC(oicUserName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PersonApprove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PersonApproveTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            List<string> listId = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.PersonApprove(listId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PersonEditApprove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PersonEditApproveTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            List<string> listId = null; // TODO: Initialize to an appropriate value
            string appresult = string.Empty; // TODO: Initialize to an appropriate value
            string userid = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.PersonEditApprove(listId, appresult, userid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PersonNotApprove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PersonNotApproveTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            List<string> listId = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.PersonNotApprove(listId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RenewPassword
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void RenewPasswordTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string oldpassword = string.Empty; // TODO: Initialize to an appropriate value
            string newpassword = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.RenewPassword(username, email, oldpassword, newpassword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetOffLineStatus
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SetOffLineStatusTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.SetOffLineStatus(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetPersonTemp
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void SetPersonTempTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            PersonTemp tmpPerson = null; // TODO: Initialize to an appropriate value
            List<PersonAttatchFile> tmpFiles = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.SetPersonTemp(tmpPerson, tmpFiles);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateOIC
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateOICTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string userId = string.Empty; // TODO: Initialize to an appropriate value
            string oicUserName = string.Empty; // TODO: Initialize to an appropriate value
            string preNameCode = string.Empty; // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            string sex = string.Empty; // TODO: Initialize to an appropriate value
            string memberType = string.Empty; // TODO: Initialize to an appropriate value
            byte[] imgsign = new byte[1024]; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.UpdateOIC(userId, oicUserName, preNameCode, firstName, lastName, sex, memberType,imgsign);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getPDetailByIDCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void getPDetailByIDCardTest()
        {
            IPersonService target = CreateIPersonService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.getPDetailByIDCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}