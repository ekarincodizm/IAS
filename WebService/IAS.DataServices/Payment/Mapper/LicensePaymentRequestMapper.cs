using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DTO;
using System.Data;

namespace IAS.DataServices.Payment.Mapper
{
    public static class LicensePaymentRequestMapper
    {
        public static LicensePaymentRequest ConvertToLicensePaymentRequest(this DataRow resource) 
        {
            LicensePaymentRequest licensePayment = new LicensePaymentRequest() {
                PETITION_TYPE_CODE = resource["PETITION_TYPE_CODE"].ToString(),
                ID_CARD_NO = resource["ID_CARD_NO"].ToString(),
                LICENSE_TYPE_CODE = resource[""].ToString(),
                LICENSE_NO = resource[""].ToString(),
                COMP_CODE = resource[""].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(resource[""]),
                RECEIPT_NO = resource[""].ToString(),
                EXPIRATION_DATE = Convert.ToDateTime(resource[""]),
                PAYMENT_NO = resource[""].ToString(),
                AREA = resource[""].ToString(),
                REQUEST_NO = resource[""].ToString(),
                UPLOAD_GROUP_NO = resource[""].ToString(),
                SEQ_NO = resource[""].ToString()
            };


            return licensePayment;
        }  
    }
}