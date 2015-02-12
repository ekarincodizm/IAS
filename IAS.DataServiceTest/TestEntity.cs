using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class TestEntity
    {

        [TestMethod]
        public void TestEntityWhenNot_Save()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();

            AG_IAS_PAYMENT_G_T payment = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == "999999561000000151");

            payment.STATUS = "M";

            AG_IAS_PAYMENT_G_T paymentnew = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == "999999561000000151");
            AG_IAS_PAYMENT_G_T paymentnew_3 = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == "999999561000000151" && a.STATUS=="M");
            Assert.AreEqual(payment.STATUS, paymentnew.STATUS);
            Assert.IsNotNull(paymentnew_3);

        }
    }
}
