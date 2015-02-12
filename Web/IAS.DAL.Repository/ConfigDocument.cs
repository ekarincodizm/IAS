using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    //    [Serializable]
    //    public class ConfigDocument : DAL.AG_IAS_DOCUMENT_TYPE_T
    //    {
    //    }


    public class ConfigDocument
    {
        public string DOCUMENT_NAME { get; set; }
        public string MEMBER_TYPE_CODE { get; set; }
        public decimal ID { get; set; }
        public string FUNCTION_ID { get; set; }
        public string MEMBER_CODE { get; set; }
        public string DOCUMENT_CODE { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string PETITION_TYPE_CODE { get; set; }
        public string DOCUMENT_REQUIRE { get; set; }
        public string STATUS { get; set; }
        public string MEMBER_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }  
        public Boolean IS_REQUIRE { get { return (DOCUMENT_REQUIRE == "Y") ? true : false; } }
        //public string  { get; set; }
    }
}
