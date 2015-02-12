using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class HistoryPersonal
    {
        public string TRANS_ID{ get; set; }
        public string ID { get; set; }
        public string MEMBER_TYPE { get; set; }
        public string ID_CARD_NO { get; set; }
        public string EMPLOYEE_NO { get; set; }
        public string PRE_NAME_CODE { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string NATIONALITY { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public string SEX { get; set; }
        public string EDUCATION_CODE { get; set; }
        public string EDUCATION_NAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string AREA_CODE { get; set; }
        public string PROVINCE_CODE { get; set; }
        public string ZIP_CODE { get; set; }
        public string TELEPHONE { get; set; }
        public string LOCAL_ADDRESS1 { get; set; }
        public string LOCAL_ADDRESS2 { get; set; }
        public string LOCAL_AREA_CODE { get; set; }
        public string LOCAL_PROVINCE_CODE { get; set; }
        public string LOCAL_PROVINCE { get; set; }  
        public string LOCAL_ZIPCODE { get; set; }
        public string LOCAL_TELEPHONE { get; set; }
        public string EMAIL { get; set; }
        public string STATUS { get; set; }
        public string TUMBON_CODE { get; set; }
        public string LOCAL_TUMBON_CODE { get; set; }
        public string LOCAL_TUMBON { get; set; }
        public string LOCAL_AMPUR { get; set; }
        public string COMP_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string PROVINCE { get; set; }
        public string AMPUR { get; set; }
        public string TAMBON { get; set; }
    }
}
