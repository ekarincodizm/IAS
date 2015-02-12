using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IAS.DTO
{
    [Serializable]
    public class Registration 
    {
        public String ID { get; set; } //	VARCHAR2	(	15	)
        public String MEMBER_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	13	)
        public String EMPLOYEE_NO { get; set; } //	VARCHAR2	(	20	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        public String NAMES { get; set; } //	VARCHAR2	(	50	)
        public String LASTNAME { get; set; } //	VARCHAR2	(	70	)
        public String NATIONALITY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? BIRTH_DATE { get; set; } //	DATE	(	7	)
        public String SEX { get; set; } //	VARCHAR2	(	1	)
        public String EDUCATION_CODE { get; set; } //	VARCHAR2	(	2	)
        public String ADDRESS_1 { get; set; } //	VARCHAR2	(	200	)
        public String ADDRESS_2 { get; set; } //	VARCHAR2	(	200	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String ZIP_CODE { get; set; } //	VARCHAR2	(	5	)
        public String TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String LOCAL_ADDRESS1 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_ADDRESS2 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String LOCAL_PROVINCE_CODE { get; set; } //	VARCHAR2	(	20	)
        public String LOCAL_ZIPCODE { get; set; } //	VARCHAR2	(	5	)
        public String LOCAL_TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String EMAIL { get; set; } //	VARCHAR2	(	255	)
        public String STATUS { get; set; } //	VARCHAR2	(	1	)
        public String TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String LOCAL_TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)
        public DateTime? NOT_APPROVE_DATE { get; set; } //	DATE	(	7	)
        public String LINK_REDIRECT { get; set; } //	VARCHAR2	(	50	)
        public String REG_PASS { get; set; } //	VARCHAR2	(	100	)
        public String APPROVE_RESULT { get; set; } //	VARCHAR2	(	100	)
        public String APPROVED_BY { get; set; } //	VARCHAR2	(	15	)
        public String AGENT_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String Company_Name { get; set; }
        public String REG_PASSWORD { get; set; }
        public DateTime? LASTPASSWORD_CHANGDATE { get; set; }
        public String IMPORT_STATUS { get; set; }

    }
}
