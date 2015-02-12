using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantNoPay : Applicant
    {
        public string TITLE_NAME { get; set; }
        public string EDUCATION_NAME { get; set; }
        public DateTime TESTING_DATE { get; set; }
    }
}
