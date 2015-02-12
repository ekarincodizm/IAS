using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAS.DTO.FileService;
using System.Configuration;

namespace IAS.BLL
{
    public class FileBiz
    {
        FileService.FileTransferServiceClient svc;
        string fsTemp = ConfigurationManager.AppSettings["FS_TEMP"];
        string fsAttach = ConfigurationManager.AppSettings["FS_ATTACH"];
        string fsOIC = ConfigurationManager.AppSettings["FS_OIC"];

        public FileBiz()
        {
            this.svc = new FileService.FileTransferServiceClient();
        }

        //private DTO.ResponseService<string>
        //    UploadTo(Stream fileUploadStream, string localFileName, string targetFolder, string targetFileName, string fsFolder)
        //{
        //    var res = new DTO.ResponseService<string>();
        //    var resMsg = new FileService.FileUploadMessage();
        //    try
        //    {
        //        resMsg.FileByteStream = fileUploadStream;
        //        resMsg.Metadata = new FileService.FileMetaData
        //        {
        //            localFilename = localFileName,
        //            remoteFilename = targetFileName,
        //            targetFolder = string.Format(fsFolder + @"\{0}\", targetFolder),
        //        };
        //        svc.UploadFile(ref resMsg.Metadata, ref fileUploadStream);
        //        res.DataResponse = resMsg.Metadata.resMsg;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ"; // +resMsg.Metadata.resMsg;
        //    }
        //    return res;
        //}
        private DTO.ResponseService<string>
            UploadTo(Stream fileUploadStream, string localFileName, string targetFolder, string targetFileName, string fsFolder)
        {
            var res = new DTO.ResponseService<string>();
            //var resMsg = new FileService.FileUploadMessage();
            UploadFileResponse response = new UploadFileResponse() ;
            try
            {
                //resMsg.FileByteStream = fileUploadStream;
                //resMsg.Metadata = new FileService.FileMetaData
                //{
                //    localFilename = localFileName,
                //    remoteFilename = targetFileName,
                //    targetFolder = string.Format(fsFolder + @"\{0}\", targetFolder),
                //};

                response = svc.UploadFile(new UploadFileRequest() {
                                                TargetContainer = String.Format(fsFolder + @"\{0}\", targetFolder),
                                                TargetFileName = targetFileName,
                                                FileStream = fileUploadStream
                                            });
                //svc.UploadFile(ref resMsg.Metadata, ref fileUploadStream);

                res.DataResponse = response.Message; // resMsg.Metadata.resMsg;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ"; // +resMsg.Metadata.resMsg;
            }
            return res;
        }
        /// <summary>
        /// สำหรับ Upload ไปพักไว้ก่อน
        /// </summary>
        /// <param name="fileUploadStream"></param>
        /// <param name="localFileName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public DTO.ResponseService<string>
            UploadToTemp(Stream fileUploadStream, string localFileName, string targetFolder, string targetFileName)
        {
            return UploadTo(fileUploadStream, localFileName, targetFolder, targetFileName, fsTemp);
        }

        /// <summary>
        /// สำหรับ Upload ไฟล์ไปเก็บ
        /// </summary>
        /// <param name="fileUploadStream"></param>
        /// <param name="localFileName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public DTO.ResponseService<string>
            UploadToAttach(Stream fileUploadStream, string localFileName, string targetFolder, string targetFileName)
        {
            return UploadTo(fileUploadStream, localFileName, targetFolder, targetFileName, fsAttach);
        }

        /// <summary>
        /// สำหรับ Upload ไฟล์ไปเก็บ
        /// </summary>
        /// <param name="fileUploadStream"></param>
        /// <param name="localFileName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public DTO.ResponseService<string>
            UploadToOIC(Stream fileUploadStream, string localFileName, string targetFolder, string targetFileName)
        {
            return UploadTo(fileUploadStream, localFileName, targetFolder, targetFileName, fsOIC);
        }

                public DTO.ResponseMessage<bool> MoveFile(string sourceFile, string targetFile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// Download File จาก File Server
        /// </summary>
        /// <param name="httpResponse">Current Response from Page</param>
        /// <param name="folderContrainner">Folder ที่เก็บไฟล์</param>
        /// <param name="fileName">ชื่อไฟล์</param>
        public void DownloadFile(System.Web.HttpResponse httpResponse, string folderContrainner, string fileName)
        {
            //ระบุตำแหน่งไฟล์และที่เก็บบน File Server
            //FileService.DownloadRequest requestData = new FileService.DownloadRequest();
            
            //FileService.RemoteFileInfo fileInfo = new FileService.RemoteFileInfo();

            //string resMsg = svc.DownloadFile(ref fileName, folderContrainner, out fileInfo.Length, out fileInfo.FileByteStream);

            DownloadFileResponse response = new DownloadFileResponse();
            response = svc.DownloadFile(new DownloadFileRequest() {
                                                TargetContainer = folderContrainner,
                                                TargetFileName = fileName
                                            });

            Stream fileStream = response.FileByteStream;
            
            httpResponse.Clear();
            httpResponse.BufferOutput = true;

            // Set Response.ContentType
            httpResponse.ContentType = response.ContentType;  //GetContentType(fileExtension);

            // Append header
            httpResponse.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

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
                    if (httpResponse.IsClientConnected)
                    {
                        length = download.Read(buffer, 0, bufferLength);
                        httpResponse.OutputStream.Write(buffer, 0, length);
                        buffer = new Byte[bufferLength];
                    }
                    else
                    {
                        length = -1;
                    }
                }
                while (length > 0);

                httpResponse.Flush();
                httpResponse.End();
            }
            finally
            {
                if (download != null)
                    download.Close();
            }
        }

        public byte[] Signature_Img(System.Web.HttpResponse httpResponse, string folderContrainner, string fileName)//(string httpTXT)
        {

            DTO.FileService.DownloadFileResponse response = new DTO.FileService.DownloadFileResponse();
            response = svc.DownloadFile(new DTO.FileService.DownloadFileRequest()
            {
                TargetContainer = folderContrainner,
                TargetFileName = fileName
            });

            Stream fileStream = response.FileByteStream;
            Double e = response.Length;

            httpResponse.Clear();
            httpResponse.BufferOutput = true;

            // Set Response.ContentType
            httpResponse.ContentType = response.ContentType;  //GetContentType(fileExtension);

            // Append header
            httpResponse.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

            // Write the file to the Response
            Double bufferL = response.Length;
            int bufferLength = unchecked((int)bufferL);
            byte[] buffer = new Byte[bufferLength + 1];
            BinaryReader br = new BinaryReader(fileStream);
            buffer = br.ReadBytes(Convert.ToInt32((bufferLength)));
            br.Close();
           
            return buffer;
        }

        
    }
}
