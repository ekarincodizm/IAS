using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IAS.DAL.Interfaces;

namespace IAS.DAL
{
    public class PersonEntities : IASPersonEntities, IIASPersonEntities
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["IASPersonEntities"].ConnectionString;

        public PersonEntities() : this(connStr){}

        public PersonEntities(string connectionString) : base(connectionString) { }


    }
}
