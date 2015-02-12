using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace IAS.Mockup
{
    public partial class LabelNewLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                NewLine();

            }
            else
            {
                NewLine();
            }
        }

        private void NewLine()
        {
            string[] strls = { "Line-1", "Line-2", "Line-3", "Line-4" };

            if (strls.Count() > 0)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < strls.Count(); i++)
                {
                    string dd = strls[i];
                    builder.Append(strls[i]);

                    if (i % 35 == 0)
                    {
                        lblNewLine.Text.Insert(i, strls[i]);
                    }
                    
                    //lblNewLine.Text.Insert(i, Environment.NewLine);
                }

                //lblNewLine.Text.Insert(0, "dd");
                //lblNewLine.Text.Insert(builder.ToString(), Environment.NewLine);
                
            }


        }
    }
}