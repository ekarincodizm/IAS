using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Email
{
    public class AttachmentFileIsNotFoundException : Exception
    {
        public AttachmentFileIsNotFoundException(string message)
            : base(message)
        {

        }
    }
}
