using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace IAS.UserControl
{
    public partial class FileApplicantUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                FileStream myStream = new FileStream(Server.MapPath("~/Reports/Structure.csv"), FileMode.Open, FileAccess.Read);

                ShowDocument(myStream, myStream.Length, Utils.ContentTypeHelper.MimeType(".csv"), "Structure.csv");
            }
            
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
                    Response.AddHeader("Content-Disposition", String.Format("inline;filename=\"{0}\"", filename));
                    Response.AddHeader("Content-Length", length.ToString());
                    Context.Response.ContentType = contentType;
                    Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Context.Response.Flush();
                }
            }

        }
    }
}