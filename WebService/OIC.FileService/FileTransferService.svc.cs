using System;
using System.Collections.Generic;
using System.Linq;
using IAS.DTO.FileService;
using System.ServiceModel.Activation;
using IAS.FileService.FileManager;


namespace IAS.FileService
{
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class FileTransferService : IFileTransferService
    {
        //#region Member Variable

        //private NASDrive nasDrive;
        //string netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
        //string userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
        //string passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];

        //#endregion

        //#region Member Method

        //public void Dispose()
        //{
        //    if (nasDrive != null)
        //    {
        //        nasDrive.Dispose();
        //        nasDrive = null;
        //    }
        //}
        //private void ConnectNetDrive()
        //{
        //    this.nasDrive = new NASDrive(netDrive, userNetDrive, passNetDrive);
        //}

        //private void DisConnectNetDrive()
        //{
        //    if (this.nasDrive != null)
        //    {
        //        this.nasDrive.Dispose();
        //    }
        //}

        //private string GetContentType(string fileExtension)
        //{
        //    if (string.IsNullOrEmpty(fileExtension))
        //        return string.Empty;

        //    string contentType = string.Empty;
        //    switch (fileExtension)
        //    {
        //        case ".htm":
        //        case ".html":
        //            contentType = "text/HTML";
        //            break;

        //        case ".txt":
        //            contentType = "text/plain";
        //            break;

        //        case ".doc":
        //        case ".rtf":
        //        case ".docx":
        //            contentType = "Application/msword";
        //            break;

        //        case ".xls":
        //        case ".xlsx":
        //            contentType = "Application/x-msexcel";
        //            break;

        //        case ".jpg":
        //        case ".jpeg":
        //            contentType = "image/jpeg";
        //            break;

        //        case ".gif":
        //            contentType = "image/GIF";
        //            break;

        //        case ".pdf":
        //            contentType = "application/pdf";
        //            break;
        //    }

        //    return contentType;
        //}

        //#endregion


        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UploadFileResponse UploadFile(UploadFileRequest request)
        {
            UploadFileResponse response = new UploadFileResponse();

            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }




        public DownloadFileResponse DownloadFile(DownloadFileRequest request)
        {
            DownloadFileResponse response = new DownloadFileResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
                
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
          
            }

            return response;
        }

        public MoveFileResponse MoveFile(MoveFileRequest request)
        {
            MoveFileResponse response = new MoveFileResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Code = ex.Message;
            }

            return response;
        }

        public DeleteFileResponse DeleteFile(DeleteFileRequest request)
        {
            DeleteFileResponse response = new DeleteFileResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
            }
            return response;
        }

        public AmendFileResponse AmendFile(AmendFileRequest request)
        {
            AmendFileResponse response = new AmendFileResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
            }
            return response;
        }


        public ContainDetailResponse ContainDetail(ContainDetailRequest request)
        {
            ContainDetailResponse response = new ContainDetailResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
            }
            return response;
        }

        public DeleteContainerResponse DeleteContainer(DeleteContainerRequest request)
        {
            DeleteContainerResponse response = new DeleteContainerResponse();
            try
            {
                response = FileManagerService.RemoteFileCommand(request).Action();
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
            }
            return response;
        }
    }

}
