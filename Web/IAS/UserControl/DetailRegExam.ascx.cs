using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class DetailRegExam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }


        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetTitle(int id)
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationalityById(message);
            //BindToDDL(ddlTitleName, ls);

            //**รอข้อมูล Tob 8/3/20556**//
            //string code = ls.FirstOrDefault(w => w.Name == "ไทย").Id;
            //ddlTitleName.SelectedValue = code;
            //**รอข้อมูล Tob 8/3/20556**//

        }

        private void GetEducation(string id)
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducationById(message);
            //BindToDDL(ddlTitleName, ls);

            //**รอข้อมูล Tob 8/3/20556**//
            //string code = ls.FirstOrDefault(w => w.Name == "ไทย").Id;
            //ddlTitleName.SelectedValue = code;
            //**รอข้อมูล Tob 8/3/20556**//
        }
    }
}