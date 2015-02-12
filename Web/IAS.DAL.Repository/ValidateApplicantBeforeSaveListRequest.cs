using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ValidateApplicantBeforeSaveListRequest
    {
        public String TestingNo { get; set; }
        public String IdCard { get; set; }
        public DateTime TestingDate { get; set; }
        public String TestTimeCode { get; set; }
        public String ExamPlaceCode { get; set; }
        public String Time { get; set; }
        public IEnumerable<AddApplicant> AddApplicants { get; set; }
    }
}
