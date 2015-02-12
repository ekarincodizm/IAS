using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public  class SaveSetApplicantRoom
    {
        #region AG_EXAM_LICENSE_R

        public string TESTING_NO { get; set; }
        public string EXAM_PLACE_CODE {get;set;}
        public DateTime TESTING_DATE { get; set; }
        public string TEST_TIME_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string USER_ID { get; set; }
        public DateTime USER_DATE { get; set; }
        public string EXAM_STATUS { get; set; }
        public int EXAM_APPLY { get; set; }
        public int EXAM_ADMISSION { get; set; }
        public int EXAM_FEE { get; set; }
        public string EXAM_OWNER { get; set; }
        public string SPECIAL { get; set; }
        public string COURSE_NUMBER { get; set; }
        public string EXAM_STATE { get; set; }
        public string EXAM_REMARK { get; set; }
        public string IMPORT_TYPE { get; set; }
        #endregion

        #region AG_IAS_EXAM_ROOM_LICENSE_R

        public List<ExamSubLicense> EXAM_ROOM_CODE { get; set; }
        public int NUMBER_SEAT_ROOM { get; set; }
        public string USER_ID_UPDATE { get; set; }
        public string ACTIVE { get; set; }

        #endregion
    }
}
