using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class SubjectBiz
    {
        private ExamService.ExamServiceClient svc;
        public SubjectBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }
 

        public DTO.ResponseService<DTO.Subjectr[]> GetSubjectrList(string p)
        {           
            return svc.GetSubjectrList(p);
        }

        public DTO.ResponseService<string> AddSubject(DTO.Subjectr subject)
        {
           return svc.AddSubject(subject);
        }

        public DTO.ResponseService<string> UpdateSubject(DTO.Subjectr subject)
        {
            return svc.UpdateSubject(subject);
        }

        public DTO.ResponseService<string> DeleteSubject(DTO.Subjectr subject)
        {
            return svc.DeleteSubject(subject);
        }      
    }
}
