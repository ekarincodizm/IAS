using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ExamStatistic
    {
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public decimal REGIS_EXAM { get; set; }
        public decimal EXAM_M { get; set; }
        public decimal EXAM_N { get; set; }
        public decimal RESULT_P { get; set; }
        public decimal RESULT_F { get; set; }
        public decimal PERCEN_EXAM { get; set; }
        public decimal PERCEN_RESULT { get; set; }
    }
}
