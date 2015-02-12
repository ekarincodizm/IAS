using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace IAS.Common.Logging.Services
{
    [ServiceContract]
    public interface IServiceLogger
    {
        [OperationContract]
        void NewLog(LogServiceMessage message);
    }
}
