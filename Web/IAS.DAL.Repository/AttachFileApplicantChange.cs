using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class AttachFileApplicantChange 
    {
        public String ID_ATTACH_FILE { get; set; } //	VARCHAR2	(	15	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	15	)
        public String ATTACH_FILE_TYPE { get; set; } //	VARCHAR2	(	4	)
        public String ATTACH_FILE_PATH { get; set; } //	VARCHAR2	(	100	)
        public String REMARK { get; set; } //	VARCHAR2	(	100	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)
        public String FILE_STATUS { get; set; } //	VARCHAR2	(	1	)
        public Int32 CHANGE_ID { get; set; } //	NUMBER	(	22	)

    }
}
