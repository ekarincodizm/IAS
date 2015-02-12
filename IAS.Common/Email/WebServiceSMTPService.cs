using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAS.Common.Domain.Events;
using IAS.Common.Email.Events;
using System.Net.Mail;

namespace IAS.Common.Email
{
    public class WebServiceSMTPService : IEmailService
    {
        public void SendMail(string from, string to, string subject, string body)
        {
            MailMessageServiceContent mailMessage = new MailMessageServiceContent() { From = from, To = to, Subject = subject, Body = body, AttachFiles = new List<AttachStream>() };
            DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = mailMessage });
        }

        public void SendMail(string from, string to, string subject, string body, IEnumerable<FileInfo> attachFiles)
        {
            MailMessageServiceContent mailMessage = new MailMessageServiceContent() { From = from, To = to, Subject = subject, Body = body };
            IList<AttachStream> attachStreams = new List<AttachStream>();

            foreach (FileInfo item in attachFiles)
            {
                if (item.Exists) {
                    AttachStream attachStream = new AttachStream();
                    attachStream.FileName = item.Name;
                    attachStream.FileStream = item.Create();
                    attachStreams.Add(attachStream);
                }
                
            }

            mailMessage.AttachFiles = attachStreams;

            DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = mailMessage });
        }

        public void SendMail(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles)
        {
            MailMessageServiceContent mailMessage = new MailMessageServiceContent() { From = from, To = to, Subject = subject, Body = body, AttachFiles = attachFiles };

            DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = mailMessage });
        }


        public void SendMail(string from, IEnumerable<string> tos, string subject, string body)
        {
            MailListMessageServiceContent mailMessage = new MailListMessageServiceContent() { From = from, Tos = tos, Subject = subject, Body = body, AttachFiles = new List<AttachStream>() };
            DomainEvents.Raise(new WebClientSentListMailEvent() { MailMessage = mailMessage });
        }


        public void SendMail(string from, string to, string subject, string body, IEnumerable<System.Net.Mail.Attachment> attachFiles)
        {
            IList<AttachStream> attachStreams = new List<AttachStream>();
            foreach (var item in attachFiles)
            {
                 attachStreams.Add(new AttachStream(){ FileName=item.Name, FileStream = item.ContentStream});
            }
            MailMessageServiceContent mailMessage = new MailMessageServiceContent() { From = from, To = to, Subject = subject, Body = body, AttachFiles = attachStreams };

            DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = mailMessage });
        }

        public void SendMail(MailMessage mailMessage)
        {
            if (mailMessage.Attachments != null && mailMessage.Attachments.Count > 0)
            {
                IList<AttachStream> attachStreams = new List<AttachStream>();

                foreach (var item in mailMessage.Attachments)
                {
                    attachStreams.Add(new AttachStream() { FileName = item.Name, FileStream = item.ContentStream });
                }
                MailMessageServiceContent sentmailMessage = new MailMessageServiceContent() { From = mailMessage.From.Address, To = String.Join(";", mailMessage.To.Select(a => a.Address).ToArray())
                            , Subject = mailMessage.Subject, Body = mailMessage.Body, AttachFiles = attachStreams };

                DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = sentmailMessage });
            }
            else {
                IList<AttachStream> attachStreams = new List<AttachStream>();

                MailMessageServiceContent sentmailMessage = new MailMessageServiceContent()
                {
                    From = mailMessage.From.Address,
                    To = String.Join(";", mailMessage.To.Select(a => a.Address).ToArray())
                    ,
                    Subject = mailMessage.Subject,
                    Body = mailMessage.Body
                };

                DomainEvents.Raise(new WebClientSentMailEvent() { MailMessage = sentmailMessage });
            }  
          
        }
    }
}
