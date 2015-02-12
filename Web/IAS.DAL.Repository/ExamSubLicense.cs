using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamSubLicense
    {
        public string TESTING_NO { get; set; }
        public string EXAM_ROOM_CODE { get; set; }
        public Int16? NUMBER_SEAT_ROOM { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        //Option
        public string ROOM_NAME { get; set; }
        public Int16? SEAT_AMOUNT { get; set; }
    }
}
