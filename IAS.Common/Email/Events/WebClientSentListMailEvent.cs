using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Domain.Events;
using System.Net.Mail;

namespace IAS.Common.Email.Events
{
    public class WebClientSentListMailEvent : IDomainEvent
    {
        public MailListMessageServiceContent MailMessage { get; set; }  
    }
}
