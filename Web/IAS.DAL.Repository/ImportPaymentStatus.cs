using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DTO
{
    public enum ImportPaymentStatus
    {
        Loading = 0,
        Valid = 1,                     
        MissingRefNo = 2,
        Duplicate = 3,
        Paid = 4,
        Paylate = 5,              
        Invalid = 6,
        ChangeData = 7
        
    }
}
