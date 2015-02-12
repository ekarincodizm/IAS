using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class FinanceAccountTest
    {
        [TestMethod]
        public void AccountInFinance_WhereTest()
        {
            IAS.DAL.Interfaces.IIASFinanceEntities ctxFin = DAL.DALFactory.GetFinanceContext();
            String user =  OICUserIdHelper.PhaseOICId("511018");
            var userInfo = ctxFin.APPS_CONFIG_INPUT
                                  .SingleOrDefault(s => s.USER_ID == user && 
                                                        s.MENU_CODE == "73050");

            Assert.AreEqual(user, "51-1-018");
            Assert.IsNotNull(user);

        }
    }
}
