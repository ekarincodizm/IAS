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
    public partial class PDFRender : System.Web.UI.Page  
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             String targetImage =  Request.QueryString["targetImage"].ToString();
            String pdfFile = CryptoBase64.Decryption(targetImage);
            
           // Response.Redirect(String.Format("~/PDF/PDF_OIC/{0}", pdfFile));


            //using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            //{
            //    DownloadFileResponse response = new DownloadFileResponse();
                
            //    String targetImage =  Request.QueryString["targetImage"].ToString();

            //    response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = CryptoBase64.Decryption(targetImage) });



            //    if (response.Code == "0000")
            //    {
            //        if (response.ContentType == "application/pdf")
            //        {
            //            ShowDocument(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType);
            //        }
            //        else
            //        {
            //            ShowImage(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType);
            //        }

            //    }

               
            //}

        }

        private void ShowImage(Stream fileStream, Int64 length, String contentType)
        {

            MemoryStream ms = new MemoryStream();
            using (fileStream)
            {
                ms.SetLength(length);
                fileStream.Read(ms.GetBuffer(), 0, (int)length);
            }

            Response.Expires = 0;
            Response.Buffer = false;
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentType = contentType;
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
        }

        private void ShowDocument(Stream fileStream, Int64 length, String contentType)
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
    }
}