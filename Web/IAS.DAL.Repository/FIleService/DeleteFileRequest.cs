using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
     
     [MessageContract]
    public class DeleteFileRequest
    {
        [MessageHeader(MustUnderstand=true)]
        public String TargetFileName { get; set; }

    }
}