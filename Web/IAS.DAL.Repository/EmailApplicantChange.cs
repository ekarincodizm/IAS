using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class EmailApplicantChange
    {
        public String FullNameOld { get; set; }
        public String FullNameNew { get; set; }
        public String OLDIDCard { get; set; }

        public String NewIDCard { get; set; }
        public String Email { get; set; }
        public String CancelReason { get; set; }
        public Int32 status { get; set; }
        public Int16? Asso { get; set; }
        public Int16? OIC { get; set; }
        public String TestingNo { get; set; }
    }
}
