using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestBarCode
{
    public partial class SenderTextBoxToBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            string qstr = "Barcodes.aspx?code=" + txt1.Text;

            btn1.Attributes.Add("onclick", string.Format("window.open ('{0}', 'popupwindow', 'width=795,height=495,scrollbars,resizable=0'); return false", qstr));
        }
    }
}