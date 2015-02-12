using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class LocalTelephoneNumberHelper
    {
        public static String GetPhoneNumber(String phoneNumber) 
        {
            if(String.IsNullOrWhiteSpace(phoneNumber))
                return "";

            String[] strr = phoneNumber.Split('#');
            return strr[0];
        }
        public static String GetExtenNumber(String phoneNumber) {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                return "";

            String[] strr = phoneNumber.Split('#');
            return (strr.Length > 1)? strr[1]:"";
        }
    }
}
