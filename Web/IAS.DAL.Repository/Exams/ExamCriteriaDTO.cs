using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    [Serializable]
    public class ExamCriteriaDTO
    {
        public String ExamPlaceGroupCode { get; set; }
        public String ExamPlaceCode { get; set; }

        public String LicenseTypeCode { get; set; }  
        public String AgentType { get; set; }

        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
        public Int32 Day { get; set; }

        public String TimeCode { get; set; }

        public String OwnerCode { get; set; }

    }
}
