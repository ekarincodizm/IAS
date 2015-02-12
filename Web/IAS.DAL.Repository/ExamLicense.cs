using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamLicense 
    {
        public string TESTING_NO { get; set; }
        public string EXAM_PLACE_CODE { get; set; }
        public DateTime? TESTING_DATE { get; set; }
        public string TEST_TIME_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string EXAM_STATUS { get; set; }
        public short? EXAM_APPLY { get; set; }
        public short? EXAM_ADMISSION { get; set; }
        public short? EXAM_FEE { get; set; }
        public string EXAM_OWNER { get; set; }
    }
}
