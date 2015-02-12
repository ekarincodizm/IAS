using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class VerifyDocumentHeader //: DAL.AG_IAS_LICENSE_H
    {
        public String UPLOAD_GROUP_NO { get; set; } //	VARCHAR2	(	15	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String COMP_NAME { get; set; } //	VARCHAR2	(	200	)
        public DateTime TRAN_DATE { get; set; } //	DATE	(	7	)
        public Int32 LOTS { get; set; } //	NUMBER	(	22	)
        public Double MONEY { get; set; } //	NUMBER	(	22	)
        public String REQUEST_NO { get; set; } //	VARCHAR2	(	20	)
        public String PAYMENT_NO { get; set; } //	VARCHAR2	(	20	)
        public String FLAG_REQ { get; set; } //	VARCHAR2	(	1	)
        public String LICENSE_TYPE_CODE { get; set; } //	VARCHAR2	(	2	)
        public String FILENAME { get; set; } //	VARCHAR2	(	300	)
        public String PETITION_TYPE_CODE { get; set; } //	VARCHAR2	(	2	)
        public String UPLOAD_BY_SESSION { get; set; } //	VARCHAR2	(	15	)
        public String FLAG_LIC { get; set; } //	VARCHAR2	(	1	)
        public String APPROVE_COMPCODE { get; set; } //	VARCHAR2	(	4	)
        public String APPROVED_DOC { get; set; } //	VARCHAR2	(	1	)
        public DateTime APPROVED_DATE { get; set; } //	DATE	(	7	)
        public String APPROVED_BY { get; set; } //	VARCHAR2	(	20	)
    }
}
