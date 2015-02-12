using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment.TransactionBanking;
using System.IO;
using System.Threading;
using System.Transactions;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.Payment.Mapper;
using IAS.DataServiceTest.Properties;
using IAS.DataServices.Payment.TransactionBanking.Citi;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class CityBankTextFileTest
    {
        private const String _type1 = "1  5xxxxxx012                    Citibank                      190720110407xxxxxxxxxxxx";
        private const String _type2 = "2          xxxxxxxxxxCitibank  ";
        private const String _type4 = "43155612                 5    13155612                 20018072011        18682.20121673              1100099738          2163488110701       S                                                                                                                                                                                                                                                                                .00             .00             .00             .00                    1";
        private const String _type5 = "5          0000000                 1Citibank            xxxxxx    ";
        private const String _type6 = "60000000                 1    10000000             CASH 004                                     THBCSH       19072011       334410.31          0004  0040233   000                 2163418110701                 xxxxxxx                                                               2067                xxxxxxx                                                               xxxxxx              xxxxxx                                                                                                                                                                                                                                                                                                                                                                                                                              2163418110701       190720111907201119072011                                                                                                         1907201119072011091028004023          xxxxxx                                                             334410.31";
        private const String _type7 = "7          0000000                 1          334410.31121673    ";
        private const String _type8 = "8                    334410.31";
        private const String _type9 = "9    33";


        [TestMethod]
        public void CityBankTextFileHeaderType1_Can_SubString_To_BankFileHeader()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            CityFileHeader bankHeader = new CityFileHeader(ctx)
                                            {
                                                RECORD_TYPE = _type1.Substring(0, 1),
                                                SEQUENCE_NO = "",
                                                BANK_CODE = _type1.Substring(33, 3),
                                                COMPANY_ACCOUNT= _type1.Substring(3, 10),
                                                COMPANY_NAME = "",
                                                EFFECTIVE_DATE = "",
                                                SERVICE_CODE = ""
                                            };

            Assert.AreEqual(bankHeader.RECORD_TYPE, "1");
            Assert.AreEqual(bankHeader.BANK_CODE, "Cit");
            Assert.AreEqual(bankHeader.COMPANY_ACCOUNT, "5xxxxxx012");
        }

        [TestMethod]
        public void CityBankTextFileDetailType6_Can_SubString_To_BankFileDetail() {
            CityFileDetail bankDetial = new CityFileDetail()
            {
                ID = "",
                RECORD_TYPE = _type6.Substring(0, 1),
                SequenceNo = "",
                BANK_CODE = _type6.Substring(144, 3),
                COMPANY_ACCOUNT = _type6.Substring(3, 10),
                PAYMENT_DATE = _type6.Substring(109, 8),
                PAYMENT_TIME = "",
                CUSTOMER_NAME = _type6.Substring(209, 70),
                CUSTOMER_NO_REF1 = _type6.Substring(389, 20),
                REF2 = _type6.Substring(529, 20),
                REF3 = "",
                BRANCH_NO = _type6.Substring(152, 4),
                TELLER_NO = "",
                KIND_OF_TRANSACTION = "",
                TRANSACTION_CODE = _type6.Substring(51, 3),
                CHEQUE_NO = "",
                AMOUNT = _type6.Substring(120, 13) ,
                CHEQUE_BANK_CODE = ""
                                            };

            Assert.AreEqual(bankDetial.RECORD_TYPE, "6");
            Assert.AreEqual(bankDetial.BANK_CODE, "004");
            Assert.AreEqual(bankDetial.COMPANY_ACCOUNT, "00000     ");
            Assert.AreEqual(bankDetial.PAYMENT_DATE, "19072011");
            Assert.AreEqual(bankDetial.CUSTOMER_NAME, "xxxxxxx                                                               ");
            Assert.AreEqual(bankDetial.CUSTOMER_NO_REF1, "xxxxxx              ");
            Assert.AreEqual(bankDetial.REF2, "                    ");
            Assert.AreEqual(bankDetial.BRANCH_NO, "0233");
            Assert.AreEqual(bankDetial.TRANSACTION_CODE, "CAS");
            Assert.AreEqual(bankDetial.AMOUNT, "    334410.31");


        }

        [TestMethod]
        public void CityBankTextFileTotalType8_Can_SubString_To_BankFileTotal() {
            CityFileTotal bankTotal = new CityFileTotal() {
                RECORD_TYPE = _type8.Substring(0, 1),
                SEQUENCE_NO = "",
                BANK_CODE = "",
                COMPANY_ACCOUNT = _type8.Substring(3, 10),
                TOTAL_DEBIT_AMOUNT = "",
                TOTAL_DEBIT_TRANSACTION = "",
                TOTAL_CREDIT_AMOUNT = _type8.Substring(14, 16),
                TOTAL_CREDIT_TRANSACTION = ""
            };

            Assert.AreEqual(bankTotal.RECORD_TYPE, "8");
            Assert.AreEqual(bankTotal.COMPANY_ACCOUNT, "          ");
            Assert.AreEqual(bankTotal.TOTAL_CREDIT_AMOUNT, "       334410.31");
        
        }

        [TestMethod]
        public void CityBankTextFile_CanLoadFile_AndValidateData() {
            DTO.UploadData data = ReadDataFromFile();

            IAS.DAL.Interfaces.IIASPersonEntities ctx =  DAL.DALFactory.GetPersonContext();

            CityFileHeader header = BankFileFactory.ConcreateCityBankFileTransfer(ctx, "test.txt", data);

            Assert.IsNotNull(header);
            Assert.AreEqual(header.CityFileBoxHeaders.Count(), 3);
            Assert.AreEqual(header.CityFileOverFlows.Count(), 7);
            Assert.AreEqual(header.CityFileBatchHeaders.Count(), 6);
            Assert.AreEqual(header.CityFileDetails.Count(), 6);
            Assert.AreEqual(header.CityFileBatchTotals.Count(), 6);
            Assert.AreEqual(header.CityFileTotals.Count(), 3);
            
       
            Assert.AreEqual(header.CityFileTotals.ElementAt(0).CityFileDetails.Count(), 1);
            Assert.AreEqual(header.CityFileTotals.ElementAt(1).CityFileDetails.Count(), 3);
            Assert.AreEqual(header.CityFileTotals.ElementAt(2).CityFileDetails.Count(), 2);
   
            Int32 rownum = (1 + header.CityFileBoxHeaders.Count()
                                +header.CityFileOverFlows.Count()
                                +header.CityFileBatchHeaders.Count() 
                                + header.CityFileDetails.Count()
                                + header.CityFileBatchTotals.Count()
                                + header.CityFileTotals.Count()
                                + 1);
            Assert.AreEqual(header.RowCount, rownum);


            DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction> uploadResult = header.ValidateData();  
   


        }

        private static DTO.UploadData ReadDataFromFile()
        {
            FileInfo fileCity = new FileInfo(@"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\Industrial_Generic_Deposit_Handoff_xxxxxx_19072011.txt");


            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();

            if (!fileCity.Exists)
            {
                throw new ApplicationException(Resources.errorKTBFileTextFileUploadTest_001);
            }

            //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };


            FileStream filestream = new FileStream(fileCity.FullName, FileMode.Open);
            using (StreamReader sr = new StreamReader(filestream, System.Text.Encoding.GetEncoding("TIS-620")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        data.Body.Add(line);
                    }
                }
            }
            return data;
        }
                                      
        [TestMethod]
        public void CityBank_Upload_Can_Commit_ToEntity() 
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            DTO.UploadData data = ReadDataFromFile();
            String fileName = "testData.txt";
            CityFileHeader bankHeader = BankFileFactory.ConcreateCityBankFileTransfer(ctx, fileName, data);

            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();

            res.DataResponse = bankHeader.ValidateData();
            if (res.IsError)
            {
                throw new ApplicationException(res.ErrorMsg);
            }

            AG_IAS_TEMP_PAYMENT_HEADER payment_g_t = new AG_IAS_TEMP_PAYMENT_HEADER();
            bankHeader.MappingToEntity<CityFileHeader, AG_IAS_TEMP_PAYMENT_HEADER>(payment_g_t);
            ctx.AG_IAS_TEMP_PAYMENT_HEADER.AddObject(payment_g_t);

            foreach (CityFileDetail item in bankHeader.CityFileDetails)
            {
                AG_IAS_TEMP_PAYMENT_DETAIL detail = new AG_IAS_TEMP_PAYMENT_DETAIL();
                item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL>(detail);
                ctx.AG_IAS_TEMP_PAYMENT_DETAIL.AddObject(detail);
            }


            ctx.AG_IAS_TEMP_PAYMENT_TOTAL.AddObject(bankHeader.GetAG_IAS_TEMP_PAYMENT_TOTAL());

            try
            {
                using (var ts = new TransactionScope())
                {
                    ctx.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

    }
}
