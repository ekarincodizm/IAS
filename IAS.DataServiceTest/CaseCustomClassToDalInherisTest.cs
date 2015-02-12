using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DAL;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class CaseCustomClassToDalInherisTest
    {
        [TestMethod]
        public void TestCase_Class_KTBFileHeader_To_AG_IAS_PAYMENT_G_T()
        {
            //BankFileHeader head = new BankFileHeader(OracleDB.GetGenAutoId(), "temp.txt");
            //head.BANK_CODE = "001";
            //head.SEQUENCE_NO = "00001";

            //AG_IAS_TEMP_PAYMENT_HEADER p = (AG_IAS_TEMP_PAYMENT_HEADER)head;

            //Assert.AreEqual(p.ID, head.ID);
            //Assert.AreEqual(p.BANK_CODE, head.BANK_CODE);
            //Assert.AreEqual(p.SEQUENCE_NO, head.SEQUENCE_NO);

        }
    }
}
