using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.PersonalIAS
{
    public class PersonalIssueException : Exception
    {
        public PersonalIssueException(String message)
            :base(message)
        {

        }
    }
}
