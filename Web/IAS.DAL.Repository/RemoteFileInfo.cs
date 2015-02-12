using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class RemoteFileInfo : IDisposable
    {
        public string TargetFolder { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public System.IO.Stream FileByteStream { get; set; }
        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}
