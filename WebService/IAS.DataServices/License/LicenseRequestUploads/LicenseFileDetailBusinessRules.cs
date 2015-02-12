using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;


namespace IAS.DataServices.License.LicenseRequestUploads
{
    public class LicenseFileDetailBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";
        static String notrequiredFormat = "ขอรับใบอนุญาตใหม่ไม่ต้องระบุ {0}.";
        
        public static readonly BusinessRule IMPORT_ID_Required = new BusinessRule("IMPORT_ID", String.Format(requiredFormat, LicenseFileDetailFieldNames.IMPORT_ID));
        public static readonly BusinessRule PETITION_TYPE_Required = new BusinessRule("PETITION_TYPE", String.Format(requiredFormat, LicenseFileDetailFieldNames.PETITION_TYPE));
        public static readonly BusinessRule COMP_CODE_Required = new BusinessRule("COMP_CODE", String.Format(requiredFormat, LicenseFileDetailFieldNames.COMP_CODE));
        public static readonly BusinessRule COMP_CODE_Required_02 = new BusinessRule("COMP_CODE", "ขอใบอนุญาตใบที่ 2  บริษัทเดิมยื่นขอไม่ได้.");





        public static readonly BusinessRule SEQ_Required = new BusinessRule("SEQ", String.Format(requiredFormat, LicenseFileDetailFieldNames.SEQ));
        public static readonly BusinessRule SEQ_NotOrder = new BusinessRule("SEQ", Resources.errorLicenseFileDetailBusinessRules_SEQ_NotOrder);  

        public static readonly BusinessRule LICENSE_NO_Required = new BusinessRule("LICENSE_NO", String.Format(requiredFormat, LicenseFileDetailFieldNames.LICENSE_NO));
        public static readonly BusinessRule LICENSE_NO_NotMath_Required = new BusinessRule("LICENSE_NO", Resources.errorLicenseFileDetailBusinessRules_LICENSE_NO_NotMatch_Required);
        public static readonly BusinessRule LICENSE_NO_NotMath_ID_CARD = new BusinessRule("LICENSE_NO", "เลขที่ใบอนุญาตไม่ตรงกับเลขบัตรประชาชน.");
        public static readonly BusinessRule LICENSE_NO_NotMath_Renew_13 = new BusinessRule("LICENSE_NO", "ต่ออายุครั้งที่ 3 ขึ้นไปไม่สามารถต่ออายุ 1 ปีได้.");
        public static readonly BusinessRule LICENSE_NO_NotMath_Renew_14 = new BusinessRule("LICENSE_NO", "ต่ออายุ 5 ปี จำนวนครั้งตั้งแต่ครั้งที่ 3 ขึ้นไป.");


        public static readonly BusinessRule LICENSE_ACTIVE_DATE_Required = new BusinessRule("LICENSE_ACTIVE_DATE", String.Format(requiredFormat, LicenseFileDetailFieldNames.LICENSE_ACTIVE_DATE));
        public static readonly BusinessRule LICENSE_EXPIRE_DATE_Required = new BusinessRule("LICENSE_EXPIRE_DATE", String.Format(requiredFormat, LicenseFileDetailFieldNames.LICENSE_EXPIRE_DATE));
        public static readonly BusinessRule LICENSE_EXPIRE_DATE_Expire = new BusinessRule("LICENSE_EXPIRE_DATE", "ใบอนุญาตหมดอายุแล้วไม่สามารถต่อใบอนุญาตได้ ให้ไปทำขาดต่อขอใหม่.");
        public static readonly BusinessRule LICENSE_EXPIRE_DATE_SixtyDay = new BusinessRule("LICENSE_EXPIRE_DATE", "ต่อใบอนุญาตภายใน 60 ก่อนวันหมดอายุ.");


