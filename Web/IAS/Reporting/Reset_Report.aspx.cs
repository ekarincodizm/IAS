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
    public partial class Reset_Report : basepage
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
                string FirstName = Request.QueryString["FirstName"].Trim();
                string LastName = Request.QueryString["LastName"].Trim();
                string Click = Request.QueryString["Click"];

                BLL.PersonBiz biz = new BLL.PersonBiz();
                var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);
                DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;


                // string MemberType = MemberType_Temp.ToString();
                string ReportFolder = base.ReportFilePath_Key;
                string PDF_Temp = base.PDFPath_Temp_Key;
                string PDF_OIC = base.PDFPath_OIC_Key;
                //string FileNameInput = "RcpReport_Temp.pdf";
                //string FileNameOutput = "RcpReport.pdf";

                if (Click == "Print")
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                    {
                        //ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath(ReportFolder + "RptStatisticPassword.rpt"));
                        rpt.DataDefinition.FormulaFields["UserName"].Text = String.Format("'{0}'", base.UserProfile.Name);
                        rpt.SetDataSource(dt);
                        rpt.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
                        CRYreport.ReportSource = rpt;

                        #region Comment
                        //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(ReportFolder + "RptStatisticPassword.pdf"));
                        //BindReport(rpt);
                        //downloadexcel(rpt);
                        //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                        //using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                        //using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                        //{
                        //    PdfReader reader = new PdfReader(input);
                        //    PdfEncryptor.Encrypt(reader, output, true, "", "", PdfWriter.AllowPrinting);
                        //   // PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        //}

                        ////ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                        //string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                        //FileInfo File = new FileInfo(PathDelete);

                        //if (File.Exists)
                        //{
                        //    File.Delete();
                        //}
                        ////ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

                        //string FilePath = Server.MapPath(PDF_OIC + FileNameOutput);
                        //WebClient User = new WebClient();
                        //Byte[] FileBuffer = User.DownloadData(FilePath);
                        //if (FileBuffer != null)
                        //{
                        //    Response.ContentType = "application/pdf";
                        //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        //    Response.BinaryWrite(FileBuffer);
                        //}

                        //upd.Update();
                        #endregion
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
            rpt.SetParameterValue("datethai", DateTime.Now.ToShortDateString());
            CRYreport.ReportSource = rpt;
        }

        private void downloadexcel(ReportDocument rpt)
        {
            System.IO.Stream sream = rpt.ExportToStream(ExportFormatType.Excel);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Reset_Password_" + DateTime.Now.Date + ".xls");
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
