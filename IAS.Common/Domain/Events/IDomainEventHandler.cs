using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Domain.Events
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }

}
