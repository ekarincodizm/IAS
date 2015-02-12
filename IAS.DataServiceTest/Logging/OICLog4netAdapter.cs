using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Logging;

namespace IAS.DataServiceTest.Logging
{
    public class OICLog4netAdapter : Log4NetAdapter
    {
        public OICLog4netAdapter(Type callingType, String methodName)
            :base(callingType, methodName)
        {
            //log4net.LogicalThreadContext.Properties["functionName"] = _methodName;
        }
    }
}
