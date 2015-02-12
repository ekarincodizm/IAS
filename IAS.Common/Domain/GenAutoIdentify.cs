using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Domain
{
    public static class GenAutoIdentify
    {
        public static String Create(){
            System.Threading.Thread.Sleep(5);
            string autoId = DateTime.Now.Year.ToString("0000").Substring(2, 2) +
                            DateTime.Now.ToString("MMddHHmmssfff");
            return autoId;
        }
        
    }
}
