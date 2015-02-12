using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IAS.Properties;

namespace IAS.Mockup
{
    public partial class testImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AttachmentsDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            // Entities ctx = new Entities();
            byte[] myFileBytes = null;
            FileUpload myUpload = (FileUpload)DetailsView1.FindControl("FileUpload1");


            // Need  if not post back

            if (myUpload.HasFile)
                try
                {
                    myFileBytes = myUpload.FileBytes;

                    e.Values["REQUEST_ID"] = Convert.ToDecimal(Session["ccREQUESTID"]);
                    e.Values["DATA"] = myFileBytes;
                    e.Values["FILE_NAME"] = Path.GetFileName(myUpload.PostedFile.FileName);
                    e.Values["DATE_ATTACHED"] = DateTime.Now;
                    Label3.Text = "Successfully Attached. You may attach another or select button below";
                }

                catch (Exception ex)
                {
                    Label3.Text = Resources.errortestImg_001 + ex.Message.ToString();
                    e.Cancel = true;
                }

            else
            {
                Label3.Text = "You have not specified a file.";
                e.Cancel = true;
            }
        }


    }
}