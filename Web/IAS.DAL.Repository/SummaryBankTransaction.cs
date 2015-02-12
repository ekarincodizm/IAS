using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class SummaryBankTransaction
    {
        public DateTime UploadDate { get; set; }
        public string FileName { get; set; }
        public int NumberOfItems { get; set; }
        public int NumberOfValid { get; set; }
        public int NumberOfInValid { get; set; }
        public decimal Total { get; set; }
        public String ErrMessage { get; set; }

    }
}
