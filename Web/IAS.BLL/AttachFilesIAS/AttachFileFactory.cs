using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS
{
    public class AttachFileFactory
    {
        public static AttachFile ConcreateAttachFile() 
        {
            return new AttachFile();
        }
    }
}
