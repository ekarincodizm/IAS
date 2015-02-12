using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IAS.DTO
{
    [Serializable]
    public class ResponseService<T> where T : class
    {
        public string ErrorMsg { get; set; }
        public T DataResponse { get; set; }
        public bool IsError { get { return !string.IsNullOrEmpty(this.ErrorMsg); } }
    }
}
