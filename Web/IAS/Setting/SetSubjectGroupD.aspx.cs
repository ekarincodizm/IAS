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
    public partial class SetSubjectGroup : basepage
    {
        SubjectGroupBiz biz = new SubjectGroupBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataCenterBiz bizCenter = new DataCenterBiz();
                ddlType.DataSource = bizCenter.GetLicenseType("--เลือกประเภทใบอนุญาต--").DataResponse;
                ddlType.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "")
            {
                gvSearch.DataBind();
                GvGroup.DataBind();
                fieldListSubject.Visible = false;
                fieldCourse.Visible = false;
            }
            else
            {
                GvGroup.DataSource = biz.GetSubjectGroup(ddlType.SelectedValue);
                GvGroup.DataBind();

                gvSearch.DataSource = biz.GetSubjectGroupSearch(ddlType.SelectedValue,1,100).DataResponse;
                gvSearch.DataBind();

                fieldListSubject.Visible = true;
                fieldCourse.Visible = true;
            }
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
                DTO.ConditionGroup conditiongroup = new DTO.ConditionGroup();
                List<DTO.Subjectr> subject = new List<DTO.Subjectr>();
                conditiongroup.LICENSE_TYPE_CODE = ddlType.SelectedValue;
                conditiongroup.NOTE = txtNote.Text;              
                conditiongroup.START_DATE = Convert.ToDateTime(txtStartDate.Text);
                conditiongroup.USER_ID = base.UserId;
                conditiongroup.USER_DATE = DateTime.Now;

                if (check.Checked)
                {
                    conditiongroup.STATUS = "A";
                }
                else
                {
                    conditiongroup.STATUS = "I";
                }


                if (txtEndDate.Text != "")
                {
                    conditiongroup.END_DATE = Convert.ToDateTime(txtEndDate.Text);
                    if (conditiongroup.START_DATE > conditiongroup.END_DATE)
                    {
                        UCModalError1.ShowMessageError = "วันที่มีผลบังคับใช้ต้องมากกว่าถึงวันที่";
                        UCModalError1.ShowModalError();
                        return;
                    }
                }
                else
                {
                    conditiongroup.END_DATE = null;
                }

                foreach (GridViewRow item in GvGroup.Rows)
                {
                    subject.Add(new DTO.Subjectr
                    {
                        LICENSE_TYPE_CODE = ddlType.SelectedValue,
                        SUBJECT_CODE = ((Label)(item.FindControl("lblSubjectCode"))).Text,
                        MAX_SCORE = ((Label)(item.FindControl("lblMaxScore"))).Text.ToShort(),
                        GROUP_ID = ((Label)(item.FindControl("lblGroup"))).Text.ToDecimal()
                    });
                }
                conditiongroup.Subject = subject;
                var res = biz.AddExamGroup(conditiongroup);
                if (res.ErrorMsg == null)
                {
                    txtEndDate.Text = string.Empty;
                    txtStartDate.Text = string.Empty;
                    check.Checked = false;
                    UCModalSuccess1.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess1.ShowModalSuccess();
                    gvSearch.DataSource = biz.GetSubjectGroupSearch(ddlType.SelectedValue,1,100).DataResponse;
                    gvSearch.DataBind();

                    txtNote.Text = "";
                    fieldSubject.Visible = false;
                }
                else
                {
                    if (res.ErrorMsg == "1")
                    {
                        UCModalError1.ShowMessageError = "วันที่มีผลบังคับใช้ต้องมากกว่าหรือเท่ากับหลักสูตรล่าสุด";
                        UCModalError1.ShowModalError();
                    }
                    else if (res.ErrorMsg == "2")
                    {
                        UCModalError1.ShowMessageError = "วันที่มีผลบังคับใช้ต้องน้อยกว่าหรือเท่าถึงวันที่ของหลักสูตรล่าสุด";
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                }
            }

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "")
            {
                UCModalError1.ShowMessageError = "กรุณาเลือกประเภทใบอนุญาต";
                UCModalError1.ShowModalError();
            }
            else
            {
                fieldSubject.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            fieldSubject.Visible = false;
            txtEndDate.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtStartDate.Text = string.Empty;
        }

        protected void btnSaveCourse_Click(object sender, EventArgs e)
        {
            if (hidNewCourse.Value == "")
            {
                UCModalError1.ShowMessageError = "คุณยังไม่ได้เลือกหลักสูตรใหม่";
                UCModalError1.ShowModalError();
            }
            else if (hidNewCourse.Value == hidOldCourse.Value)
            {
                UCModalError1.ShowMessageError = "หลักสูตรที่คุณเลือกได้ถูกใช้อยู่แล้ว";
                UCModalError1.ShowModalError();
            }
            else
            {
                var res = biz.ActiveConditionGroup(hidNewCourse.Value, ddlType.SelectedValue);
                if (res.ErrorMsg == null)
                {
                    UCModalSuccess1.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
                else
                {
                    UCModalError1.ShowMessageError = res.ErrorMsg;
                    UCModalError1.ShowModalError();
                }
            }
        }

        protected void check_CheckedChanged(object sender, EventArgs e)
        {

            GridViewRow rowchek = (GridViewRow)((CheckBox)sender).Parent.Parent;
            Label lblidcheck = (Label)rowchek.FindControl("lblCourseId");
            if (hidNewCourse.Value == lblidcheck.Text)
            {
                ((CheckBox)sender).Checked = true;
            }
            else
            {
                hidNewCourse.Value = lblidcheck.Text;

                foreach (GridViewRow rows in gvSearch.Rows)
                {
                    CheckBox ck = (CheckBox)rows.FindControl("check");
                    Label lblidnewcheck = (Label)rows.FindControl("lblCourseId");
                    Label lblStatus = (Label)rows.FindControl("lblStatus");
                    if (lblidcheck.Text == lblidnewcheck.Text)
                    {
                        ck.Checked = true;
                        lblStatus.Text = "ใช้งาน";
                    }
                    else
                    {
                        ck.Checked = false;
                        lblStatus.Text = "ไม่ใช้งาน";
                    }
                }
            }
        }

        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                CheckBox ck = (CheckBox)e.Row.FindControl("check");
                if (lblStatus.Text == "A")
                {
                    Label lblcourseid = (Label)e.Row.FindControl("lblCourseId");
                    hidOldCourse.Value = lblcourseid.Text;
                    hidNewCourse.Value = lblcourseid.Text;
                    ck.Checked = true;
                    lblStatus.Text = "ใช้งาน";
                }
                else
                {
                    lblStatus.Text = "ไม่ใช้งาน";
                    ck.Checked = false;
                }
            }
        }
    }
}