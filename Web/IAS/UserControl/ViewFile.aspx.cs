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
    public partial class ViewFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                try
                {
                    String targetImage = Request.QueryString["targetImage"].ToString();

                    DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = CryptoBase64.Decryption(targetImage) });

                    if (response.Code == "0000")
                    {
                        Int32 index = response.FileName.LastIndexOf(@"\");
                        String filename = response.FileName.Substring((index > 0) ? index + 1 : 0);
                        if (response.ContentType == Utils.ContentTypeHelper.MimeType(".pdf") 
                            || response.ContentType == Utils.ContentTypeHelper.MimeType(".doc")
                            || response.ContentType == Utils.ContentTypeHelper.MimeType(".docx")
                            || response.ContentType == Utils.ContentTypeHelper.MimeType(".xls")
                            || response.ContentType == Utils.ContentTypeHelper.MimeType(".xlsx") )

                        {
                            ShowDocument(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType, filename);
                        }
                        else if (response.ContentType == "application/octet-stream" && 
                            ( filename.Substring(filename.LastIndexOf('.'))==".docx"
                            || filename.Substring(filename.LastIndexOf('.')) == ".doc"
                            || filename.Substring(filename.LastIndexOf('.')) == ".xls"
                            || filename.Substring(filename.LastIndexOf('.')) == ".xlsx"
                            ))
                        {

                            ShowDocument(response.FileByteStream, Convert.ToInt64(response.Length), 
                                Utils.ContentTypeHelper.MimeType(filename.Substring(filename.LastIndexOf('.'))), filename);
                        }
                        else
                        {
                            ShowImage(response.FileByteStream, Convert.ToInt64(response.Length), response.ContentType, filename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }



        private void ShowImage(Stream fileStream, Int64 length, String contentType, String filename)
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

        private void ShowDocument(Stream fileStream, Int64 length, String contentType, String filename)
        {
            Context.Response.Buffer = false;

            byte[] buffer = new byte[length];
            long byteCount;


            while ((byteCount = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (Context.Response.IsClientConnected)
                {
                    Context.Response.ContentType = contentType;
                    Response.AddHeader("Content-Disposition", String.Format("inline;filename=\"{0}\"", filename));
                    Response.AddHeader("Content-Length", length.ToString());

                    Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Context.Response.Flush();
                    Context.Response.End();
                }
            }
        }

        private void LoadDocument(Stream fileStream, Int64 length, String contentType, String filename)
        {
            byte[] img = new byte[(int)length];

            using (BinaryReader br = new BinaryReader(fileStream))
            {
                string filenameOnly = (filename.IndexOf(@"\") >= 0) ? filename.Substring(filename.IndexOf(@"\")) : filename;//milk
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenameOnly); // change name by milk
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }
    }
}