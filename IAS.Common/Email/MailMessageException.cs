using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Email
{
    public class MailMessageException : Exception
    {
        public MailMessageException(string message)
            : base(message) 
        {

        }
    }
}
