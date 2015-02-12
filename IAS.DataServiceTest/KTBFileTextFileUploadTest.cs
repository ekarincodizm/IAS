using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServiceTest.Properties;
using IAS.DataServices.Payment.TransactionBanking.KTB;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class KTBFileTextFileUploadTest
    {
        [TestMethod]
        public void KTBBankFile_Upload_Falit()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();

            String filename = "bank_20131104.txt";


            DTO.UploadData data = ReadDataFromFile(filename);
            BankFileHeader bankHeader = BankFileFactory.ConcreateKTBFileTransfer(ctx, filename, data);

            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();

            res.DataResponse = bankHeader.ValidateData();
            if (res.IsError)
            {
                throw new ApplicationException(res.ErrorMsg);
            }
        }


        private static DTO.UploadData ReadDataFromFile(String filename)
        {
            FileInfo fileCity = new FileInfo(Path.Combine( @"D:\OIC\IAS\IAS.DataServiceTest\SimpleFile\" , filename));


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
        public void KTBBankFile_Upload_File_bank_20131106()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();

            String filename = "bank_20131106.txt";          


            DTO.UploadData data = ReadDataFromFile(filename);
            BankFileHeader bankHeader = BankFileFactory.ConcreateKTBFileTransfer(ctx, filename, data);

            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();

            res.DataResponse = bankHeader.ValidateData();
            if (res.IsError)
            {
                throw new ApplicationException(res.ErrorMsg);
            }
        }
    }
}
