using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using IAS.Utils;

namespace IAS.Payment
{
    public partial class GetReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["PostReceipt"] != null) {
                String[] result = CryptoBase64.Decryption(Request["PostReceipt"]).Split('|');

                if (result.Length == 3)
                {
                    ViewState["username"] = result[0];
                    ViewState["filepath"] = result[2];
                    txtUserName.Text = result[0];
                }
            }
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                BLL.PersonBiz biz = new BLL.PersonBiz();

                var res = biz.UserAuthen(txtUserName.Text, txtPassword.Text, false,"");
                if (res.IsError)
                {
                    UCModalPopupError.ShowMessageError = res.ErrorMsg;

                    UCModalPopupError.ShowModalError();
                    UpdatePanelForgetPassword.Update();
                    return;
                }
                else
                {
                    if(ViewState["filepath"]!=null){
                        Response.Redirect(String.Format("~/UserControl/ViewFile.aspx?targetImage={0}", CryptoBase64.Encryption(ViewState["filepath"].ToString())));
                    }
                    
                    return;
                }

            }
            else
            {
                UCModalPopupError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalPopupError.ShowModalError();
                UpdatePanelForgetPassword.Update();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }
    }
}