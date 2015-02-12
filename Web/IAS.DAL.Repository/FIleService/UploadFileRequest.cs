using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class UploadFileRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public String TargetFileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String TargetContainer { get; set; }

        [MessageBodyMember(Order = 0)]
        public Stream FileStream { get; set; }
    }
}