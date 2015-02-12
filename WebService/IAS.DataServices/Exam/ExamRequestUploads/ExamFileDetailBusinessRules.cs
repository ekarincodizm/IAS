using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;


namespace IAS.DataServices.Exam.ExamRequestUploads    
{
    public class ExamFileDetailBusinessRules
    {                                                   
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";
        static String worngSize = "จำนวนตัวอักษรของ {0} ไม่ถูกต้อง {1}";
        public static readonly BusinessRule STATUS_SAVE_SCORE_Required = new BusinessRule("STATUS_SAVE_SCORE", String.Format(requiredFormat, ExamFileDetailFieldNames.STATUS_SAVE_SCORE));
        public static readonly BusinessRule ABSENT_EXAM_Required = new BusinessRule("ABSENT_EXAM", String.Format(requiredFormat, ExamFileDetailFieldNames.ABSENT_EXAM));
        public static readonly BusinessRule ABSENT_EXAM_Worng = new BusinessRule("ABSENT_EXAM", string.Format(worngSize, ExamFileDetailFieldNames.ABSENT_EXAM, Resources.errorExamFileDetailBusinessRules_ABSENT_EXAM_Worng));
        public static readonly BusinessRule PRE_NAME_CODE_Required = new BusinessRule("PRE_NAME_CODE", String.Format(requiredFormat, ExamFileDetailFieldNames.PRE_NAME_CODE));
        public static readonly BusinessRule ERROR_MSG_Required = new BusinessRule("ERROR_MSG", String.Format(requiredFormat, ExamFileDetailFieldNames.ERROR_MSG));
        public static readonly BusinessRule UPLOAD_GROUP_NO_Required = new BusinessRule("UPLOAD_GROUP_NO", String.Format(requiredFormat, ExamFileDetailFieldNames.UPLOAD_GROUP_NO));
        public static readonly BusinessRule SEQ_NO_Required = new BusinessRule("SEQ_NO", String.Format(requiredFormat, ExamFileDetailFieldNames.SEQ_NO));
        public static readonly BusinessRule SEQ_NO_Worng = new BusinessRule("SEQ_NO", String.Format(worngSize, ExamFileDetailFieldNames.SEQ_NO,Resources.errorExamFileDetailBusinessRules_SEQ_NO_Worng));
        public static readonly BusinessRule SEAT_NO_Dup = new BusinessRule("SEAT_NO", Resources.errorExamFileDetailBusinessRules_SEAT_NO_Dup);
        public static readonly BusinessRule SEQ_NO_Dup = new BusinessRule("SEQ_NO", Resources.errorExamFileDetailBusinessRules_SEQ_NO_Dup);
        public static readonly BusinessRule SEAT_NO_Required = new BusinessRule("SEAT_NO", String.Format(requiredFormat, ExamFileDetailFieldNames.SEAT_NO));
        public static readonly BusinessRule SEAT_NO_Worng = new BusinessRule("SEAT_NO", String.Format(worngSize, ExamFileDetailFieldNames.SEAT_NO,Resources.errorExamFileDetailBusinessRules_SEQ_NO_Worng));
        public static readonly BusinessRule ID_CARD_NO_Required = new BusinessRule("ID_CARD_NO", String.Format(requiredFormat, ExamFileDetailFieldNames.ID_CARD_NO));
        public static readonly BusinessRule ID_CARD_NO_NOT_13 = new BusinessRule("ID_CARD_NO",string.Format(worngSize,ExamFileDetailFieldNames.ID_CARD_NO,Resources.errorExamFileDetailBusinessRules_ID_CARD_NO_NOT_13));
        public static readonly BusinessRule ID_CARD_NO_Dup = new BusinessRule("ID_CARD_NO", Resources.errorExamFileDetailBusinessRules_ID_CARD_NO_Dup);
        public static readonly BusinessRule TITLE_Required = new BusinessRule("TITLE", String.Format(requiredFormat, ExamFileDetailFieldNames.TITLE));
        public static readonly BusinessRule NAMES_Required = new BusinessRule("NAMES", String.Format(requiredFormat, ExamFileDetailFieldNames.NAMES));
        public static readonly BusinessRule LAST_NAME_Required = new BusinessRule("LAST_NAME", String.Format(requiredFormat, ExamFileDetailFieldNames.LAST_NAME));
        public static readonly BusinessRule TITLE_Worng = new BusinessRule("TITLE", String.Format(worngSize, ExamFileDetailFieldNames.TITLE,Resources.errorExamFileDetailBusinessRules_SEQ_NO_Worng));
        public static readonly BusinessRule NAMES_Worng = new BusinessRule("NAMES", String.Format(worngSize, ExamFileDetailFieldNames.NAMES,Resources.errorExamFileDetailBusinessRules_SEQ_NO_Worng));
        public static readonly BusinessRule LAST_NAME_Worng = new BusinessRule("LAST_NAME", String.Format(worngSize, ExamFileDetailFieldNames.LAST_NAME,Resources.errorExamFileDetailBusinessRules_SEQ_NO_Worng));
        public static readonly BusinessRule ADDRESS1_Required = new BusinessRule("ADDRESS1", String.Format(requiredFormat, ExamFileDetailFieldNames.ADDRESS1));
        public static readonly BusinessRule ADDRESS2_Required = new BusinessRule("ADDRESS2", String.Format(requiredFormat, ExamFileDetailFieldNames.ADDRESS2));
        public static readonly BusinessRule AREA_CODE_Required = new BusinessRule("AREA_CODE", String.Format(requiredFormat, ExamFileDetailFieldNames.AREA_CODE));
        public static readonly BusinessRule AREA_CODE_Worng = new BusinessRule("AREA_CODE", String.Format(worngSize, ExamFileDetailFieldNames.AREA_CODE,Resources.errorExamFileDetailBusinessRules_AREA_CODE_Worng));
        public static readonly BusinessRule BIRTH_DATE_Required = new BusinessRule("BIRTH_DATE", String.Format(requiredFormat, ExamFileDetailFieldNames.BIRTH_DATE));
        public static readonly BusinessRule SEX_Required = new BusinessRule("SEX", String.Format(requiredFormat, ExamFileDetailFieldNames.SEX));
        public static readonly BusinessRule SEX_NOT_1 = new BusinessRule("SEX", string.Format(worngSize, ExamFileDetailFieldNames.SEX, Resources.errorExamFileDetailBusinessRules_SEX_NOT_1));
        public static readonly BusinessRule EDUCATION_CODE_Required = new BusinessRule("EDUCATION_CODE", String.Format(requiredFormat, ExamFileDetailFieldNames.EDUCATION_CODE));
        public static readonly BusinessRule EDUCATION_CODE_Worng = new BusinessRule("EDUCATION_CODE", string.Format(worngSize, ExamFileDetailFieldNames.EDUCATION_CODE, Resources.errorExamFileDetailBusinessRules_EDUCATION_CODE_Worng));
        public static readonly BusinessRule COMP_CODE_Required = new BusinessRule("COMP_CODE", String.Format(requiredFormat, ExamFileDetailFieldNames.COMP_CODE));
        public static readonly BusinessRule APPROVE_DATE_Required = new BusinessRule("APPROVE_DATE", String.Format(requiredFormat, ExamFileDetailFieldNames.APPROVE_DATE));
        public static readonly BusinessRule EXAM_RESULT_Required = new BusinessRule("EXAM_RESULT", String.Format(requiredFormat, ExamFileDetailFieldNames.EXAM_RESULT));
        public static readonly BusinessRule SCORE_1_Required = new BusinessRule("SCORE_1", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_1, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_2_Required = new BusinessRule("SCORE_2", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_2, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_3_Required = new BusinessRule("SCORE_3", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_3, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_4_Required = new BusinessRule("SCORE_4", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_4, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_5_Required = new BusinessRule("SCORE_5", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_5, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_6_Required = new BusinessRule("SCORE_6", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_6, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_7_Required = new BusinessRule("SCORE_7", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_7, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_8_Required = new BusinessRule("SCORE_8", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_8, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_9_Required = new BusinessRule("SCORE_9", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_9, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_10_Required = new BusinessRule("SCORE_10", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_10, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_11_Required = new BusinessRule("SCORE_11", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_11, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_12_Required = new BusinessRule("SCORE_12", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_12, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_13_Required = new BusinessRule("SCORE_13", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_13, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_14_Required = new BusinessRule("SCORE_14", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_14, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_15_Required = new BusinessRule("SCORE_15", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_15, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
        public static readonly BusinessRule SCORE_16_Required = new BusinessRule("SCORE_16", String.Format(worngSize, ExamFileDetailFieldNames.SCORE_16, Resources.errorExamFileDetailBusinessRules_SCORE_Required));
    }
}