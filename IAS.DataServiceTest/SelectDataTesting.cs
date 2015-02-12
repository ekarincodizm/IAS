using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class SelectDataTesting
    {
        [TestMethod]
        public void Test()
        {
            String email = "tikclicker@hotmail.com";
            String username = "1944316199773";
            String idCard = "1944316199773";

            String emailchange = "personal@iasoic.or.th";


            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_PERSONAL_T entExist = ctx.AG_IAS_PERSONAL_T
                  .FirstOrDefault(s => ((s.EMAIL == email)) && (s.ID_CARD_NO != idCard ));

            Assert.IsNull(entExist);

            AG_IAS_PERSONAL_T entExistChange = ctx.AG_IAS_PERSONAL_T
                   .FirstOrDefault(s => ((s.EMAIL == emailchange)) && (s.ID_CARD_NO != idCard ));

            AG_IAS_PERSONAL_T entExistChange2 = ctx.AG_IAS_PERSONAL_T     
                   .FirstOrDefault(s => ((s.EMAIL == emailchange)) && (s.ID_CARD_NO != idCard));

            Assert.IsNotNull(entExistChange);
       
        }
    }
}
