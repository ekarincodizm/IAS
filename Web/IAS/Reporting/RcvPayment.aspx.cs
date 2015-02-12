using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using iTextSharp.text.pdf;
using System.Net;
using IAS.Utils;

namespace IAS.Reporting
{
    public partial class RcvPayment : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void BindReport(ReportDocument rpt)
        {
            string Click = Request.QueryString["Click"];

            if (Click == "Print")
            {
                this.RptPaymentReportViewer.ReportSource = rpt;
                //this.RptPaymentReportViewer.DataBind();
            }
            else if (Click == "Download")
            {
                this.RptPaymentReportViewer.ReportSource = rpt;
            }

        }

        private void InitData()
        {
            var MySess = (List<IAS.Payment.PaymentDetail.lsPrint>)Session["lsPaymentPrint"];

            string Click = Request.QueryString["Click"];

            string ReportFolder = base.ReportFilePath_Key;

            //string PDFFolder = base.PDFPath_Key;

            string PDF_Temp = base.PDFPath_Temp_Key;

            string PDF_OIC = base.PDFPath_OIC_Key;

            string PDF_Users = base.PDFPath_Users_Key;

            string FileNameInput = "RecivePayment_Temp.pdf";

            string FileNameOutput = "RecivePayment.pdf";

            var ls = new List<RptReciveClassService>();

            var data = new List<DTO.SubPaymentDetail>();

            if (Click == "Print")
            {
                foreach (var item in MySess)
                {
                    RptReciveClassService rcv = new RptReciveClassService();

                    rcv.BillNumber = item.strBillNumber;
                    rcv.PaymentType = item.strPaymentType;
                    rcv.IDNumber = item.strIDNumber;
                    rcv.FirstName = item.strFirstName;
                    rcv.LastName = item.strLastName;
                    rcv.TestingNo = item.strTestingNo;
                    rcv.CompanyCode = item.strCompanyCode;
                    rcv.ExamPlaceCode = item.strExamPlaceCode;

                    ls.Add(rcv);

                    data.Add(new DTO.SubPaymentDetail
                    {
                        ID_CARD_NO = item.strIDNumber,
                        TESTING_NO = item.strTestingNo,
                        COMPANY_CODE = item.strCompanyCode,
                        EXAM_PLACE_CODE = item.strExamPlaceCode,
                        Click = "Print"
                    });

                }

                var biz = new BLL.PaymentBiz();
                var res = biz.PlusPrintDownloadCount(data.ToArray());

                ReportDocument rpt = new ReportDocument();

                rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));

                rpt.SetDataSource(ls);

                BindReport(rpt);

                if (base.UserProfile.MemberType==DTO.RegistrationType.General.GetEnumValue())
                {
                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_Users + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);

                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/
                    }

                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                    string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                    FileInfo File = new FileInfo(PathDelete);

                    if (File.Exists)
                    {
                        File.Delete();
                    }
                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

                    string FilePath = Server.MapPath(PDF_Users + FileNameOutput);
                    WebClient User = new WebClient();
                    Byte[] FileBuffer = User.DownloadData(FilePath);
                    if (FileBuffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        Response.BinaryWrite(FileBuffer);
                    }

                    upd.Update();
                }
                else
                {
                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);

                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/
                    }

                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                    string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                    FileInfo File = new FileInfo(PathDelete);

                    if (File.Exists)
                    {
                        File.Delete();
                    }
                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

                    string FilePath = Server.MapPath(PDF_OIC + FileNameOutput);
                    WebClient User = new WebClient();
                    Byte[] FileBuffer = User.DownloadData(FilePath);
                    if (FileBuffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        Response.BinaryWrite(FileBuffer);
                    }

                    upd.Update();
                }


            }
            else if (Click == "Download")
            {
                foreach (var item in MySess)
                {
                    RptReciveClassService rcv = new RptReciveClassService();

                    rcv.BillNumber = item.strBillNumber;
                    rcv.PaymentType = item.strPaymentType;
                    rcv.IDNumber = item.strIDNumber;
                    rcv.FirstName = item.strFirstName;
                    rcv.LastName = item.strLastName;
                    rcv.TestingNo = item.strTestingNo;
                    rcv.CompanyCode = item.strCompanyCode;
                    rcv.ExamPlaceCode = item.strExamPlaceCode;

                    ls.Add(rcv);

                    data.Add(new DTO.SubPaymentDetail
                    {
                        ID_CARD_NO = item.strIDNumber,
                        TESTING_NO = item.strTestingNo,
                        COMPANY_CODE = item.strCompanyCode,
                        EXAM_PLACE_CODE = item.strExamPlaceCode,
                        Click = "Download"
                    });

                }

                var biz = new BLL.PaymentBiz();
                var res = biz.PlusPrintDownloadCount(data.ToArray());

                ReportDocument rpt = new ReportDocument();

                rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));

                rpt.SetDataSource(ls);

                BindReport(rpt);

                if (base.UserProfile.MemberType==DTO.RegistrationType.General.GetEnumValue())
                {
                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_Users + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);

                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/
                    }

                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                    string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                    FileInfo File = new FileInfo(PathDelete);

                    if (File.Exists)
                    {
                        File.Delete();
                    }
                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง


                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileNameOutput);
                    Response.TransmitFile(Server.MapPath(PDF_Users + FileNameOutput));

                    Response.End();

                    upd.Update();

                }
                else
                {
                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);

                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/
                    }

                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                    string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                    FileInfo File = new FileInfo(PathDelete);

                    if (File.Exists)
                    {
                        File.Delete();
                    }
                    //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง


                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileNameOutput);
                    Response.TransmitFile(Server.MapPath(PDF_OIC + FileNameOutput));

                    Response.End();

                    upd.Update();
                }

            }


        }
    }
}