using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class Bill
    {
        public int No { get; set; }
        public int RegDetailId { get; set; }
        public string DocNo { get; set; }
        public string Name { get; set; }
        public DateTime DocDate { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public decimal Price { get; set; }
        public int RegId { get; set; }
        public int TrainId { get; set; }
    }
}
