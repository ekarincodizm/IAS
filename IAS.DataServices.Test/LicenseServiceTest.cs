using IAS.DataServices.License;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IAS.DTO;
using System.Collections.Generic;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Test
{
    
    
    /// <summary>
    ///This is a test class for LicenseServiceTest and is intended
    ///to contain all LicenseServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LicenseServiceTest
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
        ///A test for GetAllLicenseByIDCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAllLicenseByIDCardTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "1589900053854"; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> actual;
            actual = target.GetAllLicenseByIDCard(idCard, "1", 1);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveExtachFile
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void MoveExtachFileTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string userId = "131026163746250"; // TODO: Initialize to an appropriate value
            string groupid = "140424102806350"; // TODO: Initialize to an appropriate value
            List<AttachFileDetail> attachFiles = null; // TODO: Initialize to an appropriate value
            ResponseService<List<AttatchFileLicense>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<AttatchFileLicense>> actual;
            actual = target.MoveExtachFile(userId, groupid, attachFiles);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }





        /// <summary>
        ///A test for ValidateProp
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void ValidatePropTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string groupId = "140630154628451"; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = null; // TODO: Initialize to an appropriate value
            ResponseMessage<bool> actual;
            actual = target.ValidateProp(groupId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPersonalHistoryByIdCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void GetPersonalHistoryByIdCardTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "1980000000001"; // TODO: Initialize to an appropriate value
            ResponseService<PersonalHistory> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<PersonalHistory> actual;
            actual = target.GetPersonalHistoryByIdCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void LicenseRevokedValidationTest()
        {
            List<string> license = new List<string>
            {
                "4501068692", "4801032607"
                //"4801033980", "5402003198"
            };
            string licenseCode = "03";

            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            expected.ResultMessage = true;

            ResponseMessage<bool> actual;
            actual = target.LicenseRevokedValidation(license, licenseCode);
            Assert.AreEqual(expected.ResultMessage, actual.ResultMessage);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }


        [TestMethod()]
        public void GetPersonalTrainByCriteria()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string licenseNo = "5701000020"; // TODO: Initialize to an appropriate value
            string idCard = "6577883700221"; // TODO: Initialize to an appropriate value
            string specialCode = "1006";
            ResponseService<DTO.TrainPersonHistory> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<DTO.TrainPersonHistory> actual;
            //4803003454
            actual = target.GetPersonalTrainByCriteria("01", "14", "4", idCard, licenseNo, "1006");
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetRenewLicneseByIdCard()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "3860800014666"; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.PersonLicenseTransaction>> expected = null; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.PersonLicenseTransaction>> actual;
            actual = target.GetRenewLicneseByIdCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetExpiredLicneseByIdCard()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "3100902079682"; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.PersonLicenseTransaction>> expected = null; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.PersonLicenseTransaction>> actual;
            actual = target.GetExpiredLicneseByIdCard(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetExamHistoryByID()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "1669700065351"; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.ExamHistory>> expected = null; // TODO: Initialize to an appropriate value
            DTO.ResponseService<List<DTO.ExamHistory>> actual;
            actual = target.GetExamHistoryByID(idCard);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetExamHistoryByIDWithCondition
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetExamHistoryByIDWithConditionTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "3100900224766"; // TODO: Initialize to an appropriate value
            string licenseTypeCode = "04"; // TODO: Initialize to an appropriate value
            ResponseService<List<ExamHistory>> expected = new ResponseService<List<ExamHistory>>(); // TODO: Initialize to an appropriate value
            ResponseService<List<ExamHistory>> actual;
            actual = target.GetExamHistoryByIDWithCondition(idCard, licenseTypeCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetLicenseConfigByPetition
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetLicenseConfigByPetitionTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string petitionType = "11"; // TODO: Initialize to an appropriate value
            string licenseType = "03"; // TODO: Initialize to an appropriate value
            ResponseService<List<ConfigDocument>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<ConfigDocument>> actual;
            actual = target.GetLicenseConfigByPetition(petitionType, licenseType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertPersonLicense
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void InsertPersonLicenseTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            #region Header Init
            List<PersonLicenseHead> headerls = new List<PersonLicenseHead>(); // TODO: Initialize to an appropriate value
            PersonLicenseHead headEnt = new PersonLicenseHead();
		    headEnt.APPROVE_COMPCODE = "111";
		    headEnt.APPROVED_BY = null;
		    headEnt.APPROVED_DATE	=null;
		    headEnt.APPROVED_DOC	="W";
		    headEnt.COMP_CODE	=null;
		    headEnt.COMP_NAME	=null;
		    headEnt.FILENAME	=null;
		    headEnt.FLAG_LIC	=null;
		    headEnt.FLAG_REQ	=null;
		    headEnt.LICENSE_TYPE_CODE	="03";
		    headEnt.LOTS	=1;
		    headEnt.MONEY	=200;
		    headEnt.PAYMENT_NO	=null;
		    headEnt.PETITION_TYPE_CODE	="16";
		    headEnt.REQUEST_NO	=null;
		    headEnt.TRAN_DATE	= Convert.ToDateTime("1/7/2557");
		    headEnt.UPLOAD_BY_SESSION	="140609145948540";
            headEnt.UPLOAD_GROUP_NO = "140701152850898";
            headerls.Add(headEnt);
            #endregion

            #region Detail Init
            List<PersonLicenseDetail> detaills = new List<PersonLicenseDetail>(); // TODO: Initialize to an appropriate value
            PersonLicenseDetail detailEnt = new PersonLicenseDetail();
            detailEnt.ADDRESS_1 = "asdfasfdasf";
            detailEnt.ADDRESS_2 = null;
            detailEnt.APPROVED = "W";
            detailEnt.APPROVED_BY = null;
            detailEnt.APPROVED_DATE = null;
            detailEnt.AR_DATE = Convert.ToDateTime("1/1/0544");
            detailEnt.AREA_CODE = "10";
            detailEnt.CURRENT_ADDRESS_1 = "asdfasfdasf";
            detailEnt.CURRENT_ADDRESS_2 = null;
            detailEnt.CURRENT_AREA_CODE = "10";
            detailEnt.EMAIL = "ss@ss.ss";
            detailEnt.ERR_DESC = null;
            detailEnt.FEES = 200;
            detailEnt.HEAD_REQUEST_NO = null;
            detailEnt.ID_CARD_NO = "1589900053854";
            detailEnt.LASTNAME = "มุ่งดี";
            detailEnt.LICENSE_DATE = null;
            detailEnt.LICENSE_EXPIRE_DATE = null;
            detailEnt.LICENSE_NO = "5603007789";
            detailEnt.NAMES = "เบญจมาส";
            detailEnt.OLD_COMP_CODE = null;
            detailEnt.ORDERS = null;
            detailEnt.PAY_EXPIRE = Convert.ToDateTime("21/5/2558");
            detailEnt.PRE_NAME_CODE = "2";
            detailEnt.RENEW_TIMES = "1";
            detailEnt.REQUEST_NO = null;
            detailEnt.SEQ_NO = "0001";
            detailEnt.TITLE_NAME = "นางสาว";
            detailEnt.UPLOAD_GROUP_NO = "140701152850898";
            detaills.Add(detailEnt);
            #endregion

            #region UserProfile Init
            UserProfile userProfile = new UserProfile(); // TODO: Initialize to an appropriate value
            userProfile.AgentType = "Z";
		    userProfile.CompCode	= null;
		    userProfile.DepartmentCode	= null;
		    userProfile.DepartmentName	= null	;
		    userProfile.DeptCode	= null	;
		    userProfile.EmployeeCode	= null	;
		    userProfile.EmployeeName	= null	;
		    userProfile.Id	= "140609145948540"	;
		    userProfile.IdCard	= "1589900053854"	;
		    userProfile.IS_APPROVE	= "Y"	;
		    userProfile.LastName	= null	;
		    userProfile.LicenseNo	= null	;
		    userProfile.LoginName	= "1589900053854"	;
		    userProfile.LoginStatus	= "0"	;
		    userProfile.MemberType	= 1	;
		    userProfile.Name	= "เบญจมาส มุ่งดี"	;
		    userProfile.OIC_EMP_NO	= null	;
		    userProfile.OIC_User_Id	= null	;
		    userProfile.OIC_User_Type	= null	;
		    userProfile.PositionCode	= null	;
		    userProfile.PositionName	= null	;
		    userProfile.STATUS	= "2"	;
		    userProfile.TitleName	= null	;
            userProfile.UserGroup = null;
            #endregion

            #region AttatchFileLicense Init
            List<AttatchFileLicense> files = new List<AttatchFileLicense>(); // TODO: Initialize to an appropriate value
            int count = 2;
            for (int i = 0; i < count; i++)
            {
                i = i+1;
                AttatchFileLicense file = new AttatchFileLicense();
                if (i == 1)
                {
                    file.ATTACH_FILE_PATH = "Temp\\140609145948540\\140701152905098_03.jpg";
                    file.ATTACH_FILE_TYPE = "03";
                    file.CREATED_BY = "1589900053854";
                    file.CREATED_DATE = Convert.ToDateTime("1/7/2557");
                    file.FILE_STATUS = "W";
                    file.GROUP_LICENSE_ID = "140701152850898";
                    file.ID_ATTACH_FILE = "140609145948540";
                    file.ID_CARD_NO = "1589900053854";
                    file.LICENSE_NO = null;
                    file.REMARK = "hhh";
                    file.RENEW_TIME = null;
                    file.UPDATED_BY = "1589900053854";
                    file.UPDATED_DATE = Convert.ToDateTime("1/7/2557");
                }
                else
                {

                    file.ATTACH_FILE_PATH	= "Temp\\140609145948540\\140701153226031_65.jpg"	;
		            file.ATTACH_FILE_TYPE	= "65"	;
		            file.CREATED_BY	= "1589900053854"	;
		            file.CREATED_DATE	= Convert.ToDateTime("1/7/2557");
		            file.FILE_STATUS	= "W"	;
		            file.GROUP_LICENSE_ID	= "140701153215473"	;
		            file.ID_ATTACH_FILE	= "140609145948540"	;
		            file.ID_CARD_NO	= "1589900053854"	;
		            file.LICENSE_NO	= null	;
		            file.REMARK	 = ""	;
		            file.RENEW_TIME	= null	;
		            file.UPDATED_BY	= "1589900053854"	;
                    file.UPDATED_DATE = Convert.ToDateTime("1/7/2557");
                }

                files.Add(file);

                i++;
            }
            #endregion

            
            
            
            ResponseMessage<bool> expected = new ResponseMessage<bool>(); // TODO: Initialize to an appropriate value
            expected.ResultMessage = true;
            ResponseMessage<bool> actual = new ResponseMessage<bool>();
            actual = target.InsertPersonLicense(headerls, detaills, userProfile, files);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getViewPersonLicense
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void getViewPersonLicenseTest()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "3601101042183"; // TODO: Initialize to an appropriate value
            string status = "A"; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> actual;
            actual = target.getViewPersonLicense(idCard, status);

            ResponseService<List<PersonLicenseTransaction>> actual2;
            actual2 = target.getViewPersonLicense(idCard, "Y");

            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAllLicenseByIDCard
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void GetAllLicenseByIDCardTest1()
        {
            LicenseService target = new LicenseService(); // TODO: Initialize to an appropriate value
            string idCard = "3361857228377"; // TODO: Initialize to an appropriate value
            string mode = "1"; // TODO: Initialize to an appropriate value
            int feemode = 1; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> expected = null; // TODO: Initialize to an appropriate value
            ResponseService<List<PersonLicenseTransaction>> actual;
            actual = target.GetAllLicenseByIDCard(idCard, mode, feemode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
