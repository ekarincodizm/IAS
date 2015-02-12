using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using IAS.Common.Configuration;
using StructureMap;
using IAS.Common.Logging;
using IAS.Common.Email;
using IAS.Common.Validator;

namespace IAS.DataServices
{
    public class Global : System.Web.HttpApplication
    {
        //Can't get IAS.Common.Configuration solved
        protected void Application_Start(object sender, EventArgs e)
        {
            BootStrapper.ConfigureDependencies();

            ApplicationSettingsFactory.InitializeApplicationSettingsFactory
                    (ObjectFactory.GetInstance<IApplicationSettings>());

            LoggerFactory.SetCurrent(new Log4NetOICLogFactory());

            EmailServiceFactory.InitializeEmailServiceFactory
                            (ObjectFactory.GetInstance<IEmailService>());

            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            LoggerFactory.CreateLog().LogInfo("WebServiceStart Started");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
