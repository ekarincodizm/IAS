using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class AmendFileResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public String Code { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Message { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Certificate { get; set; }
    }
}