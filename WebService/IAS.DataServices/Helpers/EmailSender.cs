using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using IAS.Common.Email;

namespace IAS.DataServices.Helpers
{
    public class EmailSender
    {
        private static MailMessage _mailMessage; 
        public static String Footer
        { 
            get {
                StringBuilder footer = new StringBuilder("");
                footer.AppendLine(Environment.NewLine );
                footer.AppendLine("ด้วยความเคารพ<br/>");
                footer.AppendLine("สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย (คปภ.) <br/>");
                footer.AppendLine("โทร: xxxxxxxxxx <br/>");
                footer.AppendLine("<a href='amsadmin@oic.or.th'>amsadmin@oic.or.th</a><br/> ");
                
                return footer.ToString(); 
            } 
        }
        public static String Header
        {
            get {
                StringBuilder header = new StringBuilder("");
                header.AppendLine(""+ Environment.NewLine);
                return "";
            } 
        }
        public static String FromEmail
        {
            get { return ConfigurationManager.AppSettings["EmailOut"].ToString(); }
        }

        private static String CombineBody(String userMessage) {
            return String.Format("{0}{1}{2}", Header, userMessage, Footer);
        }

        public static MailMessage Sending(StringBuilder emailBody, string emailAddress, string emailSubject = "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย")
        {

                    _mailMessage = new MailMessage(FromEmail, emailAddress)
                                                        { 
                                                            IsBodyHtml = true, 
                                                            Subject = emailSubject, 
                                                            Body = CombineBody(emailBody.ToString()) 
                                                        };
                    return _mailMessage;
        }
        public static MailMessage Sending(StringBuilder emailBody, string emailAddress, IEnumerable<FileInfo> attachs, string emailSubject = "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย")
        {

            _mailMessage = new MailMessage(FromEmail, emailAddress)
            {
                IsBodyHtml = true,
                Subject = emailSubject,
                Body = CombineBody(emailBody.ToString())
            };

            if (attachs.Count() > 0) {
                foreach (FileInfo item in attachs)
                {
                    AddAttachFile(item);
                }
            }

            return _mailMessage;
        }
        public static void AddAttachFile(FileInfo fileInfo) {
            if (fileInfo.Exists) 
            {
                Attachment attach = new Attachment(fileInfo.FullName);
                _mailMessage.Attachments.Add(attach);
            }
        }

    }


    public static class SenderMessage
    {
        public static Boolean Sent(this MailMessage mailMessage)
        {
            try
            {
                using (SmtpClient SmtpServer = new SmtpClient())
                {
                    //if (mailMessage != null)
                    //    SmtpServer.Send(mailMessage);
                    //else
                    //    return false;
                    EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}
