using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;

namespace IAS.DTO
{
    [Serializable]
    public class GBHoliday
    {
        public DateTime HL_DATE { get; set; }
        public string HL_DESC { get; set; }
        public decimal? COUNT { get; set; }
        public decimal? NUM { get; set; }
    }
}
