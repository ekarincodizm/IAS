using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public  class ImportBankTransferRequest
    {
        public String GroupId { get; set; }
        public IEnumerable<ImportBankTransferData> ImportBankTransfers { get; set; }
        public String UserOicId { get; set; }
        public String UserId { get; set; }
    }

    public class ImportBankTransferData
    {
        public String Id { get; set; }
        public String Ref1 { get; set; }
        public Int32 Status { get; set; }
        public String ChangeRef1 { get; set; }
        public String ChangeAmount { get; set; }
    }
}
