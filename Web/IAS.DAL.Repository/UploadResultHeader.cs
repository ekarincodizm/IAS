using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class UploadResultHeader
    {
        public string UploadFileName { get; set; }
        public String FileName { get; set; }
        public int Totals { get; set; }
        public int RightTrans { get; set; }
        public int MissingTrans { get; set; }
        public string TestingNo { get; set; }
    }
}
