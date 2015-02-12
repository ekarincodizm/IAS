using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using IAS.BLL;
using System.Data;

namespace IAS.Reporting
{
    public partial class PopMoveComPanyIn : basepage
    {
        MoveCompanyInBiz biz = new MoveCompanyInBiz();
        ReportDocument report = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string start = Request.QueryString["startdate"].ToString();
                string enddate = Request.QueryString["enddate"].ToString();
                string license = Request.QueryString["licensetype"].ToString();
                string comcode = Request.QueryString["comcode"].ToString();
                string lincense_name = Request.QueryString["lincense_name"].ToString();
                
                report.Load(Server.MapPath(base.ReportFilePath_Key+ "RptMoveComPanyIn.rpt"));
                List<string> head = new List<string>();
                DataTable table = new DataTable();
                double sum = 0;
                bool check = false;
                biz.GetTable(ref head, ref table, ref sum, license, comcode, start, enddate, ref check);
                if (check)
                {
                    Response.Write("ไม่พบข้อมูลที่ค้นหา");
                }
                else
                {
                    report.SetDataSource(table);

                    for (int i = 1; i <= 25; i++)
                    {
                        if (i < head.Count + 1)
                        {
                            report.SetParameterValue("A" + i, head[i - 1]);
                        }
                        else
                        {
                            report.SetParameterValue("A" + i, "");
                        }
                    }
                    report.SetParameterValue("A20", "รวม");
                    report.SetParameterValue("sum", sum.ToString());
                    report.SetParameterValue("license_name", lincense_name);
                    report.SetParameterValue("startdate", Convert.ToDateTime(start).ToLongDateString());
                    report.SetParameterValue("enddate", Convert.ToDateTime(enddate).ToLongDateString());
                    CrystalReportViewer1.ReportSource = report;
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