using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_TEMP_PAYMENT_DETAIL_HIS_Mapper
    {
        public static AG_IAS_PAYMENT_DETAIL_HIS ConvertToAG_IAS_PAYMENT_DETAIL_HIS(this AG_IAS_TEMP_PAYMENT_DETAIL_HIS item) {
            AG_IAS_PAYMENT_DETAIL_HIS entity = new AG_IAS_PAYMENT_DETAIL_HIS();
            entity.HIS_ID = item.HIS_ID;
            entity.ID = item.ID;
            entity.RECORD_TYPE = item.RECORD_TYPE;
            entity.BANK_CODE = item.BANK_CODE;
            entity.COMPANY_ACCOUNT = item.COMPANY_ACCOUNT;
            entity.PAYMENT_DATE = ParseDateFromString.ParseDateHeaderBank(item.PAYMENT_DATE);
            entity.PAYMENT_TIME = item.PAYMENT_TIME;
            entity.CUSTOMER_NAME = item.CUSTOMER_NAME;
            entity.CUSTOMER_NO_REF1 = item.CUSTOMER_NO_REF1;
            entity.REF2 = item.REF2;
            entity.REF3 = item.REF3;
            entity.BRANCH_NO = item.BRANCH_NO;
            entity.TELLER_NO = item.TELLER_NO;
            entity.KIND_OF_TRANSACTION = item.KIND_OF_TRANSACTION;
            entity.TRANSACTION_CODE = item.TRANSACTION_CODE;
            entity.CHEQUE_NO = item.CHEQUE_NO;
            entity.AMOUNT = item.AMOUNT;
            entity.CHEQUE_BANK_CODE = item.CHEQUE_BANK_CODE;
            entity.HEADER_ID = item.HEADER_ID; 
            return entity;
        } 
    }
}