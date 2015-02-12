using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices
{
    public class DBConnection
    {
        public static string GetConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager
                                           .ConnectionStrings["OraDB_Person"]
                                           .ConnectionString;
            }
        }
    }
}