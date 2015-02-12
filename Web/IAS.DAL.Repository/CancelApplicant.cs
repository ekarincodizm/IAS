using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class CancelApplicant
    {
        public int ApplicantCode { get; set; }
        public string TestingNo { get; set; }
        public string ExamPlaceCode { get; set; }
    }
}
