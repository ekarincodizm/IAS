using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class PersonLicenseDetail // : AG_IAS_LICENSE_D
    {
        public String ERR_DESC { get; set; } //	VARCHAR2	(	100	)
        public String APPROVED { get; set; } //	VARCHAR2	(	1	)
        public DateTime? APPROVED_DATE { get; set; } //	DATE	(	7	)
        public String APPROVED_BY { get; set; } //	VARCHAR2	(	20	)
        public String REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public String HEAD_REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public DateTime? PAY_EXPIRE { get; set; } //	DATE	(	7	)
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String SEQ_NO { get; set; } //	VARCHAR2	(	4	)
        public String ORDERS { get; set; } //	VARCHAR2	(	4	)
        public String LICENSE_NO { get; set; } //	VARCHAR2	(	15	)
        public DateTime? LICENSE_DATE { get; set; } //	DATE	(	7	)
        public DateTime? LICENSE_EXPIRE_DATE { get; set; } //	DATE	(	7	)
        public Decimal? FEES { get; set; } //	NUMBER	(	22	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	15	)
        public String RENEW_TIMES { get; set; } //	VARCHAR2	(	3	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        public String TITLE_NAME { get; set; } //	VARCHAR2	(	20	)
        public String NAMES { get; set; } //	VARCHAR2	(	50	)
        public String LASTNAME { get; set; } //	VARCHAR2	(	40	)
        public String ADDRESS_1 { get; set; } //	VARCHAR2	(	60	)
        public String ADDRESS_2 { get; set; } //	VARCHAR2	(	60	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String CURRENT_ADDRESS_1 { get; set; } //	VARCHAR2	(	60	)
        public String CURRENT_ADDRESS_2 { get; set; } //	VARCHAR2	(	60	)
        public String CURRENT_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String EMAIL { get; set; } //	VARCHAR2	(	50	)
        public DateTime AR_DATE { get; set; } //	DATE	(	7	)
        public String OLD_COMP_CODE { get; set; } //	VARCHAR2	(	4	)
    }
}
