using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
   public class ExamRoomLicense 
    {
        public string TESTING_NO { get; set; }
        public string EXAM_ROOM_CODE { get; set; }
        public int NUMBER_SEAT_ROOM { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string USER_ID_UPDATE { get; set; }
        public DateTime? USER_DATE_UPDATE { get; set; }
        public string ACTIVE { get; set; }
    }
}
