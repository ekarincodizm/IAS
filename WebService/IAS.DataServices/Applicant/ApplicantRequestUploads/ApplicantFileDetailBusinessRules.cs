using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantFileDetailBusinessRules
    {                                                   
        static String requiredFormat = "- ข้อมูล {0} ไม่ถูกต้อง." + Environment.NewLine;
        static String defaultFormat = "- {0}." + Environment.NewLine;
        public static readonly BusinessRule LOAD_STATUS_Required = 
            new BusinessRule("LOAD_STATUS", String.Format(requiredFormat, ApplicantFileDetailFieldNames.LOAD_STATUS));
        public static readonly BusinessRule APPLICANT_CODE_Required = 
            new BusinessRule("APPLICANT_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.APPLICANT_CODE));
        public static readonly BusinessRule TESTING_NO_Required = 
            new BusinessRule("TESTING_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.TESTING_NO));
        public static readonly BusinessRule EXAM_PLACE_CODE_Required = 
            new BusinessRule("EXAM_PLACE_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.EXAM_PLACE_CODE));
        public static readonly BusinessRule ACCEPT_OFF_CODE_Required = 
            new BusinessRule("ACCEPT_OFF_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ACCEPT_OFF_CODE));
        public static readonly BusinessRule APPLY_DATE_Required = 
            new BusinessRule("APPLY_DATE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.APPLY_DATE));
        public static readonly BusinessRule ID_CARD_NO_Required = 
            new BusinessRule("ID_CARD_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ID_CARD_NO));
        public static readonly BusinessRule ID_CARD_NO_DuplicateInFile =
            new BusinessRule("ID_CARD_NO", String.Format(defaultFormat, Resources.errorApplicantFileDetailBusinessRules_ID_CARD_NO_DuplicateInFile));
        public static readonly BusinessRule ID_CARD_NO_Registed =
            new BusinessRule("ID_CARD_NO", String.Format(defaultFormat, Resources.errorApplicantFileDetailBusinessRules_ID_CARD_NO_Registed));
                                                               
        public static readonly BusinessRule PRE_NAME_CODE_Required = 
            new BusinessRule("PRE_NAME_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.PRE_NAME_CODE));
        public static readonly BusinessRule NAMES_Required = 
            new BusinessRule("NAMES", String.Format(requiredFormat, ApplicantFileDetailFieldNames.NAMES));
        public static readonly BusinessRule NAMES_TooLong =
            new BusinessRule("NAMES", String.Format(defaultFormat, Resources.errorApplicantFileDetailBusinessRules_NAMES_TooLong));

        public static readonly BusinessRule LASTNAME_TooLong =       
            new BusinessRule("LASTNAME", String.Format(defaultFormat, Resources.errorApplicantFileDetailBusinessRules_LASTNAME_TooLong));
        public static readonly BusinessRule LASTNAME_Required =
            new BusinessRule("LASTNAME", String.Format(requiredFormat, ApplicantFileDetailFieldNames.LASTNAME));
        public static readonly BusinessRule BIRTH_DATE_Required = 
            new BusinessRule("BIRTH_DATE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.BIRTH_DATE));
        public static readonly BusinessRule SEX_Required = 
            new BusinessRule("SEX", String.Format(requiredFormat, ApplicantFileDetailFieldNames.SEX));
        public static readonly BusinessRule EDUCATION_CODE_Required = 
            new BusinessRule("EDUCATION_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.EDUCATION_CODE));
        public static readonly BusinessRule ADDRESS1_Required = 
            new BusinessRule("ADDRESS1", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ADDRESS1));
        public static readonly BusinessRule ADDRESS2_Required = 
            new BusinessRule("ADDRESS2", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ADDRESS2));
        public static readonly BusinessRule AREA_CODE_Required = 
            new BusinessRule("AREA_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.AREA_CODE));
        public static readonly BusinessRule PROVINCE_CODE_Required = 
            new BusinessRule("PROVINCE_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.PROVINCE_CODE));
        public static readonly BusinessRule ZIPCODE_Required = 
            new BusinessRule("ZIPCODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ZIPCODE));
        public static readonly BusinessRule TELEPHONE_Required = 
            new BusinessRule("TELEPHONE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.TELEPHONE));
        public static readonly BusinessRule AMOUNT_TRAN_NO_Required = 
            new BusinessRule("AMOUNT_TRAN_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.AMOUNT_TRAN_NO));
        public static readonly BusinessRule PAYMENT_NO_Required = 
            new BusinessRule("PAYMENT_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.PAYMENT_NO));
        public static readonly BusinessRule INSUR_COMP_CODE_Required = 
            new BusinessRule("INSUR_COMP_CODE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.INSUR_COMP_CODE));
        public static readonly BusinessRule ABSENT_EXAM_Required = 
            new BusinessRule("ABSENT_EXAM", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ABSENT_EXAM));
        public static readonly BusinessRule RESULT_Required = 
            new BusinessRule("RESULT", String.Format(requiredFormat, ApplicantFileDetailFieldNames.RESULT));
        public static readonly BusinessRule EXPIRE_DATE_Required = 
            new BusinessRule("EXPIRE_DATE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.EXPIRE_DATE));
        public static readonly BusinessRule LICENSE_Required = 
            new BusinessRule("LICENSE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.LICENSE));
        public static readonly BusinessRule CANCEL_REASON_Required = 
            new BusinessRule("CANCEL_REASON", String.Format(requiredFormat, ApplicantFileDetailFieldNames.CANCEL_REASON));
        public static readonly BusinessRule RECORD_STATUS_Required = 
            new BusinessRule("RECORD_STATUS", String.Format(requiredFormat, ApplicantFileDetailFieldNames.RECORD_STATUS));
        public static readonly BusinessRule USER_ID_Required = 
            new BusinessRule("USER_ID", String.Format(requiredFormat, ApplicantFileDetailFieldNames.USER_ID));
        public static readonly BusinessRule USER_DATE_Required = 
            new BusinessRule("USER_DATE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.USER_DATE));
        public static readonly BusinessRule EXAM_STATUS_Required = 
            new BusinessRule("EXAM_STATUS", String.Format(requiredFormat, ApplicantFileDetailFieldNames.EXAM_STATUS));
        public static readonly BusinessRule REQUEST_NO_Required = 
            new BusinessRule("REQUEST_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.REQUEST_NO));
        public static readonly BusinessRule UPLOAD_GROUP_NO_Required = 
            new BusinessRule("UPLOAD_GROUP_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.UPLOAD_GROUP_NO));
        public static readonly BusinessRule SEQ_NO_Required = 
            new BusinessRule("SEQ_NO", String.Format(requiredFormat, ApplicantFileDetailFieldNames.SEQ_NO));
        public static readonly BusinessRule TITLE_Required = 
            new BusinessRule("TITLE", String.Format(requiredFormat, ApplicantFileDetailFieldNames.TITLE));
        public static readonly BusinessRule ERROR_MSG_Required = 
            new BusinessRule("ERROR_MSG", String.Format(requiredFormat, ApplicantFileDetailFieldNames.ERROR_MSG));

    }
}