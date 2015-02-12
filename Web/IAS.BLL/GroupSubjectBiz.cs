using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class GroupSubjectBiz
    {
        ExamService.ExamServiceClient svc;
        public GroupSubjectBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }
        public DTO.ResponseService<DTO.GroupSubject[]> GetSubjectGroupList(string p)
        {
            return svc.GetSubjectGroupList(p);
        }

        public DTO.ResponseService<string> AddSubjectGroup(DTO.GroupSubject examgroup)
        {
            return svc.AddSubjectGroup(examgroup);
        }

        public DTO.ResponseService<string> DeleteSubjectGroup(string p)
        {
            return svc.DeleteSubjectGroup(p);
        }

        public DTO.ResponseService<string> UpdateSubjectGroup(DTO.GroupSubject examgroup)
        {
            return svc.UpdateSubjectGroup(examgroup);
        }
    }
}
