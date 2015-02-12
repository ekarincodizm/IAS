using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class OICActiveDirectoryBiz
    {
        ADService.ADServiceAuthenClient svc;

        public OICActiveDirectoryBiz()
        {
            svc = new ADService.ADServiceAuthenClient();
        }

        public ADService.LoginResult OICAuthentication(string UsrName, string Password)
        {
            var res = new ADService.LoginResult();
            try
            {
                res = svc.Login(UsrName, Password);
            }
            catch (Exception ex)
            {

                throw;
            }

            return res;

        }

    }
}
