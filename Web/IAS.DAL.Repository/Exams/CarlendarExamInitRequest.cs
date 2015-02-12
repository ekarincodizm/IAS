using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    public class CarlendarExamInitRequest
    {
        public String UserId { get; set; }

        public String FirstItemLicenseType { get; set; }
        public String FirstItemExamTime { get; set; }
        public String FirstItemExamPlaceGroup { get; set; }
        public String FirstItemExamPlace { get; set; }

    }                                        
}
