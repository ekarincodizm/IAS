using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.pdf;
using Oracle.DataAccess.Client;
using System.Data;
using Ionic.Zip;

namespace IAS.Mockup
{
    public partial class TestZip : basepage  // System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session[PageList.UserProfile] = new DTO.UserProfile
                {
                    Id = "123",
                    //CanAccessSystem = new List<string> { "/Mockup/TestZip.aspx", "/Mockup/webTest.aspx" },
                    MemberType = 1,

                };

                base.HasPermit();

                //System.Web.VirtualPathUtility.GetFileName();
                //string[] file = Request.CurrentExecutionFilePath.Split('/');
                //string fileName = file[file.Length - 1];

            }
        }

        class ZipFile
        {
            public string OrgFilePath { get; set; }
            public string FilePath { get; set; }
            public string FileName { get; set; }
        }

        protected void btnListOfZip_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/UploadFile/");
            //string fileName = "UploadFile.zip";
            string fileName = "TestZip.zip";
            Utils.CompressFile cf = new Utils.CompressFile();
            List<ZipFile> list = new List<ZipFile>();
            var ls = cf.GetFilesInZip(filePath + fileName);

            //for(int i=0; i<ls.Count; i++)
            //{
            //    string fileName = ls.
            //list.Add(new ZipFile
            //{
            //     FileName = 
            //});
            //}
            gv.DataSource = ls;
            gv.DataBind();
        }

        protected void btnListOfRar_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/UploadFile/");
            string fileName = "UploadFile.rar";
            //string fileName = "TestZip.rar";
            Utils.CompressFile cf = new Utils.CompressFile();
            var list = cf.GetFilesInRar(filePath + fileName);
            gv.DataSource = list;
            gv.DataBind();
        }

        protected void btnExtractZip_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/UploadFile/");
            string fileName = "UploadFile.zip";
            Utils.CompressFile cf = new Utils.CompressFile();
            bool result = cf.ZipExtract(filePath + fileName, filePath);
            Response.Write(result);
        }

        protected void btnExtractRar_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/UploadFile/");
            string fileName = "UploadFile.rar";
            Utils.CompressFile cf = new Utils.CompressFile();
            bool result = cf.RarExtract(filePath + fileName, filePath);
            Response.Write(result);
        }

        protected void btnUload_Click(object sender, EventArgs e)
        {
            //Copy ด้านล่างไปใช้งาน

            var biz = new BLL.LicenseBiz();

            //ใช้จริงให้ใช้ basepage.UploadTempPath
            string temp = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadTempPath"]);

            string autoId = biz.GetAutoId();
            string tempFolder = temp + autoId;
            Directory.CreateDirectory(tempFolder);
            string tempFile = tempFolder + @"\" + fUpload.FileName;
            fUpload.SaveAs(tempFile);
            //var res = biz.UploadData(autoId, tempFile, temp, @"~/UploadTemp/", "test", "", "");

            //Copy ถึงตรงนี้


            //var res = biz.ExtractFile(fUpload.PostedFile.FileName, temp);
            //gv.DataSource = res;
            //gv.DataBind();
        }

        protected void Redirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("webTest.aspx");
        }

        protected void btnTestPDF_Click(object sender, EventArgs e)
        {
            using (Stream input = new FileStream(Server.MapPath("~/mockup/test.pdf"), FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream output = new FileStream(Server.MapPath("~/mockup/test_encrypted.pdf"), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfReader reader = new PdfReader(input);
                PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
            }
        }

        protected void btnTestStore_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection objConn = new OracleConnection(connStr))
            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "AG_IAS_CHK_FILE_TO_DB";

                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("import_id", OracleDbType.Long).Value = 1;
                objCmd.Parameters.Add("agent_type", OracleDbType.Varchar2).Value = "A";
                objCmd.Parameters.Add("petition", OracleDbType.Varchar2).Value = "11";
                objCmd.Parameters.Add("license_type_code", OracleDbType.Varchar2).Value = "01";

                var errFlag = new OracleParameter("ERR_FLG", OracleDbType.Int32, ParameterDirection.InputOutput);
                errFlag.Value = 0;
                objCmd.Parameters.Add(errFlag);


                var errMess = new OracleParameter("ERR_MESS", OracleDbType.Varchar2, ParameterDirection.Output);
                errMess.Size = 4000;
                errMess.Value = "";
                objCmd.Parameters.Add(errMess);

                objCmd.Parameters.Add("B_FLAG", OracleDbType.Varchar2).Value = "L";

                var isDone = new OracleParameter("IS_DONE", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                isDone.Value = "N";
                objCmd.Parameters.Add(isDone);

                try
                {

                    objConn.Open();

                    objCmd.ExecuteNonQuery();




                }

                catch (Exception ex)
                {


                }



                objConn.Close();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection objConn = new OracleConnection(connStr))
            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "AG_IAS_UPD_FILE_TO_DB";

                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("vimport_id", OracleDbType.Long).Value = 1;
                objCmd.Parameters.Add("agent_type", OracleDbType.Varchar2).Value = "A";
                objCmd.Parameters.Add("petition", OracleDbType.Varchar2).Value = "11";

                var errFlag = new OracleParameter("err_flag", OracleDbType.Int32, ParameterDirection.InputOutput);
                errFlag.Value = 0;
                objCmd.Parameters.Add(errFlag);


                var errMess = new OracleParameter("err_mess", OracleDbType.Varchar2, ParameterDirection.Output);
                errMess.Size = 4000;
                errMess.Value = "";
                objCmd.Parameters.Add(errMess);

                objCmd.Parameters.Add("flag", OracleDbType.Varchar2, ParameterDirection.Input).Value = "L";

                var isDone = new OracleParameter("IS_DONE", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                isDone.Value = "N";
                objCmd.Parameters.Add(isDone);


                try
                {

                    objConn.Open();

                    objCmd.ExecuteNonQuery();




                }

                catch (Exception ex)
                {


                }



                objConn.Close();

            }
        }

        private void GetAllFiles(List<string> res, string dir)
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                res.Add(d);
                GetAllFiles(res, d);
            }
        }


        protected void btnTestDownload_Click(object sender, EventArgs e)
        {
            string dic = Server.MapPath("~/UploadFile");

            var biz = new BLL.LicenseBiz();
            biz.DownloadLicenseZip(Response, DateTime.Today, dic);

            //List<string> list = new List<string>();
            //list.Add(dic);

            //GetAllFiles(list, dic);

            //string folderName = string.Empty;
            //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            //{
            //    zip.AlternateEncodingUsage = ZipOption.AsNecessary;

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        DirectoryInfo dInfo = new DirectoryInfo(list[i]);
            //        folderName += (i == 0 ? "" : "\\") + dInfo.Name;
            //        zip.AddDirectoryByName(folderName);

            //        foreach (string f in Directory.GetFiles(list[i]))
            //        {
            //            zip.AddFile(f, folderName);
            //        }
            //    }

            //    Response.Clear();
            //    Response.BufferOutput = false;
            //    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            //    Response.ContentType = "application/zip";
            //    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            //    zip.Save(Response.OutputStream);
            //    Response.End();
            //}
        }
    }
}