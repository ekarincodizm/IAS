using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamTime
    {
        public string TEST_TIME_CODE { get; set; }
        public string TEST_TIME { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public decimal START_TIME { get; set; }
        public decimal END_TIME { get; set; }
    }
}
