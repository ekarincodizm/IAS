using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Logging.Services
{
    [Serializable]
    public class LogServiceMessage
    {
        public DateTime TransDate { get; set; }
        public Int32 LogLevel { get; set; }
        public String LogingName { get; set; }
        public String OICUserId { get; set; }
        public String DeptCode { get; set; }
        public String CompanyCode { get; set; }
        public String SystemCode { get; set; }
        public String SubSystemCode { get; set; }
        public String PrgId { get; set; }
        public String IpAddress { get; set; }
        public String LogHeader { get; set; }
        public String Detail { get; set; }
        public String LogException { get; set; }

        public DateTime CreateDate { get; set; }
        public String CreateBy { get; set; }
    }
}
