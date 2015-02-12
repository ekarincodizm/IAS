using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class AcceptOff 
    {
        public String ACCEPT_OFF_CODE { get; set; } //	VARCHAR2	(	3	)
        public String OFF_NAME { get; set; } //	VARCHAR2	(	50	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String USER_ID { get; set; } //	VARCHAR2	(	15	)
        public DateTime USER_DATE { get; set; } //	DATE	(	7	)
    }
}
