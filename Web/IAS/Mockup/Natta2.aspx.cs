using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace IAS.Mockup
{
    public partial class Natta2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExportOptions objExOpt;

            CrystalReportViewer1.ReportSource = (object)getReportDocument();
            CrystalReportViewer1.DataBind();
            // Get the report document
            ReportDocument repDoc = getReportDocument();
            repDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            repDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();

            string strLine = "09605954432 5969 59681 2993 00";
            List<string> txtLines = new List<string>();
            string strnew = strLine.Replace(" ","");
            string path = @"C:\CrystalExport\newline.txt";

            for (int i = 0; i < strnew.Count(); i++)
            {
                txtLines.Add(strnew[i].ToString());
            }

            foreach (string str in txtLines)
            {
                File.AppendAllText(path, str + Environment.NewLine);
            }

            

            string filePath = @"\\192.168.15.10\IASFileUpload\ReceiptFile\3501600072585\3501600072585_12122e11300319.pdf";
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("WINDOWS-874"));
                // Write data to the file
                //newFile.Write(Buffer, 0, Buffer.Length);
                HashAlgorithm ha = HashAlgorithm.Create();
                //FileStream fs = new FileStream(filePath, FileMode.Open);

                byte[] hash = ha.ComputeHash(fs);
                fs.Close();

                objDiskOpt.DiskFileName = @"C:\CrystalExport\TFA.pdf";
                repDoc.ExportOptions.DestinationOptions = objDiskOpt;
                repDoc.Export();

                //hashValue = BitConverter.ToString(hash);
            }

            //objDiskOpt.DiskFileName = @"C:\CrystalExport\TFA.pdf";
            //repDoc.ExportOptions.DestinationOptions = objDiskOpt;
            //repDoc.Export();
        }

        private ReportDocument getReportDocument()
        {

            // File Path for Crystal Report
            //string repFilePath = Server.MapPath("~/WebService/IAS.DataServices/Reports/RptRecive.rpt");
            // Declare a new Crystal Report Document object
            // and the report file into the report document
            ReportDocument repDoc = new ReportDocument();
            string ReportFolder = @"\Mockup\";
            //string ReportFolder = @"\Reports\";
            repDoc.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + ReportFolder + "CrystalReport1.rpt");
            //repDoc.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + ReportFolder + "RptRecive.rpt");
            //repDoc.Load(repFilePath);

            // Set the datasource by getting the dataset from business
            // layer and
            // In our case business layer is getCustomerData function
            return repDoc;
        }
    }
}