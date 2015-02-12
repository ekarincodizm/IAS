using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IAS.Utils;
using IAS.DTO.FileService;
using IAS.FileService.Properties;
using IAS.FileService.Helpers;


namespace IAS.FileService.FileManager
{
    public class ContainDetail : BaseFileController , IDisposable 
    {
         private String _targetPath;

         public ContainDetail(ContainDetailRequest request)
        {
            ConnectNetDrive();
            _targetPath =  request.TargetContainer;

    
          
     
        }

        public string TargetPath  {
            get { return _targetPath ; }
            set {  _targetPath = value;  }
        }


        public ContainDetailResponse Action()
        {
            
            ContainDetailResponse response = new ContainDetailResponse();
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(DefaultNetDrive, _targetPath));
                if (!directoryInfo.Exists)
                {
                    IList<FileInfo> fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        response.Files.Add(CryptoBase64.Encryption(fileInfo.FullName));
                    }
                }
                else
                {
                    throw new IOException(Resources.errorContainDetail_001);
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
            finally {
                DisConnectNetDrive();
            }
           

            return response;
        }



        public void Dispose()
        {
            base.DisConnectNetDrive();
        }
    }
}