using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class ReceiveLicenseHeader // : AG_IAS_IMPORT_HEADER_TEMP  //  AG_IAS_LICENSE_H_TEMP
    {
        public Int64 IMPORT_ID { get; set; } //	NUMBER	(	22	)
        public DateTime IMPORT_DATETIME { get; set; } //	TIMESTAMP(6)	(	11	)
        public 	String	FILE_NAME	 { get; set; } //	VARCHAR2	(	300	)
        public 	String	PETTITION_TYPE	 { get; set; } //	VARCHAR2	(	3	)
        public 	String	LICENSE_TYPE_CODE	 { get; set; } //	VARCHAR2	(	2	)
        public 	String	COMP_CODE	 { get; set; } //	VARCHAR2	(	4	)
        public 	String	COMP_NAME	 { get; set; } //	VARCHAR2	(	200	)
        public 	String	LICENSE_TYPE	 { get; set; } //	VARCHAR2	(	2	)
        public 	DateTime	SEND_DATE	 { get; set; } //	DATE	(	7	)
        public 	Decimal	TOTAL_AGENT	 { get; set; } //	NUMBER	(	22	)
        public 	Decimal	TOTAL_FEE	 { get; set; } //	NUMBER	(	22	)
        public 	String	ERR_MSG	 { get; set; } //	VARCHAR2	(	4000	)
        public 	String	APPROVE_COMPCODE	 { get; set; } //	VARCHAR2	(	4	)
    }
}
