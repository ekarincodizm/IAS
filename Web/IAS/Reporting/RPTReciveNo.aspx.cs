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
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class RPTReciveNo : basepage
    {
        public List<DTO.ReceiveNo> lsPaymentPrint
        {
            get
            {
                if (Session["lsPaymentPrint"] == null)
                {
                    Session["lsPaymentPrint"] = new List<DTO.ReceiveNo>();
                }

                return (List<DTO.ReceiveNo>)Session["lsPaymentPrint"];
            }
            set
            {
                Session["lsPaymentPrint"] = value;
            }
        }

        public class lsPrint
        {
            //public int str
            public string HEAD_REQUEST_NO { get; set; }
            public string GROUP_REQUEST_NO { get; set; }
            public string PERSON_NO { get; set; }
            public string GROUP_AMOUNT { get; set; }
            public string SUBPAYMENT_DATE { get; set; }
            public string REMARK { get; set; }
            public byte[] SigImgPath { get; set; } //milk
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

            string FileNameInput = "RptRecive.pdf";

            string FileNameOutput = "RptRecive.pdf";

            var ls = new List<RptReciveClassService>();
            var data = new List<DTO.SubPaymentDetail>();
            string mapPath = "~/IASFileUpload";
            if (Click == "Print")
            {
                //var para = Request.QueryString["Invoice"];

                //var biz = new BLL.PaymentBiz();
               
                for(int i = 0 ; i < lsPaymentPrint.Count ; i++){

                    RptReciveClassService rcv = new RptReciveClassService();

                    BLL.FileBiz bizfile = new BLL.FileBiz();
                        //   // string dd = item.ReceiptNumber;
                        rcv.BillNumber = lsPaymentPrint[i].ReceiptNumber;
                        rcv.ReceiptDate = lsPaymentPrint[i].ReceiptDate;
                        rcv.FirstName = lsPaymentPrint[i].FirstName;
                        rcv.LastName = lsPaymentPrint[i].LastName;
                        rcv.AMOUNT = lsPaymentPrint[i].Amt;
                        rcv.PaymentType = lsPaymentPrint[i].PaymentType;
                        rcv.BathThai = ConvertMoneyToThai(rcv.AMOUNT);
                       
                        rcv.SigImgPathArray = bizfile.Signature_Img(Page.Response, "",lsPaymentPrint[i].SigImgPath.ToString());
                        rcv.QRcordPathArray = bizfile.Signature_Img(Page.Response, "", lsPaymentPrint[i].QRcordPath.ToString());
                        rcv.GUID = lsPaymentPrint[i].GUID;
                        ls.Add(rcv);
                        ////data.Add(new DTO.SubPaymentDetail
                        ////{
                        ////    HEAD_REQUEST_NO = lsPaymentPrint[i].HEAD_REQUEST_NO,
                        ////    PAYMENT_NO = lsPaymentPrint[i].PAYMENT_NO,
                        ////    Click = "Print"
                        ////});
                    
                }
                //data.Add(new DTO.SubPaymentDetail
                //{
                //    HEAD_REQUEST_NO = lsPaymentPrint[1].HEAD_REQUEST_NO,
                //    PAYMENT_NO = lsPaymentPrint[1].PAYMENT_NO,
                //    Click = "Print"
                //});
                if (base.UserProfile.MemberType==DTO.RegistrationType.General.GetEnumValue())
                {

                    var biz = new BLL.PaymentBiz();
                    var res = biz.PrintDownloadCount(data.ToArray(),"",base.UserId);

                    ReportDocument rpt = new ReportDocument();

                    rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));
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

                    var biz = new BLL.PaymentBiz();
                    var res = biz.PrintDownloadCount(data.ToArray(),"",base.UserId);
                    ReportDocument rpt = new ReportDocument();

                    rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));

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
            else if (Click == "Download")
            {
    
                for(int i = 0 ; i < lsPaymentPrint.Count ; i++){

                    RptReciveClassService rcv = new RptReciveClassService();
                  //  FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient();
                //   // string dd = item.ReceiptNumber;
                    BLL.FileBiz bizfile = new BLL.FileBiz();
                    rcv.BillNumber = lsPaymentPrint[i].ReceiptNumber;
                    rcv.ReceiptDate = lsPaymentPrint[i].ReceiptDate;
                    rcv.FirstName = lsPaymentPrint[i].FirstName;
                    rcv.LastName = lsPaymentPrint[i].LastName;
                    rcv.AMOUNT = lsPaymentPrint[i].Amt;
                    rcv.PaymentType = lsPaymentPrint[i].PaymentType;
                    rcv.BathThai = ConvertMoneyToThai(rcv.AMOUNT);
              
                    rcv.SigImgPathArray = bizfile.Signature_Img(Page.Response, "", lsPaymentPrint[i].SigImgPath.ToString());
                    ls.Add(rcv);

                    data.Add(new DTO.SubPaymentDetail
                {
                    HEAD_REQUEST_NO = lsPaymentPrint[i].HEAD_REQUEST_NO,
                    PAYMENT_NO = lsPaymentPrint[i].PAYMENT_NO,
                    Click = "Download"
                });
                }



                if (base.UserProfile.MemberType==DTO.RegistrationType.General.GetEnumValue())
                {
                    GenPDFDownload1(ReportFolder, PDF_OIC, FileNameInput, ls, data);

                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileNameOutput);
                    Response.TransmitFile(Server.MapPath(PDF_OIC + FileNameOutput));

                    Response.End();

                    upd.Update();

                }
                else
                {
                    GenPDFDownload2(ReportFolder, PDF_OIC, FileNameInput, ls, data);
                    #region TempCode
                    //using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))

                    //using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                    //{
                    //    //  PdfReader reader = new PdfReader(input);

                    //    /*ใส่ Password*/
                    //    //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                    //    /*ใส่ Password*/
                    //}

                    ////ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                    //string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                    //FileInfo File = new FileInfo(PathDelete);

                    //if (File.Exists)
                    //{
                    //    File.Delete();
                    //}

                    #endregion

                    //Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileNameOutput);
                    //Response.TransmitFile(Server.MapPath(PDF_Temp + FileNameOutput));
                    //Response.TransmitFile(Server.MapPath(PDF_OIC + FileNameOutput));
                   //string passDownload =   String.Format("window.open('{0}?targetImage={1}','','')"
                   //         , UrlHelper.Resolve("/UserControl/PDFRender.aspx"), IAS.Utils.CryptoBase64.Encryption("RptRecive.pdf"));
                   //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "alert", passDownload, true);
                    string path = Server.MapPath("~/PDF/PDF_OIC/") + FileNameInput;
                    String FileName = FileNameInput;
                    String FilePath = "~/PDF/PDF_OIC/"; //Replace this

                    
                    string dd = "http://www.qworld-plus.com/download/QWorldPlus_Patch_V0.14.163.exe";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + dd + "','_blank')", true);
                    //Response.Redirect("" + dd + "");
                    string ff = "window.open('http://www.qworld-plus.com/download/QWorldPlus_Patch_V0.14.163.exe');";

                    string newwin = "window.open('http://www.qworld-plus.com/download/QWorldPlus_Patch_V0.14.163.exe' , 'mypopup1' , 'nenuber=no,toorlbar=no,location=no,scrollbars=no, status=no,resizable=no,width=180,height=180,top=220,left=650 ' );";
                    AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup",
                        newwin, true);
                    //Response.Write("<script>window.open('" + path + "');<script>");
                    //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    //response.ClearContent();
                    //response.Clear();
                    //response.ContentType = "Application/pdf";
                    //response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                    //response.TransmitFile(FilePath + FileName);
                    //response.Flush();
                    
                    //response.End();
                   // Response.End();

                    //upd.Update();
                }

            }
        }

        private void GenPDFDownload1(string ReportFolder, string PDF_OIC, string FileNameInput, List<RptReciveClassService> ls, List<DTO.SubPaymentDetail> data)
        {
            var biz = new BLL.PaymentBiz();
            var res = biz.PrintDownloadCount(data.ToArray(),"",base.UserId);

            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));

            rpt.SetDataSource(ls);

            BindReport(rpt);
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_OIC + FileNameInput));
        }

        private void GenPDFDownload2(string ReportFolder, string PDF_OIC, string FileNameInput, List<RptReciveClassService> ls, List<DTO.SubPaymentDetail> data)
        {
            var biz = new BLL.PaymentBiz();
            var res = biz.PrintDownloadCount(data.ToArray(),"",base.UserId);

            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(ReportFolder + "RptRecive.rpt"));

            rpt.SetDataSource(ls);

            BindReport(rpt);

            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_OIC + FileNameInput));
        }

        private void BindReport(ReportDocument rpt)
        {
            string Click = Request.QueryString["Click"];

            if (Click == "Print")
            {
                this.RptReciveReportViewer.ReportSource = rpt;
                //this.RptReciveReportViewer.DataBind();
            }
            else if (Click == "Download")
            {
                this.RptReciveReportViewer.ReportSource = rpt;
            }
        }
        public string ConvertMoneyToThai(string Amt)
        {
            string[] NumberWord;
            string[] NumberWord2;
            string Num3 = "";
            string ProcessValue;
            NumberWord = Amt.Split('.');
            NumberWord2 = NumberWord[0].Split(',');
            for (int i = 0; i <= NumberWord2.Length - 1; i++)
            {
                Num3 = Num3 + NumberWord2[i];
            }
            ProcessValue = SplitWord(Num3);
            if (NumberWord.Length > 1)
            {
                if (int.Parse(NumberWord[1]) > 0)
                {
                    ProcessValue = ProcessValue + Resources.propRptBillPayment_001 + SplitWord(NumberWord[1]) + Resources.propRptBillPayment_002;
                }
                else
                {
                    ProcessValue = ProcessValue + Resources.propRptBillPayment_003;
                }
            }
            else
            {
                ProcessValue = ProcessValue + Resources.propRptBillPayment_003;
            }
            return ProcessValue;
        }
        public string SplitWord(string numberVar)
        {
            int i = numberVar.Length;
            int k = 0;
            int n = i;
            int m = i;
            int b = 6;
            //char value2;
            char[] value1;
            string CurrencyWord = "";
            value1 = numberVar.ToCharArray();
            for (int a = 0; a <= i; a = a + 7)
            {
                if (n <= a + 7 && n > 0)
                {
                    b = n - 1;
                    if (i > 7)
                    {
                        k = 1;
                    }
                }
                else
                {
                    b = 6;
                }
                if (n > 0)
                {
                    for (int j = 0; j <= b; j++)
                    {
                        n--;
                        k++;
                        CurrencyWord = GetWord(value1[n].ToString(), k) + CurrencyWord;
                    }
                }
            }
            return CurrencyWord;
        }
        public string GetWord(string str1, int Num1)
        {
            string value1 = GetCurrency(Num1);
            switch (str1)
            {
                case "1":
                    if (Num1 == 1)
                    {
                        value1 = value1 + "เอ็ด";
                    }
                    else if (Num1 > 2)
                    {
                        value1 = "หนึ่ง" + value1;
                    }
                    break;
                case "2":
                    if (Num1 == 2)
                    {
                        value1 = "ยี่" + value1;
                    }
                    else
                    {
                        value1 = "สอง" + value1;
                    }
                    break;
                case "3":
                    value1 = "สาม" + value1;
                    break;
                case "4":
                    value1 = "สี่" + value1;
                    break;
                case "5":
                    value1 = "ห้า" + value1;
                    break;
                case "6":
                    value1 = "หก" + value1;
                    break;
                case "7":
                    value1 = "เจ็ด" + value1;
                    break;
                case "8":
                    value1 = "แปด" + value1;
                    break;
                case "9":
                    value1 = "เก้า" + value1;
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
        public string GetCurrency(int Num2)
        {
            string value1;
            switch (Num2)
            {
                case 1:
                    value1 = "";
                    break;
                case 2:
                    value1 = "สิบ";
                    break;
                case 3:
                    value1 = "ร้อย";
                    break;
                case 4:
                    value1 = "พัน";
                    break;
                case 5:
                    value1 = "หมื่น";
                    break;
                case 6:
                    value1 = "แสน";
                    break;
                case 7:
                    value1 = "ล้าน";
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
    }
}

