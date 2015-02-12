using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.RegistrationIAS.Implements;

namespace IAS.BLL.RegistrationIAS
{
    public static class RegistrationMapper 
    {
        public static DTO.Registration ConvertToDTORegisteration(this IRegistration registeration) 
        {
            return  new DTO.Registration() { 
                                    ID=registeration.ID, 
                                    MEMBER_TYPE=registeration.MEMBER_TYPE, 
                                    ID_CARD_NO=registeration.ID_CARD_NO, 
                                    EMPLOYEE_NO=registeration.EMPLOYEE_NO, 
                                    PRE_NAME_CODE=registeration.PRE_NAME_CODE, 
                                    NAMES=registeration.NAMES, 
                                    LASTNAME=registeration.LASTNAME, 
                                    NATIONALITY=registeration.NATIONALITY, 
                                    BIRTH_DATE=registeration.BIRTH_DATE, 
                                    SEX=registeration.SEX, 
                                    EDUCATION_CODE=registeration.EDUCATION_CODE, 
                                    ADDRESS_1=registeration.ADDRESS_1, 
                                    ADDRESS_2=registeration.ADDRESS_2, 
                                    AREA_CODE=registeration.AREA_CODE, 
                                    PROVINCE_CODE=registeration.PROVINCE_CODE, 
                                    ZIP_CODE=registeration.ZIP_CODE, 
                                    TELEPHONE=registeration.TELEPHONE, 
                                    LOCAL_ADDRESS1=registeration.LOCAL_ADDRESS1, 
                                    LOCAL_ADDRESS2=registeration.LOCAL_ADDRESS2, 
                                    LOCAL_AREA_CODE=registeration.LOCAL_AREA_CODE, 
                                    LOCAL_PROVINCE_CODE=registeration.LOCAL_PROVINCE_CODE, 
                                    LOCAL_ZIPCODE=registeration.LOCAL_ZIPCODE, 
                                    LOCAL_TELEPHONE=registeration.LOCAL_TELEPHONE, 
                                    EMAIL=registeration.EMAIL, 
                                    STATUS=registeration.STATUS, 
                                    TUMBON_CODE=registeration.TUMBON_CODE, 
                                    LOCAL_TUMBON_CODE=registeration.LOCAL_TUMBON_CODE, 
                                    COMP_CODE=registeration.COMP_CODE, 
                                    CREATED_BY=registeration.CREATED_BY, 
                                    CREATED_DATE=registeration.CREATED_DATE, 
                                    UPDATED_BY=registeration.UPDATED_BY, 
                                    UPDATED_DATE=registeration.UPDATED_DATE, 
                                    NOT_APPROVE_DATE=  registeration.NOT_APPROVE_DATE, 
                                    LINK_REDIRECT=registeration.LINK_REDIRECT,
                                    REG_PASS=registeration.REG_PASS, 
                
                };

        
        }

        public static IRegistration ConvertToPersonRegisteration(this DTO.Registration registeration) 
        {
            DateTime createdate =  (registeration.CREATED_DATE==null)?DateTime.MinValue: (DateTime)registeration.CREATED_DATE;
            DateTime birth_date = (registeration.BIRTH_DATE == null) ? DateTime.MinValue : (DateTime)registeration.BIRTH_DATE;
            DateTime updatedate = (registeration.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)registeration.UPDATED_DATE;

            IRegistration personRegis = new GeneralRegistration()
            {
                ID = registeration.ID,
                MEMBER_TYPE = registeration.MEMBER_TYPE,
                ID_CARD_NO = registeration.ID_CARD_NO,
                EMPLOYEE_NO = registeration.EMPLOYEE_NO,
                PRE_NAME_CODE = registeration.PRE_NAME_CODE,
                NAMES = registeration.NAMES,
                LASTNAME = registeration.LASTNAME,
                NATIONALITY = registeration.NATIONALITY,
                BIRTH_DATE = birth_date,
                SEX = registeration.SEX,
                EDUCATION_CODE = registeration.EDUCATION_CODE,
                ADDRESS_1 = registeration.ADDRESS_1,
                ADDRESS_2 = registeration.ADDRESS_2,
                AREA_CODE = registeration.AREA_CODE,
                PROVINCE_CODE = registeration.PROVINCE_CODE,
                ZIP_CODE = registeration.ZIP_CODE,
                TELEPHONE = registeration.TELEPHONE,
                LOCAL_ADDRESS1 = registeration.LOCAL_ADDRESS1,
                LOCAL_ADDRESS2 = registeration.LOCAL_ADDRESS2,
                LOCAL_AREA_CODE = registeration.LOCAL_AREA_CODE,
                LOCAL_PROVINCE_CODE = registeration.LOCAL_PROVINCE_CODE,
                LOCAL_ZIPCODE = registeration.LOCAL_ZIPCODE,
                LOCAL_TELEPHONE = registeration.LOCAL_TELEPHONE,
                EMAIL = registeration.EMAIL,
                STATUS = registeration.STATUS,
                TUMBON_CODE = registeration.TUMBON_CODE,
                LOCAL_TUMBON_CODE = registeration.LOCAL_TUMBON_CODE,
                COMP_CODE = registeration.COMP_CODE,
                CREATED_BY = registeration.CREATED_BY,
                CREATED_DATE = createdate,
                UPDATED_BY = registeration.UPDATED_BY,
                UPDATED_DATE = updatedate,
                NOT_APPROVE_DATE = registeration.NOT_APPROVE_DATE,
                LINK_REDIRECT = registeration.LINK_REDIRECT
                //, 
                //REG_PASS=registeration.REG_PASS, 

            };

            return personRegis;
        }

