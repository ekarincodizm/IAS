using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.Messages;
using System.Data;
using IAS.DAL;

namespace IAS.DataServices.Payment.Helpers
{
    public class UpdateAfterUploadPaymentRequestFactory
    {
        public static LicensePetitionType18Request ConcreateLicensePetitionType18Request(DataRow row)
        {
            LicensePetitionType18Request licenseRequest18 = new LicensePetitionType18Request()
            {
                UPLOAD_GROUP_NO = row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = row["license_type_code"].ToString(),
                ID_CARD_NO = row["id_card_no"].ToString(),
                COMP_CODE = row["COMP_CODE"].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = row["receipt_no"].ToString(),
                PAYMENT_NO = row["payment_no"].ToString(),
            };
            return licenseRequest18;
        }
        public static LicensePetitionType18Request ConcreateLicensePetitionType18Request(AG_IAS_SUBPAYMENT_D_T subDetail)
        {

            LicensePetitionType18Request licenseRequest = new LicensePetitionType18Request()
            {
                UPLOAD_GROUP_NO = subDetail.UPLOAD_GROUP_NO,// row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = subDetail.SEQ_NO, // row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = subDetail.LICENSE_TYPE_CODE, // row["license_type_code"].ToString(),
                ID_CARD_NO = subDetail.ID_CARD_NO, // row["id_card_no"].ToString(),
                COMP_CODE = subDetail.COMPANY_CODE, // row["COMP_CODE"].ToString(),
                RECEIPT_DATE = (DateTime)subDetail.RECEIPT_DATE, // Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = subDetail.RECEIPT_NO, // row["receipt_no"].ToString(),
                PAYMENT_NO = subDetail.PAYMENT_NO, // row["payment_no"].ToString(),
            };
            return licenseRequest;
        }
        public static LicensePetitionType17Request ConcreateLicensePetitionType17Request(DataRow row)
        {
            LicensePetitionType17Request licenseRequest17 = new LicensePetitionType17Request()
            {
                UPLOAD_GROUP_NO = row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = row["license_type_code"].ToString(),
                ID_CARD_NO = row["id_card_no"].ToString(),
                COMP_CODE = row["COMP_CODE"].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = row["receipt_no"].ToString(),
                PAYMENT_NO = row["payment_no"].ToString(),
            };
            return licenseRequest17;
        }
        public static LicensePetitionType17Request ConcreateLicensePetitionType17Request(AG_IAS_SUBPAYMENT_D_T subDetail)
        {
            LicensePetitionType17Request licenseRequest = new LicensePetitionType17Request()
            {
                UPLOAD_GROUP_NO = subDetail.UPLOAD_GROUP_NO,// row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = subDetail.SEQ_NO, // row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = subDetail.LICENSE_TYPE_CODE, // row["license_type_code"].ToString(),
                ID_CARD_NO = subDetail.ID_CARD_NO, // row["id_card_no"].ToString(),
                COMP_CODE = subDetail.COMPANY_CODE, // row["COMP_CODE"].ToString(),
                RECEIPT_DATE = (DateTime)subDetail.RECEIPT_DATE, // Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = subDetail.RECEIPT_NO, // row["receipt_no"].ToString(),
                PAYMENT_NO = subDetail.PAYMENT_NO, // row["payment_no"].ToString(),
            };
            return licenseRequest;
        }
        public static LicensePetitionType15Request ConcreateLicensePetitionType15Request(DataRow row)
        {
            LicensePetitionType15Request licenseRequest15 = new LicensePetitionType15Request()
            {
                UPLOAD_GROUP_NO = row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = row["license_type_code"].ToString(),
                ID_CARD_NO = row["id_card_no"].ToString(),
                COMP_CODE = row["COMP_CODE"].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = row["receipt_no"].ToString(),
                PAYMENT_NO = row["payment_no"].ToString(),
            };
            return licenseRequest15;
        }
        public static LicensePetitionType15Request ConcreateLicensePetitionType15Request(AG_IAS_SUBPAYMENT_D_T subDetail)
        {
            LicensePetitionType15Request licenseRequest = new LicensePetitionType15Request()
            {
                UPLOAD_GROUP_NO = subDetail.UPLOAD_GROUP_NO,// row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = subDetail.SEQ_NO, // row["SEQ_NO"].ToString(),
                LICENSE_TYPE_CODE = subDetail.LICENSE_TYPE_CODE, // row["license_type_code"].ToString(),
                ID_CARD_NO = subDetail.ID_CARD_NO, // row["id_card_no"].ToString(),
                COMP_CODE = subDetail.COMPANY_CODE, // row["COMP_CODE"].ToString(),
                RECEIPT_DATE = (DateTime)subDetail.RECEIPT_DATE, // Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = subDetail.RECEIPT_NO, // row["receipt_no"].ToString(),
                PAYMENT_NO = subDetail.PAYMENT_NO, // row["payment_no"].ToString(),
            };
            return licenseRequest;
        }

