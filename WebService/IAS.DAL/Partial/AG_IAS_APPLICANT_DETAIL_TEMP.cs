using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IAS.DAL
{
    public partial class AG_IAS_APPLICANT_DETAIL_TEMP
    {
        DateTime? _testingDate;
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> TESTING_DATE
        {
            get
            {
                return _testingDate;
            }
            set
            {
                _testingDate = value;
            }
        }
    }
}
