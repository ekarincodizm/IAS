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
    public partial class SearchSubjectGroup : Page
    {
        SubjectGroupBiz biz = new SubjectGroupBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataCenterBiz bizCenter = new DataCenterBiz();
                ddlType.DataSource = bizCenter.GetLicenseType("--เลือกประเภทใบอนุญาต--").DataResponse;
                ddlType.DataBind();
                var res = biz.GetSubjectGroupSearch("",txtPage.Text.ToInt(),txtCountRecord.Text.ToInt());
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
                lblCountRecord.Text = res.CountRecord.ToString();
                lblCountPage.Text = res.CountPage.ToString();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var res = biz.GetSubjectGroupSearch(ddlType.SelectedValue,txtPage.Text.ToInt(),txtCountRecord.Text.ToInt()); 
            gvSearch.DataSource = res.DataResponse;
            gvSearch.DataBind();
            lblCountRecord.Text = res.CountRecord.ToString();
            lblCountPage.Text = res.CountPage.ToString();
        }

        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = lblStatus.Text == "A" ? "ใช้งาน" : "ไม่ใช้";
            }
        }

        protected void lbtView_Click(object sender, EventArgs e)
        {
            Label lblNumber = (Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblCourseName");
            lblLicenseName.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblLisenseTypeName")).Text;
            lblStartDate.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblStartDate")).Text;
            lblEndDate.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblEndDate")).Text;
            lblStatus.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblStatus")).Text;
            lblNote.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblNote")).Text;
            GvSubject.DataSource = biz.GetSubjectInGroup(lblNumber.Text);
            GvSubject.DataBind();
            modalSubject.Show();
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            if (txtPage.Text.ToInt() !=1)
            {
                txtPage.Text = (txtPage.Text.ToInt() -1).ToString();
                btnShow_Click(sender, e);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            var res = biz.GetSubjectGroupSearch(ddlType.SelectedValue, txtPage.Text.ToInt(), txtCountRecord.Text.ToInt());
            gvSearch.DataSource = res.DataResponse;
            gvSearch.DataBind();
            lblCountRecord.Text = res.CountRecord.ToString();
            lblCountPage.Text = res.CountPage.ToString();
        }

    
        protected void btn_last_Click(object sender, EventArgs e)
        {
            if (txtPage.Text.ToInt() < lblCountPage.Text.ToInt())
            {
                txtPage.Text = (txtPage.Text.ToInt()+1).ToString();
                btnShow_Click(sender, e);
            }
        }
    }
}