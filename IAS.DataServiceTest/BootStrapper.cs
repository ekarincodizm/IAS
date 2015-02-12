//using IAS.Common.Authentication;
//using IAS.Common.CookieStorage;
//using IAS.Common.Domain.Events;
using IAS.Common.Logging;
//using IAS.Common.UnitOfWork;
using IAS.Common.Configuration;
using StructureMap;
using StructureMap.Configuration.DSL;
//using IAS.Common.Email;



namespace IAS.DataServiceTest
{
    public class BootStrapper
    {
        public static void ConfigureDependencies()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ControllerRegistry>();

            });
        }



        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {
                // Handlers for Domain Events
                //For<IDomainEventHandlerFactory>().Use<StructureMapDomainEventHandlerFactory>();


                //// Uncomment the line below to use the product service caching layer
                ////For<IProductCatalogueService>().Use<CachedProductCatalogueService>()
                ////    .CtorDependency<IProductCatalogueService>().Is(x => x.TheInstanceNamed("RealProductCatalogueService"));

                //// TruckQueue Service

                //For<ICookieStorageService>().Use<CookieStorageService>();


                // Application Settings                 
                For<IApplicationSettings>().Use<ApplicationSettings>();

                // Logger
                For<ILogger>().Use<Log4NetServiceAdapter>();
                //For<ILogger>().Use<Log4NetOICAdapter>();
                // Email Service                 
                //For<IEmailService>().Use<TextLoggingEmailService>();

                ////For<IUserService>().Use<wsUserService>();

                //// Authentication

                //For<IFormsAuthentication>().Use<AspFormsAuthentication>();
                //For<ILocalAuthenticationService>().Use<AspMembershipAuthentication>();


            }

        }
    }
}