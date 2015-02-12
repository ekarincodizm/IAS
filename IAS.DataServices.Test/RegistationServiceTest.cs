using IAS.DataServices.Registration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using System.Data;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for RegistationServiceTest and is intended
    ///to contain all RegistationServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RegistationServiceTest
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
        ///A test for EntityValidation
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void EntityValidationTest()
        {
            RegistationService target = new RegistationService(); // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = new DTO.Registration(); // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            expected.ResultMessage = true;
            ResponseMessage<bool> actual;

            entity.ID_CARD_NO = "0065322327231";
            entity.MEMBER_TYPE = ((int)DTO.MemberType.Insurance).ToString();
            entity.NAMES = "กกดกเ";
            entity.LASTNAME = "ddddddd";
            entity.EMAIL = "ass1664957822151@gmail.com";
            actual = target.EntityValidation(DTO.RegistrationType.Insurance, entity);
            Assert.AreEqual(expected.ResultMessage, actual.ResultMessage);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetPersonalDetailByIDCard()
        {
            RegistationService target = new RegistationService(); // TODO: Initialize to an appropriate value
            RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration entity = new DTO.Registration(); // TODO: Initialize to an appropriate value
            DTO.ResponseService<DTO.Person> expected = new DTO.ResponseService<DTO.Person>(); // TODO: Initialize to an appropriate value

            DTO.ResponseService<DTO.Person> actual;

            entity.ID_CARD_NO = "6992298475033";
            entity.MEMBER_TYPE = ((int)DTO.MemberType.Insurance).ToString();
            entity.NAMES = "กกดกเ";
            entity.LASTNAME = "ddddddd";
            entity.EMAIL = "ass1664957822151@gmail.com";
            actual = target.GetPersonalDetailByIDCard("5210280819012");
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
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

            RegistationService target = new RegistationService(); // TODO: Initialize to an appropriate value
            //RegistrationType registerType = new RegistrationType(); // TODO: Initialize to an appropriate value
            DTO.Registration x = new DTO.Registration(); // TODO: Initialize to an appropriate value
            x.ID = DateTime.Now.Year.ToString("0000").Substring(2, 2) + DateTime.Now.ToString("MMddHHmmssfff");
            x.ID_CARD_NO = "3100800728835";
            x.BIRTH_DATE = DateTime.Now.AddYears(-21);
            x.MEMBER_TYPE = "1";
            x.PRE_NAME_CODE = "3";
            x.NAMES = "ชินบุตร";
            x.LASTNAME = "สุวรรณเลิศวัฒนา";
            x.NATIONALITY = "001";
            x.SEX = "M";
            x.EDUCATION_CODE = "05";
            x.EMAIL = "3100800844908per@gmail.com";
            x.IMPORT_STATUS = "N";

            List<RegistrationAttatchFile> listAttatchFile = new List<RegistrationAttatchFile>(); // TODO: Initialize to an appropriate value
            //listAttatchFile.Add(new RegistrationAttatchFile
            //{


            //});

            ResponseService<DTO.Registration> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.Registration> actual;


            actual = target.InsertWithAttatchFile(DTO.RegistrationType.General, x, listAttatchFile);
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
            RegistationService target = new RegistationService(); // TODO: Initialize to an appropriate value
            GetReistrationByCriteriaRequest request = new GetReistrationByCriteriaRequest(); // TODO: Initialize to an appropriate value
            ResponseService<DataSet> expected = new ResponseService<DataSet>(); // TODO: Initialize to an appropriate value
            ResponseService<DataSet> actual;
            
            //Status 1 = รออนุมัติ(สมัคร), Status 2 = อนุมัติ(สมัคร), Status 3 = ไม่อนุมัติ(สมัคร)
            //GetRegistrationsByCriteria(txtFirstNameAfterReg.Text, txtLastNameAfterReg.Text, null, null, txtIDNumberAfterReg.Text, null, txtEmailAfterReg.Text, null, Session["RegStatus"].ToString(), 1, 20, "2");
            request.FirstName = "ดาวรุ่งรตา";
            request.LastName = "วงษ์ไกร";
            request.MemberTypeCode = "1";
            request.IdCard = "3670800584855";
            request.PageNo = 1;
            request.Status = "2";
            request.RecordPerPage = 20;
            request.Para = "2";

            actual = target.GetRegistrationsByCriteria(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
