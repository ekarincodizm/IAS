using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DAL.Interfaces
{
    public interface IDALFactory
    {

        IIASPersonEntities GetPersonContext();

        FinanceEntities GetFinanceContext();

        IASGBModelEntities GetGBContext();
    }
}
