using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantHeaderTemp
    {
        public string UPLOAD_GROUP_NO { get; set; }
        public string SOURCE_TYPE { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string COMP_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public DateTime? TESTING_DATE { get; set; }
        public short? EXAM_APPLY { get; set; }
        public decimal? EXAM_AMOUNT { get; set; }
        public string TEST_TIME_CODE { get; set; }
        public string FILENAME { get; set; }
    }
}
