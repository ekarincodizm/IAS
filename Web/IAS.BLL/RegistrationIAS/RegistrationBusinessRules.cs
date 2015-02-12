using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS
{
    public class RegistrationBusinessRules  
    {
        static String requiredFormat = "การลงทะเบียนจะต้องมี ข้อมูล {0} .";

        public static readonly BusinessRule ID_Required = 
            new BusinessRule("ID", String.Format(requiredFormat, THRegFieldNames.ID));

        public static readonly BusinessRule MEMBER_TYPE_Required = 
            new BusinessRule("MEMBER_TYPE", String.Format(requiredFormat, THRegFieldNames.MEMBER_TYPE));

        public static readonly BusinessRule ID_CARD_NO_Required = 
            new BusinessRule("ID_CARD_NO", String.Format(requiredFormat, THRegFieldNames.ID_CARD_NO));

        public static readonly BusinessRule EMPLOYEE_NO_Required = 
            new BusinessRule("EMPLOYEE_NO", String.Format(requiredFormat, THRegFieldNames.EMPLOYEE_NO));

        public static readonly BusinessRule PRE_NAME_CODE_Required = 
            new BusinessRule("PRE_NAME_CODE", String.Format(requiredFormat, THRegFieldNames.PRE_NAME_CODE));

        public static readonly BusinessRule NAMES_Required = 
            new BusinessRule("NAMES", String.Format(requiredFormat, THRegFieldNames.NAMES));

        public static readonly BusinessRule LASTNAME_Required = 
            new BusinessRule("LASTNAME", String.Format(requiredFormat, THRegFieldNames.LASTNAME));

        public static readonly BusinessRule NATIONALITY_Required = 
            new BusinessRule("NATIONALITY", String.Format(requiredFormat, THRegFieldNames.NATIONALITY));

        public static readonly BusinessRule BIRTH_DATE_Required = 
            new BusinessRule("BIRTH_DATE", String.Format(requiredFormat, THRegFieldNames.BIRTH_DATE));

        public static readonly BusinessRule SEX_Required = 
            new BusinessRule("SEX", String.Format(requiredFormat, THRegFieldNames.SEX));

        public static readonly BusinessRule EDUCATION_CODE_Required = 
            new BusinessRule("EDUCATION_CODE", String.Format(requiredFormat, THRegFieldNames.EDUCATION_CODE));

        public static readonly BusinessRule ADDRESS_1_Required = 
            new BusinessRule("ADDRESS_1", String.Format(requiredFormat, THRegFieldNames.ADDRESS_1));

        public static readonly BusinessRule ADDRESS_2_Required = 
            new BusinessRule("ADDRESS_2", String.Format(requiredFormat, THRegFieldNames.ADDRESS_2));

        public static readonly BusinessRule AREA_CODE_Required = 
            new BusinessRule("AREA_CODE", String.Format(requiredFormat, THRegFieldNames.AREA_CODE));

        public static readonly BusinessRule PROVINCE_CODE_Required = 
            new BusinessRule("PROVINCE_CODE", String.Format(requiredFormat, THRegFieldNames.PROVINCE_CODE));

        public static readonly BusinessRule ZIP_CODE_Required = 
            new BusinessRule("ZIP_CODE", String.Format(requiredFormat, THRegFieldNames.ZIP_CODE));

        public static readonly BusinessRule TELEPHONE_Required = 
            new BusinessRule("TELEPHONE", String.Format(requiredFormat, THRegFieldNames.TELEPHONE));

        public static readonly BusinessRule LOCAL_ADDRESS1_Required = 
            new BusinessRule("LOCAL_ADDRESS1", String.Format(requiredFormat, THRegFieldNames.LOCAL_ADDRESS1));

        public static readonly BusinessRule LOCAL_ADDRESS2_Required = 
            new BusinessRule("LOCAL_ADDRESS2", String.Format(requiredFormat, THRegFieldNames.LOCAL_ADDRESS2));

        public static readonly BusinessRule LOCAL_AREA_CODE_Required = 
            new BusinessRule("LOCAL_AREA_CODE", String.Format(requiredFormat, THRegFieldNames.LOCAL_AREA_CODE));

        public static readonly BusinessRule LOCAL_PROVINCE_CODE_Required = 
            new BusinessRule("LOCAL_PROVINCE_CODE", String.Format(requiredFormat, THRegFieldNames.LOCAL_PROVINCE_CODE));

        public static readonly BusinessRule LOCAL_ZIPCODE_Required = 
            new BusinessRule("LOCAL_ZIPCODE", String.Format(requiredFormat, THRegFieldNames.LOCAL_ZIPCODE));

        public static readonly BusinessRule LOCAL_TELEPHONE_Required = 
            new BusinessRule("LOCAL_TELEPHONE", String.Format(requiredFormat, THRegFieldNames.LOCAL_TELEPHONE));

        public static readonly BusinessRule EMAIL_Required = 
            new BusinessRule("EMAIL", String.Format(requiredFormat, THRegFieldNames.EMAIL));

        public static readonly BusinessRule STATUS_Required = 
            new BusinessRule("STATUS", String.Format(requiredFormat, THRegFieldNames.STATUS));

        public static readonly BusinessRule TUMBON_CODE_Required = 
            new BusinessRule("TUMBON_CODE", String.Format(requiredFormat, THRegFieldNames.TUMBON_CODE));

        public static readonly BusinessRule LOCAL_TUMBON_CODE_Required = 
            new BusinessRule("LOCAL_TUMBON_CODE", String.Format(requiredFormat, THRegFieldNames.LOCAL_TUMBON_CODE));

        public static readonly BusinessRule COMP_CODE_Required = 
            new BusinessRule("COMP_CODE", String.Format(requiredFormat, THRegFieldNames.COMP_CODE));

        public static readonly BusinessRule CREATED_BY_Required = 
            new BusinessRule("CREATED_BY", String.Format(requiredFormat, THRegFieldNames.CREATED_BY));

        public static readonly BusinessRule CREATED_DATE_Required = 
            new BusinessRule("CREATED_DATE", String.Format(requiredFormat, THRegFieldNames.CREATED_DATE));

        public static readonly BusinessRule UPDATED_BY_Required = 
            new BusinessRule("UPDATED_BY", String.Format(requiredFormat, THRegFieldNames.UPDATED_BY));

        public static readonly BusinessRule UPDATED_DATE_Required = 
            new BusinessRule("UPDATED_DATE", String.Format(requiredFormat, THRegFieldNames.UPDATED_DATE));

        public static readonly BusinessRule NOT_APPROVE_DATE_Required = 
            new BusinessRule("NOT_APPROVE_DATE", String.Format(requiredFormat, THRegFieldNames.NOT_APPROVE_DATE));

        public static readonly BusinessRule LINK_REDIRECT_Required = 
            new BusinessRule("LINK_REDIRECT", String.Format(requiredFormat, THRegFieldNames.LINK_REDIRECT));

        public static readonly BusinessRule REG_PASS_Required = 
            new BusinessRule("REG_PASS", String.Format(requiredFormat, THRegFieldNames.REG_PASS));


    }
}
