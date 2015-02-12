using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Utils;
using System.Data;

namespace IAS.Account
{
    public partial class AccountDetail : basepage
    {
        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }


        private int RowPerPage { get{return PAGE_SIZE_Key;}}
        private int NumberGvSearch = 1;
        private string NameMemberType1 = "บุคคลทั่วไป/ตัวแทน/นายหน้า";
        private string UserMemberType
        {
            get { return (Session["_MemberType"] == null) ? "" : Session["_MemberType"].ToString(); }
            set { Session["_MemberType"] = value; }
        }
        
        private string UserCompCode
        {
            get { return (Session["_CompCode"] == null) ? "" : Session["_CompCode"].ToString(); }
            set { Session["_CompCode"] = value; }
        }

        private double TotalRows
        {
            get { return (Session["_TotalRows"] == null) ? double.Parse("0") : double.Parse(Session["_TotalRows"].ToString()); }
            set { Session["_TotalRows"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMemberType();
                BindGrid();
                ResetControl();
                SetDDLExamPlaceGroup();
            }
        }

        protected void SetMemberType()
        {
            DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetMemberType("ทั้งหมด", false);
            ls[1].Name = NameMemberType1;
            BindToDDL(ddlUserType, ls.ToArray());
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void BindGrid()
        {
            TotalRows = TotalRow();
            AccountBiz biz = new AccountBiz();
            txtNumberGvSearch.Text = (txtTotalPage.Text == LastPage().ToString() ? txtNumberGvSearch.Text : "1");
            int page = int.Parse(txtNumberGvSearch.Text);
            gvAccountDetail.DataSource = biz.GetAccountDetail(ddlUserType.SelectedValue, txtUserName.Text.Trim(), txtIdCard.Text.Trim(), txtEmail.Text.Trim(), page, rowsPerPage(), false).DataResponse;
            gvAccountDetail.DataBind();
            SetTextTotlaRows();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtRowsPerpage.Text = RowPerPage.ToString();
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            BindGrid();
            SetControlPage();
        }

        private double TotalRow()
        {
            AccountBiz biz = new AccountBiz();
            var res = biz.GetAccountDetail(ddlUserType.SelectedValue, txtUserName.Text.Trim(), txtIdCard.Text.Trim(), txtEmail.Text.Trim(), 0, 0, true).DataResponse;
            return double.Parse(res.Tables[0].Rows[0][0].ToString());
        }

        private double LastPage()
        {
            double mod = TotalRows / rowsPerPage();
            double lp = Math.Ceiling(mod);
            return lp == 0 ? 1 : lp;
        }

        private void SetControlPage()
        {
            //if (TotalRows < 1 || rowsPerPage() == 0)
            //{
            //    ctrPage.Visible = false;
            //}
            //else
            //{
                ctrPage.Visible = true;
                int gvPage = int.Parse(txtNumberGvSearch.Text);
                txtTotalPage.Text = LastPage().ToString();
                if (txtNumberGvSearch.Text == NumberGvSearch.ToString() && txtTotalPage.Text == NumberGvSearch.ToString())
                {
                    btnPreviousGvSearch.Visible = false;
                    btnNextGvSearch.Visible = false;
                }
                else if (TotalRows <= rowsPerPage())
                {
                    btnPreviousGvSearch.Visible = false;
                    btnNextGvSearch.Visible = false;
                }
                else if (gvPage == 1)
                {
                    btnPreviousGvSearch.Visible = false;
                    btnNextGvSearch.Visible = true;
                }
                else if (gvPage == LastPage())
                {
                    btnPreviousGvSearch.Visible = true;
                    btnNextGvSearch.Visible = false;
                }
                else
                {
                    btnPreviousGvSearch.Visible = true;
                    btnNextGvSearch.Visible = true;
                }
            //}
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) - 1;
            if( currPage > 0)
                txtNumberGvSearch.Text = currPage.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) + 1;
            if (currPage <= LastPage())
                txtNumberGvSearch.Text = currPage.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
            BindGrid();
            SetControlPage();
        }

