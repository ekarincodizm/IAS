using IAS.DataServices.Registration;
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
    ///This is a test class for IRegistrationServiceTest and is intended
    ///to contain all IRegistrationServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IRegistrationServiceTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {

        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (ctx != null)
                ctx.Dispose();
        }
        //
        #endregion
        IAS.DAL.Interfaces.IIASPersonEntities ctx;

        internal virtual IRegistrationService CreateIRegistrationService()
        {
            // TODO: Instantiate an appropriate concrete class.
            ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();
            IRegistrationService target = new RegistationService(ctx);
            return target;
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
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.Delete(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteAttatchFile
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteAttatchFileTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> actual;
            actual = target.DeleteAttatchFile(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EntityValidation
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void EntityValidationTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.EntityValidation(registerType, entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetPersonalDetailByIDCard()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string idCard = "2711000016188"; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Person> actual;
            actual = target.GetPersonalDetailByIDCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAgentType
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAgentTypeTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string agentType = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<List<AgentTypeEntity>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<AgentTypeEntity>> actual;
            actual = target.GetAgentType(agentType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttatchFileById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAttatchFileByIdTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> actual;
            actual = target.GetAttatchFileById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttatchFilesByRegisterationNo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAttatchFilesByRegisterationNoTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string registerationNo = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<IList<RegistrationAttatchFile>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<IList<RegistrationAttatchFile>> actual;
            actual = target.GetAttatchFilesByRegisterationNo(registerationNo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByFirstLastName
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetByFirstLastNameTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            DTO.GetByFirstLastNameRequest request = new GetByFirstLastNameRequest();
            actual = target.GetByFirstLastName(request);
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
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string Id = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.GetById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByIdCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetByIdCardTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.GetByIdCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRegistrationsByCriteria
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetRegistrationsByCriteriaTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
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
            DTO.GetReistrationByCriteriaRequest request  = new GetReistrationByCriteriaRequest();
            actual = target.GetRegistrationsByCriteria(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRegistrationsByCriteriaAtPage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetRegistrationsByCriteriaAtPageTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string firstName = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            string IdCard = string.Empty; // TODO: Initialize to an appropriate value
            string memberTypeCode = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            string status = string.Empty; // TODO: Initialize to an appropriate value
            int pageIndex = 0; // TODO: Initialize to an appropriate value
            int pageSize = 0; // TODO: Initialize to an appropriate value
            ResponseService<PagingResponse<DataSet>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<PagingResponse<DataSet>> actual;
            actual = target.GetRegistrationsByCriteriaAtPage(firstName, lastName, IdCard, memberTypeCode, email, compCode, status, pageIndex, pageSize);
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
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = null; // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.Insert(entity, registerType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertAttachFileToRegistration
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertAttachFileToRegistrationTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string registrationId = string.Empty; // TODO: Initialize to an appropriate value
            RegistrationAttatchFile attachFile = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> actual;
            actual = target.InsertAttachFileToRegistration(registrationId, attachFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertWithAttatchFile
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertWithAttatchFileTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = null; // TODO: Initialize to an appropriate value
            List<RegistrationAttatchFile> listAttatchFile = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.InsertWithAttatchFile(registerType, entity, listAttatchFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsCompAssoUserRegistered
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void IsCompAssoUserRegisteredTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string lastName = string.Empty; // TODO: Initialize to an appropriate value
            string compCode = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.IsCompAssoUserRegistered(email, name, lastName, compCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGeneralUserRegistered
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void IsGeneralUserRegisteredTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.IsGeneralUserRegistered(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RegistrationApprove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void RegistrationApproveTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            List<string> listId = null; // TODO: Initialize to an appropriate value
            string appresult = string.Empty; // TODO: Initialize to an appropriate value
            string userid = string.Empty; // TODO: Initialize to an appropriate value
            string memType = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            DTO.RegistrationApproveRequest request = new RegistrationApproveRequest();
            actual = target.RegistrationApprove(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RegistrationNotApprove
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void RegistrationNotApproveTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value

            List<string> listId = null; // TODO: Initialize to an appropriate value
            string appresult = string.Empty; // TODO: Initialize to an appropriate value
            string userid = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            DTO.RegistrationNotApproveRequest request = new RegistrationNotApproveRequest();
            actual = target.RegistrationNotApprove(request);
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
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value

            String id = OracleDB.GetGenAutoId();
            AG_IAS_REGISTRATION_T regis1 = new AG_IAS_REGISTRATION_T()
            {
                ID = id,
                ID_CARD_NO = "1236277389234",
                NAMES = "MyName",
                LASTNAME = "LastName"
            };
            ctx.AG_IAS_REGISTRATION_T.AddObject(regis1);


            DTO.Registration entity = new DTO.Registration()
            {
                ID = id,
                ID_CARD_NO = "1234567890123",
                NAMES = "NewName",
                LASTNAME = "LastName"
            }; // TODO: Initialize to an appropriate value

            ResponseService<DTO.Registration> expected = new ResponseService<DTO.Registration>(); // TODO: Initialize to an appropriate value
            expected.DataResponse = new DTO.Registration()
            {
                ID = id,
                ID_CARD_NO = "1234567890123",
                NAMES = "NewName",
                LASTNAME = "LastName"
            };

            ResponseService<DTO.Registration> actual;
            actual = target.Update(entity);

            Assert.AreEqual(expected.DataResponse.ID, actual.DataResponse.ID);
            Assert.AreEqual(expected.DataResponse.ID_CARD_NO, actual.DataResponse.ID_CARD_NO);
            Assert.AreEqual(expected.DataResponse.NAMES, actual.DataResponse.NAMES);
            Assert.AreEqual(expected.DataResponse.LASTNAME, actual.DataResponse.LASTNAME);
            Assert.AreNotEqual(expected.DataResponse.UPDATED_DATE, actual.DataResponse.UPDATED_DATE);
            //Assert.Inconclusive("Verify the correctness of this test method.");

        }

        /// <summary>
        ///A test for UpdateAttachFile
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateAttachFileTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            RegistrationAttatchFile entity = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<RegistrationAttatchFile> actual;
            actual = target.UpdateAttachFile(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateWithAttachFiles
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void UpdateWithAttachFilesTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = null; // TODO: Initialize to an appropriate value
            List<RegistrationAttatchFile> listAttatchFile = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;
            actual = target.UpdateWithAttachFiles(entity, listAttatchFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateBeforeSubmit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ValidateBeforeSubmitTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.ValidateBeforeSubmit(registerType, entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VerifyIdCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void VerifyIdCardTest()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string idCard = string.Empty; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.VerifyIdCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void EntityValidation()
        {
            IRegistrationService target = CreateIRegistrationService(); // TODO: Initialize to an appropriate value
            string idCard = "1111111111101"; // TODO: Initialize to an appropriate value
            DTO.ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            expected.ResultMessage = true;
            DTO.ResponseMessage<bool> actual;

            //Assign
            DTO.Registration ent = new DTO.Registration();
            ent.ID_CARD_NO = "1664957822151";
            ent.MEMBER_TYPE = ((int)DTO.MemberType.Insurance).ToString();
            ent.NAMES = "กกดกเ";
            ent.LASTNAME = "ddddddd";
            ent.EMAIL = "ass1664957822151@gmail.com";

            actual = target.EntityValidation(DTO.RegistrationType.Insurance, ent);
            Assert.AreEqual(expected.ResultMessage, actual.ResultMessage);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
