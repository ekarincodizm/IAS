using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DTO.FileService;
using IAS.DataServices.Properties;



namespace IAS.DataServices.FileManager
{
    public class MoveFile : BaseFileController,  IDisposable
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

        public MoveFile(MoveFileRequest request)
        {
            ConnectNetDrive();
            _targetPath = request.TargetContainer +
                            (String.IsNullOrEmpty(request.TargetContainer)
                            ? "" : @"\") + request.TargetFileName;
            _currentPath = request.CurrentContainer +
                            (String.IsNullOrEmpty(request.CurrentContainer)
                            ? "" : @"\") + request.CurrentFileName;

            DirectoryInfo di = null;
            if (!Directory.Exists(DefaultNetDrive + _targetPath))
            {
                di = Directory.CreateDirectory(DefaultNetDrive + request.TargetContainer);
            }
            _targetfileName = request.TargetFileName;
            _currentFileName = request.CurrentFileName;
            
        } 

        public MoveFileResponse Action()
        {
            
            MoveFileResponse response = new MoveFileResponse();
            try
            {
                FileInfo fileInfoTarget = new FileInfo(Path.Combine(DefaultNetDrive, _targetPath));
                FileInfo fileInfoCurrent = new FileInfo(Path.Combine(DefaultNetDrive, _currentPath));

                if (!fileInfoCurrent.Exists) throw new FileNotFoundException(String.Format("ไม่พบไฟล์ {0} ในระบบ", _currentFileName), _currentFileName);
                //if (fileInfoTarget.Exists) throw new FileNotFoundException("File is dupicate", _targetfileName);
                if (fileInfoTarget.Exists)
                {
                    File.Delete(fileInfoTarget.FullName);
                }
                File.Copy(fileInfoCurrent.FullName, fileInfoTarget.FullName);

                response.Code = "0000";
                response.Message = Resources.infoAmendFile_Success;
                response.TargetFullName = _targetPath;

            }
            catch (FileNotFoundException fex) {
                response.Code = "0002";
                response.Message = fex.Message;
            }
            catch (Exception ex)
            {
                response.Code = "0001";
                response.Message = Resources.errorMoveFile_001;
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