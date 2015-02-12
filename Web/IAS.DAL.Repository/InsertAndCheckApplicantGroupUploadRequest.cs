using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class InsertAndCheckApplicantGroupUploadRequest
    {
        public String FilePath { get; set; } 
        public String FileName { get; set; }
        public RegistrationType RegistrationType { get; set; }
        public String TestingNo { get; set; }
        public String ExamPlaceCode { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
