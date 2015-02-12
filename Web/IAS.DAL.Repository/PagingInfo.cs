using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class PagingInfo
    {
        public Int32 TotalItems { get; set; }
        public Int32 ItemsPerPage { get; set; }
        public Int32 CurrentPage { get; set; }

        public Int32 TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }
}
