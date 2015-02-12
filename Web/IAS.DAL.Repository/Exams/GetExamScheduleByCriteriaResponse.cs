using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    public class GetExamScheduleByCriteriaResponse
    {
        public IEnumerable<ExamInfoDTO> ExamInfos { get; set; }
        public PagingInfo PageInfo { get; set; }
    }
}
