using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class AccountItem
    {
        public string AccountCode { get; set; }
        public string GLAccount { get; set; }
        public string SecretAccount { get; set; }
        public string Descriptions { get; set; }
        public double Amount { get; set; }
        public string UnitType { get; set; }
        public int UnitAmount { get; set; }
        public string RefAP { get; set; }
        public char Type { get; set; }
    }
}
