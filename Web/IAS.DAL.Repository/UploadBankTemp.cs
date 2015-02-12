using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class UploadBankTemp
    {
        public List<SummaryBankTransaction> Summary { get; set; }
        public List<BankTransaction> Transactions { get; set; }
    }
}