        public static readonly BusinessRule LICENSE_NO_NotRequired = new BusinessRule("LICENSE_NO", String.Format(notrequiredFormat, LicenseFileDetailFieldNames.LICENSE_NO));
        public static readonly BusinessRule LICENSE_ACTIVE_DATE_NotRequired = new BusinessRule("LICENSE_ACTIVE_DATE", String.Format(notrequiredFormat, LicenseFileDetailFieldNames.LICENSE_ACTIVE_DATE));
        public static readonly BusinessRule LICENSE_EXPIRE_DATE_NotRequired = new BusinessRule("LICENSE_EXPIRE_DATE", String.Format(notrequiredFormat, LicenseFileDetailFieldNames.LICENSE_EXPIRE_DATE));
        public static readonly BusinessRule LICENSE_FEE_Required = new BusinessRule("LICENSE_FEE", String.Format(requiredFormat, LicenseFileDetailFieldNames.LICENSE_FEE));
        public static readonly BusinessRule CITIZEN_ID_Required = new BusinessRule("CITIZEN_ID", String.Format(requiredFormat, LicenseFileDetailFieldNames.CITIZEN_ID));
        public static readonly BusinessRule TITLE_NAME_Required = new BusinessRule("TITLE_NAME", String.Format(requiredFormat, LicenseFileDetailFieldNames.TITLE_NAME));
        public static readonly BusinessRule NAME_Required = new BusinessRule("NAME", String.Format(requiredFormat, LicenseFileDetailFieldNames.NAME));
        public static readonly BusinessRule SURNAME_Required = new BusinessRule("SURNAME", String.Format(requiredFormat, LicenseFileDetailFieldNames.SURNAME));
        public static readonly BusinessRule ADDR1_Required = new BusinessRule("ADDR1", String.Format(requiredFormat, LicenseFileDetailFieldNames.ADDR1));
        public static readonly BusinessRule ADDR2_Required = new BusinessRule("ADDR2", String.Format(requiredFormat, LicenseFileDetailFieldNames.ADDR2));
        public static readonly BusinessRule AREA_CODE_Required = new BusinessRule("AREA_CODE", String.Format(requiredFormat, LicenseFileDetailFieldNames.AREA_CODE));
        public static readonly BusinessRule EMAIL_Required = new BusinessRule("EMAIL", String.Format(requiredFormat, LicenseFileDetailFieldNames.EMAIL));
        public static readonly BusinessRule CUR_ADDR_Required = new BusinessRule("CUR_ADDR", String.Format(requiredFormat, LicenseFileDetailFieldNames.CUR_ADDR));
        public static readonly BusinessRule TEL_NO_Required = new BusinessRule("TEL_NO", String.Format(requiredFormat, LicenseFileDetailFieldNames.TEL_NO));
        public static readonly BusinessRule CUR_AREA_CODE_Required = new BusinessRule("CUR_AREA_CODE", String.Format(requiredFormat, LicenseFileDetailFieldNames.CUR_AREA_CODE));
        public static readonly BusinessRule REMARK_Required = new BusinessRule("REMARK", String.Format(requiredFormat, LicenseFileDetailFieldNames.REMARK));
        public static readonly BusinessRule AR_ANSWER_Required = new BusinessRule("AR_ANSWER", String.Format(requiredFormat, LicenseFileDetailFieldNames.AR_ANSWER));
        public static readonly BusinessRule AR_DATE_Required = new BusinessRule("AR_DATE", "ข้อมูล AR_DATE ไม่ถูกต้อง.");

        public static readonly BusinessRule OLD_COMP_CODE_Required = new BusinessRule("OLD_COMP_CODE", String.Format(requiredFormat, LicenseFileDetailFieldNames.OLD_COMP_CODE));
        public static readonly BusinessRule ERR_MSG_Required = new BusinessRule("ERR_MSG", String.Format(requiredFormat, LicenseFileDetailFieldNames.ERR_MSG));
        public static readonly BusinessRule LOAD_STATUS_Required = new BusinessRule("LOAD_STATUS", String.Format(requiredFormat, LicenseFileDetailFieldNames.LOAD_STATUS));

        public static readonly BusinessRule CITIZEN_ID_DuplicateInFile = new BusinessRule("CITIZEN_ID", Resources.errorLicenseFileDetailBusinessRules_CITIZEN_ID_DuplicateInFile);
        public static readonly BusinessRule CITIZEN_ID_Duplicate = new BusinessRule("CITIZEN_ID", Resources.errorLicenseFileDetailBusinessRules_CITIZEN_ID_Duplicate );
        public static readonly BusinessRule CITIZEN_ID_Waiting = new BusinessRule("CITIZEN_ID", Resources.errorLicenseFileDetailBusinessRules_CITIZEN_ID_Waiting);

        public static readonly BusinessRule STARDATE_Required = new BusinessRule("STARDATE", "ข้อมูลวันที่มีผลบังคับใช้ ไม่ถูกต้อง.");
        public static readonly BusinessRule SPECIAL_TYPE_CODE_EXAM_NotFormate = new BusinessRule("SPECIAL_TYPE_CODE_EXAM", "ข้อมูลรหัสเอกสารลดผลสอบ ไม่ถูกต้อง.");
        public static readonly BusinessRule SPECIAL_TYPE_CODE_TRAIN_NotFormate = new BusinessRule("SPECIAL_TYPE_CODE_TRAIN", "ข้อมูลรหัสเอกสารลดผลอบรม ไม่ถูกต้อง.");
        public static readonly BusinessRule SPECIAL_TYPE_CODE_NotFound = new BusinessRule("SPECIAL_TYPE_CODE_TRAIN", "ไม่พบเอกสารพิเศษที่ระบุ.");


        public static readonly BusinessRule SPECIAL_TYPE_CODE_Required = new BusinessRule("SPECIAL_TYPE_CODE_TRAIN", "ข้อมูลรหัสเอกสารพิเศษ ไม่ถูกต้อง.");

        public static readonly BusinessRule PICTURE_Formate = new BusinessRule("PICTURE_Formate", "รูปถ่ายต้องเป็นไฟล์นามสกุล JPG,BMP,GIF,PNG,TIF เท่านั้น.");
        public static readonly BusinessRule DOC_Formate = new BusinessRule("SPECIAL_DOC_Formate", "เอกสารแนบต้องเป็นไฟล์นามสกุล  BMP,GIF,JPG,PNG,TIF,PDF,WORD เท่านั้น.");



    }
}