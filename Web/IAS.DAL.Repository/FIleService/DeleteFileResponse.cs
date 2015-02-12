using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class DeleteFileResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public String Code { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Message { get; set; }
    }
}