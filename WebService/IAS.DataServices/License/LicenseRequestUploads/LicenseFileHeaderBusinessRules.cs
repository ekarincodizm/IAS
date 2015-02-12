using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServices.License.LicenseRequestUploads
{
    public class LicenseFileHeaderBusinessRules
    {
        static String requiredFormat = "ข้อมูล {0} ไม่ถูกต้อง.";
        public static readonly BusinessRule IMPORT_ID_Required = new BusinessRule("IMPORT_ID", String.Format(requiredFormat, LicenseFileHeaderFieldNames.IMPORT_ID));
        public static readonly BusinessRule IMPORT_DATETIME_Required = new BusinessRule("IMPORT_DATETIME", String.Format(requiredFormat, LicenseFileHeaderFieldNames.IMPORT_DATETIME));
        public static readonly BusinessRule FILE_NAME_Required = new BusinessRule("FILE_NAME", String.Format(requiredFormat, LicenseFileHeaderFieldNames.FILE_NAME));
        public static readonly BusinessRule PETTITION_TYPE_Required = new BusinessRule("PETTITION_TYPE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.PETTITION_TYPE));
        public static readonly BusinessRule LICENSE_TYPE_CODE_Required = new BusinessRule("LICENSE_TYPE_CODE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.LICENSE_TYPE_CODE));
        public static readonly BusinessRule COMP_CODE_Required = new BusinessRule("COMP_CODE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.COMP_CODE));
        public static readonly BusinessRule COMP_NAME_Required = new BusinessRule("COMP_NAME", String.Format(requiredFormat, LicenseFileHeaderFieldNames.COMP_NAME));
        public static readonly BusinessRule LICENSE_TYPE_Required = new BusinessRule("LICENSE_TYPE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.LICENSE_TYPE));
        public static readonly BusinessRule SEND_DATE_Required = new BusinessRule("SEND_DATE", String.Format(requiredFormat, "วันที่นำส่ง"));
        public static readonly BusinessRule TOTAL_AGENT_Required = new BusinessRule("TOTAL_AGENT", String.Format(requiredFormat, LicenseFileHeaderFieldNames.TOTAL_AGENT));
        public static readonly BusinessRule TOTAL_FEE_Required = new BusinessRule("TOTAL_FEE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.TOTAL_FEE));
        public static readonly BusinessRule ERR_MSG_Required = new BusinessRule("ERR_MSG", String.Format(requiredFormat, LicenseFileHeaderFieldNames.ERR_MSG));
        public static readonly BusinessRule APPROVE_COMPCODE_Required = new BusinessRule("APPROVE_COMPCODE", String.Format(requiredFormat, LicenseFileHeaderFieldNames.APPROVE_COMPCODE));
    }
}