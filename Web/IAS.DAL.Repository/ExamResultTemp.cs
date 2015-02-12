using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamResultTemp
    {
        #region Database Field
        public String STATUS_SAVE_SCORE { get; set; } //	VARCHAR2	(	1	)
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String SEQ_NO { get; set; } //	VARCHAR2	(	20	)
        public String SEAT_NO { get; set; } //	VARCHAR2	(	20	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	20	)
        public String TITLE { get; set; } //	VARCHAR2	(	20	)
        public String NAMES { get; set; } //	VARCHAR2	(	20	)
        public String LAST_NAME { get; set; } //	VARCHAR2	(	20	)
        public String ADDRESS1 { get; set; } //	VARCHAR2	(	100	)
        public String ADDRESS2 { get; set; } //	VARCHAR2	(	100	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	20	)
        public DateTime? BIRTH_DATE { get; set; } //	DATE	(	7	)
        public String SEX { get; set; } //	VARCHAR2	(	1	)
        public String EDUCATION_CODE { get; set; } //	VARCHAR2	(	3	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public DateTime? APPROVE_DATE { get; set; } //	DATE	(	7	)
        public String EXAM_RESULT { get; set; } //	VARCHAR2	(	20	)
        public String SCORE_1 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_2 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_3 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_4 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_5 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_6 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_7 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_8 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_9 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_10 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_11 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_12 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_13 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_14 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_15 { get; set; } //	VARCHAR2	(	3	)
        public String SCORE_16 { get; set; } //	VARCHAR2	(	3	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	20	)
        public String ERROR_MSG { get; set; } //	VARCHAR2	(	200	)
        public String ABSENT_EXAM { get; set; } //	VARCHAR2	(	1	)
        #endregion

        #region Partial Property
        public String InsurCompName { get; set; }
        public String AssociateName { get; set; }
        public String LicenseTypeCode { get; set; }
        public String ProvinceCode { get; set; }
        public String AssociateCode { get; set; }
        public String TestingDate { get; set; }
        public String ApplicantCode { get; set; }
        public String TimeCode { get; set; }

        #endregion

    }
}
