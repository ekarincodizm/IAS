using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using IAS.BLL;
using System.Data;
using IAS.Class;
using System.IO;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using IAS.Common.Logging;


namespace IAS.Reporting
{
    public partial class LicenseStatisticsReportViewer : basepage
    {
        double COUNT_COME_CODE1;
        double COUNT_COME_CODE2;
        double sum1;
        double sum2;
        double share1;
        double share2;
        double CompareLicense;
        ReportDocument rpt = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var ls = new List<RptLicenseStatisticsService>();
                ReportDocument report = new ReportDocument();
                // LicenseBiz biz = new LicenseBiz();
                string ddlLicenseType = Request.QueryString["LicenseType"].ToString();
                string dateStart1 = Request.QueryString["DateStart1"].ToString();
                string dateStart2 = Request.QueryString["DateStart2"].ToString();
                string dateEnd1 = Request.QueryString["DateEnd1"].ToString();
                string dateEnd2 = Request.QueryString["DateEnd2"].ToString();

                int x, y = 6, countT;
                if (ddlLicenseType == "00")
                    x = 4;
                else
                    x = 1;
                string[,] LicenseStatistics = new string[x, y];

                for (countT = 1; countT <= x; countT++)
                {
                    RptLicenseStatisticsService rcv = new RptLicenseStatisticsService();

                    string temp = "";
                    if (x == 1)
                        temp = ddlLicenseType;
                    else
                        temp = "0" + countT.ToString();


                    #region code
                    LicenseBiz biz = new LicenseBiz();
                    var res1 = biz.GetLicenseStatisticsReport(temp, dateStart1, dateEnd1, dateStart2, dateEnd2);
                    if (res1.DataResponse.Tables[0].Rows.Count != 0)
                    {
                        DataTable DT1 = res1.DataResponse.Tables[0];
                        DataRow DR1 = DT1.Rows[0];
                        COUNT_COME_CODE1 = Convert.ToDouble(DR1["COUNT_COME_CODE1"]);
                    }
                    var res2 = biz.GetLicenseStatisticsReport(temp, dateStart2, dateEnd2, dateStart2, dateStart2);
                    if (res2.DataResponse.Tables[0].Rows.Count != 0)
                    {
                        DataTable DT1 = res2.DataResponse.Tables[0];
                        DataRow DR1 = DT1.Rows[0];
                        COUNT_COME_CODE2 = Convert.ToDouble(DR1["COUNT_COME_CODE1"]);
                    }
                    var varsum1 = biz.GetSumLicenseStatisticsReport(dateStart1, dateEnd1);
                    if (varsum1.DataResponse.Tables[0].Rows.Count != 0)
                    {
                        DataTable DT1 = varsum1.DataResponse.Tables[0];
                        DataRow DR1 = DT1.Rows[0];
                        sum1 = Convert.ToDouble(DR1["COUNT_COME_CODE1"]);
                    }
                    var varsum2 = biz.GetSumLicenseStatisticsReport(dateStart2, dateEnd2);
                    if (varsum2.DataResponse.Tables[0].Rows.Count != 0)
                    {
                        DataTable DT1 = varsum2.DataResponse.Tables[0];
                        DataRow DR1 = DT1.Rows[0];
                        sum2 = Convert.ToDouble(DR1["COUNT_COME_CODE1"]);
                    }
                    share1 = sum1 == 0 ? 0 : COUNT_COME_CODE1 * 100 / sum1;
                    share2 = sum2 == 0 ? 0 : COUNT_COME_CODE2 * 100 / sum2;
                    CompareLicense = share2 - share1;
                    #endregion code

                    if (x == 1)
                        rcv.LicenseType = LicenseTypeName(temp);
                    else
                    {

                        rcv.LicenseType = LicenseTypeName(temp);
                    }
                    rcv.CountLicense1 = COUNT_COME_CODE1;
                    rcv.Share1 = share1;
                    rcv.CountLicense2 = COUNT_COME_CODE2;
                    rcv.Share2 = share2;
                    rcv.CompareLicense = CompareLicense;
                    ls.Add(rcv);
                }

                string ReportFolder = base.ReportFilePath_Key;
                string PDF_Temp = base.PDFPath_Temp_Key;
                //string FileNameInput = "RptLicenseStatistics_Temp.pdf";
                string PDF_Users = base.PDFPath_Users_Key;
                //string FileNameOutput = "RptLicenseStatistics.pdf";


                rpt.Load(Server.MapPath(ReportFolder + "RptLicenseStatistics.rpt"));

                rpt.SetDataSource(ls);


                string date_st1 = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateStart1"].ToString(), '/');
                string date_en1 = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateEnd1"].ToString(), '/');
                string date_st2 = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateStart2"].ToString(), '/');
                string date_en2 = Utils.ConvertCustom.ConvertToTxtThai(Request.QueryString["DateEnd2"].ToString(), '/');


                rpt.SetParameterValue("startdate1", date_st1);
                rpt.SetParameterValue("enddate1", date_en1);
                rpt.SetParameterValue("startdate2", date_st2);
                rpt.SetParameterValue("enddate2", date_en2);


                BindReport(rpt);
            }
            catch (Exception ex)
            {
                Response.Write("โปรดติดต่อผู้ดูแลระบบ");
                LoggerFactory.CreateLog().Fatal("LicenseReportViewer", ex);
            }

            #region ทำเป็นไฟล์ PDF
            //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));


            //using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream output = new FileStream(Server.MapPath(PDF_Users + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    PdfReader reader = new PdfReader(input);


            //    /*ใส่ Password*/
            //    //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
            //    /*ใส่ Password*/

            //    PdfEncryptor.Encrypt(reader, output, true, string.Empty, string.Empty, PdfWriter.AllowPrinting);
            //}

            ////ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
            //string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
            //FileInfo File = new FileInfo(PathDelete);

            //if (File.Exists)
            //{
            //    File.Delete();
            //}
            ////ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

            //string FilePath = Server.MapPath(PDF_Users + FileNameOutput);
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
            

            //report.Load(Server.MapPath("../Reports/RptLicenseStatistics.rpt"));
            //report.SetDataSource(biz.GetLicenseStatisticsReport(Request.QueryString["LicenseType"].ToString(), Request.QueryString["DateStart1"].ToString(), Request.QueryString["DateEnd1"].ToString(), Request.QueryString["DateStart2"].ToString(), Request.QueryString["DateEnd2"].ToString()).DataResponse.Tables[0]);
            //report.SetParameterValue("startdate1", Request.QueryString["DateStart1"].ToString());
            //report.SetParameterValue("enddate1", Request.QueryString["DateEnd1"].ToString());
            //report.SetParameterValue("startdate2", Request.QueryString["DateStart2"].ToString());
            //report.SetParameterValue("enddate2", Request.QueryString["DateEnd2"].ToString());
            //report.SetParameterValue("LicenseName", Session["license"].ToString());
            

            //CrystalReportViewer1.ReportSource = report;
        }

        protected string LicenseTypeName(string code)
        {
            string Name = "";
            switch(code)
            {
                case "01": Name = "ตัวแทนประกันชีวิต"; break;
                case "02": Name = "ตัวแทนประกันวินาศภัย"; break;
                case "03": Name = "นายหน้าประกันชีวิต (บุคคลธรรมดา)"; break;
                default: Name = "นายหน้าประกันวินาศภัย (บุคคลธรรมดา)"; break;
            }
            return Name;
        }
        private void BindReport(ReportDocument rpt)
        {
            this.RptReciveReportViewer.ReportSource = rpt;
            //this.RptReciveReportViewer.DataBind();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
}