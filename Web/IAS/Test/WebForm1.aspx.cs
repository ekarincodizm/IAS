using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Text;


namespace IAS.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Redirect("../Register/regSearchOfficerOIC.aspx");
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ToolkitScriptManager.RegisterStartupScript(Page, typeof(Page), "popup", "window.open('../Register/Reg_Co.aspx', '_blank')", true);
        }

        protected void btnPopup_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Register/Reg_Co.aspx", "_blank", "menubar=0,width=100,height=100");
            
            //Redirect(Response, "../Register/Reg_Co.aspx", "_blank", "popup");
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            string strDate = lblDate.Text;
            //string[] dateArray = dd.Replace(dd.Substring(0,1), @"/");
            var aStringBuilder = new StringBuilder(strDate);
            aStringBuilder.Insert(2, "/");
            aStringBuilder.Insert(5, "/");
            //aStringBuilder.Insert(2, "/");
            txtDate1.Text = aStringBuilder.ToString();
            txtDate2.Text = aStringBuilder.ToString();
            DateTime dates = Convert.ToDateTime(aStringBuilder.ToString());
            DateTime date1 = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", txtDate1.Text));
            DateTime date2 = Convert.ToDateTime(String.Format("{0:yyyy/mm/dd}", txtDate2.Text));
        }

        //public static string Replace(this string str, char oldchar, char newchar)
        //{
        //    var sb = new StringBuilder(str);
        //    sb.Replace(oldchar, newchar);
        //    return sb.ToString();
        //}


    }
}