using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantHeaderRequest
    {
        public  IAS.DAL.Interfaces.IIASPersonEntities Context { get; set; }
        public DTO.UserProfile UserProfile { get; set; }
        public String FileName { get; set; }
        public String TestingNumber { get; set; }
        public String ExamPlaceCode { get; set; }
        public String[] LineData { get; set; } 
    }                                              
}
