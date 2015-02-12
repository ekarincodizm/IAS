using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class SubjectGroupD
    {
        public decimal ID { get; set; }
        public decimal COURSE_NUMBER { get; set; }
        public string SUBJECT_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string SUBJECT_NAME { get; set; }
        public decimal MAX_SCORE { get; set; }
    }
}
