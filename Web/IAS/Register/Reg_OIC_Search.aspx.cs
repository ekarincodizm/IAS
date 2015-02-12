using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.BLL.RegistrationService;
using IAS.BLL;
using System.Data;
using IAS.Properties;

namespace IAS.Register
{
    public partial class Reg_OIC_Search : basepage
    {
        public const int PAGE_SIZE = 20;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();

                GetSearchOICType();
                GetSearchStatus();
            }
        }

        private void GetSearchOICType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetOICType(SysMessage.DefaultSelecting);
            BindToDDL(ddlSearchMemberType, ls);
        }

        private void GetSearchStatus()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus("");
            BindToDDL(ddlSearchStatus, ls);
        }

        private void GetSearchOfficerOIC()
        {

            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var resCount = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                       txtLastName.Text,
                                                       null,
                                                       null,
                                                       txtIDCard.Text,
                                                       ddlSearchMemberType.SelectedValue,
                                                       txtEmail.Text,
                                                       "",
                                                       ddlSearchStatus.SelectedValue,
                                                       resultPage,
                                                       PAGE_SIZE, "1");
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage.Text = Convert.ToString(TotalPages);
            var res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                  txtLastName.Text,
                                                  null,
                                                  null,
                                                  txtIDCard.Text,
                                                  ddlSearchMemberType.SelectedValue,
                                                  txtEmail.Text,
                                                  "",
                                                  ddlSearchStatus.SelectedValue,
                                                  resultPage,
                                                  PAGE_SIZE, "2");
            if (res.ErrorMsg == null)
            {

                if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                        ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                {
                    gvSearchOfficerOIC.DataSource = res.DataResponse.Tables[0];
                    gvSearchOfficerOIC.DataBind();
                    if (gvSearchOfficerOIC.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                        {
                            gvSearchOfficerOIC.Columns[0].Visible = true;
                        }
                        btnGroupApprove.Visible = true;
                        if (txtTotalPage.Text == "1")
                        {

                            txtNumberGvSearch.Visible = true;
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                        }
                        else
                        {
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = true;
                        }
                    }
                }
                else
                {
                    if (gvSearchOfficerOIC.Rows.Count > 0)
                    {
                        gvSearchOfficerOIC.DataSource = res.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();
                        gvSearchOfficerOIC.Columns[0].Visible = false;
                        btnGroupApprove.Visible = false;
                        if (txtTotalPage.Text == "1")
                        {

                            txtNumberGvSearch.Visible = true;
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                        }
                        else
                        {
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = true;
                        }
                    }
                }
            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }

        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetSearchOfficerOIC();
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lbl = (Label)gr.FindControl("lblID");
            Session["PersonID"] = lbl.Text;
            Response.Redirect("~/Person/Edit_Reg_Person.aspx");
        }

        protected void hplApprove_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lbl = (Label)gr.FindControl("lblID");
            LinkButton lnk = (LinkButton)gr.FindControl("hplApprove");
            Session["PersonID"] = lbl.Text;
            if (ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString())
            {
                Response.Redirect("~/Register/editRegGuest.aspx");
            }
            else
            {
                Response.Redirect("~/Register/regApproveOfficerOic.aspx");
            }

        }

        /// <summary>
        /// RegistrationService Update 
        /// RegistrationApprove()
        /// By NATTAPONG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGroupApprove_Click(object sender, EventArgs e)
        {
            #region Get MemberType From Control @Nattapong
            string RegMemType = string.Empty;
            if (ddlSearchMemberType.SelectedItem.Text != "")
            {
                switch (ddlSearchMemberType.SelectedItem.Text)
                {
                    case "บุคคลทั่วไป":
                        RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
                        break;
                    case "บุคคลทั่วไป/ตัวแทน/นายหน้า":
                        RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
                        break;
                    case "บริษัทประกัน":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
                        break;
                    case "บริษัท":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
                        break;
                    case "สมาคม":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Association.GetEnumValue());
                        break;
                }
            }

            #endregion

            for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)gvSearchOfficerOIC.Rows[i].FindControl("chkSelect");
                List<string> ls = new List<string>();

                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        //string strID = gvSearchOfficerOIC.Rows[i].Cells[2].Text;
                        Label lbl = (Label)gvSearchOfficerOIC.Rows[i].FindControl("lblID");
                        string strID = lbl.Text;
                        if (!string.IsNullOrEmpty(strID))
                        {
                            ls.Add(strID);
                            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
                            string userid = UserProfile.Id;
                            var res = biz.RegistrationApprove(ls, "อนุมัติกลุ่ม", userid, RegMemType);
                            if (!string.IsNullOrEmpty(res.ErrorMsg))
                            {
                                UCModalError.ShowMessageError = Resources.errorReg_OIC_Search_001;
                                UCModalError.ShowModalError();
                            }
                            else
                            {
                                UCModalError.ShowMessageError = Resources.errorReg_OIC_Search_002;
                                UCModalError.ShowModalError();

                            }
                        }
                    }
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            btnGroupApprove.Enabled = true;
        }

        protected void gvSearchOfficerOIC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = ((Label)e.Row.FindControl("lblStatus"));
                LinkButton approve = ((LinkButton)e.Row.FindControl("hplApprove"));

                if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                        ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                {
                    approve.Visible = true;
                }
                else
                {
                    approve.Visible = false;
                }
            }
        }
     
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                btnPreviousGvSearch.Visible = false;

                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;
            }
            BindPage();

        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;

            }
            else
            {
                txtNumberGvSearch.Text = txtTotalPage.Text;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;

            }
            BindPage();
        }

        protected void BindPage()
        {
            var resultPage = txtNumberGvSearch.Text.ToInt();

            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            var res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                      txtLastName.Text,
                                                      null,
                                                      null,
                                                      txtIDCard.Text,
                                                      ddlSearchMemberType.SelectedValue,
                                                      txtEmail.Text,
                                                      "",
                                                      ddlSearchStatus.SelectedValue,
                                                      resultPage,
                                                      PAGE_SIZE, "2");



            gvSearchOfficerOIC.DataSource = res.DataResponse;
            gvSearchOfficerOIC.DataBind();
        
        }
    }
}
