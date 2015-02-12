using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class RegistrationApproveRequest
    {
        public String AppreResult { get; set; } 
        public String UserId { get; set; }
        public String MemberType { get; set; }
        public IEnumerable<String> ListId { get; set; } 
    }
}
