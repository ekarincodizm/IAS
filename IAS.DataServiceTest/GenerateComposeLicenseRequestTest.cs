using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;
using IAS.Utils;
using System.IO;
using System.Globalization;

using System.Configuration;
using Ionic.Zip;
using IAS.DTO.FileService;
using IAS.FileService.FileManager;


namespace IAS.DataServiceTest
{
    [TestClass]
    public class GenerateComposeLicenseRequestTest
    {
        private String _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
        private String _tempPath = ConfigurationManager.AppSettings["TEMP_FOLDER_ATTACH"];
        private String _oicPath = ConfigurationManager.AppSettings["OIC_FOLDER_ATTACH"];


        private IList<AG_IAS_LICENSE_TYPE_R> _licenseTypeRs = new List<AG_IAS_LICENSE_TYPE_R>();
        private IList<AG_IAS_PETITION_TYPE_R> _petitionTypeRs = new List<AG_IAS_PETITION_TYPE_R>();
        private IList<AG_IAS_PAYMENT_G_T> _paymentGTs = new List<AG_IAS_PAYMENT_G_T>();
        private IList<AG_IAS_SUBPAYMENT_H_T> _subPaymentHTs = new List<AG_IAS_SUBPAYMENT_H_T>();
        private IList<AG_IAS_SUBPAYMENT_D_T> _subPaymentDTs = new List<AG_IAS_SUBPAYMENT_D_T>();
        private IList<AG_IAS_LICENSE_H> _licenseHs = new List<AG_IAS_LICENSE_H>();
        private IList<AG_IAS_LICENSE_D> _licenseDs = new List<AG_IAS_LICENSE_D>();
        private IList<AG_IAS_ATTACH_FILE_LICENSE> _attachLicenses = new List<AG_IAS_ATTACH_FILE_LICENSE>();
        private String _compCode;
        private String _compName;
        private String _userId;

        public String CompanyCode { get { return _compCode; } set { _compCode=value;} }
        public String CompanyName { get { return _compName; } set { _compName=value;} }
        public String UserId { get { return _userId; } set { _userId=value;} }
        public IEnumerable<AG_IAS_LICENSE_TYPE_R> LicenseTypeRs { get { return _licenseTypeRs; } }
        public IEnumerable<AG_IAS_PETITION_TYPE_R> PetitionTypeRs { get { return _petitionTypeRs; } }
        public IEnumerable<AG_IAS_PAYMENT_G_T> PaymentGTs { get { return _paymentGTs; } }
        public IEnumerable<AG_IAS_SUBPAYMENT_H_T> SubPaymentHTs { get { return _subPaymentHTs; } }
        public IEnumerable<AG_IAS_SUBPAYMENT_D_T> SubPaymentDTs { get { return _subPaymentDTs; } }
        public IEnumerable<AG_IAS_LICENSE_H> LicenseHs { get { return _licenseHs; } }
        public IEnumerable<AG_IAS_LICENSE_D> LicenseDs { get { return _licenseDs; } }
        public IEnumerable<AG_IAS_ATTACH_FILE_LICENSE> AttachLicenses { get { return _attachLicenses; } }

        [TestInitialize]
        public void Intit() { 
        
        
        }


