using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using IAS.Common.Helpers;

namespace IAS.Common.Email
{
    public class AttachmentFactory
    {
        public static Attachment CreateAttachFrom(Stream fileStream, String fileName) 
        {
            if (fileStream != null && fileStream.Length > 0)
            {
                return new Attachment(fileStream, fileName, ContentTypeHelper.MimeType(fileName));
            }
            else {
                throw new AttachmentFileIsNotFoundException("Attachfile stream missing.");
            }
        }

        public static Attachment CreateAttachFrom(String filePath) {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
                return new Attachment(file.FullName);
            else
                throw new AttachmentFileIsNotFoundException("Attachfile stream missing.");
        }

        public static Attachment CreateAttachFrom(FileInfo file)
        {
            if (file.Exists)
                return new Attachment(file.FullName);
            else
                throw new AttachmentFileIsNotFoundException("Attachfile stream missing.");
        }
    }
}
