using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IAS.DTO
{
    [Serializable]
    public class AttachFile
    {
        public String ID { get; set; }
        public String AttechType { get; set; }
        public String FileName { get; set; }
        public String FileType { get; set; }
        public String FileSize { get; set; }
        public String TargetFileName {

            get {
                return ID + "_" + Convert.ToInt32(AttechType).ToString("000") + FileType;
            } 
            
            }
        public String TargetContainer { get; set; }
        public String TargetFullName { get; set; }



    }
}
