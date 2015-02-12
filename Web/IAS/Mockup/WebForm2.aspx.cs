using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }


        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            IsValidEmail(txtEmail.Text);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.InnerException);

                return false;
            }


        }

        
    }
}