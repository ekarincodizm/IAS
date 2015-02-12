using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils.Helpers
{
    public class GuidOracleHelper
    {
        public static String ConventGuidOracleToDotNet( String raw16) {
            byte[] bytes = ParseHex(raw16);
            Guid guid = new Guid(bytes);
            return guid.ToString("N").ToUpperInvariant();
        }
        public static String ConventGuidDotNetToOracle( String strGuid)    
        {
            Guid guid = new Guid(strGuid);
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", ""); 
        }
        static byte[] ParseHex(string text)
        {
            // Not the most efficient code in the world, but
            // it works...
            byte[] ret = new byte[text.Length / 2];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
            }
            return ret;
        }
    }
}
