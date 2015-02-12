using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigExtraEntity
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Item_Value { get; set; }
        public string GROUP_CODE { get; set; }
    }
}
