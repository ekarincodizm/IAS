using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IAS.DAL.Interfaces;

namespace IAS.DAL
{
    public class FinanceEntities : IASFinanceEntities, IIASFinanceEntities
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["IASFinanceEntities"].ConnectionString;

        public FinanceEntities() : this(connStr){}

        public FinanceEntities(string connectionString) : base(connectionString) { }


    }
}
