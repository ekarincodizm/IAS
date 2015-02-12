using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using IAS.BLL;

namespace IAS.Reporting
{
    public partial class QuickReportViewer : basepage
    {
        private string PetitionType;
        private DateTime? DateStart;
        private DateTime? DateEnd;
        private string CompCode;
        private string Days;
        ReportDocument rpt = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDataToViewer();
        }

        protected void LoadDataToViewer()
        {
            try
            {
                PetitionType = Request.QueryString["PetitionType"].Trim();
                if (!String.IsNullOrEmpty(Request.QueryString["DateStart"].Trim()))
                {
                    DateStart = Convert.ToDateTime(Request.QueryString["DateStart"].ToString());
                }
                if (!String.IsNullOrEmpty(Request.QueryString["DateEnd"].Trim()))
                {
                    DateEnd = Convert.ToDateTime(Request.QueryString["DateEnd"].ToString());
                }
                CompCode = Request.QueryString["CompCode"].Trim();
                Days = Request.QueryString["Days"].Trim();

                LicenseBiz biz = new LicenseBiz();
                DataSet ds = biz.GetRenewLicenseQuick(PetitionType, DateStart, DateEnd, CompCode, Days).DataResponse;
                DataTable dt = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
                if (dt != null)
                {
                    rpt.Load(Server.MapPath(base.ReportFilePath_Key + "RptLicenseQuick.rpt"));
                    rpt.SetDataSource(ds.Tables[0]);
                    cryViewer.ReportSource = rpt;
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

        protected void Page_Unload(object sender, EventArgs e)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
}