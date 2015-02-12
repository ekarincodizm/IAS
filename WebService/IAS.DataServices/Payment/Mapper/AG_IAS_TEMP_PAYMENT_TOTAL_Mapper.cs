using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_TEMP_PAYMENT_TOTAL_Mapper
    {
        public static AG_IAS_PAYMENT_TOTAL ConvertToAG_IAS_PAYMENT_TOTAL(this AG_IAS_TEMP_PAYMENT_TOTAL item) {
            AG_IAS_PAYMENT_TOTAL entity = new AG_IAS_PAYMENT_TOTAL();
            entity.ID = item.ID;
            entity.RECORD_TYPE = item.RECORD_TYPE;
            entity.SEQUENCE_NO = item.SEQUENCE_NO;
            entity.BANK_CODE = item.BANK_CODE;
            entity.COMPANY_ACCOUNT = item.COMPANY_ACCOUNT;
            entity.TOTAL_DEBIT_AMOUNT = item.TOTAL_DEBIT_AMOUNT;
            entity.TOTAL_DEBIT_TRANSACTION = item.TOTAL_DEBIT_TRANSACTION;
            entity.TOTAL_CREDIT_AMOUNT = item.TOTAL_CREDIT_AMOUNT;
            entity.TOTAL_CREDIT_TRANSACTION = item.TOTAL_CREDIT_TRANSACTION;
            entity.HEADER_ID = item.HEADER_ID; 


            return entity;
        }
    }
}