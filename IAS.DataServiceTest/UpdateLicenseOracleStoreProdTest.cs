using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;
using IAS.DataServices.Payment;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class UpdateLicenseOracleStoreProdTest
    {
        protected IAS.DAL.Interfaces.IIASFinanceEntities ctx;

        [TestInitialize]
        public void Setup()
        {


            this.ctx = DAL.DALFactory.GetFinanceContext();
        }

        [TestMethod]
        public void TestUploadFileBankSubmit_Then_Update_License_By_OracleStoreProdcedure()
        {
            ClearValueUploadFileBank("xxxxxxxxxxxxxxxxxx");

            PaymentService paymentService = new PaymentService();
            List<String> request = new List<string>();
            request.Add("");


            //paymentService.Insert12T(request);

            OracleDB ora = new OracleDB();



            Assert.AreEqual("1", "2");


        }

        private void ClearValueUploadFileBank(String groupRequestNo) { 
        
        
        }
    }
}
