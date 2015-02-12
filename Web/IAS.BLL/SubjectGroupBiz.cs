using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IAS.BLL
{
    public class SubjectGroupBiz
    {
        private ExamService.ExamServiceClient svc;
        public SubjectGroupBiz()
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

        public DTO.ResponePage<DTO.SubjectGroup[]> GetSubjectGroupSearch(string p,int page,int record)
        {
            return svc.GetSubjectGroupSearch(p,page,record);
        }


        public DTO.SubjectGroupD[] GetSubjectInGroup(string p)
        {
            return svc.GetSubjectInGroup(p).DataResponse;
        }

        public DTO.ResponseService<string> ActiveConditionGroup(string p, string license)
        {
            return svc.ActiveConditionGroup(p, license);
        }
    }
}
