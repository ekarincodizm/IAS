using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.Messages;
using System.Data;

namespace IAS.DataServices.Payment.Mapper
{
    public static class CreateReceiptMapper
    {
        public static CreateReceiptReqeust ToCreateReceiptRequest(this DataRow resource) 
        { 
            CreateReceiptReqeust receipt = new CreateReceiptReqeust(){
                HEAD_REQUEST_NO = resource["HEAD_REQUEST_NO"].ToString(),
                ID_CARD_NO = resource["ID_CARD_NO"].ToString(),
                PETITION_TYPE_NAME = resource["PETITION_TYPE_NAME"].ToString(),
                PAYMENT_DATE = Convert.ToDateTime(resource["PAYMENT_DATE"]),
                FIRSTNAME = resource["FIRSTNAME"].ToString(),
                LASTNAME = resource["LASTNAME"].ToString(),
                GROUP_REQUEST_NO = resource["GROUP_REQUEST_NO"].ToString(),
                //GROUP_DATE = Convert.ToDateTime(resource["GROUP_DATE"]),
                RECEIPT_NO = resource["RECEIPT_NO"].ToString(),
                PAYMENT_NO = resource["PAYMENT_NO"].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(resource["RECEIPT_DATE"]),
                AMOUNT = Convert.ToDecimal(resource["AMOUNT"]),
               // CREATED_DATE = Convert.ToDateTime(resource["CREATED_DATE"]),
                CREATE_BY = resource["CREATED_BY"].ToString(),
                //SIGNATURE_IMG = resource["SIGNATURE_IMG"].ToString(),
               // RUN_NO = Convert.ToInt32(resource["RUN_NO"]),
                IMG_SIGN = (byte[])(resource["IMG_SIGN"]),//milk
                LICENSE_TYPE_CODE = resource["LICENSE_TYPE_CODE"].ToString()

            };

            return receipt;
            
        } 
    }
}