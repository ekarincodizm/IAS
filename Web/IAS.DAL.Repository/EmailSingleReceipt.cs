using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class EmailSingleReceipt
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string LicenseType { get; set; }
        public string totalMoney { get; set; }

    }
}
