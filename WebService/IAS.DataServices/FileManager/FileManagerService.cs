using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DTO.FileService;


namespace IAS.DataServices.FileManager
{
    public static class FileManagerService
    {
        public static DownloadFile RemoteFileCommand(DownloadFileRequest request) {
            return new DownloadFile(request);
        }

        public static UploadFile RemoteFileCommand(UploadFileRequest request) {
            return new UploadFile(request);
        }

        public static MoveFile RemoteFileCommand(MoveFileRequest request) { 
            return new MoveFile(request);
        }

        public static DeleteFile RemoteFileCommand(DeleteFileRequest request) {
            return new DeleteFile(request);
        }

        public static AmendFile RemoteFileCommand(AmendFileRequest request)
        {
            return new AmendFile(request);
        }

        public static ContainDetail RemoteFileCommand(ContainDetailRequest request) 
        {
            return new ContainDetail(request);
        }

        public static DeleteContainer RemoteFileCommand(DeleteContainerRequest request) 
        {
            return new DeleteContainer(request);
        }
    }
}