using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class UploadFileResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public String Code { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Message { get; set; }

       [MessageHeader(MustUnderstand = true)]
        public String TargetFileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String TargetContainer { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Certificate { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String TargetFullName { get; set; }
    }
} 