        public static IRegistration ConvertToCompanyRegisteration(this DTO.Registration registeration) 
        {
            DateTime createdate = (registeration.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)registeration.CREATED_DATE;
            DateTime birth_date = (registeration.BIRTH_DATE == null) ? DateTime.MinValue : (DateTime)registeration.BIRTH_DATE;
            DateTime updatedate = (registeration.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)registeration.UPDATED_DATE;

            IRegistration personRegis = new CompanyRegistration()
            {
                ID = registeration.ID,
                MEMBER_TYPE = registeration.MEMBER_TYPE,
                ID_CARD_NO = registeration.ID_CARD_NO,
                EMPLOYEE_NO = registeration.EMPLOYEE_NO,
                PRE_NAME_CODE = registeration.PRE_NAME_CODE,
                NAMES = registeration.NAMES,
                LASTNAME = registeration.LASTNAME,
                NATIONALITY = registeration.NATIONALITY,
                BIRTH_DATE = birth_date,
                SEX = registeration.SEX,
                EDUCATION_CODE = registeration.EDUCATION_CODE,
                ADDRESS_1 = registeration.ADDRESS_1,
                ADDRESS_2 = registeration.ADDRESS_2,
                AREA_CODE = registeration.AREA_CODE,
                PROVINCE_CODE = registeration.PROVINCE_CODE,
                ZIP_CODE = registeration.ZIP_CODE,
                TELEPHONE = registeration.TELEPHONE,
                LOCAL_ADDRESS1 = registeration.LOCAL_ADDRESS1,
                LOCAL_ADDRESS2 = registeration.LOCAL_ADDRESS2,
                LOCAL_AREA_CODE = registeration.LOCAL_AREA_CODE,
                LOCAL_PROVINCE_CODE = registeration.LOCAL_PROVINCE_CODE,
                LOCAL_ZIPCODE = registeration.LOCAL_ZIPCODE,
                LOCAL_TELEPHONE = registeration.LOCAL_TELEPHONE,
                EMAIL = registeration.EMAIL,
                STATUS = registeration.STATUS,
                TUMBON_CODE = registeration.TUMBON_CODE,
                LOCAL_TUMBON_CODE = registeration.LOCAL_TUMBON_CODE,
                COMP_CODE = registeration.COMP_CODE,
                CREATED_BY = registeration.CREATED_BY,
                CREATED_DATE = createdate,
                UPDATED_BY = registeration.UPDATED_BY,
                UPDATED_DATE = updatedate,
                NOT_APPROVE_DATE = registeration.NOT_APPROVE_DATE,
                LINK_REDIRECT = registeration.LINK_REDIRECT
                //, 
                //REG_PASS=registeration.REG_PASS, 

            };

            return personRegis;
        }

        public static IRegistration ConvertToOICRegisteration(this DTO.Registration registeration)
        {
            DateTime createdate = (registeration.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)registeration.CREATED_DATE;
            DateTime birth_date = (registeration.BIRTH_DATE == null) ? DateTime.MinValue : (DateTime)registeration.BIRTH_DATE;
            DateTime updatedate = (registeration.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)registeration.UPDATED_DATE;

            IRegistration personRegis = new OICTestStuffRegistration()
            {
                ID = registeration.ID,
                MEMBER_TYPE = registeration.MEMBER_TYPE,
                ID_CARD_NO = registeration.ID_CARD_NO,
                EMPLOYEE_NO = registeration.EMPLOYEE_NO,
                PRE_NAME_CODE = registeration.PRE_NAME_CODE,
                NAMES = registeration.NAMES,
                LASTNAME = registeration.LASTNAME,
                NATIONALITY = registeration.NATIONALITY,
                BIRTH_DATE = birth_date,
                SEX = registeration.SEX,
                EDUCATION_CODE = registeration.EDUCATION_CODE,
                ADDRESS_1 = registeration.ADDRESS_1,
                ADDRESS_2 = registeration.ADDRESS_2,
                AREA_CODE = registeration.AREA_CODE,
                PROVINCE_CODE = registeration.PROVINCE_CODE,
                ZIP_CODE = registeration.ZIP_CODE,
                TELEPHONE = registeration.TELEPHONE,
                LOCAL_ADDRESS1 = registeration.LOCAL_ADDRESS1,
                LOCAL_ADDRESS2 = registeration.LOCAL_ADDRESS2,
                LOCAL_AREA_CODE = registeration.LOCAL_AREA_CODE,
                LOCAL_PROVINCE_CODE = registeration.LOCAL_PROVINCE_CODE,
                LOCAL_ZIPCODE = registeration.LOCAL_ZIPCODE,
                LOCAL_TELEPHONE = registeration.LOCAL_TELEPHONE,
                EMAIL = registeration.EMAIL,
                STATUS = registeration.STATUS,
                TUMBON_CODE = registeration.TUMBON_CODE,
                LOCAL_TUMBON_CODE = registeration.LOCAL_TUMBON_CODE,
                COMP_CODE = registeration.COMP_CODE,
                CREATED_BY = registeration.CREATED_BY,
                CREATED_DATE = createdate,
                UPDATED_BY = registeration.UPDATED_BY,
                UPDATED_DATE = updatedate,
                NOT_APPROVE_DATE = registeration.NOT_APPROVE_DATE,
                LINK_REDIRECT = registeration.LINK_REDIRECT
                //, 
                //REG_PASS=registeration.REG_PASS, 

            };

            return personRegis;
        }
    }
}
