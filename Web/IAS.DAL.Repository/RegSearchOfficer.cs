using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class RegSearchOfficer
    {
        public string MemberType { get; set; }
        public string status { get; set; }
        public string Idcard { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string CurrentPage { get; set; }
        public string txt_startDate { get; set; }
        public string txt_endDate { get; set; }
    }
}
