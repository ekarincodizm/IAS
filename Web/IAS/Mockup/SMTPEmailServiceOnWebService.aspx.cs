using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Common.Email;
using StructureMap;

namespace IAS.Mockup
{
    public partial class SMTPEmailServiceOnWebService : System.Web.UI.Page
    {
        public SMTPEmailServiceOnWebService()
        {
         
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                ObjectFactory.BuildUp(this);
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            EmailServiceFactory.GetEmailService().SendMail("tikclicker@gmail.com", "tikclicker@hotmail.com", "TestSendMail", "Hello World!!!!");
            //AttachStream attachFile = new AttachStream(){ FileName="ddd", FileStream=
            //EmailServiceFactory.GetEmailService().SendMail(
        }
    }
}