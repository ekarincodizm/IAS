using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Oracle.DataAccess.Client;
using System.Data;
using IAS.Utils;
using System.IO;
using IAS.Properties;

namespace IAS.Mockup
{
    public partial class webTestUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //private void ShowImage()
        //{
        //    MemoryStream ms = fUpload.PostedFile.InputStream;
        //     if (ms != null)
        //       {
        //            Byte[] byteArray = ms.ToArray();
        //            ms.Flush();
        //            ms.Close();
        //            Response.BufferOutput = true;
        //            // Clear all content output from the buffer stream
        //            Response.Clear();
        //            //to fix the “file not found” error when opening excel file
        //            //See http://www.aspose.com/Community/forums/ShowThread.aspx?PostID=61444
        //            Response.ClearHeaders();
        //            // Add a HTTP header to the output stream that specifies the default filename
        //            // for the browser’s download dialog
        //            string timeStamp = Convert.ToString(DateTime.Now.ToString("MMddyyyy_HHmmss"));
        //            Response.AddHeader("Content-Disposition",
        //                               "attachment; filename=testFileName_" + timeStamp + ".fileextention");
        //            // Set the HTTP MIME type of the output stream
        //            Response.ContentType = "application/octet-stream";
        //            // Write the data
        //            Response.BinaryWrite(byteArray);
        //            Response.End();
        //        }
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            if (fUpload.PostedFile.FileName != "")
            {
                ////BLL.UploadDataBiz biz = new BLL.UploadDataBiz();
                //BLL.ExamResultBiz biz = new BLL.ExamResultBiz();
                //var res = biz.UploadData(fUpload.PostedFile.FileName, fUpload.PostedFile.InputStream, "AGDOI");
                ////if (!res.IsError)
                ////{
                //lblMsg.Text = res.DataResponse.GroupId;
                ////Response.Write(res.DataResponse.GroupId + "<br/>");
                //gv.DataSource = res.DataResponse.Header;
                //gv.DataBind();
                //gv2.DataSource = res.DataResponse.Detail;
                //gv2.DataBind();
                //}
            }
            //string fi = Server.MapPath("~/Mockup/job.txt");

            //lblHash.Text = FileObject.GetHashSHA1(fi);

            //if (fUpload.HasFile)
            //{
                //string file = Server.MapPath(@"~/UploadFile/Job.txt");
                //System.IO.FileInfo fileInfo = new System.IO.FileInfo(file); //fUpload.PostedFile.FileName);
                
                //FileService.IFileService clientUpload = new FileService.FileServiceClient();
                //FileService.RemoteFileInfo uploadRequestInfo = new FileService.RemoteFileInfo();

                ////using (System.IO.FileStream stream = new System.IO.FileStream(fUpload.PostedFile.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                
                //using (System.IO.FileStream stream = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                //{
                //    uploadRequestInfo.FileName = "job.txt"; // fUpload.FileName;
                //    //uploadRequestInfo.Length = //fileInfo.Length;
                //    uploadRequestInfo.FileByteStream = stream;
                //    clientUpload.UploadFile(uploadRequestInfo);
                //}
            //}

