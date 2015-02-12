using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace IAS.UserControl
{
    public partial class Register : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            //this.txtBirthDay.Text = DateTime.Today.ToString("dd MMMM yyyy"); 
        }
    }
}