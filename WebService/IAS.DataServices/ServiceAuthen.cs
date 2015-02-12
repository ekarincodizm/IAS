using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.IdentityModel.Selectors;
using IAS.DataServices.Properties;

namespace IAS.DataServices
{
    public class ServiceAuthen : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (!(userName == "test" && password == "test"))
            {
                throw new FaultException(Resources.errorServiceAuthen_001);
            }
        }
    }
}