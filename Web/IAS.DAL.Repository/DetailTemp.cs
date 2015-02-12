using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    public class DetailTemp 
    {
        public Decimal IMPORT_ID { get; set; } //	NUMBER	(	22	)
        public String PETITION_TYPE { get; set; } //	VARCHAR2	(	3	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String SEQ { get; set; } //	VARCHAR2	(	4	)
        public String LICENSE_NO { get; set; } //	VARCHAR2	(	15	)
        public DateTime? LICENSE_ACTIVE_DATE { get; set; } //	DATE	(	7	)
        public DateTime? LICENSE_EXPIRE_DATE { get; set; } //	DATE	(	7	)
        public Decimal? LICENSE_FEE { get; set; } //	NUMBER	(	22	)
        public String CITIZEN_ID { get; set; } //	VARCHAR2	(	13	)
        public String TITLE_NAME { get; set; } //	VARCHAR2	(	50	)
        public String NAME { get; set; } //	VARCHAR2	(	50	)
        public String SURNAME { get; set; } //	VARCHAR2	(	40	)
        public String ADDR1 { get; set; } //	VARCHAR2	(	60	)
        public String ADDR2 { get; set; } //	VARCHAR2	(	60	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String EMAIL { get; set; } //	VARCHAR2	(	255	)
        public String CUR_ADDR { get; set; } //	VARCHAR2	(	60	)
        public String TEL_NO { get; set; } //	VARCHAR2	(	15	)
        public String CUR_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String REMARK { get; set; } //	VARCHAR2	(	500	)
        public String AR_ANSWER { get; set; } //	VARCHAR2	(	50	)
        public String OLD_COMP_CODE { get; set; } //	VARCHAR2	(	10	)
        public String ERR_MSG { get; set; } //	VARCHAR2	(	4000	)
        public String LOAD_STATUS { get; set; } //	VARCHAR2	(	1	)

    }
}
