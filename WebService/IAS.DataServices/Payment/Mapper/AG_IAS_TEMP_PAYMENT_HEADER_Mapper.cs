using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_TEMP_PAYMENT_HEADER_Mapper
    {
        public static AG_IAS_PAYMENT_HEADER ConvertToAG_IAS_PAYMENT_HEADER(this AG_IAS_TEMP_PAYMENT_HEADER item, BankType bankType) {
            AG_IAS_PAYMENT_HEADER entity = new AG_IAS_PAYMENT_HEADER();
            entity.EFFECTIVE_DATE = item.EFFECTIVE_DATE;
            entity.SERVICE_CODE = item.SERVICE_CODE;
            entity.ID = item.ID;
            entity.RECORD_TYPE = item.RECORD_TYPE;
            entity.SEQUENCE_NO = item.SEQUENCE_NO;
            entity.BANK_CODE = item.BANK_CODE;
            entity.COMPANY_ACCOUNT = item.COMPANY_ACCOUNT;
            entity.COMPANY_NAME = item.COMPANY_NAME;
            entity.BANK = bankType.ToString("g");

            return entity;
        }
    }
}                                                         