        public void InitialDataMocking() 
        {
            // ***********  cost data ********************//
            CompanyCode = "3139";
            CompanyName = "บริษัท ไทยพาณิชย์ประกันภัย จำกัด (มหาชน)";
            _userId = "0000000000000000";
            DateTime oneYearStart = new DateTime(2013, 1, 1);
            DateTime oneYearEnd = new DateTime(2014, 12, 31);
            DateTime fiveYearStart = new DateTime(2013, 1, 1);
            DateTime fiveYearEnd = new DateTime(2018, 12, 31);
            /*********************************************/
            AG_IAS_LICENSE_H licenseH1 = CreateLicenseH("131004104328818", CompanyCode, CompanyName, 3, "13","01");
            _licenseHs.Add(licenseH1);
            _licenseDs.Add(CreateLicenseD(licenseH1, (1).ToString("0000"), "L000000111", oneYearStart, oneYearEnd,
                                                    "1111111111111", "นาย", "กไก่", "กุ่กกุ๊ก", UserId));
            _licenseDs.Add(CreateLicenseD(licenseH1, (2).ToString("0000"), "L000000112", oneYearStart, oneYearEnd,
                                                    "1111111111112", "นาย", "ขไข่", "อยู่ในเล่า",UserId));
            _licenseDs.Add(CreateLicenseD(licenseH1, (3).ToString("0000"), "L000000113", oneYearStart, oneYearEnd,
                                                    "1111111111113", "นาย", "คควาย", "เข้านา",UserId));

            AG_IAS_LICENSE_H licenseH2 = CreateLicenseH("131004104328819", CompanyCode, CompanyName, 3, "14", "02");
            _licenseHs.Add(licenseH2);
            _licenseDs.Add(CreateLicenseD(licenseH2, (1).ToString("0000"), "L000000221", fiveYearStart, oneYearEnd,
                                                  "2222222222221", "นาง", "กไก่", "กุ่กกุ๊ก", UserId));
            _licenseDs.Add(CreateLicenseD(licenseH2, (2).ToString("0000"), "L000000222", fiveYearStart, fiveYearEnd,
                                                    "2222222222222", "นาง", "ขไข่", "อยู่ในเล่า", UserId));
            _licenseDs.Add(CreateLicenseD(licenseH2, (3).ToString("0000"), "L000000223", fiveYearStart, fiveYearEnd,
                                                    "2222222222223", "นาง", "คควาย", "เข้านา", UserId));


            AG_IAS_PAYMENT_G_T paymentGT = CreatePaymentGT("999999560900000001", "P", 6);
            _paymentGTs.Add(paymentGT);

            AG_IAS_SUBPAYMENT_H_T subHT1 = CreateSubPaymentHT(paymentGT, "1309231620210000", "13", "P", 3);
            _subPaymentHTs.Add(subHT1);
            DateTime d = DateTime.Now;
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT1, (1).ToString("0000"),"1111111111111" ,"A", "12122e11300001", d));
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT1, (2).ToString("0000"), "1111111111112", "A", "12122e11300002", d));
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT1, (3).ToString("0000"), "1111111111113", "A", "12122e11300003", d)); 

            AG_IAS_SUBPAYMENT_H_T subHT2 = CreateSubPaymentHT(paymentGT, "1309231620220000", "14", "P", 3);
            _subPaymentHTs.Add(subHT2);
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT2, (1).ToString("0000"), "2222222222221", "A", "12122e11300004", d));
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT2, (2).ToString("0000"), "2222222222222", "A", "12122e11300005", d));
            _subPaymentDTs.Add(CreateSubPaymentDT(subHT2, (3).ToString("0000"), "2222222222223", "A", "12122e11300006", d));

            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
             _licenseTypeRs = ctx.AG_IAS_LICENSE_TYPE_R.ToList();
             _petitionTypeRs = ctx.AG_IAS_PETITION_TYPE_R.ToList();

        }

        [TestMethod]
        public void TestInitailDataPromeUse() 
        {
            Assert.AreEqual(LicenseHs.Count(), 2);
            Assert.AreEqual(LicenseDs.Count(), 6);

            Assert.AreEqual(PaymentGTs.Count(), 1);
            Assert.AreEqual(SubPaymentHTs.Count(), 2);
            Assert.AreEqual(SubPaymentDTs.Count(), 6); 
        
        }

        [TestMethod]
        public void TestSelect_PaymentRequestLicense_From_DAL()   
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            IList<AG_IAS_LICENSE_H> _licenseHs = new List<AG_IAS_LICENSE_H>();
            IList<AG_IAS_LICENSE_D> _licenseDs = new List<AG_IAS_LICENSE_D>();
            _licenseTypeRs = ctx.AG_IAS_LICENSE_TYPE_R.ToList();
            _petitionTypeRs = ctx.AG_IAS_PETITION_TYPE_R.ToList();

            String zipName = "TestZip";
            DirectoryInfo zipFolder = CreateDirectory(_netDrive, zipName, 0);

            //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
            var paymentGTs = ctx.AG_IAS_PAYMENT_G_T.Where(f => f.STATUS == "P" && f.GROUP_REQUEST_NO=="999999561000000040")
                               .ToList();

            foreach (AG_IAS_PAYMENT_G_T paymentGT in paymentGTs)
            {
                //หาข้อมูลที่ Sub Payment Head
                //var subPaymentHTs = ctx.AG_IAS_SUBPAYMENT_H_T
                //                    .Where(w => w.GROUP_REQUEST_NO == paymentGT.GROUP_REQUEST_NO &&
                //                              (w.STATUS != null && w.STATUS == "P"))
                //                    .ToList();
                var subPaymentHTs = ctx.AG_IAS_SUBPAYMENT_H_T
                                .Where(w => w.GROUP_REQUEST_NO==paymentGT.GROUP_REQUEST_NO
                                            && (w.STATUS != null && w.STATUS == "P")  &&
                                            (
                                                 w.PETITION_TYPE_CODE=="11"
                                                || w.PETITION_TYPE_CODE == "13"
                                                || w.PETITION_TYPE_CODE == "14"
                                                || w.PETITION_TYPE_CODE == "15"
                                                || w.PETITION_TYPE_CODE == "16"
                                                || w.PETITION_TYPE_CODE == "17"
                                                || w.PETITION_TYPE_CODE == "18"
                                            )
                                        ).ToList();
                                

                foreach (AG_IAS_SUBPAYMENT_H_T SubPaymentHT in subPaymentHTs)
                {

                    IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPaymentDTs =
                        ctx.AG_IAS_SUBPAYMENT_D_T.Where(w => w.HEAD_REQUEST_NO == SubPaymentHT.HEAD_REQUEST_NO &&
                                            !String.IsNullOrEmpty(w.RECEIPT_NO));


                    foreach (AG_IAS_SUBPAYMENT_D_T subPaymentDT in subPaymentDTs)
                    {
                        AG_IAS_LICENSE_D licenD = ctx.AG_IAS_LICENSE_D.SingleOrDefault(w => w.UPLOAD_GROUP_NO == subPaymentDT.UPLOAD_GROUP_NO &&
                                                            w.SEQ_NO == subPaymentDT.SEQ_NO);
                        AG_IAS_LICENSE_H licenH = ctx.AG_IAS_LICENSE_H.Single(w => w.UPLOAD_GROUP_NO == licenD.UPLOAD_GROUP_NO);

                        AG_IAS_ATTACH_FILE_LICENSE attach = ctx.AG_IAS_ATTACH_FILE_LICENSE.Single(a => a.ID_CARD_NO == licenD.ID_CARD_NO
                                                                                && a.GROUP_LICENSE_ID == licenD.UPLOAD_GROUP_NO
                                                                                && a.ATTACH_FILE_PATH.EndsWith("03.jpg"));
                        AG_IAS_LICENSE_TYPE_R licenType = LicenseTypeRs.Single(l => l.LICENSE_TYPE_CODE == subPaymentDT.LICENSE_TYPE_CODE);


                        String filePath = String.Format(@"{0}\{1}\{2}", zipFolder.FullName, SubPaymentHT.PETITION_TYPE_CODE, licenType.LICENSE_TYPE_NAME);
                        DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(_netDrive, filePath));
                        if (!dirInfo.Exists)
                            dirInfo.Create();

                        FileInfo fileInfo = new FileInfo(Path.Combine(dirInfo.FullName, "1.txt"));
                        if (!fileInfo.Exists)
                        {
                            using (System.IO.StreamWriter file = fileInfo.CreateText())
                            {
                                file.WriteLine("ชื่อรูป,เลขที่ใบอนุญาต,เลขบัตรประชาชน,ชื่อ,สกุล,วันที่ออกใบอนุญาต,วันที่หมดอายุ,บริษัท,ประเภทใบอนุญาต,");
                            }
                        }


                        using (System.IO.StreamWriter file = fileInfo.AppendText())
                        {
                            file.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},", subPaymentDT.ID_CARD_NO,
                                                                                                WordSpacing(subPaymentDT.LICENSE_NO),
                                                                                                WordSpacing(subPaymentDT.ID_CARD_NO),
                                                                                                String.Format("{0} {1}", licenD.TITLE_NAME, licenD.NAMES),
                                                                                                licenD.LASTNAME,
                                                                                                ((DateTime)licenD.LICENSE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                                ((DateTime)licenD.LICENSE_EXPIRE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                                licenH.COMP_NAME,
                                                                                                licenType.LICENSE_TYPE_NAME));
                        }


                    }


                }


            }
        }

        [TestMethod]
        public void TestSelect_PaymentRequestLicense_From_MockData()
        {

            String zipName = "TestZip6";

            DirectoryInfo zipFolder = CreateDirectory(Path.Combine(_netDrive, _oicPath), zipName, 0);
             
            //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
            IEnumerable<AG_IAS_PAYMENT_G_T> paymentGTs = GetPaymentGTs();

            foreach (AG_IAS_PAYMENT_G_T paymentGT in paymentGTs)
            {
                //หาข้อมูลที่ Sub Payment Head
                IEnumerable<AG_IAS_SUBPAYMENT_H_T> subPaymentHTs = GetSubPaymentHead(paymentGT);

                foreach (AG_IAS_SUBPAYMENT_H_T SubPaymentHT in subPaymentHTs)
                {

                    IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPaymentDTs = GetSubPaymentDetails(SubPaymentHT);


                    foreach (AG_IAS_SUBPAYMENT_D_T subPaymentDT in subPaymentDTs)    
                    {
                        AG_IAS_LICENSE_D licenD = LicenseDs.SingleOrDefault(w => w.UPLOAD_GROUP_NO == subPaymentDT.UPLOAD_GROUP_NO &&
                                                            w.SEQ_NO == subPaymentDT.SEQ_NO);
                        AG_IAS_LICENSE_H licenH = LicenseHs.Single(w=>w.UPLOAD_GROUP_NO==licenD.UPLOAD_GROUP_NO);
                       
                        AG_IAS_LICENSE_TYPE_R licenType = LicenseTypeRs.Single(l=>l.LICENSE_TYPE_CODE==subPaymentDT.LICENSE_TYPE_CODE);

                        AG_IAS_ATTACH_FILE_LICENSE attach = AttachLicenses.Single(a => a.ID_CARD_NO == licenD.ID_CARD_NO
                                                           && a.GROUP_LICENSE_ID == licenD.UPLOAD_GROUP_NO
                                                           && a.ATTACH_FILE_PATH.EndsWith("03.jpg"));
                        AddLicenseRequest(zipFolder, SubPaymentHT, subPaymentDT, licenD, licenH, licenType, attach);

                    }


                }

       
            }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(zipFolder.FullName); // recurses subdirectories
                zip.Save(zipFolder.FullName + ".zip");

            }
        }


        #region Method Get File
        private IEnumerable<AG_IAS_PAYMENT_G_T> GetPaymentGTs()
        {
            IEnumerable<AG_IAS_PAYMENT_G_T> paymentGTs = PaymentGTs.Where(f => f.STATUS == "P" && f.GROUP_REQUEST_NO == "999999560900000001")
                               .ToList();
            return paymentGTs;
        }

        private void AddLicenseRequest(DirectoryInfo zipFolder, AG_IAS_SUBPAYMENT_H_T SubPaymentHT, AG_IAS_SUBPAYMENT_D_T subPaymentDT,
                    AG_IAS_LICENSE_D licenD, AG_IAS_LICENSE_H licenH, AG_IAS_LICENSE_TYPE_R licenType, AG_IAS_ATTACH_FILE_LICENSE attach)  
        {
            String filePath = String.Format(@"{0}\{1}\{2}", zipFolder.FullName, SubPaymentHT.PETITION_TYPE_CODE, licenType.LICENSE_TYPE_CODE);
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(_netDrive, filePath));
            if (!dirInfo.Exists)
                dirInfo.Create();

            FileInfo fileInfo = new FileInfo(Path.Combine(dirInfo.FullName, "1.txt"));
            if (!fileInfo.Exists)
            {
                using (System.IO.StreamWriter file = fileInfo.CreateText())
                {
                    file.WriteLine("ชื่อรูป,เลขที่ใบอนุญาต,เลขบัตรประชาชน,ชื่อ,สกุล,วันที่ออกใบอนุญาต,วันที่หมดอายุ,บริษัท,ประเภทใบอนุญาต,");
                }
            }


            using (System.IO.StreamWriter file = fileInfo.AppendText())
            {
                file.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},", subPaymentDT.ID_CARD_NO,
                                                                                    WordSpacing(subPaymentDT.LICENSE_NO),
                                                                                    WordSpacing(subPaymentDT.ID_CARD_NO),
                                                                                    String.Format("{0} {1}", licenD.TITLE_NAME, licenD.NAMES),
                                                                                    licenD.LASTNAME,
                                                                                    ((DateTime)licenD.LICENSE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    ((DateTime)licenD.LICENSE_EXPIRE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    licenH.COMP_NAME,
                                                                                    licenType.LICENSE_TYPE_NAME));
            }

     

            Int32 start = attach.ATTACH_FILE_PATH.LastIndexOf('.');
            Int32 len = attach.ATTACH_FILE_PATH.Length;
            String extension = attach.ATTACH_FILE_PATH.Substring(attach.ATTACH_FILE_PATH.LastIndexOf('.'), len - start);

            MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
            {
                CurrentContainer = "",
                CurrentFileName = attach.ATTACH_FILE_PATH,
                TargetContainer = String.Format(@"{0}\{1}", dirInfo.FullName.Replace(_netDrive, ""), "images"),
                TargetFileName = String.Format("{0}{1}", licenD.ID_CARD_NO, extension)
            }).Action();
            if (response.Code != "0000")
                throw new ApplicationException("cannot movefile");
        }

        private IEnumerable<AG_IAS_SUBPAYMENT_D_T> GetSubPaymentDetails(AG_IAS_SUBPAYMENT_H_T SubPaymentHT)
        {
            IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPaymentDTs =
                SubPaymentDTs.Where(w => w.HEAD_REQUEST_NO == SubPaymentHT.HEAD_REQUEST_NO &&
                                    !String.IsNullOrEmpty(w.RECEIPT_NO));
            return subPaymentDTs;
        }

        private IEnumerable<AG_IAS_SUBPAYMENT_H_T> GetSubPaymentHead(AG_IAS_PAYMENT_G_T paymentGT)
        {
            IEnumerable<AG_IAS_SUBPAYMENT_H_T> subPaymentHTs = SubPaymentHTs
                                 .Where(w => w.GROUP_REQUEST_NO == paymentGT.GROUP_REQUEST_NO
                                        && (w.STATUS != null && w.STATUS == "P") &&
                                        (
                                             w.PETITION_TYPE_CODE == "11"
                                            || w.PETITION_TYPE_CODE == "13"
                                            || w.PETITION_TYPE_CODE == "14"
                                            || w.PETITION_TYPE_CODE == "15"
                                            || w.PETITION_TYPE_CODE == "16"
                                            || w.PETITION_TYPE_CODE == "17"
                                            || w.PETITION_TYPE_CODE == "18"
                                        )
                                    ).ToList();
            return subPaymentHTs;
        }

        public DirectoryInfo CreateDirectory(String path, String folderName, Int16 num) 
        {
            DirectoryInfo dirInfo;
            if (num == 0)
            {
                dirInfo = new DirectoryInfo(Path.Combine(path, folderName));
            }
            else {
                dirInfo = new DirectoryInfo(Path.Combine(path, folderName + " (" + num.ToString() + ")"));
            }
            
            if (dirInfo.Exists){
                num++;
               dirInfo = CreateDirectory(path, folderName , num);
            }    
            else {
                dirInfo.Create();
            }
            return dirInfo;
        }

        public String WordSpacing(String word) {
            StringBuilder result = new StringBuilder("");
            foreach (Char item in word.ToArray())
            {
               result.Append(item.ToString()+" " );
            }
            return result.ToString();
        }


        #endregion

        #region Method InitialData
        private AG_IAS_LICENSE_H CreateLicenseH(String group_no, String compCode, String compName, Int32 lots,String petitionCode, String licentypeCode) {
            return new AG_IAS_LICENSE_H() { 
                    UPLOAD_GROUP_NO=group_no,
                    COMP_CODE=compCode,  
                    COMP_NAME = compName,
                    LOTS=lots ,
                    PETITION_TYPE_CODE = petitionCode ,
                    LICENSE_TYPE_CODE =  licentypeCode
                };
        }
        private AG_IAS_LICENSE_D CreateLicenseD(AG_IAS_LICENSE_H h, String seq, String lencenseNo, DateTime licenseStart, 
                    DateTime licenseEnd,  String idCard, String titleName, String names, String lastName, String userId) {


            AG_IAS_LICENSE_D license = new AG_IAS_LICENSE_D()
            {
                UPLOAD_GROUP_NO = h.UPLOAD_GROUP_NO,
                SEQ_NO = seq,
                LICENSE_NO = lencenseNo,
                LICENSE_DATE = licenseStart,
                LICENSE_EXPIRE_DATE = licenseEnd,
                ID_CARD_NO = idCard,
                TITLE_NAME = titleName,
                NAMES = names,
                LASTNAME = lastName
            };
            CreateAttachFileLicense(license, userId);
            return license;
        }
                                                      
        private AG_IAS_PAYMENT_G_T CreatePaymentGT(String groupNo, String Status, Int16 numPerson) {
            return new AG_IAS_PAYMENT_G_T()
                        {
                            GROUP_REQUEST_NO = groupNo,
                            STATUS = Status,
                            SUBPAYMENT_QTY = numPerson
                        };
        }
        private AG_IAS_SUBPAYMENT_H_T CreateSubPaymentHT(AG_IAS_PAYMENT_G_T gt, String headNo, String pettNo, String status, Int16 numPerson) {
           return new AG_IAS_SUBPAYMENT_H_T()
            {
                GROUP_REQUEST_NO = gt.GROUP_REQUEST_NO,
                HEAD_REQUEST_NO = headNo,
                PETITION_TYPE_CODE = pettNo,
                STATUS = status,
                PERSON_NO = numPerson
            };
        }
        private AG_IAS_SUBPAYMENT_D_T CreateSubPaymentDT(AG_IAS_SUBPAYMENT_H_T ht ,String paymentNo,String idCard,  
                                            String status, String receiptNo, DateTime receiptDate) 
        {
            AG_IAS_LICENSE_D licenD = LicenseDs.Single(l => l.ID_CARD_NO == idCard);
            AG_IAS_LICENSE_H licenH = LicenseHs.Single(l => l.UPLOAD_GROUP_NO == licenD.UPLOAD_GROUP_NO);
            return new AG_IAS_SUBPAYMENT_D_T()
            {
                PAYMENT_NO=paymentNo,
                HEAD_REQUEST_NO=ht.HEAD_REQUEST_NO,
                ID_CARD_NO = licenD.ID_CARD_NO,
                LICENSE_NO = licenD.LICENSE_NO,
                LICENSE_TYPE_CODE = licenH.LICENSE_TYPE_CODE,
                RECORD_STATUS=status,
                RECEIPT_NO=receiptNo,
                RECEIPT_DATE=receiptDate,
                UPLOAD_GROUP_NO = licenD.UPLOAD_GROUP_NO,
                SEQ_NO = licenD.SEQ_NO
                
            };
        }
        private void CreateAttachFileLicense(AG_IAS_LICENSE_D license,String userId)
        {
           
            for (int i = 1; i <= 3; i++) 
            {
                String fullName = String.Format(@"AttachFile\{0}\{1}_{2}.jpg", userId, license.ID_CARD_NO, i.ToString("00"));
                _attachLicenses.Add(new AG_IAS_ATTACH_FILE_LICENSE()
                {
                    ID_ATTACH_FILE = OracleDB.GetGenAutoId(), 
                    ID_CARD_NO = license.ID_CARD_NO,
                    ATTACH_FILE_PATH = fullName,
                    FILE_STATUS = "A",
                    GROUP_LICENSE_ID = license.UPLOAD_GROUP_NO
                });
            }
            
        }
        #endregion



        #region Test SubCase
        [TestMethod]
        public void TestCreateDirectory() {
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(_netDrive, @"Zip\01"));
            if (!dirInfo.Exists)
                dirInfo.Create();
        }

        [TestMethod]
        public void TestSpaceTest() {
            String idcard = "1929900071638";

            String wordspace = WordSpacing(idcard);

            Assert.AreEqual(wordspace, "1 9 2 9 9 0 0 0 7 1 6 3 8 "); 
        }

        [TestMethod]
        public void TestWhereEntity() {
            var subPaymentHTs = SubPaymentHTs
                             .Where(w => w.GROUP_REQUEST_NO == "999999560900000001" &&
                                       (w.STATUS != null && w.STATUS == "P"));
             Assert.AreEqual(subPaymentHTs.Count(), 2);
        }

        [TestMethod]
        public void TestCreateDirectory_And_Ordering_name_when_Exitsed()
        {
           DirectoryInfo isCreate =  CreateDirectory(_netDrive, "LicenFile", 0);

            Assert.IsTrue(isCreate.Exists);

        }

        [TestMethod]
        public void TestSelectGroupPaymentLicenseDaily() { 
             IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
             var paymentGTs = ctx.AG_IAS_PAYMENT_G_T.Where(a=>a.STATUS=="P").GroupBy(p => p.PAYMENT_DATE);
             IList<DateTime> importDate = new List<DateTime>();

             foreach (var item in paymentGTs.ToList())
             {
                 importDate.Add((DateTime)item.Key); 
             }

             Assert.IsNotNull(paymentGTs);
             Assert.AreEqual(importDate.Count(), 4);
        }


        #endregion
    }
}
