using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class AssociationLicense
    {
        public string ASSOCIATION_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string ACTIVE { get; set; }

        //Optional
        public string ASSOCIATION_NAME { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }

    }
}
