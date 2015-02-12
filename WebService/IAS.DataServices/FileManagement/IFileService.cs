using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace IAS.DataServices.FileManagement
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract(IsOneWay = false)]
        FileUploadMessage UploadFile(FileUploadMessage request);

        [OperationContract(IsOneWay = false)]
        FileDownloadReturnMessage DownloadFile(FileDownloadMessage request);

        [OperationContract]
        RemoteFileInfo DownloadFileSign(DownloadRequest request);

        [OperationContract]
        void UpdateOIC(RemoteFileInfo request);

        [OperationContract]
        void InsertOIC(RemoteFileInfoAddOic request);

        [OperationContract]
        DTO.ResponseMessage<bool> ValidateApplicantSingleBeforeSubmit(List<DTO.ApplicantTemp> app);

        [OperationContract]
        DTO.ResponseService<DTO.SummaryReceiveApplicant>
            InsertAndCheckApplicantGroupUpload(DTO.UploadData data, string fileName,
                                               DTO.RegistrationType regType,
                                               string testingNo, string examPlaceCode, DTO.UserProfile userProfile);
    }

    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public FileMetaData Metadata;
        [MessageBodyMember(Order = 0)]
        public Stream FileByteStream;
    }

    [DataContract]
    public class FileMetaData
    {
        public FileMetaData(
           string localFileName,
           string remoteFileName)
        {
            this.LocalFileName = localFileName;
            this.RemoteFileName = remoteFileName;
        }

        public FileMetaData(
           string localFileName,
           string remoteFileName,
           string targetFolder)
        {
            this.LocalFileName = localFileName;
            this.RemoteFileName = remoteFileName;
            this.TargetFolder = targetFolder;
        }

        [DataMember(Name = "localFilename", Order = 0, IsRequired = false)]
        public string LocalFileName;

        [DataMember(Name = "remoteFilename", Order = 1, IsRequired = false)]
        public string RemoteFileName;

        [DataMember(Name = "targetFolder", Order = 2, IsRequired = false)]
        public string TargetFolder;

        [DataMember(Name = "resMsg", Order = 3, IsRequired = false)]
        public string ResponseMessage;
    }

    [MessageContract]
    public class FileDownloadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public FileMetaData FileMetaData;
    }

    [MessageContract]
    public class FileDownloadReturnMessage
    {
        public FileDownloadReturnMessage(FileMetaData metaData, Stream stream)
        {
            this.DownloadedFileMetadata = metaData;
            this.FileByteStream = stream;
        }

        [MessageHeader(MustUnderstand = true)]
        public FileMetaData DownloadedFileMetadata;

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream;
    }

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string Id;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string userId;

        [MessageHeader(MustUnderstand = true)]
        public string oicUserName;

        [MessageHeader(MustUnderstand = true)]
        public string preNameCode;

        [MessageHeader(MustUnderstand = true)]
        public string firstName;

        [MessageHeader(MustUnderstand = true)]
        public string lastName;

        [MessageHeader(MustUnderstand = true)]
        public string sex;

        [MessageHeader(MustUnderstand = true)]
        public string memberType;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class RemoteFileInfoAddOic : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string oicEmpNo;

        [MessageHeader(MustUnderstand = true)]
        public string oicUserName;

        [MessageHeader(MustUnderstand = true)]
        public string preNameCode;

        [MessageHeader(MustUnderstand = true)]
        public string firstName;

        [MessageHeader(MustUnderstand = true)]
        public string lastName;

        [MessageHeader(MustUnderstand = true)]
        public string sex;

        [MessageHeader(MustUnderstand = true)]
        public string oicTypeCode;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}
