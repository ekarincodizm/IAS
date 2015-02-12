using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class ApproverDoctype 
    {
        public String APPROVE_DOC_TYPE { get; set; } //	VARCHAR2	(	2	)
        public String APPROVE_DOC_NAME { get; set; } //	VARCHAR2	(	200	)
        public String APPROVER { get; set; } //	VARCHAR2	(	4	)
        public String DESCRIPTION { get; set; } //	VARCHAR2	(	200	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? UPDATED_DATE { get; set; } //	DATE	(	7	)
        public String ITEM_VALUE { get; set; } //	VARCHAR2	(	2	)
    }
}