        protected void lbtnEditMemberType_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            string id = ((Label)gr.FindControl("lblUserID")).Text;
            SetPopUpEditMemberType(id);
            PopUpEditTypeAccount.Show();
            UplPopUp.Update();
        }

        protected void btnClosePopup_Click(object sender, EventArgs e)
        {
            PopUpEditTypeAccount.Hide();
            UplPopUp.Update();
        }

        protected void SetMemberTypeFor(string MemberType)
        {
            BindToDDL(ddlMemberTypeEdit, GetMemberTypeFor(MemberType).ToArray());
            ddlMemberTypeEdit.SelectedValue = MemberType;
            ddlMemberTypeEdit.Enabled = false;
        }

        protected void SetPopUpEditMemberType(string id)
        {
            AccountBiz biz = new AccountBiz();
            var res = biz.GetAccountDetailById(id);
            chkEnableMemberType.Checked = false;
            trAssoc.Visible = false;
            trCompany.Visible = false;
            trExamPlaceGroup.Visible = false;
            if (res.DataResponse != null)
            {
                var ent = res.DataResponse;
                lblIdCard.Text = ent.ID_CARD_NO;
                lblNames.Text = ent.NAMES + " " + ent.LASTNAME;
                lblEmail.Text = ent.EMAIL;
                UserMemberType = ent.MEMBER_TYPE.Trim();
                SetMemberTypeFor(ent.MEMBER_TYPE);
                lblUserId.Text = ent.ID.Trim();
                txtRemark.Text = ent.OTH_USER_TYPE;
                UserCompCode = !String.IsNullOrEmpty(ent.COMP_CODE) ? ent.COMP_CODE.Trim() : "";
                DisableControlByMemberType(ent.MEMBER_TYPE);
            }
        }

        protected void chkEnableMemberType_CheckedChanged(object sender, EventArgs e)
        {
            SetCompanyAndAssociation();
            if (chkEnableMemberType.Checked)
            {
                ddlMemberTypeEdit.Enabled = true;
            }
            else
            {
                ddlMemberTypeEdit.SelectedValue = UserMemberType;
                ddlMemberTypeEdit.Enabled = false;
            }
            
        }

        protected void lbtnEditUserActive_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            string id = ((Label)gr.FindControl("lblUserID")).Text;
            SetControlPopUpDisableUser(id);
            PopUpDisableUser.Show();
        }

        protected void btnDisCancel_Click(object sender, EventArgs e)
        {
            PopUpDisableUser.Hide();
        }

        protected void SetControlPopUpDisableUser(string id)
        {
            try
            {
                AccountBiz biz = new AccountBiz();
                var res = biz.GetAccountDetailById(id);
                if (res.DataResponse != null)
                {
                    var ent = res.DataResponse;
                    lblDisUserID.Text = ent.ID;
                    lblDisIdCard.Text = ent.ID_CARD_NO;
                    lblDisNames.Text = ent.NAMES + " " + ent.LASTNAME;
                    lblDisEmail.Text = ent.EMAIL;
                    lblDisMemberType.Text = ent.MEMBER_TYPE == "1" ? NameMemberType1 : ent.MEMBER_TYPE_NAME;
                    radIsActive.SelectedValue = ent.ACTIVE;
                    if (ent.ACTIVE == "D")
                    {
                        btnDisSave.Enabled = false;
                        radIsActive.Enabled = false;
                        trCancelComment.Visible = true;
                        ddlDisRemark.SelectedValue = ent.DELETE_USER;
                        ddlDisRemark.Enabled = false;
                        if (!String.IsNullOrEmpty(ent.OTH_DELETE_USER))
                        {
                            txtDisRemarkText.Text = ent.OTH_DELETE_USER;
                            txtDisRemarkText.Visible = true;
                            txtDisRemarkText.Enabled = false;
                        }
                        else
                        {
                            txtDisRemarkText.Visible = false;
                            txtDisRemarkText.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlDisRemark.SelectedValue = "";
                        txtDisRemarkText.Text = "";
                        txtDisRemarkText.Visible = false;
                        trCancelComment.Visible = false;
                        ddlDisRemark.Enabled = true;
                        radIsActive.Enabled = true;
                        btnDisSave.Enabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
        }

        protected List<DTO.DataItem> GetMemberTypeFor(string MemberType)
        {
            DataCenterBiz biz = new DataCenterBiz();
            List<DTO.DataItem> item = new List<DTO.DataItem>();
            List<DTO.DataItem> res = biz.GetMemberType("", false);

            switch (MemberType)
            {
                case "1":
                    item = res.Where(x => x.Id == "1").ToList();
                    break;
                case "2":
                case "3":
                case "7":
                    item = res.Where(x => x.Id == "2" || x.Id == "3" || x.Id == "7").ToList();
                    break;
                case "4":
                    /*item = res.Where(x => x.Id == "4").ToList();
                    break;*/
                case "5":
                case "6":
                    item = res.Where(x => x.Id == "5" || x.Id == "6" || x.Id == "4").ToList();
                    break;
                default:
                    item = res;
                    break;
            }
            return item;
        }

        protected string GetMemberTypeTextFor(string MemberType)
        {
            return GetMemberTypeFor(MemberType).SingleOrDefault(x => x.Id == MemberType).Name;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AccountBiz biz = new AccountBiz();
            try
            {
                DTO.AccountDetail ent = new DTO.AccountDetail();
                ent.ID = lblUserId.Text;
                ent.MEMBER_TYPE = ddlMemberTypeEdit.SelectedValue;
                if (ent.MEMBER_TYPE == "2" && trCompany.Visible == true)
                {
                    ent.COMP_CODE = txtCompanyId.Text.Trim();
                }
                else if (ent.MEMBER_TYPE == "3" && trAssoc.Visible == true)
                {
                    ent.COMP_CODE = txtAssocId.Text.Trim();
                }
                else if (ent.MEMBER_TYPE == "7" && trExamPlaceGroup.Visible == true)
                {
                    ent.COMP_CODE = ddlExamPlaceGroup.SelectedValue;
                }
                else
                {
                    ent.COMP_CODE = UserCompCode;
                }
                
                ent.OTH_USER_TYPE = txtRemark.Text;

                var res = biz.EditMemberTypeAndActive(ent, UserProfile);
                if (!res.IsError)
                {
                    PopUpEditTypeAccount.Hide();
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    btnSearch_Click(sender, e);
                    UpdatePanelGv.Update();
                    UplPopUp.Update();
                }
                else
                {
                    PopUpEditTypeAccount.Show();
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();

                    if (ent.MEMBER_TYPE == "2" && trCompany.Visible == true)
                    {
                        SetCompanyIdName();
                    }
                    else if (ent.MEMBER_TYPE == "3" && trAssoc.Visible == true)
                    {
                        SetAssocIdName();
                    }
                    else if (ent.MEMBER_TYPE == "7" && trExamPlaceGroup.Visible == true)
                    {
                        SetExamPlaceGroup();
                    }
                    UplPopUp.Update();
                }
            }
            catch (Exception ex)
            {
                PopUpEditTypeAccount.Show();
                this.MasterSite.ModelError.ShowMessageError = ex.Message;
                this.MasterSite.ModelError.ShowModalError();
                UplPopUp.Update();
            }
        }

        protected void SetTextTotlaRows()
        {
            lblTotalRows.Text = String.Format("จำนวน {0} รายการ", TotalRows);  
        }

        protected void pageGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            BindGrid();
            SetControlPage();
        }

        protected int rowsPerPage()
        {
            int _rpp = RowPerPage;
            if (txtRowsPerpage.Text != "")
                _rpp = int.Parse(txtRowsPerpage.Text);
            return _rpp;
        }

        protected void ResetControl()
        {
            ddlUserType.SelectedValue = "";
            txtUserName.Text = "";
            txtIdCard.Text = "";
            txtEmail.Text = "";
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            txtRowsPerpage.Text = RowPerPage.ToString();
            SetControlPage();
        }

        protected void btnDisSave_Click(object sender, EventArgs e)
        {
            try
            {
                AccountBiz biz = new AccountBiz();
                DTO.AccountDetail user = new DTO.AccountDetail();
                user.ID = lblDisUserID.Text;
                user.ACTIVE = radIsActive.SelectedValue;
                user.ID_CARD_NO = lblDisIdCard.Text;
                if (user.ACTIVE == "D")
                {
                    user.DELETE_USER = ddlDisRemark.SelectedValue;
                    user.OTH_DELETE_USER = txtDisRemarkText.Text;
                    if (String.IsNullOrEmpty(user.DELETE_USER))
                    {
                        MessageBoxError("กรุณาเลือก เหตุผลการยกเลิก");
                        return;
                    }
                }

                var res = biz.DisableUser(user, UserProfile);
                if (!res.IsError)
                {
                    PopUpDisableUser.Hide();
                    MessageBoxSuccess("บันทึกข้อมูลเรียบร้อยแล้ว");
                    BindGrid();
                    UpdatePanelGv.Update();
                }
                else
                {
                    MessageBoxError(res.ErrorMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
        }

        protected void ddlDisRemark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDisRemark.SelectedValue == "อื่น ๆ")
            {
                txtDisRemarkText.Text = "";
                txtDisRemarkText.Visible = true;
            }
            else
            {
                txtDisRemarkText.Text = "";
                txtDisRemarkText.Visible = false;
            }
        }

        protected void radIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radIsActive.SelectedValue == "D")
            {
                AccountBiz biz = new AccountBiz();
                var res = biz.GetAccountDetailById(lblDisUserID.Text.Trim()).DataResponse;
                if (res.ACTIVE == radIsActive.SelectedValue)
                {
                    ddlDisRemark.SelectedValue = res.DELETE_USER;
                    ddlDisRemark.Enabled = false;
                    if (!String.IsNullOrEmpty(res.OTH_DELETE_USER))
                    {
                        txtDisRemarkText.Text = res.OTH_DELETE_USER;
                        txtDisRemarkText.Enabled = false;
                        txtDisRemarkText.Visible = true;
                    }
                }
                else
                {
                    ddlDisRemark.SelectedValue = "";
                    txtDisRemarkText.Visible = false;
                    txtDisRemarkText.Enabled = true;
                }
                trCancelComment.Visible = true;
            }
            else
            {
                ddlDisRemark.SelectedValue = "";
                trCancelComment.Visible = false;
                txtDisRemarkText.Text = "";
                txtDisRemarkText.Visible = false;
                ddlDisRemark.Enabled = true;
                txtDisRemarkText.Enabled = true;
            }
        }

        protected void MessageBoxError(string Message)
        {
            this.MasterSite.ModelError.ShowMessageError = Message;
            this.MasterSite.ModelError.ShowModalError();
        }

        protected void MessageBoxSuccess(string Message)
        {
            this.MasterSite.ModelSuccess.ShowMessageSuccess = Message;
            this.MasterSite.ModelSuccess.ShowModalSuccess();
        }

        protected void SetCompanyAndAssociation()
        {
            if (ddlMemberTypeEdit.SelectedValue == "2" && chkEnableMemberType.Checked)
            {
                SetCompanyIdName();
                trCompany.Visible = true;
                trAssoc.Visible = false;
                trExamPlaceGroup.Visible = false;
            }
            else if (ddlMemberTypeEdit.SelectedValue == "3" && chkEnableMemberType.Checked)
            {
                SetAssocIdName();
                trAssoc.Visible = true;
                trCompany.Visible = false;
                trExamPlaceGroup.Visible = false;
            }
            else if (ddlMemberTypeEdit.SelectedValue == "7" && chkEnableMemberType.Checked)
            {
                SetExamPlaceGroup();
                trAssoc.Visible = false;
                trCompany.Visible = false;
                trExamPlaceGroup.Visible = true;
            }
            else
            {
                trCompany.Visible = false;
                trAssoc.Visible = false;
                trExamPlaceGroup.Visible = false;
                txtCompanyId.Text = string.Empty;
                txtCompany.Text = string.Empty;
                txtAssocId.Text = string.Empty;
                txtAssoc.Text = string.Empty;
                ddlExamPlaceGroup.SelectedValue = "";
            }
        }

        protected void ddlMemberTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCompanyAndAssociation();
        }

        protected void DisableControlByMemberType(string MemberType)
        {
            switch (MemberType)
            {
                case "1":
                //case "4":
                    chkEnableMemberType.Enabled = false;
                    txtRemark.Enabled = false;
                    btnUpdate.Enabled = false;
                    break;
                default:
                    chkEnableMemberType.Enabled = true;
                    txtRemark.Enabled = true;
                    btnUpdate.Enabled = true;
                    break;
            }
        }

        protected void gvAccountDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActive = e.Row.FindControl("lblIsActive") as Label;
                Label UserType = e.Row.FindControl("lblUserTypeCode") as Label;
                Label UserTypeName = e.Row.FindControl("lblUserType") as Label;
                LinkButton EType = e.Row.FindControl("lbtnEditMemberType") as LinkButton;
                LinkButton EPass = e.Row.FindControl("lbtnEditPassword") as LinkButton;
                LinkButton EDis = e.Row.FindControl("lbtnEditUserActive") as LinkButton;
                Label ShowActive = e.Row.FindControl("lblShowActive") as Label;
                switch (lblActive.Text)
                {
                    case "I":
                        ShowActive.Text = "ไม่ใช้งาน";
                        break;
                    case "D":
                        ShowActive.Text = "ยกเลิกบัญชีผู้ใช้งาน";
                        break;
                    default:
                        ShowActive.Text = "ใช้งาน";
                        break;
                }

                switch (UserType.Text)
                {
                    case "1":
                        EType.Visible = false;
                        EPass.Visible = lblActive.Text == "D" ? false : true;
                        EDis.Visible = true;
                        UserTypeName.Text = NameMemberType1;
                        break;
                    case "4":
                    case "5":
                    case "6":
                        EType.Visible = true;
                        EPass.Visible = false;
                        EDis.Visible = false;
                        break;
                    default :
                        EType.Visible = lblActive.Text == "D" ? false : true;
                        EPass.Visible = lblActive.Text == "D" ? false : true;
                        EDis.Visible = true;
                        break;
                }

            }
        }

        protected void lbtnEditPassword_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            string id = ((Label)gr.FindControl("lblUserID")).Text;
            SetPopupEditPassword(id);
            PopupEditPassword.Show();

            
        }

        protected void SetPopupEditPassword(string id)
        {
            try
            {
                AccountBiz biz = new AccountBiz();
                var res = biz.GetAccountDetailById(id);
                if (res.DataResponse != null)
                {
                    var ent = res.DataResponse;
                    lblPwdUserID.Text = ent.ID;
                    lblPwdIdCard.Text = ent.ID_CARD_NO;
                    lblPwdNames.Text = ent.NAMES + " " + ent.LASTNAME;
                    lblPwdEmail.Text = ent.EMAIL;
                    lblPwdMemberType.Text = ent.MEMBER_TYPE == "1" ? NameMemberType1 : ent.MEMBER_TYPE_NAME;
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
                PopupEditPassword.Hide();
            }
        }

        protected void btnPwdSave_Click(object sender, EventArgs e)
        {
            try
            {
                var valid = PasswordValidation();
                if (valid.IsError)
                {
                    MessageBoxError(valid.ErrorMsg);
                }
                else
                {
                    BLL.AccountBiz biz = new AccountBiz();
                    DTO.User ent = new DTO.User();
                    ent.USER_ID = lblPwdUserID.Text;
                    ent.USER_PASS = txtNewPassword.Text;

                    var res = biz.ChangePasswordByAdmin(ent, UserProfile);
                    if (!res.IsError)
                    {
                        MessageBoxSuccess(SysMessage.SaveSucess);
                        PopupEditPassword.Hide();
                    }
                    else
                    {
                        MessageBoxError(res.ErrorMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }

        }

        protected void btnPwdCancel_Click(object sender, EventArgs e)
        {
            PopupEditPassword.Hide();
        }

        private DTO.ResponseMessage<bool> PasswordValidation()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {

                this.Page.Validate("ChangePW");
                if (this.Page.IsValid)
                {
                    res.ResultMessage = true;
                }
                else
                {
                    if (this.reqNewPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqNewPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqRegNewPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqRegNewPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqConfirmPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqConfirmPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqRegConfirmPW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqRegConfirmPW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if (this.reqComparePW.IsValid == false)
                    {
                        res.ErrorMsg = this.reqComparePW.ErrorMessage;
                        res.ResultMessage = false;
                    }
                    else if ((this.reqNewPW.IsValid == true) && (this.reqRegNewPW.IsValid == true) && (this.reqConfirmPW.IsValid == true)
                        && (this.reqRegConfirmPW.IsValid == true) && (this.reqComparePW.IsValid == true))
                    {
                        res.ResultMessage = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        protected void SetCompanyIdName()
        {
            if (!String.IsNullOrEmpty(UserCompCode))
            {
                AccountBiz biz = new AccountBiz();
                string CompName = biz.GetCompanyNameById(UserCompCode);
                if (!string.IsNullOrEmpty(CompName) && UserMemberType == ddlMemberTypeEdit.SelectedValue)
                {
                    txtCompanyId.Text = UserCompCode;
                    txtCompany.Text = string.Format("{0} [{1}]", CompName, UserCompCode);
                }
                else
                {
                    txtCompanyId.Text = string.Empty;
                    txtCompany.Text = string.Empty;
                }
            }
        }

        protected void SetAssocIdName()
        {
            if (!String.IsNullOrEmpty(UserCompCode))
            {
                AccountBiz biz = new AccountBiz();
                string AssocName = biz.GetAssociationNameById(UserCompCode);
                if (!string.IsNullOrEmpty(AssocName) && UserMemberType == ddlMemberTypeEdit.SelectedValue)
                {
                    txtAssocId.Text = UserCompCode;
                    txtAssoc.Text = string.Format("{0} [{1}]", AssocName, UserCompCode);
                }
                else
                {
                    txtAssocId.Text = string.Empty;
                    txtAssoc.Text = string.Empty;
                }
            }
        }

        protected void SetExamPlaceGroup()
        {
            if (!String.IsNullOrEmpty(UserCompCode) && UserMemberType == ddlMemberTypeEdit.SelectedValue)
            {
                ddlExamPlaceGroup.SelectedValue = ddlExamPlaceGroup.Items.FindByValue(UserCompCode) != null ? UserCompCode : "";
            }
            else
            {
                ddlExamPlaceGroup.SelectedValue = "";
            }
        }

        protected void SetDDLExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlExamPlaceGroup,ls.DataResponse);
        }
    }
}
