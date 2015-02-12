using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.Helpers
{
    public class GenerateIdHelper
    {
        //Auto Generate Id
        public static string GetGenAutoId()
        {
            System.Threading.Thread.Sleep(5);
            string autoId = DateTime.Now.Year.ToString("0000").Substring(2, 2) +
                            DateTime.Now.ToString("MMddHHmmssfff");
            return autoId;
        }
    }
}
