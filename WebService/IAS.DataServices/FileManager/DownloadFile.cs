using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DTO.FileService;
using IAS.DataServices.Properties;
using IAS.Common.Helpers;
using IAS.Utils;


namespace IAS.DataServices.FileManager
{
    public class DownloadFile : BaseFileController , IDisposable
    {
        private String _targetPath;
        private Stream _streamAttech;
        private String _fileName;
        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
        }

        public Stream StreamAttech
        {
            get { return _streamAttech; }
            set { _streamAttech = value; }
        }

        public DownloadFile(DownloadFileRequest request)
        {
            ConnectNetDrive();
            _targetPath = request.TargetContainer +
                                    (string.IsNullOrEmpty(request.TargetContainer)
                                        ? ""
                                        : @"\") + request.TargetFileName;

            FileInfo fi = new FileInfo(request.TargetFileName);
            _targetPath = Path.Combine(DefaultNetDrive, _targetPath);
            _fileName = request.TargetFileName;
        }
        public DownloadFileResponse Action()
        {
            ConnectNetDrive();
            DownloadFileResponse response = new DownloadFileResponse();

            try
            {


                System.IO.FileInfo fileInfo = new FileInfo(_targetPath);
                
                // check if exists
                if (!fileInfo.Exists) throw new FileNotFoundException(Resources.errorDeleteFile_FileNotFound, _fileName);

                response.HashCode = FileObject.GetHashSHA1(_targetPath); 

                // open stream
                System.IO.FileStream stream = new FileStream(_targetPath, FileMode.Open, FileAccess.Read);

                // ดึง Content Type คืนไปฝั่ง Web ที Consume Service
                response.ContentType = Utils.ContentTypeHelper.MimeType(fileInfo.Name);
                response.FileName = _fileName;
                response.Length = fileInfo.Length;
                response.FileByteStream = stream;
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
            if (_streamAttech != null)
            {
                _streamAttech.Dispose();
                _streamAttech = null;
                base.DisConnectNetDrive();
            }
        }
    }
}