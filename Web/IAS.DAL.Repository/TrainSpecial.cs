using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class TrainSpecial
    {
        public string SPECIAL_TYPE_CODE { get; set; }
        public string SPECIAL_TYPE_DESC { get; set; }
        public string USED_TYPE { get; set; }
    }
}
