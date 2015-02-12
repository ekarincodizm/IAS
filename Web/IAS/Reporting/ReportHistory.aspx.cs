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
    public partial class ReportHistory : basepage
    {
        ReportDocument report = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string pettition = Request.QueryString["pettition"].Trim();
                string id_card = Request.QueryString["IDCard"].Trim();
                string txtfirsname = Request.QueryString["FirstName"].Trim();
                string txtlastname = Request.QueryString["LastName"].Trim();
                var biz = new BLL.PaymentBiz();
                //var res = biz.GetReportNumberPrintBill(txtIDCard.Text, ddlLicenseType.SelectedValue, txtFirstName.Text, txtLastName.Text, resultPage, PageSize, false);
                //var res = biz.GetReportNumberPrintBill(id_card, pettition, txtfirsname, txtlastname, 1, 1, false);
                var res = biz.GetDownloadReceiptHistory(id_card, pettition, txtfirsname, txtlastname);
                DataTable dt = (res.DataResponse != null && res.DataResponse.Tables[0].Rows.Count > 0) ? res.DataResponse.Tables[0] : null;
                if (dt != null)
                {
                    
                    report.Load((Server.MapPath("../Reports/RptHistory.rpt")));
                    report.SetDataSource(res.DataResponse.Tables[0]);
                    report.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
                    CRYreport.ReportSource = report;
                }
                else
                {
                    Response.Write("ไม่พบข้อมูล");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            report.Dispose();
        }
    }
}