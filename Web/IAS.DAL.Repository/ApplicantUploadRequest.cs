using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantUploadRequest
    {
        public String FileName { get; set; }
        public DTO.UploadData UploadData { get; set; }
        public String TestingNo { get; set; }
        public String ExamPlaceCode { get; set; }
        public String TestTimeCode { get; set; }
        public String LicenseTypeCode { get; set; }
        public DateTime TestingDate { get; set; }
        public DTO.UserProfile UserProfile { get; set; }
    }
}
