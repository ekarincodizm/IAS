using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Exam.ExamRequestUploads 
{
    public class ExamFileHeaderBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";
        static String worngSize = "จำนวนตัวอักษรของ {0} ไม่ถูกต้อง {1}";
        public static readonly BusinessRule FILENAME_Required = new BusinessRule("FILENAME", String.Format(requiredFormat, ExamFileHeaderFieldNames.FILENAME));
        public static readonly BusinessRule UPLOAD_GROUP_NO_Required = new BusinessRule("UPLOAD_GROUP_NO", String.Format(requiredFormat, ExamFileHeaderFieldNames.UPLOAD_GROUP_NO));
        public static readonly BusinessRule ASSOCIATE_NAME_Required = new BusinessRule("ASSOCIATE_NAME", String.Format(requiredFormat, ExamFileHeaderFieldNames.ASSOCIATE_NAME));
        public static readonly BusinessRule ASSOCIATE_NAME_Worng = new BusinessRule ("ASSOCIATE_NAME",string.Format(worngSize,ExamFileHeaderFieldNames.ASSOCIATE_NAME,Resources.errorExamFileHeaderBusinessRules_ASSOCIATE_NAME_Worng));
        public static readonly BusinessRule LICENSE_TYPE_CODE_Required = new BusinessRule("LICENSE_TYPE_CODE", String.Format(requiredFormat, ExamFileHeaderFieldNames.LICENSE_TYPE_CODE));
        public static readonly BusinessRule LICENSE_TYPE_CODE_Worng = new BusinessRule("LICENSE_TYPE_CODE_WORNG",string.Format(worngSize,ExamFileHeaderFieldNames.LICENSE_TYPE_CODE,Resources.errorExamFileHeaderBusinessRules_LICENSE_TYPE_CODE_Worng));
        public static readonly BusinessRule PROVINCE_CODE_Required = new BusinessRule("PROVINCE_CODE", String.Format(requiredFormat, ExamFileHeaderFieldNames.PROVINCE_CODE));
        public static readonly BusinessRule PROVINCE_CODE_Worng = new BusinessRule("PROVINCE_CODE",string.Format(worngSize,ExamFileHeaderFieldNames.PROVINCE_CODE));
        public static readonly BusinessRule ASSOCIATE_CODE_Required = new BusinessRule("ASSOCIATE_CODE", String.Format(requiredFormat, ExamFileHeaderFieldNames.ASSOCIATE_CODE));
        public static readonly BusinessRule ASSOCIATE_CODE_Worng = new BusinessRule("ASSOCIATE_CODE", String.Format(worngSize, ExamFileHeaderFieldNames.ASSOCIATE_CODE, Resources.errorExamFileHeaderBusinessRules_ASSOCIATE_CODE_Worng));
        public static readonly BusinessRule TESTING_DATE_Required = new BusinessRule("TESTING_DATE", String.Format(requiredFormat, ExamFileHeaderFieldNames.TESTING_DATE));
        public static readonly BusinessRule EXAM_TIME_CODE_Required = new BusinessRule("EXAM_TIME_CODE", String.Format(requiredFormat, ExamFileHeaderFieldNames.EXAM_TIME_CODE));
        public static readonly BusinessRule EXAM_TIME_CODE_Worng = new BusinessRule("EXAM_TIME_CODE", string.Format(worngSize, ExamFileHeaderFieldNames.EXAM_TIME_CODE, Resources.errorExamFileDetailBusinessRules_AREA_CODE_Worng));
        public static readonly BusinessRule CNT_PER_Required = new BusinessRule("CNT_PER", String.Format(requiredFormat, ExamFileHeaderFieldNames.CNT_PER));
        public static readonly BusinessRule CNT_PER_Worng = new BusinessRule("CNT_PER",string.Format(worngSize,ExamFileHeaderFieldNames.CNT_PER,Resources.errorExamFileHeaderBusinessRules_CNT_PER_Worng));

    }
}