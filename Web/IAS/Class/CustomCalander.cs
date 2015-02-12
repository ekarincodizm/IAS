using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class CustomCalander : System.Web.UI.WebControls.Calendar
    {
        public Boolean IsCanRender { get; set; }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
  
            base.Render(writer);
        }
    }
}