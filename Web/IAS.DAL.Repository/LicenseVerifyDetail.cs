using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class LicenseVerifyDetail
    {
        public string PRE_NAME_CODE { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string ID_CARD_NO { get; set; }
        public DateTime? LICENSE_DATE { get; set; }
        public DateTime? LICENSE_EXPIRE_DATE { get; set; }
        public string LICENSE_NO { get; set; }
        public string EMAIL { get; set; }
        public string RENEW_TIMES { get; set; }
        public string OLD_COMP_CODE { get; set; }
        public DateTime? AR_DATE { get; set; }
        public decimal? FEES { get; set; }
        public string CURRENT_ADDRESS { get; set; }
        public string CURRENT_PROVINCE_CODE { get; set; }
        public string CURRENT_AMPUR_CODE { get; set; }
        public string CURRENT_TUMBON_CODE { get; set; }
        public string LOCAL_ADDRESS { get; set; }
        public string LOCAL_PROVINCE_CODE { get; set; }
        public string LOCAL_AMPUR_CODE { get; set; }
        public string LOCAL_TUMBON_CODE { get; set; }
        public string UPLOAD_GROUP_NO { get; set; }
        public string SEQ_NO { get; set; }
        public string TITLE_NAME { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public string APPROVED { get; set; }
        public string COMP_NAME { get; set; }//ดา
    }
}
