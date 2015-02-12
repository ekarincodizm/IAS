using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Domain.Events;
using IAS.Common.Email.Events;
using IAS.Common.Email;
using System.Runtime.InteropServices;
using IAS.Common.Logging;

namespace IAS.BLL.DomainEventHandlers
{
    public class WebClientSentMailHandler : IDomainEventHandler<WebClientSentMailEvent>
    {
        EmailSmtpService.EmailSmtpServiceClient svc;

        public WebClientSentMailHandler()
        {
            this.svc = new EmailSmtpService.EmailSmtpServiceClient();
        }
        public void Handle(WebClientSentMailEvent domainEvent)
        {
            MailMessageServiceContent mailMessage = domainEvent.MailMessage;
            if (mailMessage != null)
            {
                svc.SendMail(mailMessage.From, mailMessage.To, mailMessage.Subject, mailMessage.Body);
            }
            else {
                LoggerFactory.CreateLog().LogError("Cannot sent email.");
            }
            
        }
    }
}
