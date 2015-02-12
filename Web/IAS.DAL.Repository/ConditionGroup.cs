using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ConditionGroup
    {
        public decimal COURSE_NUMBER { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public string STATUS { get; set; }
        public string USER_ID { get; set; }
        public DateTime USER_DATE { get; set; }
        public string NOTE { get; set; }
        public List<Subjectr> Subject { get; set; }
    }
}
