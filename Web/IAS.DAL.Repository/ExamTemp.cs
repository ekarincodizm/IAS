using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{   
    [Serializable]
    public class ExamTemp
    {
        public DTO.UploadHeader Header { get; set; }

        public Int32? APPLICANT_CODE { get; set; }//number (6)
        public String TESTING_NO { get; set; } //varchar2(6)
        public String EXAM_PLACE_CODE { get; set; } //varchar2 (6)
        public string SUBJECT_CODE { get; set; } //var2 (3)
        public int? SCORE { get; set; }//number(3)
        public string LICENSE_TYPE_CODE {get;set;}//var2 (2)    
        public string USER_ID {get;set;} //var2 (15)
        public DateTime? USER_DATE { get; set; } //date
    }
}
