using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.Common.Email;

namespace IAS.DataServices.Email
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailSmtpService" in both code and config file together.
    [ServiceContract]
    public interface IEmailSmtpService
    {
        [OperationContract]
        void SendMail(string from, string to, string subject, string body);
        [OperationContract]
        void SendMailWithAttachment(string from, string to, string subject, string body, IEnumerable<AttachStream> attachFiles);
        [OperationContract]
        void SendMailToList(string from, IEnumerable<string> tos, string subject, string body);

    }
}
