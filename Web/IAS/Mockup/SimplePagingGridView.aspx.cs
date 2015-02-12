using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAS.Mockup
{
    public partial class SimplePagingGridView : System.Web.UI.Page
    {
        private DTO.PagingInfo _pagingInfo;
        public DTO.PagingInfo PagingInfo { get { return (DTO.PagingInfo)ViewState["PagingInfo"]; } set { _pagingInfo = value; ViewState["PagingInfo"] = _pagingInfo; } }
        public GridView GridControl { get { return gvSearchOfficerOIC; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack){
                BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
                DTO.ResponseService<DTO.PagingResponse<DataSet>> res = biz.GetRegistrationsByCriteriaAtPage("", "", "", "", "", "","2", 1,12);

                gvSearchOfficerOIC.DataSource = res.DataResponse.DataResponse;

                PagingInfo = res.DataResponse.PagingInfo;

                gvSearchOfficerOIC.DataBind();             
                GetSearchOICType();
                GetSearchStatus();
            }
        }

        private void InitPageInfo(DTO.PagingInfo pageInfo) 
        { 
               
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
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
        protected void gvSearchOfficerOIC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e) { 
            
        }

        protected void hplView_Click(object sender, EventArgs e) { }

        protected void hplApprove_Click(object sender, EventArgs e) { }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            gvSearchOfficerOIC.DataSource = biz.GetRegistrationsByCriteriaAtPage
                (txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, 
                    "", ddlSearchStatus.SelectedValue, 1,Convert.ToInt32(txtPageSize.Text.Trim())).DataResponse.DataResponse;
            gvSearchOfficerOIC.DataBind();
        }

        protected void gvSearchOfficerOIC_PageIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void GetData(Int32 pageIndex) {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            gvSearchOfficerOIC.DataSource = biz.GetRegistrationsByCriteriaAtPage
                (txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text,
                "", ddlSearchStatus.SelectedValue, 1, Convert.ToInt32(txtPageSize.Text.Trim())).DataResponse.DataResponse;
            gvSearchOfficerOIC.DataBind();
        }
        protected void gvSearchOfficerOIC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ((GridView)sender).PageIndex = e.NewPageIndex;

            //DataView dvEmployee = Getdata();
            //GridVwPagingSorting.DataSource = dvEmployee;
            //GridVwPagingSorting.DataBind();
        }

        protected void hplPrevious_Click(object sender, EventArgs e) { 
        
        }

        protected void hplNext_Click(object sender, EventArgs e) {
            TextBox currentPage = (TextBox)((GridViewRow)((LinkButton)sender).NamingContainer).Cells[0].FindControl("txtCurrentPage");
            if (PagingInfo.CurrentPage < PagingInfo.TotalPages) {
                PagingInfo.CurrentPage++;
                GetData(PagingInfo.CurrentPage);
                currentPage.Text = PagingInfo.CurrentPage.ToString();
            } 
        }

        protected void txtCurrentPage_TextChanged(object sender, EventArgs e) { 
        
        }
    }
}