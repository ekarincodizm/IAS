using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ValidateLicense
    {
        public decimal ID { get; set; }
        public string ITEM { get; set; }
        public Int32 ITEM_GROUP { get; set; }
        public string STATUS { get; set; }


    }
}
