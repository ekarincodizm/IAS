using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;

namespace IAS.Mockup
{
    public partial class PagingTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //base.HasPermit();

                GetSearchOICType();
                GetSearchStatus();
            }
        }

        private void RegistrationSearch(Int32 pageIndex, Int32 pageSize) {
            using (BLL.RegistrationBiz biz = new BLL.RegistrationBiz()) {
                var ls = biz.GetRegistrationsByCriteriaAtPage(txtFirstName.Text,
                                            txtLastName.Text,
                                            txtIDCard.Text,
                                            ddlSearchMemberType.SelectedValue,
                                            txtEmail.Text,
                                            "",
                                            ddlSearchStatus.SelectedValue,
                                            pageIndex,
                                            pageSize);

                
                if (ls.DataResponse != null && ls.DataResponse.DataResponse.Tables.Count > 0)
                {
                    DTO.PagingInfo pageInfo = ls.DataResponse.PagingInfo;


                    gvSearchOfficerOIC.DataSource = ls.DataResponse.DataResponse.Tables[0];
                 
                    gvSearchOfficerOIC.DataBind();
                }
            }
        }
        private void PersonSearch(Int32 pageIndex, Int32 pageSize)
        {
            using (BLL.PersonBiz bizPerson = new BLL.PersonBiz()) {
                var ls = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, 
                                                                txtLastName.Text, 
                                                                null,
                                                                null,
                                                                txtIDCard.Text, 
                                                                ddlSearchMemberType.SelectedValue, 
                                                                txtEmail.Text, 
                                                                "",
                                                                ddlSearchStatus.SelectedValue, pageIndex, pageSize, "2");

                if (ls.DataResponse != null && ls.DataResponse.Tables.Count>0) {
                    gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
                    gvSearchOfficerOIC.DataBind();  
                }
                
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PersonSearch(1, 20);
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{


        //    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
        //    BLL.PersonBiz bizPerson = new BLL.PersonBiz();

        //    //this.RegStatus = ddlSearchStatus.SelectedValue;
        //    //var resultPage = txtNumberGvSearch.Text.ToInt();
        //    //Status 1 = รออนุมัติ(สมัคร), Status 2 = อนุมัติ(สมัคร), Status 3 = ไม่อนุมัติ(สมัคร)
        //    if (ddlSearchStatus.SelectedValue.Equals("1") || ddlSearchStatus.SelectedValue.Equals("2") || ddlSearchStatus.SelectedValue.Equals("3"))
        //    {
        //        //btnGroupApprove.Visible = true;

        //        var ls = biz.GetRegistrationsByCriteria(txtFirstName.Text,
        //                                                txtLastName.Text,
        //                                                txtIDCard.Text,
        //                                                ddlSearchMemberType.SelectedValue,
        //                                                txtEmail.Text,
        //                                                "",
        //                                                ddlSearchStatus.SelectedValue,
        //                                                gvSearchOfficerOIC.PageIndex,
        //                                                20);

        //        if (ls.ErrorMsg == null)
        //        {
        //            if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
        //                    ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
        //            {
        //                gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //                gvSearchOfficerOIC.DataBind();

        //                if (gvSearchOfficerOIC.Rows.Count > 0)
        //                {
        //                    //btnGroupApprove.Visible = true;
        //                    //for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
        //                    //{
        //                    //    gvSearchOfficerOIC.Columns[0].Visible = true;
        //                    //    btnPreviousGvSearch.Visible = true;
        //                    //    txtNumberGvSearch.Visible = true;
        //                    //    btnNextGvSearch.Visible = true;
        //                    //}
        //                    //btnPreviousGvSearch.Visible = true;
        //                    //txtNumberGvSearch.Visible = true;
        //                    //btnNextGvSearch.Visible = true;
        //                }
        //                else
        //                {
        //                    //btnGroupApprove.Visible = false;
        //                }
        //            }
        //            else
        //            {

        //                gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //                gvSearchOfficerOIC.DataBind();
        //                if (gvSearchOfficerOIC.Rows.Count > 0)
        //                {

        //                    gvSearchOfficerOIC.Columns[0].Visible = false;
        //                    //btnGroupApprove.Visible = false;
        //                    //btnPreviousGvSearch.Visible = true;
        //                    //txtNumberGvSearch.Visible = true;
        //                    //btnNextGvSearch.Visible = true;

        //                }
        //            }
        //        }
        //        else
        //        {
        //            //UCModalError.ShowMessageError = ls.ErrorMsg;
        //            //UCModalError.ShowModalError();
        //        }
        //    }
        //    //Status 4 = รออนุมัติ(แก้ไข), Status 5 = อนุมัติ(แก้ไข), Status 6 = ไม่อนุมัติ(แก้ไข)
        //    //else if (ddlSearchStatus.SelectedValue.Equals("4"))
        //    //{
        //    //    btnGroupApprove.Visible = false;
        //    //    var ls = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue);

        //    //    if (ls.ErrorMsg == null)
        //    //    {

        //    //        if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
        //    //                ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
        //    //        {
        //    //            gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //    //            gvSearchOfficerOIC.DataBind();

        //    //            if (gvSearchOfficerOIC.Rows.Count > 0)
        //    //            {
        //    //                for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
        //    //                {
        //    //                    gvSearchOfficerOIC.Columns[0].Visible = true;
        //    //                }
        //    //                btnGroupApprove.Visible = true;
        //    //            }
        //    //            else
        //    //            {
        //    //                btnGroupApprove.Visible = false;
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //    //            gvSearchOfficerOIC.DataBind();
        //    //            gvSearchOfficerOIC.Columns[0].Visible = false;
        //    //            btnGroupApprove.Visible = false;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        UCModalError.ShowMessageError = ls.ErrorMsg;
        //    //        UCModalError.ShowModalError();
        //    //    }
        //    //}
        //    //else if (ddlSearchStatus.SelectedValue.Equals("5") || ddlSearchStatus.SelectedValue.Equals("6"))
        //    //{
        //    //    btnGroupApprove.Visible = false;
        //    //    var ls = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue);

        //    //    if (ls.ErrorMsg == null)
        //    //    {

        //    //        if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
        //    //                ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
        //    //        {
        //    //            gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //    //            gvSearchOfficerOIC.DataBind();

        //    //            if (gvSearchOfficerOIC.Rows.Count > 0)
        //    //            {
        //    //                for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
        //    //                {
        //    //                    gvSearchOfficerOIC.Columns[0].Visible = true;
        //    //                }
        //    //                btnGroupApprove.Visible = true;
        //    //            }
        //    //            else
        //    //            {
        //    //                btnGroupApprove.Visible = false;
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            gvSearchOfficerOIC.DataSource = ls.DataResponse.Tables[0];
        //    //            gvSearchOfficerOIC.DataBind();
        //    //            gvSearchOfficerOIC.Columns[0].Visible = false;
        //    //            btnGroupApprove.Visible = false;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        UCModalError.ShowMessageError = ls.ErrorMsg;
        //    //        UCModalError.ShowModalError();
        //    //    }
        //    //}
        //    //else if (ddlSearchStatus.SelectedValue.Equals(""))
        //    //{
        //    //    UCModalError.ShowMessageError = "กรุณาเลือกสถานะอนุมัติ";
        //    //    UCModalError.ShowModalError();

        //    //    var ls = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue);
        //    //    gvSearchOfficerOIC.DataSource = null;
        //    //    gvSearchOfficerOIC.DataBind();
        //    //    btnGroupApprove.Visible = false;
        //    //}
        //}
        private void GetSearchOICType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberType(SysMessage.DefaultSelecting, true);
            //ls.RemoveAt(4);
            BindToDDL(ddlSearchMemberType, ls);
        }

        private void GetSearchStatus()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //var ls = biz.GetStatus("");
            var ls = biz.GetStatus("");
            //ls.Insert(0, new DTO.DataItem { Id = string.Empty, Name = "เลือก" });
            BindToDDL(ddlSearchStatus, ls);
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        public void chkSelect_CheckedChanged(object sender, EventArgs e) { 
        
        }

        public void hplView_Click(object sender, EventArgs e) { 
        
        }

        public void hplApprove_Click(object sender, EventArgs e) { }

        protected void gvSearchOfficerOIC_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvSearchOfficerOIC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }      
    }
}