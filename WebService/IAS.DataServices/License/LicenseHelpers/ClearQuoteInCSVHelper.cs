using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class ClearQuoteInCSVHelper
    {
        public static string ClearQuoteInCSV(string anyStr)
        {
            char[] aryChar = anyStr.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int i = 0;
            bool foundQoute = false;
            while (i < aryChar.Length)
            {
                char curChar = aryChar[i];
                if (curChar != '"' && !foundQoute)
                {
                    sb.Append(curChar);
                }
                else if (curChar == '"' && !foundQoute)
                {
                    foundQoute = true;
                }
                else if (curChar == ',' && foundQoute)
                {

                }
                else if (curChar == '"' && foundQoute)
                {
                    foundQoute = false;
                }
                else
                {
                    sb.Append(curChar);
                }

                i++;
            }
            return sb.ToString();
        }
    }
}