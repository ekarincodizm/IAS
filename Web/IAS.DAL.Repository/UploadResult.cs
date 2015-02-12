using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class UploadResult<H, D>
        where H : class
        where D : class
    {
        public string GroupId { get; set; }
        public List<H> Header { get; set; }
        public List<D> Detail { get; set; }
    }

    [Serializable]
    public class UploadLicenseResult<H, D, A>
        where H : class
        where D : class
        where A : class
    {
        public string GroupId { get; set; }
        public List<H> Header { get; set; }
        public List<D> Detail { get; set; }
        public List<A> AttachFiles { get; set; }
    }
}
