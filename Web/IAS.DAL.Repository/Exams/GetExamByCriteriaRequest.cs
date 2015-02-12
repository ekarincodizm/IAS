using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    public class GetExamByCriteriaRequest
    {
        public String LicenseTypeCode { get; set; }
        public String ExamPlaceGroupCode { get; set; }
        public String ExamPlaceCode { get; set; }
        
        public Int32 Month { get; set; }
        public Int32 Year { get; set; }
        public String TimeCode { get; set; }
        public String TestingDate { get; set; }
        public String Owner { get; set; }
    }
}                     
