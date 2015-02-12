using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;
using System.Runtime.Serialization;

namespace IAS.DTO
{
    [Serializable]
    public class Applicant
    {
        public int APPLICANT_CODE { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public string TESTING_NO { get; set; }
        public string EXAM_PLACE_CODE { get; set; }
        public string ACCEPT_OFF_CODE { get; set; }
        public DateTime? APPLY_DATE { get; set; }
        public string ID_CARD_NO { get; set; }
        public string PRE_NAME_CODE { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string SEX { get; set; }
        public string EDUCATION_CODE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string AREA_CODE { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string ZIPCODE { get; set; }
        public string TELEPHONE { get; set; }
        public string AMOUNT_TRAN_NO { get; set; }
        public string PAYMENT_NO { get; set; }
        public string INSUR_COMP_CODE { get; set; }
        public string ABSENT_EXAM { get; set; }
        public string RESULT { get; set; }
        public DateTime? EXPIRE_DATE { get; set; }
        public string LICENSE { get; set; }
        public string CANCEL_REASON { get; set; }
        public string RECORD_STATUS { get; set; }
        public string USER_ID { get; set; }
        public DateTime? USER_DATE { get; set; }
        public string EXAM_STATUS { get; set; }

    }
}
