using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class RegistrationAttatchFile
    {
        public string ID { get; set; }
        public string REGISTRATION_ID { get; set; }
        public string ATTACH_FILE_TYPE { get; set; }
        public string ATTACH_FILE_PATH { get; set; }
        public string REMARK { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string FILE_STATUS { get; set; }
        public string TempFilePath { get; set; }
        public string DocumentTypeName { get; set; }
        public bool IsImage { get; set; }
        public string FileName { get; set; }
        public string REQUEST_STATUS { get; set; }
    }

    [Serializable]
    public class PersonAttatchFile : RegistrationAttatchFile
    {
    }

    [Serializable]
    public class TempAttachFile : RegistrationAttatchFile
    {
    }

    [Serializable]
    public class LicenseAttatchFile : RegistrationAttatchFile
    {
    }

    [Serializable]
    public class LicenseTempAttachFile : RegistrationAttatchFile
    {
    }


}
