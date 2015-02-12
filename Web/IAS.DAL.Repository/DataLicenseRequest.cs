using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class DataLicenseRequest
    {
        public DTO.UserProfile UserProfile { get; set; }
        public String FileName { get; set; }
        public String PettitionTypeCode { get; set; }
        public String LicenseTypeCode { get; set; }
        public String ReplaceType { get; set; }
        public String ApproveCom { get; set; }

    }
}
