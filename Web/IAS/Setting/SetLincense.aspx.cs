using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS.Setting
{
    public partial class SetLincense : basepage
    {
        ConfigBiz biz = new ConfigBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            GvLicense.DataSource = biz.GetLincse0304("").DataResponse.Tables[0];
            GvLicense.DataBind();
        }

        protected void GvLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl_select = (DropDownList)e.Row.FindControl("ddl_select");
                Label lbl_value = (Label)e.Row.FindControl("lbl_value");
                ddl_select.SelectedValue = lbl_value.Text;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (GridViewRow item in GvLicense.Rows)
            {
                Label lbl_Id = (Label)item.FindControl("lbl_Id");
                DropDownList ddl_select = (DropDownList)item.FindControl("ddl_select");
                dic.Add(lbl_Id.Text, ddl_select.SelectedValue);
            }
            var res = biz.AddLincse0304(dic);
            if (res.IsError)
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
            else
            {
                UCModalSuccess1.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                UCModalSuccess1.ShowModalSuccess();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}