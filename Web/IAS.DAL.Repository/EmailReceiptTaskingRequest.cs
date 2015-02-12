using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IAS.DTO
{
    public class EmailReceiptTaskingRequest
    {
        public String FullName { get; set; }
        public String IDCard { get; set; }
        public String Email { get; set; }
        public FileInfo Receipt { get; set; }
        public String PettionTypeName { get; set; }
        public string ReciveNo { get; set; }
    }
}
