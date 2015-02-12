using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;

using IAS.DTO;
using System.Drawing;
using System.Xml;
using System.IO;
using IAS.Properties;

namespace IAS.Register
{
    public partial class regOfficerOIC1 : basepage
    {
        public string UserID
        {
            get
            {
                return (string)Session["UserID"];
            }

        }

        public string MemberTypeOfficerOIC
        {
            get
            {
                return (string)Session["MemberTypeOfficerOIC"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTitleName();
                GetMemberTypeOIC();
                Session["UserID"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                Session["MemberTypeOfficerOIC"] = "4";
            }
        }

        private void GetMemberTypeOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetOICType(SysMessage.DefaultSelecting);
            BindToDDL(ddlMemberType, ls);
        }

        private void GetTitleName()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(SysMessage.DefaultSelecting);
            BindToDDL(ddlAntecedent, ls);
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            InsertMode();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlMemberType.SelectedIndex > 0)
            {
                dvOIC.Visible = true;
                txtMemberType.Text = ddlMemberType.SelectedItem.Text;
                txtMemberType.Enabled = false;
                ddlMemberType.Enabled = false;
                if (ddlMemberType.SelectedIndex == 2)
                {
                    lbl_licent.Visible = true;
                    fulSignature.Visible = true;
                    lblDescription.Visible = true;
                }
                else
                {
                    lbl_licent.Visible = false;
                    fulSignature.Visible = false;
                    lblDescription.Visible = false;
                }
            }
            else
            {
                UCModalError1.ShowMessageError = SysMessage.PleaseChooesTypeMemberOIC;
                UCModalError1.ShowModalError();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();           
        }

        private void ClearControl()
        {
            txtMemberType.Text = String.Empty;
            txtIDNumber.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtOICUserName.Text = string.Empty;
            
            rblSex.ClearSelection();
            ddlAntecedent.SelectedIndex = 0;
            ddlMemberType.Enabled = true;
            dvOIC.Visible = false;
            ddlMemberType.SelectedIndex = 0;
        }

        private void InsertMode()
        {            
            BLL.PersonBiz biz = new BLL.PersonBiz();
            PersonTemp per = new PersonTemp();
            byte[] sign = new byte[1024];

            if (ddlMemberType.SelectedIndex == 2)
            {
                sign = fulSignature.FileBytes;
                string name = fulSignature.FileName;

                if (fulSignature.FileName == "")
                {
                    UCModalError1.ShowMessageError = Resources.errorReg_OIC_001;
                    UCModalError1.ShowModalError();
                    return;
                }

                if (Path.GetExtension(fulSignature.FileName) != ".png")
                {
                    UCModalError1.ShowMessageError = Resources.errorReg_OIC_001;
                    UCModalError1.ShowModalError();
                    return;
                }
            }

         
            per.ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            per.PRE_NAME_CODE = ddlAntecedent.SelectedValue;

            if (rblSex.SelectedValue == "M")
            {
                per.SEX = "M";
            }
            else
            {
                per.SEX = "F";
            }
            per.NAMES = txtFirstName.Text;
            per.LASTNAME = txtLastName.Text;
            per.MEMBER_TYPE = this.MemberTypeOfficerOIC;
            per.EMPLOYEE_NO = txtIDNumber.Text;
            var result = biz.InsertOIC(txtIDNumber.Text, txtOICUserName.Text
                                        , ddlAntecedent.SelectedValue, txtFirstName.Text
                                        , txtLastName.Text, rblSex.SelectedValue
                                        , ddlMemberType.SelectedValue,sign);
            if (result.IsError)
            {
                UCModalError1.ShowMessageError = result.ErrorMsg;
                UCModalError1.ShowModalError();                
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessInsertTypeOIC;
                UCModalSuccess.ShowModalSuccess();
                ClearControl();
                dvOIC.Visible = false;

            }
            
        }

        protected void ddlAntecedent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resSex = Utils.GetTitleName.GetSex(ddlAntecedent.SelectedItem.Text);
            if ((resSex != null) && (resSex != ""))
            {
                if (resSex.Equals("M"))
                {
                    rblSex.SelectedValue = "M";
                }
                if (resSex.Equals("F"))
                {
                    rblSex.SelectedValue = "F";
                }
                rblSex.Enabled = false;
            }
            else
            {
                //Enable for select Sex
                rblSex.SelectedValue = null;
                rblSex.Enabled = true;
            }
        }

    }
}