using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamSchedule //: DAL.AG_EXAM_LICENSE_R
    {
        public string TESTING_NO { get; set; }
        public string EXAM_PLACE_GROUP_CODE { get; set; }
        public string EXAM_PLACE_CODE { get; set; }
        public DateTime TESTING_DATE { get; set; }
        public string TEST_TIME_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string USER_ID { get; set; }
        public DateTime USER_DATE { get; set; }
        public string EXAM_STATUS { get; set; }
        public short? EXAM_APPLY { get; set; }
        public short? EXAM_ADMISSION { get; set; }
        public decimal? EXAM_FEE { get; set; }
        public string EXAM_OWNER { get; set; }
        public string TEST_TIME { get; set; }
        public string NAME { get; set; }
        public string EXAM_PLACE_GROUP_NAME { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public decimal COURSE_NUMBER { get; set; }
        public string ASSOCIATION_NAME { get; set; }
        public string SPECIAL { get; set; }
        public string ASSOCIATION_CODE { get; set; }
        public string IMPORT_YPE { get; set; }
        public string PRIVILEGE_STATUS { get; set; }
        public string REMARK { get; set; }
    }
}
