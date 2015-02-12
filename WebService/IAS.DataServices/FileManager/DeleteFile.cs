using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DTO.FileService;
using IAS.DataServices.Properties;



namespace IAS.DataServices.FileManager
{
    public class DeleteFile : BaseFileController  , IDisposable 
    {
        private String _targetPath;

        public DeleteFile(DeleteFileRequest request)
        {
            ConnectNetDrive();
            _targetPath = request.TargetFileName;
            _targetPath = Path.Combine(DefaultNetDrive, _targetPath);
        }


        public DeleteFileResponse Action()
        {
            

            DeleteFileResponse response = new DeleteFileResponse();
            try
            {

                System.IO.FileInfo fileInfo = new FileInfo(Path.Combine(DefaultNetDrive, _targetPath));

                // check if exists
                if (!fileInfo.Exists) throw new FileNotFoundException(Resources.errorDeleteFile_FileNotFound, _targetPath);

                // open stream
                File.Delete(_targetPath);

          
                response.Code = "0000";
                response.Message = Resources.infoDeleteContainer_001;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
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