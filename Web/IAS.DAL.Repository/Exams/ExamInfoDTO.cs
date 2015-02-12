using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    [Serializable]
    public class ExamInfoDTO
    {
        public Int32 RunNo { get; set; }
        public String TestingNo { get; set; }
        public DateTime TestingDate { get; set; }
        public String TestingTime { get; set; }
        public String ExamPlaceGroup { get; set; }
        public String ExamPlace { get; set; }
        public String Province { get; set; }
        public String SeatAmount { get; set; }
        public String LicenseType { get; set; }
        public Decimal ExamFee { get; set; }
        public String AgentType { get; set; }
        public String ExamOwner { get; set; }

        public String EXAM_PLACE_Code { get; set; }
        public String EXAM_PLACE_NAME { get; set; }
        public String TEST_TIME_CODE { get; set; } 
        public String LICENSE_TYPE_CODE { get; set; }
        public String PROVINCE_CODE { get; set; }    
        public String EXAM_PLACE_GROUP_CODE { get; set; }

    }
}
