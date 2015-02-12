using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL.Interfaces;

namespace IAS.DAL
{
    public class DALFactory
    {

        public static IIASPersonEntities GetPersonContext()
        {
            return new PersonEntities(); 
        }

        public static IIASFinanceEntities GetFinanceContext()
        {
            return new FinanceEntities();
        }

        public static IASGBModelEntities GetGBContext()
        {
            return new IASGBModelEntities();
        }

    }
}
