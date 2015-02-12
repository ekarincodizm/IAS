using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Authentication
{
    public interface IExternalAuthenticationService
    {
        User GetUserDetailsFrom(string token);
    }
}
