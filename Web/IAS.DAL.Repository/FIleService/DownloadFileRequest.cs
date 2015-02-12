using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class DownloadFileRequest
    {
         [MessageHeader(MustUnderstand = true)]
        public String TargetFileName { get; set; }

         [MessageHeader(MustUnderstand = true)]
        public String TargetContainer { get; set; }
         
    }
}