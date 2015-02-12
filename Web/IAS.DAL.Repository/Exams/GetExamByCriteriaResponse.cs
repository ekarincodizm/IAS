using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    public class GetExamByCriteriaResponse
    {
        public IEnumerable<DateTime> ExamShedules { get; set; }
    }
}
