using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class GetReistrationByCriteriaRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string IdCard { get; set; }
        public string MemberTypeCode { get; set; }
        public string Email { get; set; }
        public string CompCode { get; set; }
        public string Status { get; set; }
        public int PageNo { get; set; }
        public int RecordPerPage { get; set; }
        public string Para { get; set; }
    }
}
