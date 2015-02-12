using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class PaymentNotCompleteResponse
    {
       public IEnumerable<DTO.BankTransaction> BankTransaction { get; set; } 
    }
}
