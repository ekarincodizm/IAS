using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace IAS.Reports
{
    public partial class ResetReport : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // base.HasPermit();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + txtIDCard.Text + "','" + txtName.Text + "','" + txtLastName.Text + "')", true);

            /*string IDCard = Request.QueryString["IDCard"];
            string FirstName = Request.QueryString["FirstName"];
            string LastName = Request.QueryString["LastName"];
            //string Click = Request.QueryString["Click"];
            BLL.PersonBiz biz = new BLL.PersonBiz();
            var res = biz.GetStatisticResetPassword(txtIDCard.Text, txtName.Text, txtLastName.Text);
            DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;
            string MemberType = base.UserProfile.MemberType.ToString();
            // string MemberType = MemberType_Temp.ToString();
            string ReportFolder = base.ReportFilePath_Key;
            string PDF_Temp = base.PDFPath_Temp_Key;
            string PDF_OIC = base.PDFPath_OIC_Key;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(ReportFolder + "RptStatisticPassword.rpt"));
            rpt.DataDefinition.FormulaFields["UserName"].Text = String.Format("'{0}'", base.UserProfile.Name);
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
            CRYreset.ReportSource = rpt;
            CRYreset.DataBind();
            */
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtIDCard.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
        }

    }
}
