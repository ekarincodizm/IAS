using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamPlaceGroup
    {
        public string EXAM_PLACE_GROUP_CODE { get; set; }
        public string EXAM_PLACE_GROUP_NAME { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
    }
}
