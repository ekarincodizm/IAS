using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DataServices.Properties;
using IAS.DTO.FileService;

using System.Drawing;

namespace IAS.DataServices.FileManager
{
    public class UploadFile : BaseFileController, IDisposable
    {
        private String _targetPath;
        private Stream _streamAttech;


        public UploadFile(UploadFileRequest request)
        {
            try
            {
                ConnectNetDrive();
                _targetPath = request.TargetContainer + @"\";

                DirectoryInfo di = new DirectoryInfo(Path.Combine( DefaultNetDrive , _targetPath));
                if (!di.Exists)
                {
                    di.Create();
                }
                _targetPath = Path.Combine(_targetPath, request.TargetFileName);
                _streamAttech = request.FileStream;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public string TargetPath  {
            get { return _targetPath ; }
            set {  _targetPath = value;  }
        }

        public Stream StreamAttech { 
            get { return _streamAttech;    } 
            set  {  _streamAttech = value; }
        }

        public UploadFileResponse Action()
        {
            
            UploadFileResponse response = new UploadFileResponse();
            try
            {
                using (FileStream outfile = new FileStream(Path.Combine(DefaultNetDrive , _targetPath), FileMode.Create))
                {
                    const int bufferSize = 1165536; // 64K
                   // 65536
                    Byte[] buffer = new Byte[bufferSize];
        
                    int bytesRead = _streamAttech.Read(buffer, 0, bufferSize);

                    if (bytesRead > 2147483647) {
                        response.Code = "0002";
                        response.Message = Resources.errorUploadFile_001;
                        return response;
                    }
                    while (bytesRead > 0)
                    {
                        outfile.Write(buffer, 0, bytesRead);
                        bytesRead = _streamAttech.Read(buffer, 0, bufferSize);
                    }

                    response.Code = "0000";
                    response.Message = Resources.infoAmendFile_Success; // "Success Save at " + serverFileName;
                    response.TargetFullName = _targetPath;
                   
                }
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = ex.Message;
                throw new ApplicationException(ex.Message);
            }
            finally {
                DisConnectNetDrive();
            }
           

            return response;
        }

        public void Dispose()
        {
            if (_streamAttech != null)
            {
                _streamAttech.Dispose();
                _streamAttech = null;
                base.DisConnectNetDrive(); 
            }
        }

 
    }
}