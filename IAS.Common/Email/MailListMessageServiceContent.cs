using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace IAS.Common.Email
{
    [MessageContract]
    public class MailListMessageServiceContent
    {
        [MessageHeader(MustUnderstand = true)]
        public String From { get; set; }
        [MessageHeader(MustUnderstand = true)]
        public IEnumerable<String> Tos { get; set; } 
        [MessageHeader(MustUnderstand = true)]
        public String Subject { get; set; }
        [MessageHeader(MustUnderstand = true)]
        public String Body { get; set; }
        [MessageBodyMember(Order = 0)]
        public IEnumerable<AttachStream> AttachFiles { get; set; }
    }
}
