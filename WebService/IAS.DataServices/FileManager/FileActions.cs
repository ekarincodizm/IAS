using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.FileManager
{
    public enum FileActions
    {
        DownloadFile=1,
        UploadFile=2,
        MoveFile=3,
        DeleteFile=4,
        AppendFile=5,
        ContainDetail=6
    }
}