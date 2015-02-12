using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class CompressFileDetail
    {
        public string FileName { get { return (String.IsNullOrEmpty(TextFilePath)?"": TextFilePath.Substring(TextFilePath.LastIndexOf('.'))); } }
        public string TextFilePath { get; set; }
        public string ExtFile { get; set; }
        public bool IsCSVFile { get { return this.ExtFile.ToUpper() == ".CSV"; } }

        public List<AttachFileDetail> AttatchFiles { get; set; }
    }

    [Serializable]
    public class AttachFileDetail
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FullFileName { get; set; }
        public string MapFileName { get; set; }
        public string FileTypeCode { get; set; }
        public string FileType { get; set; }
        public string FileGroupID { get; set; }
        public string Status { get; set; }
        public String ErrMsg { get; set; }
    }
}
