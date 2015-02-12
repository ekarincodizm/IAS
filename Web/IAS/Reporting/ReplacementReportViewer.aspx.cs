using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using IAS.BLL;
using IAS.Common.Logging;

namespace IAS.Reporting
{
    public partial class ReplacementReportViewer : basepage
    {
        ReportDocument report = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                LicenseBiz biz = new LicenseBiz();
                report.Load(Server.MapPath("../Reports/RptReplacementReport.rpt"));

                var dt  = biz.GetReplacementReport(Request.QueryString["LicenseType"].ToString(), Request.QueryString["CompCode"].ToString(), Request.QueryString["Replacement"].ToString(), Request.QueryString["DateStart"].ToString(), Request.QueryString["DateEnd"].ToString()).DataResponse.Tables[0];


                if (dt.Rows.Count > 0)
                {
                    report.SetDataSource(dt);

                    string date_st = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateStart"].ToString(), '/');
                    string date_en = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateEnd"].ToString(), '/');

                    report.SetParameterValue("startdate", date_st);
                    report.SetParameterValue("enddate", date_en);

                    report.SetParameterValue("replacemant", Session["ReplaceName"].ToString());
                    CrystalReportViewer1.ReportSource = report;
                }
                else
                {
                    Response.Write("ไม่พบข้อมูล");
                }
            }
            catch (Exception ex)
            {
                Response.Write("โปรดติดต่อผู้ดูแลระบบ");
                LoggerFactory.CreateLog().Fatal("LicenseReportViewer", ex);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            report.Dispose();
        }
    }
}