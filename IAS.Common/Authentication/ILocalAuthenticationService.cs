using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication
{
    public interface ILocalAuthenticationService
    {
        User Login(string username, string password);
        User RegisterUser(string username, string email, string password);

        User RegisterUser(string username, string password, ProfileProperties profileProperties);
    }

}
