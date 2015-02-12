using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace IAS.Common.Authentication
{
    public class AspFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthenticationToken(string username)
        {
            FormsAuthentication.SetAuthCookie(username, false);
        }

        public string GetAuthenticationToken()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

}
