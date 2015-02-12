using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class TestAutoCom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAssociation();
            }
        }

        private void GetAssociation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var list = biz.GetCompanyCodeByName("");
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            hdf.Value = jsonString;
        }
    }
}