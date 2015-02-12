using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DTO.FileService;
using IAS.DataServices.Properties;


namespace IAS.DataServices.FileManager
{
    public class AmendFile: BaseFileController,  IDisposable
    {
        private String _targetPath;
        private String _currentPath;
        private Stream _streamAttech;
        private String _targetfileName;
        private String _currentFileName;

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

        public AmendFile(AmendFileRequest request)
        {
            ConnectNetDrive();
            _targetPath = request.TargetContainer +
                            (String.IsNullOrEmpty(request.TargetContainer)
                            ? "" : @"\") + request.TargetFileName;
            _currentPath = request.CurrentContainer +
                            (String.IsNullOrEmpty(request.CurrentContainer)
                            ? "" : @"\") + request.CurrentFileName;
            _targetfileName = request.TargetFileName;
            _currentFileName = request.CurrentFileName;
            
        } 

        public AmendFileResponse Action()
        {
            
            AmendFileResponse response = new AmendFileResponse();
            try
            {
                FileInfo fileInfoTarget = new FileInfo(_targetPath);
                FileInfo fileInfoCurrent = new FileInfo(_currentPath);

                if (!fileInfoCurrent.Exists) throw new FileNotFoundException("File not Found", _currentFileName);
                if (fileInfoTarget.Exists) throw new FileNotFoundException("File is dupicate", _targetfileName);

                File.Copy(fileInfoCurrent.FullName, fileInfoTarget.FullName);

                response.Code = "0000";
                response.Message = Resources.infoAmendFile_Success;
                


                
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
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