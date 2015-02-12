using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;
using IAS.Utils;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class MethodDuplicateTest
    {
        [TestMethod]
        public void Duplicate_Data_Test_Get_Clear_Duplicate()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            IEnumerable<AG_IAS_ATTACH_FILE> attachFiles = ctx.AG_IAS_ATTACH_FILE.Where(a=>a.REGISTRATION_ID=="131106132209758" );

            if (attachFiles != null) {
                Assert.AreEqual(attachFiles.Count(), 10);
                IEnumerable<AG_IAS_ATTACH_FILE> attachFileUndup = DistinctDuplicatesHelper.Duplicates<AG_IAS_ATTACH_FILE>(attachFiles, true).ToList();

                Assert.AreEqual(attachFileUndup.Count(), 5);
            }


            //DTO.UserProfile userProfile = new DTO.UserProfile() {
        }

        [TestMethod]
        public void Duplicate_Data_Test_Not_Duplicant()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            IEnumerable<AG_IAS_ATTACH_FILE> attachFiles = ctx.AG_IAS_ATTACH_FILE.Where(a => a.REGISTRATION_ID == "130924103253839");

            if (attachFiles != null)
            {
                Assert.AreEqual(attachFiles.Count(), 3);
                IEnumerable<AG_IAS_ATTACH_FILE> attachFileUndup = DistinctDuplicatesHelper.Duplicates<AG_IAS_ATTACH_FILE>(attachFiles, false).ToList();

                Assert.AreEqual(attachFileUndup.Count(), 3);
            }


            //DTO.UserProfile userProfile = new DTO.UserProfile() {
        }

        [TestMethod]
        public void Duplicate_Data_Test_Null_Data()   
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            IEnumerable<AG_IAS_ATTACH_FILE> attachFiles = ctx.AG_IAS_ATTACH_FILE.Where(a => a.REGISTRATION_ID == "130924103253834");

            if (attachFiles != null)
            {
                Assert.AreEqual(attachFiles.Count(), 0);
                IEnumerable<AG_IAS_ATTACH_FILE> attachFileUndup = DistinctDuplicatesHelper.Duplicates<AG_IAS_ATTACH_FILE>(attachFiles, false).ToList();

                Assert.AreEqual(attachFileUndup.Count(), 0);
            }


            //DTO.UserProfile userProfile = new DTO.UserProfile() {
        }

    }
}
