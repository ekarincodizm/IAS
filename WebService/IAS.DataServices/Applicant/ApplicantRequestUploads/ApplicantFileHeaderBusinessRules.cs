using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantFileHeaderBusinessRules
    {
        static String requiredFormat = "- ข้อมูล {0} ไม่ถูกต้อง." + Environment.NewLine;              

        public static readonly BusinessRule FILENAME_Required = new BusinessRule("FILENAME", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.FILENAME));
        public static readonly BusinessRule UPLOAD_GROUP_NO_Required = new BusinessRule("UPLOAD_GROUP_NO", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.UPLOAD_GROUP_NO));
        public static readonly BusinessRule SOURCE_TYPE_Required = new BusinessRule("SOURCE_TYPE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.SOURCE_TYPE));
        public static readonly BusinessRule PROVINCE_CODE_Required = new BusinessRule("PROVINCE_CODE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.PROVINCE_CODE));
        public static readonly BusinessRule COMP_CODE_Required = new BusinessRule("COMP_CODE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.COMP_CODE));
        public static readonly BusinessRule LICENSE_TYPE_CODE_Required = new BusinessRule("LICENSE_TYPE_CODE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.LICENSE_TYPE_CODE));
        public static readonly BusinessRule TESTING_DATE_Required = new BusinessRule("TESTING_DATE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.TESTING_DATE));
        public static readonly BusinessRule EXAM_APPLY_Required = new BusinessRule("EXAM_APPLY", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.EXAM_APPLY));
        public static readonly BusinessRule EXAM_AMOUNT_Required = new BusinessRule("EXAM_AMOUNT", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.EXAM_AMOUNT));
        public static readonly BusinessRule TEST_TIME_CODE_Required = new BusinessRule("TEST_TIME_CODE", String.Format(requiredFormat, ApplicantFileHeaderFieldNames.TEST_TIME_CODE));

    
    }
}