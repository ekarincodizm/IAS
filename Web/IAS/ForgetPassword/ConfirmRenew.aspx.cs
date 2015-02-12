using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.ForgetPassword
{
    public partial class ConfirmRenew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["renew"] != null)
                {
                    String _renew = Request.QueryString["renew"].ToString();

                    String decrty = Utils.CryptoBase64.Decryption(_renew);

                    String[] results = decrty.Split('|');

                    if (results.Length == 7)
                    {
                        String username = results[0];
                        String oldpass = results[2];
                        String newpass = results[4];
                        String email = results[6];
                        BLL.PersonBiz personBiz = new BLL.PersonBiz();

                        var res = personBiz.RenewPassword(username, email, oldpass, newpass);

                        if (res.IsError)
                        {

                        }


                        Response.Redirect(PageList.Home);
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect(PageList.Home);
            }
           
        }
    }
}