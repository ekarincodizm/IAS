using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigAssociation
    {
        public string ASSOCIATION_CODE {get; set;}
        public string ASSOCIATION_NAME {get; set;}
        public string USER_ID {get; set;}
        public DateTime? USER_DATE {get; set;}
        public string UPDATED_BY {get; set;}
        public DateTime? UPDATED_DATE {get; set;}
        public string ACTIVE {get; set;}
        public string COMP_TYPE {get; set;}
        public string AGENT_TYPE { get; set; }
    }
}
