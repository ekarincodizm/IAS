using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamHeaderResultTemp 
    {
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String ASSOCIATE_NAME { get; set; } //	VARCHAR2	(	80	)
        public String LICENSE_TYPE_CODE { get; set; } //	VARCHAR2	(	2	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	2	)
        public String ASSOCIATE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String TESTING_DATE { get; set; } //	VARCHAR2	(	10	)
        public String EXAM_TIME_CODE { get; set; } //	VARCHAR2	(	2	)
        public String CNT_PER { get; set; } //	VARCHAR2	(	10	)
        public String FILENAME { get; set; } //	VARCHAR2	(	300	)
        public String EXAM_PLACE_CODE { get; set; }
    }
}
