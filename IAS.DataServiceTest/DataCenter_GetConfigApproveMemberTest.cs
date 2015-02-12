using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class DataCenter_GetConfigApproveMemberTest
    {
        [TestMethod]
        public void CanGetConfig01_02_03()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            var list = ctx.AG_IAS_APPROVE_CONFIG
                             .Where(s => s.ITEM_TYPE == "01" &&
                                         "01_02_03".Contains(s.ID))
                             .Select(s => new DTO.ConfigEntity
                             {
                                 Id = s.ID,
                                 Name = s.ITEM,
                                 Value = s.ITEM_VALUE,
                                 Description = s.DESCRIPTION
                             }).ToList();

            Assert.AreEqual(list.Count, 3);
        }
    }
}
