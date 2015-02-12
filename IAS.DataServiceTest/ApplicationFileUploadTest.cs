using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.Utils;
using IAS.DataServices.Applicant.ApplicantHelper;
using IAS.DataServices.Applicant.ApplicantRequestUploads;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class ApplicationFileUploadTest
    {

        private String _header = "H,30,444,03,10/10/2556,220,4400,23,";
        private List<String> _lines = new List<String>();
        IAS.DAL.Interfaces.IIASPersonEntities ctx;

        [TestInitialize]
        public void InitData() 
        {
                ctx = DAL.DALFactory.GetPersonContext();
                _lines.Add( "1,9156460258875,นาง,Imp Broker221,นายหน้าประกันชีวิต221,17/1/2533,ญ,04,1001,,");
                _lines.Add( "2,1990000002211,นางสาว,Imp Broker222,นายหน้าประกันชีวิต222,18/1/2533,ญ,05,1001,,");
                _lines.Add( "3,9990000002220,นาย,Imp Broker223,นายหน้าประกันชีวิต223,19/1/2533,ช,04,1001,,");
                _lines.Add( "4,9990000002238,นาง,Imp Broker224,นายหน้าประกันชีวิต224,17/1/2533,ญ,04,1001,,");
                _lines.Add( "5,9990000002246,นางสาว,Imp Broker225,นายหน้าประกันชีวิต225,18/1/2533,ญ,05,1001,,");
        }


        private DTO.UploadData GetUploadDatafromVariable() 
        {
            DTO.UploadData uploadData = new DTO.UploadData() { 
                Header = _header,
                Body = _lines,
                IsCSVFile = true
            };
            return uploadData;
        }


        [TestMethod]
        public void ApplicantFileHeader_Can_Read_Text_To_Object()
        {
            ApplicantFileHeader applicantHeader;

            applicantHeader = CreateHeader();

            Assert.IsNotNull(applicantHeader.UPLOAD_GROUP_NO);
            Assert.AreNotEqual(applicantHeader.UPLOAD_GROUP_NO, "");
            Assert.AreEqual(applicantHeader.SOURCE_TYPE, "C");
            Assert.AreEqual(applicantHeader.PROVINCE_CODE, "30");
            Assert.AreEqual(applicantHeader.COMP_CODE, "444");
            Assert.AreEqual(applicantHeader.TESTING_DATE, new DateTime(2013, 10, 10));
            Assert.AreEqual(applicantHeader.EXAM_APPLY, (short)220);
            Assert.AreEqual(applicantHeader.EXAM_AMOUNT, 4400m);
            Assert.AreEqual(applicantHeader.TEST_TIME_CODE, "23");

        }

        [TestMethod]
        public void ApplicantFileDetail_Can_Read_Text_To_Object() {
            ApplicantFileHeader applicantHeader = CreateHeader();

            String[] data = _lines.FirstOrDefault().Split(',');

            ApplicantFileDetail detial = new ApplicantFileDetail(ctx)
            {
                LOAD_STATUS = "",
                APPLICANT_CODE = PhaseAppliantCodeHelper.Phase(data.GetIndexOf(0)),
                ID_CARD_NO = data.GetIndexOf(1),
                PRE_NAME_CODE = PreNameHelper.ConvertToCode(ctx, data.GetIndexOf(2)),
                NAMES = data.GetIndexOf(3),
                LASTNAME = data.GetIndexOf(4),
                BIRTH_DATE = PhaseDateHelper.PhaseToDateNull(data.GetIndexOf(5)),
                SEX = data.GetIndexOf(6),
                EDUCATION_CODE = data.GetIndexOf(7),
                ADDRESS1 = data.GetIndexOf(9),
                AREA_CODE = data.GetIndexOf(10),
                INSUR_COMP_CODE = data.GetIndexOf(8),
                TITLE = data.GetIndexOf(2)
            };
        }

        private ApplicantFileHeader CreateHeader()
        {
            String testingNumber = "561644";
            DTO.UserProfile userProfile = CreateUserProfile();
            String filename = "test.txt";
            String[] _lineData = _header.Split(',');
            ApplicantHeaderRequest request = new ApplicantHeaderRequest()
            {
                Context = ctx,
                UserProfile = userProfile,
                FileName = filename,
                TestingNumber = testingNumber,
                LineData = _lineData
            };
            ApplicantFileHeader applicantHeader = ConcreateApplicantFileHeader(request);
            return applicantHeader;
        }

        private static ApplicantFileHeader ConcreateApplicantFileHeader(ApplicantHeaderRequest request)
        {
            String[] header = request.LineData;

            ApplicantFileHeader applicantHeader;
            applicantHeader = new ApplicantFileHeader(request);

            applicantHeader.PROVINCE_CODE = header.GetIndexOf(1);
            applicantHeader.COMP_CODE = header.GetIndexOf(2);
            applicantHeader.LICENSE_TYPE_CODE = header.GetIndexOf(3);
            applicantHeader.TESTING_DATE = PhaseDateHelper.PhaseToDateNull(header.GetIndexOf(4));
            applicantHeader.EXAM_APPLY = PhaseApplyAmountHelper.Phase(header.GetIndexOf(5));
            applicantHeader.EXAM_AMOUNT = PhaseCurrencyAmount.Phase(header.GetIndexOf(6));
            applicantHeader.TEST_TIME_CODE = header.GetIndexOf(7);
            return applicantHeader;
        }

        private static DTO.UserProfile CreateUserProfile()
        {
            DTO.UserProfile userProfile;
            userProfile = new DTO.UserProfile()
            {
                Id = "131106132209758",
                MemberType = 2,
                LoginName = "testname",
                STATUS = "A",
                CompCode = "1022"
            };
            return userProfile;
        }

        [TestMethod]
        public void ApplicantFileFactory_Can_Create_ApplicationFileHeader() {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            DTO.ApplicantUploadRequest request = new DTO.ApplicantUploadRequest() { 
                                                     FileName="ApplicantFile.csv",
                                                     TestingNo="561655",
                                                     UploadData=GetUploadDatafromVariable(),
                                                     UserProfile=CreateUserProfile()
                                                    };

            DTO.ResponseService<ApplicantFileHeader> response = ApplicantFileFactory.ConcreateApplicantFileRequest(ctx, request);
            ApplicantFileHeader headFile = response.DataResponse;
            Assert.IsNotNull(headFile.UPLOAD_GROUP_NO);
            Assert.AreNotEqual(headFile.UPLOAD_GROUP_NO, "");
            Assert.AreEqual(headFile.SOURCE_TYPE, "C");
            Assert.AreEqual(headFile.PROVINCE_CODE, "30");
            Assert.AreEqual(headFile.COMP_CODE, "444");
            Assert.AreEqual(headFile.TESTING_DATE, new DateTime(2013, 10, 10));
            Assert.AreEqual(headFile.EXAM_APPLY, (short)220);
            Assert.AreEqual(headFile.EXAM_AMOUNT, 4400m);
            Assert.AreEqual(headFile.TEST_TIME_CODE, "23");
            Assert.IsNotNull(headFile.ExamPlace);

            //"1,9990000002203,นาง,Imp Broker221,นายหน้าประกันชีวิต221,17/1/2533,ญ,04,1001,,"
            ApplicantFileDetail detail_1 = headFile.ApplicantFileDetails.SingleOrDefault(a => a.ID_CARD_NO == "9156460258875");
            Assert.AreEqual(detail_1.LOAD_STATUS, "F");
            Assert.AreEqual(detail_1.APPLICANT_CODE, 1);
            Assert.AreEqual(detail_1.TESTING_NO, "561655");
            Assert.AreEqual(detail_1.EXAM_PLACE_CODE, "30444");
            Assert.IsNull(detail_1.ACCEPT_OFF_CODE);
            Assert.IsNotNull(detail_1.APPLY_DATE);
            Assert.AreEqual(detail_1.ID_CARD_NO, "9156460258875");
            Assert.AreEqual(detail_1.PRE_NAME_CODE, "3");
            Assert.AreEqual(detail_1.NAMES, "Imp Broker221");
            Assert.AreEqual(detail_1.LASTNAME, "นายหน้าประกันชีวิต221");
            Assert.AreEqual(detail_1.BIRTH_DATE, (new DateTime(1990, 1,17))); // "17/1/2533");
            Assert.AreEqual(detail_1.SEX, "F");
            Assert.AreEqual(detail_1.EDUCATION_CODE, "04");
            Assert.AreEqual(detail_1.ADDRESS1, "");
            Assert.IsNull(detail_1.ADDRESS2);
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.AREA_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.PROVINCE_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.ZIPCODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.TELEPHONE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.AMOUNT_TRAN_NO));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.PAYMENT_NO));
            Assert.AreEqual(detail_1.INSUR_COMP_CODE, "1001");
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.ABSENT_EXAM));        
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.RESULT));
            Assert.IsNull(detail_1.EXPIRE_DATE);                   
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.LICENSE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.CANCEL_REASON));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.RECORD_STATUS));
            Assert.AreEqual(detail_1.USER_ID, "131106132209758");
            Assert.IsNotNull(detail_1.USER_DATE);
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.EXAM_STATUS));
            Assert.IsTrue(String.IsNullOrEmpty(detail_1.REQUEST_NO));
            Assert.AreEqual(detail_1.UPLOAD_GROUP_NO, headFile.UPLOAD_GROUP_NO);
            Assert.AreEqual(detail_1.SEQ_NO, (1).ToString("0000"));
            Assert.AreEqual(detail_1.TITLE, "นาง");
            Assert.IsFalse(String.IsNullOrEmpty(detail_1.ERROR_MSG));

            //"2,9990000002211,นางสาว,Imp Broker222,นายหน้าประกันชีวิต222,18/1/2533,ญ,05,1001,,"
            ApplicantFileDetail detail_2 = headFile.ApplicantFileDetails.SingleOrDefault(a => a.ID_CARD_NO == "1990000002211");
            Assert.AreEqual(detail_2.LOAD_STATUS, "F");
            Assert.AreEqual(detail_2.APPLICANT_CODE, 2);
            Assert.AreEqual(detail_2.TESTING_NO, "561655");
            Assert.AreEqual(detail_2.EXAM_PLACE_CODE, "30444");
            Assert.IsNull(detail_2.ACCEPT_OFF_CODE);
            Assert.IsNotNull(detail_2.APPLY_DATE);
            Assert.AreEqual(detail_2.ID_CARD_NO, "1990000002211");
            Assert.AreEqual(detail_2.PRE_NAME_CODE, "2");
            Assert.AreEqual(detail_2.NAMES, "Imp Broker222");
            Assert.AreEqual(detail_2.LASTNAME, "นายหน้าประกันชีวิต222");
            Assert.AreEqual(detail_2.BIRTH_DATE, (new DateTime(1990, 1, 18))); // "17/1/2533");
            Assert.AreEqual(detail_2.SEX, "F");
            Assert.AreEqual(detail_2.EDUCATION_CODE, "05");
            Assert.AreEqual(detail_2.ADDRESS1, "");
            Assert.IsNull(detail_2.ADDRESS2);
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.AREA_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.PROVINCE_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.ZIPCODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.TELEPHONE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.AMOUNT_TRAN_NO));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.PAYMENT_NO));
            Assert.AreEqual(detail_2.INSUR_COMP_CODE, "1001");
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.ABSENT_EXAM));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.RESULT));
            Assert.IsNull(detail_2.EXPIRE_DATE);
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.LICENSE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.CANCEL_REASON));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.RECORD_STATUS));
            Assert.AreEqual(detail_2.USER_ID, "131106132209758");
            Assert.IsNotNull(detail_2.USER_DATE);
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.EXAM_STATUS));
            Assert.IsTrue(String.IsNullOrEmpty(detail_2.REQUEST_NO));
            Assert.AreEqual(detail_2.UPLOAD_GROUP_NO, headFile.UPLOAD_GROUP_NO);
            Assert.AreEqual(detail_2.SEQ_NO, (2).ToString("0000"));
            Assert.AreEqual(detail_2.TITLE, "นางสาว");
            Assert.IsFalse(String.IsNullOrEmpty(detail_2.ERROR_MSG));

            //"3,9990000002220,นาย,Imp Broker223,นายหน้าประกันชีวิต223,19/1/2533,ช,04,1001,,"
            ApplicantFileDetail detail_3 = headFile.ApplicantFileDetails.SingleOrDefault(a => a.ID_CARD_NO == "9990000002220");
            Assert.AreEqual(detail_3.LOAD_STATUS, "F");
            Assert.AreEqual(detail_3.APPLICANT_CODE, 3);
            Assert.AreEqual(detail_3.TESTING_NO, "561655");
            Assert.AreEqual(detail_3.EXAM_PLACE_CODE, "30444");
            Assert.IsNull(detail_3.ACCEPT_OFF_CODE);
            Assert.IsNotNull(detail_3.APPLY_DATE);
            Assert.AreEqual(detail_3.ID_CARD_NO, "9990000002220");
            Assert.AreEqual(detail_3.PRE_NAME_CODE, "1");
            Assert.AreEqual(detail_3.NAMES, "Imp Broker223");
            Assert.AreEqual(detail_3.LASTNAME, "นายหน้าประกันชีวิต223");
            Assert.AreEqual(detail_3.BIRTH_DATE, (new DateTime(1990, 1, 19))); // "17/1/2533");
            Assert.AreEqual(detail_3.SEX, "M");
            Assert.AreEqual(detail_3.EDUCATION_CODE, "04");
            Assert.AreEqual(detail_3.ADDRESS1, "");
            Assert.IsNull(detail_3.ADDRESS2);
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.AREA_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.PROVINCE_CODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.ZIPCODE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.TELEPHONE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.AMOUNT_TRAN_NO));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.PAYMENT_NO));
            Assert.AreEqual(detail_3.INSUR_COMP_CODE, "1001");
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.ABSENT_EXAM));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.RESULT));
            Assert.IsNull(detail_3.EXPIRE_DATE);
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.LICENSE));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.CANCEL_REASON));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.RECORD_STATUS));
            Assert.AreEqual(detail_3.USER_ID, "131106132209758");
            Assert.IsNotNull(detail_3.USER_DATE);
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.EXAM_STATUS));
            Assert.IsTrue(String.IsNullOrEmpty(detail_3.REQUEST_NO));
            Assert.AreEqual(detail_3.UPLOAD_GROUP_NO, headFile.UPLOAD_GROUP_NO);
            Assert.AreEqual(detail_3.SEQ_NO, (3).ToString("0000"));
            Assert.AreEqual(detail_3.TITLE, "นาย");
            Assert.IsFalse(String.IsNullOrEmpty(detail_3.ERROR_MSG));
        }

        [TestMethod]
        public void ApplicantFileFactory_Can_Read_DataFromFile() { 
            
        } 
    }
}
