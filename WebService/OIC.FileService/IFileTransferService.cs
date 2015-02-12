using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;




namespace IAS.FileService
{
    [ServiceContract]
    public interface IFileTransferService
    {
        //[OperationContract(IsOneWay = false)]
        //FileUploadMessage UploadFile(FileUploadMessage request);
        //[OperationContract(IsOneWay = false)]
        //UploadFileResponse UploadFile(UploadFileRequest request);

        //[OperationContract]
        //RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        DTO.FileService.DownloadFileResponse DownloadFile(DTO.FileService.DownloadFileRequest request);

        [OperationContract]
        DTO.FileService.UploadFileResponse UploadFile(DTO.FileService.UploadFileRequest request);

        [OperationContract]
        DTO.FileService.MoveFileResponse MoveFile(DTO.FileService.MoveFileRequest request);

        [OperationContract]
        DTO.FileService.DeleteFileResponse DeleteFile(DTO.FileService.DeleteFileRequest request);

        [OperationContract]
        DTO.FileService.AmendFileResponse AmendFile(DTO.FileService.AmendFileRequest request);

        [OperationContract]
        DTO.FileService.ContainDetailResponse ContainDetail(DTO.FileService.ContainDetailRequest request);

        [OperationContract]
        DTO.FileService.DeleteContainerResponse DeleteContainer(DTO.FileService.DeleteContainerRequest request);
        
    }

    

}
