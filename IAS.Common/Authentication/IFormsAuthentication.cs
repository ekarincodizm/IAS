using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Authentication
{
    public interface IFormsAuthentication
    {
       void SetAuthenticationToken(string username);
       string GetAuthenticationToken();
       void SignOut();
    }                

}
