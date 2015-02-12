using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ReplacementReport
    {
        public long COUNTCOMP { get; set; }
        public string COMP_CODE { get; set; }
        public string NAME { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public DateTime OIC_APPROVED_DATE { get; set; }
        public string REPLACENAME { get; set; }
        public long COUNTTYPE { get; set; }
        public string LICENSE_TYPE_CODE2 { get; set; }
        public float FORSHARE { get; set; }
    }
}
