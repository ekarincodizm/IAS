using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class UploadData
    {
        public string Header { get; set; }
        public List<string> Body { get; set; }
        public bool IsCSVFile { get; set; }
    }
}
