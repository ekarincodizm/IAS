using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class PhaseAmountHelperTest
    {
        [TestMethod]
        public void ConvertStringAmount_Can_Phase_Test()
        {

            Decimal result;

            result = PhaseAmountHelper.ConvertStringAmount("0000000020000");
            Assert.AreEqual(result, 200);

            result = PhaseAmountHelper.ConvertStringAmount("0000000220000");
            Assert.AreEqual(result, 2200);

            result = PhaseAmountHelper.ConvertStringAmount("0000000220021");
            Assert.AreEqual(result, 2200.21);

            Decimal resultError = PhaseAmountHelper.ConvertStringAmount("000xxx2000000");
            Assert.AreEqual(result, 0);
        }
    }
}
