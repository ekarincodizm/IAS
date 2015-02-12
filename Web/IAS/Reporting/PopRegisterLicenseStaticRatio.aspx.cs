using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using IAS.DTO;

namespace IAS.Reporting
{
    public partial class PopRegisterLicenseStaticRatio : basepage
    {
        ReportDocument report = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterLicenseBiz biz = new RegisterLicenseBiz();
                var res = biz.ReportRegisterLicenseStaticRatio(Request.QueryString["licensetype"].ToString(), Request.QueryString["startdate"].ToString(), Request.QueryString["enddate"].ToString(), Request.QueryString["startdate2"].ToString(), Request.QueryString["enddate2"].ToString());
                if (res.IsError)
                {
                    Response.Write("ไม่พบข้อมูลที่ค้นหา");
                }
                else
                {
                    report.Load(Server.MapPath("../Reports/RptLicenseStaticRatio.rpt"));
                    report.SetDataSource(res.DataResponse.Tables[0]);
                    report.SetParameterValue("startdate1", Convert.ToDateTime(Request.QueryString["startdate"].ToString()).ToLongDateString());
                    report.SetParameterValue("enddate1", Convert.ToDateTime(Request.QueryString["enddate"].ToString()).ToLongDateString());
                    report.SetParameterValue("startdate2", Convert.ToDateTime(Request.QueryString["startdate2"].ToString()).ToLongDateString());
                    report.SetParameterValue("enddate2", Convert.ToDateTime(Request.QueryString["enddate2"].ToString()).ToLongDateString());
                    CrystalReportViewer1.ReportSource = report;
                    CrystalReportViewer1.DataBind();
                }
            }
            catch { }
        }

        protected override void OnUnload(EventArgs e)
        {
            report.Dispose();
        }
    }
}