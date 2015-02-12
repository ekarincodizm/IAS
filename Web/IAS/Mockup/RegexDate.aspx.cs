using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IAS.Mockup
{
    public partial class RegexDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //string Date = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            if (txtDate.Text == "")
            {
                return;
            }
            else
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(0[1-9]|[12][0-9]|3[01])[-|/](0[1-9]|1[012])[-|/](19|20|25)\d{2}$");
                if (regex.IsMatch(txtDate.Text))
                {
                    MessageBox.Show("Date is correct.");
                }
                else
                {
                    MessageBox.Show("Date is not correct.");
                }
            }

            //if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
            //{
            //    Date = Convert.ToString(DateTime.Now.AddYears(543).Year);
            //}
            //else
            //{
            //    Date = Convert.ToString(DateTime.Now.Year);
            //}

            //string Date = "December 8";
            //MatchCollection MC = Regex.Matches(Date, @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");

        }
    }
}