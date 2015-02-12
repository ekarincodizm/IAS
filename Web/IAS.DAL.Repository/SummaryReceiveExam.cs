using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class SummaryReceiveExam
    {
        public String Identity { get; set; }

        public DTO.UploadHeader Header { get; set; }

        public IEnumerable<DTO.ExamTemp> ReceiveExam { get; set; }
        public String MessageError { get; set; }  
    }
}
