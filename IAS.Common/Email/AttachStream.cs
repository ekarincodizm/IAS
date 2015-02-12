using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace IAS.Common.Email
{
     [MessageContract]
    public class AttachStream
    {
        [MessageHeader(MustUnderstand = true)]
        public String FileName { get; set; }
        [MessageBodyMember(Order = 0)]
        public Stream FileStream { get; set; }
    }
}
