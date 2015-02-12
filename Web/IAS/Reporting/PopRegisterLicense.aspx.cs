using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace IAS.Reporting
{
    public partial class PopRegisterLicense : basepage
    {
        ReportDocument report = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterLicenseBiz biz = new RegisterLicenseBiz();
                report.Load(Server.MapPath(base.ReportFilePath_Key + "RptRegisterLicense.rpt"));
                DataTable tb = biz.ReportRegisterLicense(Request.QueryString["licensetype"].ToString(), Request.QueryString["comcode"].ToString(), Request.QueryString["startdate"].ToString(), Request.QueryString["enddate"].ToString()).Tables[0];
                if (tb != null && tb.Rows.Count > 0)
                {
                    report.SetDataSource(tb);
                    report.SetParameterValue("startdate", Request.QueryString["startdate"].ToString());
                    report.SetParameterValue("enddate", Request.QueryString["enddate"].ToString());
                    CrystalReportViewer1.ReportSource = report;
                }
                else
                {
                    Response.Write("ไม่พบข้อมูล..");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            report.Close();
            report.Dispose();
        }
    }
}