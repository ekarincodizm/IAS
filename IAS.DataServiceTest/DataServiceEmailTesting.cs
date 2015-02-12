using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Net.Mail;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class DataServiceEmailTesting
    {
        [TestMethod]
        public void TestMailServer()
        {
            try
            {


                SmtpClient smtp = new SmtpClient();
                MailMessage message = new MailMessage("kositc@oic.or.th", "kositc@oic.or.th", "My Message Subject", "This is a test message");
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
           

        }


        [TestMethod]
        public void TestGmailSendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("kositc@oic.or.th");
                mail.To.Add("kositc@oic.or.th");
                mail.Subject = "Test Mail - 1";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Write some HTML code here";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("tikclicker@gmail.com", "Tonhoam54");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

                Console.Out.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
          
        }
    }

}
