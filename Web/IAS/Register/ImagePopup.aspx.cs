using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO.FileService;
using System.IO;
using IAS.Utils;

namespace IAS
{
    public partial class ImagePopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                DownloadFileResponse response = new DownloadFileResponse();
                
                String targetImage =  Request.QueryString["targetImage"].ToString();

                response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = CryptoBase64.Decryption(targetImage) });


                if (response.Code == "0000") {
                    ShowImage(response.FileByteStream, Convert.ToInt64( response.Length), response.ContentType);
                }

               
            }

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
    }
}