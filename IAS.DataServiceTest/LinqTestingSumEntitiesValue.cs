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
    public class LinqTestingSumEntitiesValue
    {
        [TestMethod]
        public void TestSubLinq()
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            var result = ctx.AG_IAS_SUBPAYMENT_D_T.Where(a => a.HEAD_REQUEST_NO == "131112111255517").Sum(a => a.AMOUNT);

            Assert.AreEqual(1000m, result);
        }

        [TestMethod]
        public void TestPersisAfterAddObject()
        {
            String id = "131029190252201";

            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_TEMP_PAYMENT_DETAIL result = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);

            AG_IAS_PAYMENT_DETAIL data = new AG_IAS_PAYMENT_DETAIL();
            result.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL, AG_IAS_PAYMENT_DETAIL>(data);

            ctx.AG_IAS_PAYMENT_DETAIL.AddObject(data);

            AG_IAS_PAYMENT_DETAIL t = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);
            Assert.IsNotNull(t);
        }


        [TestMethod]
        public void TestPersisAfterAttach()      
        {
            //String id = "131029190252201";

            //IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            //AG_IAS_TEMP_PAYMENT_DETAIL result = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);

            //AG_IAS_PAYMENT_DETAIL data = new AG_IAS_PAYMENT_DETAIL();
            //result.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL, AG_IAS_PAYMENT_DETAIL>(data);

            //ctx.AG_IAS_PAYMENT_DETAIL.AddObject(data);
            ////ctx.AG_IAS_PAYMENT_DETAIL.Attach(data);
            ////ctx.ObjectStateManager.ChangeObjectState(data, System.Data.EntityState.Added);
            //AG_IAS_PAYMENT_DETAIL obj_a = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);
            //AG_IAS_PAYMENT_DETAIL obj_b = ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(obj=> obj.Entity).OfType<AG_IAS_PAYMENT_DETAIL>().SingleOrDefault(a => a.ID == id);
            //ctx.SaveChanges();
            //ctx = null;

            //ctx = DAL.DALFactory.GetPersonContext();
            //AG_IAS_PAYMENT_DETAIL newobj = ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(obj => obj.Entity).OfType<AG_IAS_PAYMENT_DETAIL>().SingleOrDefault(a => a.ID == id);

            //Assert.IsNull(obj_a);
            //Assert.IsNotNull(obj_b);

            String id = "140401141556931";

            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_TEMP_PAYMENT_DETAIL_HIS item = ctx.AG_IAS_TEMP_PAYMENT_DETAIL_HIS.SingleOrDefault(a => a.HIS_ID == id);

            AG_IAS_PAYMENT_DETAIL_HIS entity = new AG_IAS_PAYMENT_DETAIL_HIS();
            item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL_HIS, AG_IAS_PAYMENT_DETAIL_HIS>(entity);
            //entity.HIS_ID = OracleDB.GetGenAutoId();
            //entity.ID = item.ID;
            //entity.RECORD_TYPE = item.RECORD_TYPE;
            //entity.BANK_CODE = item.BANK_CODE;
            //entity.COMPANY_ACCOUNT = item.COMPANY_ACCOUNT;
            //entity.PAYMENT_DATE = item.PAYMENT_DATE;
            //entity.PAYMENT_TIME = item.PAYMENT_TIME;
            //entity.CUSTOMER_NAME = item.CUSTOMER_NAME;
            //entity.CUSTOMER_NO_REF1 = item.CUSTOMER_NO_REF1;
            //entity.REF2 = item.REF2;
            //entity.REF3 = item.REF3;
            //entity.BRANCH_NO = item.BRANCH_NO;
            //entity.TELLER_NO = item.TELLER_NO;
            //entity.KIND_OF_TRANSACTION = item.KIND_OF_TRANSACTION;
            //entity.TRANSACTION_CODE = item.TRANSACTION_CODE;
            //entity.CHEQUE_NO = item.CHEQUE_NO;
            //entity.AMOUNT = item.AMOUNT;
            //entity.CHEQUE_BANK_CODE = item.CHEQUE_BANK_CODE;
            //entity.HEADER_ID = item.HEADER_ID;
            //ctx.AG_IAS_PAYMENT_DETAIL_HIS.AddObject(entity);
            //ctx.AddToAG_IAS_PAYMENT_DETAIL_HIS(data);
            //ctx.ObjectStateManager.ChangeObjectState(data, System.Data.EntityState.Added);
            //AG_IAS_PAYMENT_DETAIL obj_a = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);
            //AG_IAS_PAYMENT_DETAIL obj_b = ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(obj => obj.Entity).OfType<AG_IAS_PAYMENT_DETAIL>().SingleOrDefault(a => a.ID == id);
            ctx.SaveChanges();
            ctx = null;

            ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_PAYMENT_DETAIL_HIS newobj = ctx.AG_IAS_PAYMENT_DETAIL_HIS.SingleOrDefault(a => a.HIS_ID == id);

            Assert.IsNotNull(newobj);
        }
        [TestMethod]
        public void TestCopyTransaction()
        {                                        
            String id = "131029190252201";

            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_TEMP_PAYMENT_DETAIL result = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);

            AG_IAS_PAYMENT_DETAIL data = new AG_IAS_PAYMENT_DETAIL();
            result.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL, AG_IAS_PAYMENT_DETAIL>(data);

            ctx.AG_IAS_PAYMENT_DETAIL.AddObject(data);
            ctx.AddToAG_IAS_PAYMENT_DETAIL(data);
            ctx.ObjectStateManager.ChangeObjectState(data, System.Data.EntityState.Added);
            //AG_IAS_PAYMENT_DETAIL obj_a = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);
            //AG_IAS_PAYMENT_DETAIL obj_b = ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(obj => obj.Entity).OfType<AG_IAS_PAYMENT_DETAIL>().SingleOrDefault(a => a.ID == id);
            ctx.SaveChanges();
            ctx = null;

            ctx = DAL.DALFactory.GetPersonContext();
            AG_IAS_PAYMENT_DETAIL newobj = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(a => a.ID == id);

            Assert.IsNotNull(newobj);
        }
    }
}
