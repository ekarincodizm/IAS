using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class DocumentType 
    {
        public String DOCUMENT_CODE { get; set; } //	VARCHAR2	(	2	)
        public String DOCUMENT_NAME { get; set; } //	VARCHAR2	(	500	)
        public String USER_ID { get; set; } //	VARCHAR2	(	20	)
        public DateTime USER_DATE { get; set; } //	DATE	(	7	)
        public String MEMBER_TYPE_CODE { get; set; } //	VARCHAR2	(	1	)
        public String DOCUMENT_REQUIRE { get; set; } //	VARCHAR2	(	1	)
        public String IS_CARD_PIC { get; set; } //	VARCHAR2	(	1	)
        public String STATUS { get; set; } //	VARCHAR2	(	1	)
        public String TRAIN_DISCOUNT_STATUS { get; set; } //	VARCHAR2	(	1	)
        public String EXAM_DISCOUNT_STATUS { get; set; } //	VARCHAR2	(	1	)
        public String SPECIAL_TYPE_CODE_TRAIN { get; set; } //	VARCHAR2	(	5	)
        public String SPECIAL_TYPE_CODE_EXAM { get; set; } //	VARCHAR2	(	5	)
    }
}
