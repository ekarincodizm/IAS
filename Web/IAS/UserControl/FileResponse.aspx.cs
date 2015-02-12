using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO.FileService;
using System.IO;
using IAS.Utils;

namespace IAS.UserControl
{
    public partial class FileResponse : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack) {
                if (Request["req"] != null && Request["mode"] != null)
                {
                    String path = CryptoBase64.Decryption(Request["req"].ToString());
                    String mode = Request["mode"];
                    if (mode == "P")
                    {
                        Print(path);
                    }
                    else
                    {
                        Download(path);
                    }
                }
            }
        }


        public void Download(string FileName)
        {

            DownloadFileResponse download;

            using (FileService.FileTransferServiceClient fileService = new FileService.FileTransferServiceClient())
            {
                download = fileService.DownloadFile(new DownloadFileRequest()
                {
                    TargetContainer = "",
                    TargetFileName = FileName

                });
                Int32 indexS = download.FileName.IndexOf(@"\");  
                if (indexS <= 0)
                {
                    ShowDocument(download.FileByteStream, (long)download.Length, download.ContentType);
                }
                else {
                    
                    ShowDocument(download.FileByteStream, (long)download.Length, download.ContentType, download.FileName.Substring(indexS+1));
                }
                
            }
        }
        public void Print(string FileName)
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
                            String fileName = response.FileName.Substring(response.FileName.IndexOf(@"\"));
                            if (String.IsNullOrEmpty(fileName)) {
                                ViewDocument(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType);
                            }else
                                ViewDocument(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType, fileName);
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
        private void ViewDocument(Stream fileStream, Int64 length, String contentType, String fileName)
        {
            Context.Response.Buffer = false;

            byte[] buffer = new byte[length];
            long byteCount;


            while ((byteCount = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (Context.Response.IsClientConnected)
                {
                    Context.Response.AddHeader("filename", fileName);
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
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Document.zip" ); // change name by milk
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }

        private void ShowDocument(Stream fileStream, Int64 length, String contentType, String fileName)
        {
            byte[] img = new byte[(int)length];

            using (BinaryReader br = new BinaryReader(fileStream))
            {
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName); // change name by milk
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }
    }
}