using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using iTextSharp.text.pdf;
using System.Net;
using IAS.Utils;
namespace IAS.Reporting
{
    public partial class Rcp_Report : basepage
    {
        ReportDocument rpt = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            try
            {
                string IDCard = Request.QueryString["IDCard"].Trim();
                string LicenseType = Request.QueryString["LicenseType"].Trim();
                string FirstName = Request.QueryString["FirstName"].Trim();
                string LastName = Request.QueryString["LastName"].Trim();
                string Click = Request.QueryString["Click"];

                BLL.PaymentBiz biz = new BLL.PaymentBiz();
                var res = biz.GetReportNumberPrintBill(IDCard, LicenseType, FirstName, LastName, 0, 0, false);
                DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;
                
                string ReportFolder = base.ReportFilePath_Key;
                if (Click == "Print")
                {
                    //if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    //    (base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue()))
                    if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) //แก้ตามเอกสารของ SA
                    {
                        rpt.Load(Server.MapPath(ReportFolder + "RptElectronicReceipt.rpt"));
                        rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";
                        rpt.SetDataSource(dt);
                        rpt.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
                        BindReport(rpt);
                        //downloadexcel(rpt);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        private void BindReport(ReportDocument rpt)                   
        {
            CRYreport.ReportSource = rpt;
        }

        private void downloadexcel(ReportDocument rpt)
        {
            System.IO.Stream sream = rpt.ExportToStream(ExportFormatType.Excel);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=History_Recive_" + DateTime.Now.Date + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            MemoryStream mem = new MemoryStream();
            sream.CopyTo(mem);
            Response.BinaryWrite(mem.ToArray());
            HttpContext.Current.Response.End();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
}
