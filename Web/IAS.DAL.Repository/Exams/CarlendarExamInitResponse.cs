using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO.Exams
{
    public class CarlendarExamInitResponse
    {
        public IEnumerable<DTO.DataItem> LicenseTypes { get; set; }
        public IEnumerable<DTO.DataItem> ExamTimes { get; set; }
        public IEnumerable<DTO.DataItem> ExamPlaceGroups { get; set; }
        public IEnumerable<DTO.DataItem> ExamPlaces { get; set; }
    }
}
                                    