using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace IAS.Mockup
{
    public partial class TestDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("http://download.uphaha.com/download/downloadFile.aspx?pku=2811BBAC94F4PEQ[1PXQXZC9NP75[&quot;, false, false))", "Test.pdf");
            } 
        }
    }
}