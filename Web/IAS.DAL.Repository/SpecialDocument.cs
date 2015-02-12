using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class SpecialDocument
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
        public String ORDER_BY { get; set; } //	VARCHAR2	(	3	)


        public String GROUP_LICENSE_ID { get; set; } //	VARCHAR2	(	15	)
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
        public String LICENSE_NO { get; set; } //	VARCHAR2	(	15	)
        public String RENEW_TIME { get; set; } //	VARCHAR2	(	2	)

        public String Id { get; set; }
        public String Name { get; set; }
    }
}
