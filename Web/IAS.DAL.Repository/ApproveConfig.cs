using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApproveConfig
    {
        public string ID { get; set; }
        public string ITEM { get; set; }
        public string ITEM_VALUE { get; set; }
        public string DESCRIPTION { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string KEYWORD { get; set; }
    }
}
