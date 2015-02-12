using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using IAS.FileService;

namespace IAS.FileService.FileManager
{
    public class BaseFileController 
    {
        private NASDrive nasDrive;
        string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
        string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
        string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];

        public String DefaultNetDrive  { get { return _netDrive; } }
        public String UserNetDrive { get { return _userNetDrive; } }
        public String PassNetDrive { get { return _passNetDrive; } }

        protected void ConnectNetDrive()
        {
            this.nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);
        }

        protected void DisConnectNetDrive()
        {
            if (this.nasDrive != null)
            {
                this.nasDrive.Dispose();
            }
        }
        protected string GetContentType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return string.Empty;

            string contentType = string.Empty;
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/HTML";
                    break;

                case ".txt":
                    contentType = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                case ".docx":
                    contentType = "Application/msword";
                    break;

                case ".xls":
                case ".xlsx":
                    contentType = "Application/x-msexcel";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/GIF";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }

            return contentType;
        }

        
    }
}