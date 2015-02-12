using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class LsApplicant
    {
        public string ExamNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ExamPlaceGroup { get; set; }
        public string ExamPlace { get; set; }
        public string Province { get; set; }
        public string Seat { get; set; }
        public string LicenseTypeName { get; set; }
        public string ExamFee { get; set; }
        public string AgentType { get; set; }
        public string ExamPlaceCode { get; set; }
        public string InSurCompCode { get; set; }
        public DateTime ApplyDate { get; set; }
        public string RUN_NO { get; set; }
    }
}
