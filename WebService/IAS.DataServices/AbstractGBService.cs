using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices
{
    public class AbstractGBService
    {
        protected IAS.DAL.IASGBModelEntities ctx;

        public AbstractGBService()
        {
            this.ctx = DAL.DALFactory.GetGBContext();
        }
    }
}