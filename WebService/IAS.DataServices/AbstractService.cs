using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Linq;

namespace IAS.DataServices
{
    public class AbstractService 
    {
        protected IAS.DAL.Interfaces.IIASPersonEntities ctx;
        public AbstractService()
        {
   
            this.ctx = DAL.DALFactory.GetPersonContext();
        }
        public AbstractService(IAS.DAL.Interfaces.IIASPersonEntities _ctx)
        {

            ctx = _ctx;
        }

        protected IEnumerable<T> PersistEntities<T>()  
        {
            return ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Select(obj => obj.Entity).OfType<T>();
        }
    }
}