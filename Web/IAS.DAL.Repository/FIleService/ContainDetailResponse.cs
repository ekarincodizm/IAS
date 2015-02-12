using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class ContainDetailResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public String Code { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Message { get; set; }

        [MessageBodyMember]
        public IList<String> Files { get; set; }
    }
}