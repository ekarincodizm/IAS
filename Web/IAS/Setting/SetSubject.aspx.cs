using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Utils;
using IAS.Properties;

namespace IAS.Setting
{
    public partial class SetSubject : basepage
    {
        SubjectBiz biz = new SubjectBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataCenterBiz bizCenter = new DataCenterBiz();
                ddlType.DataSource = bizCenter.GetLicenseType("--เลือกประเภทใบอนุญาต--").DataResponse;
                ddlType.DataBind();

                ddlGroup.DataSource = bizCenter.GetSubjectGroup("--เลือกกลุ่ม--").DataResponse;
                ddlGroup.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "")
            {
                panSubject.Visible = false;
                panGrid.Visible = false;
            }
            else
            {
                BinGrid();
                ClearText();
            }
        }

        private void BinGrid()
        {
            var list = biz.GetSubjectrList(ddlType.SelectedValue);
            GvSubjectR.DataSource = list.DataResponse;
            GvSubjectR.DataBind();
            panGrid.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "บันทึก")
            {
                string validate = Validation.Validate(Page, "a");
                if (validate != "")
                {
                    UCModalError1.ShowMessageError = validate;
                    UCModalError1.ShowModalError();
                }
                else
                {
                    DTO.Subjectr subject = new DTO.Subjectr();
                    subject.LICENSE_TYPE_CODE = ddlType.SelectedValue;
                    subject.SUBJECT_NAME = txtSubjectName.Text;
                    subject.SUBJECT_CODE = txtSubjectCode.Text;
                    subject.GROUP_ID = ddlGroup.SelectedValue.ToShort();
                   // subject.EXAM_PASS = txtPass.Text.ToShort();
                    subject.MAX_SCORE = txtMaxScore.Text.ToShort();
                    subject.USER_ID = base.UserId;
                    var res = biz.AddSubject(subject);
                    if (res.ErrorMsg != null)
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        BinGrid();
                        UCModalSuccess1.ShowMessageSuccess = Resources.infoSetSubject_001;
                        UCModalSuccess1.ShowModalSuccess();
                        txtSubjectCode.Text = "";
                        txtSubjectName.Text = "";
                        txtMaxScore.Text = "";
                      //  txtPass.Text = "";
                        ddlGroup.SelectedValue = "";
                    }
                }
            }
            else //แก้ไขข้อมูล
            {
                string validate = Validation.Validate(Page, "a");
                if (validate != "")
                {
                    UCModalError1.ShowMessageError = validate;
                    UCModalError1.ShowModalError();
                }
                else
                {
                    DTO.Subjectr subject = new DTO.Subjectr();
                    subject.LICENSE_TYPE_CODE = ddlType.SelectedValue;
                    subject.SUBJECT_NAME = txtSubjectName.Text;
                    subject.SUBJECT_CODE = LabelSubjectId.Text;
                    subject.MAX_SCORE = txtMaxScore.Text.ToShort();
                   // subject.EXAM_PASS = txtPass.Text.ToShort();
                   // subject.GROUP_ID = ddlGroup.SelectedValue.ToShort();

                    var res = biz.UpdateSubject(subject);
                    if (res.ErrorMsg != null)
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        BinGrid();
                        UCModalSuccess1.ShowMessageSuccess = Resources.infoSetSubject_002;
                        UCModalSuccess1.ShowModalSuccess();
                    }
                }
            }
        }


        protected void lbtUpdate_Click(object sender, EventArgs e)
        {
            txtSubjectName.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblSubjectName")).Text;
            LabelSubjectId.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblSubjectCode")).Text;            
            hiGroup.Value = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblGRP_SUBJECT_CODE")).Text;
            lblgroup.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblGRP_SUBJECT_NAME")).Text;      
            txtMaxScore.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblMAX_SCORE")).Text;
            btnSave.Text = "แก้ไข";
            panSubject.Visible = true;
            lblSubjectId.Visible = true;
            txtsubjectId.Visible = false;
            lableGroup.Visible = true;
            trgroup.Visible = false;
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DTO.Subjectr subject = new DTO.Subjectr();
            subject.LICENSE_TYPE_CODE = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblLicenCode")).Text;
            subject.SUBJECT_CODE = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblSubjectCode")).Text;
            var res = biz.DeleteSubject(subject);
            if (res.ErrorMsg != null)
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
            else
            {
                BinGrid();
                UCModalSuccess1.ShowMessageSuccess = Resources.infoSetSubject_003;
                UCModalSuccess1.ShowModalSuccess();
            }
        }

        protected void btnCancelSubject_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            ddlGroup.SelectedValue = "";
            txtMaxScore.Text = string.Empty;
           // txtPass.Text = string.Empty;
            txtSubjectCode.Text = string.Empty;
            txtSubjectName.Text = string.Empty;
            btnSave.Text = "บันทึก";
            panSubject.Visible = false;
            lblSubjectId.Visible = false;
            txtsubjectId.Visible = true;
            lableGroup.Visible = false;
            trgroup.Visible = true;
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
                ClearText();
                panSubject.Visible = true;
            }
        }
    }
}