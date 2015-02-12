using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ResultValidateApplicant
    {
        public bool ResultMessage { get; set; }
        public string ValidateMessage { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsCanExam { get; set; }
        public bool IsError { get { return !string.IsNullOrEmpty(this.ErrorMsg); } }
    }
}
