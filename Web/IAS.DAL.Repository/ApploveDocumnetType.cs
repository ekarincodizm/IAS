using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ApploveDocumnetType
    {
        public string APPROVE_DOC_TYPE { get; set; }
        public string APPROVE_DOC_NAME { get; set; }
        public string APPROVER { get; set; }
        public string DESCRIPTION { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string ITEM_VALUE { get; set; }
        public string ASSO_APPLOVE { get; set; }
    }
}
