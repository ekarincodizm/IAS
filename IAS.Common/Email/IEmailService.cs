using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;


namespace IAS.Common.Email
{
    public interface IEmailService
    {
        void SendMail(string from, string to, string subject, string body);

        void SendMail(string from, IEnumerable<String> tos, string subject, string body);

        void SendMail(string from, string to, string subject, string body, IEnumerable<FileInfo> attachFiles);

        void SendMail(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles);

        void SendMail(string from, string to, string subject, string body, IEnumerable<Attachment> attachFiles);

        void SendMail(System.Net.Mail.MailMessage mailMessage); 
    }
}
