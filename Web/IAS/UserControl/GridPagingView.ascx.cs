using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class GridPagingView : System.Web.UI.UserControl
    {
        private String _name =  "GridPagingView1";
        private Int32 _pageSize = 10;
        public Int32 PageSize { 
            get { 
                return _pageSize; }
            set
            {
                ViewState[String.Format("{0}{1}", Name, "PageSize")] = value;
                _pageSize = Convert.ToInt32(ViewState[String.Format("{0}{1}", Name, "PageSize")]); 
            } 
        }
        public GridView GridData { get { return gvDataEnties; } set { gvDataEnties = value; } }
        public String Name { get; set; }

        public Object DataSource { set { gvDataEnties.DataSource = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _pageSize = 10;
                ViewState[String.Format("{0}{1}", Name, "PageSize")] = _pageSize.ToString();
            }
            else 
            {
                _pageSize = Convert.ToInt32( ViewState[String.Format("{0}{1}", Name, "PageSize")]);
            }
        }

       
    }
}