using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public interface IBaseEntity<TId>
    {
        IEnumerable<BusinessRule> GetBrokenRules();
    }
}
