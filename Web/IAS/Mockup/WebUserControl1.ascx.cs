using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public string GetDDL1
        { get { return this.ddl1.SelectedValue;}  }

        public string GetDDL2
        { get { return this.ddl2.SelectedValue; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataList();
            }
        }

        private void GetDataList()
        {
            var list = new List<DTO.DataItem>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new DTO.DataItem
                {
                    Id = i.ToString(),
                    Name = "Value " + i.ToString()
                });
            }



            ddl1.DataTextField = "Name";
            ddl1.DataValueField = "Id";
            ddl1.DataSource = list;
            ddl1.DataBind();

            ddl2.DataTextField = "Name";
            ddl2.DataValueField = "Id";
            ddl2.DataSource = list;
            ddl2.DataBind();
        }
    }
}