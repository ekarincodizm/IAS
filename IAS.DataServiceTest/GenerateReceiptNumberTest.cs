using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment;
using IAS.DataServices.Payment.Receipts;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class GenerateReceiptNumberTest
    {
        [TestMethod]
        public void CreateReceiveNumber()
        {
            BillBiz billBiz = new BillBiz();

            DateTime paymentDate = DateTime.Now;
            String billNumber = GenBillCodeFactory.GetBillNo("ar01", paymentDate.ToString("dd/MM/") + paymentDate.Year.ToString("0000"), "", "e1", "");

            Assert.AreNotEqual(billNumber, String.Empty);
        }
    }
}
