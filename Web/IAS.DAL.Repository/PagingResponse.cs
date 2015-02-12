using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IAS.DTO
{
    [Serializable]
    public class PagingResponse<T>
    {
        public PagingInfo PagingInfo { get; set; }
        public T DataResponse { get; set; }
        
    }
}
