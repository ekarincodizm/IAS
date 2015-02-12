using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class PersonalHistory
    {
        public string FLNAME { get; set; }
        public string SEX { get; set; }
        public string ID_CARD_NO { get; set; }
        public string NATIONALITY { get; set; }
        public string EDUCATION_NAME { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ZIPCODE { get; set; }
        public string TELEPHONE { get; set; }
        public string PROVINCE { get; set; }
        public string AMPUR { get; set; }
        public string TAMBON { get; set; }
        public string LOCAL_ADDRESS1 { get; set; }
        public string LOCAL_ZIPCODE { get; set; }
        public string LOCAL_TELEPHONE { get; set; }
        public string LOCAL_PROVINCE { get; set; }
        public string LOCAL_AMPUR { get; set; }
        public string LOCAL_TAMBON { get; set; }
    }
}
