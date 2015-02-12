using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using IAS.Common.Logging;
using System.IO;

namespace IAS.Common.Email
{
    public class MockSMTPService : IEmailService
    {
        private String ReplaceMailTo(String to) {
            return "iasoic.assoc.artester@gmail.com";
        }

        public void SendMail(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage(from, ReplaceMailTo(to));
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();
                                                                    
            smtp.Send(message);

            IEmailService mailLog = new TextLoggingEmailService();
            mailLog.SendMail(from, to, subject, body);
       
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<FileInfo> attachFiles)
        {
            MailMessage message = new MailMessage(from, ReplaceMailTo(to));
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();

            foreach (FileInfo item in attachFiles)
            {
                try
                {
                    Attachment attach = AttachmentFactory.CreateAttachFrom(item);
                    message.Attachments.Add(attach);
                }
                catch (AttachmentFileIsNotFoundException ex)
                {
                    LoggerFactory.CreateLog().LogError("Cannot AttachFile", ex);
                }
               
            }

            smtp.Send(message);

            IEmailService mailLog = new TextLoggingEmailService();
            mailLog.SendMail(from, to, subject, body, attachFiles);
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles)
        {
            MailMessage message = new MailMessage(from, ReplaceMailTo(to));
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();

            foreach (AttachStream item in attachFiles)
            {
                try
                {
                    Attachment attach = AttachmentFactory.CreateAttachFrom(item.FileStream, item.FileName);
                    message.Attachments.Add(attach);
                }
                catch (AttachmentFileIsNotFoundException ex)
                {
                    LoggerFactory.CreateLog().LogError("Cannot AttachFile", ex);
                }

            }

            smtp.Send(message);

            IEmailService mailLog = new TextLoggingEmailService();
            mailLog.SendMail(from, to, subject, body, message.Attachments);
        }


        public void SendMail(string from, IEnumerable<string> tos, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.IsBodyHtml = true;
            message.From = new MailAddress(from);

            foreach (String item in tos)
            {
                message.To.Add(new MailAddress(ReplaceMailTo(item)));
            }

            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();

            smtp.Send(message);

            IEmailService mailLog = new TextLoggingEmailService();
            mailLog.SendMail(from, tos, subject, body);
        }


        public void SendMail(MailMessage mailMessage)
        {
            SmtpClient smtp = new SmtpClient();

            IList<MailAddress> mailAddresses = new List<MailAddress>();
            foreach (MailAddress item in mailMessage.To)
            {
                mailAddresses.Add(new MailAddress(ReplaceMailTo(item.Address)));
            }
            mailMessage.To.Clear();
            foreach (MailAddress item in mailAddresses)
            {
                mailMessage.To.Add(item);
            }
            smtp.Send(mailMessage);

            IEmailService mailLog = new TextLoggingEmailService();
            if (mailMessage.Attachments.Count > 0)
            {
                mailLog.SendMail(mailMessage.From.Address, String.Join(";", mailMessage.To.Select(a => a.Address).ToArray()), mailMessage.Subject, mailMessage.Body, mailMessage.Attachments);
            }
            else
            {
                mailLog.SendMail(mailMessage.From.Address, String.Join(";", mailMessage.To.Select(a => a.Address).ToArray()), mailMessage.Subject, mailMessage.Body);
            }
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<Attachment> attachFiles)
        {
            MailMessage message = new MailMessage(from, ReplaceMailTo( to));
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient();

            foreach (Attachment item in attachFiles)
            {
                message.Attachments.Add(item);

            }

            smtp.Send(message);

            IEmailService mailLog = new TextLoggingEmailService();
            mailLog.SendMail(from, to, subject, body, message.Attachments);
        }
    }

}
