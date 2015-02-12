using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.License.LicenseHelpers;
using IAS.DAL;
using System.IO;
using System.Threading;
using System.Configuration;
using IAS.FileService;
using IAS.DTO;
using IAS.DataServiceTest.Properties;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class LicenseRequestFileUploadTest
    {
        private const String _header = "H,1023,บริษัท ไอเอ็นจีประกันชีวิต จำกัด,01,08/08/2555,10,3000,,,,,,,,,";
        private const String _personRequest1 = "1,,,,300,3330500032353,นาง,กวินทรา,งีเกาะ,181 ม.1,,33051300,,181 ม.1,0845853736,33051300";
        private const String _personRequest2 = "2,,,,300,1669800077025,น.ส.,จรรยา,ยวงนุ่น,286/1,,66040200,,286/1,0843825682,66040200";

        private const String _petitionTypeCode = "11";
        private String GetDataField(String[] fields, Int32 index)
        {
            return (fields.Length > index)? fields[index]: "";
        }

        [TestMethod]
        public void LicenseHelper_Can_PhaseToDate() {
            DateTime date = LicenseFileHelper.PhaseToDate("08/08/2555");

            Assert.AreEqual(date, new DateTime(Convert.ToInt32(2555 - 543), 8, 8));
        }

        [TestMethod]
        public void LicenseRequest_Can_Seperate_DataRowHeader()
        {
             IAS.DAL.Interfaces.IIASPersonEntities ctx =  DAL.DALFactory.GetPersonContext();

             String groupNo = "130923142524097";

            String fileName = "test.csv";
            String[] rowDatas = _header.Split(',');
            DateTime dateNow = DateTime.Now;
            String licenseTypeCode = GetDataField(rowDatas, 3);
           AG_IAS_APPROVE_DOC_TYPE comp = ctx.AG_IAS_APPROVE_DOC_TYPE.Where(w => w.APPROVE_DOC_TYPE == licenseTypeCode && w.ITEM_VALUE == "Y").FirstOrDefault();

           LicenseFileHeader header = new LicenseFileHeader()
           {
               IMPORT_ID = Convert.ToInt64(groupNo.Trim()),
               IMPORT_DATETIME = dateNow,
               FILE_NAME = fileName,
               PETTITION_TYPE = _petitionTypeCode,
               LICENSE_TYPE_CODE = GetDataField(rowDatas, 3),
               COMP_CODE = GetDataField(rowDatas,1),
               COMP_NAME = GetDataField(rowDatas, 2),
               LICENSE_TYPE = GetDataField(rowDatas, 3),
               SEND_DATE = LicenseFileHelper.PhaseToDate(GetDataField(rowDatas, 4)),
               TOTAL_AGENT = LicenseFileHelper.PhaseToAmount(GetDataField(rowDatas, 5)),
               TOTAL_FEE = LicenseFileHelper.PhaseToMoney(GetDataField(rowDatas, 6)),
               ERR_MSG = "",
               APPROVE_COMPCODE = comp == null ? null : comp.APPROVER
           };

           Assert.AreEqual(header.IMPORT_ID, 130923142524097);
           Assert.AreEqual(header.IMPORT_DATETIME, dateNow);
           Assert.AreEqual(header.PETTITION_TYPE, _petitionTypeCode);
           Assert.AreEqual(header.LICENSE_TYPE_CODE, "01");
           Assert.AreEqual(header.COMP_CODE, "1023");
           Assert.AreEqual(header.COMP_NAME, "บริษัท ไอเอ็นจีประกันชีวิต จำกัด");
           Assert.AreEqual(header.LICENSE_TYPE, "01");
           Assert.AreEqual(header.SEND_DATE, new DateTime(Convert.ToInt32(2555-543),8,8) );
           Assert.AreEqual(header.TOTAL_AGENT, 10);
           Assert.AreEqual(header.TOTAL_FEE, 3000m);
           Assert.AreEqual(header.ERR_MSG, "");
           Assert.AreEqual(header.APPROVE_COMPCODE, "111");

        }

        [TestMethod]
        public void LicenseRequest_Can_Seperate_DataRowDetail()
        {
            Int32 rownum = 1;


            string[] rowDatas = _personRequest1.Split(',');
            LicenseFileDetail detail = new LicenseFileDetail()
            {

                SEQ = Convert.ToInt32(rowDatas[0]).ToString("0000"),
                LICENSE_NO = rowDatas[1],
                LICENSE_ACTIVE_DATE = LicenseFileHelper.PhaseToDateNull(rowDatas[2]),// issueDate,
                LICENSE_EXPIRE_DATE = LicenseFileHelper.PhaseToDateNull(rowDatas[3]),  // expireDate,
                LICENSE_FEE = LicenseFileHelper.PhaseToMoney(rowDatas[4]),
                CITIZEN_ID = GetDataField(rowDatas,5),
                TITLE_NAME = GetDataField(rowDatas,6),
                NAME = GetDataField(rowDatas,7),
                SURNAME = GetDataField(rowDatas,8),
                ADDR1 = GetDataField(rowDatas,9),
                ADDR2 = GetDataField(rowDatas,10),
                AREA_CODE = GetDataField(rowDatas,11),
                EMAIL = GetDataField(rowDatas,12),
                CUR_ADDR = GetDataField(rowDatas,13),
                TEL_NO = GetDataField(rowDatas,14),
                CUR_AREA_CODE = GetDataField(rowDatas,15),
                AR_ANSWER = GetDataField(rowDatas,16),
                OLD_COMP_CODE = GetDataField(rowDatas,17),

            };

            Assert.AreEqual(detail.SEQ, "0001");
            Assert.AreEqual(detail.LICENSE_NO, "");
            Assert.AreEqual(detail.LICENSE_ACTIVE_DATE, null);
            Assert.AreEqual(detail.LICENSE_EXPIRE_DATE, null);
            Assert.AreEqual(detail.LICENSE_FEE, 300m);
            Assert.AreEqual(detail.CITIZEN_ID, "3330500032353");
            Assert.AreEqual(detail.TITLE_NAME, "นาง");
            Assert.AreEqual(detail.NAME, "กวินทรา");
            Assert.AreEqual(detail.SURNAME, "งีเกาะ");
            Assert.AreEqual(detail.ADDR1, "181 ม.1");
            Assert.AreEqual(detail.ADDR2, "");
            Assert.AreEqual(detail.AREA_CODE, "33051300");
            Assert.AreEqual(detail.EMAIL, "");
            Assert.AreEqual(detail.CUR_ADDR, "181 ม.1");
            Assert.AreEqual(detail.TEL_NO, "0845853736");
            Assert.AreEqual(detail.CUR_AREA_CODE, "33051300");
            Assert.AreEqual(detail.AR_ANSWER, "");
            Assert.AreEqual(detail.OLD_COMP_CODE, "");
        }


        [TestMethod]
        public void LicenseRequest_UploadFile_ValidationTest() {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            DTO.UserProfile userProfile =  CreateUserProfile();

            DTO.UploadData data = ReadDataFromFile("ขอใหม่ - li01_1001_84.csv");
            String fileName = "testData.txt";
            //LicenseFileHeader licenseHeader = LicenseFileFactory.ConcreateLicenseRequest(ctx, userProfile, fileName, data, "11");


            var res = CreateLicenseDataResponse();
            

            //res.DataResponse = licenseHeader.ValidateDataOfKTB();
            //if (res.IsError)
            //{
            //    throw new ApplicationException(res.ErrorMsg);
            //}

            //AG_IAS_TEMP_PAYMENT_HEADER payment_g_t = new AG_IAS_TEMP_PAYMENT_HEADER();
            //licenseHeader.MappingToEntity<CityFileHeader, AG_IAS_TEMP_PAYMENT_HEADER>(payment_g_t);
            //ctx.AG_IAS_TEMP_PAYMENT_HEADER.AddObject(payment_g_t);

            //foreach (CityFileDetail item in licenseHeader.CityFileDetails)
            //{
            //    AG_IAS_TEMP_PAYMENT_DETAIL detail = new AG_IAS_TEMP_PAYMENT_DETAIL();
            //    item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL>(detail);
            //    ctx.AG_IAS_TEMP_PAYMENT_DETAIL.AddObject(detail);
            //}


            //ctx.AG_IAS_TEMP_PAYMENT_TOTAL.AddObject(licenseHeader.GetAG_IAS_TEMP_PAYMENT_TOTAL());

            //try
            //{
            //    using (var ts = new TransactionScope())
            //    {
            //        ctx.SaveChanges();
            //        ts.Complete();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}  
        }

        private static DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>> CreateLicenseDataResponse()
        {
            DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>> res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>();
            res.DataResponse.Header = new List<DTO.UploadHeader>();
            res.DataResponse.Detail = new List<DTO.ReceiveLicenseDetail>();
            return res;
        }

        private static DTO.UserProfile CreateUserProfile()
        {
            return  new DTO.UserProfile()
            {
                CompCode = "",
                LoginName = "TestLicenUpload"
            };
        }


        private static DTO.UploadData ReadDataFromFile(String filename)
        {
            var res = new DTO.ResponseService<DTO.UploadLicenseResult<
                                                                        DTO.UploadHeader,
                                                                        DTO.ReceiveLicenseDetail,
                                                                        DTO.AttachFileDetail>>();

            res.DataResponse = new DTO.UploadLicenseResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail, DTO.AttachFileDetail>();

            FileInfo fileLicense = new FileInfo(Path.Combine(@"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\", filename));

            if (!fileLicense.Exists)
            {
                throw new ApplicationException(Resources.errorKTBFileTextFileUploadTest_001);
            }

            //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };


            FileStream filestream = new FileStream(fileLicense.FullName, FileMode.Open);
            using (StreamReader sr =
                   new StreamReader(filestream,
                                       System.Text.Encoding.GetEncoding("TIS-620")))
            {
                string line = sr.ReadLine().Trim();

                if (line != null && line.Length > 0)
                {
                    if (line.Substring(0, 1) == "H")
                    {
                        data.Header = line;
                    }
                    else
                    {
                        //res.ErrorMsg = "บรรทัดแรกต้องเป็น Header เท่านั้น!";
                        res.ErrorMsg = Resources.errorLicenseRequestFileUploadTest_001;
                        throw new ApplicationException(Resources.errorLicenseRequestFileUploadTest_001);
                    }


                }
                else
                {
                    res.ErrorMsg = Resources.errorLicenseRequestFileUploadTest_002;
                    throw new ApplicationException(Resources.errorLicenseRequestFileUploadTest_002);

                }

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().Length > 0)
                    {
                        data.Body.Add(line.Trim());
                    }
                }

                if (data.Body.Count == 0)
                {
                    res.ErrorMsg = Resources.errorLicenseRequestFileUploadTest_003;
                    throw new ApplicationException(Resources.errorLicenseRequestFileUploadTest_003);
                }
            }
            return data;
        }


        [TestMethod]
        public void LicenseRequestFactory_Can_Reader_ComposeFile() {
               IAS.DAL.Interfaces.IIASPersonEntities ctx =  DAL.DALFactory.GetPersonContext();
            DTO.UserProfile userProfile = CreateUserProfile();

            String filepath = @"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\83.rar";
            DTO.DataLicenseRequest request = new DataLicenseRequest(){
                                                FileName=filepath,
                                                LicenseTypeCode="01",
                                                PettitionTypeCode="01",
                                                UserProfile=userProfile 
                                            };

            DTO.ResponseService<LicenseFileHeader> res = LicenseFileFactory.ConcreateLicenseRequest(ctx, request);


            Assert.IsNotNull(res.DataResponse);


        }


        [TestMethod]
        public void LicenseRequestFactory_Can_Reader_ComposeFile2() {
               IAS.DAL.Interfaces.IIASPersonEntities ctx =  DAL.DALFactory.GetPersonContext();
            DTO.UserProfile userProfile = CreateUserProfile();

            String filepath = @"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\ขอใหม่ - li01_1001_22.rar";
            DTO.DataLicenseRequest request = new DataLicenseRequest(){
                                                FileName=filepath,
                                                LicenseTypeCode="01",
                                                PettitionTypeCode="01",
                                                UserProfile=userProfile 
                                            };

            DTO.ResponseService<LicenseFileHeader> res = LicenseFileFactory.ConcreateLicenseRequest(ctx, request);

            SummaryReceiveLicense sumary = res.DataResponse.ValidateDataOfData();

            Assert.IsNotNull(res.DataResponse);


        }
           [TestMethod]
        public void LicenseRequestFactory_Can_Reader_ComposeFile_ขอใหม่_li01_1001_22_1() {  
               IAS.DAL.Interfaces.IIASPersonEntities ctx =  DAL.DALFactory.GetPersonContext();
            DTO.UserProfile userProfile = CreateUserProfile();

            String filepath = @"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\ขอใหม่_li01_1001_22.rar";
            DTO.DataLicenseRequest request = new DataLicenseRequest(){
                                                FileName=filepath,
                                                LicenseTypeCode="01",
                                                PettitionTypeCode="11",
                                                UserProfile=userProfile 
                                            };

            DTO.ResponseService<LicenseFileHeader> res = LicenseFileFactory.ConcreateLicenseRequest(ctx, request);

            SummaryReceiveLicense sumary = res.DataResponse.ValidateDataOfData();

            Assert.IsNotNull(res.DataResponse);


        }
       
    }    
}
