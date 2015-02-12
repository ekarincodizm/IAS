using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IAS.DAL
{
    public class GBEntities : IASGBModelEntities
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["IASGBModelEntities"].ConnectionString;
        
        public GBEntities() : this(connStr){}

        public GBEntities(string connectionString) : base(connectionString) { }
    }
}
