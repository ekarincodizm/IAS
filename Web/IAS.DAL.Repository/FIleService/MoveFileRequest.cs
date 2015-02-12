using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class MoveFileRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public String TargetFileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String TargetContainer { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String CurrentFileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String CurrentContainer { get; set; }
    }
}