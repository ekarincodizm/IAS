using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Utils;

namespace IAS.Setting
{
    public partial class SetExamGroup : basepage
    {
        GroupSubjectBiz biz = new GroupSubjectBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            GvExamGroup.DataSource = biz.GetSubjectGroupList("").DataResponse;
            GvExamGroup.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var validate = Validation.Validate(this, "a");
            if (validate != "")
            {
                UCModalError1.ShowMessageError = validate;
                UCModalError1.ShowModalError();
            }
            else
            {
                DTO.GroupSubject group = new DTO.GroupSubject();
                group.GROUP_NAME = txtGroupName.Text.Trim();
                group.EXAM_PASS = txtExamPass.Text.ToDecimal();
              
                var res = biz.AddSubjectGroup(group);
                if (res.ErrorMsg == null)
                {
                    txtExamPass.Text = string.Empty;
                    txtGroupName.Text = string.Empty;
                    BindGrid();
                    UCModalSuccess1.ShowMessageSuccess = "บันทีกข้อมูลสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
                else
                {
                    UCModalError1.ShowMessageError = res.ErrorMsg;
                    UCModalError1.ShowModalError();
                }
            }
        }

        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            txtExamPass.Text = string.Empty;
            txtGroupName.Text = string.Empty;
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            var res = biz.DeleteSubjectGroup(((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblId")).Text);
           if (res.ErrorMsg == null)
           {
               BindGrid();
               UCModalSuccess1.ShowMessageSuccess = "ลบข้อมูลสำเร็จ";
               UCModalSuccess1.ShowModalSuccess();
           }
           else
           {
               UCModalError1.ShowMessageError = "ลบข้อมูลไม่สำเร็จ";
               UCModalError1.ShowModalError();
           }
        }

        protected void lbtUpdate_Click(object sender, EventArgs e)
        {
            txtUpdateGroupName.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblName")).Text;
            txtUpdatePass.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblExamPass")).Text;
            HID.Value = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblId")).Text;
            ModalPopupUpdate.Show();           
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var validate = Validation.Validate(this, "b");
            if (validate != "")
            {
                UCModalError1.ShowMessageError = validate;
                UCModalError1.ShowModalError();
            }
            else
            {
                DTO.GroupSubject group = new DTO.GroupSubject();
                group.ID = HID.Value.ToDecimal();
                group.GROUP_NAME = txtUpdateGroupName.Text;
                group.EXAM_PASS = txtUpdatePass.Text.ToDecimal();
                group.STATUS = "A";
                var res = biz.UpdateSubjectGroup(group);
                if (res.ErrorMsg == null)
                {
                    BindGrid();
                    UCModalSuccess1.ShowMessageSuccess = "แก้ไขข้อมูลสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
                else
                {
                    UCModalError1.ShowMessageError = "แก้ไขข้อมูลไม่สำเร็จ";
                    UCModalError1.ShowModalError();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupUpdate.Hide();
        }
    }
}