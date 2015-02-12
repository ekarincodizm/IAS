using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace IAS.DTO.FileService
{
    
    [MessageContract]
    public class DownloadFileResponse : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public String Code { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String Message { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String FileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public Double Length { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String ContentType { get; set; }

        [MessageBodyMember(Order=0)]
        public Stream FileByteStream { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public String HashCode { get; set; } 

        public void Dispose()
        {
            if (FileByteStream != null) {
                FileByteStream.Close();
                FileByteStream.Dispose();
            }
        }
    }
}