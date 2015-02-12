using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DTO.FileService;
using IAS.FileService.Properties;

namespace IAS.FileService.FileManager
{
    public class DeleteContainer : BaseFileController , IDisposable
    {
         private String _targetPath;

         public DeleteContainer(DeleteContainerRequest request)
        {
            ConnectNetDrive();
            _targetPath = request.TargetContainer;
        }


         public DeleteContainerResponse Action()
        {
            

            DeleteContainerResponse response = new DeleteContainerResponse();
            try
            {
                System.IO.DirectoryInfo dirctoryInfo = new DirectoryInfo(Path.Combine(DefaultNetDrive, _targetPath));

                // check if exists
                if (!dirctoryInfo.Exists) throw new FileNotFoundException(Resources.String1, _targetPath);

                // Directory Delete
                Directory.Delete(dirctoryInfo.FullName, true);
          

          
                response.Code = "0000";
                response.Message = Resources.infoDeleteContainer_001;
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
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