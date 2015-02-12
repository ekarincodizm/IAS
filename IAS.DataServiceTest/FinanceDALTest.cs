using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;


namespace IAS.DataServiceTest
{
    [TestClass]
    public class FinanceDALTest
    {
        protected IAS.DAL.Interfaces.IIASFinanceEntities ctx;

        [TestInitialize]
        public void Setup() {
           
            
            this.ctx = DAL.DALFactory.GetFinanceContext();
        }

        [TestMethod]
        public void TestSelectDatabase() 
        {

         
           APPR_DO_HEADER item = ctx.APPR_DO_HEADER.FirstOrDefault();
           APPR_DO_DETAIL appr_do_details = ctx.APPR_DO_DETAIL.FirstOrDefault();
           APPR_DO_ADDRESS appr_do_address = ctx.APPR_DO_ADDRESS.FirstOrDefault();
            APPR_DO_REMARK appr_do_remark = ctx.APPR_DO_REMARK.FirstOrDefault();
            APPR_RECEIVE_H appr_receive_h = ctx.APPR_RECEIVE_H.FirstOrDefault();
            APPR_RECEIVE_DETAIL_D appr_receive_detail_d = ctx.APPR_RECEIVE_DETAIL_D.FirstOrDefault();
            APPR_GL_HEADER appr_gl_header = ctx.APPR_GL_HEADER.FirstOrDefault();
            APPR_GL_DETAIL appr_gl_detail = ctx.APPR_GL_DETAIL.FirstOrDefault();

            APPR_DO_HEADER header = new APPR_DO_HEADER()
            {
                BRANCH_NO = 2,
                SECTION = item.SECTION,
                DOC_TYPE = item.DOC_TYPE,
                DOC_NO = "12122e35451790",                          
                DOC_DATE = item.DOC_DATE,
                DESCRIPTION = item.DESCRIPTION,
                AMOUNT_BEFORE_DISCOUNT = item.AMOUNT_BEFORE_DISCOUNT,
                DISCOUNT_CREDIT_PATTERN = item.DISCOUNT_CREDIT_PATTERN,
                DISCOUNT_CASH_PATTERN = item.DISCOUNT_CASH_PATTERN,
                DISCOUNT_CREDIT = item.DISCOUNT_CREDIT,
                DISCOUNT_CASH = item.DISCOUNT_CASH,
                AMOUNT_BEFORE_VAT = item.AMOUNT_BEFORE_VAT,
                VAT_RATE = item.VAT_RATE,
                VAT_AMOUNT = item.VAT_AMOUNT,
                TOTAL_AMOUNT = item.TOTAL_AMOUNT,
                LEDGER_TYPE = item.LEDGER_TYPE,
                EMP_CODE = item.EMP_CODE,
                CUSTOMER_CODE = item.CUSTOMER_CODE,
                CUSTOMER_CODE_SHIPTO = item.CUSTOMER_CODE_SHIPTO,
                REFER_TYPE = item.REFER_TYPE,
                REFER_NO = item.REFER_NO,
                REFER_DATE = item.REFER_DATE,
                LAST_LINE = item.LAST_LINE,
                REMARK = item.REMARK,
                STATUS = item.STATUS,
                USER_ID = item.USER_ID,
                TIME = item.TIME,
                REF_BUDGET = item.REF_BUDGET,
                REF_PROJECT = item.REF_PROJECT,
                BUD_ACCOUNT_CODE = item.BUD_ACCOUNT_CODE,
                REF_ACTIVITY = item.REF_ACTIVITY,
                STATUS_APPROVE = item.STATUS_APPROVE,
                REMARK_APPROVE = item.REMARK_APPROVE,
                DATE_APPROVED = item.DATE_APPROVED,
                BUD_YEAR = item.BUD_YEAR,
                PAYMENT_TERM = item.PAYMENT_TERM,
                DELIVERY_DATE = item.DELIVERY_DATE,
                DUEDATE = item.DUEDATE,
                TYPE_TAX = item.TYPE_TAX,
                PAY_TYPE = item.PAY_TYPE,
                INVOICE_TYPE = item.INVOICE_TYPE,
                MEMBER_CODE = item.MEMBER_CODE,
                SALE_TYPE = item.SALE_TYPE,
                PAY_CASH = item.PAY_CASH,
                PAY_OTHER = item.PAY_OTHER,
                PAY_DEPOSIT = item.PAY_DEPOSIT,
                TIME_PRINT = item.TIME_PRINT,
                DATE_UPDATE = item.DATE_UPDATE,
                AR_ACCOUNT = item.AR_ACCOUNT,
                SHOW_DOC_DATE = item.SHOW_DOC_DATE,
                TYPE_DISPOSE = item.TYPE_DISPOSE,
                PREMIUM_AMOUNT = item.PREMIUM_AMOUNT,
                PLAN_CODE = item.PLAN_CODE,
                FUND_CODE = item.FUND_CODE, 

            };

            ctx.APPR_DO_HEADER.AddObject(header);
            ctx.SaveChanges();
            ctx.APPR_DO_HEADER.DeleteObject(header);
            ctx.SaveChanges();

      

            //Assert.IsNotNull(header);
            Assert.IsNotNull(appr_do_details);
            Assert.IsNotNull(appr_do_address);
            Assert.IsNotNull(appr_do_remark);
            Assert.IsNotNull(appr_receive_h);
            Assert.IsNotNull(appr_receive_detail_d);
            Assert.IsNotNull(appr_gl_header);
            Assert.IsNotNull(appr_gl_detail);
        }
    }
}
