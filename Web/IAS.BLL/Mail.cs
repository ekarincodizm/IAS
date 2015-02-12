using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace IAS.BLL
{
    public class Mail
    {
        public static void Send(List<string> mailTo, string mailSubject, string mailMessage) //, string imagePath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mailTo.ForEach(m => { mail.To.Add(new MailAddress(m)); });
                mail.Subject = mailSubject.Replace('\r', ' ').Replace('\n', ' '); ;
                mail.Body = mailMessage;
                mail.IsBodyHtml = true;
                //AttachedImage(mail, mailMessage, imagePath);
                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private static void AttachedImage(MailMessage mail, string htmlContent, string imagePath)
        {
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(htmlContent, null, "text/html");
            LinkedResource img1 = new LinkedResource(imagePath);
            img1.ContentId = "pic1";
            htmlBody.LinkedResources.Add(img1);
            mail.AlternateViews.Add(htmlBody);
        }

        public static string GetBodyMsg()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<img src='cid:pic1' />");
            sb.Append("<p>");
            sb.Append("</p>");
            sb.Append("<hr />");
            sb.Append("<h2></h2>");
            sb.Append("<hr />");
            return sb.ToString();
        }

    }
}
