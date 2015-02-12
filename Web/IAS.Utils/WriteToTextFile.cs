using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class WriteToTextFile
    {
        public static void Write(String message, String filename = @"C:\logs\TestWriteText.txt") 
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
            {
                file.WriteLine(message);
            }
        }

        public static void Clear(String filename = @"C:\logs\TestWriteText.txt") {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
            {
                file.Write("", true);
            } 
        }
    }
}
