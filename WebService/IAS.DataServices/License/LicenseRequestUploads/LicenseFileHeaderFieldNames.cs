using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Properties;

namespace IAS.DataServices.License.LicenseRequestUploads
{
    public class LicenseFileHeaderFieldNames
    {
        public static readonly String IMPORT_ID = Resources.propLicenseFileDetailFieldNames_IMPORT_ID;
        public static readonly String IMPORT_DATETIME = Resources.propLicenseFileHeaderFieldNames_IMPORT_DATETIME;
        public static readonly String FILE_NAME = Resources.propApplicantFileHeaderFieldNames_FILENAME;
        public static readonly String PETTITION_TYPE = Resources.propLicenseFileDetailFieldNames_PETITION_TYPE;
        public static readonly String LICENSE_TYPE_CODE = Resources.propLicenseFileHeaderFieldNames_LICENSE_TYPE_CODE;
        public static readonly String COMP_CODE = Resources.propApplicantFileHeaderFieldNames_COMP_CODE;
        public static readonly String COMP_NAME = Resources.propLicenseFileHeaderFieldNames_COMP_NAME;
        public static readonly String LICENSE_TYPE = Resources.propLicenseFileHeaderFieldNames_LICENSE_TYPE_CODE;
        public static readonly String SEND_DATE = Resources.propLicenseFileHeaderFieldNames_SEND_DATE;
        public static readonly String TOTAL_AGENT = Resources.propLicenseFileHeaderFieldNames_TOTAL_AGENT;
        public static readonly String TOTAL_FEE = Resources.propLicenseFileHeaderFieldNames_TOTAL_FEE;
        public static readonly String ERR_MSG = Resources.propLicenseFileDetailFieldNames_ERR_MSG;
        public static readonly String APPROVE_COMPCODE = Resources.propLicenseFileHeaderFieldNames_APPROVE_COMPCODE; 
    }
}