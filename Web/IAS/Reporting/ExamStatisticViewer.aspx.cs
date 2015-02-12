using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace IAS.Reporting
{
    public partial class ExamStatisticViewer : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDataToViewer();
        }

        ReportDocument rpt = new ReportDocument();
        protected void LoadDataToViewer()
        {
            string LicenseType = Request.QueryString["LicenseType"].ToString();
            DateTime? DateStart = null;
            DateTime? DateEnd = null;
            string StringDate = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["DateStart"].ToString()) &&
                !string.IsNullOrEmpty(Request.QueryString["DateEnd"].ToString()))
            {
                DateStart = Convert.ToDateTime(Request.QueryString["DateStart"].ToString());
                DateEnd = Convert.ToDateTime(Request.QueryString["DateEnd"].ToString());
                StringDate = "วันที่ " + ((DateTime)DateStart).ToLongDateString() + " ถึง " + ((DateTime)DateEnd).ToLongDateString();
            }
            try
            {
                BLL.ExamResultBiz biz = new BLL.ExamResultBiz();
                var res = biz.GetExamStatistic(LicenseType, DateStart, DateEnd);
                if (res.IsError)
                {
                    Response.Write(res.ErrorMsg);
                }
                else if (res.DataResponse != null)
                {
                    DataSet ds = res.DataResponse;
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       
                        rpt.Load(Server.MapPath(base.ReportFilePath_Key + "RptExamStatistic.rpt"));
                        rpt.SetDataSource(dt);
                        rpt.SetParameterValue("paramDate", StringDate);
                        cryViewer.ReportSource = rpt;
                    }
                    else
                    {
                        Response.Write("ไม่พบข้อมูลที่ค้นหา..");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            rpt.Dispose();
        }
    }
}