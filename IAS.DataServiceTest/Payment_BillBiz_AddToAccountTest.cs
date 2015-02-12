using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment;
using Oracle.DataAccess.Client;
using System.Configuration;
using IAS.DAL;
using IAS.DataServices.Payment.TransactionBanking;
using System.Transactions;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class Payment_BillBiz_AddToAccountTest
    {
        [TestMethod]
        public void AddToAccountTest_Can_Default_Test()
        {
            try
            {
                IAS.DAL.Interfaces.IIASPersonEntities _ctx = DAL.DALFactory.GetPersonContext(); ;
                IAS.DAL.Interfaces.IIASFinanceEntities _ctxFin = DAL.DALFactory.GetFinanceContext();

 

                using (var ts = new TransactionScope())
                {
                    OracleConnection Connection = new OracleConnection(ConfigurationManager
                                        .ConnectionStrings[ConnectionFor.OraDB_Finance.ToString()]
                                        .ToString());
                    Connection.Open();

                    BillBiz biz = new BillBiz();
                    AG_IAS_PAYMENT_G_T paymentG = _ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault();
                    AG_IAS_SUBPAYMENT_D_T subd = _ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.HEAD_REQUEST_NO == "130924165707249" && a.PAYMENT_NO == "0001");

                    //var res = biz.AddToAccount(ref _ctx, ref _ctxFin, ref Connection, subd,paymentG, "52-2-034", BankType.KTB, (Decimal)subd.AMOUNT); 

               
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
