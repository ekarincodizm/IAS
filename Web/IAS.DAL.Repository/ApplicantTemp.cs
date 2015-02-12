using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantTemp 
    {
        public DTO.UploadHeader Header { get; set; }

        public String LOAD_STATUS { get; set; } //	VARCHAR2	(	1	)
        public Int32? APPLICANT_CODE { get; set; } //	NUMBER	(	22	)
        public String TESTING_NO { get; set; } //	VARCHAR2	(	6	)
        public String EXAM_PLACE_CODE { get; set; } //	VARCHAR2	(	6	)
        public String ACCEPT_OFF_CODE { get; set; } //	VARCHAR2	(	3	)
        public DateTime? APPLY_DATE { get; set; } //	DATE	(	7	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	13	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        public String NAMES { get; set; } //	VARCHAR2	(	30	)
        public String LASTNAME { get; set; } //	VARCHAR2	(	35	)
        public DateTime? BIRTH_DATE { get; set; } //	DATE	(	7	)
        public String SEX { get; set; } //	VARCHAR2	(	1	)
        public String EDUCATION_CODE { get; set; } //	VARCHAR2	(	2	)
        public String ADDRESS1 { get; set; } //	VARCHAR2	(	60	)
        public String ADDRESS2 { get; set; } //	VARCHAR2	(	60	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String ZIPCODE { get; set; } //	VARCHAR2	(	5	)
        public String TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String AMOUNT_TRAN_NO { get; set; } //	VARCHAR2	(	15	)
        public String PAYMENT_NO { get; set; } //	VARCHAR2	(	12	)
        public String INSUR_COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String ABSENT_EXAM { get; set; } //	VARCHAR2	(	1	)
        public String RESULT { get; set; } //	VARCHAR2	(	1	)
        public DateTime? EXPIRE_DATE { get; set; } //	DATE	(	7	)
        public String LICENSE { get; set; } //	VARCHAR2	(	1	)
        public String CANCEL_REASON { get; set; } //	VARCHAR2	(	300	)
        public String RECORD_STATUS { get; set; } //	VARCHAR2	(	1	)
        public String USER_ID { get; set; } //	VARCHAR2	(	15	)
        public DateTime? USER_DATE { get; set; } //	DATE	(	7	)
        public String EXAM_STATUS { get; set; } //	VARCHAR2	(	1	)
        public String REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String SEQ_NO { get; set; } //	VARCHAR2	(	4	)
        public String TITLE { get; set; } //	VARCHAR2	(	20	)
        public String ERROR_MSG { get; set; } //	VARCHAR2	(	200	)

        public DateTime? TESTING_DATE { get; set; }

        public string RUN_NO { get; set; }

        public String TEST_TIME_CODE { get; set; }

    }
}
