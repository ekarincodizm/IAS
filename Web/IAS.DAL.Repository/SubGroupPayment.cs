using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SubGroupPayment
    {
        public string PaymentType { get; set; }
        public int ApplicantCode { get; set; }
        public string TestingNo { get; set; }
        public string ExamPlaceCode { get; set; }
        public string LicenseNo { get; set; }
        public string RenewTime { get; set; }
        public string uploadG { get; set; }
        public string seqNo { get; set; }

        public string RUN_NO { get; set; }
    }
}