            //BLL.FileBiz biz = new BLL.FileBiz();
            //string targetFolder = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
            //biz.UploadToTemp(fUpload.PostedFile.InputStream, fUpload.FileName, targetFolder, fUpload.FileName);
            //Response.Write(Resources.errorwebTestUpload_003);
        }


        //protected bool Upload(HttpPostedFile file, long actualFileSize)
        //{
        //    int filePosition = 0;
        //    int filePart = 16 * 1024; //Each hit 16 kb file to avoid any serialization issue when transfering  data across WCF

        //    //Create buffer size to send to service based on filepart size
        //    byte[] bufferData = new byte[filePart];

        //    //Set the posted file data to file stream.
        //    Stream fileStream = file.InputStream;

        //    //Create the service client
            

        //    //IAS. .FileUploadServiceClient serviceClient = new FUWcfService.FileUploadServiceClient();

        //    try
        //    {
        //        long actualFileSizeToUpload = actualFileSize;
        //        //Start reading the file from the specified position.
        //        fileStream.Position = filePosition;
        //        int fileBytesRead = 0;

        //        //Upload file data in parts until filePosition reaches the actual file end or size.
        //        while (filePosition != actualFileSizeToUpload)
        //        {
        //            // read the next file part i.e. another 100 kb of data 
        //            fileBytesRead = fileStream.Read(bufferData, 0, filePart);
        //            if (fileBytesRead != bufferData.Length)
        //            {
        //                filePart = fileBytesRead;
        //                byte[] bufferedDataToWrite = new byte[fileBytesRead];
        //                //Copy the buffered data into bufferedDataToWrite
        //                Array.Copy(bufferData, bufferedDataToWrite, fileBytesRead);
        //                bufferData = bufferedDataToWrite;
        //            }

        //            //Populate the data contract to send it to the service method
                    
        //            //bool fileDataWritten = serviceClient.UploadFileData(
        //            //    new FUWcfService.FileData { FileName = file.FileName, BufferData = bufferData, FilePosition = filePosition });
        //            //if (!fileDataWritten)
        //            //{
        //            //    break;
        //            //}

        //            //Update the filePosition position to continue reading data from that position back to server
        //            filePosition += fileBytesRead;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log data to database or file system here
        //        return false;
        //    }
        //    finally
        //    {
        //        //Close the fileStream once all done.
        //        fileStream.Close();
        //    }
        //    return true;
        //}


        protected void btnGet_Click(object sender, EventArgs e)
        {
            var biz = new BLL.DataCenterBiz();
            lblMsg.Text = biz.GetCompanyNameById("3155");
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            //using (OracleConnection objConn = new OracleConnection("Data Source=localhost;User Id=AGDOI;Password=oracle;"))
            //{

            //    OracleCommand objCmd = new OracleCommand();

            //    objCmd.Connection = objConn;

            //    objCmd.CommandText = "PL_IAS_EXAM_LICENSE";

            //    objCmd.CommandType = CommandType.StoredProcedure;



            //    OracleParameter p1 = new OracleParameter
            //    {
            //        OracleDbType = Oracle.DataAccess.Client.OracleDbType.RefCursor,
            //        Direction = ParameterDirection.Output
            //    };

            //    OracleParameter p2 = new OracleParameter
            //    {
            //        OracleDbType = Oracle.DataAccess.Client.OracleDbType.Varchar2,
            //        Direction = ParameterDirection.Input
            //    };
            //    p1.Value = "201306";

            //    objCmd.Parameters.Add(p1);
            //    objCmd.Parameters.Add(p2);

            //    try
            //    {

            //        objConn.Open();

            //        OracleDataAdapter oda = new OracleDataAdapter();
            //        DataTable dt = new DataTable();
            //        oda.SelectCommand = objCmd;
            //        oda.Fill(dt);

            //        //OracleDataReader objReader = objCmd.ExecuteReader();




            //        //dt.Load(objReader);

            //        gv.DataSource = dt;
            //        gv.DataBind();
            //    }

            //    catch (Exception ex)
            //    {

            //        System.Console.WriteLine("Exception: {0}", ex.ToString());

            //    }



            //    objConn.Close();

            //}

            //var biz = new BLL.ExamScheduleBiz();
            //var res = biz.GetExamByCriteria("", "", "", "201306", "", null);
            //gv.DataSource = res.DataResponse.Tables[0];
            //gv.DataBind();
        }

        protected void btn3_Click(object sender, EventArgs e)
        {
            //var biz = new BLL.ExamResultBiz();
            //var res = biz.ExamResultUploadToSubmit(lblMsg.Text, "Neng", null);
           // if (!res.ResultMessage)
           // {
           //     Response.Write(Resources.errorwebTestUpload_001 + res.ErrorMsg);
           // }
          //  else
           // {
           //     Response.Write(Resources.errorwebTestUpload_002);
           // }
        }

        protected void btn4_Click(object sender, EventArgs e)
        {
            var biz = new BLL.PaymentBiz();

            DTO.UserProfile userProfile = new DTO.UserProfile
            {
                CompCode = "1008",
                Id = "123",
                IdCard = "456",
                MemberType = 2,
            };
            //var res =  biz.GetSubGroup("01", userProfile, 0, 10);
            //gv.DataSource = res.DataResponse;
            //gv.DataBind();
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
            var biz = new BLL.FileBiz();
            //var res = biz.DownloadFile(Page.Response, @"desert.jpg", @"\\192.168.15.10\IASFileUpload\desert.jpg");

            biz.DownloadFile(Page.Response, "", "Jellyfish.jpg");

            
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            BLL.FileBiz biz = new BLL.FileBiz();
            string targetFolder = "1234";
            biz.UploadToAttach(fUpload.PostedFile.InputStream, fUpload.FileName, targetFolder, fUpload.FileName);
            Response.Write(Resources.errorwebTestUpload_003);
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            BLL.FileBiz biz = new BLL.FileBiz();
            string targetFolder = "5678";
            biz.UploadToOIC(fUpload.PostedFile.InputStream, fUpload.FileName, targetFolder, fUpload.FileName);
            Response.Write(Resources.errorwebTestUpload_003);
        }
    }
}
