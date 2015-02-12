using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_TEMP_PAYMENT_DETAIL_Mapper
    {
        public static AG_IAS_PAYMENT_DETAIL ConvertToAG_IAS_PAYMENT_DETAIL(this AG_IAS_TEMP_PAYMENT_DETAIL bankPaymentDetail) 
        {
            AG_IAS_PAYMENT_DETAIL transferDetail = new AG_IAS_PAYMENT_DETAIL();

            transferDetail.ID = bankPaymentDetail.ID;
            transferDetail.RECORD_TYPE = bankPaymentDetail.RECORD_TYPE;
            transferDetail.BANK_CODE = bankPaymentDetail.BANK_CODE;
            transferDetail.COMPANY_ACCOUNT = bankPaymentDetail.COMPANY_ACCOUNT;
       
            transferDetail.PAYMENT_DATE = ParseDateFromString.ParseDateHeaderBank(bankPaymentDetail.PAYMENT_DATE);
            transferDetail.PAYMENT_TIME = bankPaymentDetail.PAYMENT_TIME;
            transferDetail.CUSTOMER_NAME = bankPaymentDetail.CUSTOMER_NAME;
            transferDetail.CUSTOMER_NO_REF1 = bankPaymentDetail.CUSTOMER_NO_REF1;
            transferDetail.REF2 = bankPaymentDetail.REF2;
            transferDetail.REF3 = bankPaymentDetail.REF3;
            transferDetail.BRANCH_NO = bankPaymentDetail.BRANCH_NO;
            transferDetail.TELLER_NO = bankPaymentDetail.TELLER_NO;
            transferDetail.KIND_OF_TRANSACTION = bankPaymentDetail.KIND_OF_TRANSACTION;
            transferDetail.TRANSACTION_CODE = bankPaymentDetail.TRANSACTION_CODE;
            transferDetail.CHEQUE_NO = bankPaymentDetail.CHEQUE_NO;
            transferDetail.AMOUNT = bankPaymentDetail.AMOUNT;
            transferDetail.CHEQUE_BANK_CODE = bankPaymentDetail.CHEQUE_BANK_CODE;
            transferDetail.HEADER_ID = bankPaymentDetail.HEADER_ID;
            transferDetail.STATUS = bankPaymentDetail.STATUS;
            return transferDetail;
        }
    }
}