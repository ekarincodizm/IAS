using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class StatisticPassword : basepage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // base.HasPermit();

                GetLicenseType();
            }
        }

        private void BindReport(ReportDocument rpt)
        {
            //this.ElectronicReceiptCrystalReportViewer.ReportSource = rpt;
            //this.ElectronicReceiptCrystalReportViewer.DataBind();

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            BLL.PaymentBiz biz = new BLL.PaymentBiz();
            var res = biz.GetReportNumberPrintBill(txtIDCard.Text, ddlLicenseType.SelectedValue, txtFirstName.Text, txtLastName.Text, 0, 0, false);

            DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;
            string ReportFolder = base.ReportFilePath_Key;

            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath(ReportFolder + "RptElectronicReceipt.rpt"));            
            report.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";
            report.SetDataSource(dt);
            report.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
            CRVStatic.ReportSource = report;
            CRVStatic.DataBind();
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetLicenseType()
        {
            var message = Resources.infoSysMessage_PleaseSelectAll;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType_NOias(message);

            BindToDDL(ddlLicenseType, ls.DataResponse.ToList());

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlLicenseType.SelectedValue = "";
            txtIDCard.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
        }

    }
}