using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamHistory
    {
        public string ID_CARD_NO { get; set; }
        public Int32 APPLICANT_CODE { get; set; }
        public string TESTING_NO { get; set; }
        public DateTime? TESTING_DATE { get; set; }
        public string TEST_TIME { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public string EXAM_PLACE_NAME { get; set; }
        public string INSUR_COMP_NAME { get; set; }
        public string EXAM_RESULT { get; set; }
        public DateTime? EXPIRE_DATE { get; set; }

        public string RESULT_DESC { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string EXAM_PLACE_CODE { get; set; }

    }
}
