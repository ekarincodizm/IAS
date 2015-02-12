using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using IAS.DTO.FileService;
using System.Xml;
using System.Configuration;
using IAS.Utils;
using IAS.Properties;
//using IAS.Utils.FileManager;




namespace IAS.Mockup
{
    public partial class TestUploadData : System.Web.UI.Page
    {
        private FileService.FileTransferServiceClient svc;
        //public  IList<DTO.AttachFile> AttachFiles()
        //{

        //    if(Session["AttachFiles"]==null)
        //        Session["AttachFiles"] = new List<DTO.AttachFile>();

        //    return ((IList<DTO.AttachFile>)Session["AttachFiles"]).ToList();
            
        //}
        protected String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        protected String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString(); 
        public String AttachTemp {
            get {
                return Session["ContainerTemp"].ToString();
            }
        }

        private void AddAttechFile(DTO.AttachFile attachFile)
        {

            if (((List<DTO.AttachFile>)Session["AttachFiles"]).Where(a => a.AttechType == attachFile.AttechType).Count() > 0)
            {
                throw new ApplicationException(Resources.errorTestUploadData_002);
            }
            else
            {
                

                using (svc = new FileService.FileTransferServiceClient()) {
                    UploadFileResponse response = new UploadFileResponse();
                    Stream fileStrem = fUpload.PostedFile.InputStream;
                    response = svc.UploadFile( new UploadFileRequest() {
                                         TargetContainer = attachFile.TargetContainer,
                                        TargetFileName = attachFile.TargetFileName,
                                        FileStream = fileStrem});

                    attachFile.TargetFullName = CryptoBase64.Encryption(response.TargetFullName);
                    ((List<DTO.AttachFile>)Session["AttachFiles"]).Add(attachFile);
                    gv.DataSource = ((List<DTO.AttachFile>)Session["AttachFiles"]);
                    gv.DataBind();
                
                }
                
       
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                Session["ContainerTemp"] = "Temp";
     
            }
        }
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            

            if (txtIdCard.Text.Trim().Length != 13)
            {
                Label1.Text = Resources.errorTestUploadData_001;
            }
            else {
                try
                {
                    if (Session["AttachFiles"] == null)
                        Session["AttachFiles"] = new List<DTO.AttachFile>();
                    
                    DTO.AttachFile attachFile = new DTO.AttachFile();
                    if (((List<DTO.AttachFile>)Session["AttachFiles"]).Count > 0)
                    {
                        txtIdCard.Enabled = false;
                        attachFile.TargetContainer = ((List<DTO.AttachFile>)Session["AttachFiles"]).FirstOrDefault().TargetContainer;
                    }
                    else {
                        attachFile.TargetContainer = AttachTemp + @"\" + IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                        txtIdCard.Enabled = true;
                    }

                        attachFile.FileName = Path.GetFileName(fUpload.FileName);
                        attachFile.FileType = Path.GetExtension(fUpload.FileName);
                        attachFile.ID = txtIdCard.Text.Trim();
                        //attachFile.TargetFileName = txtIdCard.Text.Trim() +"_"+ (Convert.ToInt32(ddlAttechType.SelectedValue)).ToString("000"); 
                        attachFile.AttechType = ddlAttechType.SelectedValue.ToString();


                    AddAttechFile(attachFile);
                }
                catch (Exception ex)
                {

                    Label1.Text = ex.Message;
                }
            
            }
            
            
            
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            MoveFileResponse response = new MoveFileResponse();
            try
            {
                using (svc = new FileService.FileTransferServiceClient())
                {


                    IList<DTO.AttachFile> attachFiles = ((IList<DTO.AttachFile>)Session["AttachFiles"]);

                    foreach (DTO.AttachFile item in attachFiles)
                    {
                        String target = AttachFileContainer +@"\" + item.ID;
                        response = svc.MoveFile(new MoveFileRequest() {
                                                            CurrentContainer = "",
                                                            CurrentFileName = CryptoBase64.Decryption( item.TargetFullName),
                                                            TargetContainer = target,
                                                            TargetFileName = item.TargetFileName
                                                        });

                        if (response.Code != "0000")
                            throw new ApplicationException(response.Message);

                        item.TargetFullName = response.TargetFullName;
                    }

                    gv.DataSource = attachFiles;
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = "0001";
            }
            

            
            //IList<DTO.AttachFile> attachFiles = ((IList<DTO.AttachFile>)Session["AttechFiles"]).ToList();
            //foreach (DTO.AttachFile item in AttachFiles)
            //{
                ////svc = new FileService.FileTransferServiceClient();
                //svc = new FileService.FileTransferServiceClient();
                //UploadFileResponse response = new UploadFileResponse();

                //string res = svc.UploadFile(item.TargetFolder, item.ID, item.DataStream, out response.Code, out response.Message, out response.Certificate);
                //BLL.FileBiz biz = new BLL.FileBiz();
                //string targetFolder = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                ////biz.UploadToTemp(fUpload.PostedFile.InputStream, fUpload.FileName, targetFolder, fUpload.FileName);
                //biz.UploadToTemp(item.DataStream, item.FileName, item.TargetFolder, item.FileName);
                //Response.Write("Success");

                //IAS.FileService.FileUploadMessage fileUploadMessage = new IAS.FileService.FileUploadMessage();

                //var res = new DTO.ResponseService<string>();
                //var resMsg = new FileService.FileUploadMessage();
                //try
                //{
                //    resMsg.FileByteStream = item.DataStream;
                //    resMsg.Metadata = new FileMetaData
                //    {
                //        localFilename = item.FileName,
                //        remoteFilename = item.ID,
                //        targetFolder = string.Format(item.FileName + @"\{0}\", item.ID),
                //    };

                //    svc.UploadFile(ref resMsg.Metadata, ref resMsg.FileByteStream);
                //    res.DataResponse = resMsg.Metadata.resMsg;
                //}
                //catch (Exception ex)
                //{
                //    res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ"; // +resMsg.Metadata.resMsg;
                //}
               
            //}
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            using (svc = new FileService.FileTransferServiceClient()) {
                DownloadFileResponse response = new DownloadFileResponse();
                DTO.AttachFile attachFile = ((List<DTO.AttachFile>)Session["AttachFiles"]).FirstOrDefault();
                String container = "";
                String fileName = "";
                response = svc.DownloadFile( new DownloadFileRequest(){ TargetContainer = container, TargetFileName = attachFile.TargetFullName});

                Page.Response.Clear();
                Page.Response.BufferOutput = true;
                Page.Response.ContentType = response.ContentType;

    
                // Append header
                //Page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + response.FileName);

                // Write the file to the Response
                const int bufferLength = 10000;
                byte[] buffer = new Byte[bufferLength];
                int length = 0;
                Stream download = null;
                
                try
                {
                    download = response.FileByteStream; // GetFile(fileName);

                    do
                    {
                        if (Page.Response.IsClientConnected)
                        {
                            length = download.Read(buffer, 0, bufferLength);
                            Page.Response.OutputStream.Write(buffer, 0, length);

                           
                            buffer = new Byte[bufferLength];
                            
                        }
                        else
                        {
                            length = -1;
                        }
                    }
                    while (length > 0);

                    Page.Response.Flush();
                    Page.Response.End();
                }
                finally
                {
                    if (download != null)
                        download.Close();
                }
            }
          
        }
        private void ShowImage(Stream fileStream, Int64 length)
        {
         
            MemoryStream ms = new MemoryStream();
            using (fileStream )
            {
                ms.SetLength(length);
                fileStream.Read(ms.GetBuffer(), 0, (int)length);
            }

            Response.Expires = 0;
            Response.Buffer = false;
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.Close();
        }
        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTO.AttachFile attachFile = ((List<DTO.AttachFile>)Session["AttachFiles"]).FirstOrDefault();
            String container = "";
            String fileName = gv.Rows[gv.SelectedIndex].Cells[7].Text;


