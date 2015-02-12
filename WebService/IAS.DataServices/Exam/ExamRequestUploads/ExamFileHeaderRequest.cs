using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DataServices.Exam.ExamRequestUploads 
{
    public class ExamFileHeaderRequest
    {
        public IAS.DAL.IASPersonEntities Context { get; set; }
        public DTO.UserProfile UserProfile { get; set; }
        public String FileName { get; set; }

        public String LineData { get; set; }        
    }
}
                                               