using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SubPaymentHead 
    {
        public DateTime TRAIN_DATE_EXP { get; set; } //	DATE	(	7	)
        public String UPLOAD_BY_SESSION { get; set; } //	VARCHAR2	(	15	)
        public String APPROVED_DOC { get; set; } //	VARCHAR2	(	1	)
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String HEAD_REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public String GROUP_REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public String PETITION_TYPE_CODE { get; set; } //	VARCHAR2	(	2	)
        public Decimal PERSON_NO { get; set; } //	NUMBER	(	22	)
        public Decimal SUBPAYMENT_AMOUNT { get; set; } //	NUMBER	(	22	)
        public DateTime SUBPAYMENT_DATE { get; set; } //	DATE	(	7	)
        public String REMARK { get; set; } //	VARCHAR2	(	100	)
        public String STATUS { get; set; } //	VARCHAR2	(	1	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)

        public String SEQ_OF_GROUP { get; set; }
    }
}
