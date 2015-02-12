using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class AutoComplet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            //Widget w = MyEntityService.GetWidget(selectedWidgetID);
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtID.Text = compCode[1];
            txtNmae.Text = compCode[0];

        }
    }
}