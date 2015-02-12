using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class GetByFirstLastNameRequest
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
