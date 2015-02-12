using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class TopCompanyMoveOut
    {
        public string COMP_CODE_OLD { get; set; }
        public string NAME_OLD { get; set; }
        public Int32 COUNT_OUT { get; set; }
        public string COMP_CODE_IN { get; set; }
        public string NAME_OUT { get; set; }
        public Int32 COUNT_IN { get; set; } 
    }
}
