using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class ErrorMsgNewLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if ((regPass.IsValid == true) || (regPass1.IsValid == true))
            {
                this.ucError.ShowMessageError = regPass.ErrorMessage;
                this.ucError.ShowModalError();
                return;
            }
            else
            {
                this.ucSuccess.ShowMessageSuccess = "regPass.IsValid == false";
                this.ucSuccess.ShowModalSuccess();

            }

        }
    }
}