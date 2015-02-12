using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace IAS.Utils
{
    public class Email
    {
        public string LastError = "";
        public bool IsSSLEnabled = false;


        public static bool IsRightEmailFormat(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                  + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            return  reStrict.IsMatch(emailAddress);
        }

        public bool SendIndividualEmailsPerRecipient(string[] to, string subject, string message)
        {
            // Prepare a message
            MailMessage msgMail = new MailMessage();

            try
            {
                if (to != null)
                {
                    for (int iCount = 0; iCount < to.Length; iCount++)
                    {
                        msgMail.To.Add(new MailAddress(to[iCount]));
                    }
                }

                /*/
                if (null != strCCEmails)
                {
                    for (int iCount = 0; iCount < strCCEmails.Length; iCount++)
                        msgMail.CC.Add(new MailAddress(strCCEmails[iCount]));
                }
                /*/

                msgMail.Subject = subject;
                msgMail.IsBodyHtml = true;
                msgMail.Body = message;

                // Sending out an email
                return Send(msgMail);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public bool Send(MailMessage msgMail)
        {
            try
            {
                /*/
                var smtpcl = new SmtpClient(ConfigurationManager.AppSettings["MailServer"], int.Parse(ConfigurationManager.AppSettings["MailServerPort"]))
                {
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]),
                    EnableSsl = true
                };
                //msgMail.From = new MailAddress(ConfigurationManager.AppSettings["MailUserName"]);                
                /*/

                SmtpClient smtpcl = new SmtpClient();

                msgMail.BodyEncoding = Encoding.UTF8;
                //smtpcl.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                //smtpcl.UseDefaultCredentials = true;                
                //smtpcl.EnableSsl = IsSSLEnabled;
                smtpcl.Send(msgMail);

                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }
    }
}
