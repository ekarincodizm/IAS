using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace IAS.FileService.FileManager
{
    public interface IFileController
    {
        String TargetPath { get; set; }

        Stream StreamAttech { get; set; }

    
    }
}