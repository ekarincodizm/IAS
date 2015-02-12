using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class AccountDetail
    {
        public string ID { get; set; }
        public string MEMBER_TYPE { get; set; }
        public string ID_CARD_NO { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string EMAIL { get; set; }
        public string ACTIVE { get; set; }
        public string OTH_USER_TYPE { get; set; }
        public string DELETE_USER { get; set; }
        public string OTH_DELETE_USER { get; set; }
        public string COMP_CODE { get; set; }
        public string MEMBER_TYPE_NAME { get; set; }
        public string TITLE_NAME { get; set; }
    }
}
