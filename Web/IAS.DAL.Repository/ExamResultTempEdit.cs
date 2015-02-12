using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ExamResultTempEdit
    {
        public DTO.ExamHeaderResultTemp Header { get; set; }
        public DTO.ExamResultTemp Detail { get; set; }
    }
}
