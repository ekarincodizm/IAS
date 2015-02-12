using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class SummaryReceiveApplicant
    {
        public String UploadGroupNo { get; set; }

        public  DTO.UploadHeader Header { get; set; }

        public IEnumerable<DTO.ApplicantTemp> ReceiveApplicantDetails { get; set; }
        public String MessageError { get; set; }  
    }
}
                                      