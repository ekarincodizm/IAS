using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class LicenseStaticRatioReport
    {
        public long COUNT_TYPE { get; set; }
        public string TYPE_NAME { get; set; }
        public string TYPE_CODE { get; set; }
        public long SUMONE { get; set; }
        public float FOR_SHARE { get; set; }
        public long COUNT_TYPE2 { get; set; }
        public string TYPE_NAME2 { get; set; }
        public string TYPE_CODE2 { get; set; }
        public long SUMONE2 { get; set; }
        public float FOR_SHARE2 { get; set; }
        public float RATIO { get; set; }
    }
}
