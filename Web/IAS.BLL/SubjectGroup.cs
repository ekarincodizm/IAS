using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class SubjectGroup
    {
        private ExamService.ExamServiceClient svc;
        public SubjectGroup()
        {
            svc = new ExamService.ExamServiceClient();
        }

        public DTO.Subjectr[] GetSubjectGroup(string p)
        {
            return svc.GetSubjectGroup(p).DataResponse;        
        }

        public DTO.ResponseService<string> AddExamGroup(DTO.ConditionGroup conditiongroup)
        {
            return svc.AddExamGroup(conditiongroup);
        }
    }
}
