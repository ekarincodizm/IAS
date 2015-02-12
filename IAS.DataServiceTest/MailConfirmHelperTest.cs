using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;
using IAS.DataServices.Registration.Helpers;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class MailConfirmHelperTest
    {
        [TestMethod]
        public void SendMailConfirmRegistration_Can_Sent_Email()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();

            AG_IAS_REGISTRATION_T regis = ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(a => a.ID_CARD_NO == "1544447642968");

            Assert.IsNotNull(regis);

            Boolean result = MailConfirmHelper.SendMailConfirmRegistration(regis);

            Assert.IsTrue(result);


        }
    }
}
