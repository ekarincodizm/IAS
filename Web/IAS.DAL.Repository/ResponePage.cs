using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ResponePage<T> where T : class
    {
        public string ErrorMsg { get; set; }
        public T DataResponse { get; set; }
        public bool IsError { get { return !string.IsNullOrEmpty(this.ErrorMsg); } }
        public int CountRecord { get; set; }
        public int CountPage { get; set; }
    }
}
