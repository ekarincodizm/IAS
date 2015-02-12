using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigPaymentExpireDay
    {
        public string ID {get; set;}
        public string DESCRIPTION {get; set;}
        public int PAYMENT_EXPIRE_DAY { get; set; }
    }
}
