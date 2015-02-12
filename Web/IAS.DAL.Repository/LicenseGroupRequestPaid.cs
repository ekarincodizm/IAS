using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class LicenseGroupRequestPaid
    {
        public IEnumerable<DateTime> ImportedDates { get; set; }  
    }
}
