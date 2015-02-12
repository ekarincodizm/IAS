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
using IAS.Utils;
using System.Net;
using System.Data;

namespace IAS.Reporting
{
    public partial class RptRecive : basepage
    {
        public class lsPrint
        {
            //public int str
            public string HEAD_REQUEST_NO { get; set; }
            public string GROUP_REQUEST_NO { get; set; }
            public string PERSON_NO { get; set; }
            public string GROUP_AMOUNT { get; set; }
            public string SUBPAYMENT_DATE { get; set; }
            public string REMARK { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            string Click = Request.QueryString["Click"];

            string ReportFolder = base.ReportFilePath_Key;

            //string PDFFolder = base.PDFPath_Key;

            string PDF_Temp = base.PDFPath_Temp_Key;

            string PDF_OIC = base.PDFPath_OIC_Key;

            string PDF_Users = base.PDFPath_Users_Key;

            string FileNameInput = "RptReceipt_Temp.pdf";

            string FileNameOutput = "RptReceipt.pdf";

            var ls = new List<RcvPaymentClass>();

            if (Click == "Print")
            {
                var para = Request.QueryString["Invoice"];

                var biz = new BLL.PaymentBiz();

                var res = biz.GetSubPaymentHeadByHeadRequestNo(para);

                if (!res.IsError)
                {
                    var item = res.DataResponse;

                    //var data = new List<lsPrint>();

                    //data.Add(new lsPrint
                    //{
                    //    HEAD_REQUEST_NO = head.HEAD_REQUEST_NO,
                    //    GROUP_REQUEST_NO = head.GROUP_REQUEST_NO,
                    //    PERSON_NO = head.PERSON_NO.ToString(),
                    //    GROUP_AMOUNT = head.SUBPAYMENT_AMOUNT.ToString(),
                    //    SUBPAYMENT_DATE = head.SUBPAYMENT_DATE.ToString(),
                    //    REMARK = head.REMARK
                    //});

                    //foreach (var item in data)
                    //{
                    RcvPaymentClass rcv = new RcvPaymentClass();


                    rcv.HEAD_REQUEST_NO = item.HEAD_REQUEST_NO;
                    rcv.GROUP_REQUEST_NO = item.GROUP_REQUEST_NO;
                    rcv.PERSON_NO = item.PERSON_NO.ToString();
                    rcv.GROUP_AMOUNT = item.SUBPAYMENT_AMOUNT.ToString();
                    rcv.SUBPAYMENT_DATE = item.SUBPAYMENT_DATE.ToString();
                    rcv.REMARK = item.REMARK;

                    
                    //rcv.ExpireDateShortString = (item.EXPIRATION_DATE==null) ? "": ((DateTime)item.EXPIRATION_DATE).ToShortDateString();
                    //rcv.ExpireDateString = (item.EXPIRATION_DATE==null) ? "": ((DateTime)item.EXPIRATION_DATE).ToLongDateString();

                    //var resDetail = biz.GetDetailSubPayment(item.HEAD_REQUEST_NO);
                    //DTO.DetailSubPayment detailSubPayment = new DTO.DetailSubPayment();
                    //if (resDetail.DataResponse.Tables[0].Rows.Count > 0)
                    //{
                    //    DataRow dataRow =   resDetail.DataResponse.Tables[0].Rows[0];
                    //    detailSubPayment = dataRow.MapToEntity<DTO.DetailSubPayment>();
                    //    rcv.PaymentBy = detailSubPayment.FIRSTLASTNAME;
                    //    rcv.Referance1No = item.GROUP_REQUEST_NO;
                    //    rcv.BankAccountNumber = "11010201010800";
                    //    rcv.PatitionName = detailSubPayment.PETITION_TYPE_NAME;
                        
                    //}
                    

                    ls.Add(rcv);
                    //}
                }

                if (base.UserProfile.MemberType==DTO.RegistrationType.General.GetEnumValue())
                {
                    ReportDocument rpt = new ReportDocument();

                    rpt.Load(Server.MapPath(ReportFolder + "RptReceipt.rpt"));

                    rpt.SetDataSource(ls);

                    BindReport(rpt);

                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_Users + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);


                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/

                        PdfEncryptor.Encrypt(reader, output, true, string.Empty, string.Empty, PdfWriter.AllowPrinting);
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
                    ReportDocument rpt = new ReportDocument();

                    rpt.Load(Server.MapPath(ReportFolder + "RptReceipt.rpt"));

                    rpt.SetDataSource(ls);

                    BindReport(rpt);

                    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                    using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader = new PdfReader(input);
                        /*ใส่ Password*/
                        //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                        /*ใส่ Password*/

                        PdfEncryptor.Encrypt(reader, output, true, string.Empty, string.Empty, PdfWriter.AllowPrinting);
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

        }

        private void BindReport(ReportDocument rpt)
        {
            this.RptReciveReportViewer.ReportSource = rpt;
            //this.RptReciveReportViewer.DataBind();
        }
    }
}