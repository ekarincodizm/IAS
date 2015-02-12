using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class Payment12TEntity
    {
        public string IdCard { get; set; }
        public string petition_type_code { get; set; }
        public string SSeqNo { get; set; }
        public string upGroup { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string licenseT { get; set; }
        public string requestNo { get; set; }
        public string payment_no { get; set; }
        public string receiptNo { get; set; }
        public string area { get; set; }
        public string selectLicense { get; set; }
        public DateTime? LicenseExpireDate { get; set; }
        public string oldComp { get; set; }
        public string ComCode { get; set; }

        public int renewtimes { get; set; }
        public string ApproverUserId { get; set; }


    }
}
