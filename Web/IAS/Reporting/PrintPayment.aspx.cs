using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace IAS.Reporting
{
    public partial class PrintPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //base.HasPermit();
            }
        }

        private void BindReport(ReportDocument rpt)
        {
            this.PrintPaymentCrystalReportViewer.ReportSource = rpt;
            this.PrintPaymentCrystalReportViewer.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetReport();
        }

        private void GetReport()
        {
            //string ReportFolder = base.ReportFilePath_Key;
            BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(txtIDCard.Text, txtName.Text, txtLastName.Text);
            ReportDocument rpt = new ReportDocument();
            //rpt.Load(Server.MapPath(ReportFolder + "RptPayment.rpt"));
            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;
            //rpt.DataDefinition.FormulaFields["Username"].Text = "'" + base.UserProfile.Name + "'";
            //rpt.SetDataSource(dt);
            BindReport(rpt);
        }
    }
}