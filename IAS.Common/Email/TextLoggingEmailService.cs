using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Logging;
using System.IO;
using System.Net.Mail;

namespace IAS.Common.Email
{
    public class TextLoggingEmailService : IEmailService
    {
        public void SendMail(string from, string to, string subject, string body)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", to));
            email.AppendLine(String.Format("From: {0}", from));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<FileInfo> attachFiles)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", to));
            email.AppendLine(String.Format("From: {0}", from));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            foreach (FileInfo item in attachFiles)
            {
                email.AppendLine(String.Format("Attach: '{0}' Size: '{1}' ", item.FullName, item.Length)); 
            }

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<Attachment> attachFiles)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", to));
            email.AppendLine(String.Format("From: {0}", from));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            foreach (Attachment item in attachFiles)
            {
                email.AppendLine(String.Format("Attach: '{0}' Size: '{1}' ", item.Name, item.ContentStream.Length));
            }

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }
        public void SendMail(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", to));
            email.AppendLine(String.Format("From: {0}", from));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            foreach (AttachStream item in attachFiles)
            {
                email.AppendLine(String.Format("Attach: '{0}' Size: '{1}' ", item.FileName, item.FileStream.Length));
            }

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }

        public void SendMail(string from, IEnumerable<string> tos, string subject, string body)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", String.Join<String>(";", tos)));
            email.AppendLine(String.Format("From: {0}", from));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }


        public void SendMail(MailMessage mailMessage)
        {
            StringBuilder email = new StringBuilder();

            email.AppendLine(String.Format("To: {0}", mailMessage.To.Select(a => a.Address).ToArray()));
            email.AppendLine(String.Format("From: {0}", mailMessage.From));
            email.AppendLine(String.Format("Subject: {0}", mailMessage.Subject));
            email.AppendLine(String.Format("Body: {0}", mailMessage.Body));

            foreach (Attachment item in mailMessage.Attachments)
            {
                email.AppendLine(String.Format("Attach: '{0}' Size: '{1}' ", item.Name, item.ContentStream.Length));
            }

            LoggerFactory.CreateLog().LogInfo(email.ToString());
        }



    }

}
