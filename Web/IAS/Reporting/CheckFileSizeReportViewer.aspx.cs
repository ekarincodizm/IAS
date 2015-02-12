using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using IAS.BLL;

namespace IAS.Reporting
{
    public partial class CheckFileSizeReportViewer : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                PaymentBiz biz = new PaymentBiz();

                report.Load(Server.MapPath("../Reports/RptCheckFileSize.rpt"));
                report.SetDataSource(biz.GetCheckFileSize(Request.QueryString["TypePay"].ToString(), Request.QueryString["DateStart"].ToString(), Request.QueryString["DateEnd"].ToString()).DataResponse.Tables[0]);
                report.SetParameterValue("startdate", Request.QueryString["DateStart"].ToString());
                report.SetParameterValue("enddate", Request.QueryString["DateEnd"].ToString());
                if (Request.QueryString["DateStart"].ToString() == "" && Request.QueryString["DateEnd"].ToString() == "")
                {
                    report.SetParameterValue("lblSDate", "");
                    report.SetParameterValue("lblEDate", "");
                }
                else
                {
                    report.SetParameterValue("lblSDate", "วันที่สร้างใบเสร็จ(เริ่ม) :");
                    report.SetParameterValue("lblEDate", "วันที่สร้างใบเสร็จ(สิ้นสุด) :");
                }
                CrystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}