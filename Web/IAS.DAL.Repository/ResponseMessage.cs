using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ResponseMessage<T> where T : struct
    {
        public T ResultMessage { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsError { get { return !string.IsNullOrEmpty(this.ErrorMsg); } }
        public string ConfigLicenseId { get; set; }
    }
}