        public static LicensePetitionType1314Request ConcreateLicensePetitionType1314Request(DataRow row)
        {
            LicensePetitionType1314Request licenseRequest1314 = new LicensePetitionType1314Request()
            {
                UPLOAD_GROUP_NO = row["UPLOAD_GROUP_NO"].ToString(),
                SEQ_NO = row["SEQ_NO"].ToString(),
                PETITION_TYPE_CODE = row["petition_type_code"].ToString(),

                RECEIPT_DATE = Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = row["receipt_no"].ToString(),
                REQUEST_NO = row["request_no"].ToString(),
                PAYMENT_NO = row["payment_no"].ToString(),
                LICENSE_NO = row["license_no"].ToString(),
                AREA = row["AREA"].ToString(),
            };
            return licenseRequest1314;
        }
        public static LicensePetitionType1314Request ConcreateLicensePetitionType1314Request(AG_IAS_SUBPAYMENT_D_T subDetail,AG_IAS_LICENSE_D licenseD )
        {
            LicensePetitionType1314Request licenseRequest1314 = new LicensePetitionType1314Request()
            {

                SEQ_NO = subDetail.SEQ_NO, // row["SEQ_NO"].ToString(),
                UPLOAD_GROUP_NO = subDetail.UPLOAD_GROUP_NO,// row["UPLOAD_GROUP_NO"].ToString(),
                PETITION_TYPE_CODE= subDetail.PETITION_TYPE_CODE, 
                RECEIPT_DATE = (DateTime)subDetail.RECEIPT_DATE, // Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = subDetail.RECEIPT_NO, // row["receipt_no"].ToString(),
                REQUEST_NO = licenseD.REQUEST_NO,
                PAYMENT_NO = subDetail.PAYMENT_NO, // row["payment_no"].ToString(),
                LICENSE_NO = licenseD.LICENSE_NO,
                AREA = (licenseD.AREA_CODE.Length >= 2)? licenseD.AREA_CODE.Substring(0,2) : ""


            };
            return licenseRequest1314;
        }
        public static LicensePetitionType11Request ConcreateLicensePetitionType11Request(DataRow row)
        {
            LicensePetitionType11Request licenseRequest11 = new LicensePetitionType11Request()
            {
                ID_CARD_NO = row["id_card_no"].ToString(),
                LICENSE_TYPE_CODE = row["license_type_code"].ToString(),
                COMP_CODE = row["COMP_CODE"].ToString(),
                RECEIPT_DATE = Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = row["receipt_no"].ToString(),
                PAYMENT_NO = row["payment_no"].ToString(),
                SEQ_NO = row["SEQ_NO"].ToString(),
                UPLOAD_GROUP_NO = row["UPLOAD_GROUP_NO"].ToString(),
                TESTING_NO = row["testing_no"].ToString()
            };
            return licenseRequest11;
        }
        public static LicensePetitionType11Request ConcreateLicensePetitionType11Request(AG_IAS_SUBPAYMENT_D_T subDetail)
        {
            LicensePetitionType11Request licenseRequest11 = new LicensePetitionType11Request()
            {
                ID_CARD_NO = subDetail.ID_CARD_NO, // row["id_card_no"].ToString(),
                LICENSE_TYPE_CODE = subDetail.LICENSE_TYPE_CODE, // row["license_type_code"].ToString(),
                COMP_CODE = subDetail.COMPANY_CODE, // row["COMP_CODE"].ToString(),
                RECEIPT_DATE = (DateTime)subDetail.RECEIPT_DATE, // Convert.ToDateTime(row["receipt_date"]),
                RECEIPT_NO = subDetail.RECEIPT_NO, // row["receipt_no"].ToString(),
                PAYMENT_NO = subDetail.PAYMENT_NO, // row["payment_no"].ToString(),
                SEQ_NO = subDetail.SEQ_NO, // row["SEQ_NO"].ToString(),
                UPLOAD_GROUP_NO = subDetail.UPLOAD_GROUP_NO,// row["UPLOAD_GROUP_NO"].ToString(),
                TESTING_NO = subDetail.TESTING_NO, // row["testing_no"].ToString()
            };
            return licenseRequest11;
        }
    }
}