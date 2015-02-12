using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DTO;
using IAS.DataServices.Registration;
using IAS.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IAS.DataServices.Test.RegistrationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void UpdateTestTest()  
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = new IAS.DataServices.Test.Mocking.MockIASPersonEntities();

            String id = OracleDB.GetGenAutoId();
            AG_IAS_REGISTRATION_T regis1 = new AG_IAS_REGISTRATION_T()
            {
                ID = id,
                ID_CARD_NO = "1236277389234",
                NAMES = "MyName",
                LASTNAME = "LastName"
            };
            ctx.AG_IAS_REGISTRATION_T.AddObject(regis1);
            RegistationService target = new RegistationService(ctx); // TODO: Initialize to an appropriate value

            DTO.Registration entity = new DTO.Registration()
            {
                ID = id,
                ID_CARD_NO = "1234567890123",
                NAMES = "NewName",
                LASTNAME = "LastName"
            }; // TODO: Initialize to an appropriate value

            ResponseService<DTO.Registration> expected = new ResponseService<DTO.Registration>(); // TODO: Initialize to an appropriate value
            expected.DataResponse = new DTO.Registration()
            {
                ID = id,
                ID_CARD_NO = "1234567890123",
                NAMES = "NewName",
                LASTNAME = "LastName"
            };

            ResponseService<DTO.Registration> actual;
            actual = target.Update(entity);

            Assert.AreEqual(expected.DataResponse.ID, actual.DataResponse.ID);
            Assert.AreEqual(expected.DataResponse.ID_CARD_NO, actual.DataResponse.ID_CARD_NO);
            Assert.AreEqual(expected.DataResponse.NAMES, actual.DataResponse.NAMES);
            Assert.AreEqual(expected.DataResponse.LASTNAME, actual.DataResponse.LASTNAME);
            Assert.AreNotEqual(expected.DataResponse.UPDATED_DATE, actual.DataResponse.UPDATED_DATE);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }


    }

}
