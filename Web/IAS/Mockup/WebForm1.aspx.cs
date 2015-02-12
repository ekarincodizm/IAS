using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace IAS.Mockup
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CutDate2 = "56";
            string licenseTypeC = "03";

            StringBuilder strls = new StringBuilder();
            strls.Append(CutDate2);
            strls.Append(licenseTypeC);
            string dd = strls.ToString();

            //string V_Str = strls[0].ToString();
            string V_Str = CutDate2 + licenseTypeC;


            int total = 10000;
            lbl.Text = String.Format("{0:n}", total);

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            lbl.Text = "Value 1 is => " + uc1.GetDDL1;
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            lbl.Text = "Value 1 is => " + uc1.GetDDL2;
        }
    }
}