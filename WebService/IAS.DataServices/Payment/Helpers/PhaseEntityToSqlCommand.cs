using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using System.Text;
using System.Globalization;

namespace IAS.DataServices.Payment.Helpers
{
    public class PhaseEntityToSqlCommand
    {
       
        public static String ConcreateInsertCommand(APPR_DO_HEADER source)
        {

            StringBuilder sqlCommand = new StringBuilder("INSERT INTO APPR_DO_HEADER ");
            sqlCommand.Append(" (BRANCH_NO,SECTION,DOC_TYPE,DOC_NO,DOC_DATE,DESCRIPTION,AMOUNT_BEFORE_DISCOUNT,DISCOUNT_CREDIT_PATTERN,DISCOUNT_CASH_PATTERN,DISCOUNT_CREDIT,DISCOUNT_CASH,AMOUNT_BEFORE_VAT,VAT_RATE,VAT_AMOUNT,TOTAL_AMOUNT,LEDGER_TYPE,EMP_CODE,CUSTOMER_CODE,CUSTOMER_CODE_SHIPTO,REFER_TYPE,REFER_NO,REFER_DATE,LAST_LINE,REMARK,STATUS,USER_ID,TIME,REF_BUDGET,REF_PROJECT,BUD_ACCOUNT_CODE,REF_ACTIVITY,STATUS_APPROVE,REMARK_APPROVE,DATE_APPROVED,BUD_YEAR,PAYMENT_TERM,DELIVERY_DATE,DUEDATE,TYPE_TAX,PAY_TYPE,INVOICE_TYPE,MEMBER_CODE,SALE_TYPE,PAY_CASH,PAY_OTHER,PAY_DEPOSIT,TIME_PRINT,DATE_UPDATE,AR_ACCOUNT,SHOW_DOC_DATE,TYPE_DISPOSE,PREMIUM_AMOUNT,PLAN_CODE,FUND_CODE ) ");

            sqlCommand.Append(" Values (");

            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION) + "' ,");
            sqlCommand.Append(BindValue(source.AMOUNT_BEFORE_DISCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.DISCOUNT_CREDIT_PATTERN) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DISCOUNT_CASH_PATTERN) + "' ,");
            sqlCommand.Append(BindValue(source.DISCOUNT_CREDIT) + " ,");
            sqlCommand.Append(BindValue(source.DISCOUNT_CASH) + " ,");
            sqlCommand.Append(BindValue(source.AMOUNT_BEFORE_VAT) + " ,");
            sqlCommand.Append(BindValue(source.VAT_RATE) + " ,");
            sqlCommand.Append(BindValue(source.VAT_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.TOTAL_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.EMP_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_CODE_SHIPTO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_NO) + "' ,");
            sqlCommand.Append(BindValue(source.REFER_DATE) + " ,");
            sqlCommand.Append(BindValue(source.LAST_LINE) + " ,");
            sqlCommand.Append("'" + BindValue(source.REMARK) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS) + "' ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append("'" + BindValue(source.TIME) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_BUDGET) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_PROJECT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BUD_ACCOUNT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_APPROVE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REMARK_APPROVE) + "' ,");
            sqlCommand.Append(BindValue(source.DATE_APPROVED) + " ,");
            sqlCommand.Append(BindValue(source.BUD_YEAR) + " ," );
            sqlCommand.Append(BindValue(source.PAYMENT_TERM) + " ,");
            sqlCommand.Append(BindValue(source.DELIVERY_DATE) + " ,");
            sqlCommand.Append(BindValue(source.DUEDATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.TYPE_TAX) + "' ,");
            sqlCommand.Append("'" + BindValue(source.PAY_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.INVOICE_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.MEMBER_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SALE_TYPE) + "' ,");
            sqlCommand.Append(BindValue(source.PAY_CASH) + " ,");
            sqlCommand.Append(BindValue(source.PAY_OTHER) + " ,");
            sqlCommand.Append(BindValue(source.PAY_DEPOSIT) + " ,");
            sqlCommand.Append(BindValue(source.TIME_PRINT) + " ,");
            sqlCommand.Append(BindValue(source.DATE_UPDATE) + ",");
            sqlCommand.Append("'" + BindValue(source.AR_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.SHOW_DOC_DATE) + ",");
            sqlCommand.Append("'" + BindValue(source.TYPE_DISPOSE) + "' ,");
            sqlCommand.Append(BindValue(source.PREMIUM_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.PLAN_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.FUND_CODE) + "' )");     

            return sqlCommand.ToString();
        }

        public static String ConcreateInsertCommand(APPR_DO_DETAIL source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO  APPR_DO_DETAIL");
            sqlCommand.Append(" (REF_ACTIVITY,REF_PRODUCT_CODE,REF_ASSET_NO,REF_SERIAL_NO,STATUS,USER_ID,STATUS_APPROVED,REMARK_APPROVED,DATE_APPROVED,QTY_OUT,REF_SO_DOCTYPE,REF_SO_DOCNO,REF_SO_DOCDATE,REF_SO_DOCSEQ,ASSET_START,ASSET_END,DISCOUNT_BILL_AMOUNT,VAT_DISCOUNT_BILL,TYPE_TAX,CHECK_PM,REF_PROJECT,SHOW_DOC_DATE,PREMIUM_AMOUNT,PLAN_CODE,FUND_CODE,BRANCH_NO,SECTION,DOC_TYPE,DOC_NO,DOC_DATE,SEQUENCE,DESCRIPTION_HEADER,DESCRIPTION_DETAIL,PRODUCT_CODE,WAREHOUSE,LOT_NO,UNIT_CODE,QUANTITY_UNIT,PRICE,AMOUNT_BEFORE_DISCOUNT,DISCOUNT_PATTERN,DISCOUNT_AMOUNT,AMOUNT_BEFORE_VAT,VAT_AMOUNT,AMOUNT_AFTER_VAT,LEDGER_TYPE,CURRENCY_CODE,CUSTOMER_CODE) ");

            sqlCommand.Append(" Values (");
            sqlCommand.Append("'" + BindValue(source.REF_ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_PRODUCT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_ASSET_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_SERIAL_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS) + "' ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_APPROVED) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REMARK_APPROVED) + "' ,");
            sqlCommand.Append(BindValue(source.DATE_APPROVED) + " ,");
            sqlCommand.Append(BindValue(source.QTY_OUT) + " ,");
            sqlCommand.Append("'" + BindValue(source.REF_SO_DOCTYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_SO_DOCNO) + "' ,");
            sqlCommand.Append(BindValue(source.REF_SO_DOCDATE) + " ,");
            sqlCommand.Append(BindValue(source.REF_SO_DOCSEQ) + " ,");
            sqlCommand.Append("'" + BindValue(source.ASSET_START) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ASSET_END) + "' ,");
            sqlCommand.Append(BindValue(source.DISCOUNT_BILL_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.VAT_DISCOUNT_BILL) + " ,");
            sqlCommand.Append("'" + BindValue(source.TYPE_TAX) + "' ,");
            sqlCommand.Append(BindValue(source.CHECK_PM) + " ,");
            sqlCommand.Append("'" + BindValue(source.REF_PROJECT) + "' ,");
            sqlCommand.Append(BindValue(source.SHOW_DOC_DATE) + ",");
            sqlCommand.Append(BindValue(source.PREMIUM_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.PLAN_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.FUND_CODE) + "' ,");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append(BindValue(source.SEQUENCE) + " ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION_HEADER) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION_DETAIL) + "' ,");
            sqlCommand.Append("'" + BindValue(source.PRODUCT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.WAREHOUSE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.LOT_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.UNIT_CODE) + "' ,");
            sqlCommand.Append(BindValue(source.QUANTITY_UNIT) + " ,");
            sqlCommand.Append(BindValue(source.PRICE) + " ,");
            sqlCommand.Append(BindValue(source.AMOUNT_BEFORE_DISCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.DISCOUNT_PATTERN) + "' ,");
            sqlCommand.Append(BindValue(source.DISCOUNT_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.AMOUNT_BEFORE_VAT) + " ,");
            sqlCommand.Append(BindValue(source.VAT_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.AMOUNT_AFTER_VAT) + " ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CURRENCY_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_CODE) + "' )"); 

            return sqlCommand.ToString();
        
        }
        public static String ConcreateInsertCommand(APPR_DO_ADDRESS source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO  APPR_DO_ADDRESS");
            sqlCommand.Append(" (BRANCH_NO,DOC_TYPE,DOC_NO,DOC_DATE,LEDGER_TYPE,ADDRESS1,ADDRESS2,ADDRESS3,ADDRESS4,ADDRESS_SHIPTO1,ADDRESS_SHIPTO2,ADDRESS_SHIPTO3,ADDRESS_SHIPTO4,CUSTOMER_CODE,CUSTOMER_NAME,SHOW_DOC_DATE)");

            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS1) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS2) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS3) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS4) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS_SHIPTO1) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS_SHIPTO2) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS_SHIPTO3) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ADDRESS_SHIPTO4) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_NAME) + "' ,");
            sqlCommand.Append(BindValue(source.SHOW_DOC_DATE) + ")"); 

            return sqlCommand.ToString();

        }
        public static String ConcreateInsertCommand(APPR_DO_REMARK source)     
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO  APPR_DO_REMARK");
            sqlCommand.Append(" (BRANCH_NO,SECTION,DOC_TYPE,DOC_NO,DOC_DATE,SEQUENCE,REF_REMARK,SHOW_DOC_DATE) ");

            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append(BindValue(source.SEQUENCE) + " ,");
            sqlCommand.Append("'" + BindValue(source.REF_REMARK) + "' ,");
            sqlCommand.Append(BindValue(source.SHOW_DOC_DATE) + ")"); 

            return sqlCommand.ToString();

        }
        public static String ConcreateInsertCommand(APPR_RECEIVE_H source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO APPR_RECEIVE_H ");
            //sqlCommand.Append("(BRACH_NO,DOC_TYPE,DOC_NO,DOC_DATE,SECTION,EMP_CODE,CUSTOMER_CODE,DESCRIPTION1,DESCRIPTION2,LEDGER_TYPE,BANK_CODE,CHEQUE_NOCHEQUE_DATE,CREDIT_BANK_ACCOUNT,CREDIT_BANK_SECRET_ACCOUNT,DEBIT_AR_ACCOUNT,DEBIT_AR_SECRET_ACCOUNT,CREDIT_WHOLDING_ACCOUNT,CREDIT_WHOLDING_SECRET_ACCOUNT,DEBIT_CHARGE_ACCOUNT,DEBIT_CHARGE_SECRET_ACCOUNT,INVOICE_AMOUNT,INVOICE_RECEIVE_AMOUNT,WITHOLDINGTAX_AMOUNT,CHARGE_AMOUNT,PAYMENT_AMOUNT,REFER_TYPE,REFER_NO,REFER_DATE,REMARK,STATUS,USER_ID,REF_BUDGET,REF_PROJECT,BUD_ACCOUNT_CODE,ACTIVITY,BUD_YEAR,AMOUNT_DEPOSIT,PAY_CASH,PAY_OTHER,RECEIVE_TYPE,INVOICE_TYPE,INVOICE_RECEIVE_BEFORE_VAT,INVOICE_RECEIVE_VAT,SENDING_FLAG,SENDING_TYPE,SENDING_NO,SENDING_DATE,DONATE_FLAG,DONATE_NO,PREMIUM_AMOUNT,PLAN_CODE,FUND_CODE)");
            sqlCommand.Append("(BRACH_NO,DOC_TYPE,DOC_NO,DOC_DATE,SECTION,EMP_CODE,CUSTOMER_CODE,DESCRIPTION1,DESCRIPTION2,LEDGER_TYPE,BANK_CODE,CHEQUE_NO,CHEQUE_DATE,CREDIT_BANK_ACCOUNT,CREDIT_BANK_SECRET_ACCOUNT,DEBIT_AR_ACCOUNT,DEBIT_AR_SECRET_ACCOUNT,CREDIT_WHOLDING_ACCOUNT,CREDIT_WHOLDING_SECRET_ACCOUNT,DEBIT_CHARGE_ACCOUNT,DEBIT_CHARGE_SECRET_ACCOUNT,INVOICE_AMOUNT,INVOICE_RECEIVE_AMOUNT,WITHOLDINGTAX_AMOUNT,CHARGE_AMOUNT,PAYMENT_AMOUNT,REFER_TYPE,REFER_NO,REFER_DATE,REMARK,STATUS,USER_ID,REF_BUDGET,REF_PROJECT,BUD_ACCOUNT_CODE,ACTIVITY,BUD_YEAR,AMOUNT_DEPOSIT,PAY_CASH,PAY_OTHER,RECEIVE_TYPE,INVOICE_TYPE,INVOICE_RECEIVE_BEFORE_VAT,INVOICE_RECEIVE_VAT,SENDING_FLAG,SENDING_TYPE,SENDING_NO,SENDING_DATE,DONATE_FLAG,DONATE_NO,PREMIUM_AMOUNT,PLAN_CODE,FUND_CODE)");
            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRACH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + ",");
            sqlCommand.Append("'" + BindValue(source.SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.EMP_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CUSTOMER_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION1) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION2) + "' ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BANK_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CHEQUE_NO) + "' ,");
            sqlCommand.Append(BindValue(source.CHEQUE_DATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.CREDIT_BANK_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.CREDIT_BANK_SECRET_ACCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.DEBIT_AR_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.DEBIT_AR_SECRET_ACCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.CREDIT_WHOLDING_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.CREDIT_WHOLDING_SECRET_ACCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.DEBIT_CHARGE_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.DEBIT_CHARGE_SECRET_ACCOUNT) + " ,");
            sqlCommand.Append(BindValue(source.INVOICE_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.INVOICE_RECEIVE_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.WITHOLDINGTAX_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.CHARGE_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.PAYMENT_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.REFER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_NO) + "' ,");
            sqlCommand.Append(BindValue(source.REFER_DATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.REMARK) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS) + "' ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_BUDGET) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_PROJECT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BUD_ACCOUNT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ACTIVITY) + "' ,");
            sqlCommand.Append(BindValue(source.BUD_YEAR) + ",");
            sqlCommand.Append(BindValue(source.AMOUNT_DEPOSIT) + " ,");
            sqlCommand.Append(BindValue(source.PAY_CASH) + " ,");
            sqlCommand.Append(BindValue(source.PAY_OTHER) + " ,");
            sqlCommand.Append("'" + BindValue(source.RECEIVE_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.INVOICE_TYPE) + "' ,");
            sqlCommand.Append(BindValue(source.INVOICE_RECEIVE_BEFORE_VAT) + " ,");
            sqlCommand.Append(BindValue(source.INVOICE_RECEIVE_VAT) + " ,");
            sqlCommand.Append(BindValue(source.SENDING_FLAG) + " ,");
            sqlCommand.Append("'" + BindValue(source.SENDING_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SENDING_NO) + "' ,");
            sqlCommand.Append(BindValue(source.SENDING_DATE) + ",");
            sqlCommand.Append(BindValue(source.DONATE_FLAG) + " ,");
            sqlCommand.Append("'" + BindValue(source.DONATE_NO) + "' ,");
            sqlCommand.Append(BindValue(source.PREMIUM_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.PLAN_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.FUND_CODE) + "' )");  

            return sqlCommand.ToString();

        }
        public static String ConcreateInsertCommand(APPR_RECEIVE_DETAIL_D source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO APPR_RECEIVE_DETAIL_D ");
            sqlCommand.Append(" (BRANCH_NO,DOC_TYPE,DOC_NO,DOC_DATE,SEQUENCE,PAYMENT_GROUP,PAYMENT_CODE,REFER_NO,REFER_DATE,AMOUNT,USER_ID,DATE_UPDATE,TIME_UPDATE,DESCRIPTION,STATUS,FLAG_RECEIVE,RECEIVE_DATE,SLIP_NO,SLIP_DATE,ACCOUNT_CODE,RETURN_FLAG,RETURN_TYPE,RETURN_NO,RETURN_DATE,SENDING_FLAG,SENDING_TYPE,SENDING_NO,SENDING_DATE,REF_SEQUENCE_NO,SHOW_DOC_DATE)");

            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append(BindValue(source.SEQUENCE) + " ,");
            sqlCommand.Append("'" + BindValue(source.PAYMENT_GROUP) + "' ,");
            sqlCommand.Append("'" + BindValue(source.PAYMENT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_NO) + "' ,");
            sqlCommand.Append(BindValue(source.REFER_DATE) + ",");
            sqlCommand.Append(BindValue(source.AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append(BindValue(source.DATE_UPDATE) + ",");
            sqlCommand.Append("'" + BindValue(source.TIME_UPDATE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS) + "' ,");
            sqlCommand.Append(BindValue(source.FLAG_RECEIVE) + " ,");
            sqlCommand.Append(BindValue(source.RECEIVE_DATE) + ",");
            sqlCommand.Append("'" + BindValue(source.SLIP_NO) + "' ,");
            sqlCommand.Append(BindValue(source.SLIP_DATE) + ",");
            sqlCommand.Append("'" + BindValue(source.ACCOUNT_CODE) + "' ,");
            sqlCommand.Append(BindValue(source.RETURN_FLAG) + " ,");
            sqlCommand.Append("'" + BindValue(source.RETURN_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.RETURN_NO) + "' ,");
            sqlCommand.Append(BindValue(source.RETURN_DATE) + ",");
            sqlCommand.Append(BindValue(source.SENDING_FLAG) + " ,");
            sqlCommand.Append("'" + BindValue(source.SENDING_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SENDING_NO) + "' ,");
            sqlCommand.Append(BindValue(source.SENDING_DATE) + ",");
            sqlCommand.Append(BindValue(source.REF_SEQUENCE_NO) + " ,");
            sqlCommand.Append(BindValue(source.SHOW_DOC_DATE) + ")"); 

            return sqlCommand.ToString();

        }

        public static String ConcreateInsertCommand(APPR_GL_HEADER source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO APPR_GL_HEADER ");
            sqlCommand.Append(" (BRANCH_NO,DOC_TYPE,DOC_NO,DOC_DATE,DESCRIPTION1,DESCRIPTION2,JOB_NO,LEDGER_TYPE,STATUS_,USER_ID,PERIOD,SECTION,CONTRACT_CODE,TOTAL_AMOUNT,PAYMENT_AMOUNT,STATUS_PAYMENT,EMP_CODE,REFER_TYPE,REFER_NO,REFER_DATE,PAYMENT_TERM,DUEDATE,REMARK,REF_BUDGET,REF_PROJECT,REF_ACTIVITY,REF_BUD_ACCOUNT,BUD_YEAR,APPROVE_STATUS,DATE_APPROVED)");

            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + ",");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION1) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION2) + "' ,");
            sqlCommand.Append("'" + BindValue(source.JOB_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_) + "' ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append(BindValue(source.PERIOD) + " ,");
            sqlCommand.Append("'" + BindValue(source.SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CONTRACT_CODE) + "' ,");
            sqlCommand.Append(BindValue(source.TOTAL_AMOUNT) + " ,");
            sqlCommand.Append(BindValue(source.PAYMENT_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_PAYMENT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.EMP_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REFER_NO) + "' ,");
            sqlCommand.Append(BindValue(source.REFER_DATE) + ",");
            sqlCommand.Append(BindValue(source.PAYMENT_TERM) + " ,");
            sqlCommand.Append(BindValue(source.DUEDATE) + ",");
            sqlCommand.Append("'" + BindValue(source.REMARK) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_BUDGET) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_PROJECT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_BUD_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.BUD_YEAR) + ",");
            sqlCommand.Append("'" + BindValue(source.APPROVE_STATUS) + "' ,");
            sqlCommand.Append(BindValue(source.DATE_APPROVED) + ")"); 

            return sqlCommand.ToString();

        }
        public static String ConcreateInsertCommand(APPR_GL_DETAIL source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO APPR_GL_DETAIL ");
            sqlCommand.Append("(BRANCH_NO,DOC_TYPE,DOC_NO,DOC_DATE,SEQUENCE,DEPARTMENT,ACCOUNT_CODE,SUBSIDAIRY,ACTIVITY,GL_ACCOUNT,SECRET_ACCOUNT,DESCRIPTION1,DESCRIPTION2,DEBIT,CREDIT,LEDGER_TYPE,CURRENCY_TYPE,UNIT_TYPE,UNIT_AMOUNT,REF_AP,REF_AR,REF_FIXASSET,REF_CHECK_NO,REF_TAX_NO,STATUS_,USER_ID,DATE_UPDATE,DATE_POSTED,TIME_POSTED,REF_BUDGET_CODE,REF_PROJECT,BUD_ACCOUNT_CODE,REF_BUD_YEAR,UNIT_START,UNIT_END,PLAN_CODE,FUND_CODE,DEBIT_CREDIT,JOB_NO,CREATE_FROM_CANCEL,FLAG_CURRENCY,R_ACTIVITY,R_PROJECT,R_SECTION)");

            sqlCommand.Append(" Values (");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + ",");
            sqlCommand.Append(BindValue(source.SEQUENCE) + " ,");
            sqlCommand.Append("'" + BindValue(source.DEPARTMENT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ACCOUNT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SUBSIDAIRY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.GL_ACCOUNT) + "' ,");
            sqlCommand.Append(BindValue(source.SECRET_ACCOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION1) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION2) + "' ,");
            sqlCommand.Append(BindValue(source.DEBIT) + " ,");
            sqlCommand.Append(BindValue(source.CREDIT) + " ,");
            sqlCommand.Append("'" + BindValue(source.LEDGER_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.CURRENCY_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.UNIT_TYPE) + "' ,");
            sqlCommand.Append(BindValue(source.UNIT_AMOUNT) + " ,");
            sqlCommand.Append("'" + BindValue(source.REF_AP) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_AR) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_FIXASSET) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_CHECK_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_TAX_NO) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_) + "' ,");
            sqlCommand.Append("'" + BindValue(source.USER_ID) + "' ,");
            sqlCommand.Append(BindValue(source.DATE_UPDATE) + ",");
            sqlCommand.Append(BindValue(source.DATE_POSTED) + " ,");
            sqlCommand.Append("'" + BindValue(source.TIME_POSTED) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_BUDGET_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_PROJECT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BUD_ACCOUNT_CODE) + "' ,");
            sqlCommand.Append(BindValue(source.REF_BUD_YEAR) + ",");
            sqlCommand.Append(BindValue(source.UNIT_START) + " ,");
            sqlCommand.Append(BindValue(source.UNIT_END) + " ,");
            sqlCommand.Append("'" + BindValue(source.PLAN_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.FUND_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DEBIT_CREDIT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.JOB_NO) + "' ,");
            sqlCommand.Append(BindValue(source.CREATE_FROM_CANCEL) + " ,");
            sqlCommand.Append(BindValue(source.FLAG_CURRENCY) + " ,");
            sqlCommand.Append("'" + BindValue(source.R_ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.R_PROJECT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.R_SECTION) + "' )"); 

            return sqlCommand.ToString();

        }

        public static String ConcreateInsertCommand(APPR_BD_MOVEMENT source)
        {
            StringBuilder sqlCommand = new StringBuilder("INSERT INTO  APPR_BD_MOVEMENT (");
            sqlCommand.Append("BUDGET_CODE, SECTION_CODE, PROJECT_CODE, ACTIVITY, BUD_ACCOUNT_CODE, ACCOUNT_CODE, PLAN_CODE, FUND_CODE, GROUP_SECTION, GROUP_ACTIVITY, MUAD_ACCOUNT, BUD_ACCOUNT_TYPE, STATUS, STATUS_APPROVE, STATUS_POST, DESCRIPTION, DESCRIPTION2, REQUEST_AMOUNT_A, REQUEST_AMOUNT_R, RETURN_AMOUNT_A, RETURN_AMOUNT_R, BUDGET_REFERENCE, TRI_21, TRO_22, ADD_25, RED_26, PR_A_31, PR_R_31, ESN_A_32, ESN_R_32, ESP_A_33, ESP_R_33, PO_A_41, PO_R_41, RO_A_42, RO_R_42, AP_A_43, AP_R_43, AR_A_44, AR_R_44, RCAR_A_48, RCAR_R_48, PA_A_51, PA_R_51, PAAP_A_52, PAAP_R_52, CLR_A_55, CLR_R_55, ADJ_A_52, ADJ_R_52, RC_A_80, RC_R_80, BALANCE_DATE, BUD_YEAR, PERIOD, BRANCH_NO, SYSTEM_NO, SYSTEM_CODE, SYSTEM_REF, DOC_TYPE, DOC_NO, DOC_DATE, REF_DOC_TYPE, REF_DOC_NO, REF_DOC_DATE, SEQUENCE_GL, BD_AMOUNT_A, BD_AMOUNT_R, TRAN_AMOUNT_A, TRAN_AMOUNT_R, RESERVE_AMOUNT_A, RESERVE_AMOUNT_R, USED_AMOUNT_A, USED_AMOUNT_R, PAYMENT_AMOUNT_A, PAYMENT_AMOUNT_R, RECEIVE_AMOUNT_A, RECEIVE_AMOUNT_R  ");

            sqlCommand.Append(") Values (");

            sqlCommand.Append("'" + BindValue(source.BUDGET_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SECTION_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.PROJECT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BUD_ACCOUNT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.ACCOUNT_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.PLAN_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.FUND_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.GROUP_SECTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.GROUP_ACTIVITY) + "' ,");
            sqlCommand.Append("'" + BindValue(source.MUAD_ACCOUNT) + "' ,");
            sqlCommand.Append("'" + BindValue(source.BUD_ACCOUNT_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_APPROVE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.STATUS_POST) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DESCRIPTION2) + "' ,");
            sqlCommand.Append(BindValue(source.REQUEST_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.REQUEST_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.RETURN_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.RETURN_AMOUNT_R) + " ,");
            sqlCommand.Append("'" + BindValue(source.BUDGET_REFERENCE) + "' ,");
            sqlCommand.Append(BindValue(source.TRI_21) + " ,");
            sqlCommand.Append(BindValue(source.TRO_22) + " ,");
            sqlCommand.Append(BindValue(source.ADD_25) + " ,");
            sqlCommand.Append(BindValue(source.RED_26) + " ,");
            sqlCommand.Append(BindValue(source.PR_A_31) + " ,");
            sqlCommand.Append(BindValue(source.PR_R_31) + " ,");
            sqlCommand.Append(BindValue(source.ESN_A_32) + " ,");
            sqlCommand.Append(BindValue(source.ESN_R_32) + " ,");
            sqlCommand.Append(BindValue(source.ESP_A_33) + " ,");
            sqlCommand.Append(BindValue(source.ESP_R_33) + " ,");
            sqlCommand.Append(BindValue(source.PO_A_41) + " ,");
            sqlCommand.Append(BindValue(source.PO_R_41) + " ,");
            sqlCommand.Append(BindValue(source.RO_A_42) + " ,");
            sqlCommand.Append(BindValue(source.RO_R_42) + " ,");
            sqlCommand.Append(BindValue(source.AP_A_43) + " ,");
            sqlCommand.Append(BindValue(source.AP_R_43) + " ,");
            sqlCommand.Append(BindValue(source.AR_A_44) + " ,");
            sqlCommand.Append(BindValue(source.AR_R_44) + " ,");
            sqlCommand.Append(BindValue(source.RCAR_A_48) + " ,");
            sqlCommand.Append(BindValue(source.RCAR_R_48) + " ,");
            sqlCommand.Append(BindValue(source.PA_A_51) + " ,");
            sqlCommand.Append(BindValue(source.PA_R_51) + " ,");
            sqlCommand.Append(BindValue(source.PAAP_A_52) + " ,");
            sqlCommand.Append(BindValue(source.PAAP_R_52) + " ,");
            sqlCommand.Append(BindValue(source.CLR_A_55) + " ,");
            sqlCommand.Append(BindValue(source.CLR_R_55) + " ,");
            sqlCommand.Append(BindValue(source.ADJ_A_52) + " ,");
            sqlCommand.Append(BindValue(source.ADJ_R_52) + " ,");
            sqlCommand.Append(BindValue(source.RC_A_80) + " ,");
            sqlCommand.Append(BindValue(source.RC_R_80) + " ,");
            sqlCommand.Append(BindValue(source.BALANCE_DATE) + " ,");
            sqlCommand.Append(BindValue(source.BUD_YEAR) + " ,");
            sqlCommand.Append(BindValue(source.PERIOD) + " ,");
            sqlCommand.Append(BindValue(source.BRANCH_NO) + " ,");
            sqlCommand.Append(BindValue(source.SYSTEM_NO) + " ,");
            sqlCommand.Append("'" + BindValue(source.SYSTEM_CODE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.SYSTEM_REF) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.DOC_DATE) + " ,");
            sqlCommand.Append("'" + BindValue(source.REF_DOC_TYPE) + "' ,");
            sqlCommand.Append("'" + BindValue(source.REF_DOC_NO) + "' ,");
            sqlCommand.Append(BindValue(source.REF_DOC_DATE) + " ,");
            sqlCommand.Append(BindValue(source.SEQUENCE_GL) + " ,");
            sqlCommand.Append(BindValue(source.BD_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.BD_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.TRAN_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.TRAN_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.RESERVE_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.RESERVE_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.USED_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.USED_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.PAYMENT_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.PAYMENT_AMOUNT_R) + " ,");
            sqlCommand.Append(BindValue(source.RECEIVE_AMOUNT_A) + " ,");
            sqlCommand.Append(BindValue(source.RECEIVE_AMOUNT_R) + " )");
            return sqlCommand.ToString();

        }


        //public static String ConcreateInsertCommand(APPR_DO_DETAIL source)
        //{
        //    StringBuilder sqlCommand = new StringBuilder("INSERT INTO  ");
        //    sqlCommand.Append("");

        //    sqlCommand.Append(" Values (");


        //    return sqlCommand.ToString();

        //}
        #region SetDefault Value

        private static String BindValue(DateTime source) {

            return String.Format("to_date('{0}','dd/mm/yyyy')",source.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
        }
        private static String BindValue(DateTime? source)
        {
            return String.Format("to_date('{0}','dd/mm/yyyy')", (source == null) ? 
                                                    DateTime.MinValue.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")) : 
                                                    ((DateTime)source).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")));
        }
        private static String BindValue(String source)
        {
            return (String.IsNullOrEmpty(source))?" ": source;
        }
        private static String BindValue(Int16 source)
        {
            return source.ToString();
        }
        private static String BindValue(Int16? source)
        {
            return (source==null)?"0":((Int16)source).ToString();
        }
        private static String BindValue(Int32? source)
        {
            return (source==null)?"0": ((Int32) source).ToString();
        }
        private static String BindValue(Int32 source)
        {
            return source.ToString();
        }
        private static String BindValue(Int64 source)
        {
            return source.ToString();
        }
        private static String BindValue(Int64? source)
        {
            return (source == null) ? "0" : ((Int32)source).ToString();
        }
        private static String BindValue(Decimal source)
        {
            return source.ToString();
        }
        private static String BindValue(Decimal? source)
        {
            return (source == null) ? "0" : ((Int32)source).ToString();
        }
        #endregion

    }
}