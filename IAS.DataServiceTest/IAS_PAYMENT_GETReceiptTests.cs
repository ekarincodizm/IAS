using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.Utils;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class IAS_PAYMENT_GETReceiptTests
    {
        String Request = CryptoBase64.Encryption(@"2717330117871||Receipt\2717330117871\2717330117871_12122e41300037.pdf");

        [TestMethod]
        public void OnLoad_GetRequestTest()
        {

            String[] result = CryptoBase64.Decryption( Request).Split('|');

            String username = "";
            String filepath = "";

            if (result.Length == 3) {
                username = result[0];
                filepath = result[2];
            }

            Assert.AreEqual(username, "2717330117871");
            Assert.AreEqual(filepath, @"Receipt\2717330117871\2717330117871_12122e41300037.pdf");

        }
    }
}
