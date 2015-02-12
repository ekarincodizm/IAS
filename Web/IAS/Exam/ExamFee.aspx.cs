using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Exam
{
    public partial class ExamFee : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {

            }
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            //var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            //var text = (Label)gr.FindControl("lblFileGv");

            //Session["ViewFileName"] = text.Text;

            //string url = "regViewDocument.aspx";
            //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}'); </script>", Page.ResolveUrl(url)));


        }

        protected void hplPrint_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {

        }
    }
}