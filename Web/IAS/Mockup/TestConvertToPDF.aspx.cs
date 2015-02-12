using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IAS.Class;

namespace IAS.Mockup
{
    public partial class TestConvertToPDF : System.Web.UI.Page
    {

        ReportDocument rptDoc = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {


            //upd.Update();
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            string rptPath = null;

            rptPath = Server.MapPath("~/Reports/RptRecive.rpt");

            rptDoc.Load(rptPath);

            CrystalReportViewer1.ReportSource = rptDoc;

            //rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\test.pdf");
            
            rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ExportedReport");
        }
    }
}