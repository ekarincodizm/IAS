using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Domain.Events
{
    public static class DomainEvents
    {
        static IDomainEventHandlerFactory _domainEventHandlerFactory = StructureMap.ObjectFactory.GetInstance < IDomainEventHandlerFactory>();

        public static IDomainEventHandlerFactory DomainEventHandlerFactory
        {
            get
            {
                return _domainEventHandlerFactory;
            }
            set { _domainEventHandlerFactory = value; }
        }



        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            DomainEventHandlerFactory.GetDomainEventHandlersFor(domainEvent)
                                                    .ForEach(h => h.Handle(domainEvent));
        }
    }

}
