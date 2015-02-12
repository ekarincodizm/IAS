using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public static class GetDataInArrayOfIndexHelper
    {
        public static String GetIndexOf(this String[] source, Int32 index) {
            if (source.Length > index)
            {
                return source[index];
            }
            else return "";
        }
    }
}
