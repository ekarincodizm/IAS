using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Registration.Helpers;
using IAS.DAL;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class EmailDataServiceTest
    {
        [TestMethod]
        public void MailConfirmHelper_Can_send_mail_registration_toMember()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_REGISTRATION_T  regis  = ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(a=>a.EMAIL=="pichit.sri@hotmail.com");

            MailConfirmHelper.SendMailConfirmRegistration(regis);
        }
    }
}
