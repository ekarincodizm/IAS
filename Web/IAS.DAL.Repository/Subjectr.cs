using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class Subjectr
    {
        //
        public string SUBJECT_CODE { get; set; }
        public string SUBJECT_NAME { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }

        //
        public decimal? GROUP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public decimal? EXAM_PASS { get; set; }
        public short? MAX_SCORE { get; set; }
        public string USER_ID { get; set; }
    }
}
