using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO.FileService;
using System.IO;
using IAS.Utils;
using System.Text;

namespace IAS.UserControl
{
    public partial class SavePdfFileFromStream : System.Web.UI.Page
    {
        string txt_path = string.Empty; // by milk
        protected void Page_Load(object sender, EventArgs e)
        {
            string dwPDF = Session["FileName"].ToString();
            string[] chk = dwPDF.Split('-');
            string Path = chk[0];
            string ChkCase = chk[1];
            txt_path = Path; // by milk
            if (ChkCase == "P")
            {
                PrintPDF(Path);
            }
            else
            {
                DownloadPDF(Path);
            }
        }

        public void DownloadPDF(string FileName)
        {
           
            DownloadFileResponse download;
            
            using (FileService.FileTransferServiceClient fileService = new FileService.FileTransferServiceClient())
            {
                download = fileService.DownloadFile(new DownloadFileRequest()
                {
                    TargetContainer = "",
                    TargetFileName = FileName

                });

                ShowDocument(download.FileByteStream, (long)download.Length, download.ContentType);
               
            }
            
            
        }
        public void PrintPDF(string FileName)
        {

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                try
                {
                    DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = FileName });

                    if (response.Code == "0000")
                    {
                        if (response.ContentType == "application/pdf")
                        {
                            ViewDocument(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
        }
        private void ViewDocument(Stream fileStream, Int64 length, String contentType)
        {
            Context.Response.Buffer = false;

            byte[] buffer = new byte[length];
            long byteCount;

            //StreamReader sr = new StreamReader("XXX.shb", Encoding.GetEncoding("windows-874"));
            //BinaryReader br = new BinaryReader(sr.BaseStream);

            while ((byteCount = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (Context.Response.IsClientConnected)
                {
                    Context.Response.ContentType = contentType;
                    Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Context.Response.Flush();
                }
            }
        }

        private void ShowDocument(Stream fileStream, Int64 length, String contentType)
        {
            byte[] img = new byte[(int)length];

            using (BinaryReader br = new BinaryReader(fileStream))
            {
                string a = (txt_path.IndexOf("_")>=0)? txt_path.Substring(txt_path.IndexOf("_")):txt_path;//milk
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PaymentDetail" + a); // change name by milk
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }
    }
}