using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SubmitLicenseVerify
    {
        public string UploadGroupNo { get; set; }
        public string SeqNo { get; set; }
    }
}
