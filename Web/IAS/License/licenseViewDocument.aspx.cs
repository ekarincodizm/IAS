using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace IAS.License
{
    public partial class licenseViewDocument : System.Web.UI.Page
    {
        private DTO.DataActionMode DataAction
        {
            get
            {
                return Session["UserProfile"] == null ? DTO.DataActionMode.Add : DTO.DataActionMode.Edit;
            }
        }

        private string FileName = string.Empty;
        private string UserProfile = string.Empty;
        private string TempFolderOracle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.FileName = Session["ViewFileName"].ToString();
            this.TempFolderOracle = Session["TempFolderOracle"].ToString();

            Session.Remove("ViewFileName");
            ViewDocument();

        }

        private void ViewDocument()
        {

            string fName = this.FileName;
            string TFolderOracle = this.TempFolderOracle;

            string path = Server.MapPath("~/UploadFile/RecieveLicense/" + TFolderOracle + "/" + fName);

            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            string ext = fi.Extension.ToUpper().Replace(".", "");

            if (ext == DTO.DocumentFileType.PDF.ToString())
            {
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);

                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                }
            }
            else if (DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().Contains(ext))
            {
                img.ImageUrl = "~/UploadFile/RecieveLicense/" + TFolderOracle + "/" + fName;
            }
            else
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + this.FileName);
                Response.TransmitFile(Server.MapPath("~/UploadFile/RecieveLicense/" + TFolderOracle + "/" + fName));
                Response.End();
            }

        }
    }
}