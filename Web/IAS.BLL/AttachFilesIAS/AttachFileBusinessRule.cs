using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS
{
    class AttachFileBusinessRule
    {
        static String requiredFormat = "การแนบเอกสารจะต้องมี ข้อมูล {0}.";

        public static readonly BusinessRule ID_Required = 
            new BusinessRule("ID", String.Format(requiredFormat, AttachFileFieldTHNames.ID));

        public static readonly BusinessRule REGISTRATION_ID_Required = 
            new BusinessRule("REGISTRATION_ID", String.Format(requiredFormat, AttachFileFieldTHNames.REGISTRATION_ID));

        public static readonly BusinessRule ATTACH_FILE_TYPE_Required = 
            new BusinessRule("ATTACH_FILE_TYPE", String.Format(requiredFormat, AttachFileFieldTHNames.ATTACH_FILE_TYPE));

        public static readonly BusinessRule ATTACH_FILE_PATH_Required = 
            new BusinessRule("ATTACH_FILE_PATH", String.Format(requiredFormat, AttachFileFieldTHNames.ATTACH_FILE_PATH));

        public static readonly BusinessRule REMARK_Required = 
            new BusinessRule("REMARK", String.Format(requiredFormat, AttachFileFieldTHNames.REMARK));

        public static readonly BusinessRule CREATED_BY_Required = 
            new BusinessRule("CREATED_BY", String.Format(requiredFormat, AttachFileFieldTHNames.CREATED_BY));

        public static readonly BusinessRule CREATED_DATE_Required = 
            new BusinessRule("CREATED_DATE", String.Format(requiredFormat, AttachFileFieldTHNames.CREATED_DATE));

        public static readonly BusinessRule UPDATED_BY_Required = 
            new BusinessRule("UPDATED_BY", String.Format(requiredFormat, AttachFileFieldTHNames.UPDATED_BY));

        public static readonly BusinessRule UPDATED_DATE_Required = 
            new BusinessRule("UPDATED_DATE", String.Format(requiredFormat, AttachFileFieldTHNames.UPDATED_DATE));

        public static readonly BusinessRule FILE_STATUS_Required = 
            new BusinessRule("FILE_STATUS", String.Format(requiredFormat, AttachFileFieldTHNames.FILE_STATUS));
    }
}
