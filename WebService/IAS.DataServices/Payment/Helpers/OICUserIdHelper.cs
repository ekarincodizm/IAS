using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Helpers
{
    public class OICUserIdHelper
    {
        public static String PhaseOICId(String oicid) {
            if (oicid.Length == 6)
            {
                return String.Format("{0}-{1}-{2}", oicid.Substring(0, 2), oicid.Substring(2, 1), oicid.Substring(3));
            }
            else {
                return oicid;
            }
        }
    }
}