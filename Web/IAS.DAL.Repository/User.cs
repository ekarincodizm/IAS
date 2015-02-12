using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class User
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASS { get; set; }
        public string USER_TYPE { get; set; }
        public string IS_ACTIVE { get; set; }
        public string USER_RIGHT { get; set; }
        public string USER_TERM_ACCEPTED { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
