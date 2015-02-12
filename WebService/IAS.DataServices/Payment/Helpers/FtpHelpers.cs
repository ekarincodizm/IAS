using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using IAS.Utils;

namespace IAS.DataServices.Payment.Helpers
{
    public class FtpHelpers
    {
        private static FTP ftp;
        private static string Host = ConfigurationManager.AppSettings["ftpHost"].ToString();
        private static string User = ConfigurationManager.AppSettings["ftpUser"].ToString();
        private static string Pass = ConfigurationManager.AppSettings["ftpPass"].ToString();

        public static void Upload(Stream localFile, string remoteFile)
        {
            try
            {
                ftp = new FTP(Host, User, Pass);
                ftp.CreateDirectoryByRemoteFile(remoteFile);
                ftp.Upload(localFile, remoteFile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool CheckFtpConnect()
        {
            ftp = new FTP(Host, User, Pass);
            return ftp.CheckFTPConnection();
        }
    }
}