            using (svc = new FileService.FileTransferServiceClient())
            {
                DownloadFileResponse response = new DownloadFileResponse();

                response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = container, TargetFileName = IAS.Utils.CryptoBase64.Decryption(fileName) });
                if (response.Code == "0000") {
                    ShowImage(response.FileByteStream, Convert.ToInt64( response.Length));
                }
            }
            //    Page.Response.Clear();
            //    Page.Response.BufferOutput = true;
            //    Page.Response.ContentType = response.ContentType;



            //    // Append header
            //    Page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + response.FileName);

            //    // Write the file to the Response
            //    //const int bufferLength = Convert.ToInt32(response.Length);
            //    byte[] buffer = new Byte[Convert.ToInt32(response.Length)];
            //    //int length = 0;
            //    //Stream download = null;
            //    //response.FileByteStream.Write(buffer, 0, Convert.ToInt32(response.Length));
            //    //myImage.ImageUrl = Convert.ToBase64String(buffer).ToString();

            //    using (var memoryStream = new MemoryStream())
            //    {
            //        response.FileByteStream.CopyTo(memoryStream);
            //        buffer = memoryStream.ToArray();
            //    }

            //    myImage.ImageUrl = Convert.ToBase64String(buffer).ToString();
               
            //    //try
            //    //{
            //    //    download = response.FileByteStream; // etFile(fileName);

            //    //    do
            //    //    {
            //    //        if (Page.Response.IsClientConnected)
            //    //        {
            //    //            length = download.Read(buffer, 0, bufferLength);
            //    //            Page.Response.OutputStream.Write(buffer, 0, length);
            //    //            myImage.ImageUrl = Convert.ToBase64String(buffer).ToString();
            //    //            buffer = new Byte[bufferLength];
            //    //        }
            //    //        else
            //    //        {
            //    //            length = -1;
            //    //        }
            //    //    }
            //    //    while (length > 0);

            //    //    Page.Response.Flush();
            //    //    Page.Response.End();
            //    //}
            //    //finally
            //    //{
            //    //    if (download != null)
            //    //        download.Close();
            //    //}
            //}
           
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (svc = new FileService.FileTransferServiceClient())
            {
                String targetfile =  gv.Rows[e.RowIndex].Cells[7].Text;

                DeleteFileResponse response = svc.DeleteFile(new DeleteFileRequest() {
                                                                    TargetFileName = targetfile });

                DTO.AttachFile file = ((List<DTO.AttachFile>)Session["AttachFiles"]).Where(a => a.AttechType == gv.Rows[e.RowIndex].Cells[1].Text.Trim()).FirstOrDefault();
                ((List<DTO.AttachFile>)Session["AttachFiles"]).Remove(file);
                gv.DataSource = ((List<DTO.AttachFile>)Session["AttachFiles"]);
                gv.DataBind();
                Label1.Text = response.Message;
            }
        }

    }
}