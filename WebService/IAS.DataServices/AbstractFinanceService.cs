using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices
{
    public class AbstractFinanceService
    {
        protected IAS.DAL.Interfaces.IIASPersonEntities ctx;
        protected IAS.DAL.Interfaces.IIASFinanceEntities ctxFin;
        public AbstractFinanceService()
        {
            this.ctx = DAL.DALFactory.GetPersonContext();
            this.ctxFin = DAL.DALFactory.GetFinanceContext();
        }
    }
}