using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS
{
    public class AttachFileIssueException : Exception
    {
        public AttachFileIssueException(String message)
            :base(message)
        {

        }
    }
}
