using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Properties;


namespace IAS.Mockup
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        private string txt = Resources.errorWebForm6_001;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] strS = txt.Split(' ');
                //if (strS != null)
                //{
                //    for (int i = 0; i < strS.Count(); i++)
                //    {
                //       // txtStr.Text += "-  "+ strS[i] + Environment.NewLine ;
                        
                //        Label1.Text += strS[i] + Environment.NewLine+ "label";
                       
                //    }
                    

                //}

                Label1.Text = "Dsfsff" + Environment.NewLine + "sdfsdf";
            }
        }
    }
}