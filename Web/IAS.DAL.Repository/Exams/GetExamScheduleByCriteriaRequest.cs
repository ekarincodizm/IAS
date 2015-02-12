using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    [Serializable]
    public class GetExamScheduleByCriteriaRequest
    {
        public ExamCriteriaDTO ExamCriteria { get; set; }

        public PagingInfo PageInfo { get; set; }

        public String UserId { get; set; }
    }
}
