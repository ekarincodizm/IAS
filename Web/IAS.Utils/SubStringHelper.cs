using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class SubStringHelper
    {
        public static String RangeOf(String text, Int32 index, Int32 length)
        {
            return (text.Length > (index + length)) ? text.Substring(index, length) : "";
        }
    }
}
