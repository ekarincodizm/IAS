using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Repository
{
    [Serializable]
    public class StatisticResetPassword
    {
        public string USER_NAME { get; set; }
        public string MEMBER_NAME { get; set; }
        public string FLNAME { get; set; }
        public string ID_CARD_NO { get; set; }
        public int RESET_TIMES { get; set; }
    }
}