using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Data;
using System.Configuration;



namespace IAS.Utils
{
    public static class AlertMessage
    {
        public static void ShowAlertMessage(string title, string message)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page != null)
            {
                message = message.Replace("'", "\'");
                ScriptManager.RegisterStartupScript(page, page.GetType(), title, "alert('" + message + "');", true);
            }
        }
    }
}
