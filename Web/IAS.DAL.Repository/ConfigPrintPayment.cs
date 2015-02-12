using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigPrintPayment
    {
        public string Id { get; set; }
        public string ITEM_VALUE { get; set; }
        public string GROUP_CODE { get; set; }
        public string USER_ID { get; set; }
    }
}
