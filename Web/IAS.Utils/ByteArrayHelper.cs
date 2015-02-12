using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class ByteArrayHelper
    {
        public static string ConvertByteArrayToString(Byte[] ByteOutput)
        {

            string StringOutput = System.Text.Encoding.ASCII.GetString(ByteOutput);

            return StringOutput;

        }

        public static byte[] ConvertStringToByte(string Input)
        {
            return System.Text.Encoding.ASCII.GetBytes(Input);
        }

    }
}
