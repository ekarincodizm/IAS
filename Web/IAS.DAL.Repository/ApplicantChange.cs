using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantChange
    {

        public int CHANGE_ID { get; set; }
        public string COMP_CODE { get; set; }
        public string TESTING_NO { get; set; }
        public string OLD_ID_CARD_NO { get; set; }
        public decimal? OLD_PREFIX { get; set; }
        public string OLD_FNAME { get; set; }
        public string OLD_LNAME { get; set; }
        public string NEW_ID_CARD_NO { get; set; }
        public decimal? NEW_PREFIX { get; set; }
        public string NEW_FNAME { get; set; }
        public string NEW_LNAME { get; set; }
        public string REMARK { get; set; }
        public Int16 STATUS { get; set; }
        public string CREATE_BY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string ASSOCIATION_USER_ID { get; set; }
        public DateTime? ASSOCIATION_DATE {get; set;}
        public Int16 ASSOCIATION_RESULT {get; set;}
        public string OIC_USER_ID {get; set;}
        public DateTime? OIC_DATE {get; set;}
        public Int16 OIC_RESULT { get; set; }
        public string CANCEL_REASON { get; set; } 



        
    }
}
