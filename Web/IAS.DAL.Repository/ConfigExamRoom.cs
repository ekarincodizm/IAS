using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    public class ConfigExamRoom 
    {
        public String EXAM_ROOM_CODE { get; set; } //	VARCHAR2	(	6	)
        public String EXAM_ROOM_NAME { get; set; } //	VARCHAR2	(	60	)
        public Decimal SEAT_AMOUNT { get; set; } //	NUMBER	(	22	)
        public String EXAM_PLACE_CODE { get; set; } //	VARCHAR2	(	6	)
        public String USER_ID { get; set; } //	VARCHAR2	(	15	)
        public DateTime USER_DATE { get; set; } //	DATE	(	7	)
        public String USER_ID_UPDATE { get; set; } //	VARCHAR2	(	15	)
        public DateTime USER_DATE_UPDATE { get; set; } //	DATE	(	7	)
        public String ACTIVE { get; set; } //	VARCHAR2	(	1	)
    }
}
