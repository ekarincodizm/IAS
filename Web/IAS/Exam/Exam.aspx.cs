using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Exam
{
    public partial class Exam : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                //DataCenterService biz = new
                BLL.ExamResultBiz biz = new BLL.ExamResultBiz();
                var a = biz.GetExamResultUploadByGroupId("0474");
            }
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            
            //var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            //var text = (Label)gr.FindControl("lblFileGv");

            //Session["ViewFileName"] = text.Text;

            //string url = "regViewDocument.aspx";
            //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}'); </script>", Page.ResolveUrl(url)));


        }

        protected void hplDelete_Click(object sender, EventArgs e)
        {
            //var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            //Label lblFileGv = (Label)gr.FindControl("lblFileGv");

            //var list = this.AttachFiles;

            //var _item = list.Find(l => l.ATTACH_FILE_PATH == lblFileGv.Text);

            //list.Remove(_item);

            //var source = Server.MapPath(mapPath + "/" + lblFileGv.Text);

            //FileInfo fiPath = new FileInfo(source);
            //if (fiPath.Exists)
            //{
            //    File.Delete(source);
            //}

            //gvUpload.DataSource = list;
            //gvUpload.DataBind();

            //ddlTypeAttachment.SelectedIndex = 0;
            //txtDetail.Text = string.Empty;
        }

        protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvUpload.EditIndex = e.NewEditIndex;


            //gvUpload.DataSource = this.AttachFiles;
            //gvUpload.DataBind();


        }

        protected void gvDetail_PreRender(object sender, EventArgs e)
        {
            //if (this.gvUpload.EditIndex != -1)
            //{
            //    LinkButton hplView = gvUpload.Rows[gvUpload.EditIndex].Cells[0].FindControl("hplView") as LinkButton;
            //    LinkButton hplDelete = gvUpload.Rows[gvUpload.EditIndex].Cells[0].FindControl("hplDelete") as LinkButton;

            //    if (hplView != null)
            //    {
            //        hplView.Enabled = false;
            //        hplDelete.Enabled = false;
            //    }
            //}

        }

        protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //GridViewRow row = gvUpload.Rows[e.RowIndex];

            //TextBox txtDetailGv = (TextBox)row.FindControl("txtDetailGv");


            //string _ID = gvUpload.DataKeys[e.RowIndex].Value.ToString();



            //var _item = this.AttachFiles.Find(l => l.ID == _ID);

            //if (_item != null)
            //{
            //    _item.REMARK = txtDetailGv.Text;
            //}

            //gvUpload.EditIndex = -1;

            //gvUpload.DataSource = this.AttachFiles;
            //gvUpload.DataBind();


        }

        protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //e.Cancel = true;
            //gvUpload.EditIndex = -1;

            //gvUpload.DataSource = this.AttachFiles;
            //gvUpload.DataBind();
        }
    }
}