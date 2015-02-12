using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class LicenseQuickReport
    {
        public string UPLOAD_GROUP_NO { get; set; }
        public DateTime TRAN_DATE { get; set; }
        public string LICENSE_NO { get; set; }
        public string ID_CARD_NO { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string PETITION_TYPE_NAME { get; set; }
        public DateTime LICENSE_EXPIRE_DATE { get; set; }
    }
}
