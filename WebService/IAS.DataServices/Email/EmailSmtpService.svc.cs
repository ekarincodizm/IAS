using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IAS.Common.Email;
using IAS.Common.Logging;
using System.ServiceModel.Activation;

namespace IAS.DataServices.Email
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailSmtpService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmailSmtpService : IEmailSmtpService
    {

        public void SendMail(string from, string to, string subject, string body)
        {
            try
            {
                EmailServiceFactory.GetEmailService().SendMail(from, to, subject, body);
            }
            catch (MailMessageException mailEx)
            {
                LoggerFactory.CreateLog().LogError("Terminal server cannot sent email.", mailEx);
                throw new FaultException(mailEx.Message);
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }
            
        }

        public void SendMailToList(string from, IEnumerable<string> tos, string subject, string body)
        {
            try
            {
                EmailServiceFactory.GetEmailService().SendMail(from, tos, subject, body);
            }
            catch (MailMessageException mailEx)
            {
                LoggerFactory.CreateLog().LogError("Terminal server cannot sent email.", mailEx);
                throw new FaultException(mailEx.Message);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SendMailWithAttachment(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles)
        {
            
            try
            {
                EmailServiceFactory.GetEmailService().SendMail(from, to, subject, body, attachFiles);
            }
            catch (MailMessageException mailEx)
            {
                LoggerFactory.CreateLog().LogError("Terminal server cannot sent email.", mailEx);
                throw new FaultException(mailEx.Message);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
